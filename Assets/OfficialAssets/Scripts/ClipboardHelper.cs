using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public static class ClipboardHelper
{
	// Token: 0x060005CD RID: 1485
	[DllImport("user32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool IsClipboardFormatAvailable(uint format);

	// Token: 0x060005CE RID: 1486
	[DllImport("user32.dll")]
	private static extern bool OpenClipboard(IntPtr hWndNewOwner);

	// Token: 0x060005CF RID: 1487
	[DllImport("user32.dll")]
	private static extern bool CloseClipboard();

	// Token: 0x060005D0 RID: 1488
	[DllImport("user32.dll")]
	private static extern IntPtr GetClipboardData(uint format);

	// Token: 0x060005D1 RID: 1489
	[DllImport("kernel32.dll")]
	private static extern IntPtr GlobalLock(IntPtr hMem);

	// Token: 0x060005D2 RID: 1490
	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GlobalUnlock(IntPtr hMem);

	// Token: 0x060005D3 RID: 1491
	[DllImport("kernel32.dll")]
	private static extern int GlobalSize(IntPtr hMem);

	// Token: 0x060005D4 RID: 1492 RVA: 0x00025EFC File Offset: 0x000240FC
	public static string GetClipboardString()
	{
		if (!ClipboardHelper.IsClipboardFormatAvailable(1U))
		{
			return null;
		}
		string result;
		try
		{
			if (!ClipboardHelper.OpenClipboard(IntPtr.Zero))
			{
				result = null;
			}
			else
			{
				IntPtr clipboardData = ClipboardHelper.GetClipboardData(1U);
				if (clipboardData == IntPtr.Zero)
				{
					result = null;
				}
				else
				{
					IntPtr intPtr = IntPtr.Zero;
					try
					{
						intPtr = ClipboardHelper.GlobalLock(clipboardData);
						int len = ClipboardHelper.GlobalSize(clipboardData);
						result = Marshal.PtrToStringAnsi(clipboardData, len);
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							ClipboardHelper.GlobalUnlock(intPtr);
						}
					}
				}
			}
		}
		finally
		{
			ClipboardHelper.CloseClipboard();
		}
		return result;
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00025F94 File Offset: 0x00024194
	public static void PutClipboardString(string str)
	{
		GUIUtility.systemCopyBuffer = str;
	}

	// Token: 0x0400067B RID: 1659
	private const uint CF_TEXT = 1U;
}
