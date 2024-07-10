using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CaseItau.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task ExecuteQueryAsync(Expression<Func<IQueryable<T>, IQueryable<T>>> query, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> dbQuery = _dbSet;

            // Aplica os includes se houver
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    dbQuery = dbQuery.Include(include);
                }
            }

            // Aplica a consulta customizada
            dbQuery = query.Compile()(dbQuery);

            // Executa a consulta e salva as mudanças no contexto
            await _context.SaveChangesAsync();
        }
    }
}
