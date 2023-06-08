using System.Numerics;

namespace CryptoLib.Generators;

public interface IPrimeNumberOperation
{
    BigInteger Operate(BigInteger prime);
}