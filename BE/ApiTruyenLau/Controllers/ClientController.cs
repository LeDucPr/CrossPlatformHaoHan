using ApiTruyenLau.Objects.Converters.Users;
using ApiTruyenLau.Services;
using ApiTruyenLau.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        private ISecurityServices _securityServices;
        public ClientController(IAccountServices accountServices, ILogger<ClientController> logger, IConfiguration configuration, IClientServices clientServices, ISecurityServices securityServices)
        {
            _logger = logger;
            _configuration = configuration;
            _accountServices = accountServices;
            _clientServices = clientServices;
            _securityServices = securityServices;
        }


        [HttpGet("GetBookIdsByClientId/{clientId}")]
        public async Task<ActionResult<List<string>>> GetBookIdsByClientId(string clientId, [FromHeader] string userId, [FromHeader] string token)
        {
            bool tkComparation = await _securityServices.Compare(userId, token);
            var clientReadedCvt = await _clientServices.GetReadedById(clientId);
            return Ok(clientReadedCvt.ReadedId);
        }

        [HttpPost("UpdateBookIdsByClientId/{clientId}/{bookId}")]
        public async Task<ActionResult> UpdateBookIdsByClientId(string clientId, string bookId, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
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
            try
            {
                UserCvt.ClientInfoCvt clientInfoCvt = new UserCvt.ClientInfoCvt()
                {
                    UserNameAccount = userName,
                    PasswordAccount = password
                };
                var clientExist = await _accountServices.SignInClient(clientInfoCvt);
                UserCvt.ClientInfoCvt clientInfoExist = clientExist.ToClientInfoCvt();
                bool token = _securityServices.GenerateJwtToken(clientInfoExist);
                if (token) return Ok(clientInfoExist);
                return Unauthorized();
            }
            catch { return Unauthorized(); }
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