using DataSaving.DataNavigations.Storages;
using DataSaving.DataNavigations.Objects;

namespace DataSaving
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Employee employee1 = new Employee();
			employee1.Account = new Account() { UserName = "employee1", Password = "password1" };
			employee1.Revenues = new List<Revenue>()
			{
				new Revenue() { FirstDateTime = DateTime.Now.AddDays(-7), LastDateTime = DateTime.Now, Money = 1000 }
			};
			employee1.Salaries = new List<Salary>()
			{
				new Salary() { FirstDateTime = DateTime.Now.AddDays(-30), LastDateTime = DateTime.Now, Money = 2000, WorkingDays = 22 }
			};
			employee1.Bonus = new List<Bonus>()
			{
				new Bonus() { FirstDateTime = DateTime.Now.AddDays(-30), LastDateTime = DateTime.Now, BonusContent = "Thưởng tháng", Money = 500 }
			};
			employee1.Punishments = new List<Punishment>()
			{
				new Punishment() { FirstDateTime = DateTime.Now.AddDays(-30), LastDateTime = DateTime.Now, PunishContent = "Phạt muộn giờ", Money = 100 }
			};

			// Tạo một đối tượng Employee khác
			Employee employee2 = new Employee();
			employee2.Account = new Account() { UserName = "employee2", Password = "password2" };
			employee2.Revenues = new List<Revenue>()
			{
				new Revenue() { FirstDateTime=DateTime.Now.AddDays(-7), LastDateTime=DateTime.Now, Money=2000}
			};
			employee2.Salaries = new List<Salary>()
			{
				new Salary(){FirstDateTime=DateTime.Now.AddDays(-30),LastDateTime=DateTime.Now,Money=3000,WorkingDays=23}
			};
			employee2.Bonus = new List<Bonus>()
			{
				new Bonus(){FirstDateTime=DateTime.Now.AddDays(-30),LastDateTime=DateTime.Now,BonusContent="Thưởng tháng",Money=600}
			};
			employee2.Punishments = new List<Punishment>()
			{
				new Punishment(){FirstDateTime=DateTime.Now.AddDays(-30),LastDateTime=DateTime.Now,PunishContent="Phạt muộn giờ",Money=200}
			};

			// client 
			Client client1 = new Client();
			client1.Account = new Account() { UserName = "client1", Password = "Thằng rank con" };
			client1.Spends = new List<Spend>()
			{
				new Spend() { FirstDateTime=DateTime.Now.AddDays(-7), LastDateTime=DateTime.Now, Money=2000}
			};

			YamlDataImprovisation<Client> yamlDataImprovisation = new YamlDataImprovisation<Client>("Data/test.yaml");
			Console.WriteLine(yamlDataImprovisation.Datahandling(client1));
			Console.WriteLine(yamlDataImprovisation.TreeConfig(client1));
		}
	}
}
