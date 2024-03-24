using ApiTruyenLau.Objects.Converters.Users;
using ApiTruyenLau.Services.Interfaces;
using DataConnecion.MongoObjects;
using Microsoft.AspNetCore.Mvc;
using Item = ApiTruyenLau.Objects.Generics.Items;
using MGDBs = DataConnecion.MongoObjects.CommonObjects;
using User = ApiTruyenLau.Objects.Generics.Users;
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
		public ClientController(IAccountServices accountServices, ILogger<ClientController> logger, IConfiguration configuration) 
		{
			_logger = logger;
			_configuration = configuration;
			_accountServices = accountServices;
		}

		//[HttpGet(Name = "GetClientById")]
		//public async Task<ActionResult<IEnumerable<User.Client>>> GetClientsAsync(string clientId)
		//{
		//	return null;
		//}
		[HttpGet(Name = "GetClient")]
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
		[HttpPost(Name = "CreateClient")]
		public async Task<ActionResult> SignUp([FromBody] UserCvt.ClientInfoCvt clientInfoCvt)
		{
			try { await _accountServices.SignUpClient(clientInfoCvt); return Ok("Tạo tài khoản mới thành công."); }
			catch (Exception ex) { return Ok(ex.Message); }
		}
	}
}



//"id": "0000001",
//  "token": "string",
//  "userNameAccount": "Conchongu",
//  "passwordAccount": "nvjfrn2U@fwS",
//  "emailAccount": "duc67@gmail.com",
//  "firstNameAccount": "Không",
//  "lastNameAccount": "Trượt",
//  "phoneNumberAccount": "0918273645", 