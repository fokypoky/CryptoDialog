using System;
using System.Numerics;

namespace CryptoDialog.Models;

public class Client
{
    public BigInteger P { get; set; }
    public BigInteger A { get; set; }
    public BigInteger K { get; set; }
    public BigInteger Kc { get; set; }

    public Client(BigInteger p, BigInteger a)
    {
        P = p;
        A = a;
        SetK();
        SetKc();
    }
    
    private void SetK() => K = new Random().Next(2, 100);
    private void SetKc() => Kc =  BigInteger.Pow(K, (int)A) % P;
    
}