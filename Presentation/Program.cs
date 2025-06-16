using Applications;
using Infrastructure;

namespace Presentation
{
    public class Program
    {
       
        static void Main(string[] args)
        {
            var storage = new InMemoryStorage();

            // Servis koji upravlja logikom uredjaja
            var deviceService = new DeviceService(storage);

            var deviceId = Guid.NewGuid().ToString();
            Console.WriteLine($"Pokrećem auto slanje za uređaj {deviceId}");

            deviceService.StartAutoSending(deviceId);

            Console.WriteLine("Pritisni ENTER za izlaz");
            Console.ReadLine();

        }
    }
}
