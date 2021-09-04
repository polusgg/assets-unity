using System;
//using Rewired;
using UnityEngine;

// Token: 0x0200015D RID: 349
public class WeatherSwitchGame : Minigame
{
	// Token: 0x06000825 RID: 2085 RVA: 0x00035474 File Offset: 0x00033674
	public void Start()
	{
		for (int i = 0; i < this.Controls.Length; i++)
		{
			WeatherControl weatherControl = this.Controls[i];
			weatherControl.name = DestroyableSingleton<TranslationController>.Instance.GetString(WeatherSwitchGame.ControlNames[i], Array.Empty<object>());
			weatherControl.Label.Text = DestroyableSingleton<TranslationController>.Instance.GetString(WeatherSwitchGame.ControlNames[i], Array.Empty<object>());
		}
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x000354D8 File Offset: 0x000336D8
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.WeatherTask = (this.MyNormTask as WeatherNodeTask);
		this.Controls[this.WeatherTask.NodeId].SetInactive();
		base.SetupInput(true);
		Vector3 localPosition = this.Controls[this.WeatherTask.NodeId].transform.localPosition + this.buttonGlyphOffset_On;
		localPosition.z = this.buttonGlyph.transform.localPosition.z;
		this.buttonGlyph.transform.localPosition = localPosition;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00035570 File Offset: 0x00033770
	private void Update()
	{
		//if (ReInput.players.GetPlayer(0).GetButtonDown(20))
		//{
		//	this.FlipSwitch(this.WeatherTask.NodeId);
		//}
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00035598 File Offset: 0x00033798
	public void FlipSwitch(int i)
	{
		if (i == this.WeatherTask.NodeId)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.SwitchSound, false, 1f);
			}
			this.WeatherTask.NextStep();
			this.Controls[this.WeatherTask.NodeId].SetActive();
			base.StartCoroutine(base.CoStartClose(0.75f));
			Vector3 localPosition = this.Controls[this.WeatherTask.NodeId].transform.localPosition + this.buttonGlyphOffset_Off;
			localPosition.z = this.buttonGlyph.transform.localPosition.z;
			this.buttonGlyph.transform.localPosition = localPosition;
		}
	}

	// Token: 0x0400099E RID: 2462
	public static StringNames[] ControlNames = new StringNames[]
	{
		StringNames.NodeCA,
		StringNames.NodeTB,
		StringNames.NodeIRO,
		StringNames.NodePD,
		StringNames.NodeGI,
		StringNames.NodeMLG
	};

	// Token: 0x0400099F RID: 2463
	public WeatherControl[] Controls;

	// Token: 0x040009A0 RID: 2464
	private WeatherNodeTask WeatherTask;

	// Token: 0x040009A1 RID: 2465
	public Transform buttonGlyph;

	// Token: 0x040009A2 RID: 2466
	public Vector3 buttonGlyphOffset_Off;

	// Token: 0x040009A3 RID: 2467
	public Vector3 buttonGlyphOffset_On;

	// Token: 0x040009A4 RID: 2468
	public AudioClip SwitchSound;
}
