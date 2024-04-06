using ApiTruyenLau.Objects.Converters.Users;
using ApiTruyenLau.Services;
using ApiTruyenLau.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;
using ApiTruyenLau.Objects.Converters.Items;
using System.ComponentModel.DataAnnotations;

namespace ApiTruyenLau.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookController : ControllerBase
	{
		private readonly ILogger<ClientController> _logger;
		private readonly IConfiguration _configuration;
		private IBookServices _bookServices;
		private IAccountServices _accountServices;

		public BookController(IBookServices bookServices, IAccountServices accountServices, ILogger<ClientController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
			_bookServices = bookServices;
			_accountServices = accountServices; // cái này cần để lấy theo yêu cầu người dùng 
		}

		#region Phần intro sách
		[HttpGet("GetIntroById")]
		public async Task<ActionResult<ItemCvt.IntroBookPartCvt>> GetIntroById(string bookId, int amountPage)
		{
			try
			{
				var introBookPartCvt = await _bookServices.GetIntroById(bookId, amountPage);
				return Ok(introBookPartCvt);
			}
			catch (Exception ex) { return BadRequest(ex.Message); }
		}

		[HttpGet("GetIntros")]
		public async Task<ActionResult<ItemCvt.IntroBookPartCvt>> GetIntros(string userId)
		{
			try
			{
				var introBookPartCvt = await _bookServices.GetIntros(userId);
				return Ok(introBookPartCvt);
			}
			catch (Exception ex) { return BadRequest(ex.Message); }
		}

		[HttpPut("GetSomeByFields")] // lấy vài quyển theo fields -> intro (filter by System)
		public async Task<ActionResult<List<ItemCvt.IntroBookPartCvt>>> GetIntroByFields([FromBody] ParamForBookIntro pbi)
		{
			try
			{
				var introBookPartCvts = await _bookServices.GetIntrosBySomething(pbi.AmountIntros, pbi.SkipIds, pbi.Fields, pbi.AmountPages);
				return Ok(introBookPartCvts);
			}
			catch (Exception ex) { return BadRequest(ex.Message); }
		}
		#endregion Phần intro sách

		#region Phần nội dung sách
		[HttpGet("GetBookById")]
		public async Task<ActionResult<ItemCvt.BookCvt>> GetBookById(string bookId)
		{
			try
			{
				var a = await _bookServices.GetBookById(bookId);
				return Ok(a);
			}
			catch (Exception ex) { return BadRequest(ex.Message); }
		}

		[HttpGet("GetNextImagesForContent")]
		public async Task<ActionResult<List<string>>> GetNextImagesForContent(string bookId, int skipImages, int takeImages)
		{
			try
			{
				var images = await _bookServices.GetNextImagesForContent(bookId, skipImages, takeImages);
				return Ok(images);
			}
			catch (Exception ex) { return BadRequest(ex.Message); }
		}
		#endregion Phần nội dung sách



		#region Phần tạo sách
		[HttpPost("CreateNewBooks")]
		public async Task<ActionResult<string>> CreateBooks([FromBody] List<ItemCvt.BookCreaterCvt> bookCreaterCvts)
		{
			try
			{
				await _bookServices.CreateBooks(bookCreaterCvts);
				return Ok($"Tạo {bookCreaterCvts.Count()} sách mới thành công");
			}
			catch (Exception ex) { return BadRequest(ex.Message); }
		}
		#endregion Phần tạo sách 



		#region Class nhận Api
		public class ParamForBookIntro
		{
			public int AmountIntros { get; set; }
			public List<string> SkipIds { get; set; } = new List<string>();
			[Required]
			public Dictionary<string, string> Fields { get; set; } = null!; 
			public int AmountPages {  get; set; }
		}
		#endregion Class nhận Api
	}
}
