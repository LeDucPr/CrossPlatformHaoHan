using ApiTruyenLau.Objects.Interfaces.Items;
using System.Drawing;
using System.Drawing.Imaging;

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
		private string _contentLink = null!; // đường dẫn nội dung sách (cái này chắc là file text)
		private string _coverLink = null!; // đường dẫn bìa sách (cái này chắc là file ảnh)

		// --> Tất nhiên nếu là Comic thì _storyContent = null và ngược lại
		// --> Nội dung được lưu vào trong DataBase dưới dạng byte[]
		// --> Nhưng Intro bìa sách thì được lưu dưới dạng file ảnh thuần và được truy xuất bởi đường dẫn

		// đánh giá 
		private string? _rating = null!;
		private int _reader;
		private Dictionary<string, string>? _viewers; // người xem (userName, đánh giá)

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
		public string ContentLink { get { return _contentLink; } set { _contentLink = value; } } // đường dẫn nội dung

		// tài nguyên bìa sách
		public string CoverLink { get { return _coverLink; } set { _coverLink = value; } } // đường dẫn bìa sách

		// đánh giá
		public string? Rating { get { return _rating; } set { _rating = value; } } // đánh giá
		public int Reader { get { return _reader; } set { _reader = value; } } // số người đọc
		public Dictionary<string, string>? Viewers { get { return _viewers; } set { _viewers = value; } } // người xem

		// số bản lưu trữ trên database (dành cho lưu trữ phi tập trung, có thể dùng MQ thỏ)
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

		/// <summary>
		/// Trả về Intro của cả 2 dạng 
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		public static (List<string> comicImagePngStrings, string textString) GetIntro(this Book book, int amountImages = 3, int amountWords = 100)
		{
			return (book.GetIntroImagesStringBase64PngFromDirectory(book.ContentLink, amountImages), book.GetIntroTextFromContent(book.ContentLink, amountWords));
		}

		public static (List<string> comicImagePngStrings, string textString) GetCover(this Book book)
		{
			return (book.GetImageStringBase64PngFromDirectory(book.CoverLink), book.GetTextFromDirectory(book.CoverLink));
		}

		public static (List<string> comicImagePngStrings, string textString) GetContent(this Book book, string directoryPath)
		{
			return (book.GetImageStringBase64PngFromDirectory(book.ContentLink), book.GetTextFromDirectory(book.ContentLink));
		}

		/// <summary>
		/// Đều trả về List<string>, nhưng mà của Text thì có List chắc chắn 100 ký tự nhưng mà chỉ có 1 phần tử trong List
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static List<string> GetIntroByBookType(this Book book, string directoryPath)
		{
			return book.BookType switch
			{
				EBookType.Comic => book.GetIntroImagesStringBase64PngFromDirectory(directoryPath, amount: 3),
				EBookType.Story => new List<string>() { book.GetIntroTextFromContent(directoryPath, amountWords: 100) },
				_ => throw new Exception("Không có kiểu khác nữa đâu"),
			}; 
		}

		/// <summary>
		/// Lấy vài ảnh thôi (chắc là 3 ảnh)
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		private static List<string> GetIntroImagesStringBase64PngFromDirectory(this Book book, string directoryPath, int amount)
		{
			List<byte[]> images = book.ConvertImagesToByteArrays(true, directoryPath, amount);
			if (images != null && images.Count > 0)
				return images.Select(image => $"data:image/png;base64,{Convert.ToBase64String(image)}").ToList();
			return new List<string>();
		}

		/// <summary>
		/// Coi trong thư mục có thằng nào ảnh bế hết ra 
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		private static List<string> GetImageStringBase64PngFromDirectory(this Book book, string directoryPath)
		{
			List<byte[]> images = book.ConvertImagesToByteArrays(true, directoryPath);
			if (images != null && images.Count > 0)
				return images.Select(image => $"data:image/png;base64,{Convert.ToBase64String(image)}").ToList();
			return new List<string>();
		}
		/// <summary>
		/// Coi trong thư mục có thằng nào text bế hết ra rồi gộp lại (thích trả về từng phân vùng truyện theo tệp thì tách thành List<string>)
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		private static string GetTextFromDirectory(this Book book, string directoryPath)
		{
			List<string> strings = book.ConvertTextToStrings(directoryPath);
			if (strings != null && strings.Count > 0)
				return string.Join("\n", strings);
			return string.Empty;
		}
		/// <summary>
		/// Lây tầm 100 từ thôi 
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <param name="amountWords"></param>
		/// <returns></returns>
		private static string GetIntroTextFromContent(this Book book, string directoryPath, int amountWords = 100)
		{
			List<string> strings = book.ConvertTextToStrings(directoryPath);
			if (strings != null && strings.Count > 0)
				return string.Join(" ", strings).Split(' ').Take(amountWords).Aggregate((a, b) => $"{a} {b}");
			return string.Empty;
		}
		#endregion Xuất đầu ra


		#region Thiết lập đầu vào
		/// <summary>
		/// Lấy toàn bộ ảnh trong đường dẫn và chuyển đổi thành byte[] (byte[] này chỉ chứa dữ liệu)
		/// </summary>
		/// <param name="directoryPath"></param>
		/// <param name="amount"></param>"
		/// <returns></returns>
		private static List<byte[]> ConvertImagesToByteArrays(this Book book, bool isConvertToPng, string directoryPath, int amount = -1)
		{
			string[] imageFiles = Directory.GetFiles(directoryPath).Where(file => IsImage(file)).ToArray();
			if (amount != -1 && imageFiles.Length > amount)
				imageFiles = imageFiles.Take(amount).ToArray();
			var byteArrays = imageFiles.Select(filePath =>
			{
				using var image = Image.FromFile(filePath);
				return isConvertToPng ? ConvertImageToPngByteArray(image) : ConvertImageToByteArray(image);
			}).ToList();
			return byteArrays;
		}
		/// <summary>
		/// Giống cái cùng tên nhưng thay vì lấy từ thư mục thì lấy từ đường dẫn trực tiếp của tên ảnh 
		/// </summary>
		/// <param name="book"></param>
		/// <param name="isConvertToPng"></param>
		/// <param name="filePaths"></param>
		/// <returns></returns>
		private static List<byte[]> ConvertImagesToByteArrays(this Book book, bool isConvertToPng, params string[] filePaths)
		{
			var byteArrays = filePaths.Select(filePath =>
			{
				using var image = Image.FromFile(filePath);
				return isConvertToPng ? ConvertImageToPngByteArray(image) : ConvertImageToByteArray(image);
			}).ToList();
			return byteArrays;
		}
		/// <summary>
		/// Lấy toàn bộ text trong đường dẫn và chuyển đổi thành string (string này chỉ chứa dữ liệu)
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		private static List<string> ConvertTextToStrings(this Book book, string directoryPath, int amount = -1)
		{
			string[] textFiles = Directory.GetFiles(directoryPath).Where(file => IsText(file)).ToArray();
			if (amount != -1 && textFiles.Length > amount)
				textFiles = textFiles.Take(amount).ToArray();
			var textArrays = textFiles.Select(filePath =>
			{
				using var reader = new StreamReader(filePath);
				return reader.ReadToEnd();
			}).ToList();
			return textArrays;
		}
		/// <summary>
		/// Lưu dạng byte[] thông thường (chứa dữ liệu) cho ảnh
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		private static byte[] ConvertImageToByteArray(Image image)
		{
			var converter = new ImageConverter();
			return (byte[])converter.ConvertTo(image, typeof(byte[]))!;
		}
		/// <summary>
		/// Lưu dạng byte[] dạng Png (chứa dữ liệu) cho ảnh 
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		private static byte[] ConvertImageToPngByteArray(Image image)
		{
			using var mStream = new MemoryStream();
			image.Save(mStream, ImageFormat.Png); // lưu dạng Png
			return mStream.ToArray();
		}
		/// <summary>
		/// Check coi có phải ảnh không
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private static bool IsImage(string file)
		{
			string[] extensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp", ".svg" };
			return extensions.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		}
		/// <summary>
		/// Check coi có phải text không
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private static bool IsText(string file)
		{
			string[] extensions = new string[] { ".txt" };//, ".doc", ".docx", ".pdf" };
			return extensions.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		}
		#endregion Thiết lập đầu vào
	}
}
