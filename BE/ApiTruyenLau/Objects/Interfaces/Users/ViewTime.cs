namespace ApiTruyenLau.Objects.Interfaces.Users
{
	public interface ViewTime
	{
		public string BookGenre { get; set; }
		public DateTime FirstDateTime { get; set; }
		public TimeSpan Time { get; set; }
	}
}
