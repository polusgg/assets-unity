using System;
//using Rewired;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x0200008C RID: 140
public class SwitchMinigame : Minigame
{
	// Token: 0x06000359 RID: 857 RVA: 0x00016028 File Offset: 0x00014228
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.ship = Object.FindObjectOfType<ShipStatus>();
		SwitchSystem switchSystem = this.ship.Systems[SystemTypes.Electrical] as SwitchSystem;
		for (int i = 0; i < this.switches.Length; i++)
		{
			byte b = (byte)(1 << i);
			int num = (int)(switchSystem.ActualSwitches & b);
			this.lights[i].color = ((num == (int)(switchSystem.ExpectedSwitches & b)) ? this.OnColor : this.OffColor);
			this.switches[i].flipY = (num >> i == 0);
		}
		base.SetupInput(true);
	}

	// Token: 0x0600035A RID: 858 RVA: 0x000160C8 File Offset: 0x000142C8
	public void FixedUpdate()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.selectorHighlight.gameObject.SetActive(ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick);
		//Player player = ReInput.players.GetPlayer(0);
		//float axisRaw = player.GetAxisRaw(13);
		//float axisRaw2 = player.GetAxisRaw(17);
		//if (Mathf.Abs(axisRaw) > 0.7f)
		//{
		//	if (this.selectorCooldown <= 0f)
		//	{
		//		this.selectedIndex += (int)Mathf.Sign(axisRaw);
		//		this.selectedIndex = Mathf.Clamp(this.selectedIndex, 0, this.switches.Length);
		//		this.selectorHighlight.transform.localPosition = this.switches[this.selectedIndex].transform.localPosition;
		//		this.selectorCooldown = 0.2f;
		//	}
		//	else
		//	{
		//		this.selectorCooldown -= Time.deltaTime;
		//	}
		//}
		//else
		//{
		//	this.selectorCooldown = 0f;
		//}
		//if (Mathf.Abs(axisRaw2) > 0.7f)
		//{
		//	if (!this.prevHadVerticalInput)
		//	{
		//		if (axisRaw2 > 0.7f)
		//		{
		//			if (this.switches[this.selectedIndex].flipY)
		//			{
		//				this.FlipSwitch(this.selectedIndex);
		//			}
		//		}
		//		else if (axisRaw2 < -0.7f && !this.switches[this.selectedIndex].flipY)
		//		{
		//			this.FlipSwitch(this.selectedIndex);
		//		}
		//	}
		//	this.prevHadVerticalInput = true;
		//}
		//else
		//{
		//	this.prevHadVerticalInput = false;
		//}
		//int num = 0;
		//SwitchSystem switchSystem = this.ship.Systems[SystemTypes.Electrical] as SwitchSystem;
		//for (int i = 0; i < this.switches.Length; i++)
		//{
		//	byte b = (byte)(1 << i);
		//	int num2 = (int)(switchSystem.ActualSwitches & b);
		//	if (num2 == (int)(switchSystem.ExpectedSwitches & b))
		//	{
		//		num++;
		//		this.lights[i].color = this.OnColor;
		//	}
		//	else
		//	{
		//		this.lights[i].color = this.OffColor;
		//	}
		//	this.switches[i].flipY = (num2 >> i == 0);
		//}
		//float num3 = (float)num / (float)this.switches.Length;
		//this.bottom.Center = 0.47f * num3;
		//this.top.NoiseLevel = 1f - num3;
		//this.middle.Value = switchSystem.Level + (Mathf.PerlinNoise(0f, Time.time * 51f) - 0.5f) * 0.04f;
		//if (num == this.switches.Length)
		//{
		//	base.StartCoroutine(base.CoStartClose(0.5f));
		//}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00016348 File Offset: 0x00014548
	public void FlipSwitch(int switchIdx)
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		int num = 0;
		SwitchSystem switchSystem = this.ship.Systems[SystemTypes.Electrical] as SwitchSystem;
		for (int i = 0; i < this.switches.Length; i++)
		{
			byte b = (byte)(1 << i);
			if ((switchSystem.ActualSwitches & b) == (switchSystem.ExpectedSwitches & b))
			{
				num++;
			}
		}
		if (num == this.switches.Length)
		{
			return;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.FlipSound, false, 1f);
		}
		ShipStatus.Instance.RpcRepairSystem(SystemTypes.Electrical, (int)((byte)switchIdx));
		try
		{
			((SabotageTask)this.MyTask).MarkContributed();
		}
		catch
		{
		}
	}

	// Token: 0x040003E3 RID: 995
	public Color OnColor = Color.green;

	// Token: 0x040003E4 RID: 996
	public Color OffColor = new Color(0.1f, 0.3f, 0.1f);

	// Token: 0x040003E5 RID: 997
	private ShipStatus ship;

	// Token: 0x040003E6 RID: 998
	public SpriteRenderer[] switches;

	// Token: 0x040003E7 RID: 999
	public SpriteRenderer[] lights;

	// Token: 0x040003E8 RID: 1000
	public RadioWaveBehaviour top;

	// Token: 0x040003E9 RID: 1001
	public HorizontalGauge middle;

	// Token: 0x040003EA RID: 1002
	public FlatWaveBehaviour bottom;

	// Token: 0x040003EB RID: 1003
	public AudioClip FlipSound;

	// Token: 0x040003EC RID: 1004
	public Transform selectorHighlight;

	// Token: 0x040003ED RID: 1005
	private int selectedIndex;

	// Token: 0x040003EE RID: 1006
	private float selectorCooldown;

	// Token: 0x040003EF RID: 1007
	private bool prevHadVerticalInput;
}
