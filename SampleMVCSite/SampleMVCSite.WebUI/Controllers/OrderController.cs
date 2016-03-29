using SampleMVCSite.Contracts.Repositories;
using SampleMVCSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCSite.WebUI.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
           IRepositoryBase<Order> orders;


					 public OrderController(IRepositoryBase<Order> orders)
        {
          
            this.orders = orders;            
        }

        public ActionResult Index()
        {
            var model = orders.GetAll();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var order = orders.GetById(id);
            return View(order);
        }
    }
}