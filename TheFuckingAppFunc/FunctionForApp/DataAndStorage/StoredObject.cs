using FunctionForApp.DataAndStorage;
using YamlDotNet.RepresentationModel;

namespace TheFuckingAppFunc.FunctionForApp.DataAndStorage
{
	/// <summary>
	/// Hiện tại chưa có liên quan gì tới bảo mật 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class StoredObject<T>
	{
		private string objectDataFilePath;
		private string objectDataFileConfigurationPath;
		private JsonConfiguration<T> objectDataConfiguration;
		private YamlData<T> objectData;
		public JsonConfiguration<T> Congfiguration { get { return this.objectDataConfiguration; } private set { this.objectDataConfiguration = value; } }
		public YamlData<T> Data { get { return this.objectData; } private set { this.objectData = value; } }
		public StoredObject(string objectDataFilePath, string objectDataFileConfigurationPath)
		{
			this.objectDataFilePath = objectDataFilePath;
			this.objectDataFileConfigurationPath = objectDataFileConfigurationPath;
			this.objectData = new YamlData<T>(this.objectDataFilePath);
			this.objectDataConfiguration = new JsonConfiguration<T>(this.objectDataFileConfigurationPath);
			this.objectData.ReadAll();
		}
		public Dictionary<Type, object>? TryParseObject(string keyOfData)
		{
			foreach (Type type in this.objectDataConfiguration.TypeOfObjects)
			{
				try
				{
					object? partObject = this.objectData.GetPart(keyOfData, type);
					if (partObject != null)
						return new Dictionary<Type, object> { { type, partObject } };
				}
				catch { }
			}
			return null; // -> ref (type)(obj)
		}
		public Dictionary<Type, List<object>>? TryParseObjects()
		{
			var objectsByType = new Dictionary<Type, List<object>>();
			var objTypes = this.objectDataConfiguration.TypeOfObjects;
			//foreach (var objType in objTypes)
			//    if (objType != null)
			//    {
			//        try
			//        {
			//            List<object>? objects = this.objectData.GetAll(objType);
			//            if (objects != null && objects.Count > 0)
			//                objectsByType[objType] = objects;
			//        }
			//        catch { }
			//    }
			List<YamlScalarNode> ysNodes = this.objectData.GetAllKeys(); // đọc tất cả các đói tượng và Parse lần lượt 
			foreach (var objType in objTypes)
				foreach (var ysNode in ysNodes)
				{
					try
					{
						if (!objectsByType.ContainsKey(objType))
							objectsByType.Add(objType, new List<object>());
						object? partObject = this.objectData.GetPart(ysNode, objType);
						if (partObject != null)
							objectsByType[objType].Add(partObject);
					}
					catch { }
				}
			return objectsByType.Count > 0 ? objectsByType : null;
		}
	}
}
