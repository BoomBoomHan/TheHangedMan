using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

[Serializable]
public struct GameKeywordConfig
{
	[JsonProperty]
	public readonly string GameName;
	[JsonProperty]
	public readonly string[] ExeNameArray;
	[JsonProperty]
	public readonly string[] Keywords;

	[JsonConstructor]
	public GameKeywordConfig(string gameName, string[] exeName, params string[] keywords)
	{
		GameName = gameName;
		ExeNameArray = new string[exeName.Length];
		exeName.CopyTo(ExeNameArray, 0);
		Keywords = new string[keywords.Length + ExeNameArray.Length];
		keywords.CopyTo(Keywords, 0);
		ExeNameArray.CopyTo(Keywords, keywords.Length);
	}

	bool Repeated(string[] another)
	{
		Dictionary<string, int> keywordAppears = new Dictionary<string, int>();
		foreach (string keyword in Keywords)
		{
			if (keywordAppears.ContainsKey(keyword))
			{
				throw new ArgumentException();
			}
			else
			{
				keywordAppears.Add(keyword, 1);
			}
		}
		foreach (string keyword in another)
		{
			if (keywordAppears.ContainsKey(keyword))
			{
				return true;
			}
		}
		return false;
	}

	bool Repeated(string path)
	{
		List<string> split = path.Split('\\').ToList();
		split.RemoveAt(0);
		string last = split[split.Count - 1];
		last = last.Substring(0, last.LastIndexOf('.'));
		split[split.Count - 1] = last;
		return Repeated(split.ToArray());
	}

	public bool IsA(ref GameData gameData)
	{
		return ExeNameArray.Contains(gameData.ExeName) && Repeated(gameData.OriginPath);
	}
}