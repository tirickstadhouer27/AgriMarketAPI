using System.Collections.Generic;

namespace AgriMarketAPI.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        
        // FIX: Add '?' so it explicitly allows returning null if an ID isn't found
        T? GetById(int id); 
        
        void Add(T entity);
        void Delete(int id);
    }
}