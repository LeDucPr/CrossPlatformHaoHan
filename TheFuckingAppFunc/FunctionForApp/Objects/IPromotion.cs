namespace TheFuckingAppFunc.FunctionForApp.Objects
{
	public interface IPromotion
	{
		public double ReducedPrice { get; set; }
		public double Percent { get; set; }
		public string Description { get; set; }
	}

	public class Voucher : IPromotion
	{
		private double _reducedPrice;
		private double _percent;
		private string _description;
		public double ReducedPrice { get { return _reducedPrice; } set { _reducedPrice = value; } }
		public double Percent { get { return _percent; } set { _percent = value; } }
		public string Description { get { return _description; } set { _description = value; } }
	}

	public enum EVip
	{
		Type_1 = 1,
		Type_2 = 2,
		Type_3 = 3,
	}

	public class Vip : IPromotion
	{
		private double _reducedPrice;
		private EVip _vipType;
		private double _percent;
		private string _description;
		public double ReducedPrice { get { return _reducedPrice; } set { _reducedPrice = value; } }
		public EVip VipType { get { return _vipType; } set { _vipType = value; } }
		public double Percent { get { return _percent; } set { _percent = value; } }
		public string Description { get { return _description; } set { _description = value; } }
	}
}
