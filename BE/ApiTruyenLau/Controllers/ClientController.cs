using User = ApiTruyenLau.Objects.Generics.User;
using Microsoft.AspNetCore.Mvc;

namespace ApiTruyenLau.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ClientController : ControllerBase
	{
		private readonly ILogger<ClientController> _logger;

		public ClientController(ILogger<ClientController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetClient")]
		public async Task<ActionResult<IEnumerable<User.Client>>> GetAllClientsAsync()
		{
			//return await Task.Run(() => Enumerable.Range(1, 5).Select(x => ));
			return null; 
		}
	}
}
