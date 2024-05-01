using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataConnecion.MongoDB
{
    public class MongoDBEntity<T> //where T : class
    {
        private readonly IMongoClient _client = null!;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BsonDocument> _collection;
        //private readonly IConfiguration _configuration; 
        //private readonly ILogger _logger;
        public MongoDBEntity(string mongoDbSrv, string collectionString, string bsonDocString)
        {
            _client = new MongoClient();
            _database = _client.StartSessionAsync().Result.Client.GetDatabase(collectionString);
            _collection = _database.GetCollection<BsonDocument>(bsonDocString);
            //_logger.LogInformation("MongoDBEntity");
        }
        public async void AddMongoDBEntity(List<BsonDocument> bsons)
        {
            await _collection.InsertManyAsync(bsons);
        }
        public async void AddMongoDBEntity(BsonDocument bsons)
        {
            await _collection.InsertOneAsync(bsons);
        }
        public async Task AddMongoDBEntity(T obj)
        {
            BsonDocument document = JsonExt<T>.ToBson(obj);
            if (document != null && !document.Equals(new BsonDocument()))
                await _collection.InsertOneAsync(document);
        }
        public async Task<List<BsonDocument>> FindBsons(Dictionary<string, string> findComponents)
        {
            if (findComponents == null || findComponents.Count == 0)
                return new List<BsonDocument>();
            var filter = new BsonDocument(findComponents);
            List<BsonDocument> documents = await _collection.Find(filter).ToListAsync();
            return documents;
        }


        public async Task<List<T>> FindObjects(Dictionary<string, string> findComponents)
        {
            // bằng null hoặc rỗng thì return không có gì 
            if (findComponents == null || findComponents.Count == 0)
                return new List<T>();
            List<BsonDocument> documents = await this.FindBsons(findComponents);
            List<T> objects = documents.Select(doc => BsonSerializer.Deserialize<T>(doc)).ToList();
            return objects;
        }

        public async Task UpdateObject(Dictionary<string, string> findComponents, string updateField, object updateValue, Type updateValueType)
        {
            List<BsonDocument> documents = await this.FindBsons(findComponents);
            var filter = documents.ElementAt(0);
            object convertedValue;

            // Chuyển đổi updateValue sang kiểu dữ liệu phù hợp
            if (updateValueType.IsGenericType && updateValueType.GetGenericTypeDefinition() == typeof(List<>))
            {
                if (updateValueType.GetGenericArguments()[0] == typeof(string))
                    convertedValue = (updateValue as List<string>)!;
                else if (updateValueType.GetGenericArguments()[0] == typeof(int))
                    convertedValue = (updateValue as List<int>)!;
                else if (updateValueType.GetGenericArguments()[0] == typeof(double))
                    convertedValue = (updateValue as List<double>)!;
                else
                    throw new ArgumentException($"Unsupported list type: {updateValueType.GetGenericArguments()[0]}");
            }
            else
            {
                switch (Type.GetTypeCode(updateValueType))
                {
                    case TypeCode.Int32:
                        convertedValue = Convert.ToInt32(updateValue);
                        break;
                    case TypeCode.Double:
                        convertedValue = Convert.ToDouble(updateValue);
                        break;
                    case TypeCode.String:
                        convertedValue = (string)updateValue;
                        break;
                    default:
                        throw new ArgumentException($"Unsupported update value type: {updateValueType}");
                }
            }

            var update = Builders<BsonDocument>.Update.Set(updateField, convertedValue);
            var options = new UpdateOptions { IsUpsert = true }; // Tạo mới nếu không tìm thấy
            await _collection.UpdateOneAsync(filter, update, options);
        }




        /// <summary>
        /// Tạo bộ lọc cho các Fields 
        /// </summary>
        /// <param name="findComponents"></param>
        /// <param name="isAllKeysNeedContains">Chỉ cần 1 Field chứa 1 thành phần trong list<string></param>
        /// <returns></returns>
        public async Task<List<T>> FindObjects(Dictionary<string, List<string>> findComponents, bool isAllKeysNeedContains = false)
        {
            if (findComponents == null || findComponents.Count == 0)
                return new List<T>();

            var filters = new List<FilterDefinition<BsonDocument>>();
            foreach (var component in findComponents)
            {
                var subFilters = component.Value.Select(value => Builders<BsonDocument>.Filter.Regex(component.Key, new BsonRegularExpression(value, "i")));
                filters.Add(Builders<BsonDocument>.Filter.Or(subFilters));
            }
            FilterDefinition<BsonDocument> filter;
            if (isAllKeysNeedContains)
                filter = Builders<BsonDocument>.Filter.And(filters);
            else
                filter = Builders<BsonDocument>.Filter.Or(filters);
            var documents = await _collection.Find(filter).ToListAsync();
            var objects = documents.Select(doc => BsonSerializer.Deserialize<T>(doc)).ToList();
            return objects;
        }

        public async Task<List<T>> FindObjects(KeyValuePair<string, List<string>> findContainComponents)
        {
            // Tạo một danh sách các bộ lọc, mỗi bộ lọc tương ứng với một giá trị trong findContainComponents.Value
            var filters = findContainComponents.Value.Select(value => Builders<BsonDocument>.Filter.Eq(findContainComponents.Key, value)).ToList();
            // Filter Or cho phép lọc nhiều thằng cùng lúc với các điều kiện có thể đồng thời OK 
            var filter = Builders<BsonDocument>.Filter.Or(filters);
            var documents = await _collection.Find(filter).ToListAsync();
            var objects = documents.Select(doc => BsonSerializer.Deserialize<T>(doc)).ToList();
            return objects;
        }


        // xóa tất cả dữ liệu trong collection
        public async void DeleteAll()
        {
            await _collection.DeleteManyAsync(new BsonDocument());
        }
        public async void Task<DeleteObjects>(Dictionary<string, string> deleteComponents)
        {
            List<BsonDocument> bsonElements = await this.FindBsons(deleteComponents);
            if (bsonElements.Count == 0)
                return;
            _collection.DeleteMany(new BsonDocument(deleteComponents));
        }
        public async void DeleteObject(BsonDocument bson)
        {
            await _collection.DeleteOneAsync(bson);
        }
        public async void DeleteObjects(string key, string value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(key, value);
            await _collection.DeleteManyAsync(filter);
        }
        public async void DeleteObjects(BsonDocument bsons)
        {
            await _collection.DeleteManyAsync(bsons);
        }
        public async void DeleteObject(T obj)
        {
            BsonDocument document = JsonExt<T>.ToBson(obj);
            if (document != null && !document.Equals(new BsonDocument()))
                await _collection.DeleteManyAsync(document);
        }
        public async void DeleteObjects(List<T> objs)
        {
            foreach (var obj in objs)
            {
                BsonDocument document = JsonExt<T>.ToBson(obj);
                if (document != null && !document.Equals(new BsonDocument()))
                    await _collection.DeleteManyAsync(document);
            }
        }
        // xóa luôn collection
        public async void DropCollection()
        {
            await _database.DropCollectionAsync(_collection.CollectionNamespace.CollectionName);
        }
        public async void Index(string fieldName)
        {
            var keys = Builders<BsonDocument>.IndexKeys.Ascending(fieldName);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(keys));
        }
        public async void Indexs(params string[] fieldNames)
        {
            var keys = Builders<BsonDocument>.IndexKeys.Ascending(fieldNames[0]);
            for (int i = 1; i < fieldNames.Length; i++)
                keys = keys.Ascending(fieldNames[i]);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(keys));
        }
        // Sắp xếp lại _collection với khóa được truyền vào 
        public async Task<List<BsonDocument>> SortCollection(string fieldName)
        {
            var sort = Builders<BsonDocument>.Sort.Ascending(fieldName);
            List<BsonDocument> sortedDocuments = await _collection.Find(new BsonDocument()).Sort(sort).ToListAsync();
            return sortedDocuments;
        }

        public async Task<List<BsonDocument>> SortCollection(string[] fieldNames)
        {
            var sort = Builders<BsonDocument>.Sort.Ascending(fieldNames[0]);
            for (int i = 1; i < fieldNames.Length; i++)
                sort = sort.Ascending(fieldNames[i]);
            List<BsonDocument> sortedDocuments = await _collection.Find(new BsonDocument()).Sort(sort).ToListAsync();
            return sortedDocuments;
        }

        public async Task<List<BsonDocument>> SortCopyCollection(string[] fieldNames, bool ascending = true)
        {
            var documents = await _collection.Find(new BsonDocument()).ToListAsync();
            var sortedDocuments = ascending
                ? documents.OrderBy(doc => doc[fieldNames[0]])
                : documents.OrderByDescending(doc => doc[fieldNames[0]]);
            for (int i = 1; i < fieldNames.Length; i++)
            {
                sortedDocuments = ascending
                    ? sortedDocuments.ThenBy(doc => doc[fieldNames[i]])
                    : sortedDocuments.ThenByDescending(doc => doc[fieldNames[i]]);
            }
            return sortedDocuments.ToList();
        }

        public async Task<List<T>> SortCopyCollectionObjects(string[] fieldNames, bool ascending = true)
        {
            var sortedDocuments = await this.SortCopyCollection(fieldNames, ascending);
            return sortedDocuments.Select(doc => BsonSerializer.Deserialize<T>(doc)).ToList();
        }

        public async Task<BsonDocument> GetLastBson(string fieldName)
        {
            var sortDefinition = Builders<BsonDocument>.Sort.Descending(fieldName);
            var lastObject = await _collection.Find(new BsonDocument()).Sort(sortDefinition).Limit(1).FirstOrDefaultAsync();
            return lastObject;
        }
        public async Task<T> GetLastObject(string fieldName)
        {
            var sortDefinition = Builders<BsonDocument>.Sort.Descending(fieldName);
            var lastObject = await _collection.Find(new BsonDocument()).Sort(sortDefinition).Limit(1).FirstOrDefaultAsync();
            return BsonSerializer.Deserialize<T>(lastObject);
        }

    }
}
