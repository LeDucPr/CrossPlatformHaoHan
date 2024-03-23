using DataConnecion.MongoDB;

namespace DataConnecion.MongoObjects
{
	/// <summary>
	/// Triển khai một lớp được impliment từ các interface khác nhau nhưng có điểm chung
	/// hoặc các class được kê thừa từ các class khác nhưng đều có điểm chung 
	/// Điều này thích hợp cho việc phân quyền chức năng cho các class khác nhau 
	/// về lối sử dụng nhưng có vài điểm chung trong thiết kế 
	/// </summary>
	public class CommonObjects
	{
		public string MongoDBSrv { get; set; } = null!;
		public Dictionary<Type, string> MongoDBColections { get; set; } // chứa type và DocumentName
		public Dictionary<Type, MongoDBEntity<object>> mongoDBEntities { get; set; }// chứa type và MongoDBEntity
		public CommonObjects()
		{
			this.MongoDBColections = new Dictionary<Type, string>();
			this.mongoDBEntities = new Dictionary<Type, MongoDBEntity<object>>();
		}
	}
	public static class CommonObjectExtension
	{
		// điều hướng server 
		public static CommonObjects AddMongoDBSrv(this CommonObjects commonObj, string mongoDBSrv)
		{
			commonObj.MongoDBSrv = mongoDBSrv;
			return commonObj;
		}
		/// <summary>
		/// điều hướng collection
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="commonObj"></param>
		/// <param name="type"></param>
		/// <param name="collectionName"></param>
		public static CommonObjects AddMongoDBCollection(this CommonObjects commonObj, Type type, string collectionName)
		{
			commonObj.MongoDBColections.Add(type, collectionName);
			return commonObj;
		}
		public static CommonObjects AddMongooDBCollection(this CommonObjects commonObj, Type type)
		{
			commonObj.MongoDBColections.Add(type, type.Name);
			return commonObj;
		}
		public static CommonObjects AddMongoDBCollections(this CommonObjects commonObj, params Type[] types)
		{
			types.ToList().ForEach(type =>
			{
				commonObj.MongoDBColections.Add(type, type.Name);
				MongoDBEntity<object> mongo = new MongoDBEntity<object>(commonObj.MongoDBSrv, commonObj.MongoDBColections[type], type.Name);
				commonObj.mongoDBEntities.Add(type, mongo);
			});
			return commonObj;
		}
		public static MongoDBEntity<object> GetMongoDBEntity(this CommonObjects commonObj, Type type)
		{
			return commonObj.mongoDBEntities[type];
		}
		public static string GetMongoDBCollection(this CommonObjects commonObj, Type type)
		{
			return commonObj.MongoDBColections[type];
		}
	}
}
