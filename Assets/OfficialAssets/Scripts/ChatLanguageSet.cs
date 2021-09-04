using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x020000F5 RID: 245
public class ChatLanguageSet
{
	// Token: 0x06000619 RID: 1561 RVA: 0x00027984 File Offset: 0x00025B84
	public void Load()
	{
		string path = Path.Combine(PlatformPaths.persistentDataPath, "languageFilter");
		try
		{
			if (File.Exists(path))
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					while (!streamReader.EndOfStream)
					{
						string[] array = streamReader.ReadLine().Split(new char[]
						{
							','
						});
						try
						{
							this.Languages[array[0]] = uint.Parse(array[1]);
						}
						catch
						{
						}
					}
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x00027A28 File Offset: 0x00025C28
	public void Save()
	{
		try
		{
			string text = Path.Combine(PlatformPaths.persistentDataPath, "languageFilterTemp");
			using (StreamWriter streamWriter = new StreamWriter(text, false))
			{
				foreach (KeyValuePair<string, uint> keyValuePair in this.Languages)
				{
					streamWriter.Write(keyValuePair.Key);
					streamWriter.Write(",");
					streamWriter.WriteLine(keyValuePair.Value);
				}
			}
			string text2 = Path.Combine(PlatformPaths.persistentDataPath, "languageFilter");
			File.Delete(text2);
			File.Move(text, text2);
		}
		catch
		{
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x00027AF8 File Offset: 0x00025CF8
	public string GetString(uint flag)
	{
		foreach (KeyValuePair<string, uint> keyValuePair in this.Languages)
		{
			if (keyValuePair.Value == flag)
			{
				return keyValuePair.Key;
			}
		}
		return "???";
	}

	// Token: 0x040006CA RID: 1738
	public static readonly ChatLanguageSet Instance = new ChatLanguageSet();

	// Token: 0x040006CB RID: 1739
	public readonly Dictionary<string, uint> Languages = new Dictionary<string, uint>
	{
		{
			"English",
			256U
		},
		{
			"Español",
			2U
		},
		{
			"한국어",
			4U
		},
		{
			"Pусский",
			8U
		},
		{
			"Português",
			16U
		},
		{
			"Al Arabiya",
			32U
		},
		{
			"Filipino",
			64U
		},
		{
			"Polskie",
			128U
		},
		{
			"日本語",
			512U
		},
		{
			"Other",
			1U
		}
	};
}
