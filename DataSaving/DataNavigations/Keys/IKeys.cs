using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaving.DataNavigations.Keys
{
	/// <summary>
	/// Key nên là một kiểu dữ liệu nguyên thủy
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IKey<T>
	{
		public T Value { get; set; }
	}
	public interface INonKey<T>
	{
		public T Value { get; set; }
	}
	public interface IPKey<T> { }

	public interface IFKey<T> { }
}
