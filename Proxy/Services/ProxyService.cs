using Applications;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proxy.Services
{
    public class ProxyService
    {
        private readonly ServerService _serverService;
        private readonly Dictionary<string, ProxyCacheItem> _cache = new Dictionary<string, ProxyCacheItem>();
        private readonly Timer _cleanupTimer;

        public ProxyService(ServerService serverService)
        {
            _serverService = serverService;

            // Pokretanje timera za čišćenje keša
            _cleanupTimer = new Timer(_ => Cleanup(), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        }

        public List<Merenja> GetMerenja(string deviceId)
        {
            //Provera da li je uredjaj u kesu
            if (_cache.ContainsKey(deviceId))
            {
                var cacheItem = _cache[deviceId];
                // Provera da li je kes azuriran
                var serverLastUpdate = _serverService.GetLastUpdateTime(deviceId);
                if (serverLastUpdate > cacheItem.LastUpdated)
                {
                    return RefreshFromServer(deviceId);
                }
                cacheItem.LastAccessed = DateTime.UtcNow; // Ažuriraj vreme pristupa
                return cacheItem.Merenja;
            }
            else
            {
                return RefreshFromServer(deviceId);
            }
        }

        //Ova funkcija osvežava podatke iz servera i ažurira kes
        public List<Merenja> RefreshFromServer(string deviceId)
        {
            var merenja = _serverService.VratiMerenjaZaUredjaj(deviceId);
            var updated = _serverService.GetLastUpdateTime(deviceId);
            
            _cache[deviceId] = new ProxyCacheItem
            {
                Merenja = merenja,
                LastUpdated = updated,
                LastAccessed = DateTime.UtcNow
            };
            return merenja;
        }
        // Konstruktor za pokretanje timera koji cisti kes
        private void Cleanup()
        {
            var expired = _cache
                .Where(x => DateTime.Now - x.Value.LastAccessed > TimeSpan.FromHours(24))
                .Select(x => x.Key).ToList();

            foreach (var key in expired)
            {
                _cache.Remove(key);
            }
        }

        public List<Merenja> GetLastValuesOfAllDevices()
        {
            var allDevicesIds = _serverService.GetAllDeviceIds();
            var lastValues = new List<Merenja>();

            foreach (var id in allDevicesIds)
            {

                if (id == null)
                {
                    Console.WriteLine("Upozorenje: Nadjen je null deviceId u listi");
                    continue;
                }

                // Uzimamo merenja za svaki uredjaj
                var data = GetMerenja(id);

                // Proveravamo da li ima merenja za taj uredjaj
                if (data.Any())
                {
                    // Uzimamo poslednje merenje za svaki uredjaj
                    var last = data.OrderByDescending(m => m.TimeStamp).First();
                    // Dodajemo ga u listu 
                    lastValues.Add(last);
                }
            }
            return lastValues;
        }
        
    }
}
