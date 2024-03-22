using ApiTruyenLau.Objects.Interfaces.Items;

namespace ApiTruyenLau.Objects.Generics.Items
{
	public partial class Book : IBook
	{
		private string _id = null!;
		private string _title = null!;
		private string _author = null!;
		private string _genre = null!;
		private string _description = null!;
		private string _publisher = null!;
		private DateTime _publishDate;
		private string? _isbn;
		private string _language = null!;
		private int _page;
		private string? _format = null!;
		private int _price;
		private string? _rating = null!;
		private string? _image = null!;
		private string? _link = null!;
		public string Id { get { return _id; } set { _id = value; } } // id truyện
		public string Title { get { return _title; } set { _title = value; } } // tên truyện
		public string Author { get { return _author; } set { _author = value; } } // tác giả
		public string Genre { get { return _genre; } set { _genre = value; } } // thể loại
		public string Description { get { return _description; } set { _description = value; } } // mô tả
		public string Publisher { get { return _publisher; } set { _publisher = value; } } // nhà xuất bản
		public DateTime PublishDate { get { return _publishDate; } set { _publishDate = value; } } // ngày xuất bản
		public string? ISBN { get { return _isbn; } set { _isbn = value; } } // mã số ISBN
		public string Language { get { return _language; } set { _language = value; } } // ngôn ngữ
		public int Page { get { return _page; } set { _page = value; } } // số trang
		public string? Format { get { return _format; } set { _format = value; } } // định dạng
		public int Price { get { return _price; } set { _price = value; } } // giá
		public string? Rating { get { return _rating; } set { _rating = value; } } // đánh giá
		public string? Image { get { return _image; } set { _image = value; } } // ảnh
		public string? Link { get { return _link; } set { _link = value; } } // link
	}
}
