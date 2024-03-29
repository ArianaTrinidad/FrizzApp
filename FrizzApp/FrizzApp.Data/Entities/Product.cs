﻿using System.Collections.Generic;

namespace FrizzApp.Data.Entities
{
    public class Product : AuditableEntity
    {
        public Product()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Presentation { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsPromo { get; set; }
        public int ProductStatusId { get; set; }
        public int? CategoryId { get; set; }

        public virtual ProductStatus ProductStatus { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Order> Orders { get; set; }


    }
}
