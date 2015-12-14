using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.ReposytoryModel.Interfaces;
using ManagerSales.BL.ModelsHandlers.Intarfaces;

namespace ManagerSales.BL.ModelsHandlers.AbstractClasses
{
    public abstract class ModelHandler<T,K> : IModelHandler<T>
        where T : class 
        where K : class
    {
        protected ModelHandler()
        {
            Mapper.CreateMap<T, K>();
            Mapper.CreateMap<K, T>();
        }

        protected abstract IGenericDataRepository<K> Repository { get; }
        protected abstract T DalToBlModel(K item);
        protected abstract K BlToDalModel(T item);

        public T GetItemById(int id)
        {
            return DalToBlModel(Repository.FindById(id));
        }

        public virtual void Add(T item)
        {
            Repository.Add(BlToDalModel(item));
        }

        public void Remove(T item)
        {
            Repository.Remove(BlToDalModel(item));
        }

        public void Update(T item)
        {
            Repository.Update(BlToDalModel(item));
        }


        public ICollection<T> GetList(Func<T, bool> @where)
        {
            return new List<T>(Repository.GetAll().Select(DalToBlModel).Where(where));
        }

        public ICollection<T> GetAll()
        {
            return new List<T>(Repository.GetAll().Select(DalToBlModel));

        }

        public abstract bool IsExist(K item, IGenericDataRepository<K> repository);
        
    }
}