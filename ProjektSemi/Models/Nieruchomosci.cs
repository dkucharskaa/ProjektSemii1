using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektSemi.Models
{
    using Newtonsoft.Json;
    public class Nieruchomosci
    {
        
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "nazwa")]
        public string Nazwa { get; set; }
        [JsonProperty(PropertyName = "kategoria")]
        public Kategoria Kategoria { get; set; }
        [JsonProperty(PropertyName = "adres")]
        public string Adres { get; set; }
        [JsonProperty(PropertyName = "cena")]
        public double Cena { get; set; }
        [JsonProperty(PropertyName = "powierzchnia")]
        public double Powierzchnia { get; set; }
        [JsonProperty(PropertyName = "czySprzedane")]
        public bool CzySprzedane { get; set; }
        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "dom")]
        public Dom dom { get; set; }
        [JsonProperty(PropertyName = "mieszkanie")]
        public Mieszkanie mieszkanie { get; set; }
        [JsonProperty(PropertyName = "lokal")]
        public Lokal lokal { get; set; }
        [JsonProperty(PropertyName = "magazyn")]
        public Magazyn magazyn { get; set; }

        public Nieruchomosci()
        { }
    }
}