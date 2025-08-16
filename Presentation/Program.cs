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
            var proxyService = new ProxyService(serverService);

            //Simulacija dva uredjaja
            string deviceId1 = "device1";
            string deviceId2 = "device2";

            // Pokretanje automatskog slanja merenja za oba uredjaja
            deviceService.StartAutoSending(deviceId1);
            deviceService.StartAutoSending(deviceId2);

            var app = new ClientConsoleApp(proxyService);
            app.Run();
        }
    }
}
