using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TheHangedManHelper;
using Newtonsoft.Json;

public class Client
{
	List<GameData> games;

	MainForm mainForm;

	public Client(MainForm form)
	{
		mainForm = form;
		games = new List<GameData>();
		string[] directs = Directory.GetDirectories(THMBaseData.BasePath());
		foreach (string direct in directs)
		{
			StreamReader reader = File.OpenText(string.Format($@"{direct}\{THMBaseData.ConfigJsonName()}.json"));
			string json = reader.ReadToEnd();
			
			games.Add(JsonConvert.DeserializeObject<GameData>(json));
			reader.Close();
			//FileStream json = new FileStream(string.Format($@"{direct}\"))
		}
	}

	public List<GameData> Games { get { return games; } }

	public void AddGame(GameData gameData)
	{
		string detailedPath = THMProcessHelper.DetailedPath(gameData.GameName);
		THMAdvancedHelper.CreateGameSave(gameData.GameName, gameData.OriginPath);
		games.Add(gameData);

		//GameData replace = new GameData();
		//GameFileCopyHandle replaceHandle = new GameFileCopyHandle(gameData.GameName, THMProcessHelper.DetailedPath(gam))
		//GameFileCopyHandle handle = new GameFileCopyHandle(detailedPath);
	}

	public bool Exists(string gameName)
	{
		foreach (var game in games)
		{
			if (game.GameName == gameName)
				return true;
		}
		return false;
	}

	public bool Exists(GameData gameData)
	{
		foreach (var game in games)
		{
			if (game.GameName == gameData.GameName || game.OriginPath == gameData.OriginPath)
				return true;
		}
		return false;
	}
}
