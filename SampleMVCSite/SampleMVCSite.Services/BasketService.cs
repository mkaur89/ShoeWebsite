using SampleMVCSite.Contracts.Repositories;
using SampleMVCSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SampleMVCSite.Services
{
    public class BasketService
    {
			IRepositoryBase<Basket> baskets;
			IRepositoryBase<BasketItem> basketItems;

			public const string BasketSessionName = "eShoppingBasket";

			public BasketService(IRepositoryBase<Basket> baskets, IRepositoryBase<BasketItem> basketItems)
			{
				this.baskets = baskets;
				this.basketItems = basketItems;
			}

			private Basket createNewBasket(HttpContextBase httpContext)
			{
				//create a new basket.

				//first create a new cookie.
				HttpCookie cookie = new HttpCookie(BasketSessionName);
				//now create a new basket and set the creation date.
				Basket basket = new Basket();
				basket.OrderDate = DateTime.Now;
				basket.BasketID = Guid.NewGuid();

				//add and persist in the dabase.
				baskets.Insert(basket);
				baskets.Commit();

				//add the basket id to a cookie
				cookie.Value = basket.BasketID.ToString();
				cookie.Expires = DateTime.Now.AddDays(1);
				httpContext.Response.Cookies.Add(cookie);

				return basket;
			}

			public bool AddToBasket(HttpContextBase httpContext, int productId, int quantity)
			{
				bool success = true;
				Basket basket = GetBasket(httpContext);

				BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
				if (item == null)
				{
					item = new BasketItem()
					{
						BasketId = basket.BasketID,
						ProductId = productId,
						Quantity = quantity
					};
					basket.BasketItems.Add(item);
				}
				else
				{
					item.Quantity = item.Quantity + quantity;
				}
				baskets.Commit();
				return success;
			}
			//Added new function to remove basketItem from basket
			public bool RemoveFromBasket(HttpContextBase httpContext, int basketItemId)
			{
				bool success = true;
				basketItems.Delete(basketItems.GetById(basketItemId));
				basketItems.Commit();
				return success;
			}

			public Basket GetBasket(HttpContextBase httpContext)
			{
				HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
				Basket basket;
				Guid basketId;
				if (cookie != null)//checks if cookie is null
				{
					if (Guid.TryParse(cookie.Value, out basketId))
					{
						basket = baskets.GetById(basketId);
					}
					else
					{
						basket = createNewBasket(httpContext);
					}//end inner if-else
				}//end outer if
				else
				{
					basket = createNewBasket(httpContext);
				}
				return basket;
			}

			public decimal GetTotalPrice(HttpContextBase httpContext)
			{
				HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
				Basket basket;
				Guid basketId;
				decimal totalPrice = 0.0m;
				Guid.TryParse(cookie.Value, out basketId);
				basket = baskets.GetById(basketId);
				foreach(BasketItem item in basket.BasketItems)
				{
					totalPrice += item.Product.Price * item.Quantity;
				}
				return totalPrice;
			}

			public int GetTotalQuantity(HttpContextBase httpContext)
			{
				HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
				Basket basket;
				Guid basketId;
				int totalQuantity = 0;
				Guid.TryParse(cookie.Value, out basketId);
				basket = baskets.GetById(basketId);
				foreach (BasketItem item in basket.BasketItems)
				{
					totalQuantity += item.Quantity;
				}
				return totalQuantity;
			}
    }
}
