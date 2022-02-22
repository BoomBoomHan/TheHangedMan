using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHangedManHelper;

[Serializable]
public sealed class GameConfigCreator
{
	GameData gameData;

	public GameConfigCreator(GameData gd)
	{
		gameData = new GameData(ref gd);
	}

	public void CreateConfig(string targetPath)
	{
		THMProcessHelper.CreateJson(targetPath, THMBaseData.ConfigJsonName(), ref gameData);
	}
}
