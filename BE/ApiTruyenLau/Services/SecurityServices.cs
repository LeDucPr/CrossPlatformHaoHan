using ApiTruyenLau.Objects.Generics.Items;
using ApiTruyenLau.Services.Interfaces;
using DataConnecion.MongoObjects;
using Newtonsoft.Json;
using MGDBs = DataConnecion.MongoObjects.CommonObjects;
using User = ApiTruyenLau.Objects.Generics.Users;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using Item = ApiTruyenLau.Objects.Generics.Items;
using ApiTruyenLau.Objects.Converters.Users;
using System.Net;
using ApiTruyenLau.Objects.Generics.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiTruyenLau.Objects.Interfaces.Users;

namespace ApiTruyenLau.Services
{
    public class SecurityServices : ISecurityServices
    {
        private readonly IConfiguration _configuration;
        private MGDBs _DB;
        private readonly Type[] typeColection = new Type[] { typeof(User.Client) };
        public SecurityServices(IConfiguration configuration)
        {
            _configuration = configuration;
            this._DB = new MGDBs();
            var mongoDBSettings = _configuration.GetSection("MongoDB").Get<MongoDBSettings>();
            this._DB = this._DB.AddMongoDBSrv(mongoDBSettings?.ConnectionString!).AddMongoDBCollections(this.typeColection);
        }

        /// <summary>
        /// Hàm này thường được dùng sau khi đăng nhập, thì người dùng lúc này được cấp một token mới
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool GenerateJwtToken(UserCvt.ClientInfoCvt user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.UserNameAccount)
                    // Thêm bất kỳ claim nào khác bạn muốn token chứa
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token hết hạn sau 7 ngày
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            user.Token = tokenString; // cập nhật trước để truyền đi, việc đồng bộ được diễn ra sau đó
            // Lưu token vào cơ sở dữ liệu trên một luồng khác, _ chống lỗi thôi 
            // cho nó cập nhật tự động, lúc này có thể chắc chắn token
            // được đồng bộ trong hệ thống mà không quan trong thời gian đồng bộ 
            _ = Task.Run(async () => await _DB.GetMongoDBEntity(typeof(User.Client)).UpdateObject(new Dictionary<string, string>()
                { {nameof(User.Client.Id), user.Id} },
                $"{nameof(User.Client.Account)}.{nameof(User.Client.Account.Token)}",
                tokenString!,
                typeof(string)));
            return tokenString != null ? true : throw new Exception("Không cấp được token");
        }

        public bool CompareAutomaticallyGenerated(UserCvt.ClientInfoCvt user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = false
            };
            try
            {
                var claims = tokenHandler.ValidateToken(user.Token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> Compare(UserCvt.ClientInfoCvt user)
        {
            var findedClientObjs = await _DB.GetMongoDBEntity(typeof(User.Client)).FindObjects(new Dictionary<string, string>()
                {{ nameof(User.Client.Id), user.Id}});
            if (findedClientObjs != null && findedClientObjs.Count > 0)
            {
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    SerializationBinder = new MySerializationBinderBook()
                };
                User.Client? findedClient = findedClientObjs
                    .Select(obj => JsonConvert.DeserializeObject<User.Client>(JsonConvert.SerializeObject(obj), settings))
                    .ToList().ElementAt(0);
                if (findedClient != null)
                    if (findedClient.Account.Token == user.Token)
                        return true;
            }
            throw new Exception("Không thể lấy dữ liệu, người dùng không hợp lệ");
        }

        public async Task<bool> Compare(string userId, string token)
        {
            try
            {
                var findedClientObjs = await _DB.GetMongoDBEntity(typeof(User.Client)).FindObjects(new Dictionary<string, string>()
                {{ nameof(User.Client.Id), userId}});
                if (findedClientObjs != null && findedClientObjs.Count > 0)
                {
                    var settings = new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        SerializationBinder = new MySerializationBinderAccount()
                    };
                    User.Client? findedClient = findedClientObjs
                        .Select(obj => JsonConvert.DeserializeObject<User.Client>(JsonConvert.SerializeObject(obj), settings))
                        .ToList().ElementAt(0);
                    if (findedClient!.Account.Token.Equals(token))
                        return true;
                }
                return false;
            }
            catch { throw new Exception("Không thể lấy dữ liệu, người dùng không hợp lệ"); }
        }
    }
}
