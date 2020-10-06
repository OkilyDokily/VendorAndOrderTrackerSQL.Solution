using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendorAndOrderTrackerSQL.Models;
using System;

namespace VendorAndOrderTrackerTestsSQL.ModelsTests
{
  [TestClass]
  public class OrderTests : IDisposable
  {

    public void Dispose()
    {
      Vendor.ClearVendors();
      Vendor.CurrentID = 0;
    }

    [TestMethod]
    public void OrderConstructor_EnsureConstructorCreatesCorrectObjects_ReturnTrue()
    {
      Vendor vendor = new Vendor("Good Will", "One mans's trash is another mans treasure.");
      Order order = new Order("Used book", "A cheap investment with a high return", .25, vendor);

      Assert.AreEqual("Used book", order.Title);
      Assert.AreEqual("A cheap investment with a high return", order.Description);
      Assert.AreEqual(.25, order.Price);
      Assert.AreEqual(true, order.Date != null);
      Assert.AreEqual(0, order.Id);
      Assert.AreEqual(1, Order.CurrentID);
      Assert.AreEqual(1, vendor.GetOrders().Count);
    }
  }
}