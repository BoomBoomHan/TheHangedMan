using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using TheHangedManHelper;

public abstract class GameFileHandle
{
	protected string gameName;

	protected string path;

	protected FileStream targetGameLauncher;

	public GameFileHandle(ref GameData gameData)
	{
		gameName = gameData.GameName;
		path = gameData.OriginPath;
		try
		{
			targetGameLauncher = new FileStream(path, FileMode.Open);
		}
		catch (IOException) when (!File.Exists(path))
		{
			MessageBox.Show("打开了一个无效的文件，程序即将退出！", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Application.Exit();
		}
	}

	public GameFileHandle(string originPath)
	{
		path = originPath;
		targetGameLauncher = new FileStream(originPath, FileMode.Open);
	}

	~GameFileHandle()
	{
		CloseFile();
	}

	public bool IsValid()
	{
		return path != null && targetGameLauncher != null;
	}

	public virtual void CloseFile()
	{
		targetGameLauncher?.Close();
	}
}

[Serializable]
public sealed class GameFileCopyHandle : GameFileHandle
{

	public GameFileCopyHandle(GameData gameData)
		: base(ref gameData)
	{
		targetGameLauncher = new FileStream(gameData.OriginPath, FileMode.Open);
	}

	public GameFileCopyHandle(string originPath)
		: base(originPath)
	{
		
	}

	public void CloseLauncher()
	{
		targetGameLauncher.Close();
	}

	public void CopyTo(string gameName, string launcherName)
	{
		FileStream anotherFile = new FileStream(THMProcessHelper.FullLauncherPath(THMProcessHelper.DetailedPath(gameName), launcherName), FileMode.Create);
		targetGameLauncher.CopyTo(anotherFile);
		anotherFile.Close();
	}
}

public sealed class GameFileReplaceHandle : GameFileHandle
{
	FileStream target;

	public GameFileReplaceHandle(GameData originData, string targetPath)
		:base(ref originData)
	{
		target = new FileStream(targetPath, FileMode.Create);
	}

	public GameFileReplaceHandle(string originPath, string targetPath)
		:base(originPath)
	{
		target = new FileStream(targetPath, FileMode.Create);
	}

	public override void CloseFile()
	{
		base.CloseFile();
		target.Close();
	}

	/*public GameFileReplaceHandle(GameData gameData)
	{
		launcher = new FileStream(gameData.OriginPath, FileMode.Open);
	}

	public GameFileReplaceHandle(string originPath)
	{
		launcher = new FileStream(originPath, FileMode.Open);
	}*/

	public void Replace(bool removeDirectory = false)
	{
		Icon originIcon = Icon.ExtractAssociatedIcon(path);
		Icon clone = originIcon.Clone() as Icon;
		targetGameLauncher.CopyTo(target);
		clone.Save(target);
		originIcon.Dispose();
		base.CloseFile();
		File.Delete(path);
		if (removeDirectory && gameName != null)
		{
			THMAdvancedHelper.ClearGameSave(gameName);
		}
	}
}