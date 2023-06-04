using System.Collections.Generic;
using System.Numerics;
using CryptoDialog.Models;
using CryptoDialog.ViewModels.Base;
using CryptoLib.Encryptors;
using CryptoLib.Generators;
using CryptoLib.Generators.Implementation;
using CryptoLib.Testers;
using CryptoLib.Testers.Implementation;

namespace CryptoDialog.ViewModels;

public class MainWindowViewModel : ViewModel
{
    private IEncryptor _encryptor;
    private List<IPrimeNumberTest> _primeNumberTests;
    private IPrimeNumberGenerator _primeNumberGenerator;

    private Client _clientA;
    private Client _clientB;

    private BigInteger _p;
    private BigInteger _a;

    public BigInteger P
    {
        get => _p;
        set => SetField(ref _p, value);
    }

    public BigInteger A
    {
        get => _a;
        set => SetField(ref _a, value);
    }
    
    public MainWindowViewModel()
    {
        _primeNumberTests = new List<IPrimeNumberTest>()
        {
            new FermatTest(),
            new MillerRabinTest()
        };

        _primeNumberGenerator = new PrimeNumberGenerator(_primeNumberTests);
        
        //_encryptor = new Encryptor();
    }
}