using CodeExercise.DataAcccess.Data;
using CodeExercise_Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeExercise.Repository
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> table = null;

        public GenericService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            table = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await table.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            table.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            table.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
