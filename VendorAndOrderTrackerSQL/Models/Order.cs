using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace VendorAndOrderTrackerSQL.Models
{
  public class Order
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public int Id { get; set; }
    public int VendorId { get; set; }

    public Order(string title, string description, double price, int vendorid)
    {
      Date = DateTime.Now;
      Title = title;
      Description = description;
      VendorId = vendorid;
      Price = price;
      Save();
    }
    public Order(int id, string title, string description, double price, DateTime date, int vendorid)
    {
      Id = id;
      Date = date;
      Title = title;
      Description = description;
      VendorId = vendorid;
      Price = price;
    }

    public override bool Equals(object obj)
    {
      Order order = (Order)obj;
      return (this.Description == order.Description && this.Title == order.Title && this.Id == order.Id && this.Price == order.Price && this.VendorId == order.VendorId);
    }
    public static void ClearOrdersFromVendorById(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.Parameters.AddWithValue("@Id", id);
      cmd.CommandText = @"DELETE FROM orders WHERE vendorid = @Id;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void ClearOrders()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM orders;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    private void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO orders (title, description, price, date, vendorid) VALUES (@OrderTitle, @OrderDescription, @OrderPrice, @OrderDate, @OrderVendorId);";

      cmd.Parameters.AddWithValue("@OrderDescription", this.Description);
      cmd.Parameters.AddWithValue("@OrderTitle", this.Title);
      cmd.Parameters.AddWithValue("@OrderVendorId", this.VendorId);
      cmd.Parameters.AddWithValue("@OrderPrice", this.Price);
      cmd.Parameters.AddWithValue("@OrderDate", this.Date);

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Update()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE orders SET title = @OrderTitle, description = @OrderDescription, price = @OrderPrice WHERE id = @OrderId;";

      cmd.Parameters.AddWithValue("@OrderId", this.Id);
      cmd.Parameters.AddWithValue("@OrderDescription", this.Description);
      cmd.Parameters.AddWithValue("@OrderTitle", this.Title);
      cmd.Parameters.AddWithValue("@OrderPrice", this.Price);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Order FindOrder(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.Parameters.AddWithValue("@Id", id);
      cmd.CommandText = @"SELECT * FROM orders WHERE id = @Id;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int orderid = 0;
      string title = "";
      string description = "";
      double price = 0;
      DateTime date = DateTime.Now;
      int vendorid = 0;
      while (rdr.Read())
      {
        orderid = rdr.GetInt32(0);
        Console.WriteLine(orderid);
        title = rdr.GetString(1);
        description = rdr.GetString(2);
        price = rdr.GetDouble(3);
        date = rdr.GetDateTime(4);
        vendorid = rdr.GetInt32(5);
      }
      Order order = new Order(orderid, title, description, price, date, vendorid);
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return order;
    }
    public static void DeleteAnOrder(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.Parameters.AddWithValue("@Id", id);
      cmd.CommandText = @"DELETE FROM orders WHERE id = @Id;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Order> GetOrdersForVendorById(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.Parameters.AddWithValue("@Id", id);
      cmd.CommandText = @"SELECT * FROM orders WHERE vendorid = @Id;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Order> results = new List<Order>();
      while (rdr.Read())
      {
        int orderid = 0;
        string title = "";
        string description = "";
        double price = 0;
        DateTime date = DateTime.Now;
        int vendorid = 0;

        orderid = rdr.GetInt32(0);
        title = rdr.GetString(1);
        description = rdr.GetString(2);
        price = rdr.GetDouble(3);
        date = rdr.GetDateTime(4);
        vendorid = rdr.GetInt32(5);

        results.Add(new Order(orderid, title, description, price, date, vendorid));
      }

      return results;
    }
  }
}