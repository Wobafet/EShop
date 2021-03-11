﻿using EShop.Model;
using EShop.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{
    public class RepositoryCustomer : IRepositoryCustomer
    {
        private ShopContext shopContext;

        public RepositoryCustomer(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public void Add(Customer entity)
        {
            shopContext.Add(entity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Customer Find(Predicate<Customer> condition)
        {
            return   shopContext.Customer.Include(c=>c.Address).ToList().Find(condition);
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            shopContext.Update(customer);
        }

    }
}
