using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaving.DataNavigations.Storages.YamlComponents
{

	public class ObjectDataTree : TreeComponents
	{
		// khi kế thừa thì để yêu cầu các dữ liệu 2 chiều để dưới dạng Properties {get; set; }
		// vì thế chỉ lấy kiểu Properties và không lấy kiểu Fields
		public Type componentType = null!;
		public ObjectDataTree()
		{
			this.Children = new List<TreeComponents>();
		}
		public ObjectDataTree(Type type) : this()
		{
			this.PropertyName = type.Name;
			this.componentType = type;
		}
		public void ObjectToTree()
		{
			this.ObjectToTree(this.componentType);
		}
		private void ObjectToTree(Type type/*= this.GetType()*/)
		{
			var typeNameProperty = type.GetProperties();
			typeNameProperty.ToList().ForEach(x =>
			{
				this.Children.Create().Add(new TreeComponents() { PropertyName = type.Name });
				if (this.IsPrimitive(x.PropertyType))
					this.ObjectToTree(x.PropertyType);
			});
		}
		private bool IsPrimitive(Type type)
		{
			return type.IsPrimitive || type.IsValueType || (type == typeof(string));
		}
	}
}
