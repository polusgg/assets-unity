using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class TextMarquee : MonoBehaviour
{
	// Token: 0x060002C6 RID: 710 RVA: 0x00011D31 File Offset: 0x0000FF31
	public void Start()
	{
		base.StartCoroutine(this.Run());
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00011D40 File Offset: 0x0000FF40
	private IEnumerator Run()
	{
		yield return null;
		this.Target.render.material.SetInt("_Mask", 4);
		int num;
		for (int i = 0; i < 1000; i = num)
		{
			Vector4 temp = default(Vector4);
			this.targetText = this.Target.Text;
			this.Target.render.material.SetVector("_Offset", temp);
			float timer = 0f;
			while (timer < this.PauseTime && (this.IgnoreTextChanges || !(this.targetText != this.Target.Text)))
			{
				yield return null;
				timer += Time.deltaTime;
			}
			timer = 0f;
			while (timer < 100f && (this.IgnoreTextChanges || !(this.targetText != this.Target.Text)))
			{
				temp.x -= this.ScrollSpeed * Time.deltaTime;
				this.Target.render.material.SetVector("_Offset", temp);
				if (this.Target.Width + temp.x < this.AreaWidth)
				{
					break;
				}
				yield return null;
				timer += Time.deltaTime;
			}
			timer = 0f;
			while (timer < this.PauseTime && (this.IgnoreTextChanges || !(this.targetText != this.Target.Text)))
			{
				yield return null;
				timer += Time.deltaTime;
			}
			yield return null;
			temp = default(Vector4);
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x0400033C RID: 828
	public TextRenderer Target;

	// Token: 0x0400033D RID: 829
	private string targetText;

	// Token: 0x0400033E RID: 830
	public float ScrollSpeed = 1f;

	// Token: 0x0400033F RID: 831
	public float PauseTime = 1f;

	// Token: 0x04000340 RID: 832
	public float AreaWidth = 3f;

	// Token: 0x04000341 RID: 833
	public bool IgnoreTextChanges;
}
