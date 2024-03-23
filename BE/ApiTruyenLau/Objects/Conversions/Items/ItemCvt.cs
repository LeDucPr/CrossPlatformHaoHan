namespace ApiTruyenLau.Objects.Conversions.Items
{
	public class ItemCvt
	{
		public string Id { get; set; } = null!;
		// tham gia
		public string Author { get; set; } = null!;
		public string Publisher { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Genre { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Language { get; set; } = null!;
		public byte[] ? Image { get; set; }
		// đánh gia 
		public string ? Rating { get; set; }
		public Dictionary<string, string> ? Viewers { get; set; }
	}

	public class BookCvtExtension()
	{

	}
}
