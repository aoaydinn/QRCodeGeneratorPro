using System.Text;
using System.Text.Json;
using System.Drawing.Imaging;
using System.Net;

namespace QrCodeApp.Core;

public class ApiServer
{
    private static HttpListener? listener;
    private static Thread? listenerThread;
    private static bool isRunning = false;

    public static bool IsRunning => isRunning;

    public static void Start(int port = 5000)
    {
        if (isRunning) return;

        listener = new HttpListener();
        listener.Prefixes.Add($"http://localhost:{port}/");
        listener.Start();
        isRunning = true;

        listenerThread = new Thread(new ThreadStart(Listen));
        listenerThread.Start();
    }

    public static void Stop()
    {
        if (!isRunning) return;

        isRunning = false;
        listener?.Stop();
        listener?.Close();
        listener = null;
    }

    private static void Listen()
    {
        while (isRunning && listener != null)
        {
            try
            {
                var context = listener.GetContext();
                ThreadPool.QueueUserWorkItem((state) => ProcessRequest(context));
            }
            catch (HttpListenerException)
            {
                break;
            }
            catch (Exception)
            {
            }
        }
    }

    private static void ProcessRequest(HttpListenerContext context)
    {
        try
        {
            if (context.Request.HttpMethod == "POST" && context.Request.Url?.AbsolutePath == "/api/qr")
            {
                using var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding);
                string body = reader.ReadToEnd();
                using var jsonDoc = JsonDocument.Parse(body);
                var root = jsonDoc.RootElement;

                string content = root.GetProperty("content").GetString() ?? "";
                string barcodeTypeStr = root.TryGetProperty("barcodeType", out var typeProp) ? typeProp.GetString() ?? "QRCode" : "QRCode";
                int size = root.TryGetProperty("size", out var sizeProp) ? sizeProp.GetInt32() : 400;
                string format = root.TryGetProperty("format", out var formatProp) ? formatProp.GetString() ?? "png" : "png";
                string ecc = root.TryGetProperty("ecc", out var eccProp) ? eccProp.GetString() ?? "M" : "M";
                string moduleShapeStr = root.TryGetProperty("moduleShape", out var msProp) ? msProp.GetString() ?? "Square" : "Square";
                string eyeShapeStr = root.TryGetProperty("eyeShape", out var esProp) ? esProp.GetString() ?? "Square" : "Square";
                string frameStyleStr = root.TryGetProperty("frameStyle", out var fsProp) ? fsProp.GetString() ?? "None" : "None";
                bool useGradient = root.TryGetProperty("useGradient", out var ugProp) && ugProp.GetBoolean();

                if (!Enum.TryParse(barcodeTypeStr, out BarcodeType barcodeType))
                    barcodeType = BarcodeType.QRCode;
                if (!Enum.TryParse(moduleShapeStr, out ModuleShape moduleShape))
                    moduleShape = ModuleShape.Square;
                if (!Enum.TryParse(eyeShapeStr, out EyeShape eyeShape))
                    eyeShape = EyeShape.Square;
                if (!Enum.TryParse(frameStyleStr, out FrameStyle frameStyle))
                    frameStyle = FrameStyle.None;

                var barcode = BarcodeGenerator.GenerateBarcode(
                    content,
                    barcodeType,
                    ecc,
                    width: size,
                    height: size,
                    moduleShape: moduleShape,
                    eyeShape: eyeShape,
                    frameStyle: frameStyle,
                    useGradient: useGradient
                );

                ImageFormat imgFormat = format.ToLower() == "jpg" ? ImageFormat.Jpeg : ImageFormat.Png;
                using var stream = new MemoryStream();
                barcode.Save(stream, imgFormat);
                byte[] imageBytes = stream.ToArray();

                context.Response.ContentType = imgFormat == ImageFormat.Jpeg ? "image/jpeg" : "image/png";
                context.Response.ContentLength64 = imageBytes.Length;
                context.Response.OutputStream.Write(imageBytes, 0, imageBytes.Length);
                context.Response.Close();
            }
            else
            {
                string response = "{\"status\": \"QR Code API is running. Use POST /api/qr with JSON: {content, barcodeType, size, format, ecc, moduleShape, eyeShape, frameStyle, useGradient}\"}";
                byte[] buffer = Encoding.UTF8.GetBytes(response);
                context.Response.ContentType = "application/json";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.Close();
            }
        }
        catch (JsonException ex)
        {
            string errorResponse = $"{{\"error\": \"{ex.Message}\"}}";
            byte[] buffer = Encoding.UTF8.GetBytes(errorResponse);
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.Close();
        }
        catch (ArgumentException ex)
        {
            string errorResponse = $"{{\"error\": \"{ex.Message}\"}}";
            byte[] buffer = Encoding.UTF8.GetBytes(errorResponse);
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.Close();
        }
        catch (Exception ex)
        {
            string errorResponse = $"{{\"error\": \"{ex.Message}\"}}";
            byte[] buffer = Encoding.UTF8.GetBytes(errorResponse);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.Close();
        }
    }
}
