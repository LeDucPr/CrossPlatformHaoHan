using Newtonsoft.Json;

namespace TheFuckingAppFunc.FunctionForApp.DataAndStorage
{
	/// <summary>
	/// Cách này thật sự chỉ nên dùng với số lượng hạn chế Type
	/// nếu không thời gian Parse không đảm bảo lắm 
	/// Mặc dù thời gian Parse cho 1 đối tượng cũng không nhiều 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class JsonConfiguration<T>
	{
		private string jsonConfigurationPath = string.Empty;
		private JsonConfiguration_ odc = null!;
		// Tạo ra thằng này để deserializeJson cho dễ 
		private class JsonConfiguration_
		{
			public string _keyTypeName = null!;
			public List<Type> _typeOfObjects = null!;
		}
		private string _keyTypeName;
		private List<Type> _typeOfObjects;
		public string KeyTypeName { get { return this._keyTypeName; } set { this._keyTypeName = value; } }
		public List<Type> TypeOfObjects { get { return this._typeOfObjects; } private set { this._typeOfObjects = value; } }
		public void AddToTypeObject(Type type)
		{
			if (!this._typeOfObjects.Contains(type))
			{
				this._typeOfObjects.Add(type);
				this._typeOfObjects.Distinct().ToList();
			}
		}
		public JsonConfiguration(string objectDataConfigurationPath)
		{
			this.jsonConfigurationPath = objectDataConfigurationPath;
			if (File.Exists(this.jsonConfigurationPath))
				this.DeserializeJson();
			else
				this.odc = new JsonConfiguration_();
			this._keyTypeName = this.odc?._keyTypeName ?? string.Empty;
			this._typeOfObjects = this.odc?._typeOfObjects ?? new List<Type>();
		}
		public void SerializeJson()
		{
			this.odc._keyTypeName = this._keyTypeName ?? string.Empty;
			this.odc._typeOfObjects = this._typeOfObjects ?? new List<Type>();
			string json = JsonConvert.SerializeObject(this.odc);
			File.WriteAllText(this.jsonConfigurationPath, json); // ghi đè lại file nếu đã tồn tại 
		}
		public void DeserializeJson()
		{
			string json = File.ReadAllText(this.jsonConfigurationPath);
			this.odc = JsonConvert.DeserializeObject<JsonConfiguration_>(json) ?? new JsonConfiguration_();
		}
	}
}
