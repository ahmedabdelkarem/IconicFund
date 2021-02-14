using IconicFund.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace IconicFund.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        IconicFundDbContext _context;

        public BaseRepository(IconicFundDbContext context)
        {
            _context = context;
        }

        private IQueryable<T> InsializeQuery<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        //all
        public List<T> GetAll<T>(bool withTracking) where T : class
        {
            if (withTracking)
                return InsializeQuery<T>().ToList();
            else
                return InsializeQuery<T>().AsNoTracking().ToList();
        }
        public List<T> GetAll<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            return InsializeQuery(includes).ToList<T>();
        }
        public async Task<List<T>> GetAllAsync<T>(bool withTracking) where T : class
        {
            if (withTracking)
                return await InsializeQuery<T>().ToListAsync();
            else
                return await InsializeQuery<T>().AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            return await InsializeQuery(includes).ToListAsync();
        }
        //all where
        public List<T> GetAllWhere<T>(Expression<Func<T, bool>> predicate, bool withTracking = true) where T : class
        {
            if (withTracking)
                return InsializeQuery<T>().Where(predicate).ToList();
            else
                return InsializeQuery<T>().Where(predicate).AsNoTracking().ToList();
        }
        public List<T> GetAllWhere<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            return InsializeQuery<T>(includes).Where(predicate).ToList();
        }
        public async Task<List<T>> GetAllWhereAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking = true) where T : class

        {
            if (withTracking)
                return await InsializeQuery<T>().Where(predicate).ToListAsync();
            else
                return await InsializeQuery<T>().Where(predicate).AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllWhereAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            return await InsializeQuery<T>(includes).Where(predicate).ToListAsync();
        }
        public async Task<List<T>> GetAllWhereAsync<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class
        {
            var query = InsializeQuery<T>();

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            
            return await query.Where(predicate).ToListAsync();
        }
        public IQueryable<T> GetAllWhereQ<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            return InsializeQuery<T>(includes).Where(predicate);
        }

        public IQueryable<T> GetAllWhereQ<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return InsializeQuery<T>().Where(predicate);
        }


        //all with paging
        public List<T> GetAllWithPaging<T>(int skip, int take, bool withTracking) where T : class
        {
            if (withTracking)
                return InsializeQuery<T>().Skip(skip).Take(take).ToList();
            else
                return InsializeQuery<T>().Skip(skip).Take(take).AsNoTracking().ToList();
        }
        public List<T> GetAllWithPaging<T>(int skip, int take, params Expression<Func<T, object>>[] includes) where T : class
        {
            return InsializeQuery(includes).Skip(skip).Take(take).ToList<T>();
        }
        public async Task<List<T>> GetAllWithPagingAsync<T>(int skip, int take, bool withTracking) where T : class
        {
            if (withTracking)
                return await InsializeQuery<T>().Skip(skip).Take(take).ToListAsync();
            else
                return await InsializeQuery<T>().Skip(skip).Take(take).AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllWithPagingAsync<T>(int skip, int take, params Expression<Func<T, object>>[] includes) where T : class
        {
            return await InsializeQuery(includes).Skip(skip).Take(take).ToListAsync();
        }
        //all with paging where
        public List<T> GetAllWithPagingWhere<T>(Expression<Func<T, bool>> predicate, int skip, int take, bool withTracking = true) where T : class
        {
            if (withTracking)
                return InsializeQuery<T>().Skip(skip).Take(take).Where(predicate).ToList();
            else
                return InsializeQuery<T>().Skip(skip).Take(take).Where(predicate).AsNoTracking().ToList();
        }
        public List<T> GetAllWithPagingWhere<T>(Expression<Func<T, bool>> predicate, int skip, int take, params Expression<Func<T, object>>[] includes) where T : class
        {
            return InsializeQuery<T>(includes).Skip(skip).Take(take).Where(predicate).ToList();
        }
        public async Task<List<T>> GetAllWithPagingWhereAsync<T>(Expression<Func<T, bool>> predicate, int skip, int take, bool withTracking = true) where T : class
        {
            if (withTracking)
                return await InsializeQuery<T>().Skip(skip).Take(take).Where(predicate).ToListAsync();
            else
                return await InsializeQuery<T>().Skip(skip).Take(take).Where(predicate).AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllWithPagingWhereAsync<T>(Expression<Func<T, bool>> predicate, int skip, int take, params Expression<Func<T, object>>[] includes) where T : class
        {
            return await InsializeQuery<T>(includes).Skip(skip).Take(take).Where(predicate).ToListAsync();
        }

        //Group By
        public IQueryable<IGrouping<TKey, T>> GetWithGroupBy<T, TKey>(Expression<Func<T, TKey>> groupBy, params Expression<Func<T, object>>[] includes) where T : class
        {
            return InsializeQuery<T>(includes).GroupBy(groupBy);
        }

        //first or default
        public T FirstOrDefault<T>(int Id, bool withTracking) where T : class
        {
            if (withTracking)
                return _context.Set<T>().FindQuery(Id).FirstOrDefault();
            else
                return _context.Set<T>().FindQuery(Id).AsNoTracking().FirstOrDefault();
        }
        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, bool withTracking) where T : class
        {
            if (withTracking)
                return InsializeQuery<T>().FirstOrDefault(predicate);
            else
                return InsializeQuery<T>().AsNoTracking().FirstOrDefault(predicate);
        }
        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            return InsializeQuery(includes).FirstOrDefault(predicate);
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, string[] includes) where T : class
        {
            var query = InsializeQuery<T>();

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(predicate);
        }

        public async Task<T> FirstOrDefaultAsync<T>(int Id, bool withTracking) where T : class
        {
            if (withTracking)
                return await _context.Set<T>().FindQuery(Id).FirstOrDefaultAsync();
            else
                return await _context.Set<T>().FindQuery(Id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking) where T : class
        {
            if (withTracking)
                return await InsializeQuery<T>().FirstOrDefaultAsync(predicate);
            else
                return await InsializeQuery<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            return await InsializeQuery(includes).FirstOrDefaultAsync(predicate);
        }
        //any
        public bool Any<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return InsializeQuery<T>().Any(predicate);
        }
        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await InsializeQuery<T>().AnyAsync(predicate);
        }
        //count
        public int Count<T>() where T : class
        {
            return _context.Set<T>().Count();
        }
        public async Task<int> CountAsync<T>() where T : class
        {
            return await _context.Set<T>().CountAsync();
        }
        //add
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
        }
        public void AddRange<T>(List<T> entities) where T : class
        {
            _context.AddRange(entities);
        }
        public async Task AddRangeAsync<T>(List<T> entities) where T : class
        {
            await _context.AddAsync(entities);
        }
        //update
        public void Update<T>(T entity) where T : class
        {
            _context.Attach(entity);
            _context.Update<T>(entity);
        }
        public void UpdateRange<T>(List<T> entities) where T : class
        {
            _context.UpdateRange(entities);
        }
        //remove
        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void Remove<T>(int Id, bool withTracking) where T : class
        {
            var entity = FirstOrDefault<T>(Id, withTracking);
            _context.Remove(entity);
        }
        public void Remove<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            T entity = FirstOrDefault(predicate);
            if (entity != null) _context.Remove(entity);
        }
        public async Task RemoveAsync<T>(int Id, bool withTracking) where T : class
        {
            var entity = await FirstOrDefaultAsync<T>(Id, withTracking);
            _context.Remove(entity);
        }
        public async Task RemoveAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            T entity = await FirstOrDefaultAsync(predicate);
            if (entity != null) _context.Remove(entity);
        }
        public void RemoveRange<T>(List<T> entities) where T : class
        {
            _context.RemoveRange(entities);
        }
        //save changes
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void  AddAndReturnObject<T>(ref  T entity) where T : class
        {
            _context.Add(entity);
            this.SaveChanges();
             
        }


    }
}
