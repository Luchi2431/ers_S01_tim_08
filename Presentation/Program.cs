using Applications;
using Infrastructure;
using Proxy.Services;
using System.Reflection;

namespace Presentation
{
    public class Program
    {
       
        static void Main(string[] args)
        {
            var storage = new InMemoryStorage();
            var deviceService = new DeviceService(storage);

            var deviceId = Guid.NewGuid().ToString();
            var deviceId2 = Guid.NewGuid().ToString();
           
            Console.WriteLine($"Pokrećem auto slanje za uređaj {deviceId}");
            deviceService.StartAutoSending(deviceId);

            Console.WriteLine($"Pokrećem auto slanje za uređaj {deviceId2}");
            deviceService.StartAutoSending(deviceId2);

            // Čekamo da se merenja pošalju
            Thread.Sleep(15000); //10 sekundi

            var proxy = new ProxyService(storage);

            var podaci1 = proxy.GetMerenja(deviceId);
            Console.WriteLine($"\n[CLIENT] Tražim merenja za uređaj {deviceId} preko Proxy-ja...");
            foreach (var m in podaci1)
            {
                Console.WriteLine($"Tip: {m.Tip},Vrednost: {m.Vrednost}, Vreme:{m.TimeStamp}");
            }
            Console.WriteLine("Poslednje merenje svakog uredjaja");
            var poslednje = proxy.GetLastValuesOfAllDevices();
            foreach( var m in poslednje)
            {
                Console.WriteLine($"Device:{m.Id},{m.Tip},{m.Vrednost},{m.TimeStamp}");
            }


            Console.WriteLine("Pritisni ENTER za izlaz");
            Console.ReadLine();

        }
    }
}
