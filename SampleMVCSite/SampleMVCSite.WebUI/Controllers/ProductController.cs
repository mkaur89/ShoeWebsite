using SampleMVCSite.Contracts.Repositories;
using SampleMVCSite.Models;
using SampleMVCSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCSite.WebUI.Controllers
{
	    public class ProductController : Controller
    {
				IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        IRepositoryBase<Basket> baskets;
        IRepositoryBase<BasketItem> basketItems;
        BasketService basketService;

        public ProductController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products, IRepositoryBase<Basket> baskets, IRepositoryBase<BasketItem> basketItems)
        {
            this.customers = customers;
            this.products = products;
            this.baskets = baskets;
            this.basketItems = basketItems;
            basketService = new BasketService(this.baskets, this.basketItems);
        }


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

				public ActionResult ProductsList()
				{
					var model = products.GetAll();
					return View(model);
				}

				public ActionResult Details(int id)
				{
					var product = products.GetById(id);
					return View(product);
				}

				public ActionResult AddToBasket(int id)
				{
					basketService.AddToBasket(this.HttpContext, id, 1);//always add one to the basket
					return RedirectToAction("BasketSummary");
				}

				public ActionResult BasketSummary()
				{
					var model = basketService.GetBasket(this.HttpContext);
					ViewBag.TotalPrice = basketService.GetTotalPrice(this.HttpContext);
					ViewBag.TotalQuantity = basketService.GetTotalQuantity(this.HttpContext);
					return View(model.BasketItems);
				}

				public ActionResult DeleteFromBasket(int id)
				{
					Basket basket = basketService.GetBasket(this.HttpContext);

					BasketItem item = basket.BasketItems.Where(i => i.BasketItemId == id).First();
					if (item == null)
					{
						return HttpNotFound();
					}
					return View(item);
				}

				[HttpPost, ActionName("DeleteFromBasket")]
				[ValidateAntiForgeryToken]
				public ActionResult DeleteConfirm(int id)
				{
					basketService.RemoveFromBasket(this.HttpContext, id);
					return RedirectToAction("BasketSummary");
				}

				public ActionResult EditBasketItem(int id)
				{
					BasketItem basketItem = basketItems.GetById(id);
					return View(basketItem);
				}

				[HttpPost]
				public ActionResult EditBasketItem(BasketItem basketItem)
				{
					basketItems.Update(basketItem);
					basketItems.Commit();
					return View(basketItem);
				}
    }
}