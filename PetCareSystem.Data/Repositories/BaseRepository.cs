using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetCareSystem.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(DbContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes to the database.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Entity of type {typeof(T).Name} with ID {GetEntityId(entity)} updated successfully.");
            return true; // Save successful
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Handle concurrency issues, log the exception
            _logger.LogError(ex, $"Concurrency error occurred while updating entity of type {typeof(T).Name} with ID {GetEntityId(entity)}.");
            return false; // Save failed
        }
        catch (DbUpdateException ex)
        {
            // Handle other database update errors, log the exception
            _logger.LogError(ex, $"Database update error occurred while updating entity of type {typeof(T).Name} with ID {GetEntityId(entity)}.");
            return false; // Save failed
        }
    }

    // Helper method to get the entity ID (assuming it has an 'Id' property)
    private object? GetEntityId(T entity)
    {
        var idProperty = entity.GetType().GetProperty("Id");
        return idProperty?.GetValue(entity);
    }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
