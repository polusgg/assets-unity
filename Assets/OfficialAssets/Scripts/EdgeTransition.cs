using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000086 RID: 134
public class EdgeTransition : MonoBehaviour
{
	// Token: 0x0600033D RID: 829 RVA: 0x0001523D File Offset: 0x0001343D
	public void Awake()
	{
		base.transform.localPosition = AspectPosition.ComputePosition(this.Alignment, this.ClosedPosition);
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0001525B File Offset: 0x0001345B
	public void Open()
	{
		base.gameObject.SetActive(true);
		base.StopAllCoroutines();
		base.StartCoroutine(this.CoOpen());
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0001527C File Offset: 0x0001347C
	private IEnumerator CoOpen()
	{
		Vector3 sourcePos = base.transform.localPosition;
		Vector3 targetPos = AspectPosition.ComputePosition(this.Alignment, this.OpenPosition);
		Vector3 localPosition = default(Vector3);
		for (float timer = 0f; timer < this.Duration; timer += Time.deltaTime)
		{
			localPosition = Vector3.Lerp(sourcePos, targetPos, timer / this.Duration);
			base.transform.localPosition = localPosition;
			yield return null;
		}
		base.transform.localPosition = targetPos;
		yield break;
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0001528B File Offset: 0x0001348B
	public void Close()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		base.StopAllCoroutines();
		base.StartCoroutine(this.CoClose());
	}

	// Token: 0x06000341 RID: 833 RVA: 0x000152A9 File Offset: 0x000134A9
	private IEnumerator CoClose()
	{
		Vector3 sourcePos = base.transform.localPosition;
		Vector3 targetPos = AspectPosition.ComputePosition(this.Alignment, this.ClosedPosition);
		Vector3 localPosition = default(Vector3);
		for (float timer = 0f; timer < this.Duration; timer += Time.deltaTime)
		{
			localPosition = Vector3.Lerp(sourcePos, targetPos, timer / this.Duration);
			base.transform.localPosition = localPosition;
			yield return null;
		}
		base.transform.localPosition = targetPos;
		this.OnClose.Invoke();
		yield break;
	}

	// Token: 0x040003B0 RID: 944
	public float Duration = 0.2f;

	// Token: 0x040003B1 RID: 945
	public Vector3 OpenPosition;

	// Token: 0x040003B2 RID: 946
	public Vector3 ClosedPosition;

	// Token: 0x040003B3 RID: 947
	public AspectPosition.EdgeAlignments Alignment;

	// Token: 0x040003B4 RID: 948
	public Button.ButtonClickedEvent OnClose = new Button.ButtonClickedEvent();
}
