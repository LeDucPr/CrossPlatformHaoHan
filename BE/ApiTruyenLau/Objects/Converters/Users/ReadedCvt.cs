using System.Collections.Generic;
using ApiTruyenLau.Objects.Interfaces.Users;
using ApiTruyenLau.Objects.Interfaces.Items;
using ApiTruyenLau.Objects.Generics.Users;

namespace ApiTruyenLau.Objects.Converters.Users
{
    public class ClientReadedCvt
    {
        public List<IBook> Readed { get; set; } = null;
    }
    public static class CientReadedCvtxtension
    {
        public static Client ToClietReaded(this ClientInfoCvt clientInfoCvt)
        {
            Client client = new Client();
            client.Id = clientInfoCvt.Id;
            client.Account = new Account
            {
                UserName = clientInfoCvt.UserNameAccount,
                Password = clientInfoCvt.PasswordAccount,
                Email = clientInfoCvt.EmailAccount,
                FirstName = clientInfoCvt.FirstNameAccount,
                LastName = clientInfoCvt.LastNameAccount,
                PhoneNumber = clientInfoCvt.PhoneNumberAccount,
                LastLoginDate = clientInfoCvt.LastLoginDateAccount,
                CreateDate = clientInfoCvt.CreateDateAccount
            };
            return client;
        }
    }
    }
