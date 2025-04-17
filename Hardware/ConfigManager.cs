using Hardware.Models;
using System.Text.Json;

namespace Hardware
{
	internal class ConfigManager
	{
		private Config? config;
		private readonly string filename = "settings.cfg";
		private readonly string alfabet = "abcdefghijklmnopqrstuvwxyz";
		private readonly string digits = "0123456789";
		private readonly int key = 9;

		private void LoadConfig()
		{
			if (!File.Exists(filename))
			{
				InitDefaultConfig();
				return;
			}
			try
			{
				string jsonString = File.ReadAllText(filename);
				jsonString = Decrypt(jsonString);
				config = JsonSerializer.Deserialize<Config>(jsonString);
			}
			catch
			{
				InitDefaultConfig();
			}
		}

		private void SaveConfig()
		{
			string jsonString = JsonSerializer.Serialize(config);
			jsonString = Encrypt(jsonString);
			File.WriteAllText(filename, jsonString);
		}

		public Config GetConfig()
		{
			LoadConfig();
			return config;
		}

		public void SetConfig(string server, string user, string password, string database)
		{
			config = new()
			{
				Server = server,
				User = user,
				Password = password,
				Database = database
			};
			SaveConfig();
		}

		private void InitDefaultConfig()
		{
			config = new()
			{
				Server = "localhost",
				User = "user",
				Password = "password",
				Database = "database"
			};
			SaveConfig();
		}

		private string CodeEncode(string text, int key)
		{
			string fullAlfabet = alfabet + alfabet.ToUpper() + digits;
			int lettersCount = fullAlfabet.Length;
			string result = "";
			foreach (var c in text)
			{
				int index = fullAlfabet.IndexOf(c);
				if (index < 0)
					result += c.ToString();
				else
				{
					int codeIndex = (lettersCount + index + key) % lettersCount;
					result += fullAlfabet[codeIndex];
				}
			}
			return result;
		}

		private string Encrypt(string text) => CodeEncode(text, key);
		private string Decrypt(string text) => CodeEncode(text, -key);
	}
}
