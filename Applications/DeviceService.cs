using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Enums;

namespace Applications
{
    public class DeviceService
    {
        private readonly InMemoryStorage _storage;
        private readonly Random _random = new Random();

        public DeviceService(InMemoryStorage storage)
        {
            _storage = storage;
        }

        public void StartAutoSending(string deviceId)
        {
            var timer = new System.Timers.Timer(5 * 1000); //5 minutes in milliseconds
            timer.Elapsed += (s, e) => GenerateAndSend(deviceId);
            timer.AutoReset = true; // Reset the timer after each interval
            timer.Enabled = true; // Start the timer
        }

        private void GenerateAndSend(string deviceId)
        {
            var merenje = new Merenja
            {
                Tip = (_random.Next(2) == 0) ? TipMereneVrednosti.ANALOGNA : TipMereneVrednosti.DIGITALNA,
                Vrednost = _random.NextDouble() * 100 // Random value between 0 and 100
            };

            _storage.SaveMerenja(deviceId, merenje);
            Console.WriteLine($"[{DateTime.Now}] Poslato merenje za uređaj {deviceId}: {merenje.Tip} - {merenje.Vrednost}");

        }
    }
}
