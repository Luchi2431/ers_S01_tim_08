using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications
{
    public class ServerService
    {
        private readonly IStorage _storage;

        public ServerService(IStorage storage)
        {
            _storage = storage;
        }

        public void PrimiMerenje(string deviceId, Merenja merenje)
        {
            _storage.SaveMerenja(deviceId, merenje);
        }

        public List<Merenja> VratiMerenjaZaUredjaj(string deviceId)
        {
            return _storage.GetMerenjaByDevice(deviceId);
        }
    }
}
