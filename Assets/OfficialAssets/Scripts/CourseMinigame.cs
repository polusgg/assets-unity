using System;
using System.Runtime.InteropServices;
//using Rewired;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class CourseMinigame : Minigame
{
	// Token: 0x06000794 RID: 1940 RVA: 0x000300F8 File Offset: 0x0002E2F8
	public override void Begin(PlayerTask task)
	{
		//base.Begin(task);
		//this.PathPoints = new Vector3[this.NumPoints];
		//this.Stars = new CourseStarBehaviour[this.NumPoints];
		//this.Dots = new SpriteRenderer[this.NumPoints];
		//for (int i = 0; i < this.PathPoints.Length; i++)
		//{
		//	this.PathPoints[i].x = this.XRange.Lerp((float)i / ((float)this.PathPoints.Length - 1f));
		//	do
		//	{
		//		this.PathPoints[i].y = this.YRange.Next();
		//	}
		//	while (i > 0 && Mathf.Abs(this.PathPoints[i - 1].y - this.PathPoints[i].y) < this.YRange.Width / 4f);
		//	this.Dots[i] = Object.Instantiate<SpriteRenderer>(this.DotPrefab, base.transform);
		//	this.Dots[i].transform.localPosition = this.PathPoints[i];
		//	if (i == 0)
		//	{
		//		this.Dots[i].sprite = this.DotLight;
		//	}
		//	else
		//	{
		//		if (i == 1)
		//		{
		//			this.Ship.transform.localPosition = this.PathPoints[0];
		//			this.Ship.transform.eulerAngles = new Vector3(0f, 0f, Vector2.up.AngleSigned(this.PathPoints[1] - this.PathPoints[0]));
		//		}
		//		this.Stars[i] = Object.Instantiate<CourseStarBehaviour>(this.StarPrefab, base.transform);
		//		this.Stars[i].transform.localPosition = this.PathPoints[i];
		//		if (i == this.PathPoints.Length - 1)
		//		{
		//			this.Destination.transform.localPosition = this.PathPoints[i];
		//		}
		//	}
		//}
		//this.Path.positionCount = this.PathPoints.Length;
		//this.Path.SetPositions(this.PathPoints);
		//base.SetupInput(true);
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0003032C File Offset: 0x0002E52C
	public void FixedUpdate()
	{
		//float num = this.Converter.GetFloat(this.MyNormTask.Data);
		//int num2 = (int)num;
		//Vector2 vector = this.PathPoints[num2];
		//this.myController.Update();
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	Vector2 vector2;
		//	vector2..ctor(player.GetAxis(13), player.GetAxis(14));
		//	float magnitude = vector2.magnitude;
		//	if (magnitude > 0.1f)
		//	{
		//		if (num < (float)(this.PathPoints.Length - 1))
		//		{
		//			Vector2 vector3 = this.PathPoints[num2 + 1] - vector;
		//			Vector2 normalized = vector3.normalized;
		//			Vector2 normalized2 = vector2.normalized;
		//			float num3 = Vector2.Dot(normalized, normalized2);
		//			if (num3 > 0.7f)
		//			{
		//				num += magnitude * num3 * Time.deltaTime * 1.75f;
		//			}
		//			else
		//			{
		//				num = Mathf.Max((float)num2, Mathf.Lerp(num, (float)num2, Time.deltaTime * 5f));
		//			}
		//			this.Ship.transform.eulerAngles = new Vector3(0f, 0f, Vector2.up.AngleSigned(vector3));
		//		}
		//	}
		//	else
		//	{
		//		num = Mathf.Max((float)num2, Mathf.Lerp(num, (float)num2, Time.deltaTime * 5f));
		//	}
		//	if (num < (float)(this.PathPoints.Length - 1))
		//	{
		//		float num4 = num - Mathf.Floor(num);
		//		int num5 = (int)num;
		//		Vector3 localPosition = Vector2.Lerp(this.PathPoints[num5], this.PathPoints[num5 + 1], num4);
		//		localPosition.z = -1f;
		//		this.Ship.transform.localPosition = localPosition;
		//	}
		//	else
		//	{
		//		Vector3 localPosition2 = this.PathPoints[this.PathPoints.Length - 1];
		//		localPosition2.z = -1f;
		//		this.Ship.transform.localPosition = localPosition2;
		//	}
		//}
		//else
		//{
		//	DragState dragState = this.myController.CheckDrag(this.Ship);
		//	if (dragState != DragState.NoTouch)
		//	{
		//		if (dragState == DragState.Dragging)
		//		{
		//			if (num < (float)(this.PathPoints.Length - 1))
		//			{
		//				Vector2 vector4 = this.PathPoints[num2 + 1] - vector;
		//				Vector2 vector5;
		//				vector5..ctor(1f, vector4.y / vector4.x);
		//				Vector2 vector6 = base.transform.InverseTransformPoint(this.myController.DragPosition) - vector;
		//				if (vector6.x > 0f)
		//				{
		//					Vector2 vector7 = vector5 * vector6.x;
		//					if (Mathf.Abs(vector7.y - vector6.y) < 0.5f)
		//					{
		//						num = (float)num2 + Mathf.Min(1f, vector6.x / vector4.x);
		//						Vector3 localPosition3 = vector7 + vector;
		//						localPosition3.z = -1f;
		//						this.Ship.transform.localPosition = localPosition3;
		//						this.Ship.transform.localPosition = localPosition3;
		//						this.Ship.transform.eulerAngles = new Vector3(0f, 0f, Vector2.up.AngleSigned(vector4));
		//					}
		//					else
		//					{
		//						this.myController.Reset();
		//					}
		//				}
		//			}
		//			else
		//			{
		//				Vector3 localPosition4 = this.PathPoints[this.PathPoints.Length - 1];
		//				localPosition4.z = -1f;
		//				this.Ship.transform.localPosition = localPosition4;
		//			}
		//		}
		//	}
		//	else if (num < (float)(this.PathPoints.Length - 1))
		//	{
		//		Vector2 vector8 = this.PathPoints[num2 + 1] - vector;
		//		Vector2 vector9 = new Vector2(1f, vector8.y / vector8.x);
		//		num = Mathf.Max((float)num2, Mathf.Lerp(num, (float)num2, Time.deltaTime * 5f));
		//		Vector3 localPosition5 = vector9 * (num - (float)num2) + vector;
		//		localPosition5.z = -1f;
		//		this.Ship.transform.localPosition = localPosition5;
		//	}
		//	else
		//	{
		//		Vector3 localPosition6 = this.PathPoints[this.PathPoints.Length - 1];
		//		localPosition6.z = -1f;
		//		this.Ship.transform.localPosition = localPosition6;
		//	}
		//}
		//if ((int)num > num2 && this.Stars[num2 + 1])
		//{
		//	Object.Destroy(this.Stars[num2 + 1].gameObject);
		//	this.Dots[num2 + 1].sprite = this.DotLight;
		//	if (num2 == this.PathPoints.Length - 2)
		//	{
		//		if (Constants.ShouldPlaySfx())
		//		{
		//			SoundManager.Instance.PlaySound(this.SetCourseLastSound, false, 1f).volume = 0.7f;
		//		}
		//		this.Destination.Speed *= 5f;
		//		this.MyNormTask.NextStep();
		//		base.StartCoroutine(base.CoStartClose(0.75f));
		//	}
		//	else if (Constants.ShouldPlaySfx())
		//	{
		//		SoundManager.Instance.PlaySound(this.SetCourseSound, false, 1f).volume = 0.7f;
		//	}
		//}
		//this.Converter.GetBytes(num, this.MyNormTask.Data);
		//this.SetLineDivision(num);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0003088C File Offset: 0x0002EA8C
	private void SetLineDivision(float curVec)
	{
		//int num = (int)curVec;
		//float num2 = 0f;
		//int num3 = 0;
		//while ((float)num3 <= curVec && num3 < this.PathPoints.Length - 1)
		//{
		//	float num4 = Vector2.Distance(this.PathPoints[num3], this.PathPoints[num3 + 1]);
		//	if (num3 == num)
		//	{
		//		num4 *= curVec - (float)num3;
		//	}
		//	num2 += num4;
		//	num3++;
		//}
		//this.lineTimer -= Time.fixedDeltaTime;
		//Vector2 vector;
		//vector..ctor(this.lineTimer, 0f);
		//this.Path.material.SetTextureOffset("_MainTex", vector);
		//this.Path.material.SetTextureOffset("_AltTex", vector);
		//this.Path.material.SetFloat("_Perc", num2 + this.lineTimer / 8f);
	}

	// Token: 0x0400088E RID: 2190
	public CourseStarBehaviour StarPrefab;

	// Token: 0x0400088F RID: 2191
	public CourseStarBehaviour[] Stars;

	// Token: 0x04000890 RID: 2192
	public SpriteRenderer DotPrefab;

	// Token: 0x04000891 RID: 2193
	public Sprite DotLight;

	// Token: 0x04000892 RID: 2194
	public SpriteRenderer[] Dots;

	// Token: 0x04000893 RID: 2195
	public Collider2D Ship;

	// Token: 0x04000894 RID: 2196
	public CourseStarBehaviour Destination;

	// Token: 0x04000895 RID: 2197
	public Vector3[] PathPoints;

	// Token: 0x04000896 RID: 2198
	public int NumPoints;

	// Token: 0x04000897 RID: 2199
	public FloatRange XRange;

	// Token: 0x04000898 RID: 2200
	public FloatRange YRange;

	// Token: 0x04000899 RID: 2201
	public LineRenderer Path;

	// Token: 0x0400089A RID: 2202
	public Controller myController = new Controller();

	// Token: 0x0400089B RID: 2203
	public float lineTimer;

	// Token: 0x0400089C RID: 2204
	private CourseMinigame.UIntFloat Converter;

	// Token: 0x0400089D RID: 2205
	public AudioClip SetCourseSound;

	// Token: 0x0400089E RID: 2206
	public AudioClip SetCourseLastSound;

	// Token: 0x020003C6 RID: 966
	[StructLayout(LayoutKind.Explicit)]
	private struct UIntFloat
	{
		// Token: 0x06001866 RID: 6246 RVA: 0x000741C4 File Offset: 0x000723C4
		public float GetFloat(byte[] bytes)
		{
			this.IntValue = ((int)bytes[0] | (int)bytes[1] << 8 | (int)bytes[2] << 16 | (int)bytes[3] << 24);
			return this.FloatValue;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000741EC File Offset: 0x000723EC
		public void GetBytes(float value, byte[] bytes)
		{
			this.FloatValue = value;
			bytes[0] = (byte)(this.IntValue & 255);
			bytes[1] = (byte)(this.IntValue >> 8 & 255);
			bytes[2] = (byte)(this.IntValue >> 16 & 255);
			bytes[3] = (byte)(this.IntValue >> 24 & 255);
		}

		// Token: 0x04001A6C RID: 6764
		[FieldOffset(0)]
		public float FloatValue;

		// Token: 0x04001A6D RID: 6765
		[FieldOffset(0)]
		public int IntValue;
	}
}
