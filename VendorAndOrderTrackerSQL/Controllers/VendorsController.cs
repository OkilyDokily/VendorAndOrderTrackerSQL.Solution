using Microsoft.AspNetCore.Mvc;
using VendorAndOrderTrackerSQL.Models;

namespace VendorAndOrderTrackerSQL.Controllers
{
  public class VendorsController : Controller
  {
    // [HttpGet("/Vendor")]
    // public ActionResult Index()
    // {
    //   return View(Vendor.GetVendors());
    // }

    // [HttpGet("/Vendor/{id}")]
    // public ActionResult Show(int id)
    // {
    //   Vendor vendor = Vendor.FindVendor(id);
    //   return View(vendor);
    // }

    // [HttpGet("/Vendor/{id}/edit")]
    // public ActionResult Edit(int id)
    // {
    //   Vendor vendor = Vendor.FindVendor(id);
    //   return View(vendor);
    // }

    // [HttpPost("/Vendor/{id}/")]
    // public ActionResult Update(string name, string description, int id)
    // {
    //   Vendor vendor = Vendor.FindVendor(id);
    //   vendor.Name = name;
    //   vendor.Description = description;
    //   return RedirectToAction("Index");
    // }

    // [HttpPost("/Vendor/{id}/delete")]
    // public ActionResult Delete(int id)
    // {
    //   Vendor.DeleteVendor(id);
    //   return RedirectToAction("Index");
    // }


    // [HttpPost("/Vendor/delete")]
    // public ActionResult DeleteAll(int id)
    // {
    //   Vendor.ClearVendors();
    //   return RedirectToAction("Index");
    // }

    // [HttpGet("/Vendor/new")]
    // public ActionResult New()
    // {
    //   return View();
    // }

    // [HttpPost("/Vendor/")]
    // public ActionResult Create(string name, string description)
    // {
    //   new Vendor(name, description);
    //   return RedirectToAction("Index");
    // }
  }
}