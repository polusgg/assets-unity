using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class TempMinigame : Minigame
{
	// Token: 0x060007FD RID: 2045 RVA: 0x00033B54 File Offset: 0x00031D54
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.logValue = this.LogRange.Next();
		this.readingValue = this.ReadingRange.Next();
		this.ReadingText.Text = this.readingValue.ToString();
		this.ChangeNumber(0);
		base.SetupInput(true);
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x00033BB0 File Offset: 0x00031DB0
	public void ChangeNumber(int dir)
	{
		if (this.logValue == this.readingValue)
		{
			return;
		}
		if (dir != 0 && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.ButtonSound, false, 1f);
		}
		this.logValue += dir;
		this.LogText.Text = this.logValue.ToString();
		if (this.logValue == this.readingValue)
		{
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.75f));
		}
		this.deltaSinceLastChangeNumber = 0f;
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00033C48 File Offset: 0x00031E48
	private void Update()
	{
		//float axisRaw = ReInput.players.GetPlayer(0).GetAxisRaw(14);
		//this.deltaSinceLastChangeNumber += Time.deltaTime;
		//float num = 0.05f;
		//int num2 = 0;
		//if ((double)axisRaw > 0.9)
		//{
		//	num = 0.05f;
		//	num2 = 1;
		//}
		//else if ((double)axisRaw > 0.7)
		//{
		//	num = 0.1f;
		//	num2 = 1;
		//}
		//else if ((double)axisRaw > 0.5)
		//{
		//	num = 0.2f;
		//	num2 = 1;
		//}
		//else if ((double)axisRaw > 0.4)
		//{
		//	num = 0.3f;
		//	num2 = 1;
		//}
		//else if (axisRaw < -0.9f)
		//{
		//	num = 0.05f;
		//	num2 = -1;
		//}
		//else if (axisRaw < -0.7f)
		//{
		//	num = 0.1f;
		//	num2 = -1;
		//}
		//else if (axisRaw < -0.5f)
		//{
		//	num = 0.2f;
		//	num2 = -1;
		//}
		//else if (axisRaw < -0.4f)
		//{
		//	num = 0.3f;
		//	num2 = -1;
		//}
		//if (num2 != 0 && this.deltaSinceLastChangeNumber > num)
		//{
		//	this.ChangeNumber(num2);
		//}
	}

	// Token: 0x04000952 RID: 2386
	public TextRenderer LogText;

	// Token: 0x04000953 RID: 2387
	public TextRenderer ReadingText;

	// Token: 0x04000954 RID: 2388
	public IntRange LogRange;

	// Token: 0x04000955 RID: 2389
	public IntRange ReadingRange;

	// Token: 0x04000956 RID: 2390
	private int logValue;

	// Token: 0x04000957 RID: 2391
	private int readingValue;

	// Token: 0x04000958 RID: 2392
	public AudioClip ButtonSound;

	// Token: 0x04000959 RID: 2393
	private float deltaSinceLastChangeNumber;

	// Token: 0x0400095A RID: 2394
	public const float CHANGE_NUMBER_UPDATE_THRESHOLD_MIN = 0.05f;
}
