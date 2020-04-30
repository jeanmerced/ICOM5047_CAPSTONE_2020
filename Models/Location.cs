using System;
using System.Text.Json.Serialization;

namespace ICTS_API.Models
{
    public class Location
    {
        public int LocationID { get; set; }

        public DateTime LastUpdated { get; set; }

        public double XCoordinate { get; set; }

        public double YCoordinate { get; set; }

        public int CartID { get; set; }
     
        public int SiteID { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }
    }
}
