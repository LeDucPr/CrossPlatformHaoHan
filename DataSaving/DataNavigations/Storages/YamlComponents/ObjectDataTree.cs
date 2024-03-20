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
		private Type selfType = null!;
		public ObjectDataTree()
		{
			this.Children = new List<TreeComponents>();
		}
		public ObjectDataTree(Type type) : this()
		{
			this.Name = type.Name;
			this.selfType = type;
		}
		public void ObjectToTree()
		{
			this.ObjectToTree(this.selfType);
		}
		private void ObjectToTree(Type type/*= this.GetType()*/)
		{
			var typeNameProperty = type.GetProperties();
			typeNameProperty.ToList().ForEach(x =>
			{
				this.Children.Create().Add(new TreeComponents() { Name = type.Name });
				if (!(x.PropertyType.IsPrimitive || x.PropertyType == typeof(string)))
					this.ObjectToTree(x.PropertyType);
			});
		}
	}
}
