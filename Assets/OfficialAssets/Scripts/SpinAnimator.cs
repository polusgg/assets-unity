using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class SpinAnimator : MonoBehaviour
{
	// Token: 0x06000CA0 RID: 3232 RVA: 0x0004DE9C File Offset: 0x0004C09C
	private void Update()
	{
		if (this.curState == SpinAnimator.States.Spinning)
		{
			base.transform.Rotate(0f, 0f, this.Speed * Time.deltaTime);
		}
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0004DEC8 File Offset: 0x0004C0C8
	public void Appear()
	{
		if (this.curState != SpinAnimator.States.Invisible)
		{
			return;
		}
		this.curState = SpinAnimator.States.Visible;
		base.gameObject.SetActive(true);
		this.inputGlyph.SetActive(true);
		base.StopAllCoroutines();
		base.StartCoroutine(Effects.ScaleIn(base.transform, 0f, 1f, 0.125f));
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x0004DF25 File Offset: 0x0004C125
	public void Disappear()
	{
		if (this.curState == SpinAnimator.States.Invisible)
		{
			return;
		}
		this.curState = SpinAnimator.States.Invisible;
		base.StopAllCoroutines();
		base.StartCoroutine(this.CoDisappear());
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x0004DF4B File Offset: 0x0004C14B
	private IEnumerator CoDisappear()
	{
		yield return Effects.ScaleIn(base.transform, 1f, 0f, 0.125f);
		base.gameObject.SetActive(false);
		this.inputGlyph.SetActive(false);
		yield break;
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x0004DF5C File Offset: 0x0004C15C
	public void StartPulse()
	{
		if (this.curState == SpinAnimator.States.Pulsing)
		{
			return;
		}
		this.curState = SpinAnimator.States.Pulsing;
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		base.StartCoroutine(Effects.CycleColors(component, Color.white, Color.green, 1f, float.MaxValue));
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x0004DFA2 File Offset: 0x0004C1A2
	internal void Play()
	{
		this.curState = SpinAnimator.States.Spinning;
		this.inputGlyph.SetActive(false);
	}

	// Token: 0x04000E25 RID: 3621
	public float Speed = 60f;

	// Token: 0x04000E26 RID: 3622
	public GameObject inputGlyph;

	// Token: 0x04000E27 RID: 3623
	private SpinAnimator.States curState;

	// Token: 0x0200044D RID: 1101
	private enum States
	{
		// Token: 0x04001C46 RID: 7238
		Visible,
		// Token: 0x04001C47 RID: 7239
		Invisible,
		// Token: 0x04001C48 RID: 7240
		Spinning,
		// Token: 0x04001C49 RID: 7241
		Pulsing
	}
}
