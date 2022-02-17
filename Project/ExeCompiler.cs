using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using TheHangedManHelper;

public static class TheHangedManCompiler
{
	static List<string[]> codesList = new List<string[]>()
	{
		new[]
		{
			@"
using System.Diagnostics;
public static class Program
{
	public static void Main()
	{
		Process.Start(""https://www.bilibili.com/video/BV1GJ411x7h7?share_source=copy_web"");
	}
}
"
		},
		new[]
		{
			@"
using System.Diagnostics;
public static class Program
{
	public static void Main()
	{
		Process.Start(""https://www.icourse163.org/"");
	}
}
"
		}
	};

	public static string Compile(string targetName)
	{
		var provider = CodeDomProvider.CreateProvider("CSharp");

		var outPath = string.Format($@"{THMBaseData.BasePath()}\{targetName}.exe");
		var cp = new CompilerParameters()
		{
			GenerateExecutable = true,
			OutputAssembly = outPath,
			GenerateInMemory = false,
			TreatWarningsAsErrors = false
		};
		cp.ReferencedAssemblies.Add("System.dll");
		Random rnd = new Random();
		var results = provider.CompileAssemblyFromSource(cp, codesList[rnd.Next(codesList.Count)]);
		return outPath;
	}
}
