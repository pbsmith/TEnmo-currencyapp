using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Items
    {
        public string ProductType { get;  set; }
        public string InventoryId { get;  set; }
        public string ProductName { get;  set; }
        public decimal Price { get;  set; }
        public string Wrapper { get;  set; }
        public string Quantity { get; set; } = "100";
        public decimal QuantityTotalPrice { get; set; }
        public bool IsWrapped
        {
            get
            {
                
                if(Wrapper == "T")
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

    }
}
