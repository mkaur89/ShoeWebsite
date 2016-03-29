using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMVCSite.Models
{
    public class BasketItem
    {
        public int BasketItemId { get; set; }
				public Guid BasketId { get; set; }
        public int ProductId { get; set; }
			  public virtual Product Product { get; set; }
				public int Quantity { get; set; }
    }
}
