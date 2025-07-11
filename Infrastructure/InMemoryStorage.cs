﻿using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure
{
    public class InMemoryStorage : IStorage
    {
        //Kreiramo dictionary za merenja
        private readonly Dictionary<string, List<Merenja>> _data = new Dictionary<string, List<Merenja>>();
        private readonly string logFilePath = "log.txt";

        //Cuvamo dobijena merenja
        public void SaveMerenja(string deviceId, Merenja merenje)
        {
            if (string.IsNullOrEmpty(deviceId))
                throw new ArgumentException("deviceId ne sme biti null ili prazan.");

            //Ako ne postoji uredjaj kreira se novi
            if (!_data.ContainsKey(deviceId))
                _data[deviceId] = new List<Merenja>();

            _data[deviceId].Add(merenje);

            //Logovanje dogadjaja
            LogEvent($"Sacuvano merenje za uredjaj {deviceId}: {merenje.Vrednost} [{merenje.Tip}]");
        }
        //Uzimamo merenja
        public List<Merenja> GetMerenjaByDevice(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
                throw new ArgumentException("deviceId ne sme biti null ili prazan!");
            return _data.ContainsKey(deviceId) ? _data[deviceId] : new List<Merenja>();
        }

        //Logovanje dogadjaja u fajl
        public void LogEvent(string message)
        {
            File.AppendAllText(logFilePath, $"{DateTime.UtcNow}: {message}{Environment.NewLine}\n");
        }

        public DateTime GetLastUpdateTime(string deviceId)
        {
            //Vracamo najnovije vreme merenja za dati uredjaj
            if (_data.ContainsKey(deviceId) && _data[deviceId].Any())
            {
                return _data[deviceId].Max(m => m.TimeStamp);
            }
            return DateTime.MinValue;
        }

        //Vracamo sve ID-ove uredjaja(Sve Deviceove)
        public List<string> GetAllDeviceIds()
        {
            return _data.Keys
                .Where(id => !string.IsNullOrEmpty(id))
                .ToList();
        }
    }
}
