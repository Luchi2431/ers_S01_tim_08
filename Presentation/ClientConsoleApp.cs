using Proxy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class ClientConsoleApp
    {
        private readonly ProxyService _proxyService;

        public ClientConsoleApp(ProxyService proxyService)
        {
            _proxyService = proxyService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Proxy klijent aplikacija");
                Console.WriteLine("1. Prikaz svih merenja za uredjaj");
                Console.WriteLine("2. Prikaz poslednjeg merenja za sve uredjaje");
                Console.WriteLine("3. Prikazi sva analogna merenja");
                Console.WriteLine("4. Prikazi sva digitalna merenja");
                Console.WriteLine("0. Izlaz");
                Console.Write("Izaberite opciju: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PrikaziMerenjaZaUredjaj();
                        break;
                    case "2":
                        PrikazPoslednjegMerenjaZaUredjaj();
                        break;
                    case "3":
                        PrikaziPoslednjaMerenjaSvihUredjaja();
                        break;
                    case "4":
                        PrikaziSvaAnalognaMerenja();
                        break;
                    case "5":
                        PrikaziSvaDigitalnaMerenja();
                        break;
                    case "0":
                        Console.WriteLine("Izlaz iz aplikacije...");
                        return;
                    default:
                        Console.WriteLine("Nepoznata opcija, molimo pokušajte ponovo.");
                        break;
                }
                Console.WriteLine("Pritisnite enter za nastavak...");
                Console.ReadLine();
            }
        }

        private void PrikaziMerenjaZaUredjaj()
        {
            Console.WriteLine("Unesite ID uredjaja:");
            var deviceId = Console.ReadLine();

            var merenja = _proxyService.GetMerenja(deviceId);

            if(merenja.Count == 0)
            {
                Console.WriteLine("Nema merenja za ovaj uredjaj");
                return;
            }

            Console.WriteLine($"Merenja za uredjaj {deviceId}:");
            foreach (var mer in merenja)
            {
                Console.WriteLine($"Id Merenja:{mer.Id},Tip merenja:{mer.Tip}," +
                    $"Vrednost merenja:{mer.Vrednost},Vreme merenja:{mer.TimeStamp}");
            }
        }

        private void PrikazPoslednjegMerenjaZaUredjaj()
        {
                
        }

        private void PrikaziPoslednjaMerenjaSvihUredjaja()
        {
            var poslednjaMerenja = _proxyService.GetLastValuesOfAllDevices();

            if (poslednjaMerenja.Count == 0)
            {
                Console.WriteLine("Nema merenja za nijedan uredjaj.");
                return;
            }

            Console.WriteLine("Poslednja merenja svih uredjaja:");
            foreach (var mer in poslednjaMerenja)
            {
                Console.WriteLine($"Id Uredjaja:{mer.Id},Tip merenja:{mer.Tip}," +
                    $"Vrednost merenja:{mer.Vrednost},Vreme merenja:{mer.TimeStamp}");
            }
        }

        private void PrikaziSvaAnalognaMerenja()
        {
            var analognaMerenja = _proxyService.GetAllAnalog();
            if (analognaMerenja.Count == 0)
            {
                Console.WriteLine("Nema analognih merenja");
                return;
            }
            Console.WriteLine("Sva analogna merenja:");
            foreach (var mer in analognaMerenja)
            {
                Console.WriteLine($"Id Uredjaja:{mer.Id},Tip merenja:{mer.Tip}," +
                    $"Vrednost merenja:{mer.Vrednost},Vreme merenja:{mer.TimeStamp}");
            }
        }

        private void PrikaziSvaDigitalnaMerenja()
        {
            var digitalnaMerenja = _proxyService.GetAllDigital();
            if (digitalnaMerenja.Count == 0)
            {
                Console.WriteLine("Nema analognih merenja");
                return;
            }
            Console.WriteLine("Sva analogna merenja:");
            foreach (var mer in digitalnaMerenja)
            {
                Console.WriteLine($"Id Uredjaja:{mer.Id},Tip merenja:{mer.Tip}," +
                    $"Vrednost merenja:{mer.Vrednost},Vreme merenja:{mer.TimeStamp}");
            }
        }

    }
}
