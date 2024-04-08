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
}
