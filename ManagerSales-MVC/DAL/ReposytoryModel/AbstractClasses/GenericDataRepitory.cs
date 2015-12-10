using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.ReposytoryModel.Interfaces;
using EntytiModel;

namespace DAL.ReposytoryModel.AbstractClasses
{
    public abstract class GenericDataRepitory<T, K> : IGenericDataRepository<T>
        where T : class
        where K : class
    {
        protected abstract K ObjectToEntity(T item);

        protected abstract T EntityToObject(K item);

        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<T> list;
            using (var context = new ManagerSaleDBEntities())
            {
                list = context
                    .Set<K>()
                    .AsNoTracking()
                    .Select(EntityToObject)
                    .ToList();
            }
            return list;
        }

        public virtual IEnumerable<T> GetList(Func<T, bool> where)
        {
            IEnumerable<T> list;
            using (var context = new ManagerSaleDBEntities())
            {
                list = context.Set<K>().AsNoTracking().Select(EntityToObject).Where(where).ToList();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where)
        {
            T item;
            using (var context = new ManagerSaleDBEntities())
            {
                item = context
                    .Set<K>()
                    .Select(EntityToObject)
                    .FirstOrDefault(where);
            }
            return item;
        }

        public abstract T GetSingle(T item);
        public T FindById(int id)
        {
            T item;
            using (var context = new ManagerSaleDBEntities())
            {
                item = EntityToObject(context
                    .Set<K>()
                    .Find(id));
            }
            return item;
        }

        public virtual void Add(T item)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                context.Entry(ObjectToEntity(item)).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Add(IEnumerable<T> items)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                foreach (var item in items)
                {
                    context.Entry(ObjectToEntity(item)).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public virtual void Remove(T item)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                context.Entry(item).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual void Update(T item)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}