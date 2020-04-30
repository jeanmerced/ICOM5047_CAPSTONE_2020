using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ICTS_API.Models
{
    public class Product
    {
        [Key]
        public string LotID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Required]
        public int VirtualSiteID { get; set; }

        private bool discrepancyExists;
        public bool DiscrepancyExists 
        {
            get
            {
                discrepancyExists = false;
                if (Cart != null)
                {
                    if (Cart.Location != null)
                    {
                        if (Cart.Location.SiteID != VirtualSiteID)
                        {
                            discrepancyExists = true;
                        }
                    }
                }
                return discrepancyExists;
            }
        }

        public int Quantity { get; set; }

        public int CartID { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}
