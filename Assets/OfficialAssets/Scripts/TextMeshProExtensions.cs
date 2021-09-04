using System;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x02000071 RID: 113
public static class TextMeshProExtensions
{
	// Token: 0x060002D9 RID: 729 RVA: 0x00012FA3 File Offset: 0x000111A3
	public static float GetNotDumbRenderedHeight(this TextMeshPro self)
	{
		if ((double)self.renderedHeight > 100000000.0 || (double)self.renderedHeight < -100000000.0)
		{
			return 0f;
		}
		return self.renderedHeight;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00012FD8 File Offset: 0x000111D8
	public static Vector2 CursorPos(this TextMeshPro self)
	{
		if (self.textInfo == null || self.textInfo.lineCount == 0 || self.textInfo.lineInfo[0].characterCount <= 0)
		{
			return Vector2.zero;
		}
		return self.textInfo.lineInfo.Last((TMP_LineInfo l) => l.characterCount > 0).lineExtents.max;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00013054 File Offset: 0x00011254
	public static bool GetWordPosition(this TextMeshPro self, string str, out Vector3 bottomLeft, out Vector3 topRight)
	{
		int num = self.text.IndexOf(str);
		if (num != -1)
		{
			TMP_CharacterInfo tmp_CharacterInfo = self.textInfo.characterInfo[num];
			TMP_CharacterInfo tmp_CharacterInfo2 = self.textInfo.characterInfo[num + str.Length - 1];
			bottomLeft = tmp_CharacterInfo.bottomLeft;
			topRight = tmp_CharacterInfo2.topRight;
			bottomLeft.z = (topRight.z = self.transform.localPosition.z);
			return true;
		}
		bottomLeft = Vector3.zero;
		topRight = Vector3.zero;
		return false;
	}
}
