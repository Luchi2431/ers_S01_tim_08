using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Merenja
    {
        public string Id { get; set; } = "";

        public TipMereneVrednosti Tip { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        public double Vrednost { get; set; }

        public Merenja(string id, TipMereneVrednosti tip,double vrednost, DateTime timeStamp)
        {
            Id = id;
            Tip = tip;
            Vrednost = vrednost;
            TimeStamp = timeStamp;
            
        }

        public Merenja()
        {
        }
    }
}
