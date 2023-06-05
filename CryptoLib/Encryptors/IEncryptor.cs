using System.Numerics;

namespace CryptoLib.Encryptors;

public interface IEncryptor
{
    string Encrypt(BigInteger key, string message);
    string Decrypt(BigInteger key, string message);
}