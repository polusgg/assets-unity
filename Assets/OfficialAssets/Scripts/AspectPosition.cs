using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class AspectPosition : MonoBehaviour
{
	// Token: 0x06000435 RID: 1077 RVA: 0x0001B46D File Offset: 0x0001966D
	public void Update()
	{
		if (this.updateAlways)
		{
			this.AdjustPosition();
		}
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0001B47D File Offset: 0x0001967D
	private void OnEnable()
	{
		this.AdjustPosition();
		ResolutionManager.ResolutionChanged += this.AdjustPosition;
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x0001B496 File Offset: 0x00019696
	private void OnDisable()
	{
		ResolutionManager.ResolutionChanged -= this.AdjustPosition;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0001B4AC File Offset: 0x000196AC
	public void AdjustPosition()
	{
		Rect safeArea = Screen.safeArea;
		float aspect = Mathf.Min((this.parentCam ? this.parentCam : Camera.main).aspect, safeArea.width / safeArea.height);
		this.AdjustPosition(aspect);
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x0001B4FC File Offset: 0x000196FC
	public void AdjustPosition(float aspect)
	{
		float orthographicSize = (this.parentCam ? this.parentCam : Camera.main).orthographicSize;
		base.transform.localPosition = AspectPosition.ComputePosition(this.Alignment, this.DistanceFromEdge, orthographicSize, aspect);
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0001B548 File Offset: 0x00019748
	public static Vector3 ComputePosition(AspectPosition.EdgeAlignments alignment, Vector3 relativePos)
	{
		Rect safeArea = Screen.safeArea;
		Camera main = Camera.main;
		float aspect = Mathf.Min(main.aspect, safeArea.width / safeArea.height);
		float orthographicSize = main.orthographicSize;
		return AspectPosition.ComputePosition(alignment, relativePos, orthographicSize, aspect);
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0001B58C File Offset: 0x0001978C
	public static Vector3 ComputePosition(AspectPosition.EdgeAlignments alignment, Vector3 relativePos, float cHeight, float aspect)
	{
		float num = cHeight * aspect;
		Vector3 vector = relativePos;
		if ((alignment & AspectPosition.EdgeAlignments.Left) != (AspectPosition.EdgeAlignments)0)
		{
			vector.x -= num;
		}
		else if ((alignment & AspectPosition.EdgeAlignments.Right) != (AspectPosition.EdgeAlignments)0)
		{
			vector.x = num - vector.x;
		}
		if ((alignment & AspectPosition.EdgeAlignments.Bottom) != (AspectPosition.EdgeAlignments)0)
		{
			vector.y -= cHeight;
		}
		else if ((alignment & AspectPosition.EdgeAlignments.Top) != (AspectPosition.EdgeAlignments)0)
		{
			vector.y = cHeight - vector.y;
		}
		return vector;
	}

	// Token: 0x040004F3 RID: 1267
	public Camera parentCam;

	// Token: 0x040004F4 RID: 1268
	private const int LeftFlag = 1;

	// Token: 0x040004F5 RID: 1269
	private const int RightFlag = 2;

	// Token: 0x040004F6 RID: 1270
	private const int BottomFlag = 4;

	// Token: 0x040004F7 RID: 1271
	private const int TopFlag = 8;

	// Token: 0x040004F8 RID: 1272
	public bool updateAlways;

	// Token: 0x040004F9 RID: 1273
	public Vector3 DistanceFromEdge;

	// Token: 0x040004FA RID: 1274
	public AspectPosition.EdgeAlignments Alignment;

	// Token: 0x0200034E RID: 846
	public enum EdgeAlignments
	{
		// Token: 0x040018AB RID: 6315
		RightBottom = 6,
		// Token: 0x040018AC RID: 6316
		LeftBottom = 5,
		// Token: 0x040018AD RID: 6317
		RightTop = 10,
		// Token: 0x040018AE RID: 6318
		Left = 1,
		// Token: 0x040018AF RID: 6319
		Right,
		// Token: 0x040018B0 RID: 6320
		Top = 8,
		// Token: 0x040018B1 RID: 6321
		Bottom = 4,
		// Token: 0x040018B2 RID: 6322
		LeftTop = 9
	}
}
