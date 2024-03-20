using System.Reflection;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace DataSaving.DataNavigations.Storages
{
	/// <summary>
	/// tạo ra bộ lưu trữ dữ liệu đầu vào là các tài khoản người dùng 
	/// chuyển đổi cả tên đăng nhập và mật khẩu thành hashcode 
	/// lưu trữ dưới dạng file nhẹ .yaml có chức năng thêm sửa xóa 
	/// Tất nhiên là Data thì ở dạng nhiều rồi, nên gần như truyền vào là 1 List 
	/// </summary>
	public abstract class YamlData<T>
	{
		protected string yamlPath = string.Empty;
		protected List<T> objs;
		protected List<object> objsG = null!;
		protected ISerializer serializer;
		protected IDeserializer deserializer;
		protected YamlStream yamlStream;
		public YamlData(string yamlPath)
		{
			this.yamlPath = yamlPath;
			this.objs = new List<T>();
			// Create a serializer and a YAML stream
			this.serializer = new SerializerBuilder().Build();
			this.deserializer = new DeserializerBuilder().Build();
			this.yamlStream = new YamlStream();
		}
		/// <summary>
		/// Đọc tài nguyên từ file đã tồn tại, nên kết hợp với một giao diện Loading 
		/// </summary>
		/// <param name="timeoutMilliseconds"></param>
		/// <exception cref="TimeoutException"></exception>
		public virtual void ReadAll(int timeoutMilliseconds = -1)
		{
			if (File.Exists(this.yamlPath))
			{
				var task = Task.Run(() =>
				{
					using (var reader = new StreamReader(this.yamlPath)) { this.yamlStream.Load(reader); }
				});
				if (timeoutMilliseconds == -1)
					task.Wait();
				else if (!task.Wait(timeoutMilliseconds))
					throw new TimeoutException("Đọc file đã quá thời gian chờ");
			}
		}
		/// <summary>
		/// Thêm dữ liệu, hàm này tác động trực tiếp lên file ghi dữ liệu 
		/// Nên kết hợp với try / catch và Hàm Update ở dưới để tránh xung đột Data khi Key tồn tại
		/// </summary>
		/// <param name="newObjs"></param>
		/// <param name="keyTypeName"></param>
		/// <returns></returns>
		public bool Add(List<T> newObjs, string keyTypeName)
		{
			var typeNameProperty = typeof(T).GetProperty(keyTypeName);
			if (typeNameProperty != null)
			{
				if (File.Exists(this.yamlPath))
				{
					if (this.yamlStream.Documents.Count > 0)
					{
						var rootNode = (YamlMappingNode)this.yamlStream.Documents[0].RootNode; // Get the root node
						foreach (var obj in newObjs) // Append the new data to the root node
						{
							string TkeyName = typeNameProperty.GetValue(obj)?.ToString() ?? string.Empty;
							rootNode.Add(TkeyName, serializer.Serialize(obj));
						}
					}
				}
				else
				{
					var docNode = new YamlMappingNode();
					foreach (var obj in newObjs)
					{
						string TkeyName = typeNameProperty.GetValue(obj)?.ToString() ?? string.Empty;
						docNode.Add(TkeyName, serializer.Serialize(obj));
					}
					this.yamlStream.Add(new YamlDocument(docNode));
				}
				using (var writer = new StreamWriter(this.yamlPath)) { this.yamlStream.Save(writer); }
				if (this.objs.Count != 0)
				{
					objs.AddRange(newObjs); // tương đương với newObjs.ForEach(newObj => this.objs.Add(newObj));
					objsG.AddRange(newObjs.Cast<object>());
				}
				return true; // thêm thành công 
			}
			return false;
		}
		public bool AddSplit(List<T> newObjs, string keyTypeName, string splitObject = ".")
		{
			// Tách chuỗi keyTypeName thành một mảng các chuỗi
			string[] propertyNames = keyTypeName.Split(splitObject);

			if (File.Exists(this.yamlPath))
			{
				if (this.yamlStream.Documents.Count > 0)
				{
					var rootNode = (YamlMappingNode)this.yamlStream.Documents[0].RootNode; // Get the root node
					foreach (var obj in newObjs) // Append the new data to the root node
					{
						// Lấy giá trị của thuộc tính cuối cùng trong chuỗi keyTypeName
						object? value = obj;
						foreach (string propertyName in propertyNames)
						{
							if (value != null)
							{
								PropertyInfo? propertyInfo = value.GetType().GetProperty(propertyName);
								if (propertyInfo != null)
									value = propertyInfo.GetValue(value);
							}
						}
						// Chuyển đổi giá trị thành chuỗi và thêm vào rootNode
						string keyName = value?.ToString() ?? string.Empty;
						rootNode.Add(keyName, serializer.Serialize(obj));
					}
				}
			}
			else
			{
				var docNode = new YamlMappingNode();
				foreach (var obj in newObjs)
				{
					// Lấy giá trị của thuộc tính cuối cùng trong chuỗi keyTypeName
					object? value = obj;
					foreach (string propertyName in propertyNames)
					{
						if (value != null)
						{
							PropertyInfo? propertyInfo = value.GetType().GetProperty(propertyName);
							if (propertyInfo != null)
								value = propertyInfo.GetValue(value);
						}
					}
					// Chuyển đổi giá trị thành chuỗi và thêm vào docNode
					string keyName = value?.ToString() ?? string.Empty;
					docNode.Add(keyName, serializer.Serialize(obj));
				}
				this.yamlStream.Add(new YamlDocument(docNode));
			}
			using (var writer = new StreamWriter(this.yamlPath)) { this.yamlStream.Save(writer); }
			if (this.objs.Count != 0)
			{
				objs.AddRange(newObjs); // tương đương với newObjs.ForEach(newObj => this.objs.Add(newObj));
				objsG.AddRange(newObjs.Cast<object>());
			}
			return true; // thêm thành công 
		}
		/// <summary>
		/// Cái này là khi bạn không sử dụng kiểu dữ liệu kế thừa (tức là đa đối tượng)
		/// </summary>
		/// <param name="keyPart"></param>
		/// <returns></returns>
		public T? GetPart(string keyPart)
		{
			var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
			var targetEntry = rootNode.Children
				.FirstOrDefault(entry => ((YamlScalarNode)entry.Key).Value == keyPart);
			var TData = targetEntry.Value;
			return deserializer.Deserialize<T>(TData.ToString());
		}
		/// <summary>
		/// Cái này là khi bạn sử dụng kiểu dữ liệu đa đối tượng, Parse ra object (common) và Type 
		/// </summary>
		/// <param name="keyPart"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public object? GetPart(string keyPart, Type type)
		{
			var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
			var targetEntry = rootNode.Children
				.FirstOrDefault(entry => ((YamlScalarNode)entry.Key).Value == keyPart);
			var TData = targetEntry.Value;
			return deserializer.Deserialize(TData.ToString(), type);
		}
		/// <summary>
		/// Cái này là khi bạn sử dụng kiểu dữ liệu đa đối tượng, Parse ra object (common) và Type 
		/// Thường được dùng kết hợp với hàm GetAllKeys để lấy ra đối tượng YamlScalarNode 
		/// </summary>
		/// <param name="ysNode"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public object? GetPart(YamlScalarNode ysNode, Type type)
		{
			var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
			var targetEntry = rootNode.Children
				.FirstOrDefault(entry => entry.Key == ysNode);
			var TData = targetEntry.Value;
			return deserializer.Deserialize(TData.ToString(), type);
		}
		/// <summary>
		/// Thường được dùng khi không sử dụng dữ liệu kế thừa
		/// </summary>
		/// <returns></returns>
		public List<T> GetAll()
		{
			if (this.objs.Count == 0)
			{
				this.objs = new List<T>();
				if (this.yamlStream.Documents.Count > 0)
				{
					var newRootNode = (YamlMappingNode)this.yamlStream.Documents[0].RootNode;
					foreach (var entry in newRootNode.Children)
					{
						var TNode = (YamlScalarNode)entry.Key;
						var TData = entry.Value;
						var TObject = this.deserializer.Deserialize<T>(TData.ToString());
						this.objs.Add(TObject);
					}
				}
			}
			return this.objs;
		}
		/// <summary>
		/// Lấy ra tất cả các Node dưới dạng đối tượng
		/// </summary>
		/// <returns></returns>
		public List<YamlScalarNode> GetAllKeys()
		{
			List<YamlScalarNode> keys = new List<YamlScalarNode>();
			if (this.yamlStream.Documents.Count > 0)
			{
				var newRootNode = (YamlMappingNode)this.yamlStream.Documents[0].RootNode;
				foreach (var entry in newRootNode.Children)
				{
					var TNode = (YamlScalarNode)entry.Key;
					keys.Add(TNode);
				}
			}
			return keys;
		}
		/// <summary>
		/// Lấy ra tất cả đối tượng dưới dạng Object, sau đó bạn sẽ ép sang kiểu mà mình muốn
		/// thường áp dụng cho đối tượng kế thừa, tất nhiên không kế thừa thì cũng dùng được nhưng mất công 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public List<object>? GetAll(Type type)
		{
			if (this.objsG == null || this.objsG.Count == 0)
			{
				this.objsG = new List<object>();
				var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
				foreach (var entry in rootNode.Children)
				{
					try
					{
						var TData = entry.Value;
						var TObject = deserializer.Deserialize(TData.ToString(), type);
						if (TObject != null)
							objsG.Add(TObject);
					}
					catch { }
				}
			}
			return objsG;
		}
		/// <summary>
		/// Thay đổi dữ liệu, hàm này tác động trực tiếp lên file ghi dữ liệu 
		/// </summary>
		/// <param name="keyTypeName"></param>
		/// <param name="objUpdate"></param>
		/// <returns></returns>
		public bool Update(string keyTypeName, T objUpdate)
		{
			var typeNameProperty = typeof(T).GetProperty(keyTypeName);
			if (typeNameProperty != null)
			{
				string TkeyName = typeNameProperty.GetValue(objUpdate)?.ToString() ?? string.Empty;
				var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
				var targetEntry = rootNode.Children
					.FirstOrDefault(entry => ((YamlScalarNode)entry.Key).Value == TkeyName);
				rootNode.Children[targetEntry.Key] = this.serializer.Serialize(objUpdate);
				using (var writer = new StreamWriter(this.yamlPath)) { this.yamlStream.Save(writer); }
				return true; // Update thành công 
			}
			return false;
		}
		public bool UpdateKeyName(string keyName, T objUpdate)
		{
			var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
			var targetEntry = rootNode.Children
				.FirstOrDefault(entry => ((YamlScalarNode)entry.Key).Value == keyName);
			if (!targetEntry.Equals(default(KeyValuePair<YamlNode, YamlNode>)))
			{
				rootNode.Children[targetEntry.Key] = this.serializer.Serialize(objUpdate);
				using (var writer = new StreamWriter(this.yamlPath)) { this.yamlStream.Save(writer); }
				return true; // Update thành công 
			}
			return false;
		}
		/// <summary>
		/// Xóa dữ liệu, hàm này tác động trực tiếp lên file ghi dữ liệu
		/// </summary>
		/// <param name="keyTypeName"></param>
		/// <param name="objRemove"></param>
		/// <returns></returns>
		public bool Remove(string keyTypeName, T objRemove)
		{
			var typeNameProperty = typeof(T).GetProperty(keyTypeName);
			if (typeNameProperty != null)
			{
				string TkeyName = typeNameProperty.GetValue(objRemove)?.ToString() ?? string.Empty;
				var rootNode = (YamlMappingNode)this.yamlStream.Documents[0].RootNode;
				var targetEntry = rootNode.Children.FirstOrDefault(entry => ((YamlScalarNode)entry.Key).Value == TkeyName);
				rootNode.Children.Remove(targetEntry.Key);
				using (var writer = new StreamWriter(this.yamlPath)) { this.yamlStream.Save(writer); }
				if (this.objs.Count != 0)
					this.objs.Remove(objRemove);
				return true; // xóa thành công 
			}
			return false;
		}
	}
}
// mô hình chung cho YAMLDOTNET: YamlStream -> YamlDocument -> YamlDocuments(rootNode) -> entry(key: keyPart, (value: value -> object))
// mô hình chung cho lớp bảo mật ngoài mật khẩu (được bổ sung hashCode)
