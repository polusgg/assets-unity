using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class BanButton : MonoBehaviour
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001B6DA File Offset: 0x000198DA
	// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001B6E2 File Offset: 0x000198E2
	public BanMenu Parent { get; set; }

	// Token: 0x06000442 RID: 1090 RVA: 0x0001B6EB File Offset: 0x000198EB
	public void Start()
	{
		this.Background.SetCooldownNormalizedUvs();
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0001B6F8 File Offset: 0x000198F8
	public void Select()
	{
		this.Background.color = new Color(1f, 1f, 1f, 1f);
		this.Parent.Select(this.TargetClientId);
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0001B72F File Offset: 0x0001992F
	public void Unselect()
	{
		this.Background.color = new Color(0.3f, 0.3f, 0.3f, 0.5f);
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x0001B755 File Offset: 0x00019955
	public void SetVotes(int newVotes)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.CoSetVotes(this.numVotes, newVotes));
		this.numVotes = newVotes;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0001B778 File Offset: 0x00019978
	private IEnumerator CoSetVotes(int oldNum, int newNum)
	{
		float num = (float)oldNum / 3f;
		float end = (float)newNum / 3f;
		for (float timer = 0f; timer < 0.2f; timer += Time.deltaTime)
		{
			this.Background.material.SetFloat("_Percent", Mathf.SmoothStep(end, end, timer / 0.2f));
			yield return null;
		}
		this.Background.material.SetFloat("_Percent", end);
		yield break;
	}

	// Token: 0x040004FF RID: 1279
	public TextRenderer NameText;

	// Token: 0x04000500 RID: 1280
	public SpriteRenderer Background;

	// Token: 0x04000501 RID: 1281
	public int TargetClientId;

	// Token: 0x04000502 RID: 1282
	public int numVotes;
}
