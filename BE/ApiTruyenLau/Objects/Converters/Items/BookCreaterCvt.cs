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
		//public Dictionary<string, byte[]>? Images { get; set; }

		// thể loại truyện 0: Comic, 1 Story 
		public EBookType BookType { get; set; } = EBookType.Story; // để mặc định là truyện chữ

		// tài nguyên nội dung truyện
		public string? ContentLink { get; set; } = null!; // cái này (hiện tại để dưới dạng đường dẫn thư mục)

		// tài nguyên bìa sách 
		//public string[]? CoverImagePaths { get; set; } = null!; // đường dẫn ảnh bìa sách
		public string? CoverLink { get; set; } = null!; // link bìa sách (hiện tại để dưới dạng đường dẫn thư mục)

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
				ContentLink = bcCvt.ContentLink!,
				CoverLink = bcCvt.CoverLink!
			};
			// Sau khi thay đổi thì Document chỉ lưu bằng đường dẫn thư mục
			// Với việc sử dụng thư mục điều hướng thì có thể bỏ qua việc sử dụng BookType
			// Lúc này sử dụng đường dẫn tương đối (tính từ file .exe) là tốt nhất cho việc Lấy trích dẫn 
			return book;
		}
	}
}


//[
//  {
//    "id": "0001",
//    "author": "Kawahara Reki",
//    "publisher": "IPM",
//    "translator": "IPM",
//	"publishDate": "2024-03-31T09:06:23.228Z",
//    "title": "Sword Art Online - Alicization Beginning",
//    "part": 1,
//    "totalPart": 1,
//    "description": "Sử dụng một cỗ máy FullDive mới có tên là Soul Translator, Kirito bước vào Underworld khi còn là một đứa trẻ mà không có ký ức ở thế giới thực. Ở đó, anh cùng những người bạn Alice Zuberg và Eugeo dấn thân vào một cuộc phiêu lưu, kết thúc bằng việc Alice vô tình vi phạm Danh mục cấm kỵ, gây ra hậu quả tai hại. Sáu năm sau, Kirito tỉnh dậy trong Underworld với ký ức ở thế giới thực của mình. Lần này, Eugeo và Kirito rời làng của họ với nhiệm vụ đưa Alice trở về quê hương.",
//    "edition": "Volume 9",
//    "version": "Volume 9",
//    "genre": "No",
//    "isbn": "ISBN 4-04-886271-5",
//    "language": "Tiếng Việt",
//    "page": 408,
//    "format": ".txt",
//    "price": 78000,
//    "size": {
//      "location": {
//        "x": 0,
//        "y": 0
//      },
//      "size": {
//	"width": 13,
//        "height": 18
//      },
//      "x": 0,
//      "y": 0,
//      "width": 13,
//      "height": 18
//    },
//    "bookType": 0,
//    "contentLink": "C:\\Users\\duc18\\Downloads\\DataTest\\Content",
//    "coverLink": "C:\\Users\\duc18\\Downloads\\DataTest\\Cover"
//  }
//]