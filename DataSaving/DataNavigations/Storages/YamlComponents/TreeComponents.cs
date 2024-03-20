using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataSaving.DataNavigations.Storages.YamlComponents
{
	public enum EFirstComponentConfig
	{
		Data,
		Tree,
	}
	public class TreeComponents : YamlObject
	{
		public string Name { get; set; } = null!;
		public List<TreeComponents>? Children; // nếu type là Prime thì không có children
	}

	public static class TreeObjectDataExtension
	{
		public static List<TreeComponents> Create(this List<TreeComponents>? children)
		{
			if (children == null)
				children = new List<TreeComponents>();
			return children;
		}
		public static List<TreeComponents> CreateNewChildren(this TreeComponents tcs)
		{
			tcs.Children = new List<TreeComponents>();
			return tcs.Children;
		}
	}

}
