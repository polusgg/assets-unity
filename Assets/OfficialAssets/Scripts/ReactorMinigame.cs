using System;
using UnityEngine;

// Token: 0x020001B8 RID: 440
public class ReactorMinigame : Minigame
{
	// Token: 0x06000A23 RID: 2595 RVA: 0x00041AAC File Offset: 0x0003FCAC
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.reactor = (ShipStatus.Instance.Systems[this.MyTask.StartAt] as ReactorSystemType);
		this.hand.color = this.bad;
		base.SetupInput(false);
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00041B00 File Offset: 0x0003FD00
	public void ButtonDown()
	{
		if (!this.reactor.IsActive || this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		this.isButtonDown = !this.isButtonDown;
		if (this.isButtonDown)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.HandSound, true, 1f);
			}
			ShipStatus.Instance.RpcRepairSystem(this.MyTask.StartAt, (int)((byte)(64 | base.ConsoleId)));
		}
		else
		{
			SoundManager.Instance.StopSound(this.HandSound);
			ShipStatus.Instance.RpcRepairSystem(this.MyTask.StartAt, (int)((byte)(32 | base.ConsoleId)));
		}
		try
		{
			((SabotageTask)this.MyTask).MarkContributed();
		}
		catch
		{
		}
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00041BD0 File Offset: 0x0003FDD0
	public void FixedUpdate()
	{
		VirtualCursor.instance.SetScreenPosition(Vector2.zero);
		if (!this.reactor.IsActive)
		{
			if (this.amClosing == Minigame.CloseState.None)
			{
				this.hand.color = this.good;
				this.statusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString((this.MyTask.StartAt == SystemTypes.Reactor) ? StringNames.ReactorNominal : StringNames.SeismicNominal, Array.Empty<object>());
				this.sweeper.enabled = false;
				SoundManager.Instance.StopSound(this.HandSound);
				base.StartCoroutine(base.CoStartClose(0.75f));
				return;
			}
		}
		else
		{
			if (!this.isButtonDown)
			{
				this.statusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString((this.MyTask.StartAt == SystemTypes.Reactor) ? StringNames.ReactorHoldToStop : StringNames.SeismicHoldToStop, Array.Empty<object>());
				this.sweeper.enabled = false;
				return;
			}
			this.statusText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.ReactorWaiting, Array.Empty<object>());
			Vector3 localPosition = this.sweeper.transform.localPosition;
			localPosition.y = this.YSweep.Lerp(Mathf.Sin(Time.time) * 0.5f + 0.5f);
			this.sweeper.transform.localPosition = localPosition;
			this.sweeper.enabled = true;
		}
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00041D38 File Offset: 0x0003FF38
	public override void Close()
	{
		SoundManager.Instance.StopSound(this.HandSound);
		if (ShipStatus.Instance)
		{
			ShipStatus.Instance.RpcRepairSystem(this.MyTask.StartAt, (int)((byte)(32 | base.ConsoleId)));
		}
		base.Close();
	}

	// Token: 0x04000B91 RID: 2961
	private Color bad = new Color(1f, 0.16078432f, 0f);

	// Token: 0x04000B92 RID: 2962
	private Color good = new Color(0.3019608f, 0.8862745f, 0.8352941f);

	// Token: 0x04000B93 RID: 2963
	private ReactorSystemType reactor;

	// Token: 0x04000B94 RID: 2964
	public TextRenderer statusText;

	// Token: 0x04000B95 RID: 2965
	public SpriteRenderer hand;

	// Token: 0x04000B96 RID: 2966
	private FloatRange YSweep = new FloatRange(-2.15f, 1.56f);

	// Token: 0x04000B97 RID: 2967
	public SpriteRenderer sweeper;

	// Token: 0x04000B98 RID: 2968
	public AudioClip HandSound;

	// Token: 0x04000B99 RID: 2969
	private bool isButtonDown;
}
