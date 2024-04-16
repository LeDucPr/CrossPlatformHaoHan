using ApiTruyenLau.Objects.Converters.Users;
using ApiTruyenLau.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;

namespace ApiTruyenLau.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ClientController : ControllerBase
	{
		private readonly ILogger<ClientController> _logger;
		private readonly IConfiguration _configuration;
		private IAccountServices _accountServices;
		private IClientServices _clientServices;
		public ClientController(IAccountServices accountServices, ILogger<ClientController> logger, IConfiguration configuration, IClientServices clientServices)
		{
			_logger = logger;
			_configuration = configuration;
			_accountServices = accountServices;
			_clientServices = clientServices;
		}


		[HttpGet("GetBookIdsByClientId/{clientId}")]
		public async Task<ActionResult<List<string>>> GetBookIdsByClientId(string clientId)
		{
			var clientReadedCvt = await _clientServices.GetReadedById(clientId);
			return Ok(clientReadedCvt.ReadedId);
		}

		[HttpPost("UpdateBookIdsByClientId/{clientId}/{bookId}")]
		public async Task<ActionResult> UpdateBookIdsByClientId(string clientId, string bookId)
		{
			try
			{
				await _clientServices.UpdateClientReadedId(clientId, bookId);
				// Gợi ý truyện tiếp cho phép treo máy cập nhật đẻ không làm chậm response
				_ = Task.Run(() => _clientServices.UpdateSuggestions(clientId)); 
				return Ok("Cập nhật sách đã đọc thành công.");
			}
			catch (Exception ex) { return Ok(ex.Message); }
		}

		[HttpGet("SignIn/{userName}/{password}")]
		public async Task<ActionResult<IEnumerable<UserCvt.ClientInfoCvt>>> SignIn(string userName, string password)
		{
			UserCvt.ClientInfoCvt clientInfoCvt = new UserCvt.ClientInfoCvt()
			{
				UserNameAccount = userName,
				PasswordAccount = password
			};
			var clientExist = await _accountServices.SignInClient(clientInfoCvt);
			UserCvt.ClientInfoCvt clientInfoExist = clientExist.ToClientInfoCvt();
			return Ok(clientInfoExist);
		}

		// tạo người dùng mới
		[HttpPost("SignUp/CreateClient")]
		public async Task<ActionResult> SignUp([FromBody] UserCvt.ClientInfoCvt clientInfoCvt)
		{
			try { await _accountServices.SignUpClient(clientInfoCvt); return Ok("Tạo tài khoản mới thành công."); }
			catch (Exception ex) { return Ok(ex.Message); }
		}
	}
}



//"id": "0294731",
//  "token": "string",
//  "userNameAccount": "Conchongu",
//  "passwordAccount": "nvjfrn2U@fwS",
//  "emailAccount": "duc67@gmail.com",
//  "firstNameAccount": "Không",
//  "lastNameAccount": "Trượt",
//  "phoneNumberAccount": "0918273645", 