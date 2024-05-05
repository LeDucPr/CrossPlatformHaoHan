using Item = ApiTruyenLau.Objects.Generics.Items;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;

namespace ApiTruyenLau.Services.Interfaces
{
	public interface IBookServices
	{
		#region Phần bìa sách 
		public Task<ItemCvt.CoverBookCvt> GetCoverById(string bookId);
		public Task<List<string>> GetCoversByClientIds(string clientId);
		public Task<List<ItemCvt.CoverBookCvt>> GetCoversByFieldsEquals(int amountCovers, List<string> skipIds, Dictionary<string, string> bookFields);
		public Task<List<ItemCvt.CoverBookCvt>> GetCoversByFieldsContrains(int amountCovers, List<string> skipIds, Dictionary<string, List<string>> bookFields);
		public Task<List<string>> GetSortedCoversByFields(int amountCovers, List<string> skipIds, bool ascending = false, params string[] sortedBookFields);
		public Task<List<string>> GetSortedCoversByFields(int amountCovers, List<string> skipIds, Dictionary<string, List<string>> bookFields, bool ascending = false, params string[] sortedBookFields);
		#endregion Phần bìa sách

		#region Phần intro sách
		public Task<ItemCvt.IntroBookPartCvt> GetIntroById(string bookId);
		#endregion Phần intro sách

		#region Nội dung sách 
		public Task<ItemCvt.BookCvt> GetBookById(string bookId);
		public Task<List<string>> GetNextImagesForContent(string bookId, int skipImages, int takeImages);
		#endregion Nội dung sách 

		#region Phần người đọc
		public Task UpdateBookReader(string bookId);
		#endregion Phần người đọc


		#region Phần tạo sách
		public Task<bool> CreateBooks(List<ItemCvt.BookCreaterCvt> bookCreaterCvts);
		#endregion Phần tạo sách
	}
}
