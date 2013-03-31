using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgLab3.Models
{

    public class Product
    {

        public int ProductID { get; private set; }
        public string ProductName { get; private set; }
        public string ImagePath { get; private set; }
        public string Description { get; private set; }
				public int CategoryID { get; private set; }
        public double UnitPrice { get; private set; }
        public int inStock { get; private set; }

        public Product(int Id, string name, string imageurl, string desc, int categoryID, double price, int inStock)
        {
            this.ProductID = Id;
            this.ProductName = name;
            this.ImagePath = imageurl;
            this.Description = desc;
						this.CategoryID = categoryID;
            this.UnitPrice = price;
            this.inStock = inStock;
        }

    }

}