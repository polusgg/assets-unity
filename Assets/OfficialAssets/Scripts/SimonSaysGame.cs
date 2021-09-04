using System;
using System.Collections;
using System.Collections.Generic;
//using Rewired;
using UnityEngine;

// Token: 0x020001BB RID: 443
public class SimonSaysGame : Minigame
{
	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000A3A RID: 2618 RVA: 0x000422C7 File Offset: 0x000404C7
	// (set) Token: 0x06000A3B RID: 2619 RVA: 0x000422D6 File Offset: 0x000404D6
	private int IndexCount
	{
		get
		{
			return (int)this.MyNormTask.Data[0];
		}
		set
		{
			this.MyNormTask.Data[0] = (byte)value;
		}
	}

	// Token: 0x170000B4 RID: 180
	private byte this[int idx]
	{
		get
		{
			return this.MyNormTask.Data[idx + 1];
		}
		set
		{
			this.MyNormTask.Data[idx + 1] = value;
		}
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0004230C File Offset: 0x0004050C
	public override void Begin(PlayerTask task)
	{
		for (int i = 0; i < this.LeftSide.Length; i++)
		{
			this.LeftSide[i].color = Color.clear;
		}
		base.Begin(task);
		if (this.IndexCount == 0)
		{
			this.operations.Enqueue(128);
		}
		else
		{
			this.operations.Enqueue(32);
		}
		base.SetupInput(true);
		base.StartCoroutine(this.CoRun());
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x00042380 File Offset: 0x00040580
	public void HitButton(int bIdx)
	{
		if (this.MyNormTask.IsComplete || this.MyNormTask.taskStep >= this.IndexCount)
		{
			return;
		}
		if ((int)this[this.MyNormTask.taskStep] == bIdx)
		{
			this.MyNormTask.NextStep();
			this.SetLights(this.RightLights, this.MyNormTask.taskStep);
			if (this.MyNormTask.IsComplete)
			{
				this.SetLights(this.LeftLights, this.LeftLights.Length);
				for (int i = 0; i < this.Buttons.Length; i++)
				{
					SpriteRenderer spriteRenderer = this.Buttons[i];
					spriteRenderer.color = this.gray;
					base.StartCoroutine(this.FlashButton(-1, spriteRenderer, this.flashTime));
				}
				base.StartCoroutine(base.CoStartClose(0.75f));
				return;
			}
			this.operations.Enqueue(256 | bIdx);
			if (this.MyNormTask.taskStep >= this.IndexCount)
			{
				this.operations.Enqueue(128);
				return;
			}
		}
		else
		{
			this.IndexCount = 0;
			this.operations.Enqueue(64);
			this.operations.Enqueue(128);
		}
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x000424B2 File Offset: 0x000406B2
	private IEnumerator CoRun()
	{
		//for (;;)
		//{
		//	if (this.operations.Count <= 0)
		//	{
		//		Player player = ReInput.players.GetPlayer(0);
		//		Vector2 normalized;
		//		normalized..ctor(player.GetAxis(13), player.GetAxis(14));
		//		int num = 4;
		//		if (normalized.sqrMagnitude > 0.7f)
		//		{
		//			normalized = normalized.normalized;
		//			float num2 = Mathf.Atan2(normalized.y, normalized.x) * 57.29578f;
		//			if (num2 < 0f)
		//			{
		//				num2 += 360f;
		//			}
		//			num2 *= 0.0027777778f;
		//			this.inputAngleIndex = num2 * 8f;
		//			float num3 = this.inputAngleIndex - this.diagonalRoundingWidth;
		//			if (num3 < 0f)
		//			{
		//				num3 = 8f + num3;
		//			}
		//			float num4 = this.inputAngleIndex - Mathf.Floor(this.inputAngleIndex);
		//			if ((int)this.inputAngleIndex % 2 == 1)
		//			{
		//				if (num4 < this.diagonalRoundingWidth)
		//				{
		//					num = (int)this.inputAngleIndex;
		//				}
		//				else
		//				{
		//					num = (int)(this.inputAngleIndex + 0.5f);
		//				}
		//			}
		//			else if (num4 < 1f - this.diagonalRoundingWidth)
		//			{
		//				num = (int)this.inputAngleIndex;
		//			}
		//			else
		//			{
		//				num = (int)this.inputAngleIndex + 1;
		//			}
		//			this.roundUpIndex = Mathf.FloorToInt(this.inputAngleIndex + this.diagonalRoundingWidth);
		//			this.roundDownIndex = Mathf.FloorToInt(this.inputAngleIndex - this.diagonalRoundingWidth);
		//			num %= 8;
		//			num = this.orderedButtonIndices[num];
		//		}
		//		if (player.GetButtonDown(11))
		//		{
		//			this.HitButton(num);
		//			this.selectorHighlightObject.transform.localPosition = new Vector3(5000f, 5000f, this.selectorHighlightObject.transform.localPosition.z);
		//		}
		//		else
		//		{
		//			Vector3 localPosition = this.Buttons[num].transform.localPosition;
		//			localPosition.z = this.selectorHighlightObject.transform.localPosition.z;
		//			this.selectorHighlightObject.transform.localPosition = localPosition;
		//		}
		//		yield return null;
		//	}
		//	else
		//	{
		//		int num5 = this.operations.Dequeue();
		//		if (num5.HasAnyBit(256))
		//		{
		//			int num6 = num5 & -257;
		//			yield return this.FlashButton(num6, this.Buttons[num6], this.userButtonFlashTime);
		//		}
		//		else if (num5.HasAnyBit(128))
		//		{
		//			yield return this.CoAnimateNewLeftSide();
		//		}
		//		else if (num5.HasAnyBit(32))
		//		{
		//			yield return this.CoAnimateOldLeftSide();
		//		}
		//		else if (num5.HasAnyBit(64))
		//		{
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySound(this.FailSound, false, 1f);
		//			}
		//			this.SetAllColor(this.red);
		//			yield return new WaitForSeconds(this.flashTime);
		//			this.SetAllColor(Color.white);
		//			yield return new WaitForSeconds(this.flashTime);
		//			this.SetAllColor(this.red);
		//			yield return new WaitForSeconds(this.flashTime);
		//			this.SetAllColor(Color.white);
		//			yield return new WaitForSeconds(this.flashTime / 2f);
		//		}
		//	}
		//}
		yield break;
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x000424C4 File Offset: 0x000406C4
	private void AddIndex(int idxToAdd)
	{
		this[this.IndexCount] = (byte)idxToAdd;
		int indexCount = this.IndexCount;
		this.IndexCount = indexCount + 1;
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x000424EF File Offset: 0x000406EF
	private IEnumerator CoAnimateNewLeftSide()
	{
		this.SetLights(this.RightLights, 0);
		for (int i = 0; i < this.Buttons.Length; i++)
		{
			this.Buttons[i].color = this.gray;
		}
		this.AddIndex(this.Buttons.RandomIdx<SpriteRenderer>());
		yield return this.CoAnimateOldLeftSide();
		yield break;
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x000424FE File Offset: 0x000406FE
	private IEnumerator CoAnimateOldLeftSide()
	{
		yield return new WaitForSeconds(1f);
		this.SetLights(this.LeftLights, this.IndexCount);
		int num2;
		for (int i = 0; i < this.IndexCount; i = num2)
		{
			int num = (int)this[i];
			yield return this.FlashButton(num, this.LeftSide[num], this.flashTime);
			yield return new WaitForSeconds(0.1f);
			num2 = i + 1;
		}
		this.MyNormTask.taskStep = 0;
		for (int j = 0; j < this.Buttons.Length; j++)
		{
			this.Buttons[j].color = Color.white;
		}
		yield break;
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0004250D File Offset: 0x0004070D
	private IEnumerator FlashButton(int id, SpriteRenderer butt, float flashTime)
	{
		if (id > -1 && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.ButtonPressSound, false, 1f).pitch = Mathf.Lerp(0.5f, 1.5f, (float)id / 9f);
		}
		Color c = butt.color;
		butt.color = this.blue;
		yield return new WaitForSeconds(flashTime);
		butt.color = c;
		yield break;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00042534 File Offset: 0x00040734
	private void SetLights(SpriteRenderer[] lights, int num)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			if (i < num)
			{
				lights[i].color = this.green;
			}
			else
			{
				lights[i].color = this.gray;
			}
		}
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x00042574 File Offset: 0x00040774
	private void SetAllColor(Color color)
	{
		for (int i = 0; i < this.Buttons.Length; i++)
		{
			this.Buttons[i].color = color;
		}
		for (int j = 0; j < this.RightLights.Length; j++)
		{
			this.RightLights[j].color = color;
		}
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x000425C3 File Offset: 0x000407C3
	private void SetButtonColor(int i, Color color)
	{
		this.Buttons[i].color = color;
	}

	// Token: 0x04000BB1 RID: 2993
	private Queue<int> operations = new Queue<int>();

	// Token: 0x04000BB2 RID: 2994
	private const int FlashOp = 256;

	// Token: 0x04000BB3 RID: 2995
	private const int AnimateOp = 128;

	// Token: 0x04000BB4 RID: 2996
	private const int ReAnimateOp = 32;

	// Token: 0x04000BB5 RID: 2997
	private const int FailOp = 64;

	// Token: 0x04000BB6 RID: 2998
	private Color gray = new Color32(141, 141, 141, byte.MaxValue);

	// Token: 0x04000BB7 RID: 2999
	private Color blue = new Color32(68, 168, byte.MaxValue, byte.MaxValue);

	// Token: 0x04000BB8 RID: 3000
	private Color red = new Color32(byte.MaxValue, 58, 0, byte.MaxValue);

	// Token: 0x04000BB9 RID: 3001
	private Color green = Color.green;

	// Token: 0x04000BBA RID: 3002
	public SpriteRenderer[] LeftSide;

	// Token: 0x04000BBB RID: 3003
	public SpriteRenderer[] Buttons;

	// Token: 0x04000BBC RID: 3004
	public SpriteRenderer[] LeftLights;

	// Token: 0x04000BBD RID: 3005
	public SpriteRenderer[] RightLights;

	// Token: 0x04000BBE RID: 3006
	private float flashTime = 0.25f;

	// Token: 0x04000BBF RID: 3007
	private float userButtonFlashTime = 0.175f;

	// Token: 0x04000BC0 RID: 3008
	public AudioClip ButtonPressSound;

	// Token: 0x04000BC1 RID: 3009
	public AudioClip FailSound;

	// Token: 0x04000BC2 RID: 3010
	public Transform selectorHighlightObject;

	// Token: 0x04000BC3 RID: 3011
	public float diagonalRoundingWidth = 0.6f;

	// Token: 0x04000BC4 RID: 3012
	public float inputAngleIndex = -1f;

	// Token: 0x04000BC5 RID: 3013
	public int roundDownIndex;

	// Token: 0x04000BC6 RID: 3014
	public int roundUpIndex;

	// Token: 0x04000BC7 RID: 3015
	private int[] orderedButtonIndices = new int[]
	{
		5,
		2,
		1,
		0,
		3,
		6,
		7,
		8
	};
}
