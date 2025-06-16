using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure
{
    public class InMemoryStorage
    {
        //Kreiramo dictionary za merenja
        private readonly Dictionary<string, Device> _devices = new Dictionary<string, Device>();

        //Cuvamo dobijena merenja
        public void SaveMerenja(string deviceId, Merenja merenje)
        {
            //Ako ne postoji uredjaj kreira se novi
            if (!_devices.ContainsKey(deviceId))
                _devices[deviceId] = new Device { Id = deviceId };

            _devices[deviceId].IzmereneVrednosti.Add(merenje);
        }
        //Uzimamo merenja
        public IEnumerable<Merenja> GetMerenja(string deviceId)
        {
            if (_devices.TryGetValue(deviceId,out var device))
            {
                return device.IzmereneVrednosti;
            }
            return Enumerable.Empty<Merenja>();
        }
    }
}
