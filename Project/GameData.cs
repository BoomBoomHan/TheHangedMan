using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

[Serializable]
public struct GameData
{
	[JsonProperty]
	public readonly string GameName;
	[JsonProperty]
	public readonly string OriginPath;
	[JsonProperty]
	public readonly string ExeName;

	[JsonConstructor]
	public GameData(string gn, string op, string en)
	{
		GameName = gn;
		OriginPath = op;
		ExeName = en;
	}

	public GameData(string gn, string op)
	{
		GameName = gn;
		OriginPath = op;
		int lastSlashLocation = OriginPath.LastIndexOf(@"\");
		int lastDotLocation = OriginPath.LastIndexOf(".");
		ExeName = OriginPath.Substring(lastSlashLocation + 1, lastDotLocation - lastSlashLocation - 1);
	}

	public GameData(string op)
		:this("Any", op)
	{
		GameName = ExeName;
	}

	public GameData(ref GameData another)
	{
		GameName = another.GameName;
		OriginPath = another.OriginPath;
		ExeName = another.ExeName;
	}
}
