using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VendorAndOrderTrackerSQL.Models;

namespace VendorAndOrderTrackerSQL.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/search")]
    public ActionResult Results(string query)
    {
      Dictionary<Vendor, List<Order>> results = Vendor.SearchOrders(query);
      return View(results);
    }
  }
}