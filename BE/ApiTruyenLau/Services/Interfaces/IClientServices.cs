using User = ApiTruyenLau.Objects.Generics.Users;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using Item = ApiTruyenLau.Objects.Generics.Items;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;

namespace ApiTruyenLau.Services.Interfaces
{
	public interface IClientServices
	{
		public Task<UserCvt.ClientReadedCvt> GetReadedById(string clientId);
		public Task UpdateClientReadedId(string clientId, string boolId);
		public Task UpdateSuggestions(string clientId, int suggestAmount = 5);
	}
}
