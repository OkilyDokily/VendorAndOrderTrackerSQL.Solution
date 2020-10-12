using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendorAndOrderTrackerSQL.Models;
using VendorAndOrderTrackerSQL;
using System.Collections.Generic;
using System;

namespace VendorAndOrderTrackerTestsSQL.ModelsTests
{
  [TestClass]
  public class OrderTests : IDisposable
  {

    public void Dispose()
    {
      Vendor.ClearVendors();
      Order.ClearOrders();
    }

    public OrderTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=vendor_and_order_tracker_sql_test;";
    }

    [TestMethod]
    public void OrderConstructor_EnsureConstructorCreatesCorrectObjects_ReturnTrue()
    {
      Vendor vendor = new Vendor("Good Will", "One mans's trash is another mans treasure.");
      Order order = new Order("Used book", "A cheap investment with a high return", .25, vendor.Id);

      Assert.AreEqual("A cheap investment with a high return", order.Description);
      Assert.AreEqual(.25, order.Price);
      Assert.AreEqual(true, order.Date != null);

      Assert.AreEqual(1, Order.GetOrdersForVendorById(vendor.Id).Count);
    }

    [TestMethod]
    public void FindOrder_EnsureSingleOrderIsFound_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor.Id);
      Order eOrder = new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor.Id);
      //act
      Order aOrder = Order.FindOrder(eOrder.Id);
      Assert.AreEqual(eOrder.Description, aOrder.Description);
      Assert.AreEqual(eOrder.Title, aOrder.Title);
      Assert.AreEqual(eOrder.Price, aOrder.Price);
      Assert.AreEqual(eOrder.VendorId, aOrder.VendorId);
      //Assert.AreEqual(eOrder.Date, aOrder.Date);
      Assert.AreEqual(eOrder.Id, aOrder.Id);
      Assert.AreEqual(eOrder, aOrder);
    }
    [TestMethod]
    public void ClearOrders_EnsureOrdersBecomeEmpty_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor.Id);
      new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor.Id);
      List<Order> eOrders = new List<Order>() { };
      //act
      Order.ClearOrders();
      List<Order> aOrders = Order.GetOrdersForVendorById(vendor.Id);
      CollectionAssert.AreEqual(eOrders, aOrders);
    }

    [TestMethod]
    public void GetOrdersFromVendorById_EnsureAllOrdersFromVendorAreReturned_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor.Id);
      new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor.Id);
      new Order("Used book", "A cheap investment with a high return", .25, vendor.Id);
      
    
      List<Order> aOrders = Order.GetOrdersForVendorById(vendor.Id);
      Assert.AreEqual(3, aOrders.Count);
    }


    [TestMethod]
    public void DeleteAnOrder_EnsureSingleOrderIsDeleted_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      Order order = new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor.Id);
      new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor.Id);
      //act
      Order.DeleteAnOrder(order.Id);
      List<Order> aOrders = Order.GetOrdersForVendorById(vendor.Id);
      Assert.AreEqual(1, aOrders.Count);
    }

    [TestMethod]
    public void ClearOrdersFromVendorById_EnsureOrdersInAVendorAreDeleted_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      Order order = new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor.Id);
      new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor.Id);
      //act
      Order.ClearOrdersFromVendorById(vendor.Id);
      List<Order> aOrders = Order.GetOrdersForVendorById(vendor.Id);
      Assert.AreEqual(0, aOrders.Count);
    }
  }
}