using MongoDB.Bson;
using Newtonsoft.Json;

namespace DataConnecion.MongoDB
{
	public static class JsonExt<T>
	{
		public static string SerializeObject(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
		public static T? DeserializeObject(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
		public static BsonDocument ToBson(string json)
		{
			var document = BsonDocument.Parse(json);
			return document;
		}
		public static BsonDocument ToBson(T obj)
		{
			return ToBson(SerializeObject(obj));
		}
	}
}
