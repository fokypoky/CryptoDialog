using System.Numerics;

namespace CryptoLib.Generators;

public interface IKeyGenerator
{
    BigInteger GenerateKey();
}