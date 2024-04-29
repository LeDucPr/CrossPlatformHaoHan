using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using User = ApiTruyenLau.Objects.Generics.Users;

namespace ApiTruyenLau.Services.Interfaces
{
    public interface ISecurityServices
    {
        public bool GenerateJwtToken(UserCvt.ClientInfoCvt user);
        public bool CompareAutomaticallyGenerated(UserCvt.ClientInfoCvt user);
        public Task<bool> Compare(UserCvt.ClientInfoCvt user);
        public Task<bool> Compare(string userId, string token);
    }
}
