using System.Collections.Generic;
using ApiTruyenLau.Objects.Interfaces.Users;
using ApiTruyenLau.Objects.Interfaces.Items;
using ApiTruyenLau.Objects.Generics.Users;

namespace ApiTruyenLau.Objects.Converters.Users
{
	public class ClientReadedCvt
	{
		public string Id {get; set; } = null!;
		// id sách đã đọc 
		public List<string> ReadedId { get; set; } = null!; 
	}
	public static class CientReadedCvtxtension
	{
		public static ClientReadedCvt ToClientReaded(this Client client)
		{
			ClientReadedCvt crc = new ClientReadedCvt();
			crc.Id = client.Id;
			crc.ReadedId = client.ReadedId ?? new List<string>(){ };
			return crc;
		}
	}
}
