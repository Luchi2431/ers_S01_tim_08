using Applications;
using Infrastructure;
using Proxy.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Presentation
{
    public class Program
    {
        static void Main(string[] args)
        {
            var storage = new InMemoryStorage();
            var serverService = new ServerService(storage);
            var deviceService = new DeviceService(serverService);

            // Kreiramo listu uređaja za koje startujemo auto slanje
            var deviceIds = new List<string>
            {
                "uredjaj1",
                "uredjaj2"
            };

            // Startujemo auto slanje za svaki uređaj
            foreach (var deviceId in deviceIds)
            {
                Console.WriteLine($"Pokrećem auto slanje za uređaj {deviceId}");
                deviceService.StartAutoSending(deviceId);
            }

            // Čekamo da se pošalju neka merenja
            Thread.Sleep(15000);

            var proxy = new ProxyService(serverService);

            // Ispisujemo merenja za svaki uređaj posebno
            foreach (var deviceId in deviceIds)
            {
                var podaci = proxy.GetMerenja(deviceId);
                Console.WriteLine($"\n[CLIENT] Tražim merenja za uređaj {deviceId} preko Proxy-ja...");
                if (podaci.Count == 0)
                {
                    Console.WriteLine("Nema merenja za ovaj uređaj.");
                }
                else
                {
                    foreach (var m in podaci)
                    {
                        Console.WriteLine($"Tip: {m.Tip}, Vrednost: {m.Vrednost}, Vreme: {m.TimeStamp}");
                    }
                }
            }

            // Ispis poslednjeg merenja za sve uređaje
            Console.WriteLine("\nPoslednje merenje svakog uređaja:");
            var poslednje = proxy.GetLastValuesOfAllDevices();
            foreach (var m in poslednje)
            {
                Console.WriteLine($"MerenjeId: {m.Id}, Tip: {m.Tip}, Vrednost: {m.Vrednost}, Vreme: {m.TimeStamp}");
            }

            Console.WriteLine("\nPritisni ENTER za izlaz");
            Console.ReadLine();
        }
    }
}
