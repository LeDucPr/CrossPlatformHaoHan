using ApiTruyenLau.Objects.Generics.Items;

namespace ApiTruyenLau.Objects.Converters.Items
{
	/// <summary>
	/// chuyển đổi data truyền đi 
	/// </summary>
	public class BookCvt
	{
		public string Id { get; set; } = null!;
		// tham gia
		public string Author { get; set; } = null!;
		public string Publisher { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Genre { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Language { get; set; } = null!;
		public Dictionary<string, byte[]>? Images { get; set; }
		// đánh giá 
		public string? Rating { get; set; }
		public Dictionary<string, string>? Viewers { get; set; }
	}

	public static class BookCvtExtension
	{
		public static BookCvt ToBookCvt(this Book book)
		{
			BookCvt bookCvt = new BookCvt()
			{
				Id = book.Id,
				Author = book.Author,
				Publisher = book.Publisher,
				Title = book.Title,
				Genre = book.Genre,
				Description = book.Description,
				Language = book.Language,
				Rating = book.Rating,
				Viewers = book.Viewers
			};
			return bookCvt;
		}
	}
}
