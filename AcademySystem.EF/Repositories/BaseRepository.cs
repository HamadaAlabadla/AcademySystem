using Microsoft.EntityFrameworkCore;
using AcademySystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext dBContext;

        public BaseRepository(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public T? GetById(int id)
        {
            return dBContext.Set<T>().Find(id);
        }

        public T? Create(T obj)
        {
            try
            {
                dBContext.Set<T>().Add(obj);
                dBContext.SaveChanges();
            }
            catch
            {
                return null;
            }
            return obj;
        }

        public async Task<T?> CreateAsync(T obj)
        {
            try
            {
                await dBContext.Set<T>().AddAsync(obj);
                await dBContext.SaveChangesAsync();
            }
            catch
            {
                return null;
            }
            return obj;
        }

        public T? Delete(int id)
        {
            var obj = GetById(id);
            if (obj == null)
                return null;
            try
            {
                dBContext.Set<T>().Remove(obj);
                dBContext.SaveChanges();
            }
            catch
            {
                return null;
            }
            return obj;
        }


        public List<T>? GetAll()
        {
            return dBContext.Set<T>().ToList();
        }

        public async Task<List<T>?> GetAllAsync()
        {
            return await dBContext.Set<T>().ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await dBContext.Set<T>().FindAsync(id);
        }

        public T? Update(T obj)
        {
            try
            {
                dBContext.Set<T>().Update(obj);
                dBContext.SaveChanges();
            }
            catch
            {
                return null;
            }
            return obj;
        }

    }
}
