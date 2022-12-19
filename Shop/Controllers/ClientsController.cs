using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
  public class ClientsController : Controller
  {

    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("/clients/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/clients")]
    public ActionResult Create(string clientName)
    {
      Client newClient = new Client(clientName);
      return RedirectToAction("Index");
    }

    [HttpGet("/clients/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Client selectedClient = Client.Find(id);
      List<Order> categoryItems = selectedClient.Orders;
      model.Add("client", selectedClient);
      model.Add("orders", categoryItems);
      return View(model);
    }


    // This one creates new Items within a given Category, not new Categories:

    [HttpPost("/clients/{clientId}/orders")]
    public ActionResult Create(int clientId, string orderDescription)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Client foundClient = Client.Find(clientId);
      Order newOrder = new Order(orderDescription);
      newOrder.Save();    // New code
      foundClient.AddOrder(newOrder);
      List<Order> clientOrders = foundClient.Orders;
      model.Add("orders", clientOrders);
      model.Add("client", foundClient);
      return View("Show", model);
    }

  }
}