﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookwala.DataAccess.Repository.IRepository
{
    internal interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(Func<T, bool> filer);

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
