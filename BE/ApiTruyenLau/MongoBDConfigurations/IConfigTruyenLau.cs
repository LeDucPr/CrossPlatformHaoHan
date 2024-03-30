namespace ApiTruyenLau.MongoBDConfigurations
{
	// Interface cho các đối tượng khả dụng trong Config TruyenLauApi (Config.cs) 
	public interface IConfigTruyenLau
	{
	}

}


namespace ApiTruyenLau.Objects.Generics.User
{
	// User.cs
	public partial class Client : ApiTruyenLau.MongoBDConfigurations.IConfigTruyenLau { }
}
namespace ApiTruyenLau.Objects.Generics.Items
{
	// Item.cs
	public partial class Item : ApiTruyenLau.MongoBDConfigurations.IConfigTruyenLau { }
}
