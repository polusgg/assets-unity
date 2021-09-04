using System;
using System.Collections;
using System.Text;
using PowerTools;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class UploadDataGame : Minigame
{
	// Token: 0x060000AA RID: 170 RVA: 0x000044D0 File Offset: 0x000026D0
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x060000AB RID: 171 RVA: 0x000044E4 File Offset: 0x000026E4
	public override void Begin(PlayerTask task)
	{
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.Runner);
		base.Begin(task);
		if (this.MyNormTask.taskStep == 0)
		{
			this.Button.sprite = this.DownloadImage;
			this.Tower.SetActive(false);
			this.SourceText.text = DestroyableSingleton<TranslationController>.Instance.GetString(this.MyTask.StartAt);
			this.TargetText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MyTablet, Array.Empty<object>());
		}
		else
		{
			this.SourceText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.MyTablet, Array.Empty<object>());
			this.TargetText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.Headquarters, Array.Empty<object>());
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultButtonSelected);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000045CD File Offset: 0x000027CD
	public void Click()
	{
		base.StartCoroutine(this.Transition());
	}

	// Token: 0x060000AD RID: 173 RVA: 0x000045DC File Offset: 0x000027DC
	private IEnumerator Transition()
	{
		this.Button.gameObject.SetActive(false);
		this.Status.SetActive(true);
		float target = this.Gauge.transform.localScale.x;
		for (float t = 0f; t < 0.15f; t += Time.deltaTime)
		{
			this.Gauge.transform.localScale = new Vector3(t / 0.15f * target, 1f, 1f);
			yield return null;
		}
		base.StartCoroutine(this.PulseText());
		base.StartCoroutine(this.DoRun());
		base.StartCoroutine(this.DoText());
		base.StartCoroutine(this.DoPercent());
		yield break;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x000045EB File Offset: 0x000027EB
	private IEnumerator PulseText()
	{
		MeshRenderer rend2 = this.PercentText.GetComponent<MeshRenderer>();
		MeshRenderer rend1 = this.EstimatedText.GetComponent<MeshRenderer>();
		Color gray = new Color(0.3f, 0.3f, 0.3f, 1f);
		while (this.running)
		{
			yield return new WaitForLerp(0.4f, delegate(float t)
			{
				Color color = Color.Lerp(Color.black, gray, t);
				rend2.material.SetColor("_OutlineColor", color);
				rend1.material.SetColor("_OutlineColor", color);
			});
			yield return new WaitForLerp(0.4f, delegate(float t)
			{
				Color color = Color.Lerp(gray, Color.black, t);
				rend2.material.SetColor("_OutlineColor", color);
				rend1.material.SetColor("_OutlineColor", color);
			});
		}
		rend2.material.SetColor("_OutlineColor", Color.black);
		rend1.material.SetColor("_OutlineColor", Color.black);
		yield break;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x000045FA File Offset: 0x000027FA
	private IEnumerator DoPercent()
	{
		while (this.running)
		{
			float num = (float)this.count / 5f * 0.7f + this.timer / 3f * 0.3f;
			if (num >= 1f)
			{
				this.running = false;
			}
			num = Mathf.Clamp(num, 0f, 1f);
			this.Gauge.Value = num;
			this.PercentText.text = Mathf.RoundToInt(num * 100f).ToString() + "%";
			yield return null;
		}
		yield break;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00004609 File Offset: 0x00002809
	private IEnumerator DoText()
	{
		StringBuilder txt = new StringBuilder("Estimated Time: ");
		int baselen = txt.Length;
		int max = 604800;
		this.count = 0;
		while ((float)this.count < 5f)
		{
			txt.Length = baselen;
			int num = IntRange.Next(max / 6, max);
			int num2 = num / 86400;
			int num3 = num / 3600 % 24;
			int num4 = num / 60 % 60;
			int num5 = num % 60;
			string @string;
			if (num2 > 0)
			{
				@string = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.DownloadTestEstTimeDHMS, new object[]
				{
					num2,
					num3,
					num4,
					num5
				});
			}
			else if (num3 > 0)
			{
				@string = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.DownloadTestEstTimeHMS, new object[]
				{
					num3,
					num4,
					num5
				});
			}
			else if (num4 > 0)
			{
				@string = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.DownloadTestEstTimeMS, new object[]
				{
					num4,
					num5
				});
			}
			else
			{
				@string = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.DownloadTestEstTimeS, new object[]
				{
					num5
				});
			}
			this.EstimatedText.text = @string;
			max /= 4;
			yield return new WaitForSeconds(FloatRange.Next(0.6f, 1.2f));
			this.count++;
		}
		this.timer = 0f;
		while (this.timer < 3f)
		{
			txt.Length = baselen;
			int num6 = Mathf.RoundToInt(3f - this.timer);
			this.EstimatedText.text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.DownloadTestEstTimeS, new object[]
			{
				num6
			});
			yield return null;
			this.timer += Time.deltaTime;
		}
		yield break;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00004618 File Offset: 0x00002818
	private IEnumerator DoRun()
	{
		//while (this.running)
		//{
		//	UploadDataGame.<>c__DisplayClass28_0 CS$<>8__locals1 = new UploadDataGame.<>c__DisplayClass28_0();
		//	CS$<>8__locals1.<>4__this = this;
		//	this.LeftFolder.Play(this.FolderOpen, 1f);
		//	CS$<>8__locals1.pos = this.Runner.transform.localPosition;
		//	yield return new WaitForLerp(1.125f, delegate(float t)
		//	{
		//		CS$<>8__locals1.pos.x = Mathf.Lerp(-1.25f, 0.5625f, t);
		//		CS$<>8__locals1.<>4__this.Runner.transform.localPosition = CS$<>8__locals1.pos;
		//	});
		//	this.LeftFolder.Play(this.FolderClose, 1f);
		//	this.RightFolder.Play(this.FolderOpen, 1f);
		//	yield return new WaitForLerp(1.375f, delegate(float t)
		//	{
		//		CS$<>8__locals1.pos.x = Mathf.Lerp(0.5625f, 1.25f, t);
		//		CS$<>8__locals1.<>4__this.Runner.transform.localPosition = CS$<>8__locals1.pos;
		//	});
		//	yield return new WaitForAnimationFinish(this.RightFolder, this.FolderClose);
		//	CS$<>8__locals1 = null;
		//}
		//this.EstimatedText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.DownloadComplete, Array.Empty<object>());
		//this.MyNormTask.NextStep();
		//base.StartCoroutine(base.CoStartClose(0.75f));
		yield break;
	}

	// Token: 0x04000087 RID: 135
	public SpriteAnim LeftFolder;

	// Token: 0x04000088 RID: 136
	public SpriteAnim RightFolder;

	// Token: 0x04000089 RID: 137
	public AnimationClip FolderOpen;

	// Token: 0x0400008A RID: 138
	public AnimationClip FolderClose;

	// Token: 0x0400008B RID: 139
	public SpriteRenderer Runner;

	// Token: 0x0400008C RID: 140
	public HorizontalGauge Gauge;

	// Token: 0x0400008D RID: 141
	public TextMeshPro PercentText;

	// Token: 0x0400008E RID: 142
	public TextMeshPro EstimatedText;

	// Token: 0x0400008F RID: 143
	public TMPro.TextMeshPro SourceText;

	// Token: 0x04000090 RID: 144
	public TMPro.TextMeshPro TargetText;

	// Token: 0x04000091 RID: 145
	public SpriteRenderer Button;

	// Token: 0x04000092 RID: 146
	public Sprite DownloadImage;

	// Token: 0x04000093 RID: 147
	public GameObject Status;

	// Token: 0x04000094 RID: 148
	public GameObject Tower;

	// Token: 0x04000095 RID: 149
	private int count;

	// Token: 0x04000096 RID: 150
	private float timer;

	// Token: 0x04000097 RID: 151
	public const float RandomChunks = 5f;

	// Token: 0x04000098 RID: 152
	public const float ConstantTime = 3f;

	// Token: 0x04000099 RID: 153
	private bool running = true;

	// Token: 0x0400009A RID: 154
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400009B RID: 155
	public UiElement DefaultButtonSelected;
}
