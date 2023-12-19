using Microsoft.EntityFrameworkCore;
using ShopBee.Data;
using ShopBee.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Quic;
using System.Text;

namespace ShopBee.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _db;
        internal DbSet<T> dbSet;
        public Repository(DatabaseContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //_db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        public int GetNumberOfBooks()
        {
            return _db.Books.Count();
        }
        public int GetNumberOfUsers()
        {
            return _db.Users.Count();
        }
        public int GetNumberOfOrders()
        {
            return _db.Orders.Count();
        }
        public int GetNumberOfStores()
        {
            return _db.Stores.Count();
        }
    }
}
