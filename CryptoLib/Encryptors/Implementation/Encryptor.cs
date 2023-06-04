using CryptoLib.Generators;

namespace CryptoLib.Encryptors.Implementation;

public class Encryptor : IEncryptor
{

    public Encryptor()
    {
    }
    
    public string Encrypt(int key, string message) => message;
    public string Decrypt(int key, string message) => message;
}