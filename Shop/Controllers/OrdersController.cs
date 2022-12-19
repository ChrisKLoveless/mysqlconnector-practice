using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Controllers
{
  public class OrdersController : Controller
  {

    [HttpGet("/clients/{clientId}/orders/new")]
    public ActionResult New(int clientId)
    {
      Client client = Client.Find(clientId);
      return View(client);
    }

    [HttpGet("/clients/{clientId}/orders/{orderId}")]
    public ActionResult Show(int clientId, int orderId)
    {
      Order order = Order.Find(orderId);
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("order", order);
      model.Add("client", client);
      return View(model);
    }
  }
}