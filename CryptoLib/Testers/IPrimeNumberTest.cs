using System.Numerics;

namespace CryptoLib.Testers;

public interface IPrimeNumberTest
{
    bool Test(BigInteger number);
}