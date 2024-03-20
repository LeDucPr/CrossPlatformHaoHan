using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataSaving.DataNavigations.Keys
{
	public static class IKeyExtensions
	{
		public static bool IKeyIsValid<T>(this T key) where T : IKey<T>
		{
			if (key.Value.GetType().IsPrimitive) return true;
			return false;
		}

	}
}
