using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class ProxyCacheItem
    {
        public List<Merenja> Merenja { get; set; } = new List<Merenja>();

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public DateTime LastAccessed { get; set; } = DateTime.UtcNow;

    }
}
