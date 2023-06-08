using System;
using System.Collections.Generic;
using System.Numerics;
using CryptoLib.Generators;
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

    private IPrimeNumberOperation _rootsFinder;
    private IKeyGenerator _privateKeyGenerator;
    private IKeyGenerator _publicKeyGenerator;

    public Client(BigInteger p, BigInteger a)
    {
        _rootsFinder = new PrimitiveRootsFinder();
        _privateKeyGenerator = new PrivateKeyGenerator(p);
        
        P = p;
        G = _rootsFinder.Operate(P);
        K = _privateKeyGenerator.GenerateKey();
        Kc = BigInteger.ModPow(G, K, P);
    }

    public void SetSharedKey(BigInteger otherPublicKey)
    {
        if (_publicKeyGenerator == null)
        {
            _publicKeyGenerator = new PublicKeyGenerator(otherPublicKey, K, P);
        }
        Key = _publicKeyGenerator.GenerateKey();
    }
}