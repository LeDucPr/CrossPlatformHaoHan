using FunctionForApp.DataAndStorage;
using TheFuckingAppFunc.FunctionForApp.Objects;

namespace TheFuckingAppFunc.FunctionForApp.DataAndStorage
{
	public class DataProcessing<T> where T : class
	{
		StoredObject<T> storedObject = null!;
		public DataProcessing(string folderPath, string fileName)
		{
			if (string.IsNullOrEmpty(folderPath)) throw new ArgumentNullException(nameof(folderPath));
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
			if (!File.Exists(folderPath))
				Directory.CreateDirectory(folderPath);
			string filePath = Path.Combine(folderPath, fileName);
			string filePathYaml = string.Concat(filePath, ".yaml");
			string filePathJson = string.Concat(filePath, ".json");
			this.storedObject = new StoredObject<T>(filePathYaml, filePathJson);
			this.TryParseObject("client1");
			
		}

		public T TryParseObject(string str)
		{
			var a = this.storedObject.TryParseObject("client1");
			Type type = a.First().Key;
			object obj = a.First().Value;
			object convertedObject = Convert.ChangeType(obj, type);
			return (T)convertedObject;
		}

		/// <summary>
		/// bao gồm cả Add và Update
		/// </summary>
		public void Update()
		{

		}

		public void Delete()
		{

		}
	}
}