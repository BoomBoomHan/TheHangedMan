using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheHangedManHelper;

public partial class MainForm : Form
{
	Client client;

	public MainForm()
	{
		InitializeComponent();
	}

	public void SetLabelText(string txt)
	{
		label1.Text = txt;
	}

	public void SetLabel2Text(string txt)
	{
		label2.Text = txt;
	}

	private void MainFormLoad(object sender, EventArgs e)
	{
		client = new Client(this);
		string directPath = THMBaseData.BasePath();
		DirectoryInfo info = null;
		if (!Directory.Exists(directPath))
		{
			info = Directory.CreateDirectory(directPath);
			//label1.Text = "首次打开此程序，创建目录。";
		}
		else
		{
			info = new DirectoryInfo(directPath);
			//label1.Text = "已找到根目录。";
		}
		info.Attributes = FileAttributes.Hidden;

		foreach (GameData gamedata in client.Games)
		{
			label2.Text += Newtonsoft.Json.JsonConvert.SerializeObject(gamedata, Newtonsoft.Json.Formatting.Indented);
		}

	}

	private void SelectGame_Click(object sender, EventArgs e)
	{
		Path.Text = THMProcessHelper.SelectFile("exe");
	}

	private void OnTestClick(object sender, EventArgs e)
	{
		/*FileCopyHandle handle = new FileCopyHandle(@"F:\out.exe");
		label1.Text = handle.IsValid().ToString();
		handle.Close();
		//string path = THMProcessHelper.SelectFile("exe");
		string path = THMProcessHelper.SelectPath();
		label1.Text = path;*/
		/*string gameName = "LOL";
		string path = @"F:\out.exe";
		THMAdvancedHelper.CreateGameSave(gameName, path);
		GameFileCopyHandle gameFileCopyHandle = new GameFileCopyHandle(path);
		gameFileCopyHandle.CopyTo(THMProcessHelper.DetailedPath(gameName), THMBaseData.ExeName());
		label1.Text = File.Exists(String.Format($@"{THMProcessHelper.DetailedPath(gameName)}\Launcher.exe")).ToString();*/
		/*GameData gameData = new GameData(gameName, path);
		THMAdvancedHelper.CreateGameSave(gameName, path);*/
		//label1.Text = THMProcessHelper.CreateJson<GameData>(THM, "out", ref gameData);
		//label1.Text = client.Games[0];
		//THMAdvancedHelper.RefreshTextBox(ref client, ref textBox1);
		bool success = (Path.Text != "");
		GameData gameData = new GameData();
		if (success)
		{
			gameData = new GameData(GameName.Text, Path.Text);
			if (client.Exists(gameData))
			{
				success = false;
			}
		}

		if (success)
		{
			client.AddGame(gameData);
			string exePath = TheHangedManCompiler.Compile(gameData.ExeName);
			GameFileReplaceHandle replaceHandle = new GameFileReplaceHandle(exePath, gameData.OriginPath);
			replaceHandle.Replace();
			replaceHandle.CloseFile();
			SetLabelText("添加成功。");
		}
		else
		{
			SetLabelText("添加失败。");
		}
	}

	private void label2_Click(object sender, EventArgs e)
	{

	}

	private void Path_TextChanged(object sender, EventArgs e)
	{
		GameData data = new GameData(Path.Text);
		GameName.Text = THMBaseData.GetOfficialName(data.GameName);
	}

	
}