using System.Numerics;
using CryptoLib.Generators.Implementation;
using CryptoLib.Testers;
using CryptoLib.Testers.Implementation;

namespace TestApp;

class Program
{
    private static int length = 512;
    static void Main()
    {
        var generator = new PrimeNumberGenerator(new List<IPrimeNumberTest>()
        {
            new FermatTest(), new MillerRabinTest()
        });

        var p = generator.Generate(length);
        Console.WriteLine(p);
        
    }
}