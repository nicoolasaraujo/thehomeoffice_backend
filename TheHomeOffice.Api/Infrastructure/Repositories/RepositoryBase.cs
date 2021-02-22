using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheHomeOffice.aApi.Infrastructure.Database;
using TheHomeOffice.Api.Domain.Interfaces.Repositories;

namespace TheHomeOffice.Api.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private TheHomeOfficeContext dbContext;

        public RepositoryBase(TheHomeOfficeContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            var createdEntity = await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return createdEntity.Entity;
        }

        public Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>().AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }
    }
}
