using Item = ApiTruyenLau.Objects.Generics.Items;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;

namespace ApiTruyenLau.Services.Interfaces
{
	public interface IBookServices
	{
		#region Phần bìa sách 
		public Task<ItemCvt.CoverBookCvt> GetCoverById(string bookId);
		#endregion Phần bìa sách

		#region Phần intro sách
		public Task<ItemCvt.IntroBookPartCvt> GetIntroById(string bookId); 
		// có thể lấy theo ngẫu nhiên hoặc theo tương tác của người dùng 
		public Task<ItemCvt.IntroBookPartCvt> GetIntros(string userId);
		public Task<List<ItemCvt.CoverBookCvt>> GetCoversByFieldsEquals(int amountCovers, List<string> skipIds, Dictionary<string, string> bookFields);
		public Task<List<ItemCvt.CoverBookCvt>> GetCoversByFieldsContrains(int amountCovers, List<string> skipIds, Dictionary<string, List<string>> bookFields);
		#endregion Phần intro sách

		public Task<string> UpdateBookRating(string bookId);

        #region Nội dung sách 
        public Task<ItemCvt.BookCvt> GetBookById(string bookId);
		public Task<List<string>> GetNextImagesForContent(string bookId, int skipImages, int takeImages);
		#endregion Nội dung sách 

		#region Phần tạo sách
		public Task<bool> CreateBooks(List<ItemCvt.BookCreaterCvt> bookCreaterCvts);
		#endregion Phần tạo sách
	}
}
