using System.Numerics;
using CryptoLib.Testers;

namespace CryptoLib.Generators.Implementation;

public class PrimeNumberGenerator : IPrimeNumberGenerator
{
    private Random _random;
    public List<IPrimeNumberTest> PrimeNumberTests { get; set; }

    public PrimeNumberGenerator(List<IPrimeNumberTest> primeNumberTests)
    {
        _random = new Random();
        PrimeNumberTests = primeNumberTests;
    }

    public BigInteger Generate(int numBits)
    {
        while (true)
        {
            BigInteger candidate = GenerateRandomNumber(numBits);
            if (IsPrime(candidate))
            {
                return candidate;
            }
        }
    }

    private BigInteger GenerateRandomNumber(int numBits)
    {
        byte[] bytes = new byte[numBits / 8];
        _random.NextBytes(bytes);
        return new BigInteger(bytes);
    }

    private bool IsPrime(BigInteger number)
    {
        foreach (var test in PrimeNumberTests)
        {
            if (!test.Test(number))
            {
                return false;
            }
        }
        return true;
    }
}