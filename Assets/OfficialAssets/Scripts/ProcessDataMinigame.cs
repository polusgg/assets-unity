using System;
using System.Collections;
using System.Text;
using PowerTools;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class ProcessDataMinigame : Minigame
{
	// Token: 0x060000A1 RID: 161 RVA: 0x00004143 File Offset: 0x00002343
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00004155 File Offset: 0x00002355
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.Runner);
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected);
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000418A File Offset: 0x0000238A
	public void StartStopFill()
	{
		this.StartButton.enabled = false;
		base.StartCoroutine(this.CoDoAnimation());
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000041A5 File Offset: 0x000023A5
	private IEnumerator CoDoAnimation()
	{
		this.LeftFolder.Play(this.OpenFolderClip, 1f);
		yield return this.Transition();
		base.StartCoroutine(this.DoText());
		for (float timer = 0f; timer < this.Duration; timer += Time.deltaTime)
		{
			float num = timer / this.Duration;
			this.Gauge.Value = num;
			this.PercentText.Text = Mathf.RoundToInt(num * 100f).ToString() + "%";
			this.scenery.SetParallax(this.SceneRange.Lerp(num));
			yield return null;
		}
		this.running = false;
		this.EstimatedText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.WeatherComplete, Array.Empty<object>());
		this.RightFolder.Play(this.CloseFolderClip, 1f);
		this.MyNormTask.NextStep();
		yield return base.CoStartClose(0.75f);
		yield break;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000041B4 File Offset: 0x000023B4
	private IEnumerator Transition()
	{
		yield return Effects.ScaleIn(this.StartButton.transform, 1f, 0f, 0.15f);
		this.Status.SetActive(true);
		for (float t = 0f; t < 0.15f; t += Time.deltaTime)
		{
			this.Gauge.transform.localScale = new Vector3(t / 0.15f, 1f, 1f);
			yield return null;
		}
		this.Gauge.transform.localScale = new Vector3(1f, 1f, 1f);
		yield break;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000041C3 File Offset: 0x000023C3
	private IEnumerator DoText()
	{
		StringBuilder txt = new StringBuilder(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Processing, Array.Empty<object>()) + ": ");
		int len = txt.Length;
		while (this.running)
		{
			txt.Append(this.DocTopics.Random<string>());
			txt.Append("_");
			txt.Append(this.DocTypes.Random<string>());
			txt.Append(this.DocExtensions.Random<string>());
			this.EstimatedText.Text = txt.ToString();
			yield return Effects.Wait(FloatRange.Next(0.025f, 0.15f));
			txt.Length = len;
		}
		yield break;
	}

	// Token: 0x0400006D RID: 109
	private string[] DocTopics = new string[]
	{
		"important",
		"amongis",
		"lifeform",
		"danger",
		"mining",
		"rocks",
		"minerals",
		"dirt",
		"soil",
		"life",
		"specimen",
		"lookatthis",
		"wut",
		"happy_birthday",
		"1internet",
		"cake",
		"pineapple"
	};

	// Token: 0x0400006E RID: 110
	private string[] DocTypes = new string[]
	{
		"data",
		"srsbiz",
		"finances",
		"report",
		"growth",
		"results",
		"investigation"
	};

	// Token: 0x0400006F RID: 111
	private string[] DocExtensions = new string[]
	{
		".png",
		".tiff",
		".txt",
		".csv",
		".doc",
		".file",
		".data",
		".jpg",
		".raw",
		".xsl",
		".dot",
		".dat",
		".doof",
		".mira",
		".space"
	};

	// Token: 0x04000070 RID: 112
	public float Duration = 5f;

	// Token: 0x04000071 RID: 113
	public ParallaxController scenery;

	// Token: 0x04000072 RID: 114
	public PassiveButton StartButton;

	// Token: 0x04000073 RID: 115
	public TextRenderer EstimatedText;

	// Token: 0x04000074 RID: 116
	public TextRenderer PercentText;

	// Token: 0x04000075 RID: 117
	public SpriteAnim LeftFolder;

	// Token: 0x04000076 RID: 118
	public SpriteAnim RightFolder;

	// Token: 0x04000077 RID: 119
	public AnimationClip OpenFolderClip;

	// Token: 0x04000078 RID: 120
	public AnimationClip CloseFolderClip;

	// Token: 0x04000079 RID: 121
	public GameObject Status;

	// Token: 0x0400007A RID: 122
	public SpriteRenderer Runner;

	// Token: 0x0400007B RID: 123
	public HorizontalGauge Gauge;

	// Token: 0x0400007C RID: 124
	private bool running = true;

	// Token: 0x0400007D RID: 125
	public FloatRange SceneRange = new FloatRange(0f, 50f);

	// Token: 0x0400007E RID: 126
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400007F RID: 127
	public UiElement DefaultButtonSelected;
}
