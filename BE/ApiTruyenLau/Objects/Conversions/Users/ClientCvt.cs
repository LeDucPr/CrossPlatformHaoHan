using ApiTruyenLau.Objects.Generics.Users;

namespace ApiTruyenLau.Objects.Conversions.Users
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
	public class ClientServiceCvt()
	{
		public string Id { get; set; } = null!;
		public string? Token { get; set; }
		public string UserNameAccount { get; set; } = null!;
	}
}
