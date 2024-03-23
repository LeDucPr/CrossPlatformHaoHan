using System.Drawing;
namespace ApiTruyenLau.Objects.Interfaces.Items
{
	public interface IBook
	{
		public string Id { get; set; }
		// tham gia 
		public string Author { get; set; }// tác giả
		public string Publisher { get; set; } // nhà xuất bản
		public string Translator { get; set; } // người dịch
		public DateTime PublishDate { get; set; } // ngày xuất bản
		public string Title { get; set; } // tiêu đề
		public int Part { get; set; } // số phần
		public int TotalPart { get; set; } // tổng số phần
		public string Description { get; set; } // mô tả
		public string Edition { get; set; } // phiên bản
		public string Version { get; set; } // phiên bản
		public string Genre { get; set; } // thể loại
		public string ISBN { get; set; } // mã số ISBN
		public string Language { get; set; } // ngôn ngữ
		public int Page { get; set; } // số trang
		public string Format { get; set; } // định dạng
		public double Price { get; set; } // giá

		//public Rectangle Size { get; set; } // kích thước

		// đánh giá
		public string Rating { get; set; } // đánh giá
		public int Reader { get; set; } // số người đọc
		public Dictionary<string, string> Viewers { get; set; } // người xem

		// tài nguyên
		public byte[] Image { get; set; } // hình ảnh
		public string Link { get; set; } // liên kết

		// số bản lưu trữ trên database
		public int NumberOfCopies { get; set; } // số bản lưu trữ
		public List<string> DataStoragePaths { get; set; } // đường dẫn lưu trữ
	}
}
