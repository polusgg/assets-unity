using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class SecureDataFile
{
	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0004362F File Offset: 0x0004182F
	// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00043637 File Offset: 0x00041837
	public bool Loaded { get; private set; }

	// Token: 0x06000AA7 RID: 2727 RVA: 0x00043640 File Offset: 0x00041840
	public SecureDataFile(string filePath)
	{
		this.filePath = filePath;
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x00043650 File Offset: 0x00041850
	public void LoadData(Action<BinaryReader> performRead)
	{
		this.Loaded = true;
		Debug.Log("Loading secure: " + this.filePath);
		if (FileIO.Exists(this.filePath))
		{
			byte[] array;
			try
			{
				array = FileIO.ReadAllBytes(this.filePath);
				for (int i = 0; i < array.Length; i++)
				{
					byte[] array2 = array;
					int num = i;
					array2[num] ^= (byte)(i % 212);
				}
			}
			catch
			{
				Debug.Log("Couldn't read secure file");
				this.Delete();
				return;
			}
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(array))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						if (binaryReader.ReadString() != SystemInfo.deviceUniqueIdentifier)
						{
							Debug.Log("Invalid secure file");
							this.Delete();
						}
						else
						{
							performRead(binaryReader);
						}
					}
				}
			}
			catch
			{
				Debug.Log("Deleted corrupt secure file inner");
				this.Delete();
			}
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x00043768 File Offset: 0x00041968
	public void SaveData(params object[] items)
	{
		byte[] array;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(SystemInfo.deviceUniqueIdentifier);
				foreach (object obj in items)
				{
					if (obj is long)
					{
						binaryWriter.Write((long)obj);
					}
					else
					{
						if (obj is HashSet<string>)
						{
							using (HashSet<string>.Enumerator enumerator = ((HashSet<string>)obj).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									string value = enumerator.Current;
									binaryWriter.Write(value);
								}
								goto IL_96;
							}
						}
						if (obj is string)
						{
							binaryWriter.Write((string)obj);
						}
					}
					IL_96:;
				}
				binaryWriter.Flush();
				memoryStream.Position = 0L;
				array = memoryStream.ToArray();
			}
		}
		for (int j = 0; j < array.Length; j++)
		{
			byte[] array2 = array;
			int num = j;
			array2[num] ^= (byte)(j % 212);
		}
		FileIO.WriteAllBytes(this.filePath, array);
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x000438A4 File Offset: 0x00041AA4
	public void Delete()
	{
		try
		{
			FileIO.Delete(this.filePath);
		}
		catch
		{
		}
	}

	// Token: 0x04000BFB RID: 3067
	private string filePath;
}
