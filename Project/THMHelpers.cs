using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

/// <summary>
/// "倒吊人"函数库
/// </summary>
namespace TheHangedManHelper
{
	/// <summary>
	/// 保存了所有"倒吊人"程序的基本数据库
	/// </summary>
	public static class THMBaseData
	{
		/// <summary>
		/// 程序保存被替换的exe文件根目录
		/// </summary>
		/// <returns>"Datas"目录名称</returns>
		public static string BasePath() => @"Datas\";

		/// <summary>
		/// 每一个被替换的游戏文件Config配置文件名
		/// </summary>
		/// <returns>"Config"文件名称</returns>
		public static string ConfigJsonName() => "Config";

		/// <summary>
		/// 每一个被替换的游戏文件启动程序文件名
		/// </summary>
		/// <returns>"Launcher"文件名称</returns>
		public static string ExeName() => "Launcher";

		/// <summary>
		/// 空路径字符串
		/// </summary>
		/// <returns>"None"</returns>
		public static string NullPath() => "None";

		public static string ScanIgnoreName() => "SCANIGNORE";

		
	}

	/// <summary>
	/// "倒吊人"程序操作库
	/// </summary>
	public static class THMProcessHelper
	{
		public static string FullLauncherPath(string path, string launcher) => string.Format($@"{path}\{launcher}.exe");

		/// <summary>
		/// 获取带有游戏名的二级目录
		/// </summary>
		/// <param name="secondary"></param>
		/// <returns>二级目录名</returns>
		public static string DetailedPath(string secondary) => THMBaseData.BasePath() + secondary;

		/// <summary>
		/// 从游戏名的二级目录直接读取游戏启动程序文件
		/// </summary>
		/// <param name="gameName">游戏名</param>
		/// <returns>二级目录下的启动程序文件，若未找到则返回null</returns>
		public static FileStream LoadBySecondaryPath(string gameName) => new FileStream(DetailedPath(gameName), FileMode.Open);

		/// <summary>
		/// 检查是否存在某个游戏
		/// </summary>
		/// <param name="gameName">要查找的游戏名</param>
		/// <returns>若库中存在这个游戏则返回真，否则返回假</returns>
		public static bool ExistsGame(string gameName) => Directory.Exists(DetailedPath(gameName));

		/// <summary>
		/// 创建一个二级目录，但不在其中添加任何文件
		/// </summary>
		/// <param name="gameName">游戏名</param>
		public static void CreateDetailedPath(string gameName) => Directory.CreateDirectory(DetailedPath(gameName));

		/// <summary>
		/// 移除二级目录中的所有文件，但不删除二级目录
		/// </summary>
		/// <param name="gameName">游戏名</param>
		public static void RemoveFilesInDetailedPath(string gameName) => Directory.GetFiles(DetailedPath(gameName)).ToList().ForEach(File.Delete);

		/// <summary>
		/// 调用一个新的文件选择窗口选择文件。可以选择所有格式文件，也可以只选择一种格式的文件
		/// 如果没有选定是什么格式的文件，则默认选择所有格式的文件
		/// </summary>
		/// <param name="filter">目标文件格式</param>
		/// <returns>文件路径</returns>
		public static string SelectFile(string filter = "")
		{
			string path = THMBaseData.NullPath();
			OpenFileDialog fileSelector = new OpenFileDialog();
			fileSelector.Filter = (filter != "") ? string.Format($"{filter}文件(*.{filter})|*.{filter}") : "";
			if (fileSelector.ShowDialog() == DialogResult.OK)
			{
				path = fileSelector.FileName;
			}
			return path;
		}

		/// <summary>
		/// 调用一个文件夹选择窗口选择文件夹
		/// </summary>
		/// <returns>文件夹路径</returns>
		public static string SelectPath()
		{
			string path = string.Empty;
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				path = folderBrowserDialog.SelectedPath;
			}
			return path;
		}

		/// <summary>
		/// 以一个结构体或类为模板创建一个json文件
		/// </summary>
		/// <typeparam name="T">结构体或类类型</typeparam>
		/// <param name="path">json路径</param>
		/// <param name="jsonName">json文件名</param>
		/// <param name="dataObject">模板类型对象引用</param>
		/// <returns>json的完整路径</returns>
		public static string CreateJson<T>(string path, string jsonName, ref T dataObject)
		{
			string json = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
			string fullPath = string.Format($@"{path}\{jsonName}.json");
			FileStream file = File.Create(fullPath);
			file.Close();
			File.WriteAllText(fullPath, json);
			return fullPath;
		}

		public static bool IsValidExePath(string path)
		{
			return File.Exists(path) && path.Substring(path.Length - 3) == "exe";
		}
	}

	/// <summary>
	/// "倒吊人"程序进阶函数库
	/// </summary>
	public static class THMAdvancedHelper
	{
		/// <summary>
		/// 创建一个游戏插槽，添加一个启动程序和一个配置文件
		/// </summary>
		/// <param name="gameName">游戏名</param>
		/// <param name="originPath">游戏启动程序原始路径</param>
		/// <warning>未完成！！</warning>
		public static void CreateGameSave(string gameName, string originPath)
		{
			if (!THMProcessHelper.ExistsGame(gameName))
			{
				THMProcessHelper.CreateDetailedPath(gameName);
			}
			else
			{
				THMProcessHelper.RemoveFilesInDetailedPath(gameName);
			}
			GameData game = new GameData(gameName, originPath);
			THMProcessHelper.CreateJson<GameData>(THMProcessHelper.DetailedPath(gameName), THMBaseData.ConfigJsonName(), ref game);
			GameFileCopyHandle handle = new GameFileCopyHandle(originPath);
			handle.CopyTo(gameName, THMBaseData.ExeName());
			handle.CloseLauncher();
		}

		/// <summary>
		/// 清空一个游戏插槽，移除该目录和目录内的启动程序和配置文件
		/// </summary>
		/// <param name="gameName">游戏名</param>
		public static void ClearGameSave(string gameName)
		{
			THMProcessHelper.RemoveFilesInDetailedPath(gameName);
			Directory.Delete(THMProcessHelper.DetailedPath(gameName));
		}

		public static void RefreshTextBox(ref Client client,ref TextBox textBox)
		{
			string name = "新游戏";
			int index = 0;
			while (client.Exists(name))
			{
				name = "新游戏";
				index++;
				name += index.ToString();
			}
			textBox.Text = name;
		}
		static List<GameKeywordConfig> configs;
		static THMAdvancedHelper()
		{
			configs = new List<GameKeywordConfig>()
			{
				{new GameKeywordConfig("英雄联盟", new[]{"Client", "LeagueClient"}, "英雄联盟", "LOL", "League of Legends") },
				{new GameKeywordConfig("WeGame", new[]{"wegame"}, "WeGame", "TGP") },
				{new GameKeywordConfig("CSGO", new[]{"csgo"}, "Counter-Strike Global Offensive", "CSGO", "CS:GO") },
				{new GameKeywordConfig("永劫无间", new[]{"NarakaBladepoint"}, "NARAKA BLADEPOINT") },
				{new GameKeywordConfig("原神", new[]{"launcher", "YuanShen"}, "Genshin Impact", "Genshin Impact Game") },
				{new GameKeywordConfig("绝地求生", new[]{"ExecPubg", "TslGame", "TslGame_BE", "TslGame_UC", "TslGame_ZK"}, "PUBG") },
				{new GameKeywordConfig("侠盗猎车手5", new[]{"GTA5", "PlayGTAV", "GTAVLauncher", "GTAVLanguageSelect"}, "Grand Theft Auto V", "GTAV") },
				{new GameKeywordConfig("怪物猎人：世界", new[]{"MonsterHunterWorld"}, "Monster Hunter World") },
				{new GameKeywordConfig("极限竞速：地平线4", new[]{"ForzaHorizon4"}) },
				{new GameKeywordConfig("消逝的光芒2", new[]{"DyingLightGame_x64_rwdi"}, "Dying Light2") },
				{new GameKeywordConfig("APEX英雄", new[]{"r5apex"}, "Apex Legends") },
				{new GameKeywordConfig("糖豆人：终极淘汰赛", new[]{"FallGuys_client_game", "FallGuysGameLauncher"}, "Fall Guys") },
				{new GameKeywordConfig("饥荒联机版", new[]{"dontstarve_steam"}, "Don't Starve Together") },
				{new GameKeywordConfig("恐鬼症", new[]{"Phasmophobia"}) },
				{new GameKeywordConfig("饥荒", new[]{"dontstarve_steam"}, "dont_starve") },
				{new GameKeywordConfig("雀魂麻将", new[]{"Jantama_MahjongSoul"}, "MahjongSoul") },
				{new GameKeywordConfig("盖瑞模组", new[]{"hl2"}, "GarrysMod") },
				{new GameKeywordConfig("城市天际线", new[]{ "Cities_Skylines" }) },
				{new GameKeywordConfig("喵斯快跑", new[]{"MuseDash"}, "Muse Dash") },
				{new GameKeywordConfig("严阵以待", new[]{"ReadyOrNot"}, "Ready Or Not") },
				{new GameKeywordConfig("人渣", new[]{"SCUM_Launcher"}, "SCUM") },
				{new GameKeywordConfig("星露谷物语", new[]{"Stardew Valley"}) },
				{new GameKeywordConfig("泰特瑞拉", new[]{"Terraria"}) },
			};
		}

		public static string GetOfficialName(ref GameData gameData)
		{
			foreach (GameKeywordConfig config in configs)
			{
				if (config.IsA(ref gameData))
				{
					return config.GameName;
				}
			}
			return gameData.ExeName;
		}
	}
}