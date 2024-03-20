using FunctionForApp.DataAndStorage;
using TheFuckingAppFunc.FunctionForApp.DataAndStorage;
using TheFuckingAppFunc.FunctionForApp.Objects;
namespace TheFuckingAppFunc
{

	class Program
	{
		public static void Main(string[] args)
		{
			////string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister", $@"Login");
			////// Create a new list of Account objects
			////List<Account> accounts = new List<Account>();
			////// Add some sample data
			//////accounts.Add(new Account { UserName = "user1", Password = "pass1", Email = "user1@example.com", FirstName = "John", LastName = "Doe", PhoneNumber = "1234567890" });
			//////accounts.Add(new Account { UserName = "user2", Password = "pass2", Email = "user2@example.com", FirstName = "Jane", LastName = "Doe", PhoneNumber = "0987654321" });
			//////accounts.Add(new Account { UserName = "user3", Password = "pass3", Email = "user3@example.com", FirstName = "Jill", LastName = "Doe", PhoneNumber = "0831245679" });
			////ObjectData<Account> od = new ObjectData<Account>(dataFilePath);
			////od.ReadAll();
			////od.Add(accounts, "UserName");
			////List<Account> accs = od.GetAll();
			////foreach (var a in accs)
			////{
			////    Console.WriteLine(a.UserName);
			////    Console.WriteLine(a.Password);
			////    Console.WriteLine(a.Email);
			////    Console.WriteLine(a.FirstName);
			////    Console.WriteLine(a.LastName);
			////    Console.WriteLine(a.PhoneNumber);
			////    Console.WriteLine();
			////}
			////od.Update("UserName", accs[0]);

			//////od.Remove("UserName", accs[0]);

			//string inputString = "How about drink?";
			//inputString = "Chuỗi này chứa tối đa đâu đấy vào cỡ 128 ký tự tối đa";
			//var keys = Security.RSACreateRSAKeys().FirstOrDefault();
			//string publicKey = keys.Key;
			//string privateKey = keys.Value;
			//string encryptString = Security.RSAEncryptString(inputString, publicKey);
			//string decryptString = Security.RSADecryptString(encryptString, privateKey);
			////Console.WriteLine(encryptString);
			////Console.WriteLine(decryptString);


			//string dataFileConfigurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister", $@"Login.json");
			//ObjectDataConfiguration<Account> ob = new ObjectDataConfiguration<Account>(dataFileConfigurationPath);
			////Account acc = new Account() { UserName = "thằng ngu này" };
			////ob.KeyTypeName = nameof(acc.UserName);
			////ob.TypeOfObjects.Add(acc.GetType());
			//Console.WriteLine(ob.KeyTypeName);
			//foreach (var ob_ in ob.TypeOfObjects)
			//    Console.WriteLine(ob_);
			////ob.SerializeJson();
			///




			//string objectDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister", $@"Login.yaml");
			//string objectDataFileConfigurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister", $@"Login.json");
			//StoredObject<Account> so = new StoredObject<Account>(objectDataFilePath, objectDataFileConfigurationPath);
			//object o = new object();
			//var a = so.TryParseObject("user1", ref o);
			//Console.WriteLine(a);


			//Employee employee1 = new Employee();
			//employee1.Account = new Account() { UserName = "employee1", Password = "password1" };
			//employee1.Revenues = new List<Revenue>()
			//{
			//	new Revenue() { FirstDateTime = DateTime.Now.AddDays(-7), LastDateTime = DateTime.Now, Money = 1000 }
			//};
			//employee1.Salaries = new List<Salary>()
			//{
			//	new Salary() { FirstDateTime = DateTime.Now.AddDays(-30), LastDateTime = DateTime.Now, Money = 2000, WorkingDays = 22 }
			//};
			//employee1.Bonus = new List<Bonus>()
			//{
			//	new Bonus() { FirstDateTime = DateTime.Now.AddDays(-30), LastDateTime = DateTime.Now, BonusContent = "Thưởng tháng", Money = 500 }
			//};
			//employee1.Punishments = new List<Punishment>()
			//{
			//	new Punishment() { FirstDateTime = DateTime.Now.AddDays(-30), LastDateTime = DateTime.Now, PunishContent = "Phạt muộn giờ", Money = 100 }
			//};

			//// Tạo một đối tượng Employee khác
			//Employee employee2 = new Employee();
			//employee2.Account = new Account() { UserName = "employee2", Password = "password2" };
			//employee2.Revenues = new List<Revenue>()
			//{
			//	new Revenue() { FirstDateTime=DateTime.Now.AddDays(-7), LastDateTime=DateTime.Now, Money=2000}
			//};
			//employee2.Salaries = new List<Salary>()
			//{
			//	new Salary(){FirstDateTime=DateTime.Now.AddDays(-30),LastDateTime=DateTime.Now,Money=3000,WorkingDays=23}
			//};
			//employee2.Bonus = new List<Bonus>()
			//{
			//	new Bonus(){FirstDateTime=DateTime.Now.AddDays(-30),LastDateTime=DateTime.Now,BonusContent="Thưởng tháng",Money=600}
			//};
			//employee2.Punishments = new List<Punishment>()
			//{
			//	new Punishment(){FirstDateTime=DateTime.Now.AddDays(-30),LastDateTime=DateTime.Now,PunishContent="Phạt muộn giờ",Money=200}
			//};

			//Client client1 = new Client();
			//client1.Account = new Account() { UserName = "client1", Password = "Thằng rank con" };
			//client1.Spends = new List<Spend>()
			//{
			//	new Spend() { FirstDateTime=DateTime.Now.AddDays(-7), LastDateTime=DateTime.Now, Money=2000}
			//};


			//List<IUser> users = new List<IUser>();
			//users.Add(employee1);
			//users.Add(employee2);
			//users.Add(client1);
			//string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister", $@"User.yaml");
			//YamlData<IUser> od = new YamlData<IUser>(dataFilePath);
			//od.ReadAll();
			//od.AddSplit(users, "Account.UserName", ".");


			//string dataFileConfigurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister", $@"User.json");
			//JsonConfiguration<IUser> odc = new JsonConfiguration<IUser>(dataFileConfigurationPath);
			//odc.KeyTypeName = nameof(employee1.Account.UserName);
			//odc.KeyTypeName = nameof(employee2.Account.UserName);
			//odc.KeyTypeName = nameof(client1.Account.UserName);
			//odc.AddToTypeObject(employee1.GetType());
			//odc.AddToTypeObject(employee2.GetType());
			//odc.AddToTypeObject(client1.GetType());
			//Console.WriteLine();
			//Console.WriteLine(odc.KeyTypeName);
			//foreach (var ob_ in odc.TypeOfObjects)
			//	Console.WriteLine(ob_);
			//odc.SerializeJson();


			//string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister", $@"User.yaml");
			//string dataFileConfigurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister", $@"User.json");
			//StoredObject<IUser> storedObject = new StoredObject<IUser>(dataFilePath, dataFileConfigurationPath);
			//var a = storedObject.TryParseObject("client1");
			//Type type = a.First().Key;
			//object obj = a.First().Value;
			//object convertedObject = Convert.ChangeType(obj, type);
			//Console.WriteLine(convertedObject.GetType());


			DataProcessing<IUser> dp = new DataProcessing<IUser>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"LogAndRegister"), "User");
		}
	}

}
