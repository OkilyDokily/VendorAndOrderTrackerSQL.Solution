using System.Collections.Generic;
using System.Linq;
using System;
using MySql.Data.MySqlClient;

namespace VendorAndOrderTrackerSQL.Models
{
  public class Vendor
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }

    public Vendor(string name, string description, int id)
    {
      Name = name;
      Description = description;
      Id = id;
    }

    public Vendor(string name, string description)
    {
      Name = name;
      Description = description;
      Save();
    }

    public override bool Equals(object obj)
    {
      Vendor vendor = (Vendor)obj;
      return (this.Description == vendor.Description && this.Name == vendor.Name && this.Id == vendor.Id);
    }

    public static void ClearVendors()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM vendors;";
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
      cmd.Parameters.AddWithValue("@VendorDescription", this.Description);
      cmd.Parameters.AddWithValue("@VendorName", this.Name);
      cmd.CommandText = @"INSERT INTO vendors (name, description) VALUES (@VendorName, @VendorDescription);";
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
      cmd.CommandText = @"UPDATE vendors SET name = @VendorName, description = @VendorDescription WHERE id = @VendorId;";

      cmd.Parameters.AddWithValue("@VendorId", this.Id);
      cmd.Parameters.AddWithValue("@VendorDescription", this.Description);
      cmd.Parameters.AddWithValue("@VendorName", this.Name);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Vendor FindVendor(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.Parameters.AddWithValue("@Id", id);
      cmd.CommandText = @"SELECT * FROM vendors WHERE id = @Id;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int vendorid = 0;

      string name = "";
      string description = "";
      while (rdr.Read())
      {
        vendorid = rdr.GetInt32(0);
        name = rdr.GetString(1);
        description = rdr.GetString(2);
      }

      Vendor vendor = new Vendor(name, description, vendorid);
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return vendor;
    }

    public static List<Vendor> GetVendors()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM vendors;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Vendor> results = new List<Vendor>();
      while (rdr.Read())
      {
        int vendorid = 0;
        string name = "";
        string description = "";
        vendorid = rdr.GetInt32(0);
        name = rdr.GetString(1);
        description = rdr.GetString(2);

        results.Add(new Vendor(name, description, vendorid));
      }

      return results;
    }

    public static void DeleteVendor(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.Parameters.AddWithValue("@Id", id);
      cmd.CommandText = @"DELETE FROM vendors  WHERE id = @Id;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Dictionary<Vendor, List<Order>> SearchOrders(string query)
    {

      Dictionary<Vendor, List<Order>> matches = new Dictionary<Vendor, List<Order>>();
      foreach (Vendor vendor in Vendor.GetVendors())
      {
        Dictionary<Order, int> d = new Dictionary<Order, int>();
        foreach (Order order in Order.GetOrdersForVendorById(vendor.Id))
        {
          if (order.Description.Contains(query))
          {
            try
            {
              var match = matches[vendor];
              matches[vendor].Add(order);
            }
            catch
            {
              matches[vendor] = new List<Order>();
              matches[vendor].Add(order);
            }
          }
        };
      };
      return matches;
    }
  }
}