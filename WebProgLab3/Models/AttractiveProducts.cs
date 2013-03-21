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

        public void addPurchase(ShoppingCart shoppingCart)
        {
            allCarts.Add(shoppingCart);
        }


        //Returns an array of the most popular products.
        public ArrayList getPopular()
        {
            removeOldPurchases();

            ArrayList tmp = getAllProductRows();

            return getTop10(tmp);
        }

        //Loops through all carts and compiles an array where a product only exists once.
        private ArrayList getAllProductRows()
        {
            ArrayList tmp = new ArrayList();
            foreach (ShoppingCart shoppingCart in allCarts)
            {
                foreach (ProductRow row in shoppingCart.getAll())
                {
                    addIfNotExist(row, tmp);
                }
            }

            return tmp;
        }

        private void addIfNotExist(ProductRow purchaseRow, ArrayList list)
        {
            if (list.Count > 0)
            {
                foreach (ProductRow p in list)
                {
                    if (p.product.ProductID == purchaseRow.product.ProductID)
                    {
                        p.quantity += purchaseRow.quantity;
                        return;
                    }
                }
            }
            list.Add(purchaseRow);
        }

        private ArrayList getTop10(ArrayList allProducts)
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

        private void removeOldPurchases()
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

