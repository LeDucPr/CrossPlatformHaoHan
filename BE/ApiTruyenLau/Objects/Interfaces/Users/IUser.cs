using ApiTruyenLau.Objects.Generics;

namespace ApiTruyenLau.Objects.Interfaces.Users
{
    public interface IUser
    {
        public IAccount Account { get; set; }
    }
}
