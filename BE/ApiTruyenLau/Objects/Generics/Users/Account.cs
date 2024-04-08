using ApiTruyenLau.Objects.Interfaces.Users;
using System.Security.Cryptography;
using System.Text;

namespace ApiTruyenLau.Objects.Generics.Users
{
	public class Account : IAccount, IEquatable<Account>
	{
		public string UserName { get { return userName; } set { userName = value; } }
		public string Password { get { return password; } set { password = value; } }
		public string Email { get { return email; } set { email = value; } }
		public string FirstName { get { return firstName; } set { firstName = value; } }
		public string LastName { get { return lastName; } set { lastName = value; } }
		public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
		public DateTime LastLoginDate { get { return lastLoginDate; } set { lastLoginDate = value; } }
		public DateTime CreateDate { get { return createDate; } set { createDate = value; } }
		private string userName = string.Empty;
		private string password = string.Empty;
		private string email = string.Empty;
		private string firstName = string.Empty;
		private string lastName = string.Empty;
		private string phoneNumber = string.Empty;
		private DateTime createDate;
		private DateTime lastLoginDate;
		public static string ToSHA512(string value)
		{
			string hcValue = string.Empty;
			using (var sha512 = SHA512.Create())
			{
				var bytes = Encoding.UTF8.GetBytes(value);
				var hash = sha512.ComputeHash(bytes);
				return Convert.ToBase64String(hash);
			}
		}

		public bool Equals(Account? other)
		{
			throw new NotImplementedException();
		}
	}
}
