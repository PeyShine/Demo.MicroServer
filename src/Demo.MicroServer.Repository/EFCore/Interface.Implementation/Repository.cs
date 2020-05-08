using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.MicroServer.Repository
{
    public class Repository<T,Context> : IRepository<T,Context>
        where T : class
        where Context:DbContext
    {

        private Context _dbContext;
        private readonly DbSet<T> _dbSet;
        
        public Repository(Context dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();            
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (this._dbContext.Database.CurrentTransaction == null)
            {
                this._dbContext.Database.BeginTransaction(isolationLevel);
            }
        }

        public void Commit()
        {
            var transaction = this._dbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Rollback()
        {
            if (this._dbContext.Database.CurrentTransaction != null)
            {
                this._dbContext.Database.CurrentTransaction.Rollback();
            }
        }

        public int SaveChanges()
        {
            return this._dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._dbContext.SaveChangesAsync();
        }

        public IQueryable<T> Entities
        {
            get { return this._dbSet.AsNoTracking(); }
        }

        public IQueryable<T> TrackEntities
        {
            get { return this._dbSet; }
        }

        public T Add(T entity, bool isSave = true)
        {
            this._dbSet.Add(entity);
            if (isSave)
            {
                this.SaveChanges();
            }
            return entity;
        }
        public async Task<T> AddAsync(T entity, bool isSave = true)
        {
            await this._dbSet.AddAsync(entity);
            if (isSave)
            {
                await this.SaveChangesAsync();
            }
            return entity;
        }

        public void AddRange(IEnumerable<T> entitys, bool isSave = true)
        {
            this._dbSet.AddRange(entitys);
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> entitys, bool isSave = true)
        {
            await this._dbSet.AddRangeAsync(entitys);
            if (isSave)
            {
                await this.SaveChangesAsync();
            }
        }

        public void Delete(T entity, bool isSave = true)
        {
            this._dbSet.Remove(entity);
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task DeleteAsync(T entity, bool isSave = true)
        {
            this._dbSet.Remove(entity);
            if (isSave)
            {
                await this.SaveChangesAsync();
            }
        }

        public void Delete(bool isSave = true, params T[] entitys)
        {
            this._dbSet.RemoveRange(entitys);
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task DeleteAsync(bool isSave = false, params T[] entitys)
        {
            this._dbSet.RemoveRange(entitys);
            if (isSave)
            {
                await this.SaveChangesAsync();
            }
        }

        public void Delete(object id, bool isSave = true)
        {
            this._dbSet.Remove(this._dbSet.Find(id));
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task DeleteAsync(object id, bool isSave = true)
        {
            this._dbSet.Remove(await this._dbSet.FindAsync(id));
            if (isSave)
            {
                await this.SaveChangesAsync();
            }
        }

        public void Delete(Expression<Func<T, bool>> @where, bool isSave = true)
        {
            T[] entitys = this._dbSet.Where<T>(@where).ToArray();
            if (entitys.Length > 0)
            {
                this._dbSet.RemoveRange(entitys);
            }
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> @where, bool isSave = true)
        {
            T[] entitys = this._dbSet.Where<T>(@where).ToArray();
            if (entitys.Length > 0)
            {
                this._dbSet.RemoveRange(entitys);
            }
            if (isSave)
            {
                await this.SaveChangesAsync();
            }
        }

        public void Update(T entity, bool isSave = true)
        {
            _dbSet.Update(entity);
            //var entry = this._dbContext.Entry(entity);
            //if (entry.State == EntityState.Detached)
            //{
            //    entry.State = EntityState.Modified;
            //}
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task UpdateAsync(T entity, bool isSave = true)
        {
            _dbSet.Update(entity);
            if (isSave)
            {
                await this.SaveChangesAsync();
            }
        }

        public void Update(bool isSave = true, params T[] entitys)
        {
            _dbSet.UpdateRange(entitys);
            //var entry = this._dbContext.Entry(entitys);
            //if (entry.State == EntityState.Detached)
            //{
            //    entry.State = EntityState.Modified;
            //}
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task UpdateAsync(bool isSave = true, params T[] entitys)
        {
            _dbSet.UpdateRange(entitys);            
            if (isSave)
            {
                await this.SaveChangesAsync();
            }
        }

        public bool Any(Expression<Func<T, bool>> @where)
        {
            return this._dbSet.AsNoTracking().Any(@where);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> @where)
        {
            return await this._dbSet.AsNoTracking().AnyAsync(@where);
        }

        public int Count()
        {
            return this._dbSet.AsNoTracking().Count();
        }

        public async Task<int> CountAsync()
        {
            return await this._dbSet.AsNoTracking().CountAsync();
        }

        public int Count(Expression<Func<T, bool>> @where)
        {
            return this._dbSet.AsNoTracking().Count(@where);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> @where)
        {
            return await this._dbSet.AsNoTracking().CountAsync(@where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            return this._dbSet.AsNoTracking().FirstOrDefault(@where);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where)
        {
            return await this._dbSet.AsNoTracking().FirstOrDefaultAsync(@where);
        }

        public T FirstOrDefault<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return this._dbSet.AsNoTracking().OrderByDescending(order).FirstOrDefault(@where);
            }
            else
            {
                return this._dbSet.AsNoTracking().OrderBy(order).FirstOrDefault(@where);
            }
        }

        public async Task<T> FirstOrDefaultAsync<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return await this._dbSet.AsNoTracking().OrderByDescending(order).FirstOrDefaultAsync(@where);
            }
            else
            {
                return await this._dbSet.AsNoTracking().OrderBy(order).FirstOrDefaultAsync(@where);
            }
        }

        public IQueryable<T> Distinct(Expression<Func<T, bool>> @where)
        {
            return this._dbSet.AsNoTracking().Where(@where).Distinct();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> @where)
        {
            return this._dbSet.Where(@where);
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return this._dbSet.Where(@where).OrderByDescending(order);
            }
            else
            {
                return this._dbSet.Where(@where).OrderBy(order);
            }
        }

        public IEnumerable<T> Where<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            count = Count();
            if (isDesc)
            {
                return this._dbSet.Where(@where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return this._dbSet.Where(@where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            count = Count(@where);
            if (isDesc)
            {
                return this._dbSet.Where(@where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return this._dbSet.Where(@where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        public IQueryable<T> GetAll()
        {
            return this._dbSet.AsNoTracking();
        }

        public IQueryable<T> GetAll<TOrder>(Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return this._dbSet.AsNoTracking().OrderByDescending(order);
            }
            else
            {
                return this._dbSet.AsNoTracking().OrderBy(order);
            }
        }

        public T GetById<Ttype>(Ttype id)
        {
            return this._dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync<Ttype>(Ttype id)
        {
            return await this._dbSet.FindAsync(id);
        }

        public Ttype Max<Ttype>(Expression<Func<T, Ttype>> column)
        {
            if (this._dbSet.AsNoTracking().Any())
            {
                return this._dbSet.AsNoTracking().Max<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public async Task<Ttype> MaxAsync<Ttype>(Expression<Func<T, Ttype>> column)
        {
            if (await this._dbSet.AsNoTracking().AnyAsync())
            {
                return await this._dbSet.AsNoTracking().MaxAsync<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public Ttype Max<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where)
        {
            if (this._dbSet.AsNoTracking().Any(@where))
            {
                return this._dbSet.AsNoTracking().Where(@where).Max<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public async Task<Ttype> MaxAsync<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where)
        {
            if (await this._dbSet.AsNoTracking().AnyAsync(@where))
            {
                return await this._dbSet.AsNoTracking().Where(@where).MaxAsync<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public Ttype Min<Ttype>(Expression<Func<T, Ttype>> column)
        {
            if (this._dbSet.AsNoTracking().Any())
            {
                return this._dbSet.AsNoTracking().Min<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public async Task<Ttype> MinAsync<Ttype>(Expression<Func<T, Ttype>> column)
        {
            if (await this._dbSet.AsNoTracking().AnyAsync())
            {
                return await this._dbSet.AsNoTracking().MinAsync<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public Ttype Min<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where)
        {
            if (this._dbSet.AsNoTracking().Any(@where))
            {
                return this._dbSet.AsNoTracking().Where(@where).Min<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public async Task<Ttype> MinAsync<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where)
        {
            if (await this._dbSet.AsNoTracking().AnyAsync(@where))
            {
                return await this._dbSet.AsNoTracking().Where(@where).MinAsync<T, Ttype>(column);
            }
            return default(Ttype);
        }

        public TType Sum<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> @where) where TType : new()
        {
            object result = 0;

            if (new TType().GetType() == typeof(decimal))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, decimal>>);
            }
            if (new TType().GetType() == typeof(decimal?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, decimal?>>);
            }
            if (new TType().GetType() == typeof(double))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, double>>);
            }
            if (new TType().GetType() == typeof(double?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, double?>>);
            }
            if (new TType().GetType() == typeof(float))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, float>>);
            }
            if (new TType().GetType() == typeof(float?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, float?>>);
            }
            if (new TType().GetType() == typeof(int))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, int>>);
            }
            if (new TType().GetType() == typeof(int?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, int?>>);
            }
            if (new TType().GetType() == typeof(long))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, long>>);
            }
            if (new TType().GetType() == typeof(long?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, long?>>);
            }
            return (TType)result;
        }

        public async Task<TType> SumAsync<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> @where) where TType : new()
        {
            object result = 0;

            if (new TType().GetType() == typeof(decimal))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, decimal>>);
            }
            if (new TType().GetType() == typeof(decimal?))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, decimal?>>);
            }
            if (new TType().GetType() == typeof(double))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, double>>);
            }
            if (new TType().GetType() == typeof(double?))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, double?>>);
            }
            if (new TType().GetType() == typeof(float))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, float>>);
            }
            if (new TType().GetType() == typeof(float?))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, float?>>);
            }
            if (new TType().GetType() == typeof(int))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, int>>);
            }
            if (new TType().GetType() == typeof(int?))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, int?>>);
            }
            if (new TType().GetType() == typeof(long))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, long>>);
            }
            if (new TType().GetType() == typeof(long?))
            {
                result = await this._dbSet.AsNoTracking().Where(where).SumAsync(selector as Expression<Func<T, long?>>);
            }
            return (TType)result;
        }

        public void Dispose()
        {
            if (_dbContext != null)
                this._dbContext.Dispose();
        }

    }
}
