using ApiTruyenLau.Objects.Interfaces.Items;
using SharpCompress.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ApiTruyenLau.Objects.Generics.Items
{
	public enum EBookType
	{
		Comic, // truyện tranh 
		Story, // truyện chữ 
	}

	public partial class Book : IBook
	{
		private string _id = null!;
		// tham gia 
		private string _author = null!;
		private string _publisher = null!;
		private string _translator = null!; // tùy ngôn ngữ
		private DateTime _publishDate;
		// thông tin sách
		private string _title = null!;
		private int _part;
		private int _totalPart;
		private string _description = null!;
		private string? _edition = null!;
		private string? _version = null!;
		private string _genre = null!;
		private string? _isbn;
		private string _language = null!;
		private int _page;
		private string? _format = null!;
		private double _price; // cái này chắc đề cập thôi, mọi người đọc thì không tính 
		private Rectangle? _size; // kích thước (x, y, w, h)
		private string? _storyContent; // nội dung bên trong sách
		private List<byte[]>? _comicContent; // nội dung truyện tranh

		// --> Tất nhiên nếu là Comic thì _storyContent = null và ngược lại
		// --> Nội dung được lưu vào trong DataBase dưới dạng byte[]
		// --> Nhưng Intro bìa sách thì được lưu dưới dạng file ảnh thuần và được truy xuất bởi đường dẫn

		// đánh giá 
		private string? _rating = null!;
		private int _reader;
		private Dictionary<string, string>? _viewers; // người xem (userName, đánh giá)

		// tài nguyên bìa
		private List<byte[]>? _coverImages = null!; // ảnh bìa sách (chắc là nhiều hơn 1 ảnh)

		// số bản lưu trữ trên database
		private int _numberOfCopies;
		private List<string>? _dataStoragePaths;
		// Func ...
		private EBookType _bookType = EBookType.Story; // để mặc định là truyện chữ


		/////// Properties 
		public string Id { get { return _id; } set { _id = value; } } // id truyện

		// tham gia 
		public string Author { get { return _author; } set { _author = value; } } // tác giả
		public string Publisher { get { return _publisher; } set { _publisher = value; } } // nhà xuất bản
		public string Translator { get { return _translator; } set { _translator = value; } } // người dịch
		public DateTime PublishDate { get { return _publishDate; } set { _publishDate = value; } } // ngày xuất bản
		public string Title { get { return _title; } set { _title = value; } } // tên truyện
		public int Part { get { return _part; } set { _part = value; } } // số phần của cuốn hiện tại
		public int TotalPart { get { return _totalPart; } set { _totalPart = value; } } // tổng số phần
		public string Description { get { return _description; } set { _description = value; } } // mô tả
		public string? Edition { get { return _edition; } set { _edition = value; } } // phiên bản
		public string? Version { get { return _version; } set { _version = value; } } // phiên bản
		public string Genre { get { return _genre; } set { _genre = value; } } // thể loại
		public string? ISBN { get { return _isbn; } set { _isbn = value; } } // mã số ISBN
		public string Language { get { return _language; } set { _language = value; } } // ngôn ngữ
		public int Page { get { return _page; } set { _page = value; } } // số trang
		public string? Format { get { return _format; } set { _format = value; } } // định dạng
		public double Price { get { return _price; } set { _price = value; } } // giá
		public Rectangle? Size { get { return _size; } set { _size = value; } } // kích thước

		// tài nguyên nội dung truyện 
		public string? StoryContent { get { return _storyContent; } set { _storyContent = value; } } // nội dung truyện
		public List<byte[]>? ComicContent { get { return _comicContent; } set { _comicContent = value; } } // nội dung truyện tranh

		// đánh giá
		public string? Rating { get { return _rating; } set { _rating = value; } } // đánh giá
		public int Reader { get { return _reader; } set { _reader = value; } } // số người đọc
		public Dictionary<string, string>? Viewers { get { return _viewers; } set { _viewers = value; } } // người xem

		// tài nguyên bìa
		public List<byte[]>? CoverImages { get { return _coverImages; } set { _coverImages = value; } } // ảnh bìa sách

		// số bản lưu trữ trên database 
		public int NumberOfCopies { get { return _numberOfCopies; } set { _numberOfCopies = value; } } // số bản lưu trữ
		public List<string>? DataStoragePaths { get { return _dataStoragePaths; } set { _dataStoragePaths = value; } } // đường dẫn lưu trữ

		// Function ...
		public EBookType BookType { get => this._bookType; set => this._bookType = value; } // để mặc định là truyện chữ
	}

	public static class BookExtension
	{
		#region Xuất đầu ra 
		// intro sách là một mẩu đầu của Content (dù là comic hay là story) 
		// thế nên them một field để xác định intro là không nên 

		/// <summary>
		/// Ở đây sẽ có 2 kiểu, 1 là intro về text (string) cho story, 
		/// 2 là intro về ảnh (List<byte[]></byte>) cho comic
		/// </summary>
		/// <param name="book"></param>
		/// <returns></returns>
		private static (Type, object)? GetIntro(this Book book)
		{
			return book.BookType switch
			{
				EBookType.Comic => (typeof(List<byte[]>), book.ComicContent?.Count >= 3 ? book.ComicContent.Take(3).ToList() : null)!,
				EBookType.Story => (typeof(string), book.StoryContent)!,
				_ => null,
			};
		}
		/// <summary>
		/// Có thể trả về kiểu dữ liệu động, trong trường hợp này có thể là strg hoặc List<byte[]>
		/// </summary>
		/// <param name="intro"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static dynamic ConvertToType(this (Type, object)? intro)
		{
			if (intro == null)
				throw new ArgumentNullException(nameof(intro));
			return Convert.ChangeType(intro.Value.Item2, intro.Value.Item1);
		}
		private static T ConvertToType<T>(this (Type, object)? intro)
		{
			if (intro == null)
				throw new ArgumentNullException(nameof(intro));
			if (intro.Value.Item1 != typeof(T))
				throw new InvalidCastException($"Cannot convert from {intro.Value.Item1} to {typeof(T)}");
			return (T)intro.Value.Item2;
		}


		public static List<string> GetIntroString(this Book book)
		{
			return book.BookType switch
			{
				EBookType.Comic => book.ConvertBytesToStringsContent(),
				EBookType.Story => string.IsNullOrEmpty(book.StoryContent) ? new List<string>() : new List<string> { book.GetIntroTextFromContent() },
				_ => throw new Exception("Không có kiểu khác nữa đâu"),
			};
		}
		/// <summary>
		/// Dù là dạng ảnh nào thì cũng về Png để đồng nhất
		/// Kiến trúc của js khi load đầu vào tham số (nếu là ảnh) thì sẽ là 
		/// let base64Image = ... // The base64 string from the API
		/// let img = new Image();
		/// img.src = "data:image/png;base64," + base64Image;
		/// </summary>
		/// <returns></returns>
		private static List<string> ConvertBytesToStringsContent(this Book book)
		{
			// sử dụng linq để kiểm tra nếu đủ 3 ảnh thì chuyển đổi 3 ảnh đầu, còn ít hơn thì lấy cả
			List<byte[]> imagesFirst3 = book.ComicContent?.Count >= 3 ? book.ComicContent.Take(3).ToList() : book.ComicContent ?? new List<byte[]>();
			return imagesFirst3.Select(content => $"data:image/png;base64,{Convert.ToBase64String(content)}").ToList() ?? new List<string>();
		}
		/// <summary>
		/// Lấy int amountWord từ đầu tiên của Content
		/// </summary>
		/// <param name="amountWord"></param>
		/// <returns></returns>
		private static string GetIntroTextFromContent(this Book book, int amountWord = 100)
		{
			var words = book.StoryContent?.Split(' ');
			return string.Join(" ", words!.Take(amountWord));
		}
		#endregion Xuất đầu ra

		#region Thiết lập đầu vào
		/// <summary>
		/// Lưu dạng byte[] thông thường (chứa dữ liệu) cho ảnh
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static byte[] ConvertImageToByteArray(Image image)
		{
			var converter = new ImageConverter();
			return (byte[])converter.ConvertTo(image, typeof(byte[]))!;
		}
		/// <summary>
		/// Lưu dạng byte[] dạng Png (chứa dữ liệu) cho ảnh 
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static byte[] ConvertImageToPngByteArray(Image image)
		{
			using var mStream = new MemoryStream();
			image.Save(mStream, ImageFormat.Png); // lưu dạng Png
			return mStream.ToArray();
		}
		/// <summary>
		/// Lấy toàn bộ ảnh trong đường dẫn và chuyển đổi thành byte[] (byte[] này chỉ chứa dữ liệu)
		/// </summary>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		public static List<byte[]> ConvertImagesToByteArrays(this Book book, bool isConvertToPng, string directoryPath)
		{
			var imageFiles = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
				.Where(file => new string[] { ".jpg", ".jpeg", ".bmp", ".png", ".gif" }.Contains(Path.GetExtension(file)));
			var byteArrays = imageFiles.Select(filePath =>
			{
				using var image = Image.FromFile(filePath);
				return isConvertToPng ? ConvertImageToPngByteArray(image) : ConvertImageToByteArray(image);
			}).ToList();
			return byteArrays;
		}
		public static List<byte[]> ConvertImagesToByteArrays(this Book book, bool isConvertToPng, params string[] filePaths)
		{
			var byteArrays = filePaths.Select(filePath =>
			{
				using var image = Image.FromFile(filePath);
				return isConvertToPng ? ConvertImageToPngByteArray(image) : ConvertImageToByteArray(image);
			}).ToList();
			return byteArrays;
		}
		#endregion Thiết lập đầu vào
	}
}
