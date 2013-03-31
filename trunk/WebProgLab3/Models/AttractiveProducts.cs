using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace WebProgLab3.Models
{
    public class PopularProducts
    {
        ArrayList allCarts = new ArrayList();

        public void AddPurchase(ShoppingCart shoppingCart)
        {
            allCarts.Add(shoppingCart);
        }


        //Returns an array of the most popular products.
        public ArrayList GetPopular()
        {
            RemoveOldPurchases();

            ArrayList tmp = GetAllProductRows();

            return GetTop10(tmp);
        }

        //Loops through all carts and compiles an array where a product only exists once.
        private ArrayList GetAllProductRows()
        {
            ArrayList tmp = new ArrayList();
            foreach (ShoppingCart shoppingCart in allCarts)
            {
                foreach (ProductRow row in shoppingCart.getAll())
                {
                    AddIfNotExist(row, tmp);
                }
            }

            return tmp;
        }

        private void AddIfNotExist(ProductRow purchaseRow, ArrayList list)
        {
            if (list.Count > 0)
            {
                foreach (ProductRow p in list)
                {
                    if (p.product.productID == purchaseRow.product.productID)
                    {
                        p.quantity += purchaseRow.quantity;
                        return;
                    }
                }
            }
            list.Add(purchaseRow);
        }

        private ArrayList GetTop10(ArrayList allProducts)
        {
            ArrayList mostPopular = new ArrayList();
            int tempq = 0;
            int tempi = 0;
            if (allProducts.Count <= 10)
            {
                return allProducts;
            }
            else
            {
                while (mostPopular.Count < 10)
                {
                    for (int i = 0; i < allProducts.Count; i++)
                    {
                        ProductRow tempRow = allProducts[i] as ProductRow;
                        if (tempRow.quantity > tempq)
                        {
                            tempq = tempRow.quantity;
                            tempi = i;
                        }
                    }
                    mostPopular.Add(allProducts[tempi]);
                    allProducts.RemoveAt(tempi);
                }
            }
            return mostPopular;
        }

        private void RemoveOldPurchases()
        {
            ShoppingCart c;
            DateTime breakPoint = DateTime.Now.AddHours(-1);
            if (allCarts.Count > 0)
            {
                for (int i = 0; i < allCarts.Count; i++)
                {
                    c = allCarts[i] as ShoppingCart;
                    if (breakPoint > c.time)
                    {
                        allCarts.Remove(c);
                    }
                }
            }
        }
    }
}

