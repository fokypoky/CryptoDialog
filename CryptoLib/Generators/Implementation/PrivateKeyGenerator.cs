using System.Numerics;

namespace CryptoLib.Generators.Implementation;

public class PrivateKeyGenerator :  IKeyGenerator
{
    private BigInteger _prime;
    public PrivateKeyGenerator(BigInteger prime)
    {
        _prime = prime;
    }
    public BigInteger GenerateKey()
    {
        Random random = new Random();
        BigInteger privateKey = new BigInteger();
        do
        {
            byte[] bytes = new byte[_prime.ToByteArray().LongLength];
            random.NextBytes(bytes);
            privateKey = new BigInteger(bytes);
        }
        while (privateKey <= 1 || privateKey >= _prime - 1);

        return privateKey;
    }
}