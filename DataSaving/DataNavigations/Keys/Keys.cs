using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaving.DataNavigations.Keys
{
	public class PKey<T> : IKey<T>, IPKey<T>
	{
		public required T Value { get; set; }
	}
	public class FKey<T> : IKey<T>, IFKey<T>
	{
		public required T Value { get; set; }
	}
}
