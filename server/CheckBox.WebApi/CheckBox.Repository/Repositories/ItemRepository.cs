using CheckBox.Repository.Interfaces;
using Microsoft.EntityFrameworkCore; 
using System;                           
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckBox.Repository.Repositories
{       
                                    
    public class ItemRepository<T>(IContext context) : IRepository<T> where T : class
    {
        private readonly IContext _ctx = context;

        public async Task<T> AddItem(T item)
        {
            await _ctx.Set<T>().AddAsync(item);
            await _ctx.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var item = await _ctx.Set<T>().FindAsync(id);
            if (item != null)
            {    
                _ctx.Set<T>().Remove(item);
                await _ctx.Save();
            }
        }

        public async Task<List<T>> GetAll()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateItem(int id, T item)
        {
            _ctx.Set<T>().Update(item);
            await _ctx.Save();
            return item;
        }
    }
}