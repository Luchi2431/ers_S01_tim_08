using Applications;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer.Tests
{
    public class DeviceServiceTests
    {
        [Fact]
        public void StartAutoSending_ShouldAddMerenjaToServer()
        {
            var storage = new InMemoryStorage("");
            var server = new ServerService(storage);
            var deviceService = new DeviceService(server);

            string deviceId = "device1";
            deviceService.StartAutoSending(deviceId);

            // Čekamo 6 sekundi jer timer šalje svakih 5 sekundi (za test)
            Thread.Sleep(6000);

            var merenja = server.VratiMerenjaZaUredjaj(deviceId);
            Assert.NotEmpty(merenja);
        }
    }
}
