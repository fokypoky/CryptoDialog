using System.Numerics;
using CryptoLib.Generators.Implementation;
using CryptoLib.Testers;
using CryptoLib.Testers.Implementation;

namespace TestApp;

class Program
{
    private static int length = 256;
    static void Main()
    {
        var generator = new PrimeNumberGenerator(new List<IPrimeNumberTest>()
        {
            new FermatTest(), new MillerRabinTest()
        });

        var p = generator.Generate(length);
        //Console.WriteLine(CalculatePrimitiveRoot(p));
        //Console.WriteLine(CalculatePrimitiveRoot(p));
        Console.ReadLine();
    }
}