using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgLab3.Models
{

    public class Product
    {

        public int productID { get; private set; }
        public string productName { get; private set; }
        public string imagePath { get; private set; }
        public string description { get; private set; }
				public int categoryID { get; private set; }
        public double unitPrice { get; private set; }
        public int inStock { get; private set; }

        public Product(int Id, string name, string imageurl, string desc, int categoryID, double price, int inStock)
        {
            this.productID = Id;
            this.productName = name;
            this.imagePath = imageurl;
            this.description = desc;
						this.categoryID = categoryID;
            this.unitPrice = price;
            this.inStock = inStock;
        }

    }

}