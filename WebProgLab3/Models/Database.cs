using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using WebProgLab3.Models;

namespace WebProgLab3.Models
{
    public class Database
    {
        private SqlConnection database = new SqlConnection("Data Source=idasql-db.hb.se,56077;Initial Catalog=dbtht1204;Persist Security Info=True;");
        private ArrayList productList = new ArrayList();
				
        public ArrayList getAllProducts()
        {
            int productID;
            string productName;
            string imagePath;
            string description;
						int categoryID;
            double unitPrice;
            int inStock;
            try
            {
                database.Open();

                SqlCommand cmd = database.CreateCommand();
                cmd.CommandText = "SELECT * FROM JM_Product";
                SqlDataReader thisReader = cmd.ExecuteReader();

                while (thisReader.Read())
                {
                    productID = Convert.ToInt32(thisReader["ProductID"]);
                    productName = Convert.ToString(thisReader["ProductName"]);
                    description = Convert.ToString(thisReader["Description"]);
                    unitPrice = Convert.ToDouble(thisReader["UnitPrice"]);
										categoryID = Convert.ToInt32(thisReader["CategoryID"]);
                    inStock = Convert.ToInt32(thisReader["InStock"]);
                    imagePath = Convert.ToString(thisReader["ImagePath"]);
                    productList.Add(new Product(productID, productName, imagePath, description, categoryID, unitPrice, inStock));
                }
            }
            catch (Exception ex) { }
            database.Close();
            return productList;
        }
				
        public Product getProduct(int id)
        {
            string productName;
            string imagePath;
            string description;
						int categoryID;
            double unitPrice;
            int inStock;
            Product temp = null;
            try
            {
                database.Open();

                SqlCommand cmd = database.CreateCommand();
                cmd.CommandText = "SELECT * FROM Product WHERE productId = " + id;
                SqlDataReader thisReader = cmd.ExecuteReader();
                thisReader.Read();
                productName = Convert.ToString(thisReader["ProductName"]);
                description = Convert.ToString(thisReader["Description"]);
                unitPrice = Convert.ToDouble(thisReader["UnitPrice"]);
								categoryID = Convert.ToInt32(thisReader["CategoryID"]);
                inStock = Convert.ToInt32(thisReader["InStock"]);
                imagePath = Convert.ToString(thisReader["ImagePath"]);
                temp = new Product(id, productName, imagePath, description, categoryID, unitPrice, inStock);
            }
            catch (Exception ex) { }
            database.Close();
            return temp;
        }
				
        public void updateStock(ShoppingCart shoppingCart)
        {
            try
            {
                database.Open();
                ArrayList rows = shoppingCart.getAll();
                foreach (ProductRow i in rows)
                {
                    SqlCommand getCmd = database.CreateCommand();
                    getCmd.CommandText = "SELECT inStock FROM Product WHERE productId = " + i.product.ProductID;
                    int inStock = Convert.ToInt32(getCmd.ExecuteScalar());

                    inStock -= i.quantity;

                    SqlCommand cmd = database.CreateCommand();
                    cmd.CommandText = "UPDATE Product SET inStock = " + inStock + " WHERE productId = " + i.product.ProductID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }
            database.Close();
        }
				
        public void addOrder(ShoppingCart shoppingCart, String productName, String adress, String email)
        {
            try
            {
                database.Open();
                ArrayList rows = shoppingCart.getAll();
                SqlCommand orderCmd = database.CreateCommand();
                orderCmd.CommandText = "INSERT INTO Order1 VALUES ('" + productName + "', '" + adress + "', '" + email + "')";
                orderCmd.ExecuteNonQuery();
                orderCmd.CommandText = "SELECT @@Identity";
                int orderId = Convert.ToInt32(orderCmd.ExecuteScalar());
                foreach (ProductRow i in rows)
                {
                    SqlCommand cmd = database.CreateCommand();
                    cmd.CommandText = "INSERT INTO OrderRow VALUES (" + orderId + ", " + i.product.ProductID + ", " + i.quantity + ")";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }
            database.Close();
        }
    }
}
