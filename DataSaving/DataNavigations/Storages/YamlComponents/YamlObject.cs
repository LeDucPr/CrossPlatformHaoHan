using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaving.DataNavigations.Storages.YamlComponents
{
	/// <summary>
	/// các lớp muốn ghi vào file yaml phải kế thừa lớp này
	/// </summary>
	public abstract class YamlObject
	{
		public string ObjectNameKey { get; set; } = null!; 
	}
}
