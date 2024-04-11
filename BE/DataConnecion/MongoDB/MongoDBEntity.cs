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

        public async Task UpdateObject(Dictionary<string, string> findComponents, object updatedObject)
        {
            var filter = new BsonDocument(findComponents);

            // Chuyển đổi đối tượng cập nhật thành BsonDocument
            var updatedDocument = BsonDocument.Parse(JsonConvert.SerializeObject(updatedObject));

            // Thực hiện câu truy vấn cập nhật bằng cách thay thế tài liệu cũ bằng tài liệu mới
            await _collection.ReplaceOneAsync(filter, updatedDocument);
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
				var subFilters = component.Value.Select(value => Builders<BsonDocument>.Filter.Regex(component.Key, new BsonRegularExpression(value)));
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
	}
}
