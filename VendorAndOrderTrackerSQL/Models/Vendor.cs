using System.Collections.Generic;
using System.Linq;

namespace VendorAndOrderTrackerSQL.Models
{
  public class Vendor
  {
    private static List<Vendor> _vendors = new List<Vendor>();
    public static int CurrentID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
    private List<Order> _orders = new List<Order>();

    public Vendor(string name, string description)
    {
      Id = CurrentID;
      
      Name = name;
      Description = description;
      _vendors.Add(this);
      CurrentID++;
    }

    public override bool Equals(object obj)
    {
      Vendor vendor = (Vendor) obj;
      return (this.Description == vendor.Description && this.Name == vendor.Name);
    }

    public static List<Vendor> GetVendors()
    {
      return _vendors;
    }

    public static void ClearVendors()
    {
      _vendors.Clear();
    }

    public static void DeleteVendor(int id)
    {
      _vendors.Remove(Vendor.GetVendors().Single(x => x.Id == id));
    }

    public static Dictionary<Vendor, List<Order>> SearchOrders(string query)
    {

      Dictionary<Vendor, List<Order>> matches = new Dictionary<Vendor, List<Order>>();
      foreach (Vendor vendor in _vendors)
      {
        Dictionary<Order, int> d = new Dictionary<Order, int>();
        foreach (Order order in vendor._orders)
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

    public List<Order> GetOrders()
    {
        return _orders;
    }

    public static Vendor FindVendor(int id)
    {
      return _vendors.Find(x => x.Id == id);
    }

    public Order FindOrder(int id)
    {
      return _orders.Find(x => x.Id == id);
    }

    public void DeleteOrders()
    {
      _orders.Clear();
    }

    public void DeleteAnOrder(int id)
    {
      _orders.Remove(_orders.Single(x => x.Id == id));
    }
  }
}