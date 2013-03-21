using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgLab3.Models
{
    public class ProductRow
    {
        public Product product { private set; get; }
        public int quantity { set; get; }

        public ProductRow(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }

    }
}