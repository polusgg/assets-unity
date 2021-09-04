using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class SampleMinigame : Minigame
{
	// Token: 0x17000081 RID: 129
	// (get) Token: 0x0600077D RID: 1917 RVA: 0x0002F8FA File Offset: 0x0002DAFA
	// (set) Token: 0x0600077E RID: 1918 RVA: 0x0002F909 File Offset: 0x0002DB09
	private SampleMinigame.States State
	{
		get
		{
			return (SampleMinigame.States)this.MyNormTask.Data[0];
		}
		set
		{
			this.MyNormTask.Data[0] = (byte)value;
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x0600077F RID: 1919 RVA: 0x0002F919 File Offset: 0x0002DB19
	// (set) Token: 0x06000780 RID: 1920 RVA: 0x0002F928 File Offset: 0x0002DB28
	private int AnomalyId
	{
		get
		{
			return (int)this.MyNormTask.Data[1];
		}
		set
		{
			this.MyNormTask.Data[1] = (byte)value;
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0002F939 File Offset: 0x0002DB39
	public void Awake()
	{
		this.dropSounds = new RandomFill<AudioClip>();
		this.dropSounds.Set(this.DropSounds);
		this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedHello, Array.Empty<object>());
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0002F978 File Offset: 0x0002DB78
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		SampleMinigame.States state = this.State;
		if (state <= SampleMinigame.States.AwaitingStart)
		{
			if (state != SampleMinigame.States.PrepareSample)
			{
				if (state == SampleMinigame.States.AwaitingStart)
				{
					this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SamplesPress, Array.Empty<object>()).PadRight(20, ' ') + "-->";
					this.SetPlatformTop();
				}
			}
			else
			{
				base.StartCoroutine(this.BringPanelUp(true));
			}
		}
		else if (state != SampleMinigame.States.Selection)
		{
			if (state == SampleMinigame.States.Processing)
			{
				for (int i = 0; i < this.Tubes.Length; i++)
				{
					this.Tubes[i].color = Color.blue;
				}
				this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(SampleMinigame.ProcessingStrings.Random<StringNames>(), Array.Empty<object>());
				this.SetPlatformBottom();
			}
		}
		else
		{
			for (int j = 0; j < this.Tubes.Length; j++)
			{
				this.Tubes[j].color = Color.blue;
			}
			this.Tubes[this.AnomalyId].color = Color.red;
			this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SamplesSelect, Array.Empty<object>());
			this.SetPlatformTop();
		}
		base.SetupInput(true);
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
	private void SetPlatformBottom()
	{
		Vector3 localPosition = this.CenterPanel.transform.localPosition;
		localPosition.y = this.platformY.min;
		this.CenterPanel.transform.localPosition = localPosition;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0002FB08 File Offset: 0x0002DD08
	private void SetPlatformTop()
	{
		Vector3 localPosition = this.CenterPanel.transform.localPosition;
		localPosition.y = this.platformY.max;
		this.CenterPanel.transform.localPosition = localPosition;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0002FB4C File Offset: 0x0002DD4C
	public void Update()
	{
		//Player player = ReInput.players.GetPlayer(0);
		//SampleMinigame.States state = this.State;
		//if (state != SampleMinigame.States.AwaitingStart)
		//{
		//	if (state != SampleMinigame.States.Selection)
		//	{
		//		if (state != SampleMinigame.States.Processing)
		//		{
		//			return;
		//		}
		//		if (this.whichButtonSelector.gameObject.activeSelf)
		//		{
		//			this.whichButtonSelector.gameObject.SetActive(false);
		//		}
		//	}
		//	else
		//	{
		//		if (!this.whichButtonSelector.gameObject.activeSelf)
		//		{
		//			this.whichButtonSelector.gameObject.SetActive(true);
		//		}
		//		float axis = player.GetAxis(13);
		//		if (Mathf.Abs(axis) > 0.5f)
		//		{
		//			if (this.selectMoveCooldown > 0f)
		//			{
		//				this.selectMoveCooldown -= Time.deltaTime;
		//			}
		//			else
		//			{
		//				int num = (int)Mathf.Sign(axis);
		//				this.whichButtonSelected += num;
		//				this.whichButtonSelected = Mathf.Clamp(this.whichButtonSelected, 0, 4);
		//				Vector3 localPosition = this.whichButtonSelector.localPosition;
		//				Vector3 localPosition2 = this.Buttons[this.whichButtonSelected].transform.localPosition;
		//				localPosition.x = localPosition2.x;
		//				localPosition.y = localPosition2.y;
		//				this.whichButtonSelector.transform.localPosition = localPosition;
		//				this.selectMoveCooldown = 0.25f;
		//			}
		//		}
		//		else
		//		{
		//			this.selectMoveCooldown = 0f;
		//		}
		//		if (player.GetButtonDown(11))
		//		{
		//			ButtonBehavior component = this.Buttons[this.whichButtonSelected].GetComponent<ButtonBehavior>();
		//			if (component)
		//			{
		//				component.OnClick.Invoke();
		//				return;
		//			}
		//		}
		//	}
		//}
		//else
		//{
		//	if (this.whichButtonSelector.gameObject.activeSelf)
		//	{
		//		this.whichButtonSelector.gameObject.SetActive(false);
		//	}
		//	if (player.GetButtonDown(11))
		//	{
		//		this.NextStep();
		//		return;
		//	}
		//}
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0002FD08 File Offset: 0x0002DF08
	public void FixedUpdate()
	{
		//if (this.State == SampleMinigame.States.Processing)
		//{
		//	if (this.MyNormTask.TaskTimer <= 0f)
		//	{
		//		this.State = SampleMinigame.States.Selection;
		//		this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SamplesSelect, Array.Empty<object>());
		//		this.UpperText.Text = "";
		//		this.AnomalyId = this.Tubes.RandomIdx<SpriteRenderer>();
		//		this.Tubes[this.AnomalyId].color = Color.red;
		//		SpriteRenderer[] lowerButtons = this.LowerButtons;
		//		for (int i = 0; i < lowerButtons.Length; i++)
		//		{
		//			lowerButtons[i].color = Color.white;
		//		}
		//		base.StartCoroutine(this.BringPanelUp(false));
		//		return;
		//	}
		//	this.UpperText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MedETA, new object[]
		//	{
		//		this.MyNormTask.TaskTimer
		//	});
		//	return;
		//}
		//else
		//{
		//	if (this.State == SampleMinigame.States.Selection)
		//	{
		//		float num = Mathf.Cos(Time.time * 1.5f) - 0.2f;
		//		Color color;
		//		color..ctor(num, 1f, num, 1f);
		//		for (int j = 0; j < this.Buttons.Length; j++)
		//		{
		//			this.Buttons[j].color = color;
		//		}
		//		return;
		//	}
		//	if (this.State == SampleMinigame.States.AwaitingStart)
		//	{
		//		float num2 = Mathf.Cos(Time.time * 1.5f) - 0.2f;
		//		Color color2;
		//		color2..ctor(num2, 1f, num2, 1f);
		//		SpriteRenderer[] lowerButtons = this.LowerButtons;
		//		for (int i = 0; i < lowerButtons.Length; i++)
		//		{
		//			lowerButtons[i].color = color2;
		//		}
		//	}
		//	return;
		//}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0002FEAF File Offset: 0x0002E0AF
	public IEnumerator BringPanelUp(bool isBeginning)
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.PanelMoveSound, false, 1f);
		}
		WaitForFixedUpdate wait = new WaitForFixedUpdate();
		Vector3 pos = this.CenterPanel.transform.localPosition;
		for (float i = 0f; i < 0.75f; i += Time.deltaTime)
		{
			pos.y = this.platformY.Lerp(i / 0.75f);
			this.CenterPanel.transform.localPosition = pos;
			yield return wait;
		}
		if (isBeginning)
		{
			this.State = SampleMinigame.States.AwaitingStart;
			this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SamplesPress, Array.Empty<object>()).PadRight(19, ' ') + " -->";
		}
		pos.y = this.platformY.max;
		this.CenterPanel.transform.localPosition = pos;
		yield break;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0002FEC5 File Offset: 0x0002E0C5
	public IEnumerator BringPanelDown()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.PanelMoveSound, false, 1f);
		}
		WaitForFixedUpdate wait = new WaitForFixedUpdate();
		Vector3 pos = this.CenterPanel.transform.localPosition;
		for (float i = 0f; i < 0.75f; i += Time.deltaTime)
		{
			pos.y = this.platformY.Lerp(1f - i / 0.75f);
			this.CenterPanel.transform.localPosition = pos;
			yield return wait;
		}
		pos.y = this.platformY.min;
		this.CenterPanel.transform.localPosition = pos;
		yield break;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0002FED4 File Offset: 0x0002E0D4
	private IEnumerator DropTube(int id)
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.dropSounds.Get(), false, 1f);
		}
		this.Tubes[id].color = Color.blue;
		yield break;
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0002FEEC File Offset: 0x0002E0EC
	public void SelectTube(int tubeId)
	{
		if (this.State != SampleMinigame.States.Selection)
		{
			return;
		}
		this.State = SampleMinigame.States.PrepareSample;
		for (int i = 0; i < this.Buttons.Length; i++)
		{
			this.Buttons[i].color = Color.white;
		}
		base.StartCoroutine(this.CoSelectTube(this.AnomalyId, tubeId));
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0002FF44 File Offset: 0x0002E144
	private IEnumerator CoSelectTube(int correctTube, int selectedTube)
	{
		if (selectedTube != correctTube)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.FailSound, false, 1f);
			}
			this.UpperText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.BadResult, Array.Empty<object>());
			this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.BadResult, Array.Empty<object>());
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
			this.UpperText.Text = "";
		}
		else
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.ButtonSound, false, 0.6f);
			}
			this.UpperText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SamplesThanks, Array.Empty<object>());
			this.MyNormTask.NextStep();
			if (this.MyNormTask.IsComplete)
			{
				this.State = SampleMinigame.States.Complete;
				base.StartCoroutine(base.CoStartClose(0.75f));
			}
		}
		int num = this.MyNormTask.MaxStep - this.MyNormTask.taskStep;
		if (num == 0)
		{
			this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SamplesComplete, Array.Empty<object>());
		}
		else
		{
			this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.More, new object[]
			{
				num
			});
		}
		yield return this.BringPanelDown();
		for (int m = 0; m < this.Tubes.Length; m++)
		{
			this.Tubes[m].color = Color.white;
		}
		if (!this.MyNormTask.IsComplete)
		{
			yield return this.BringPanelUp(true);
		}
		yield break;
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0002FF64 File Offset: 0x0002E164
	public void NextStep()
	{
		if (this.State != SampleMinigame.States.AwaitingStart)
		{
			return;
		}
		this.State = SampleMinigame.States.Processing;
		SpriteRenderer[] lowerButtons = this.LowerButtons;
		for (int i = 0; i < lowerButtons.Length; i++)
		{
			lowerButtons[i].color = Color.white;
		}
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.ButtonSound, false, 1f).volume = 0.6f;
		}
		base.StartCoroutine(this.CoStartProcessing());
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0002FFDD File Offset: 0x0002E1DD
	private IEnumerator CoStartProcessing()
	{
		this.MyNormTask.TaskTimer = this.TimePerStep;
		this.MyNormTask.TimerStarted = NormalPlayerTask.TimerState.Started;
		yield return this.DropLiquid();
		this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(SampleMinigame.ProcessingStrings.Random<StringNames>(), Array.Empty<object>());
		yield return this.BringPanelDown();
		yield break;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0002FFEC File Offset: 0x0002E1EC
	private IEnumerator DropLiquid()
	{
		this.LowerText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SamplesAdding, Array.Empty<object>());
		WaitForSeconds dropWait = new WaitForSeconds(0.25f);
		WaitForFixedUpdate wait = new WaitForFixedUpdate();
		Vector3 pos = this.Dropper.transform.localPosition;
		yield return dropWait;
		yield return this.DropTube(0);
		Vector2 vPositionDelta = new Vector2(-0.25f, 0.25f);
		Vector2 vPosition = new Vector2(1f, 0f);
		VibrationManager.Vibrate(vPosition.x * 2.5f, vPosition.y * 2.5f, 0.2f, VibrationManager.VibrationFalloff.None, this.dropSounds.Get(), false);
		int num;
		for (int step = -2; step < 2; step = num)
		{
			float start = (float)step / 2f * 1.25f;
			float xTarg = (float)(step + 1) / 2f * 1.25f;
			for (float i = 0f; i < 0.15f; i += Time.deltaTime)
			{
				pos.x = Mathf.Lerp(start, xTarg, i / 0.15f);
				this.Dropper.transform.localPosition = pos;
				yield return wait;
			}
			pos.x = xTarg;
			this.Dropper.transform.localPosition = pos;
			yield return dropWait;
			int id = step + 3;
			vPosition += vPositionDelta;
			VibrationManager.Vibrate(vPosition.x * 2.5f, vPosition.y * 2.5f, 0.2f, VibrationManager.VibrationFalloff.None, this.dropSounds.Get(), false);
			yield return this.DropTube(id);
			num = step + 1;
		}
		for (float xTarg = 0f; xTarg < 0.15f; xTarg += Time.deltaTime)
		{
			pos.x = this.dropperX.Lerp(1f - xTarg / 0.15f);
			this.Dropper.transform.localPosition = pos;
			yield return wait;
		}
		pos.x = this.dropperX.min;
		this.Dropper.transform.localPosition = pos;
		yield break;
	}

	// Token: 0x04000874 RID: 2164
	private static StringNames[] ProcessingStrings = new StringNames[]
	{
		StringNames.TakeBreak,
		StringNames.GrabCoffee,
		StringNames.DontNeedWait,
		StringNames.DoSomethingElse
	};

	// Token: 0x04000875 RID: 2165
	private const float PanelMoveDuration = 0.75f;

	// Token: 0x04000876 RID: 2166
	private const byte TubeMask = 15;

	// Token: 0x04000877 RID: 2167
	public TextRenderer UpperText;

	// Token: 0x04000878 RID: 2168
	public TextRenderer LowerText;

	// Token: 0x04000879 RID: 2169
	public float TimePerStep = 15f;

	// Token: 0x0400087A RID: 2170
	public FloatRange platformY = new FloatRange(-3.5f, -0.75f);

	// Token: 0x0400087B RID: 2171
	public FloatRange dropperX = new FloatRange(-1.25f, 1.25f);

	// Token: 0x0400087C RID: 2172
	public SpriteRenderer CenterPanel;

	// Token: 0x0400087D RID: 2173
	public SpriteRenderer Dropper;

	// Token: 0x0400087E RID: 2174
	public SpriteRenderer[] Tubes;

	// Token: 0x0400087F RID: 2175
	public SpriteRenderer[] Buttons;

	// Token: 0x04000880 RID: 2176
	public SpriteRenderer[] LowerButtons;

	// Token: 0x04000881 RID: 2177
	public AudioClip ButtonSound;

	// Token: 0x04000882 RID: 2178
	public AudioClip PanelMoveSound;

	// Token: 0x04000883 RID: 2179
	public AudioClip FailSound;

	// Token: 0x04000884 RID: 2180
	public AudioClip[] DropSounds;

	// Token: 0x04000885 RID: 2181
	private RandomFill<AudioClip> dropSounds;

	// Token: 0x04000886 RID: 2182
	public Transform whichButtonSelector;

	// Token: 0x04000887 RID: 2183
	public int whichButtonSelected = 2;

	// Token: 0x04000888 RID: 2184
	private float selectMoveCooldown;

	// Token: 0x020003BF RID: 959
	public enum States : byte
	{
		// Token: 0x04001A42 RID: 6722
		PrepareSample,
		// Token: 0x04001A43 RID: 6723
		Complete = 16,
		// Token: 0x04001A44 RID: 6724
		AwaitingStart = 32,
		// Token: 0x04001A45 RID: 6725
		Selection = 64,
		// Token: 0x04001A46 RID: 6726
		Processing = 128
	}
}
