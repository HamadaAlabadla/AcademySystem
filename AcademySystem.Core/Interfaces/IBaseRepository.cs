using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        List<T>? GetAll();
        Task<List<T>?> GetAllAsync();

        T? Create(T obj);
        Task<T?> CreateAsync(T obj);
        T? Update(T obj);
        T?  Delete(int id);
    }
}
