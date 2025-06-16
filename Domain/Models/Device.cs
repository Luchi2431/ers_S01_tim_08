using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Device
    {
        public string Id { get; set; } = "";

        //Lista merenja..
        public List<Merenja> IzmereneVrednosti { get; set; } = new List<Merenja>();
    }
}
