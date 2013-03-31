using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WebProgLab3.Models;

namespace WebProgLab3.Models
{
    public class ShoppingCart
    {
        private ArrayList products;
        public DateTime time { get; set; }

        public ShoppingCart()
        {
            products = new ArrayList();
        }

        public void addProduct(Product product, int quantity)
        {
            ProductRow tmp = new ProductRow(product, quantity);
            for (int i = 0; i < products.Count; i++)
            {
                ProductRow temp = products[i] as ProductRow;
                if (product.productID == temp.product.productID)
                {
                    temp.quantity += quantity;
                    return;
                }
            }
            products.Add(tmp);
        }

        public void removeProduct(Product product)
        {
            ProductRow tmp = null;

            foreach (ProductRow row in products)
            {
                if (row.product.Equals(product))
                {
                    tmp = row;
                    break;
                }
            }

            products.Remove(tmp);

        }

        public ArrayList getAll()
        {
            return products;
        }

        public double getTotalPrice()
        {
            double sum = 0;
            foreach (ProductRow row in products)
            {
                sum += row.quantity * row.product.unitPrice;
            }

            return sum;
        }

        public void clear()
        {
            products.Clear();
        }

        public Boolean isEmpty()
        {
            return products.Count < 1;
        }

    }


}