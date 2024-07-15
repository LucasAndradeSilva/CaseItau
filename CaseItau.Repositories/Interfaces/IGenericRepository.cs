using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        public Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        public Task AddAsync(T entity);
        public void Update(T entity);
        public void Remove(T entity);
        public Task<IQueryable<T>> ExecuteQueryAsync(Expression<Func<IQueryable<T>, IQueryable<T>>> query, params Expression<Func<T, object>>[] includes);
    }
}
