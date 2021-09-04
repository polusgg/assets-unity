using System;

// Token: 0x02000208 RID: 520
public class TowelTaskConsole : AutoTaskConsole
{
	// Token: 0x06000C3F RID: 3135 RVA: 0x0004BE68 File Offset: 0x0004A068
	protected override void AfterUse(NormalPlayerTask task)
	{
		if (this.useSound && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.useSound, false, 1f);
		}
		int num = task.Data.IndexOf((byte b) => (int)b == this.ConsoleId);
		task.Data[num] = 250;
		this.Image.color = Palette.ClearWhite;
	}
}
