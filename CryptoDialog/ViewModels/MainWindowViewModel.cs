using System.Collections.Generic;
using CryptoDialog.ViewModels.Base;
using CryptoLib.Encryptors;
using CryptoLib.Encryptors.Implementation;
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
    
    public MainWindowViewModel()
    {
        _primeNumberTests = new List<IPrimeNumberTest>()
        {
            new FermatTest(),
            new MillerRabinTest()
        };

        _primeNumberGenerator = new PrimeNumberGenerator(_primeNumberTests);

        _encryptor = new Encryptor();
    }
}