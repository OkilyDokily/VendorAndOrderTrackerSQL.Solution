using Microsoft.AspNetCore.Mvc;
using VendorAndOrderTrackerSQL.Models;

namespace VendorAndOrderTrackerSQL.Controllers
{
  public class OrdersController : Controller
  {

    [HttpGet("/Vendor/{id}/Order/")]
    public ActionResult Index(int id)
    {
      Vendor vendor = Vendor.FindVendor(id);
      return View(vendor);
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
      Vendor vendor = Vendor.FindVendor(id);
      new Order(title, description, price, vendor);
      return RedirectToAction("Index");
    }

    [HttpPost("/Vendor/{id}/Order/delete")]
    public ActionResult DestroyAll(int id)
    {
      Vendor vendor = Vendor.FindVendor(id);
      vendor.DeleteOrders();
      return RedirectToAction("Index");
    }

    [HttpGet("/Vendor/{id}/Order/{num}")]
    public ActionResult Show(int id, int num)
    {
      Vendor vendor = Vendor.FindVendor(id);
      Order order = vendor.FindOrder(num);
      return View(order);
    }

    [HttpGet("/Vendor/{id}/Order/{num}/edit")]
    public ActionResult Edit(int id, int num)
    {
      Vendor vendor = Vendor.FindVendor(id);
      Order order = vendor.FindOrder(num);
      return View(order);
    }

    [HttpPost("/Vendor/{id}/Order/{num}/")]
    public ActionResult Update(string title, string description, double price, int id, int num)
    {
      Vendor vendor = Vendor.FindVendor(id);
      Order order = vendor.FindOrder(num);

      order.Title = title;
      order.Description = description;
      order.Price = price;
      return RedirectToAction("Index");
    }

    [HttpPost("/Vendor/{id}/Order/{num}/delete")]
    public ActionResult Destroy(int id, int num)
    {
      Vendor vendor = Vendor.FindVendor(id);
      vendor.DeleteAnOrder(num);
      return RedirectToAction("Index");
    }
  }
}