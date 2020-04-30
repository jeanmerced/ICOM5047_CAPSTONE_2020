using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ICTS_API.Models
{
    public class Cart
    {
        public int CartID { get; set; }

        [Required]
        public string TagAddress { get; set; }

        //TODO: neccesary?
        public DateTime LastUpdated { get; set; }

        private int nearExpirationDateWarningCount;
        public int NearExpirationDateWarningCount 
        {
            get
            {
                nearExpirationDateWarningCount = 0;
                if (Products != null)
                {
                    foreach (var product in Products)
                    {
                        var dayDifference = (product.ExpirationDate - DateTime.Today).TotalDays;
                        if (dayDifference <= 7 && dayDifference > 0)
                        {
                            nearExpirationDateWarningCount++;
                        }
                    }
                }
                return nearExpirationDateWarningCount;
            }
        }

        private int expiredWarningCount;
        public int ExpiredWarningCount 
        {
            get
            {
                expiredWarningCount = 0;
                if (Products != null)
                {
                    foreach (var product in Products)
                    {
                        var dayDifference = (product.ExpirationDate - DateTime.Today).TotalDays;
                        if (dayDifference <= 0)
                        {
                            expiredWarningCount++;
                        }
                    }
                }
                return expiredWarningCount;
            }
        }

        private bool discrepancyExists;
        public bool DiscrepancyExists 
        {
            get
            {
                discrepancyExists = false;
                if (Products != null && Location != null)
                {
                    foreach (var product in Products)
                    {
                        if (product.VirtualSiteID != Location.SiteID)
                        {
                            discrepancyExists = true;
                            break;
                        }
                    }
                }
                return discrepancyExists;
            }
        }

        [JsonIgnore]
        public List<Product> Products { get; set; }

        [JsonIgnore]
        public Location Location { get; set; }
    }
}
