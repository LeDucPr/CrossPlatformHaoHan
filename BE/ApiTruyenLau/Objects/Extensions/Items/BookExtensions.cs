﻿using ApiTruyenLau.Objects.Generics.Items;
using System.Drawing.Imaging;
using System.Drawing;

namespace ApiTruyenLau.Objects.Extensions.Items
{
	public static class BookExtension
	{
		#region Xuất đầu ra 
		// intro sách là một mẩu đầu của Content (dù là comic hay là story) 
		// thế nên them một field để xác định intro là không nên 

		/// <summary>
		/// Có thể trả về kiểu dữ liệu động, trong trường hợp này có thể là strg hoặc List<byte[]>
		/// </summary>
		/// <param name="intro"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static dynamic ConvertToType(this (Type, object)? intro)
		{
			if (intro == null)
				throw new ArgumentNullException(nameof(intro));
			return Convert.ChangeType(intro.Value.Item2, intro.Value.Item1);
		}
		private static T ConvertToType<T>(this (Type, object)? intro)
		{
			if (intro == null)
				throw new ArgumentNullException(nameof(intro));
			if (intro.Value.Item1 != typeof(T))
				throw new InvalidCastException($"Cannot convert from {intro.Value.Item1} to {typeof(T)}");
			return (T)intro.Value.Item2;
		}

		/// <summary>
		/// Trả về Intro của cả 2 dạng 
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		public static (List<string> comicImagePngStrings, string textString) GetIntro(this Book book, int amountImages = 3, int amountWords = 100)
		{
			return (book.GetIntroImagesStringBase64PngFromDirectory(book.ContentLink, amountImages), book.GetIntroTextFromContent(book.ContentLink, amountWords));
		}

		public static (List<string> comicImagePngStrings, string textString) GetCover(this Book book)
		{
			return (book.GetImageStringBase64PngFromDirectory(book.CoverLink), book.GetTextFromDirectory(book.CoverLink));
		}

		public static (List<string> comicImagePngStrings, string textString) GetContent(this Book book)
		{
			return (book.GetImageStringBase64PngFromDirectory(book.ContentLink), book.GetTextFromDirectory(book.ContentLink));
		}
		public static List<string> GetImageAtElementsStringBase64Png(this Book book, int skipAmount, int takeAmount = 0)
		{
			var bArrays = book.SkipAndNextImages(book.ContentLink, skipAmount, takeAmount);
			if (bArrays != null && bArrays.Count > 0)
				return bArrays.Select(image => $"data:image/png;base64,{Convert.ToBase64String(image)}").ToList();
			return new List<string>();
		}

		/// <summary>
		/// Đều trả về List<string>, nhưng mà của Text thì có List chắc chắn 100 ký tự nhưng mà chỉ có 1 phần tử trong List
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static List<string> GetIntroByBookType(this Book book, string directoryPath)
		{
			return book.BookType switch
			{
				EBookType.Comic => book.GetIntroImagesStringBase64PngFromDirectory(directoryPath, amount: 3),
				EBookType.Story => new List<string>() { book.GetIntroTextFromContent(directoryPath, amountWords: 100) },
				_ => throw new Exception("Không có kiểu khác nữa đâu"),
			};
		}
		/// <summary>
		/// Lấy vài ảnh thôi (chắc là 3 ảnh)
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		private static List<string> GetIntroImagesStringBase64PngFromDirectory(this Book book, string directoryPath, int amount)
		{
			List<byte[]> images = book.ConvertImagesToByteArrays(true, directoryPath, amount);
			if (images != null && images.Count > 0)
				return images.Select(image => $"data:image/png;base64,{Convert.ToBase64String(image)}").ToList();
			return new List<string>();
		}
		/// <summary>
		/// Coi trong thư mục có thằng nào ảnh bế hết ra 
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		private static List<string> GetImageStringBase64PngFromDirectory(this Book book, string directoryPath)
		{
			List<byte[]> images = book.ConvertImagesToByteArrays(true, directoryPath);
			if (images != null && images.Count > 0)
				return images.Select(image => $"data:image/png;base64,{Convert.ToBase64String(image)}").ToList();
			return new List<string>();
		}
		/// <summary>
		/// Coi trong thư mục có thằng nào text bế hết ra rồi gộp lại (thích trả về từng phân vùng truyện theo tệp thì tách thành List<string>)
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <returns></returns>
		private static string GetTextFromDirectory(this Book book, string directoryPath)
		{
			List<string> strings = book.ConvertTextToStrings(directoryPath);
			if (strings != null && strings.Count > 0)
				return string.Join("\n", strings);
			return string.Empty;
		}
		/// <summary>
		/// Lây tầm 100 từ thôi 
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <param name="amountWords"></param>
		/// <returns></returns>
		private static string GetIntroTextFromContent(this Book book, string directoryPath, int amountWords = 100)
		{
			List<string> strings = book.ConvertTextToStrings(directoryPath);
			if (strings != null && strings.Count > 0)
				return string.Join(" ", strings).Split(' ').Take(amountWords).Aggregate((a, b) => $"{a} {b}");
			return string.Empty;
		}
		#endregion Xuất đầu ra


		#region Thiết lập đầu vào
		/// <summary>
		/// Lấy toàn bộ ảnh trong đường dẫn và chuyển đổi thành byte[] (byte[] này chỉ chứa dữ liệu)
		/// </summary>
		/// <param name="isConvertToPng">biến đổi trực tiếp thành định dạng PNG???</param>
		/// <param name="directoryPath"></param>
		/// <param name="amount"></param>"
		/// <param name="isUseCurrentDirectoryPath">sử dụng đường dẫn trực tiếp đọc dữ liệu</param>
		/// <returns></returns>
		private static List<byte[]> ConvertImagesToByteArrays(this Book book, bool isConvertToPng, string directoryPath, int amount = -1, bool isUseCurrentDirectoryPath = true)
		{
			if (isUseCurrentDirectoryPath)
			{
				string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				directoryPath = Path.Combine(baseDirectory, directoryPath);
			}
			string[] imageFiles = Directory.GetFiles(directoryPath).Where(file => IsImage(file)).ToArray();
			if (amount != -1 && imageFiles.Length > amount)
				imageFiles = imageFiles.Take(amount).ToArray();
			var byteArrays = imageFiles.Select(filePath =>
			{
				using var image = Image.FromFile(filePath);
				return isConvertToPng ? ConvertImageToPngByteArray(image) : ConvertImageToByteArray(image);
			}).ToList();
			return byteArrays;
		}
		/// <summary>
		/// Bỏ qua và lấy ảnh tiếp theo trong thư mục (bắt buộc chỉ định số lượng lấy, default = 0)
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <param name="skipAmount"></param>
		/// <param name="takeAmount"></param>
		/// <param name="isUseCurrentDirectoryPath"></param>
		/// <returns></returns>
		private static List<byte[]> SkipAndNextImages(this Book book, string directoryPath, int skipAmount, int takeAmount = 0, bool isUseCurrentDirectoryPath = true)
		{
			if (isUseCurrentDirectoryPath)
			{
				string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				directoryPath = Path.Combine(baseDirectory, directoryPath);
			}
			string[] imageFiles = Directory.GetFiles(directoryPath).Where(file => IsImage(file)).ToArray();
			//if (imageFiles.Length > skipAmount + takeAmount) { }
			imageFiles = imageFiles.Skip(skipAmount).Take(takeAmount).ToArray();
			return book.ConvertImagesToByteArrays(true, imageFiles);
			//return new List<byte[]>(); // không còn ảnh nào
		}
		/// <summary>
		/// Giống cái cùng tên nhưng thay vì lấy từ thư mục thì lấy từ đường dẫn trực tiếp của tên ảnh 
		/// </summary>
		/// <param name="book"></param>
		/// <param name="isConvertToPng"></param>
		/// <param name="filePaths"></param>
		/// <returns></returns>
		private static List<byte[]> ConvertImagesToByteArrays(this Book book, bool isConvertToPng, params string[] filePaths)
		{
			var byteArrays = filePaths.Select(filePath =>
			{
				using var image = Image.FromFile(filePath);
				return isConvertToPng ? ConvertImageToPngByteArray(image) : ConvertImageToByteArray(image);
			}).ToList();
			return byteArrays;
		}
		/// <summary>
		/// Lấy toàn bộ text trong đường dẫn và chuyển đổi thành string (string này chỉ chứa dữ liệu)
		/// </summary>
		/// <param name="book"></param>
		/// <param name="directoryPath"></param>
		/// <param name="amount"></param>
		/// <param name="isUseCurrentDirectoryPath">sử dụng đường dẫn trực tiếp lấy data</param>
		/// <returns></returns>
		private static List<string> ConvertTextToStrings(this Book book, string directoryPath, int amount = -1, bool isUseCurrentDirectoryPath = true)
		{
			if (isUseCurrentDirectoryPath)
			{
				string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				directoryPath = Path.Combine(baseDirectory, directoryPath);
				Console.WriteLine(directoryPath);
			}
			string[] textFiles = Directory.GetFiles(directoryPath).Where(file => IsText(file)).ToArray();
			if (amount != -1 && textFiles.Length > amount)
				textFiles = textFiles.Take(amount).ToArray();
			var textArrays = textFiles.Select(filePath =>
			{
				using var reader = new StreamReader(filePath);
				return reader.ReadToEnd();
			}).ToList();
			return textArrays;
		}
		/// <summary>
		/// Lưu dạng byte[] thông thường (chứa dữ liệu) cho ảnh
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		private static byte[] ConvertImageToByteArray(Image image)
		{
			var converter = new ImageConverter();
			return (byte[])converter.ConvertTo(image, typeof(byte[]))!;
		}
		/// <summary>
		/// Lưu dạng byte[] dạng Png (chứa dữ liệu) cho ảnh 
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		private static byte[] ConvertImageToPngByteArray(Image image)
		{
			using var mStream = new MemoryStream();
			image.Save(mStream, ImageFormat.Png); // lưu dạng Png
			return mStream.ToArray();
		}
		/// <summary>
		/// Check coi có phải ảnh không
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private static bool IsImage(string file)
		{
			string[] extensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp", ".svg" };
			return extensions.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		}
		/// <summary>
		/// Check coi có phải text không
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private static bool IsText(string file)
		{
			string[] extensions = new string[] { ".txt" };//, ".doc", ".docx", ".pdf" };
			return extensions.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		}
		#endregion Thiết lập đầu vào
	}
}
