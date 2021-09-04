using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public static class Palette
{
	// Token: 0x040002E7 RID: 743
	public static readonly Color DisabledGrey = new Color(0.3f, 0.3f, 0.3f, 1f);

	// Token: 0x040002E8 RID: 744
	public static readonly Color DisabledClear = new Color(1f, 1f, 1f, 0.3f);

	// Token: 0x040002E9 RID: 745
	public static readonly Color EnabledColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x040002EA RID: 746
	public static readonly Color Black = new Color(0f, 0f, 0f, 1f);

	// Token: 0x040002EB RID: 747
	public static readonly Color ClearWhite = new Color(1f, 1f, 1f, 0f);

	// Token: 0x040002EC RID: 748
	public static readonly Color HalfWhite = new Color(1f, 1f, 1f, 0.5f);

	// Token: 0x040002ED RID: 749
	public static readonly Color White = new Color(1f, 1f, 1f, 1f);

	// Token: 0x040002EE RID: 750
	public static readonly Color LightBlue = new Color(0.5f, 0.5f, 1f);

	// Token: 0x040002EF RID: 751
	public static readonly Color Blue = new Color(0.2f, 0.2f, 1f);

	// Token: 0x040002F0 RID: 752
	public static readonly Color Orange = new Color(1f, 0.6f, 0.005f);

	// Token: 0x040002F1 RID: 753
	public static readonly Color Purple = new Color(0.6f, 0.1f, 0.6f);

	// Token: 0x040002F2 RID: 754
	public static readonly Color Brown = new Color(0.72f, 0.43f, 0.11f);

	// Token: 0x040002F3 RID: 755
	public static readonly Color CrewmateBlue = new Color32(140, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x040002F4 RID: 756
	public static readonly Color ImpostorRed = new Color32(byte.MaxValue, 25, 25, byte.MaxValue);

	// Token: 0x040002F5 RID: 757
	public static readonly StringNames[] ShortColorNames = new StringNames[]
	{
		StringNames.VitalsRED,
		StringNames.VitalsBLUE,
		StringNames.VitalsGRN,
		StringNames.VitalsPINK,
		StringNames.VitalsORGN,
		StringNames.VitalsYLOW,
		StringNames.VitalsBLAK,
		StringNames.VitalsWHTE,
		StringNames.VitalsBRWN,
		StringNames.VitalsCYAN,
		StringNames.VitalsLIME,
		StringNames.VitalsPURP
	};

	// Token: 0x040002F6 RID: 758
	public static readonly StringNames[] ColorNames = new StringNames[]
	{
		StringNames.ColorRed,
		StringNames.ColorBlue,
		StringNames.ColorGreen,
		StringNames.ColorPink,
		StringNames.ColorOrange,
		StringNames.ColorYellow,
		StringNames.ColorBlack,
		StringNames.ColorWhite,
		StringNames.ColorPurple,
		StringNames.ColorBrown,
		StringNames.ColorCyan,
		StringNames.ColorLime
	};

	// Token: 0x040002F7 RID: 759
	public static readonly Color32[] PlayerColors = new Color32[]
	{
		new Color32(198, 17, 17, byte.MaxValue),
		new Color32(19, 46, 210, byte.MaxValue),
		new Color32(17, 128, 45, byte.MaxValue),
		new Color32(238, 84, 187, byte.MaxValue),
		new Color32(240, 125, 13, byte.MaxValue),
		new Color32(246, 246, 87, byte.MaxValue),
		new Color32(63, 71, 78, byte.MaxValue),
		new Color32(215, 225, 241, byte.MaxValue),
		new Color32(107, 47, 188, byte.MaxValue),
		new Color32(113, 73, 30, byte.MaxValue),
		new Color32(56, byte.MaxValue, 221, byte.MaxValue),
		new Color32(80, 240, 57, byte.MaxValue)
	};

	// Token: 0x040002F8 RID: 760
	public static readonly Color32[] ShadowColors = new Color32[]
	{
		new Color32(122, 8, 56, byte.MaxValue),
		new Color32(9, 21, 142, byte.MaxValue),
		new Color32(10, 77, 46, byte.MaxValue),
		new Color32(172, 43, 174, byte.MaxValue),
		new Color32(180, 62, 21, byte.MaxValue),
		new Color32(195, 136, 34, byte.MaxValue),
		new Color32(30, 31, 38, byte.MaxValue),
		new Color32(132, 149, 192, byte.MaxValue),
		new Color32(59, 23, 124, byte.MaxValue),
		new Color32(94, 38, 21, byte.MaxValue),
		new Color32(36, 169, 191, byte.MaxValue),
		new Color32(21, 168, 66, byte.MaxValue)
	};

	// Token: 0x040002F9 RID: 761
	public static readonly Color32 VisorColor = new Color32(149, 202, 220, byte.MaxValue);
}
