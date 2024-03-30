using ApiTruyenLau.Objects.Generics.Items;
using System.Drawing;

namespace ApiTruyenLau.Objects.Converters.Items
{
	public class BookCreaterCvt
	{
		public string Id { get; set; } = null!;
		// tham gia
		public string Author { get; set; } = null!;
		public string Publisher { get; set; } = null!;
		public string Translator { get; set; } = null!;
		public DateTime PublishDate { get; set; } = DateTime.Now;
		public string Title { get; set; } = null!;
		public int Part { get; set; }
		public int TotalPart { get; set; }
		public string Description { get; set; } = null!;
		public string? Edition { get; set; }
		public string? Version { get; set; }
		public string Genre { get; set; } = null!;
		public string? ISBN { get; set; }
		public string Language { get; set; } = null!;
		public int Page { get; set; }
		public string? Format { get; set; } = null!;
		public double Price { get; set; } = 0;
		public Rectangle? Size { get; set; } = new Rectangle(0, 0, 0, 0);
		public Dictionary<string, byte[]>? Images { get; set; }

		// tài nguyên nội dung truyện
		public EBookType BookType { get; set; } = EBookType.Story; // để mặc định là truyện chữ
		public List<string> BookContent { get; set; } = null!; // nội dung truyện
		public string? ContentLink { get; set; } = null!; // cái này chỉ dùng cho truyện tranh 

		// tài nguyên bìa sách 
		//public string? LocalLink { get; set; } = null!; // tài nguyên bìa trên server nội bộ hoặc đường dẫn thư mục 
		//public string? Link { get; set; } // link tài nguyên bìa sách

		public string CoverImageDirectoryLink { get; set; } = null!; // link ảnh bìa sách
		public string[] CoverImagePaths { get; set; } = null!; // đường dẫn ảnh bìa sách
	}

	public static class BookCreaterCvtExtension
	{
		public static Book ToBookCreaterCvt(this BookCreaterCvt bcCvt)
		{
			Book book = new Book()
			{
				Id = bcCvt.Id,
				Author = bcCvt.Author,
				Publisher = bcCvt.Publisher,
				Translator = bcCvt.Translator,
				PublishDate = bcCvt.PublishDate,
				Title = bcCvt.Title,
				Part = bcCvt.Part,
				TotalPart = bcCvt.TotalPart,
				Description = bcCvt.Description,
				Edition = bcCvt.Edition,
				Version = bcCvt.Version,
				Genre = bcCvt.Genre,
				ISBN = bcCvt.ISBN,
				Language = bcCvt.Language,
				Page = bcCvt.Page,
				Format = bcCvt.Format,
				Price = bcCvt.Price,
				Size = bcCvt.Size,
				//Link = bcCvt.Link ?? bcCvt.LocalLink ?? string.Empty, // hiện tại bỏ qua cái này 
				BookType = bcCvt.BookType, // loại sách quy định kiểu đọc dữ liệu 
			};
			switch (bcCvt.BookType)
			{
				case EBookType.Story:
					book.StoryContent = string.Join("\n", bcCvt.BookContent);
					break;
				case EBookType.Comic:
					book.ComicContent = book.ConvertImagesToByteArrays(true, bcCvt.ContentLink!); // chuyển luôn qua PNG
					break;
			}
			if (!string.IsNullOrEmpty(bcCvt.CoverImageDirectoryLink))
				book.CoverImages = book.ConvertImagesToByteArrays(true, bcCvt.CoverImageDirectoryLink);
			else 
				book.CoverImages = book.ConvertImagesToByteArrays(false, bcCvt.CoverImagePaths);
			return book;
		}
	}
}
