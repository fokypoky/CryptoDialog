namespace CryptoLib.Encryptors;

public interface IEncryptor
{
    string Encrypt(int key, string message);
    string Decrypt(int key, string message);
}