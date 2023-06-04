using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using CryptoDialog.Infrastructure.Commands;
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

    private string _log = "";

    public string Log
    {
        get => _log;
        set => SetField(ref _log, value);
    }
    
    public Client ClientA
    {
        get => _clientA;
        set => SetField(ref _clientA, value);
    }

    public Client ClientB
    {
        get => _clientB;
        set => SetField(ref _clientB, value);
    }
    
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

    #region Commands

    public ICommand GeneratePaCommand
    {
        get => new RelayCommand((object p) =>
        {
            Log = "";
            int g = new Random().Next(2, 30);
            BigInteger prime = _primeNumberGenerator.Generate(16);
            ClientA = new Client(prime, g);
            
            Log += $"\nP = {prime} - большое простое число. Прошло проверку тестами Ферма и Миллера-Рабина";
            Log += $"\nG = {g} - обратное числу {prime}";
            Log += $"\n\nКлиент А: Закрытый ключ = {ClientA.K}, Открытый ключ = {ClientA.Kc}";
            
            ClientB = new Client(prime, g);
            Log += $"\nКлиент Б: Закрытый ключ = {ClientB.K}, Открытый ключ = {ClientB.Kc}";
        });
    }

    public ICommand MakeKeyCommand
    {
        get => new RelayCommand((object p) =>
        {
            if (ClientA == null || ClientB == null)
            {
                MessageBox.Show("Не установлены параметры для генерации ключа");
                return;
            }

            Log += "\n-------------------";
            Log += "\nКлиент А и Клиент Б обмениваются открытыми ключами и генерируют общий ключ";
            
            ClientA.MakeKey(ClientB.Kc);
            ClientB.MakeKey(ClientA.Kc);
            Log += $"\nКлиент А: Key = {ClientB.Kc} ^ {ClientA.K} mod {ClientA.P} = {ClientA.Key}";
            Log += $"\nКлиент Б: Key = {ClientA.Kc} ^ {ClientB.K} mod {ClientB.P} = {ClientB.Key}";
           
            Log += "\n-------------------";
        });
    }
    #endregion
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