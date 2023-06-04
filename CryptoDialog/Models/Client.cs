using System;
using System.Collections.Generic;
using System.Numerics;
using CryptoLib.Generators.Implementation;
using CryptoLib.Testers;
using CryptoLib.Testers.Implementation;

namespace CryptoDialog.Models;

public class Client
{
    public BigInteger P { get; set; }
    public BigInteger G { get; set; }
    public BigInteger K { get; set; }
    public BigInteger Kc { get; set; }
    public BigInteger Key { get; set; }

    public Client(BigInteger p, BigInteger a)
    {
        P = p;
        G = FindPrimitiveRoot(P);
        K = GeneratePrivateKey();
        Kc = BigInteger.ModPow(G, K, P);
    }
    public BigInteger GeneratePrivateKey()
    {
        // Генерация закрытого ключа случайным образом
        Random random = new Random();
        BigInteger privateKey = new BigInteger();
        do
        {
            byte[] bytes = new byte[P.ToByteArray().LongLength];
            random.NextBytes(bytes);
            privateKey = new BigInteger(bytes);
        }
        while (privateKey <= 1 || privateKey >= P - 1);

        return privateKey;
    }
    public void MakeKey(BigInteger kc)
    {
        Key = BigInteger.ModPow(kc, K, P);
    }

    public static BigInteger FindPrimitiveRoot(BigInteger prime)
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
    
    private static List<BigInteger> Factorize(BigInteger n)
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