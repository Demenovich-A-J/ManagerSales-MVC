using System;
using System.Collections.Generic;
using DAL.ReposytoryModel.Interfaces;

namespace ManagerSales.BL.ModelsHandlers.Intarfaces
{
    public interface IModelHandler<T> where T : class
    {
        T GetItemById(int id);
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        ICollection<T> GetList(Func<T, bool> where);
        ICollection<T> GetAll();

    }
}