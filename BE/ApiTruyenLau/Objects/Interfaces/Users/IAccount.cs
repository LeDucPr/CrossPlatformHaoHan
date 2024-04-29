namespace ApiTruyenLau.Objects.Interfaces.Users
{
	public interface IAccount
	{
		public string UserName { get; set; }
		public string Token { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime LastLoginDate { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
