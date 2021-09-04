using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class VendingMinigame : Minigame
{
	// Token: 0x060003FC RID: 1020 RVA: 0x0001A876 File Offset: 0x00018A76
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x0001A888 File Offset: 0x00018A88
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		int num = this.Drinks.RandomIdx<Sprite>();
		this.TargetImage.sprite = this.DrawnDrinks[num];
		for (int i = 0; i < this.Drinks.Length; i++)
		{
			Sprite sprite = this.Drinks[i];
			int num2;
			while (!this.PickARandomSlot(sprite, out num2))
			{
			}
			this.Slots[num2].DrinkImage.enabled = true;
			this.Slots[num2].DrinkImage.sprite = sprite;
			if (num == i)
			{
				this.targetCode = VendingMinigame.SlotIdToString(num2);
			}
		}
		this.NumberText.Text = string.Empty;
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x0001A94C File Offset: 0x00018B4C
	private static int StringToSlotId(string code)
	{
		int num;
		if (int.TryParse(code[0].ToString(), out num) || VendingMinigame.Letters.Any(new Func<string, bool>(code.EndsWith)))
		{
			return -1;
		}
		int num2 = VendingMinigame.Letters.IndexOf(new Predicate<string>(code.StartsWith));
		return int.Parse(code[1].ToString()) - 1 + num2 * 4;
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0001A9BC File Offset: 0x00018BBC
	private static string SlotIdToString(int slotId)
	{
		int num = slotId % 4 + 1;
		int num2 = slotId / 4;
		return VendingMinigame.Letters[num2] + num.ToString();
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0001A9E6 File Offset: 0x00018BE6
	private bool PickARandomSlot(Sprite drink, out int slotId)
	{
		slotId = this.Slots.RandomIdx<VendingSlot>();
		return !this.Slots[slotId].DrinkImage.enabled;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0001AA0C File Offset: 0x00018C0C
	public void EnterDigit(string s)
	{
		if (this.animating)
		{
			return;
		}
		if (this.done)
		{
			return;
		}
		if (this.enteredCode.Length >= 2)
		{
			base.StartCoroutine(this.BlinkAccept());
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.Button, false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		}
		this.enteredCode += s;
		this.NumberText.Text = this.enteredCode;
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0001AA9C File Offset: 0x00018C9C
	public void ClearDigits()
	{
		if (this.animating)
		{
			return;
		}
		this.enteredCode = string.Empty;
		this.NumberText.Text = string.Empty;
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.Button, false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		}
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0001AB00 File Offset: 0x00018D00
	public void AcceptDigits()
	{
		if (this.animating || this.enteredCode.Length != 2)
		{
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.Button, false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		}
		base.StartCoroutine(this.Animate());
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0001AB62 File Offset: 0x00018D62
	private IEnumerator BlinkAccept()
	{
		int num;
		for (int i = 0; i < 5; i = num)
		{
			this.AcceptButton.color = Color.gray;
			yield return null;
			yield return null;
			this.AcceptButton.color = Color.white;
			yield return null;
			yield return null;
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0001AB71 File Offset: 0x00018D71
	private IEnumerator Animate()
	{
		this.animating = true;
		int slotId = VendingMinigame.StringToSlotId(this.enteredCode);
		if (slotId >= 0 && this.Slots[slotId].DrinkImage.enabled)
		{
			yield return Effects.All(new IEnumerator[]
			{
				this.CoBlinkVend(),
				this.Slots[slotId].CoBuy(this.SliderOpen, this.DrinkShake, this.DrinkLand)
			});
			if (this.targetCode == this.enteredCode)
			{
				this.done = true;
				this.MyNormTask.NextStep();
				base.StartCoroutine(base.CoStartClose(0.75f));
			}
			yield return this.Slots[slotId].CloseSlider(this.SliderOpen);
		}
		else
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.Error, false, 1f);
			}
			WaitForSeconds wait = new WaitForSeconds(0.1f);
			this.NumberText.Text = "XXXXXXXX";
			yield return wait;
			this.NumberText.Text = string.Empty;
			yield return wait;
			this.NumberText.Text = "XXXXXXXX";
			yield return wait;
			wait = null;
		}
		this.enteredCode = string.Empty;
		this.NumberText.Text = this.enteredCode;
		this.animating = false;
		yield break;
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0001AB80 File Offset: 0x00018D80
	private IEnumerator CoBlinkVend()
	{
		int num;
		for (int i = 0; i < 5; i = num)
		{
			this.NumberText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Vending, Array.Empty<object>());
			yield return Effects.Wait(0.1f);
			this.NumberText.Text = string.Empty;
			yield return Effects.Wait(0.1f);
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x040004B3 RID: 1203
	public static readonly string[] Letters = new string[]
	{
		"a",
		"b",
		"c"
	};

	// Token: 0x040004B4 RID: 1204
	public TextRenderer NumberText;

	// Token: 0x040004B5 RID: 1205
	public SpriteRenderer TargetImage;

	// Token: 0x040004B6 RID: 1206
	public string enteredCode = string.Empty;

	// Token: 0x040004B7 RID: 1207
	private bool animating;

	// Token: 0x040004B8 RID: 1208
	private bool done;

	// Token: 0x040004B9 RID: 1209
	private string targetCode;

	// Token: 0x040004BA RID: 1210
	public SpriteRenderer AcceptButton;

	// Token: 0x040004BB RID: 1211
	public VendingSlot[] Slots;

	// Token: 0x040004BC RID: 1212
	public Sprite[] Drinks;

	// Token: 0x040004BD RID: 1213
	public Sprite[] DrawnDrinks;

	// Token: 0x040004BE RID: 1214
	public AudioClip Ambience;

	// Token: 0x040004BF RID: 1215
	public AudioClip Button;

	// Token: 0x040004C0 RID: 1216
	public AudioClip Error;

	// Token: 0x040004C1 RID: 1217
	public AudioClip SliderOpen;

	// Token: 0x040004C2 RID: 1218
	public AudioClip DrinkShake;

	// Token: 0x040004C3 RID: 1219
	public AudioClip DrinkLand;

	// Token: 0x040004C4 RID: 1220
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x040004C5 RID: 1221
	public UiElement DefaultButtonSelected;

	// Token: 0x040004C6 RID: 1222
	public List<UiElement> ControllerSelectable;
}
