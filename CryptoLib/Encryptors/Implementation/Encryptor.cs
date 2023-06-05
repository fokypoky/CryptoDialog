using System.Numerics;
using CryptoLib.Generators;

namespace CryptoLib.Encryptors.Implementation;

public class Encryptor : IEncryptor
{

    public Encryptor()
    {
    }
    
    public string Encrypt(BigInteger key, string message) => message;
    public string Decrypt(BigInteger key, string message) => message;
}