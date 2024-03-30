using ApiTruyenLau.Objects.Converters.Users;
using ApiTruyenLau.Services;
using ApiTruyenLau.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;
using ApiTruyenLau.Objects.Converters.Items;

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

		public BookController(IBookServices bookServices, IAccountServices accountServices , ILogger<ClientController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
			_bookServices = bookServices;
			_accountServices = accountServices; // cái này cần để lấy theo yêu cầu người dùng 
		}

		[HttpGet("GetIntroById")]
		public async Task<ActionResult<ItemCvt.IntroBookPartCvt>> GetIntroById(string bookId)
		{
			try
			{
				var introBookPartCvt = await _bookServices.GetIntroById(bookId);
				return Ok(introBookPartCvt);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetIntros")]
		public async Task<ActionResult<ItemCvt.IntroBookPartCvt>> GetIntros(string userId)
		{
			try
			{
				var introBookPartCvt = await _bookServices.GetIntros(userId);
				return Ok(introBookPartCvt);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("CreateNewBook")]
		public async Task<ActionResult<string>> CreateBooks([FromBody] List<ItemCvt.BookCreaterCvt> bookCreaterCvts)
		{
			try
			{
				await _bookServices.CreateBooks(bookCreaterCvts);
				return Ok("Tạo sách mới thành công");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
