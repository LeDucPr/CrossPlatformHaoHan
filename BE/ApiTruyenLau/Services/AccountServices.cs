using DataConnecion.MongoObjects;
using ApiTruyenLau.Services.Interfaces;
using Item = ApiTruyenLau.Objects.Generics.Items;
using MGDBs = DataConnecion.MongoObjects.CommonObjects;
using User = ApiTruyenLau.Objects.Generics.Users;
using ApiTruyenLau.Objects.Converters.Users;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using System.Net.WebSockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiTruyenLau.Services
{
	public class AccountServices : IAccountServices
	{
		private readonly IConfiguration _configuration;
		private MGDBs _DB;
		private readonly Type[] typeColection = new Type[] { typeof(User.Client) };
		public AccountServices(IConfiguration configuration)
		{
			_configuration = configuration;
			this._DB = new MGDBs();
			var mongoDBSettings = _configuration.GetSection("MongoDB").Get<MongoDBSettings>();
			this._DB = this._DB.AddMongoDBSrv(mongoDBSettings?.ConnectionString!).AddMongoDBCollections(this.typeColection);
			this._DB.GetMongoDBEntity(typeof(User.Client)).Indexs(nameof(User.Client.Id), nameof(User.Client.Account));
		}
		public async Task<bool> SignUpClient(UserCvt.ClientInfoCvt clientInfoCvt)
		{
			try
			{
				clientInfoCvt.Id = await this.NextClientId();
				if (clientInfoCvt.IsValid()) { }
				User.Client newClient = clientInfoCvt.ToClientAccount();
				newClient.Account.CreateDate = DateTime.Now;
				newClient.Account.LastLoginDate = DateTime.Now;
				await _DB.GetMongoDBEntity(typeof(User.Client)).AddMongoDBEntity(newClient);
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		public async Task<User.Client> SignInClient(UserCvt.ClientInfoCvt clientInfoCvt)
		{
			try
			{
				if (clientInfoCvt.IsValid(clientInfoCvt.UserNameAccount.GetType(), clientInfoCvt.PasswordAccount.GetType())) { }
				User.Client needFindClient = clientInfoCvt.ToClientAccount();
				var findedClientObjs = await _DB.GetMongoDBEntity(typeof(User.Client)).FindObjects(new Dictionary<string, string>()
				{
					{ $"{nameof(User.Client.Account)}.{nameof(User.Client.Account.UserName)}", needFindClient.Account.UserName},
					{ $"{nameof(User.Client.Account)}.{nameof(User.Client.Account.Password)}", needFindClient.Account.Password}
				});
				if (findedClientObjs != null && findedClientObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderAccount()
					};
					User.Client? findedClient = findedClientObjs
						.Select(obj => JsonConvert.DeserializeObject<User.Client>(JsonConvert.SerializeObject(obj), settings))
						.ToList().ElementAt(0);
					findedClient!.Account.LastLoginDate = DateTime.Now;
					return findedClient!;
				}
				throw new Exception("Client not found");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public async Task<string> NextClientId(int defaultAmountNumber = 5)
		{
			try
			{
				var lastClientObj = await this._DB.GetMongoDBEntity(typeof(User.Client)).GetLastObject(nameof(User.Client.Id));
				if (lastClientObj != null)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderAccount()
					};
					User.Client lastClient = JsonConvert.DeserializeObject<User.Client>(JsonConvert.SerializeObject(lastClientObj), settings)!;
					// Chuyển đổi Id thành số, tăng nó lên 1, sau đó chuyển lại thành chuỗi và thêm số 0 vào phía trước nếu cần
					int lastClientId = int.Parse(lastClient.Id);
					string nextClientId = (lastClientId + 1).ToString().PadLeft(defaultAmountNumber, '0');
					return nextClientId;
				}
				return "1".PadLeft(defaultAmountNumber, '0');
			}
			catch
			{
				// Trả về "00001" nếu không có client nào nếu defaultAmountNumber = 5
				return "1".PadLeft(defaultAmountNumber, '0');
			}
		}
	}


	public class MySerializationBinderAccount : ISerializationBinder
	{
		public Type BindToType(string? assemblyName, string typeName)
		{
			Console.WriteLine($"{assemblyName}    {typeName}");
			if (typeName == typeof(ApiTruyenLau.Objects.Interfaces.Users.IAccount).ToString())
			{
				return typeof(ApiTruyenLau.Objects.Generics.Users.Account); // Replace with your concrete Account class
			}
			return Type.GetType(typeName)!;
		}

		public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null!;
			typeName = null!;
		}
	}
}
