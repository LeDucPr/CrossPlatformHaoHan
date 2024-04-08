using ApiTruyenLau.Objects.Interfaces.Items;
using ApiTruyenLau.Objects.Interfaces.Users;

namespace ApiTruyenLau.Objects.Interfaces.Users
{
    /// <summary>
    /// Interface cho Client
    /// (người đọc truyện phổ thông)
    /// </summary>
    public interface IClient
    {
        public string Id { get; set; }
        public List<IBook> Readed { get; set; } // id truyện đã đọc (được một thời gian)
        public List<IBook> Viewed { get; set; } // id truyện đã xem (chưa đọc hoặc lướt nhanh)
        public List<ViewTime> ViewTimes { get; set; } // thời gian xem truyện theo thể loại 
        public List<IBook> Recommend { get; set; } // id truyện được gợi ý
    }
}
