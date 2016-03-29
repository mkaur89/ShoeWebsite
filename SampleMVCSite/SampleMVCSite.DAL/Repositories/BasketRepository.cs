using SampleMVCSite.Contracts.Data;
using SampleMVCSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMVCSite.DAL.Repositories
{
	public class BasketRepository : RepositoryBase<Basket>
	{
		public BasketRepository(DataContext context)
			: base(context)
		{
			if (context == null)
			{
				throw new ArgumentNullException();
			}
		}
	}

}
