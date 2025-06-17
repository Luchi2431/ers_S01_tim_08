using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStorage
    {
        void SaveMerenja(string deviceId, Merenja merenje);
        List<Merenja> GetMerenjaByDevice(string deviceId);
        void LogEvent(string message); //logovanje dogadjaja
        DateTime GetLastUpdateTime(string deviceId);
        List<string> GetAllDeviceIds();
    }
}
