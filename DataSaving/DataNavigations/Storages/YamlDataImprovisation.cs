using DataSaving.DataNavigations.Storages.YamlComponents;
using YamlDotNet.RepresentationModel;

namespace DataSaving.DataNavigations.Storages
{
	public class YamlDataImprovisation<T> : YamlData<T> where T : YamlObject, new()// new() cho khả năng khởi tạo mới 
	{
		public YamlDataImprovisation(string yamlPath) : base(yamlPath)
		{
			this.ReadAll();
			this.CreateYamlDocuments();
		}

		#region Các thành phần chung cho Yaml 
		#region Khởi tạo Document
		private void CreateYamlDocument(EFirstComponentConfig efirst)
		{
			var newNode = new YamlScalarNode(efirst.ToString()); // YamlNode mới 
			//var newDoc = new YamlDocument(newNode);
			//this.yamlStream.Documents.Add(newDoc); //[this.yamlStream.Documents.Count()] = newDoc;
			var newDoc = new YamlDocument(new YamlMappingNode { { newNode, new YamlScalarNode("") } });
			this.yamlStream.Documents.Add(newDoc);
		}
		private void CreateYamlDocuments()
		{
			if (!File.Exists(this.yamlPath))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(this.yamlPath)!);
				File.Create(this.yamlPath).Close();
				((EFirstComponentConfig[])Enum.GetValues(typeof(EFirstComponentConfig))).ToList()
					.ForEach(efirst => this.CreateYamlDocument(efirst));
				using (var writer = new StreamWriter(this.yamlPath)) { this.yamlStream.Save(writer); }
			}
		}
		#endregion Khởi tạo Document
		#region Thay đổi dữ liệu trong Document
		/// <summary>
		/// Cái này bao gồm cả Add và Update 
		/// </summary>
		/// <param name="efirst"></param>
		/// <param name="obj"></param>
		/// <exception cref="Exception"></exception>
		public bool UpdateToYamlDocument(EFirstComponentConfig efirst, YamlObject obj)
		{
			var docNode = FindYamlDocument(efirst);
			if (docNode != null)
			{
				string TkeyName = obj.ObjectNameKey;
				var targetEntry = docNode.Children
					.FirstOrDefault(entry => ((YamlScalarNode)entry.Key).Value == TkeyName);
				Console.WriteLine(targetEntry.Key);
				if (targetEntry.Key != null)
					docNode.Children[targetEntry.Key] = serializer.Serialize(obj);
				else
					docNode.Add(TkeyName, serializer.Serialize(obj));
				using (var writer = new StreamWriter(this.yamlPath)) { this.yamlStream.Save(writer); }
				return true;
			}
			return false;
		}
		private YamlMappingNode? FindYamlDocument(EFirstComponentConfig efirst)
		{
			string efirstName = efirst.ToString();
			var findNode = new YamlScalarNode(efirstName);

			// Lấy tất cả YamlMappingNodes từ yamlStream
			var mappingNodes = this.yamlStream.Documents
				.Where(r => r.RootNode is YamlMappingNode)
				.Select(r => (YamlMappingNode)r.RootNode)
				.ToList();

			// Tìm YamlMappingNode chứa key
			var targetNode = mappingNodes.FirstOrDefault(r => r.Children.ContainsKey(findNode));

			return targetNode;
		}
		private YamlMappingNode? FindYamlScalarNode(EFirstComponentConfig efirst, string scalarKey)
		{
			var docNode = FindYamlDocument(efirst);
			if (docNode != null)
			{
				var findNode = new YamlScalarNode(scalarKey);
				var a = docNode.Children.FirstOrDefault(r => r.Key == findNode);
				return a.Key != null ? (YamlMappingNode)a.Value : null;
			}
			return null;
		}
		#endregion Tìm kiếm
		#endregion Các thành phần chung cho Yaml 

		/// <summary>
		/// thay dổi cấu hình 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool TreeConfig(T obj)
		{
			ObjectDataTree tree = new ObjectDataTree(obj.GetType());
			tree.ObjectToTree();
			return this.UpdateToYamlDocument(EFirstComponentConfig.Tree, tree);
		}

		public bool Datahandling(T obj)
		{
			return this.UpdateToYamlDocument(EFirstComponentConfig.Data, obj);
		}
	}
}

namespace DataSaving.DataNavigations.Objects
{
	public partial class Client : YamlObject
	{
	}
	public partial class Employee : YamlObject
	{
	}
	public partial class Manager : YamlObject
	{
	}
}