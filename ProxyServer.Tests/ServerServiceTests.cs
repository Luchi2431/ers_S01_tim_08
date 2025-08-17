using Applications;
using Domain.Enums;
using Domain.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer.Tests
{
    
    public class ServerServiceTests
    {
        [Fact]
        public void PrimiMerenje_ShouldSaveMerenje()
        {
            // Arrange
            var storage = new InMemoryStorage("");
            var server = new ServerService(storage);
            var merenje = new Merenja
            {
                Id = "m1",
                Tip = TipMereneVrednosti.ANALOGNA,
                Vrednost = 42.0,
                TimeStamp = DateTime.UtcNow
            };

            // Act
            server.PrimiMerenje("device1", merenje);
            var result = server.VratiMerenjaZaUredjaj("device1");

            // Assert
            Assert.Single(result);
            Assert.Equal(42.0, result[0].Vrednost);
            Assert.Equal(TipMereneVrednosti.ANALOGNA, result[0].Tip);
        }
        [Fact]
        public void GetLastUpdateTime_ShouldReturnCorrectTime()
        {
            var storage = new InMemoryStorage("");
            var server = new ServerService(storage);

            var m1 = new Merenja { Id = "m1", Tip = TipMereneVrednosti.ANALOGNA, Vrednost = 10, TimeStamp = DateTime.UtcNow.AddMinutes(-5) };
            var m2 = new Merenja { Id = "m2", Tip = TipMereneVrednosti.DIGITALNA, Vrednost = 20, TimeStamp = DateTime.UtcNow };

            server.PrimiMerenje("device1", m1);
            server.PrimiMerenje("device1", m2);

            var lastUpdate = server.GetLastUpdateTime("device1");

            Assert.Equal(m2.TimeStamp, lastUpdate);
        }

    }
}
