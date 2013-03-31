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
		
		private static enum Table {CATEGORY, PRODUCT, ORDER};
		private SqlConnection database = new SqlConnection("Data Source=idasql-db.hb.se,56077;Initial Catalog=dbtht1204;Persist Security Info=True;User ID=dbtht1204;Password=stass9");
				
		public ArrayList GetAllProducts()
    {
			ArrayList productList = new ArrayList();
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
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
        {
					productID = Convert.ToInt32(reader["ProductID"]);
					productName = Convert.ToString(reader["ProductName"]);
					description = Convert.ToString(reader["Description"]);
					unitPrice = Convert.ToDouble(reader["UnitPrice"]);
					categoryID = Convert.ToInt32(reader["CategoryID"]);
					inStock = Convert.ToInt32(reader["InStock"]);
					imagePath = Convert.ToString(reader["ImagePath"]);
          productList.Add(new Product(productID, productName, imagePath, description, categoryID, unitPrice, inStock));
        }
			}
      catch (Exception ex) { }
      database.Close();
      return productList;
		}

		public ArrayList GetAllCategories()
		{
			ArrayList categoryList = new ArrayList();
			int id;
			string name;

			try
			{
				database.Open();

				SqlCommand cmd = database.CreateCommand();
				cmd.CommandText = "SELECT * FROM JM_Category";
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					id = Convert.ToInt32(reader["CategoryID"]);
					name = Convert.ToString(reader["CategoryName"]);
				}
			}
			catch (Exception e) { }
			database.Close();
			return categoryList;
		}
				
    public Product GetProduct(int id)
    {
			Product product = null;
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
        cmd.CommandText = "SELECT * FROM JM_Product WHERE ProductID = " + id;
				SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
				productName = Convert.ToString(reader["ProductName"]);
				description = Convert.ToString(reader["Description"]);
				unitPrice = Convert.ToDouble(reader["UnitPrice"]);
				categoryID = Convert.ToInt32(reader["CategoryID"]);
				inStock = Convert.ToInt32(reader["InStock"]);
				imagePath = Convert.ToString(reader["ImagePath"]);
				product = new Product(id, productName, imagePath, description, categoryID, unitPrice, inStock);
      }
      catch (Exception ex) { }
      database.Close();
			return product;
		}
				
    public void UpdateStock(ShoppingCart shoppingCart)
    {
			try
      {
				database.Open();
				ArrayList rows = shoppingCart.getAll();
        foreach (ProductRow p in rows)
        {
					SqlCommand cmd = database.CreateCommand();
					cmd.CommandText = "SELECT InStock FROM JM_Product WHERE ProductId = " + p.product.ProductID;
					int inStock = Convert.ToInt32(cmd.ExecuteScalar());

					inStock -= p.quantity;

          cmd.CommandText = "UPDATE JM_Product SET InStock = " + inStock + " WHERE ProductId = " + p.product.ProductID;
          cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex) { }
			database.Close();
		}
				
    public void AddOrder(ShoppingCart shoppingCart, String productName, String adress, String email)
    {
			try
			{
				database.Open();
				ArrayList rows = shoppingCart.getAll();
				SqlCommand cmd = database.CreateCommand();
				cmd.CommandText = "INSERT INTO JM_Order VALUES ('" + productName + "', '" + adress + "', '" + email + "')";
				cmd.ExecuteNonQuery();
				cmd.CommandText = "SELECT @@Identity"; // Hämtar ID på den senast instoppade raden i tabellen
																								 // Kommer inte fungera för oss då vi använder JDBC och inte använder auto-increment
				int orderID = Convert.ToInt32(cmd.ExecuteScalar());
          
				foreach (ProductRow r in rows)
				{
					cmd = database.CreateCommand();
          cmd.CommandText = "INSERT INTO OrderRow VALUES (" + orderID + ", " + r.product.ProductID + ", " + r.quantity + ")";
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex) { }
      database.Close();
		}

		public int GetMaxID(Table table)
		{
			int maxID = -1;

			try
			{
				database.Open();
				SqlCommand cmd = database.CreateCommand();
				SqlDataReader reader;

				switch (table)
				{
					case Table.CATEGORY:
						cmd.CommandText = "SELECT TOP(1) CategoryID FROM JM_Category ORDER BY CategoryID DESC";
						reader = cmd.ExecuteReader();
						reader.Read();
						maxID = Convert.ToInt32(reader["CategoryID"]);
						break;
					case Table.PRODUCT:
						cmd.CommandText = "SELECT TOP(1) ProductID FROM JM_Product ORDER BY ProductID DESC";
						reader = cmd.ExecuteReader();
						reader.Read();
						maxID = Convert.ToInt32(reader["ProductID"]);
						break;
					case Table.ORDER:
						cmd.CommandText = "SELECT TOP(1) OrderID FROM JM_Order ORDER BY OrderID DESC";
						reader = cmd.ExecuteReader();
						reader.Read();
						maxID = Convert.ToInt32(reader["OrderID"]);
						break;
					default:
						break;
				}
			}
			catch (Exception ex) { }
			database.Close();
			return maxID;
		}

	}

}
