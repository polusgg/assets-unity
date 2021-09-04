using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020001F7 RID: 503
public abstract class Minigame : MonoBehaviour
{
	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00049B03 File Offset: 0x00047D03
	// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00049B0B File Offset: 0x00047D0B
	public global::Console Console { get; set; }

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00049B14 File Offset: 0x00047D14
	protected int ConsoleId
	{
		get
		{
			if (!this.Console)
			{
				return 0;
			}
			return this.Console.ConsoleId;
		}
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00049B30 File Offset: 0x00047D30
	public virtual void Begin(PlayerTask task)
	{
		Minigame.Instance = this;
		this.MyTask = task;
		this.MyNormTask = (task as NormalPlayerTask);
		if (PlayerControl.LocalPlayer)
		{
			if (MapBehaviour.Instance)
			{
				MapBehaviour.Instance.Close();
			}
			PlayerControl.LocalPlayer.NetTransform.Halt();
		}
		base.StartCoroutine(this.CoAnimateOpen());
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00049B94 File Offset: 0x00047D94
	public IEnumerator CoStartClose(float duration = 0.75f)
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			yield break;
		}
		this.amClosing = Minigame.CloseState.Waiting;
		yield return Effects.Wait(duration);
		this.Close();
		yield break;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00049BAA File Offset: 0x00047DAA
	[Obsolete("Don't use, I just don't want to reselect the close button event handlers", true)]
	public void Close(bool allowMovement)
	{
		this.Close();
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00049BB2 File Offset: 0x00047DB2
	public void ForceClose()
	{
		this.Close();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00049BC8 File Offset: 0x00047DC8
	public virtual void Close()
	{
		if (this.amClosing != Minigame.CloseState.Closing)
		{
			if (this.CloseSound && Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.CloseSound, false, 1f);
			}
			ConsoleJoystick.SetMode_Menu();
			if (PlayerControl.LocalPlayer)
			{
				PlayerControl.HideCursorTemporarily();
			}
			this.amClosing = Minigame.CloseState.Closing;
			base.StartCoroutine(this.CoDestroySelf());
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00049C3F File Offset: 0x00047E3F
	protected virtual IEnumerator CoAnimateOpen()
	{
		this.amOpening = true;
		if (this.OpenSound && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.OpenSound, false, 1f);
		}
		float depth = base.transform.localPosition.z;
		switch (this.TransType)
		{
		case TransitionType.SlideBottom:
			for (float timer = 0f; timer < 0.25f; timer += Time.deltaTime)
			{
				float num = timer / 0.25f;
				base.transform.localPosition = new Vector3(0f, Mathf.SmoothStep(-8f, 0f, num), depth);
				yield return null;
			}
			base.transform.localPosition = new Vector3(0f, 0f, depth);
			break;
		case TransitionType.Alpha:
		{
			SpriteRenderer[] rends = base.GetComponentsInChildren<SpriteRenderer>();
			for (float timer = 0f; timer < 0.25f; timer += Time.deltaTime)
			{
				float num2 = timer / 0.25f;
				for (int i = 0; i < rends.Length; i++)
				{
					rends[i].color = Color.Lerp(Palette.ClearWhite, Color.white, num2);
				}
				yield return null;
			}
			for (int j = 0; j < rends.Length; j++)
			{
				rends[j].color = Color.white;
			}
			rends = null;
			break;
		}
		case TransitionType.None:
			base.transform.localPosition = new Vector3(0f, 0f, depth);
			break;
		}
		this.amOpening = false;
		yield break;
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x00049C4E File Offset: 0x00047E4E
	protected virtual IEnumerator CoDestroySelf()
	{
		switch (this.TransType)
		{
		case TransitionType.SlideBottom:
			for (float timer = 0f; timer < 0.25f; timer += Time.deltaTime)
			{
				float num = timer / 0.25f;
				base.transform.localPosition = new Vector3(0f, Mathf.SmoothStep(0f, -8f, num), -50f);
				yield return null;
			}
			break;
		case TransitionType.Alpha:
		{
			SpriteRenderer[] rends = base.GetComponentsInChildren<SpriteRenderer>();
			for (float timer = 0f; timer < 0.25f; timer += Time.deltaTime)
			{
				float num2 = timer / 0.25f;
				for (int i = 0; i < rends.Length; i++)
				{
					rends[i].color = Color.Lerp(Color.white, Palette.ClearWhite, num2);
				}
				yield return null;
			}
			rends = null;
			break;
		}
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00049C60 File Offset: 0x00047E60
	public void SetupInput(bool disableCursor)
	{
		this.inputHandler = base.GetComponentInChildren<SpecialInputHandler>(true);
		if (!this.inputHandler)
		{
			this.inputHandler = base.gameObject.AddComponent<SpecialInputHandler>();
		}
		if (disableCursor)
		{
			this.inputHandler.disableVirtualCursor = true;
		}
		ConsoleJoystick.SetMode_Task();
	}

	// Token: 0x04000D1C RID: 3356
	public static Minigame Instance;

	// Token: 0x04000D1D RID: 3357
	public const float Depth = -50f;

	// Token: 0x04000D1E RID: 3358
	public TransitionType TransType;

	// Token: 0x04000D1F RID: 3359
	protected PlayerTask MyTask;

	// Token: 0x04000D20 RID: 3360
	protected NormalPlayerTask MyNormTask;

	// Token: 0x04000D22 RID: 3362
	protected Minigame.CloseState amClosing;

	// Token: 0x04000D23 RID: 3363
	protected bool amOpening;

	// Token: 0x04000D24 RID: 3364
	public AudioClip OpenSound;

	// Token: 0x04000D25 RID: 3365
	public AudioClip CloseSound;

	// Token: 0x04000D26 RID: 3366
	[NonSerialized]
	public SpecialInputHandler inputHandler;

	// Token: 0x0200043B RID: 1083
	protected enum CloseState
	{
		// Token: 0x04001C08 RID: 7176
		None,
		// Token: 0x04001C09 RID: 7177
		Waiting,
		// Token: 0x04001C0A RID: 7178
		Closing
	}
}
