using ApiTruyenLau.Objects.Generics.Items;
using ApiTruyenLau.Objects.Generics.User;
using DataConnecion.MongoObjects;
using MGDBs = DataConnecion.MongoObjects.CommonObjects;


namespace DataConnecion.MongoBDConfigurations
{
	public class Config
	{
		MGDBs db;// = new MGDBs<Client>();
		public Config()
		{
			this.db = new MGDBs();
			this.db.AddMongoDBSrv("mongodb://localhost:27017").AddMongoDBCollections(new Type[] { typeof(Client), typeof(Book) });
		}
	}
}
