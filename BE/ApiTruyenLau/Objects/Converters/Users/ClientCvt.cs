using ApiTruyenLau.Objects.Generics.Users;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ApiTruyenLau.Objects.Converters.Users
{
	/// <summary>
	/// AccountConvert
	/// </summary>
	public class ClientInfoCvt
	{
		public string Id { get; set; } = null!;
		public string? Token { get; set; }
		public string UserNameAccount { get; set; } = null!;
		public string PasswordAccount { get; set; } = null!;
		public string EmailAccount { get; set; } = null!;
		public string FirstNameAccount { get; set; } = null!;
		public string LastNameAccount { get; set; } = null!;
		public string PhoneNumberAccount { get; set; } = null!;
		public DateTime LastLoginDateAccount { get; set; } = DateTime.Now;
		public DateTime CreateDateAccount { get; set; } = DateTime.Now;
	}

	public static class ClientInfoCvtExtensions
	{
		public static Client ToClientAccount(this ClientInfoCvt clientInfoCvt)
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

		public static ClientInfoCvt ToClientInfoCvt(this Client client)
		{
			ClientInfoCvt clientInfoCvt = new ClientInfoCvt();
			clientInfoCvt.Id = client.Id;
			clientInfoCvt.UserNameAccount = client.Account.UserName;
			clientInfoCvt.PasswordAccount = client.Account.Password;
			clientInfoCvt.EmailAccount = client.Account.Email;
			clientInfoCvt.FirstNameAccount = client.Account.FirstName;
			clientInfoCvt.LastNameAccount = client.Account.LastName;
			clientInfoCvt.PhoneNumberAccount = client.Account.PhoneNumber;
			clientInfoCvt.LastLoginDateAccount = client.Account.LastLoginDate;
			clientInfoCvt.CreateDateAccount = client.Account.CreateDate;
			return clientInfoCvt;
		}

		public static bool IsValid(this ClientInfoCvt clientInfoCvt)
		{
			var properties = typeof(ClientInfoCvt).GetProperties();
			MapClientCvtProperties map = new MapClientCvtProperties();
			bool isError = false;
			List<string> errors = new List<string>();
			foreach (var property in properties)
			{
				var value = property.GetValue(clientInfoCvt)?.ToString()?.Trim();
				if (//!map.RegexConditionMapping.ContainsKey(property.Name) ||
					(map.RegexSignUpConditionMapping.ContainsKey(property.Name) && !map.RegexSignUpConditionMapping[property.Name].IsMatch(value!))
				)
				{
					map.IsErrorMapping[property.Name] = true;
					isError = true;
					errors.Add($"{property.Name.ToString()}: {map.VietsubSignUpErrorMapping[property.Name]}");
				}
				else
					map.IsErrorMapping[property.Name] = false;
			}
			if (isError)
			{
				throw new Exception(string.Join(", ", errors));
			}
			return !isError;
		}
		public static bool IsValid(this ClientInfoCvt clientInfoCvt, params Type[] typePickers)
		{
			var properties = typeof(ClientInfoCvt).GetProperties();
			MapClientCvtProperties map = new MapClientCvtProperties();
			// lấy các type từ properties trùng với typePickers
			var propertiesPickers = properties.Where(p => typePickers.Contains(p.PropertyType));
			bool isError = false;
			List<string> errors = new List<string>();
			foreach (var property in propertiesPickers)
			{
				var value = property.GetValue(clientInfoCvt)?.ToString()?.Trim();
				if (//!map.RegexConditionMapping.ContainsKey(property.Name) ||
					(map.RegexSignInConditionMapping.ContainsKey(property.Name) && !map.RegexSignInConditionMapping[property.Name].IsMatch(value!))
				)
				{
					map.IsErrorMapping[property.Name] = true;
					isError = true;
					errors.Add($"{property.Name.ToString()}: {map.VietsubSignInErrorMapping[property.Name]}");
				}
				else
					map.IsErrorMapping[property.Name] = false;
			}
			return isError;
		}
	}

	public class MapClientCvtProperties
	{
		// lỗi đăng ký 
		public Dictionary<string, Regex> RegexSignUpConditionMapping { get; set; }
		public Dictionary<string, string> VietsubSignUpErrorMapping { get; set; }
		// lỗi đăng nhập 
		public Dictionary<string, Regex> RegexSignInConditionMapping { get; set; }
		public Dictionary<string, string> VietsubSignInErrorMapping { get; set; }
		// map error
		public Dictionary<string, bool> IsErrorMapping { get; set; }
		public MapClientCvtProperties()
		{
			// lỗi đăng ký 
			this.RegexSignUpConditionMapping = new Dictionary<string, Regex>()
			{
				{ nameof(ClientInfoCvt.UserNameAccount), new Regex(@"^[a-zA-Z][a-zA-Z]{7,}$")}, // 8 ký tự trở lên
				{ nameof(ClientInfoCvt.PasswordAccount), new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$")}, // 10 ký tự trở lên, ít nhất 1 chữ hoa, 1 số, 1 ký tự đặc biệt
				{ nameof(ClientInfoCvt.EmailAccount), new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")}, // định dạng email
				{ nameof(ClientInfoCvt.FirstNameAccount), new Regex(@"^[a-zA-ZÀ-ỹ]+$")}, // kiểu tiếng việt
				{ nameof(ClientInfoCvt.LastNameAccount), new Regex(@"^[a-zA-ZÀ-ỹ]+$")}, // kiểu tiếng việt
				{ nameof(ClientInfoCvt.PhoneNumberAccount), new Regex(@"^(0\d{9})|(\+849\d{8})$")}, // số điện thoại
			};
			this.VietsubSignUpErrorMapping = new Dictionary<string, string>()
			{
				{ nameof(ClientInfoCvt.UserNameAccount), "Tên người dùng không hợp lệ hoặc đã tồn tại"},
				{ nameof(ClientInfoCvt.PasswordAccount), "Password không đúng yêu cầu"},
				{ nameof(ClientInfoCvt.EmailAccount), "Email không đúng định dạng"},
				{ nameof(ClientInfoCvt.FirstNameAccount), "Sai định dạng"},
				{ nameof(ClientInfoCvt.LastNameAccount), "Sai định dạng"},
				{ nameof(ClientInfoCvt.PhoneNumberAccount), "Số điện thoại không đúng"},
			};
			// lỗi đăng nhập
			this.RegexSignInConditionMapping = new Dictionary<string, Regex>()
			{
				{ nameof(ClientInfoCvt.UserNameAccount), new Regex(@"^[a-zA-Z][a-zA-Z]{7,}$")}, // 8 ký tự trở lên
				{ nameof(ClientInfoCvt.PasswordAccount), new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$")}, // 10 ký tự trở lên, ít nhất 1 chữ hoa, 1 số, 1 ký tự đặc biệt
			};
			this.VietsubSignInErrorMapping = new Dictionary<string, string>()
			{
				{ nameof(ClientInfoCvt.UserNameAccount), "Tên người dùng không hợp lệ hoặc đã tồn tại"},
				{ nameof(ClientInfoCvt.PasswordAccount), "Password không đúng yêu cầu"},
			};
			this.IsErrorMapping = new Dictionary<string, bool>()
			{
				{nameof(ClientInfoCvt.UserNameAccount), false },
				{nameof(ClientInfoCvt.PasswordAccount), false },
				{ nameof(ClientInfoCvt.EmailAccount), false },
				{ nameof(ClientInfoCvt.FirstNameAccount), false },
				{ nameof(ClientInfoCvt.LastNameAccount), false },
				{ nameof(ClientInfoCvt.PhoneNumberAccount), false },
			};
		}
	}
}

