using System.Security.Cryptography;
using System.Text;

namespace QrCodeApp.Core;

public static class EncryptionHelper
{
    public static string Encrypt(string plainText, string password)
    {
        byte[] salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, salt, 32, HashAlgorithmName.SHA256, 10000);
        byte[] iv = new byte[16];
        RandomNumberGenerator.Fill(iv);
        
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor();
        byte[] cipherText = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        byte[] result = new byte[salt.Length + iv.Length + cipherText.Length];
        salt.CopyTo(result, 0);
        iv.CopyTo(result, salt.Length);
        cipherText.CopyTo(result, salt.Length + iv.Length);

        return Convert.ToBase64String(result);
    }

    public static string Decrypt(string encryptedText, string password)
    {
        try
        {
            byte[] allBytes = Convert.FromBase64String(encryptedText);
            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            int cipherLength = allBytes.Length - salt.Length - iv.Length;

            Array.Copy(allBytes, 0, salt, 0, salt.Length);
            Array.Copy(allBytes, salt.Length, iv, 0, iv.Length);

            byte[] cipherText = new byte[cipherLength];
            Array.Copy(allBytes, salt.Length + iv.Length, cipherText, 0, cipherLength);

            byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, salt, 32, HashAlgorithmName.SHA256, 10000);
            byte[] plainBytes = new byte[cipherLength];

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
        catch
        {
            return "Şifre çözülemedi! Hatalı şifre.";
        }
    }

    public static string WrapInJson(string encryptedData)
    {
        return $"{{\"enc\": \"{encryptedData}\", \"v\": \"1.0\"}}";
    }
}
