using System.Numerics;

namespace CryptoLib.Generators.Implementation;

public class PrimitiveRootsFinder : IPrimeNumberOperation
{
    public BigInteger Operate(BigInteger prime)
    {
        var factors = Factorize(prime - 1);
        
        for (BigInteger root = 2; root < prime; root++)
        {
            bool isPrimitiveRoot = true;
            
            foreach (var factor in factors)
            {
                if (BigInteger.ModPow(root, (prime - 1) / factor, prime) == 1)
                {
                    isPrimitiveRoot = false;
                    break;
                }
            }
            
            if (isPrimitiveRoot)
                return root;
        }
        
        throw new Exception("Primitive root not found.");
    }
    private List<BigInteger> Factorize(BigInteger n)
    {
        var factors = new List<BigInteger>();
        
        for (BigInteger i = 2; i * i <= n; i++)
        {
            while (n % i == 0)
            {
                factors.Add(i);
                n /= i;
            }
        }
        
        if (n > 1)
            factors.Add(n);
        
        return factors;
    }
}