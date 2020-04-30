
namespace ICTS_API.Models
{
    public class Site
    {
        public int SiteID { get; set; }

        public string SiteName { get; set; }

        public double ULCoordinate { get; set; } //Upper Left Corner

        public double LRCoordinate { get; set; } //Lower Right Corner
    }
}
