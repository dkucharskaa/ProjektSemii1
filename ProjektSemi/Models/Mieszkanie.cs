using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektSemi.Models
{
    public class Mieszkanie
    {
        public bool MiejscePostojowe { get; set; }
        public int Pokoje { get; set; }
        public bool Balkon { get; set; }
        public int Piętro { get; set; }
        public bool Piwnica { get; set; }
        public bool Komórka { get; set; }
    }
}