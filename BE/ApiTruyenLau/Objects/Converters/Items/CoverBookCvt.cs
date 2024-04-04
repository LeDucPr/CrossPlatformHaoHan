using ApiTruyenLau.Objects.Extensions.Items;
using ApiTruyenLau.Objects.Generics.Items;

namespace ApiTruyenLau.Objects.Converters.Items
{
	public class CoverBookCvt
	{
		public string Id { get; set; } = null!;
		public string Author { get; set; } = null!;
		public string Publisher { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Genre { get; set; } = null!;
		public int Page { get; set; }
		public int Part { get; set; }
		public string Description { get; set; } = null!;
		public string Language { get; set; } = null!;
		public List<string>? coverComicImagePngStrings { get; set; } = null!;
		public string? coverTextString { get; set; } = null!;
		// đánh giá 
		public string? Rating { get; set; }
		public Dictionary<string, string>? Viewers { get; set; }
	}
	public static class CoverBookCvtExtensions
	{
		public static CoverBookCvt ToCoverBookCvt(this Book book)
		{
			(List<string> coverComicImagePngStrings, string coverTextString) = book.GetCover(percentSize:40, quality:60);
			CoverBookCvt coverBookCvt = new CoverBookCvt();
			coverBookCvt.Id = book.Id;
			coverBookCvt.Author = book.Author;
			coverBookCvt.Publisher = book.Publisher;
			coverBookCvt.Title = book.Title;
			coverBookCvt.Genre = book.Genre;
			coverBookCvt.Page = book.Page;
			coverBookCvt.Part = book.Part;
			coverBookCvt.Description = book.Description;
			coverBookCvt.Language = book.Language;
			coverBookCvt.coverComicImagePngStrings = coverComicImagePngStrings;
			coverBookCvt.coverTextString = coverTextString;
			// Lấy 3 ảnh đầu tiên nếu là truyện tranh, 
			coverBookCvt.Rating = book.Rating;
			coverBookCvt.Viewers = book.Viewers;
			return coverBookCvt;
		}
	}
}
