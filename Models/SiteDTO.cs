using System.ComponentModel.DataAnnotations;

namespace ICTS_API.Models
{
    public class SiteDTO
    {
        //TODO: Unique
        //TODO: Regex
        [Required]
        [StringLength(30)]
        public string SiteName { get; set; }

        // TODO: public TYPE Coordinates { get; set; }
    }
}
