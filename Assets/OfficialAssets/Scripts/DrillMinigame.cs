using System;
using System.Linq;
using PowerTools;
//using Rewired;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class DrillMinigame : Minigame
{
	// Token: 0x060007CB RID: 1995 RVA: 0x00031BC5 File Offset: 0x0002FDC5
	public void Start()
	{
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.CaseImage);
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00031BD8 File Offset: 0x0002FDD8
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		base.SetupInput(true);
		do
		{
			for (int i = 0; i < this.states.Length; i++)
			{
				int num = this.states[i] = 0;
				this.Buttons[i].transform.localScale = Vector3.one * Mathf.Lerp(1f, 0.5f, (float)num / (float)this.MaxState);
				this.Buttons[i].gameObject.SetActive(num != this.MaxState);
			}
		}
		while (this.states.All((int s) => s == this.MaxState));
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00031C84 File Offset: 0x0002FE84
	private void Update()
	{
		//Player player = ReInput.players.GetPlayer(0);
		//if (this.prevFixedButton != null && this.changeButtonDelay >= 0f)
		//{
		//	this.changeButtonDelay -= Time.deltaTime;
		//	if (this.changeButtonDelay <= 0f)
		//	{
		//		this.prevFixedButton = null;
		//	}
		//}
		//for (int i = 0; i < 4; i++)
		//{
		//	if (player.GetButtonDown(this.drillButtonMaps[i]) && (this.prevFixedButton == null || this.prevFixedButton == this.Buttons[i]))
		//	{
		//		this.prevFixedButton = this.Buttons[i];
		//		this.changeButtonDelay = 0.25f;
		//		this.FixButton(this.Buttons[i]);
		//	}
		//}
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00031D44 File Offset: 0x0002FF44
	public void FixButton(SpriteAnim button)
	{
		int num = this.Buttons.IndexOf(button);
		if (this.states[num] == this.MaxState)
		{
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.ButtonSound, false, 1f);
		}
		int[] array = this.states;
		int num2 = num;
		int num3 = array[num2] + 1;
		array[num2] = num3;
		int num4 = num3;
		this.Buttons[num].transform.localScale = Vector3.one * Mathf.Lerp(1f, 0.5f, (float)num4 / (float)this.MaxState);
		this.Buttons[num].gameObject.SetActive(num4 != this.MaxState);
		if (num4 == this.MaxState)
		{
			this.changeButtonDelay = 0f;
		}
		if (this.states.All((int ss) => ss == this.MaxState))
		{
			this.statusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Fine, Array.Empty<object>());
			this.statusText.Color = Color.green;
			this.MyNormTask.NextStep();
			base.StartCoroutine(base.CoStartClose(0.75f));
		}
	}

	// Token: 0x040008E4 RID: 2276
	public SpriteRenderer CaseImage;

	// Token: 0x040008E5 RID: 2277
	public TextRenderer statusText;

	// Token: 0x040008E6 RID: 2278
	public SpriteAnim[] Buttons;

	// Token: 0x040008E7 RID: 2279
	public AnimationClip BadAnim;

	// Token: 0x040008E8 RID: 2280
	public AudioClip ButtonSound;

	// Token: 0x040008E9 RID: 2281
	private int MaxState = 4;

	// Token: 0x040008EA RID: 2282
	private int[] states = new int[4];

	// Token: 0x040008EB RID: 2283
	private SpriteAnim prevFixedButton;

	// Token: 0x040008EC RID: 2284
	private float changeButtonDelay;

	// Token: 0x040008ED RID: 2285
	private int[] drillButtonMaps = new int[]
	{
		20,
		22,
		21,
		24
	};
}
