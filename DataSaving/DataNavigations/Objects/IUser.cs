using System.Security.Cryptography;
using System.Text;

namespace DataSaving.DataNavigations.Objects
{
	public enum EAccount
	{
		Manager = 0,
		Employee = 1,
		Client = 2,
	}

	public class Account
	{
		public string UserName { get { return userName; } set { userName = value; } }
		public string Password { get { return password; } set { password = value; } }
		public string Email { get { return email; } set { email = value; } }
		public string FirstName { get { return firstName; } set { firstName = value; } }
		public string LastName { get { return lastName; } set { lastName = value; } }
		public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
		private string userName = string.Empty;
		private string password = string.Empty;
		private string email = string.Empty;
		private string firstName = string.Empty;
		private string lastName = string.Empty;
		private string phoneNumber = string.Empty;
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
	}

	#region Employee
	/// <summary>
	/// Doanh thu có tổng là bao nhiêu, tính theo thời điểm đầu và cuối 
	/// </summary>
	public class Revenue
	{
		public DateTime FirstDateTime;
		public DateTime LastDateTime;
		public double Money;
	}
	/// <summary>
	/// Tiền lương cho ngày đi làm 
	/// </summary>
	public class Salary
	{
		public DateTime FirstDateTime;
		public DateTime LastDateTime;
		public double Money;
		public int WorkingDays;
	}
	/// <summary>
	/// Tiền thưởng với nội dung và trong thời gian nào 
	/// </summary>
	public class Bonus
	{
		public DateTime FirstDateTime;
		public DateTime LastDateTime;
		public string BonusContent;
		public double Money;
	}
	/// <summary>
	/// TIền phạt vói nội dung và trong thời gian nào 
	/// </summary>
	public class Punishment
	{
		public DateTime FirstDateTime;
		public DateTime LastDateTime;
		public string PunishContent;
		public double Money;
	}
	#endregion Employee

	#region Client 
	public class Spend
	{
		public DateTime FirstDateTime;
		public DateTime LastDateTime;
		public double Money;
	}
	#endregion Client 

	public interface IUser
	{
		public Account Account { get; set; }
	}
	public interface IManager
	{

	}
	public interface IEmployee
	{
		public List<Revenue> Revenues { get; set; }
		public List<Salary> Salaries { get; set; }
		public List<Bonus> Bonus { get; set; }
		public List<Punishment> Punishments { get; set; }
	}
	public interface IClient
	{
		public Account Account { get; set; }
		public List<Spend> Spends { get; set; }
		public List<string> BillCodes { get; set; }
	}

	public partial class Manager : IUser, IManager
	{
		private Account _account;
		private string _lastLoginDate;
		public Account Account { get { return _account; } set { _account = value; } }
		public string LastLoginDate { get { return _lastLoginDate; } set { _lastLoginDate = value; } }
	}

	public partial class Employee : IUser, IEmployee
	{
		private Account _account;
		private List<Revenue> _revenues;
		private List<Salary> _salaries;
		private List<Bonus> _bonus;
		private List<Punishment> _punishments;
		public Account Account { get { return _account; } set { _account = value; } }
		public List<Revenue> Revenues { get { return _revenues; } set { _revenues = value; } }
		public List<Salary> Salaries { get { return _salaries; } set { _salaries = value; } }
		public List<Bonus> Bonus { get { return _bonus; } set { _bonus = value; } }
		public List<Punishment> Punishments { get { return _punishments; } set { _punishments = value; } }
	}

	public partial class Client : IUser, IClient
	{
		private Account _account;
		private List<Spend> _spends;
		private double _point;
		private List<string> _billCodes;
		private List<string> _currentShoppingCart; // chứa tên tất cả Item tồn tại trong đó, mua xong thì loại ra 
		private string _borrowBook; //phần này theo yêu cầu thì chỉ cho mượn 1 quyển sách, khi nào nó trả thì cho mượn tiếp 
		private bool _isPaid;
		public Account Account { get { return _account; } set { _account = value; } }
		public List<Spend> Spends { get { return _spends; } set { _spends = value; } }
		public double Point { get { return _point; } set { _point = value; } }
		public List<string> BillCodes { get { return _billCodes; } set { _billCodes = value; } }
		public List<string> CurrentShoppingCart { get { return _currentShoppingCart; } set { _currentShoppingCart = value; } }
		public string BorrowBook { get { return _borrowBook; } set { _borrowBook = value; } }
		public bool IsPaid { get { return _isPaid; } set { _isPaid = value; } }
	}
}
