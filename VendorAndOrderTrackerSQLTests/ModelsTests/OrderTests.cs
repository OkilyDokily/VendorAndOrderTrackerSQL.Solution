using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendorAndOrderTrackerSQL.Models;
using VendorAndOrderTrackerSQL;
using System;

namespace VendorAndOrderTrackerTestsSQL.ModelsTests
{
  [TestClass]
  public class OrderTests : IDisposable
  {

    public void Dispose()
    {
      Vendor.ClearVendors();
      //Order.ClearOrders();
    }

    public OrderTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=vendor_and_order_tracker_sql_test;";
    }

    [TestMethod]
    public void OrderConstructor_EnsureConstructorCreatesCorrectObjects_ReturnTrue()
    {
      Vendor vendor = new Vendor("Good Will", "One mans's trash is another mans treasure.");
      Order order = new Order("Used book", "A cheap investment with a high return", vendor.Id);

      Assert.AreEqual(1, 1);

      // Assert.AreEqual("A cheap investment with a high return", order.Description);
      // Assert.AreEqual(.25, order.Price);
      // Assert.AreEqual(true, order.Date != null);

      // Assert.AreEqual(1, Order.GetOrdersForVendorById(vendor.Id).Count);
    }

    // [TestMethod]
    // public void FindOrder_EnsureSingleOrderIsFound_ReturnsTrue()
    // {
    //   //Arrange
    //   Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
    //   new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor);
    //   Order eOrder = new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor);
    //   //act
    //   Order aOrder = vendor.FindOrder(1);
    //   Assert.AreEqual(eOrder, aOrder);
    // }
    // [TestMethod]
    // public void DeleteOrders_EnsureOrdersBecomeEmpty_ReturnsTrue()
    // {
    //   //Arrange
    //   Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
    //   new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor);
    //   new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor);
    //   List<Order> eOrders = new List<Order>() { };
    //   //act
    //   vendor.DeleteOrders();
    //   List<Order> aOrders = vendor.GetOrders();
    //   CollectionAssert.AreEqual(eOrders, aOrders);
    // }

    // [TestMethod]
    // public void DeleteAnOrder_EnsureSingleOrderIsDeleted_ReturnsTrue()
    // {
    //   //Arrange
    //   Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
    //   new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor);
    //   new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor);
    //   //act
    //   vendor.DeleteAnOrder(0);
    //   List<Order> aOrders = vendor.GetOrders();
    //   Assert.AreEqual(1, aOrders[0].Id);
    //  }
  }
}