using ApiTruyenLau.Objects.Converters.Users;
using User = ApiTruyenLau.Objects.Generics.Users;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;

namespace ApiTruyenLau.Services.Interfaces
{
	public interface IAccountServices
	{
		public Task<bool> SignUpClient(UserCvt.ClientInfoCvt clientInfoCvt); 
		public Task<User.Client> SignInClient(UserCvt.ClientInfoCvt clientInfoCvt);
	}
}
