using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CaseItau.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CaseItau.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _logger;

        public GenericRepository(DbContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                _logger.LogInformation("Iniciando o GetAsync - [GenericRepository]");
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching the entity with predicate.");
                throw ex;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                _logger.LogInformation("Iniciando o AnyAsync - [GenericRepository]");
                return await _dbSet.AsNoTracking().AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of the entity with predicate.");
                throw ex;
            }
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                _logger.LogInformation("Iniciando o SingleOrDefaultAsync - [GenericRepository]");
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching single or default entity with predicate.");
                throw ex;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                _logger.LogInformation("Iniciando o AddAsync - [GenericRepository]");
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding the entity.");
                throw ex;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _logger.LogInformation("Update o AddAsync - [GenericRepository]");
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the entity.");
                throw ex;
            }
        }

        public void Remove(T entity)
        {
            try
            {
                _logger.LogInformation("Iniciando o Remove - [GenericRepository]");
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing the entity.");
                throw ex;
            }
        }

        public async Task<IQueryable<T>> ExecuteQueryAsync(Expression<Func<IQueryable<T>, IQueryable<T>>> query, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                _logger.LogInformation("Iniciando o ExecuteQueryAsync - [GenericRepository]");
                IQueryable<T> dbQuery = _dbSet;
                
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        dbQuery = dbQuery.Include(include);
                    }
                }
                
                dbQuery = query.Compile()(dbQuery);
                
                await _context.SaveChangesAsync();

                return dbQuery;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing the query.");
                throw ex;
            }
        }
    }
}
