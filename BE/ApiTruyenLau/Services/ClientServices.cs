using ApiTruyenLau.Objects.Generics.Items;
using ApiTruyenLau.Services.Interfaces;
using DataConnecion.MongoObjects;
using Newtonsoft.Json;
using MGDBs = DataConnecion.MongoObjects.CommonObjects;
using User = ApiTruyenLau.Objects.Generics.Users;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using ApiTruyenLau.Objects.Converters.Users;
using System.Net;

namespace ApiTruyenLau.Services
{
	public class ClientServices : IClientServices
	{
		private readonly IConfiguration _configuration;
		private MGDBs _DB;
		private readonly Type[] typeColection = new Type[] { typeof(User.Client) };
		public ClientServices(IConfiguration configuration)
		{
			_configuration = configuration;
			this._DB = new MGDBs();
			var mongoDBSettings = _configuration.GetSection("MongoDB").Get<MongoDBSettings>();
			this._DB = this._DB.AddMongoDBSrv(mongoDBSettings?.ConnectionString!).AddMongoDBCollections(this.typeColection);
		}
		public async Task<UserCvt.ClientReadedCvt> GetReadedById(string clientId)
		{
			try
			{
				var findedClientObjs = await _DB.GetMongoDBEntity(typeof(User.Client)).FindObjects(new Dictionary<string, string>()
				{{ nameof(User.Client.Id), clientId}});
				if (findedClientObjs != null && findedClientObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					User.Client? findedClient = findedClientObjs
						.Select(obj => JsonConvert.DeserializeObject<User.Client>(JsonConvert.SerializeObject(obj), settings))
						.ToList().ElementAt(0);
					return findedClient?.ToClientReaded()!;
				}
				throw new Exception($"Không có thằng nào Id là {clientId}");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		public async Task UpdateClientReadedId(string clientId, string boolId)
		{
			try
			{
				var findedClientObjs = await _DB.GetMongoDBEntity(typeof(User.Client)).FindObjects(new Dictionary<string, string>()
				{{ nameof(User.Client.Id), clientId}});
				if (findedClientObjs != null && findedClientObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					User.Client? findedClient = findedClientObjs
						.Select(obj => JsonConvert.DeserializeObject<User.Client>(JsonConvert.SerializeObject(obj), settings))
						.ToList().ElementAt(0);
					if (findedClient != null)
					{
						findedClient.ReadedId = findedClient.ReadedId ?? new List<string>() { };
						if (findedClient.ReadedId != null && !findedClient.ReadedId.Contains(boolId))
							findedClient.ReadedId.Add(boolId);
						else 
							throw new Exception($"Thằng {clientId} đã đọc sách {boolId} rồi.");
						await _DB.GetMongoDBEntity(typeof(User.Client)).UpdateObject(new Dictionary<string, string>()
							{ { nameof(User.Client.Id), clientId}},
							nameof(User.Client.ReadedId),
							(findedClient.ReadedId!),
							typeof(User.Client).GetProperty(nameof(User.Client.ReadedId))!.PropertyType);
						return;
					}
				}
				throw new Exception($"Không có thằng nào Id là {clientId}");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
	}
}
