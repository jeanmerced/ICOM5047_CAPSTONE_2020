﻿using System;
using System.Text.Json.Serialization;

namespace ICTS_API.Models
{
    public class ProductDetailsDTO
    {
        public int ProductId { get; set; }

        public string LotId { get; set; }

        public string ProductName { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Quantity { get; set; }

        public string VirtualSiteName { get; set; }

        public bool DiscrepancyExists
        {
            get
            {
                bool discrepancyExists = false;
                if (Cart != null)
                {
                    if (Cart.Site != null)
                    {
                        if (Cart.Site.SiteName != VirtualSiteName)
                        {
                            discrepancyExists = true;
                        }
                    }
                }
                return discrepancyExists;
            }
        }

        public int CartId { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}
