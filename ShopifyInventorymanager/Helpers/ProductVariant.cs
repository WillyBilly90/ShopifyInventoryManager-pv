using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyInventorymanager.Helpers
{
    public class ProductVariant
    {
        public string productId { get; set; }
        public string variantId { get; set; }
        public string sku { get; set; }
        public string barcode { get; set; }
        public string price { get; set; }
        public string title { get; set; }
        public string displayName { get; set; }
        public int inventoryAvailable { get; set; }
        public string inventoryLocationName { get; set; }
        public string inventoryLevelId { get; set; }

    }
}
