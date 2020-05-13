using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.MicroServer.Repository.Mongo
{
    public class MongoService: IMongoService
    {
        private readonly MongoClient _mongoClient;

        private IMongoDatabase _mongoDatabase;

        private string _mongoTable;

        public MongoService(IConfiguration configuration)
        {
            _mongoClient = new MongoClient(configuration["MongoDB.DefaultConnection"]);
            _mongoDatabase = _mongoClient.GetDatabase(configuration["MongoDB.DefaultDatabase"]);
            _mongoTable = configuration["MongoDB.DefaultTable"];
        }

        public int Add<T>(T t,string mongoTable = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(mongoTable))
                    _mongoTable = mongoTable;

                var _tb = _mongoDatabase.GetCollection<T>(_mongoTable).WithReadPreference(ReadPreference.PrimaryPreferred);
                _tb.InsertOne(t);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddAsync<T>(T t, string mongoTable = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(mongoTable))
                    _mongoTable = mongoTable;

                var _tb = _mongoDatabase.GetCollection<T>(_mongoTable).WithReadPreference(ReadPreference.PrimaryPreferred);

                await _tb.InsertOneAsync(t);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public long Count<T>(FilterDefinition<T> filter, string mongoTable = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(mongoTable))
                    _mongoTable = mongoTable;

                var _tb = _mongoDatabase.GetCollection<T>(_mongoTable).WithReadPreference(ReadPreference.PrimaryPreferred);
                return _tb.CountDocuments(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> CountAsync<T>(FilterDefinition<T> filter, string mongoTable = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(mongoTable))
                    _mongoTable = mongoTable;

                var _tb = _mongoDatabase.GetCollection<T>(_mongoTable).WithReadPreference(ReadPreference.PrimaryPreferred);
                return await _tb.CountDocumentsAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> FindListByPage<T>(FilterDefinition<T> filter, int pageIndex=1, int pageSize=20, string[] field = null, SortDefinition<T> sort = null, string mongoTable = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(mongoTable))
                    _mongoTable = mongoTable;

                var _tb = _mongoDatabase.GetCollection<T>(_mongoTable).WithReadPreference(ReadPreference.PrimaryPreferred);

                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return _tb.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                    //进行排序
                    return _tb.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();

                //不排序
                if (sort == null) return _tb.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();

                //排序查询
                return _tb.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<T>> FindListByPageAsync<T>(FilterDefinition<T> filter, int pageIndex=1, int pageSize=20, string[] field = null, SortDefinition<T> sort = null, string mongoTable = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(mongoTable))
                    _mongoTable = mongoTable;

                var _tb = _mongoDatabase.GetCollection<T>(_mongoTable).WithReadPreference(ReadPreference.PrimaryPreferred);

                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null) return await _tb.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                    //进行排序
                    return await _tb.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();

                //不排序
                if (sort == null) return await _tb.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

                //排序查询
                return await _tb.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
