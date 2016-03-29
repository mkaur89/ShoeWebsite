using SampleMVCSite.Contracts.Repositories;
using SampleMVCSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCSite.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
				IRepositoryBase<Order> orders;

				public AdminController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products, IRepositoryBase<Order> orders)
        {
            this.customers = customers;
            this.products = products;
						this.orders = orders;
        }//end Constructor

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductsList()
        {
            var model = products.GetAll();
            return View(model);
        }

        public ActionResult CreateProduct()
        {
            var model = new Product();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            products.Insert(product);
            products.Commit();
            return RedirectToAction("ProductsList");
        }

        public ActionResult EditProduct(int id)
        {
            Product product = products.GetById(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            products.Update(product);
            products.Commit();

            return RedirectToAction("ProductsList");
        }
				public ActionResult DetailsProduct(int id)
				{
					var product = products.GetById(id);
					return View(product);
				}

				public ActionResult DeleteProduct(int id)
				{
					Product product = products.GetById(id);
					if (products == null)
					{
						return HttpNotFound();
					}
					return View(product);
				}

				[HttpPost, ActionName("DeleteProduct")]
				[ValidateAntiForgeryToken]
				public ActionResult DeleteConfirm(int id)
				{
					products.Delete(products.GetById(id));
					products.Commit();
					return RedirectToAction("ProductsList");
				}
				public ActionResult CustomersList()
				{
					var model = customers.GetAll();
					return View(model);
				}

				public ActionResult CreateCustomer()
				{
					var model = new Customer();
					return View(model);
				}

				[HttpPost]
				public ActionResult CreateCustomer(Customer customer)
				{
					customers.Insert(customer);
					customers.Commit();
					return RedirectToAction("CustomersList");
				}

				public ActionResult EditCustomer(int id)
				{
					Customer customer = customers.GetById(id);
					return View(customer);
				}
				[HttpPost]
				public ActionResult EditCustomer(Customer customer)
				{
					customers.Update(customer);
					customers.Commit();
					return View(customer);
				}

				public ActionResult DetailsCustomer(int id)
				{
					var customer = customers.GetById(id);
					return View(customer);
				}

				public ActionResult DeleteCustomer(int id)
				{
					var customer = customers.GetById(id);
					return View(customer);
				}


				[HttpPost, ActionName("DeleteCustomer")]
				[ValidateAntiForgeryToken]
				public ActionResult DeleteCustomer(int id, string confirmationMessage)
				{
					var customer = customers.GetById(id);
					customers.Delete(customer);
					customers.Commit();
					return RedirectToAction("CustomersList");
				}

				public ActionResult OrdersList()
				{
					var model = orders.GetAll();
					return View(model);
				}

				public ActionResult CreateOrder()
				{
					var model = new Order();
					return View(model);
				}

				[HttpPost]
				public ActionResult CreateOrder(Order order)
				{
					orders.Insert(order);
					orders.Commit();
					return RedirectToAction("OrdersList");
				}
				public ActionResult EditOrder(int id)
				{
					Order order = orders.GetById(id);
					return View(order);
				}
				[HttpPost]
				public ActionResult EditOrder(Order order)
				{
					orders.Update(order);
					orders.Commit();
					return View(order);
				}

				public ActionResult DetailsOrder(int id)
				{
					var order = orders.GetById(id);
					return View(order);
				}

				public ActionResult DeleteOrder(int id)
				{
					var order = orders.GetById(id);
					return View(order);
				}

				[HttpPost, ActionName("DeleteOrder")]
				[ValidateAntiForgeryToken]
				public ActionResult DeleteOrder(int id, string confirmationMessage)
				{
					orders.Delete(orders.GetById(id));
					orders.Commit();
					return RedirectToAction("OrdersList");
				}
			
    }
}