using Applications;
using Infrastructure;

namespace Presentation
{
    public class Program
    {
       
        static void Main(string[] args)
        {
            var storage = new InMemoryStorage();
            var serverService = new ServerService(storage);
            var deviceService = new DeviceService(serverService);

            var deviceId = Guid.NewGuid().ToString();
            var deviceId2 = Guid.NewGuid().ToString();
            var deviceId3 = Guid.NewGuid().ToString();
            Console.WriteLine($"Pokrećem auto slanje za uređaj {deviceId}, {deviceId2}, {deviceId3}");

            deviceService.StartAutoSending(deviceId);
            deviceService.StartAutoSending(deviceId2);
            deviceService.StartAutoSending(deviceId3);


            Console.WriteLine("Pritisni ENTER za izlaz");
            Console.ReadLine();

        }
    }
}
