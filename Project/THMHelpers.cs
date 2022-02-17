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

		public static string GetOfficialName(string gameName)
		{
			Dictionary<string, string> gamesMap = new Dictionary<string, string>()
			{
				{"Client", "英雄联盟"},
				{"Phasmophobia", "恐鬼症"},
			};
			return gamesMap.ContainsKey(gameName) ? gamesMap[gameName] : gameName;
		}
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
			string path = "";
			OpenFileDialog fileSelector = new OpenFileDialog();
			fileSelector.Filter = (filter != "") ? string.Format($"{filter}文件(*.{filter})|*.{filter}") : "";
			if (fileSelector.ShowDialog() == DialogResult.OK)
			{
				path = fileSelector.FileName;
			}
			else if (fileSelector.ShowDialog() == DialogResult.Cancel)
			{
				path = "";
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

		public static string CreateJson<T>(string path, string jsonName, ref T dataObject)
		{
			string json = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
			string fullPath = string.Format($@"{path}\{jsonName}.json");
			FileStream file = File.Create(fullPath);
			file.Close();
			File.WriteAllText(fullPath, json);
			return fullPath;
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
	}
}