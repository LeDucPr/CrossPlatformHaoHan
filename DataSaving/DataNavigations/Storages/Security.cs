using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataSaving.DataNavigations.Storages
{
	public static class Security
	{
		public static string RSAEncryptString(string inputString, string publicKey)
		{
			using (var rsa = new RSACryptoServiceProvider())
			{
				rsa.FromXmlString(publicKey); // Import the RSA key information.
				var inputBytes = Encoding.UTF8.GetBytes(inputString);
				var encryptedBytes = rsa.Encrypt(inputBytes, false);
				return Convert.ToBase64String(encryptedBytes);
			}
		}
		public static string RSADecryptString(string encryptString, string privateKey)
		{
			using (var rsa = new RSACryptoServiceProvider())
			{
				rsa.FromXmlString(privateKey);// Import the RSA key information.
				var inputBytes = Convert.FromBase64String(encryptString);
				var decryptedBytes = rsa.Decrypt(inputBytes, false);
				return Encoding.UTF8.GetString(decryptedBytes);
			}
		}
		public static Dictionary<String, String> RSACreateRSAKeys()
		{
			// trả về cặp publicKey và privateKey 
			var rsa = new RSACryptoServiceProvider();
			String publicKey = rsa.ToXmlString(false);
			String privateKey = rsa.ToXmlString(true);
			return new Dictionary<String, String> { { publicKey, privateKey } };
		}
	}
}
