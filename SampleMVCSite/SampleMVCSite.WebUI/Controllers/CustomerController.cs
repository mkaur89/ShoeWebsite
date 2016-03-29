using SampleMVCSite.Contracts.Repositories;
using SampleMVCSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCSite.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
       IRepositoryBase<Customer> customers;
				
				public CustomerController(IRepositoryBase<Customer> customers)
				{
					this.customers = customers;
				}

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
				public ActionResult CustomersList()
				{
					var model = customers.GetAll();
					return View(model);
				}
				
				public ActionResult Details(int id)
				{
					var customer = customers.GetById(id);
					return View(customer);
				}

    }
}