using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.MicroServer.Repository.Mongo
{
    /// <summary>
    /// mongoDB服务
    /// </summary>
    public interface IMongoService
    {
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="mongoTable"></param>
        /// <returns></returns>
        int Add<T>(T t, string mongoTable = null);
        /// <summary>
        /// 异步-新增记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="mongoTable"></param>
        /// <returns></returns>
        Task<int> AddAsync<T>(T t, string mongoTable = null);
        /// <summary>
        /// 根据过滤条件获取数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="mongoTable"></param>
        /// <returns></returns>
        long Count<T>(FilterDefinition<T> filter, string mongoTable = null);
        /// <summary>
        /// 异步-根据过滤条件获取数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="mongoTable"></param>
        /// <returns></returns>
        Task<long> CountAsync<T>(FilterDefinition<T> filter, string mongoTable = null);
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="field"></param>
        /// <param name="sort"></param>
        /// <param name="mongoTable"></param>
        /// <returns></returns>
        List<T> FindListByPage<T>(FilterDefinition<T> filter, int pageIndex, int pageSize, string[] field = null, SortDefinition<T> sort = null, string mongoTable = null);
        /// <summary>
        /// 异步-分页获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="field"></param>
        /// <param name="sort"></param>
        /// <param name="mongoTable"></param>
        /// <returns></returns>
        Task<List<T>> FindListByPageAsync<T>(FilterDefinition<T> filter, int pageIndex, int pageSize, string[] field = null, SortDefinition<T> sort = null, string mongoTable = null);
    }
}
