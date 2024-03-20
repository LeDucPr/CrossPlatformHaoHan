using System.ComponentModel;
using System.Reflection;

namespace TheFuckingAppFunc.FunctionForApp.Objects
{
	public enum EItem
	{
		Book = 0,
		Food = 1,
		Drink = 2
	}
	public interface IItem
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public byte[] ImageOfFirstPage { get; set; }
		public string Keywords { get; set; } // viết tách ra bằng phím space hoặc các phím ngăn cách khác 
	}

	public class Book : IItem
	{
		private string _name;
		private string _description;
		private double _price;
		private string _author;
		private int _pageAmount;
		private string _publisher;
		private string _version;
		private byte[] _imageOfFirstPage;
		private string _publicationDate; // Ngày xuất bản của sách.
		private string _language; // Ngôn ngữ của sách.
		private string _ISBN; // Mã số sách quốc tế(ISBN).
		private string _category; // Thể loại của sách.
		private int _stock; // Số lượng sách còn trong kho.
		private string _keywords; // Danh sách các từ khóa liên quan đến nội dung của sách. 
		public string Name { get { return _name; } set { _name = value; } }
		public string Description { get { return _description; } set { _description = value; } }
		public double Price { get { return _price; } set { _price = value; } }
		public string Author { get { return _author; } set { _author = value; } }
		public int PageAmount { get { return _pageAmount; } set { _pageAmount = value; } }
		public string Publisher { get { return _publisher; } set { _publisher = value; } }
		public string Version { get { return _version; } set { _version = value; } }
		public byte[] ImageOfFirstPage { get { return _imageOfFirstPage; } set { _imageOfFirstPage = value; } }
		public string PublicationDate { get { return _publicationDate; } set { _publicationDate = value; } }
		public string Language { get { return _language; } set { _language = value; } }
		public string ISBN { get { return _ISBN; } set { _ISBN = value; } }
		public string Category { get { return _category; } set { _category = value; } }
		public int Stock { get { return _stock; } set { _stock = value; } }
		public string Keywords { get { return _keywords; } set { _keywords = value; } }
	}


	public class Drink : IItem
	{
		private string _name;
		private string _description;
		private double _price;
		private double _energy;
		private double _realVolume;
		private string _realVolumeUnit;
		private byte[] _imageOfFirstPage;
		private string _brand; // Thương hiệu của đồ uống.
		private string _flavor; // Hương vị của đồ uống.
		private string _ingredients; // Thành phần của đồ uống.
		private string _manufactureDate; // Ngày sản xuất.
		private string _expiryDate; // Ngày hết hạn sử dụng.
		private string _countryOfOrigin; // Quốc gia sản xuất.
		private bool _isAlcoholic; // Đồ uống có chứa cồn hay không.
		private double _alcoholVolume; // Nồng độ cồn (%) nếu là đồ có cồn, mặc định = 0(%)
		private string _category; // loại đồ uống 
		private string _keywords; // từ khóa tìm kiếm đồ uống 

		public string Name { get { return _name; } set { _name = value; } }
		public string Description { get { return _description; } set { _description = value; } }
		public double Price { get { return _price; } set { _price = value; } }
		public double RealVolume { get { return _energy; } set { _energy = value; } }
		public double Energy { get { return _realVolume; } set { _realVolume = value; } }
		public string RealVolumeUnit { get { return _realVolumeUnit; } set { _realVolumeUnit = value; } }
		public byte[] ImageOfFirstPage { get { return _imageOfFirstPage; } set { _imageOfFirstPage = value; } }
		public string Brand { get { return _brand; } set { _brand = value; } }
		public string Flavor { get { return _flavor; } set { _flavor = value; } }
		public string Ingredients { get { return _ingredients; } set { _ingredients = value; } }
		public string ManufactureDate { get { return _manufactureDate; } set { _manufactureDate = value; } }
		public string ExpiryDate { get { return _expiryDate; } set { _expiryDate = value; } }
		public string CountryOfOrigin { get { return _countryOfOrigin; } set { _countryOfOrigin = value; } }
		public bool IsAlcoholic { get { return _isAlcoholic; } set { _isAlcoholic = value; } }
		public double AlcoholVolume { get { return _alcoholVolume; } set { _alcoholVolume = value; } }
		public string Category { get { return _category; } set { _category = value; } }
		public string Keywords { get { return _keywords; } set { _keywords = value; } }
	}

	public class Food : IItem
	{
		private string _name;
		private string _description;
		private double _price;
		private double _energy;
		private double _mass;
		private string _massUnit;
		private byte[] _imageOfFirstPage;
		private string _ingredients; // Thành phần của món ăn.
		private string _cuisine; // Phong cách ẩm thực của món ăn.
		private string _allergens; // Thông tin về các chất gây dị ứng có thể có trong món ăn.
		private string _dietaryRestrictions; // Thông tin về các hạn chế ăn uống,
											 // ví dụ: không gluten, không đường, thích hợp cho người ăn chay, v.v.
		private string _category; // loại đồ uống 
		private string _keywords; // từ khóa tìm kiếm đồ ăn 
		public string Name { get { return _name; } set { _name = value; } }
		public string Description { get { return _description; } set { _description = value; } }
		public double Price { get { return _price; } set { _price = value; } }
		public double Energy { get { return _energy; } set { _energy = value; } }
		public double Mass { get { return _mass; } set { _mass = value; } }
		public string MassUnit { get { return _massUnit; } set { _massUnit = value; } }
		public byte[] ImageOfFirstPage { get { return _imageOfFirstPage; } set { _imageOfFirstPage = value; } }
		public string Ingredients { get { return _ingredients; } set { _ingredients = value; } }
		public string Cuisine { get { return _cuisine; } set { _cuisine = value; } }
		public string Allergens { get { return _allergens; } set { _allergens = value; } }
		public string DietaryRestrictions { get { return _dietaryRestrictions; } set { _dietaryRestrictions = value; } }
		public string Category { get { return _category; } set { _category = value; } }
		public string Keywords { get { return _keywords; } set { _keywords = value; } }
	}

	/// <summary>
	/// Bằng một cách nào đó thì Manager, Employee, Client có thể truy cập vào mấy cái hóa đơn này (chỉ đọc)
	/// Riêng
	/// </summary>
	public class Bill
	{
		private string _id; // số hóa đơn 
		private string _clientAccountUserName; // tên account của khách 
											   // tất nhiên rồi, bạn chỉ cần gọi tên món đồ ra thôi là có thể split nó ra rồi
											   // có thể truy cập trực tiếp từ thằng có kiểu ObjectData<IItem> 
		private Dictionary<string, int> _nameOfItems = new Dictionary<string, int>(); // list đồ đã mua 
		private string _purchaseDate;
		public string ID { get { return _id; } set { _id = value; } }
		public string ClientAccountUserName { get { return _clientAccountUserName; } set { _clientAccountUserName = value; } }
		public Dictionary<string, int> NameOfItems { get { return _nameOfItems; } set { _nameOfItems = value; } }
		public string PurchaseDate { get { return _purchaseDate; } set { _purchaseDate = value; } }
		public static string CreateIdByTime()
		{
			DateTime now = DateTime.Now;
			string id = string.Format("{0:yyyyMMddHHmmssffff}", now).Replace(".", "");
			return id;
		}
	}

	/// <summary>
	/// Giỏ hàng này sẽ ứng với tên khách hàng
	/// Với mỗi lần truy cập như thế thì khách hàng (Client, Cart và các Bill sẽ có một mối liên hệ với nhau)
	/// </summary>
	public class Cart
	{
		private string userName;
		private List<string> billIds;
		private Dictionary<string, int> nameOfBills;
		public string UserName { get { return userName; } set { userName = value; } }
		public Cart() { if (nameOfBills == null) nameOfBills = new Dictionary<string, int>(); }
		public List<string> BillIds { get { return billIds; } set { billIds = value; } }
		public Dictionary<string, int> NameOfBills { get { return nameOfBills; } set { nameOfBills = value; } }
		public void AddToCart(IItem item)
		{
			if (nameOfBills.ContainsKey(item.Name))
				nameOfBills[item.Name]++;
			else
				nameOfBills[item.Name] = 1;
		}
		public void SubToCart(IItem item)
		{
			if (nameOfBills.ContainsKey(item.Name) && nameOfBills[item.Name] > 0)
				nameOfBills[item.Name]--;
		}
	}

	#region Vietsub 
	public enum VietsubBook
	{
		[Description("Tên sách")]
		Name,
		[Description("Mô tả chi tiết")]
		Description,
		[Description("Giá tiền")]
		Price,
		[Description("Tác giả")]
		Author,
		[Description("Số trang")]
		PageAmount,
		[Description("Nhà xuất bản")]
		Publisher,
		[Description("Phiên bản")]
		Version,
		//ImageOfFirstPage, // ảnh thì không tính 
		[Description("Ngày phát hành")]
		PublicationDate,
		[Description("Ngôn ngữ")]
		Language,
		[Description("Mã số sách quốc tế")]
		ISBN,
		[Description("Thể loại")]
		Category,
		[Description("Số lượng còn")]
		Stock,
	}
	//string a = VietsubBook.Price.GetDescription();
	//string enumValueString = "Price";  // Giả sử bạn đã parse được giá trị này từ đâu đó
	//VietsubBook enumValue = (VietsubBook)Enum.Parse(typeof(VietsubBook), enumValueString);
	//string description = enumValue.GetDescription();

	public enum VietsubFood
	{
		[Description("Tên món ăn")]
		Name,
		[Description("Mô tả chi tiết")]
		Description,
		[Description("Giá tiền")]
		Price,
		[Description("Năng lượng cung cấp")]
		Energy,
		[Description("Khối lượng tịnh")]
		Mass,
		[Description("Đơn vị đo khối lượng")]
		MassUnit,
		//ImageOfFirstPage, // ảnh thì không tính 
		[Description("Thành phần")]
		Ingredients,
		[Description("Phong cách ẩm thực")]
		Cuisine,
		[Description("Chất dị ứng có thể có trong món ăn")]
		Allergens,
		[Description("Chế độ ăn")]
		DietaryRestrictions,
	}

	public enum VietsubDrink
	{
		[Description("Tên thức uống")]
		Name,
		[Description("Mô tả chi tiết")]
		Description,
		[Description("Giá tiền")]
		Price,
		[Description("Thể tích thực")]
		RealVolume,
		[Description("Năng lượng")]
		Energy,
		[Description("Đơn vị thể tích thực")]
		RealVolumeUnit,
		//ImageOfFirstPage, // ảnh thì không tính 
		[Description("Thương hiệu")]
		Brand,
		[Description("Hương vị")]
		Flavor,
		[Description("Thành phần")]
		Ingredients,
		[Description("Ngày sản xuất")]
		ManufactureDate,
		[Description("Ngày hết hạn")]
		ExpiryDate,
		[Description("Quốc gia sản xuất")]
		CountryOfOrigin,
		[Description("Chứa nồng độ cồn")]
		IsAlcoholic,
		[Description("Nồng độ cồn")]
		AlcoholVolume,
	}

	public static class EnumExtensions
	{
		public static string GetDescription(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
			return attribute == null ? value.ToString() : attribute.Description;
		}
	}
	#endregion Vietsub 
}
