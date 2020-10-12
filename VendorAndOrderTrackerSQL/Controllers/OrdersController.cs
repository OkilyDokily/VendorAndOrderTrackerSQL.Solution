using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VendorAndOrderTrackerSQL.Models;

namespace VendorAndOrderTrackerSQL.Controllers
{
  public class OrdersController : Controller
  {
    [HttpGet("/Vendor/{id}/Order/")]
    public ActionResult Index(int id)
    {
      Vendor vendor = Vendor.FindVendor(id);
      List<Order> orders = Order.GetOrdersForVendorById(id);
      Dictionary<Vendor, List<Order>> d = new Dictionary<Vendor, List<Order>>();
      d.Add(vendor, orders);
      return View(d);
    }

    [HttpGet("/Vendor/{id}/Order/new")]
    public ActionResult New(int id)
    {
      Vendor vendor = Vendor.FindVendor(id);
      return View(vendor);
    }

    [HttpPost("/Vendor/{id}/Order/")]
    public ActionResult Create(string title, string description, double price, int id)
    {
      new Order(title, description, price, id);
      return RedirectToAction("Index");
    }

    [HttpPost("/Vendor/{id}/Order/delete")]
    public ActionResult DestroyAll(int id)
    {
      Order.ClearOrdersFromVendorById(id);
      return RedirectToAction("Index");
    }

    [HttpGet("/Vendor/{id}/Order/{num}")]
    public ActionResult Show(int num)
    {
      Order order = Order.FindOrder(num);
      return View(order);
    }

    [HttpGet("/Vendor/{id}/Order/{num}/edit")]
    public ActionResult Edit(int num)
    {
      Order order = Order.FindOrder(num);
      return View(order);
    }

    [HttpPost("/Vendor/{id}/Order/{num}/")]
    public ActionResult Update(string title, string description, double price, int num)
    {
      Order order = Order.FindOrder(num);
      order.Title = title;
      order.Description = description;
      order.Price = price;
      order.Update();
      return RedirectToAction("Index");
    }

    [HttpPost("/Vendor/{id}/Order/{num}/delete")]
    public ActionResult Destroy(int num)
    {
      Order.DeleteAnOrder(num);
      return RedirectToAction("Index");
    }
  }
}