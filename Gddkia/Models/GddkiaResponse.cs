using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gddkia.Models
{
    public class GddkiaResponse
    {
        [JsonProperty("utrudnienia")] public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("utr")] public IEnumerable<Obstacle> Obstacles { get; set; }
    }


    public class Delay
    {
        public class Val
        {
            [JsonProperty("kierunek")]
            public string Direction { get; set; }

            [JsonProperty("czas_od")]
            public string Since { get; set; }
            [JsonProperty("czas_do")]
            public string To { get; set; }
            [JsonProperty("pn_pt")]

            public string PnPt { get; set; }
            [JsonProperty("so")]

            public string So { get; set; }
            [JsonProperty("ni")]

            public string Ni { get; set; }
        }
        [JsonProperty("czas_oczekiwania")]
        public Val Value { get; set; }
    }

    public class Obstacle
    {
        [JsonProperty("typ")] public string Type { get; set; }
        [JsonProperty("nr_drogi")] public string Road { get; set; }
        [JsonProperty("geo_lat")] public string Latitude { get; set; }
        [JsonProperty("geo_long")] public string Longitude { get; set; }
        [JsonProperty("czasy_oczekiwania")] public Delay Delay { get; set; }

        private sealed class TypeLatitudeLongitudeEqualityComparer : IEqualityComparer<Obstacle>
        {
            public bool Equals(Obstacle x, Obstacle y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Type == y.Type && x.Latitude == y.Latitude && x.Longitude == y.Longitude;
            }

            public int GetHashCode(Obstacle obj)
            {
                return HashCode.Combine(obj.Type, obj.Latitude, obj.Longitude);
            }
        }

        public static IEqualityComparer<Obstacle> TypeLatitudeLongitudeComparer { get; } = new TypeLatitudeLongitudeEqualityComparer();
    }
}