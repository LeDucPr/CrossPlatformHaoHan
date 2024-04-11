using System.Collections.Generic;
using ApiTruyenLau.Objects.Interfaces.Users;
using ApiTruyenLau.Objects.Interfaces.Items;

namespace ApiTruyenLau.Objects.Generics.Users
{
	public partial class Client : IClient//, IUser
	{
		private string _id = null!;
		private /*I*/Account _account = null!;
		private List<string> _readedId = null!;
		private List<IBook> _readed = null!;
		private List<IBook> _viewed = null!;
		private List<IBook> _recommend = null!;
		private List<ViewTime> _viewTimes = null!;
		public /*I*/Account Account { get { return _account; } set { _account = value; } }
		public string Id { get; set; } = null!;
		public List<string> ReadedId { get { return _readedId; } set { _readedId = value; } }
		// Phần này là tài nguyên nhúng thẳng, giúp tìm truyện nhanh hơn, không cần quan tới việc tìm truyện, làm tăng tốc độ gợi ý 
		public List<IBook> Readed { get { return _readed; } set { _readed = value; } }  // id truyện đã đọc (được một thời gian)
		public List<IBook> Viewed { get { return _viewed; } set { _viewed = value; } } // id truyện đã xem (chưa đọc hoặc lướt nhanh)
		public List<ViewTime> ViewTimes { get { return _viewTimes; } set { _viewTimes = value; } }
		public List<IBook> Recommend { get { return _recommend; } set { _recommend = value; } }
	}
}
