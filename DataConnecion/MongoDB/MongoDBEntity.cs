using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

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
		public void AddMongoDBEntity(List<BsonDocument> bsons)
		{
			_collection.InsertMany(bsons);
		}
		public void AddMongoDBEntity(BsonDocument bsons)
		{
			_collection.InsertOne(bsons);
		}
		public void AddMongoDBEntity(T obj)
		{
			BsonDocument document = JsonExt<T>.ToBson(obj);
			if (document != null && !document.Equals(new BsonDocument()))
				_collection.InsertOne(document);
		}
		public List<BsonDocument> FindBsons(Dictionary<string, string> findComponents)
		{
			if (findComponents == null || findComponents.Count == 0)
				return new List<BsonDocument>();
			var filter = new BsonDocument(findComponents);
			List<BsonDocument> documents = _collection.Find(filter).ToList();
			return documents;
		}
		public List<T> FindObjects(Dictionary<string, string> findComponents)
		{
			// bằng null hoặc rỗng thì return không có gì 
			if (findComponents == null || findComponents.Count == 0)
				return new List<T>();
			List<BsonDocument> documents = this.FindBsons(findComponents);
			List<T> objects = documents.Select(doc => BsonSerializer.Deserialize<T>(doc)).ToList();
			return objects;
		}

		// xóa tất cả dữ liệu trong collection
		public void DeleteAll()
		{
			_collection.DeleteMany(new BsonDocument());
		}
		public void DeleteObjects(Dictionary<string, string> deleteComponents)
		{
			List<BsonDocument> bsonElements = this.FindBsons(deleteComponents);
			if (bsonElements.Count == 0)
				return;
			_collection.DeleteMany(new BsonDocument(deleteComponents));
		}
		public void DeleteObject(BsonDocument bson)
		{
			_collection.DeleteOne(bson);
		}
		public void DeleteObjects(string key, string value)
		{
			var filter = Builders<BsonDocument>.Filter.Eq(key, value);
			_collection.DeleteMany(filter);
		}
		public void DeleteObjects(BsonDocument bsons)
		{
			_collection.DeleteMany(bsons);
		}
		public void DeleteObject(T obj)
		{
			BsonDocument document = JsonExt<T>.ToBson(obj);
			if (document != null && !document.Equals(new BsonDocument()))
				_collection.DeleteOne(document);
		}
		public void DeleteObjects(List<T> objs)
		{
			foreach (var obj in objs)
			{
				BsonDocument document = JsonExt<T>.ToBson(obj);
				if (document != null && !document.Equals(new BsonDocument()))
					_collection.DeleteOne(document);
			}
		}
		// xóa luôn collection
		public void DropCollection()
		{
			_database.DropCollection(_collection.CollectionNamespace.CollectionName);
		}
		public void Index(string fieldName)
		{
			var keys = Builders<BsonDocument>.IndexKeys.Ascending(fieldName);
			_collection.Indexes.CreateOne(new CreateIndexModel<BsonDocument>(keys));
		}
		public void Indexs(params string[] fieldNames)
		{
			var keys = Builders<BsonDocument>.IndexKeys.Ascending(fieldNames[0]);
			for (int i = 1; i < fieldNames.Length; i++)
				keys = keys.Ascending(fieldNames[i]);
			_collection.Indexes.CreateOne(new CreateIndexModel<BsonDocument>(keys));
		}
		// Sắp xếp lại _collection với khóa được truyền vào 
		public void SortCollection(string fieldName)
		{
			var sort = Builders<BsonDocument>.Sort.Ascending(fieldName);
			_collection.Find(new BsonDocument()).Sort(sort);
		}
		public void SortCollection(string[] fieldNames)
		{
			var sort = Builders<BsonDocument>.Sort.Ascending(fieldNames[0]);
			for (int i = 1; i < fieldNames.Length; i++)
				sort = sort.Ascending(fieldNames[i]);
			_collection.Find(new BsonDocument()).Sort(sort);
		}
	}
}
