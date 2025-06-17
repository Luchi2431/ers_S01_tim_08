using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Enums;
using Domain.Interfaces;

namespace Applications
{
    public class DeviceService
    {
        private readonly IStorage _storage;
        private readonly Random _random = new Random();

        public DeviceService(IStorage storage)
        {
            _storage = storage;
        }

        public void StartAutoSending(string deviceId)
        {
            var timer = new System.Timers.Timer(5 * 1000); //5 sekudni za test
            timer.Elapsed += (s, e) => GenerateAndSend(deviceId);
            timer.AutoReset = true; // Resetuj tajmer posle svakog intervala
            timer.Enabled = true; // Tajmer krece
        }

        private void GenerateAndSend(string deviceId)
        {
            var merenje = new Merenja
            {
                Id = Guid.NewGuid().ToString(),
                Tip = (_random.Next(2) == 0) ? TipMereneVrednosti.ANALOGNA : TipMereneVrednosti.DIGITALNA,
                Vrednost = _random.NextDouble() * 100, // Random vrednost izmedju 0 i 100
                TimeStamp = DateTime.UtcNow // Postavljamo trenutni UTC datum i vreme
            };

            _storage.SaveMerenja(deviceId, merenje);
            Console.WriteLine($"[{DateTime.Now}] Poslato merenje za uređaj {deviceId}: {merenje.Tip} - {merenje.Vrednost}");

        }
    }
}
