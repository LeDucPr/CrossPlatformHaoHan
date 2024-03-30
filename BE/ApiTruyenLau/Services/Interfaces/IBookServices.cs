using Item = ApiTruyenLau.Objects.Generics.Items;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;

namespace ApiTruyenLau.Services.Interfaces
{
	public interface IBookServices
	{
		#region Phần intro sách
		public Task<ItemCvt.IntroBookPartCvt> GetIntroById(string bookId); 
		// có thể lấy theo ngẫu nhiên hoặc theo tương tác của người dùng 
		public Task<ItemCvt.IntroBookPartCvt> GetIntros(string userId);
		#endregion Phần intro sách
		#region Phần tạo sách
		public Task<bool> CreateBooks(List<ItemCvt.BookCreaterCvt> bookCreaterCvts);
		#endregion Phần tạo sách
	}
}
