using Applications;
using Domain.Enums;
using Domain.Models;
using Infrastructure;
using Proxy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer.Tests
{
    public class ProxyServiceTests
    {
        [Fact]
        public void GetMerenja_ShouldReturnFromServer_WhenCacheEmpty()
        {
            var storage = new InMemoryStorage("");
            var server = new ServerService(storage);
            var proxy = new ProxyService(server);

            var merenje = new Merenja { Id = "m1", Tip = TipMereneVrednosti.ANALOGNA, Vrednost = 15, TimeStamp = DateTime.UtcNow };
            server.PrimiMerenje("device1", merenje);

            var result = proxy.GetMerenja("device1");

            Assert.Single(result);
            Assert.Equal(15, result[0].Vrednost);
        }

        [Fact]
        public void GetAllAnalog_ShouldReturnOnlyAnalogMerenja()
        {
            var storage = new InMemoryStorage("");
            var server = new ServerService(storage);
            var proxy = new ProxyService(server);

            server.PrimiMerenje("device1", new Merenja { Id = "m1", Tip = TipMereneVrednosti.ANALOGNA, Vrednost = 10, TimeStamp = DateTime.UtcNow });
            server.PrimiMerenje("device1", new Merenja { Id = "m2", Tip = TipMereneVrednosti.DIGITALNA, Vrednost = 20, TimeStamp = DateTime.UtcNow });

            var analog = proxy.GetAllAnalog();

            Assert.Single(analog);
            Assert.Equal(TipMereneVrednosti.ANALOGNA, analog[0].Tip);
        }
    }
}
