using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E7 RID: 231
public class SlideOpen : MonoBehaviour
{
	// Token: 0x060005B6 RID: 1462 RVA: 0x00025604 File Offset: 0x00023804
	public void Awake()
	{
		Camera camera = this.parentCam ? this.parentCam : Camera.main;
		float orthographicSize = camera.orthographicSize;
		Rect safeArea = Screen.safeArea;
		float aspect = Mathf.Min(camera.aspect, safeArea.width / safeArea.height);
		this.closedPosition = AspectPosition.ComputePosition(AspectPosition.EdgeAlignments.Left, new Vector3(-3.2f, 0f, 0f), orthographicSize, aspect);
		base.transform.localPosition = this.closedPosition;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00025685 File Offset: 0x00023885
	public void Toggle()
	{
		base.StopAllCoroutines();
		if (this.isOpen)
		{
			this.Close();
			return;
		}
		this.Open();
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x000256A2 File Offset: 0x000238A2
	public void Close()
	{
		if (this.isOpen)
		{
			base.StartCoroutine(this.AnimateClose());
		}
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x000256B9 File Offset: 0x000238B9
	public void Open()
	{
		if (!this.isOpen)
		{
			base.StartCoroutine(this.AnimateOpen());
		}
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x000256D0 File Offset: 0x000238D0
	private IEnumerator AnimateClose()
	{
		for (float t = 0f; t < this.duration; t += Time.deltaTime)
		{
			float num = t / this.duration;
			Vector3 positionVector = Vector3.Lerp(this.openPosition, this.closedPosition, num);
			this.SetPositionVector(positionVector);
			yield return null;
		}
		this.SetPositionVector(this.closedPosition);
		this.isOpen = false;
		yield break;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x000256DF File Offset: 0x000238DF
	private IEnumerator AnimateOpen()
	{
		for (float t = 0f; t < this.duration; t += Time.deltaTime)
		{
			float num = t / this.duration;
			Vector3 positionVector = Vector3.Lerp(this.closedPosition, this.openPosition, num);
			this.SetPositionVector(positionVector);
			yield return null;
		}
		this.isOpen = true;
		this.SetPositionVector(this.openPosition);
		yield break;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x000256EE File Offset: 0x000238EE
	private void SetPositionVector(Vector3 pos)
	{
		base.transform.localPosition = pos;
	}

	// Token: 0x0400065E RID: 1630
	public float duration = 0.2f;

	// Token: 0x0400065F RID: 1631
	public Vector3 closedPosition = Vector3.zero;

	// Token: 0x04000660 RID: 1632
	public Vector3 openPosition = Vector3.zero;

	// Token: 0x04000661 RID: 1633
	public Button.ButtonClickedEvent OnClose = new Button.ButtonClickedEvent();

	// Token: 0x04000662 RID: 1634
	public Camera parentCam;

	// Token: 0x04000663 RID: 1635
	public bool isOpen;
}
