using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.MicroServer.Repository
{
    public interface IRepository<T,Context> : IDisposable 
        where T : class
        where Context:DbContext
    {
        /// <summary>
        /// 显式开启数据上下文事务
        /// </summary>
        /// <param name="isolationLevel">指定连接的事务锁定行为</param>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        /// <summary>
        /// 提交事务的更改
        /// </summary>
        void Commit();

        /// <summary>
        /// 显式回滚事务，仅在显式开启事务后有用
        /// </summary>
        void Rollback();

        /// <summary>
        /// 提交当前单元操作的更改
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="TrackEntities"/>数据集
        /// </summary>
        IQueryable<T> Entities { get; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="Entities"/>数据集
        /// </summary>
        IQueryable<T> TrackEntities { get; }

        /// <summary>
        /// 插入 - 通过实体对象添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        /// /// <returns></returns>
        T Add(T entity, bool isSave = true);

        /// <summary>
        /// 异步插入 - 通过实体对象添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        Task<T> AddAsync(T entity, bool isSave = true);

        /// <summary>
        /// 批量插入 - 通过实体对象集合添加
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave">是否执行</param>
        void AddRange(IEnumerable<T> entitys, bool isSave = true);

        /// <summary>
        /// 异步批量插入 - 通过实体对象集合添加
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave">是否执行</param>
        Task AddRangeAsync(IEnumerable<T> entitys, bool isSave = true);

        /// <summary>
        /// 删除 - 通过实体对象删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        void Delete(T entity, bool isSave = true);

        /// <summary>
        /// 异步删除 - 通过实体对象删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        Task DeleteAsync(T entity, bool isSave = true);

        /// <summary>
        /// 批量删除 - 通过实体对象集合删除
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave">是否执行</param>
        void Delete(bool isSave = false, params T[] entitys);

        /// <summary>
        /// 异步批量删除 - 通过实体对象集合删除
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave">是否执行</param>
        Task DeleteAsync(bool isSave = false, params T[] entitys);

        /// <summary>
        /// 删除 - 通过主键ID删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="isSave">是否执行</param>
        void Delete(object id, bool isSave = true);

        /// <summary>
        /// 异步删除 - 通过主键ID删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="isSave">是否执行</param>
        Task DeleteAsync(object id, bool isSave = true);

        /// <summary>
        /// 批量删除 - 通过条件删除
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <param name="isSave">是否执行</param>
        void Delete(Expression<Func<T, bool>> @where, bool isSave = true);

        /// <summary>
        /// 异步批量删除 - 通过条件删除
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <param name="isSave">是否执行</param>
        Task DeleteAsync(Expression<Func<T, bool>> @where, bool isSave = true);

        /// <summary>
        /// 修改 - 通过实体对象修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave"></param>
        void Update(T entity, bool isSave = true);

        /// <summary>
        /// 异步修改 - 通过实体对象修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave"></param>
        Task UpdateAsync(T entity, bool isSave = true);

        /// <summary>
        /// 批量修改 - 通过实体对象集合修改
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave"></param>
        void Update(bool isSave = true, params T[] entitys);

        /// <summary>
        /// 异步批量修改 - 通过实体对象集合修改
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave"></param>
        Task UpdateAsync(bool isSave = true, params T[] entitys);

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 异步是否满足条件
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回总条数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 异步返回总条数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 返回总条数 - 通过条件过滤
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 异步返回总条数 - 通过条件过滤
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回第一条记录
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 异步返回第一条记录
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回第一条记录 - 通过条件过滤
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        T FirstOrDefault<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 异步返回第一条记录 - 通过条件过滤
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        IQueryable<T> Distinct(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        IQueryable<T> Where(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 条件查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>
        IEnumerable<T> Where<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc = false);

        /// <summary>
        /// 条件分页查询 - 支持排序 - 支持Select导航属性查询
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>
        IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, out int count, bool isDesc = false);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 获取所有数据 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        IQueryable<T> GetAll<TOrder>(Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T GetById<Ttype>(Ttype id);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<T> GetByIdAsync<Ttype>(Ttype id);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <returns></returns>
        Ttype Max<Ttype>(Expression<Func<T, Ttype>> column);

        /// <summary>
        /// 异步获取最大值
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <returns></returns>
        Task<Ttype> MaxAsync<Ttype>(Expression<Func<T, Ttype>> column);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Ttype Max<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 异步获取最大值
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<Ttype> MaxAsync<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <returns></returns>
        Ttype Min<Ttype>(Expression<Func<T, Ttype>> column);

        /// <summary>
        /// 异步获取最小值
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <returns></returns>
        Task<Ttype> MinAsync<Ttype>(Expression<Func<T, Ttype>> column);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Ttype Min<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 异步获取最小值
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<Ttype> MinAsync<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="selector">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        TType Sum<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> @where) where TType : new();

        /// <summary>
        /// 异步获取总数
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="selector">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<TType> SumAsync<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> @where) where TType : new();
    }
}
