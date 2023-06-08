using System.Numerics;

namespace CryptoLib.Generators.Implementation;

public class PublicKeyGenerator : IKeyGenerator
{
    private BigInteger _otherPublicKey;
    private BigInteger _privateKey;
    private BigInteger _prime;
    public PublicKeyGenerator(BigInteger otherPublicKey, BigInteger privateKey, BigInteger prime)
    {
        _otherPublicKey = otherPublicKey;
        _privateKey = privateKey;
        _prime = prime;
    }

    public BigInteger GenerateKey() => BigInteger.ModPow(_otherPublicKey, _privateKey, _prime);
}