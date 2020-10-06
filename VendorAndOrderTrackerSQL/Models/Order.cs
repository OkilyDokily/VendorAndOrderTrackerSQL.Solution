using System;

namespace VendorAndOrderTrackerSQL.Models
{
  public class Order
  {
    public static int CurrentID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public int Id { get; set; }

    public Order(string title, string description, double price, Vendor vendor)
    {
      Id = CurrentID;
      Date = DateTime.Now;
      Title = title;
      Description = description;
      Price = price;
      vendor.GetOrders().Add(this);
      CurrentID++;
    }
  }
}