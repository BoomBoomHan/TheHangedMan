using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
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

	float buttonFontSize;

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

	public void AddGameInListView(GameData gameData)
	{
		ListViewItem item = new ListViewItem((1 + GamesListView.Items.Count).ToString());
		item.SubItems.Add(gameData.GameName);
		item.SubItems.Add(gameData.OriginPath);
		GamesListView.Items.Add(item);
	}

	private void MainFormLoad(object sender, EventArgs e)
	{
		buttonFontSize = AddGameButton.Font.Size;
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

		AddGameButton.Enabled = false;

		client = new Client(this);
		foreach (GameData gameData in client.Games)
		{
			AddGameInListView(gameData);
		}
		//SetLabelText(client.Games.Count.ToString());
	}

	private void SelectGame_Click(object sender, EventArgs e)
	{
		Path.Text = THMProcessHelper.SelectFile("exe");
	}

	private void OnTestClick(object sender, EventArgs e)
	{
		bool success = (Path.Text != THMBaseData.NullPath());
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
		string path = Path.Text;
		if (path == THMBaseData.NullPath())
		{
			return;
		}
		GameData data = new GameData(path);
		GameName.Text = THMAdvancedHelper.GetOfficialName(ref data);

		if (THMProcessHelper.IsValidExePath(path))
		{
			AddGameButton.Enabled = true;
		}
	}

	private void GamesListView_SelectedIndexChanged(object sender, EventArgs e)
	{
		int index = int.Parse(GamesListView.FocusedItem.Text) - 1;
		RecoverButton.Enabled = true;
		RecoverButton.Font = new Font(RecoverButton.Font.Name, buttonFontSize / 2);
		RecoverButton.Text = string.Format($"恢复\n{client.Games[index].GameName}");
		
	}

	private void RecoverButton_Click(object sender, EventArgs e)
	{
		Process.Start("https://www.bilibili.com/video/BV1GJ411x7h7?share_source=copy_web");
		Thread.Sleep(1000);
		SetLabelText("你觉得我会那么好心让你恢复吗？？？？");
	}
}