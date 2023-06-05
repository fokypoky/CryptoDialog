using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLib.Encryptors.Implementation;

public class AESEncryptor : IEncryptor
{
    private byte[] PadBytesArray(byte[] inputArray, int targetSize)
    {
        byte[] newArr = new byte[targetSize];
        Array.Copy(inputArray, newArr, Math.Min(inputArray.Length, targetSize));
        
        return newArr;
    }

    private byte[] TruncateBytesArray(byte[] inputArray, int targetSize)
    {
        byte[] newArr = new byte[targetSize];
        for (int i = 0; i < targetSize; i++)
        {
            newArr[i] = inputArray[i];
        }

        return newArr;
    }
    
    public string Encrypt(BigInteger key, string message)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key.ToString());
        byte[] paddedKey = new byte[32]; // 32 байта для AES-256

        Array.Copy(keyBytes, paddedKey, Math.Min(keyBytes.Length, paddedKey.Length));

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = paddedKey;
            aesAlg.Mode = CipherMode.ECB; // Используется режим шифрования ECB (электронный кодовый блок)
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            byte[] encryptedBytes;
            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(message);
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }
            }
            return Convert.ToBase64String(encryptedBytes);
        }
    }

    public string Decrypt(BigInteger key, string message)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key.ToString());
        byte[] paddedKey = new byte[32]; // 32 байта для AES-256

        Array.Copy(keyBytes, paddedKey, Math.Min(keyBytes.Length, paddedKey.Length));

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = paddedKey;
            aesAlg.Mode = CipherMode.ECB; // Используется режим шифрования ECB (электронный кодовый блок)
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            byte[] cipherBytes = Convert.FromBase64String(message);
            using (var msDecrypt = new System.IO.MemoryStream(cipherBytes))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
