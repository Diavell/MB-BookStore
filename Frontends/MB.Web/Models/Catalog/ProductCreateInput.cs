﻿namespace MB.Web.Models.Catalog
{
    public class ProductCreateInput
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public FeatureViewModel Feature { get; set; }
    }
}