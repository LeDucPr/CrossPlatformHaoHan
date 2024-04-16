using ApiTruyenLau.Objects.Generics.Items;
using ApiTruyenLau.Services.Interfaces;
using DataConnecion.MongoObjects;
using Newtonsoft.Json;
using MGDBs = DataConnecion.MongoObjects.CommonObjects;
using User = ApiTruyenLau.Objects.Generics.Users;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using Item = ApiTruyenLau.Objects.Generics.Items;
using ApiTruyenLau.Objects.Converters.Users;
using System.Net;
using ApiTruyenLau.Objects.Generics.Users;

namespace ApiTruyenLau.Services
{
	public class ClientServices : IClientServices
	{
		private readonly IConfiguration _configuration;
		private MGDBs _DB;
		private readonly Type[] typeColection = new Type[] { typeof(User.Client), typeof(Item.Book) };
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
		/// <summary>
		/// Tự động cập nhật gợi ý cho sách khi người dùng gọi vào hàm này 
		/// Sự kiện này thường xảy ra khi người dùng chọn đọc một cuốn sách
		/// </summary>
		/// <param name="clientId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task UpdateSuggestions(string clientId, int suggestAmount = 5)
		{
			try
			{
				var findedClientObjs = await _DB.GetMongoDBEntity(typeof(Client)).FindObjects(new Dictionary<string, string>()
				{
					{ nameof(Client.Id), clientId}
				});
				if (findedClientObjs != null && findedClientObjs.Count > 0)
				{
					var ClientSettings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderAccount()
					};
					User.Client? findedClient = findedClientObjs
						.Select(obj => JsonConvert.DeserializeObject<User.Client>(JsonConvert.SerializeObject(obj), ClientSettings))
						.ToList().ElementAt(0);

					try
					{
						// bookFields của Api trả về có thể sẽ liên quan tới vấn đề viết hoa viết thường
						// Chuyển tất cả các key trong bookFields về lowercase và đem đối chiếu với Properties của Book ở dạng lower case 
						var bookFieldIds = new Dictionary<string, List<string>>()
						{
							{ nameof(Item.Book.Id), findedClient!.ReadedId} // service này được gọi sau khi người dùng nhấn vào trong sách, vì thế chắc chắn tồn tại
						};
						var lowerCaseBookFields = bookFieldIds.ToDictionary(entry => entry.Key.ToLower(), entry => entry.Value);
						var bookProperties = typeof(Item.Book).GetProperties().ToDictionary(prop => prop.Name.ToLower(), prop => prop.Name);
						var validBookFields = lowerCaseBookFields
							.Where(entry => bookProperties.ContainsKey(entry.Key))
							.ToDictionary(entry => bookProperties[entry.Key], entry => entry.Value);
						var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(validBookFields);

						if (findedBookObjs != null && findedBookObjs.Count > 0)
						{
							var BookSettings = new JsonSerializerSettings
							{
								MissingMemberHandling = MissingMemberHandling.Ignore,
								SerializationBinder = new MySerializationBinderBook()
							};
							var findedBooks = findedBookObjs
								.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), BookSettings))
								.ToList();

							// từ những cuốn sách lấy được ở trên (findedBooks) hãy lấy ra những cuốn tên tác giả từ các cuốn sách đó và thể loại từ những cuốn sách đó
							var authors = findedBooks.Select(book => book?.Author).ToList();
							List<string> uniqueAuthors = authors.Distinct().ToList()!;
							var genres = findedBooks.Select(book => book?.Genre).ToList();
							var genreList = genres.SelectMany(genre => genre!.Split(',')).ToList();
							List<string> uniqueGenres = genreList.Distinct().ToList();

							var bookFieldsSuggests = new Dictionary<string, List<string>>()
							{
								{ nameof(Item.Book.Author), uniqueAuthors},
								{ nameof(Item.Book.Genre), uniqueGenres}
							};

							var findedSuggestBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(bookFieldsSuggests, false);
							var findedSuggestBooks = findedSuggestBookObjs
								.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), BookSettings))
								.ToList();
							var filteredBooks = findedSuggestBooks.Take(suggestAmount).Where(book => !findedClient.ReadedId.Contains(book!.Id)); // Bỏ qua các cuốn sách có Id nằm trong skips
							findedClient.SuggestedId = findedClient.SuggestedId ?? new List<string>();
							findedClient.SuggestedId.InsertRange(0, filteredBooks.Select(x => x!.Id).ToList() ?? new List<string>());
							//findedClient.SuggestedId.AddRange(filteredBooks.Select(x => x!.Id).ToList() ?? new List<string>());
							await _DB.GetMongoDBEntity(typeof(User.Client)).UpdateObject(
								new Dictionary<string, string>() { { nameof(User.Client.Id), clientId } },
								nameof(User.Client.SuggestedId),
								(findedClient.SuggestedId!),
								typeof(User.Client).GetProperty(nameof(User.Client.SuggestedId))!.PropertyType);
						}
						throw new Exception("Không có sách nào");
					}
					catch (Exception ex) { throw new Exception(ex.Message); }
				}
				throw new Exception($"Không có thằng nào Id là{clientId}");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
	}
}
