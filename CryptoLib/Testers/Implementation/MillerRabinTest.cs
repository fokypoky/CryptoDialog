using System.Numerics;

namespace CryptoLib.Testers.Implementation;

public class MillerRabinTest : IPrimeNumberTest
{
    private Random _random;
    private int _iterationCount;
    
    public MillerRabinTest(int iterationCount = 10)
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

        if (number == 2 || number == 3)
        {
            return true;
        }

        if (number % 2 == 0 || number % 3 == 0)
        {
            return false;
        }

        BigInteger d = number - 1;
        int s = 0;

        if (d % 2 == 0)
        {
            d /= 2;
            s++;
        }

        for (int i = 0; i < _iterationCount; i++)
        {
            BigInteger a = GenerateRandomBase(number);
            BigInteger x = BigInteger.ModPow(a, d, number);

            if (x == 1 || x == number - 1)
            {
                continue;
            }

            bool isPrime = false;

            for (int r = 1; r < s; r++)
            {
                x = BigInteger.ModPow(x, 2, number);
                if (x == 1)
                {
                    return false;
                }

                if (x == number - 1)
                {
                    isPrime = true;
                    break;
                }
            }
            
            if (!isPrime)
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