using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendorAndOrderTrackerSQL.Models;
using System.Collections.Generic;
using System;

namespace VendorAndOrderTrackerTestsSQL.ModelsTests
{
  [TestClass]
  public class VendorTest : IDisposable
  {
    public void Dispose()
    {
      Vendor.ClearVendors();
      Vendor.CurrentID = 0;
      Order.CurrentID = 0;
    }
    [TestMethod]
    public void VendorConstructor_ConstructorCorrectlyCreatesObject_ReturnTrue()
    {
      Vendor vendor = new Vendor("Taco Bell", "A fine food establishment");
      Assert.AreEqual("Taco Bell", vendor.Name);
      Assert.AreEqual("A fine food establishment", vendor.Description);
      Assert.AreEqual(0, vendor.Id);
      Assert.AreEqual(1, Vendor.CurrentID);
      Assert.AreEqual(1, Vendor.GetVendors().Count);
    }

    [TestMethod]
    public void SearchOrders_EnsureThatSearchListIsCorrect_ReturnTrue()
    {
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor);
      new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor);
      Vendor vendor2 = new Vendor("Pizza Hut", "A fine pizza establishment");
      new Order("Hamburger Pizza", "A tempting ground beef pizza wonder.", 6.99, vendor2);

      Dictionary<Vendor, List<Order>> result = Vendor.SearchOrders("beef");
      Dictionary<Vendor, List<Order>> result2 = Vendor.SearchOrders("treat");

      Assert.AreEqual(2, result.Count);
      Assert.AreEqual(2, result[vendor].Count);
      Assert.AreEqual(1, result[vendor2].Count);

      Assert.AreEqual(1, result2.Count);
      Assert.AreEqual(2, result2[vendor].Count);
    }
    [TestMethod]
    public void GetVendors_EnsureVendorsAreFound_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      Vendor vendor2 = new Vendor("Pizza Hut", "A fine pizza establishment");
      List<Vendor> eList = new List<Vendor> { vendor, vendor2 };
      //act
      List<Vendor> aList = Vendor.GetVendors();
      CollectionAssert.AreEqual(eList, aList);
    }
    [TestMethod]
    public void ClearVendors_EnsureVendorsAreEmptied_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      Vendor vendor2 = new Vendor("Pizza Hut", "A fine pizza establishment");
      List<Vendor> eList = new List<Vendor> { };
      //act
      Vendor.ClearVendors();
      List<Vendor> aList = Vendor.GetVendors();
      CollectionAssert.AreEqual(eList, aList);
    }
    [TestMethod]
    public void DeleteVendor_EnsureSingleVendorIsRemoved_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      Vendor vendor2 = new Vendor("Pizza Hut", "A fine pizza establishment");
      List<Vendor> eList = new List<Vendor> { vendor2 };
      //act
      Vendor.DeleteVendor(0);
      List<Vendor> aList = Vendor.GetVendors();
      CollectionAssert.AreEqual(eList, aList);
    }
    [TestMethod]
    public void FindVendor_EnsureSingleVendorIsFound_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      Vendor eVendor = new Vendor("Pizza Hut", "A fine pizza establishment");
      //act
      Vendor aVendor = Vendor.FindVendor(1);
      Assert.AreEqual(eVendor, aVendor);
    }
    [TestMethod]
    public void FindOrder_EnsureSingleOrderIsFound_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor);
      Order eOrder = new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor);
      //act
      Order aOrder = vendor.FindOrder(1);
      Assert.AreEqual(eOrder, aOrder);
    }
    [TestMethod]
    public void DeleteOrders_EnsureOrdersBecomeEmpty_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor);
      new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor);
      List<Order> eOrders = new List<Order>() { };
      //act
      vendor.DeleteOrders();
      List<Order> aOrders = vendor.GetOrders();
      CollectionAssert.AreEqual(eOrders, aOrders);
    }

    [TestMethod]
    public void DeleteAnOrder_EnsureSingleOrderIsDeleted_ReturnsTrue()
    {
      //Arrange
      Vendor vendor = new Vendor("Taco Bell", "A fine taco establishment");
      new Order("Taco", "A crunchy treat with ground beef and lettuce", 1.99, vendor);
      new Order("Burrito", "A chewy treat with ground beef and red sauce", .99, vendor);
      //act
      vendor.DeleteAnOrder(0);
      List<Order> aOrders = vendor.GetOrders();
      Assert.AreEqual(1, aOrders[0].Id);
    }
  }
}
