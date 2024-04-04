using ApiTruyenLau.Objects.Extensions.Items;
using ApiTruyenLau.Objects.Generics.Items;

namespace ApiTruyenLau.Objects.Converters.Items
{
	public class IntroBookPartCvt
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
		public List<string>? introComicImagePngStrings { get; set; } = null!;
		public string? introTextString { get; set; } = null!;
		// đánh giá 
		public string? Rating { get; set; }
		public Dictionary<string, string>? Viewers { get; set; }
	}

	public static class IntroBookPartCvtExtensions
	{
		public static IntroBookPartCvt ToIntroBookPartCvt(this Book book)
		{
			(List<string> introComicImagePngStrings, string introTextString) = book.GetIntro(percentSize: 80, quality:80);
			IntroBookPartCvt introBookPartCvt = new IntroBookPartCvt();
			introBookPartCvt.Id = book.Id;
			introBookPartCvt.Author = book.Author;
			introBookPartCvt.Publisher = book.Publisher;
			introBookPartCvt.Title = book.Title;
			introBookPartCvt.Genre = book.Genre;
			introBookPartCvt.Page = book.Page;
			introBookPartCvt.Part = book.Part;
			introBookPartCvt.Description = book.Description;
			introBookPartCvt.Language = book.Language;
			introBookPartCvt.introComicImagePngStrings = introComicImagePngStrings;
			introBookPartCvt.introTextString = introTextString;
			// Lấy 3 ảnh đầu tiên nếu là truyện tranh, 
			introBookPartCvt.Rating = book.Rating;
			introBookPartCvt.Viewers = book.Viewers;
			return introBookPartCvt;
		}
	}
}
