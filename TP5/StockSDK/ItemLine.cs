using System;
using System.Collections.Generic;
using System.Text;

namespace StockManager
{
    class ItemLine
    {
        public Item item { get; set; }
        public int quantity { get; set; }
        protected internal ItemLine(string name, float price, int quantity)
        {
            item = new Item();
            item.name = name;
            item.price = price;
            this.quantity = quantity;
        }
    }

}
