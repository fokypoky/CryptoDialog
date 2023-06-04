using System.Numerics;

namespace CryptoLib.Testers.Implementation;

public class FermatTest : IPrimeNumberTest
{
    private Random _random;
    private int _iterationCount;
    
    public FermatTest(int iterationCount = 10)
    {
        _iterationCount = iterationCount;
        _random = new Random();
    }

    public bool Test(BigInteger number)
    {
        if (number < 2)
        {
            return false;
        }
        if (number == 2)
        {
            return true;
        }

        for (int i = 0; i < _iterationCount; i++)
        {
            BigInteger a = GenerateRandomBase(number);
            if (BigInteger.ModPow(a, number - 1, number) != 1)
            {
                return false;
            }
        }

        return true;
    }

    private BigInteger GenerateRandomBase(BigInteger number)
    {
        byte[] bytes = new byte[number.ToByteArray().LongLength];
        BigInteger a;
        
        do
        {
            _random.NextBytes(bytes);
            a = new BigInteger(bytes);
        } while (a < 2 || a >= number - 2);

        return a;
    }
    
}