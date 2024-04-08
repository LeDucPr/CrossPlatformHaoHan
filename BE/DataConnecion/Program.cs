using DataConnecion.MongoDB;
using MongoDB.Bson;

namespace DataConnecion
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			// sử dụng MongoDBEntity
			//var mongo = new MongoDBEntity<object>("mongodb://localhost:27017", "TruyenLau", "TruyenLauCollection");
			var mongo = new MongoDBEntity<object>("mongodb://localhost:27017", "Client", "Client");
			//var document = new BsonDocument
			//{
			//	{ "title", "MongoDB" },
			//	{ "author", "TruyenLau" },
			//	{ "year", 2021 }
			//};
			//var document2 = new BsonDocument
			//{
			//	{ "title", "SQL" },
			//	{ "author", "HaoHan" },
			//	{ "year", 2022 }
			//};
			//var document3 = new BsonDocument
			//{
			//	{ "title", "NoSQL" },
			//	{ "author", "HaoHan" },
			//	{ "year", 2023 }
			//};
			//mongo.AddMongoDBEntity(document);
			//mongo.DeleteObjects(document);
			//mongo.DropCollection(); 

			//Employee employee1 = new Employee();
			//employee1.Id = "1";
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
			//employee2.Id = "2";
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

			//// client 
			//Client client1 = new Client();
			//client1.Id = "3";
			//client1.Account = new Account() { UserName = "client1", Password = "Thằng rank con" };
			//client1.Spends = new List<Spend>()
			//{
			//	new Spend() { FirstDateTime=DateTime.Now.AddDays(-7), LastDateTime=DateTime.Now, Money=2000}
			//};

			//var a = mongo.FindObjects(new Dictionary<string, string>(){ { nameof(employee1.Id), employee1.Id } });
			//Console.WriteLine(a.Count); 

			//mongo.AddMongoDBEntity(employee1);
			//mongo.AddMongoDBEntity(employee2);
			//mongo.AddMongoDBEntity(client1);
			//mongo.Indexs(nameof(employee1.Id), nameof(employee1.Account));
			//mongo.DeleteObjects(nameof(client1.Id), client1.Id);

			//mongo.DropCollection();
			//var a = await mongo.FindObjects(new Dictionary<string, string>()
			//{
			//	{ "Account.UserName", "Conchongu" },
			//	{ "Account.Password", "nvjfrn2U@fwS" }
			//});
			//Console.WriteLine(a.Count);
		}
	}
}