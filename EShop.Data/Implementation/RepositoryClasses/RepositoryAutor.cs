﻿using EShop.Data.Implementation.Interfaces;
using EShop.Model;
using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Implementation.RepositoryClasses
{

    /// <inheritdoc/>
    /// <summary>
    /// 
    /// </summary>
    public class RepositoryAutor : IRepositoryAutor
    {
        private readonly ShopContext context;
        public RepositoryAutor(ShopContext context) => this.context = context;
        public void Add(Autor entity) => throw new NotImplementedException();
        public Autor Find(Predicate<Autor> p) => context.Autor.ToList().Find(p);
        public List<Autor> GetAll() => throw new NotImplementedException();
    }
}
