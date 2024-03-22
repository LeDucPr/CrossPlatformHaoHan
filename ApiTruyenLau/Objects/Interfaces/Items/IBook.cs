namespace ApiTruyenLau.Objects.Interfaces.Items
{
	public interface IBook
	{
		public string Id { get; set; }
		public string Title { get; set; } // tiêu đề
		public string Author { get; set; }// tác giả
		public string Genre { get; set; } // thể loại
		public string Description { get; set; } // mô tả
		public string Publisher { get; set; } // nhà xuất bản
		public DateTime PublishDate { get; set; } // ngày xuất bản
		public string ISBN { get; set; } // mã số ISBN
		public string Language { get; set; } // ngôn ngữ
		public int Page { get; set; } // số trang
		public string Format { get; set; } // định dạng
		public int Price { get; set; } // giá
		public string Rating { get; set; } // đánh giá
		public string Image { get; set; } // hình ảnh
		public string Link { get; set; } // liên kết
	}
}
