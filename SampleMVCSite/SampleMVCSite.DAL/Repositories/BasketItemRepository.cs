using SampleMVCSite.Contracts.Data;
using SampleMVCSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMVCSite.DAL.Repositories
{
	public class BasketItemRepository : RepositoryBase<BasketItem>
	{
		public BasketItemRepository(DataContext context)
			: base(context)
		{
			if (context == null)
			{
				throw new ArgumentNullException();
			}
		}
	}

}
