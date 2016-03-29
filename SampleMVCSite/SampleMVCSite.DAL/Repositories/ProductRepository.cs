﻿using SampleMVCSite.Contracts.Data;
using SampleMVCSite.DAL.Repositories;
using SampleMVCSite.Models;
using System;

namespace SampleMVCSite.Contracts.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(DataContext context)
            : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }//end ProductRepository

}//end namespace

