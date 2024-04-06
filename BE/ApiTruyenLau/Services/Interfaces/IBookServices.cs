﻿using Item = ApiTruyenLau.Objects.Generics.Items;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;

namespace ApiTruyenLau.Services.Interfaces
{
	public interface IBookServices
	{
		#region Phần intro sách
		public Task<ItemCvt.IntroBookPartCvt> GetIntroById(string bookId, int amountPage); 
		// có thể lấy theo ngẫu nhiên hoặc theo tương tác của người dùng 
		public Task<ItemCvt.IntroBookPartCvt> GetIntros(string userId);
		public Task<List<ItemCvt.IntroBookPartCvt>> GetIntrosBySomething(int amountIntros, List<string> skipIds, Dictionary<string, string> bookFields, int amountPage);
		#endregion Phần intro sách

		#region Nội dung sách 
		public Task<ItemCvt.BookCvt> GetBookById(string bookId);
		public Task<List<string>> GetNextImagesForContent(string bookId, int skipImages, int takeImages);
		#endregion Nội dung sách 

		#region Phần tạo sách
		public Task<bool> CreateBooks(List<ItemCvt.BookCreaterCvt> bookCreaterCvts);
		#endregion Phần tạo sách
	}
}
