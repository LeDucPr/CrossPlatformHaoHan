using System.Collections.Generic;
using ApiTruyenLau.Objects.Interfaces.Users;
using ApiTruyenLau.Objects.Interfaces.Items;

namespace ApiTruyenLau.Objects.Generics.Users
{
	public partial class Client : IUser, IClient
	{
		private string _id = null!;
		private IAccount _account = null!;
		private List<IBook> _readed = null!;
		private List<IBook> _viewed = null!;
		private List<IBook> _recommend = null!;
		private List<ViewTime> _viewTimes = null!;
		public IAccount Account { get { return _account; } set { _account = value; } }
		public string Id { get; set; } = null!;
		public List<IBook> Readed { get { return _readed; } set { _readed = value; } }  // id truyện đã đọc (được một thời gian)
		public List<IBook> Viewed { get { return _viewed; } set { _viewed = value; } } // id truyện đã xem (chưa đọc hoặc lướt nhanh)
		public List<ViewTime> viewTimes { get { return _viewTimes; } set { _viewTimes = value; } }
		public List<IBook> Recommend {get { return _recommend; } set { _recommend = value; } }
	}
}
