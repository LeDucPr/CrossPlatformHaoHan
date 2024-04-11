using ApiTruyenLau.Objects.Converters.Items;
using ApiTruyenLau.Objects.Extensions.Items;
using ApiTruyenLau.Services.Interfaces;
using DataConnecion.MongoObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Item = ApiTruyenLau.Objects.Generics.Items;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;
using MGDBs = DataConnecion.MongoObjects.CommonObjects;


namespace ApiTruyenLau.Services
{
	public class BookServices : IBookServices
	{
		private readonly IConfiguration _configuration;
		private MGDBs _DB;
		private readonly Type[] typeColection = new Type[] { typeof(Item.Book) };
		public BookServices(IConfiguration configuration)
		{
			_configuration = configuration;
			this._DB = new MGDBs();
			var mongoDBSettings = _configuration.GetSection("MongoDB").Get<MongoDBSettings>();
			this._DB = this._DB.AddMongoDBSrv(mongoDBSettings?.ConnectionString!).AddMongoDBCollections(this.typeColection);
			this._DB.GetMongoDBEntity(typeof(Item.Book)).Indexs(nameof(Item.Book.Id));
		}

		// mục đích thiết kế phần bìa và intro riêng lẻ liên quan tới một số vấn đề lưu trữ tại máy người dùng (guest)

		#region Phần bìa sách
		/// <summary>
		/// Trả về bìa sách theo Id
		/// </summary>
		/// <param name="bookId"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<ItemCvt.CoverBookCvt> GetCoverById(string bookId)
		{
			try
			{
				var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(new Dictionary<string, string>()
				{
					{ nameof(Item.Book.Id), bookId}
				});
				if (findedBookObjs != null && findedBookObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					Item.Book? findedBook = findedBookObjs
						.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), settings))
						.ToList().ElementAt(0);
					return findedBook?.ToCoverBookCvt()!;
				}
				throw new Exception($"không có quyển nào Id là {bookId}");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		/// <summary>
		/// Mỗi một thông điệp gửi 1 yêu cầu (ví dụ: genre thì chỉ lấy 1 kiểu cố định, không lấy theo nhiều loại cùng lúc) 
		/// </summary>
		/// <param name="amountIntros"></param>
		/// <param name="bookFields"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<List<ItemCvt.CoverBookCvt>> GetCoversByFieldsEquals(int amountCovers, List<string> skipIds, Dictionary<string, string> bookFields)
		{
			try
			{
				// bookFields của Api trả về có thể sẽ liên quan tới vấn đề viết hoa viết thường
				// Chuyển tất cả các key trong bookFields về lowercase và đem đối chiếu với Properties của Book ở dạng lower case 
				var lowerCaseBookFields = bookFields.ToDictionary(entry => entry.Key.ToLower(), entry => entry.Value);
				var bookProperties = typeof(Item.Book).GetProperties().ToDictionary(prop => prop.Name.ToLower(), prop => prop.Name);
				var validBookFields = lowerCaseBookFields
					.Where(entry => bookProperties.ContainsKey(entry.Key))
					.ToDictionary(entry => bookProperties[entry.Key], entry => entry.Value);
				var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(validBookFields);

				if (findedBookObjs != null && findedBookObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					var findedBooks = findedBookObjs
						.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), settings))
						.ToList();
					var filteredBooks = findedBooks.Where(book => !skipIds.Contains(book!.Id)); // Bỏ qua các cuốn sách có Id nằm trong skips
					var resultBooks = filteredBooks.Take(amountCovers).ToList(); // ít hơn thì lấy tất 
					return resultBooks.Select(book => book?.ToCoverBookCvt()).ToList()!; // không còn sách thì trả về lỗi hết sách 
				}
				throw new Exception("Không có sách nào");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		/// <summary>
		/// Tương ựt như trên nhưng thay vì tìm kiếm chính xác thì tìm kiếm chứa trong 
		/// </summary>
		/// <param name="amountCovers"></param>
		/// <param name="skipIds"></param>
		/// <param name="bookFields"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<List<ItemCvt.CoverBookCvt>> GetCoversByFieldsContrains(int amountCovers, List<string> skipIds, Dictionary<string, List<string>> bookFields)
		{
			try
			{
				// bookFields của Api trả về có thể sẽ liên quan tới vấn đề viết hoa viết thường
				// Chuyển tất cả các key trong bookFields về lowercase và đem đối chiếu với Properties của Book ở dạng lower case 
				var lowerCaseBookFields = bookFields.ToDictionary(entry => entry.Key.ToLower(), entry => entry.Value);
				var bookProperties = typeof(Item.Book).GetProperties().ToDictionary(prop => prop.Name.ToLower(), prop => prop.Name);
				var validBookFields = lowerCaseBookFields
					.Where(entry => bookProperties.ContainsKey(entry.Key))
					.ToDictionary(entry => bookProperties[entry.Key], entry => entry.Value);
				var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(validBookFields);

				if (findedBookObjs != null && findedBookObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					var findedBooks = findedBookObjs
						.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), settings))
						.ToList();

					var filteredBooks = findedBooks.Where(book => !skipIds.Contains(book!.Id)); // Bỏ qua các cuốn sách có Id nằm trong skips
					var resultBooks = filteredBooks.Take(amountCovers).ToList(); // ít hơn thì lấy tất 
					return resultBooks.Select(book => book?.ToCoverBookCvt()).ToList()!; // không còn sách thì trả về lỗi hết sách 
				}
				throw new Exception("Không có sách nào");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		#endregion Phần bìa sách 

		#region Phần intro sách
		/// <summary>
		/// Lúc này là hiện ra một số ô trưng bày sách rồi nhấn vô là nó gửi về bookId 
		/// Lúc này thì tìm Id rồi đánh về IntroBookPartCvt thôi 
		/// </summary>
		/// <param name="bookId"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<ItemCvt.IntroBookPartCvt> GetIntroById(string bookId)
		{
			try
			{
				var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(new Dictionary<string, string>()
				{
					{ nameof(Item.Book.Id), bookId}
				});
				if (findedBookObjs != null && findedBookObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					Item.Book? findedBook = findedBookObjs
						.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), settings))
						.ToList().ElementAt(0);
					if (findedBook != null)
						Console.WriteLine(findedBook.CoverLink);
					return findedBook?.ToIntroBookPartCvt()!;
				}
				throw new Exception($"không có quyển nào Id là {bookId}");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		/// <summary>
		/// Theo lsy thuyết thì nó phân tích hành vi người dùng và trả về một số bản intro tương ứng với sở thích 
		/// {Còn hiện tại mấy bố làm Data lâu VL nên thôi cái này tạm thời bỏ qua}
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<ItemCvt.IntroBookPartCvt> GetIntros(string userId) /////////////////////////////// データをすばやく作成します。
		{
			try
			{
				// Đoạn trên đây lấy Data từ hàm phân tích hành vi các thứ rồi trả về thể loại hay gì đó 
				// Nếu nhiều hơn một thể loại thì chia thành nhiều Dict hoặc dùng 

				var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(
					new KeyValuePair<string, List<string>>(
						nameof(Item.Book.Genre),
						new List<string>() { "Truyện tranh", "Truyện tranh" }
					)
				);
				throw new Exception("Chưa làm xong");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		#endregion Phần intro sách

		#region Nội dung sách 
		/// <summary>
		/// Cái này sẽ lấy toàn bộ nội dung sách nên cần cân nhắc trước khi dùng 
		/// Hợp lý hơn cho việc sử dụng với truyện chữ 
		/// </summary>
		/// <param name="bookId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		/// <exception cref="Exception"></exception>
		public async Task<ItemCvt.BookCvt> GetBookById(string bookId)
		{
			try
			{
				var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(new Dictionary<string, string>()
				{
					{ nameof(Item.Book.Id), bookId}
				});
				if (findedBookObjs != null && findedBookObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					Item.Book? findedBook = findedBookObjs
						.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), settings))
						.ToList().ElementAt(0);
					return findedBook?.ToBookCvt()!;
				}
				throw new NotImplementedException();
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		/// <summary>
		/// Lấy ảnh tiếp theo của nội dung sách 
		/// Giả sử đọc tới ảnh thứ 5 và cần lấy ảnh 6 và 7 thì skipImages = 5, takeImages = 2
		/// </summary>
		/// <param name="bookId"></param>
		/// <param name="skipImages"></param>
		/// <param name="takeImages"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		/// <exception cref="Exception"></exception>
		public async Task<List<string>> GetNextImagesForContent(string bookId, int skipImages, int takeImages)
		{
			try
			{
				var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(new Dictionary<string, string>()
				{
					{ nameof(Item.Book.Id), bookId}
				});
				if (findedBookObjs != null && findedBookObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					Item.Book? findedBook = findedBookObjs
						.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), settings))
						.ToList().ElementAt(0);
					return findedBook?.GetImageAtElementsStringBase64Png(skipImages, takeImages, percentSize: 45, quality: 80)!;
				}
				throw new NotImplementedException("Hết ảnh rồi");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		#endregion Nội dung sách 

		#region Phần tạo sách
		/// <summary>
		/// Tạo mới sách bằng Json 
		/// </summary>
		/// <param name="bookCreaterCvts"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<bool> CreateBooks(List<ItemCvt.BookCreaterCvt> bookCreaterCvts)
		{
			try
			{
				/*IEnumerable<Item.Book> books = */
				bookCreaterCvts.Select(bookCreaterCvt =>
				{
					return bookCreaterCvt.ToBookCreaterCvt();
				})
				.AsParallel()
				.ForAll(async book =>
					await _DB.GetMongoDBEntity(typeof(Item.Book)).AddMongoDBEntity(book)
				);
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		#endregion Phần tạo sách

		#region Phần người đọc
		/// <summary>
		/// Update số lượng lượt đọc cho truyện này (dù là người cũ hay người mới)
		/// </summary>
		/// <param name="bookId"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task UpdateBookReader(string bookId)
		{
			try
			{
				var findedBookObjs = await _DB.GetMongoDBEntity(typeof(Item.Book)).FindObjects(new Dictionary<string, string>()
				{
					{ nameof(Item.Book.Id), bookId}
				});
				if (findedBookObjs != null && findedBookObjs.Count > 0)
				{
					var settings = new JsonSerializerSettings
					{
						MissingMemberHandling = MissingMemberHandling.Ignore,
						SerializationBinder = new MySerializationBinderBook()
					};
					Item.Book? findedBook = findedBookObjs
						.Select(obj => JsonConvert.DeserializeObject<Item.Book>(JsonConvert.SerializeObject(obj), settings))
						.ToList().ElementAt(0);

					if (findedBook != null)
					{
						// Lưu lại đối tượng findedBook đã được cập nhật vào MongoDB
						await _DB.GetMongoDBEntity(typeof(Item.Book)).UpdateObject(new Dictionary<string, string>()
							{ { nameof(Item.Book.Id), bookId}},
							nameof(Item.Book.Reader),
							(findedBook.Reader + 1),
							typeof(Item.Book).GetProperty(nameof(Item.Book.Reader))!.PropertyType);
					}
				}
				throw new Exception($"không có quyển nào Id là {bookId}");
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		#endregion Phần người đọc

	}

	public class MySerializationBinderBook : ISerializationBinder
	{
		public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null!;
			typeName = serializedType.Name;
		}

		public Type BindToType(string assemblyName, string typeName)
		{
			return Type.GetType(typeName);
		}
	}
}
