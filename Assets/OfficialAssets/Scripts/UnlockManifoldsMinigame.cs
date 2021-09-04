using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020001BA RID: 442
public class UnlockManifoldsMinigame : Minigame
{
	// Token: 0x06000A35 RID: 2613 RVA: 0x00042104 File Offset: 0x00040304
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00042118 File Offset: 0x00040318
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		int num = 2;
		int num2 = this.Buttons.Length / num;
		float[] array = FloatRange.SpreadToEdges(-1.7f, 1.7f, num2).ToArray<float>();
		float[] array2 = FloatRange.SpreadToEdges(-0.43f, 0.43f, num).ToArray<float>();
		SpriteRenderer[] array3 = this.Buttons.ToArray<SpriteRenderer>();
		array3.Shuffle(0);
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				int num3 = i + j * num2;
				array3[num3].transform.localPosition = new Vector3(array[i], array2[j], 0f);
			}
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x000421E8 File Offset: 0x000403E8
	public void HitButton(int idx)
	{
		if (this.MyNormTask.IsComplete)
		{
			return;
		}
		if (this.animating)
		{
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.PressButtonSound, false, 1f).pitch = Mathf.Lerp(0.5f, 1.5f, (float)idx / 10f);
		}
		if (idx == this.buttonCounter)
		{
			this.Buttons[idx].color = Color.green;
			this.buttonCounter++;
			if (this.buttonCounter == this.Buttons.Length)
			{
				this.MyNormTask.NextStep();
				base.StartCoroutine(base.CoStartClose(0.75f));
				return;
			}
		}
		else
		{
			this.buttonCounter = 0;
			base.StartCoroutine(this.ResetAll());
		}
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x000422B0 File Offset: 0x000404B0
	private IEnumerator ResetAll()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.FailSound, false, 1f);
		}
		this.animating = true;
		for (int i = 0; i < this.Buttons.Length; i++)
		{
			this.Buttons[i].color = Color.red;
		}
		yield return new WaitForSeconds(0.25f);
		for (int j = 0; j < this.Buttons.Length; j++)
		{
			this.Buttons[j].color = Color.white;
		}
		yield return new WaitForSeconds(0.25f);
		for (int k = 0; k < this.Buttons.Length; k++)
		{
			this.Buttons[k].color = Color.red;
		}
		yield return new WaitForSeconds(0.25f);
		for (int l = 0; l < this.Buttons.Length; l++)
		{
			this.Buttons[l].color = Color.white;
		}
		this.animating = false;
		yield break;
	}

	// Token: 0x04000BA8 RID: 2984
	public SpriteRenderer[] Buttons;

	// Token: 0x04000BA9 RID: 2985
	public byte SystemId;

	// Token: 0x04000BAA RID: 2986
	private int buttonCounter;

	// Token: 0x04000BAB RID: 2987
	private bool animating;

	// Token: 0x04000BAC RID: 2988
	public AudioClip PressButtonSound;

	// Token: 0x04000BAD RID: 2989
	public AudioClip FailSound;

	// Token: 0x04000BAE RID: 2990
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000BAF RID: 2991
	public UiElement DefaultButtonSelected;

	// Token: 0x04000BB0 RID: 2992
	public List<UiElement> ControllerSelectable;
}
