using System.Numerics;
using CryptoLib.Testers;

namespace CryptoLib.Generators;

public interface IPrimeNumberGenerator
{
    public List<IPrimeNumberTest> PrimeNumberTests { get; set; }
    BigInteger Generate(int numBits);
}