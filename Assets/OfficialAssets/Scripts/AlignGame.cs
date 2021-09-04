using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class AlignGame : Minigame
{
	// Token: 0x06000378 RID: 888 RVA: 0x00017090 File Offset: 0x00015290
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		float v = AlignGame.FromByte(this.MyNormTask.Data[base.ConsoleId]);
		Vector3 localPosition = this.col.transform.localPosition;
		localPosition.y = this.YRange.Lerp(v);
		float num = this.YRange.ReverseLerp(localPosition.y);
		localPosition.x = this.curve.Evaluate(num);
		this.col.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(20f, -20f, num));
		this.engine.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(45f, -45f, num));
		this.col.transform.localPosition = localPosition;
		this.centerline.material.SetColor("_Color", Color.red);
		this.engine.color = Color.red;
		this.guidelines[0].enabled = false;
		this.guidelines[1].enabled = false;
		base.SetupInput(true);
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000171C4 File Offset: 0x000153C4
	public void Update()
	{
		//this.centerline.material.SetTextureOffset("_MainTex", new Vector2(Time.time, 0f));
		//this.guidelines[0].material.SetTextureOffset("_MainTex", new Vector2(Time.time, 0f));
		//this.guidelines[1].material.SetTextureOffset("_MainTex", new Vector2(Time.time, 0f));
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//if (this.MyTask && this.MyNormTask.IsComplete)
		//{
		//	return;
		//}
		//Vector3 localPosition = this.col.transform.localPosition;
		//Player player = ReInput.players.GetPlayer(0);
		//Vector2 vector;
		//vector..ctor(player.GetAxis(13), player.GetAxis(14));
		//if (vector.magnitude > 0.01f)
		//{
		//	this.wasPushingJoystick = true;
		//	float num = this.YRange.ReverseLerp(localPosition.y);
		//	float num2 = Mathf.Clamp01(num + vector.y * Time.deltaTime);
		//	localPosition.y = this.YRange.Lerp(num2);
		//	localPosition.x = this.curve.Evaluate(num2);
		//	this.col.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(20f, -20f, num2));
		//	this.engine.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(45f, -45f, num2));
		//	this.MyNormTask.Data[base.ConsoleId] = AlignGame.ToByte(num2);
		//	this.centerline.material.SetColor("_Color", AlignGame.ShouldComplete(this.MyNormTask.Data[base.ConsoleId]) ? Color.green : Color.red);
		//	if (Mathf.Abs(num2 - num) > 0.001f)
		//	{
		//		this.pulseTimer += Time.deltaTime * 25f;
		//		int num3 = (int)this.pulseTimer % 3;
		//		if (num3 > 1)
		//		{
		//			if (num3 == 2)
		//			{
		//				this.engine.color = Color.clear;
		//			}
		//		}
		//		else
		//		{
		//			this.engine.color = Color.red;
		//		}
		//	}
		//	else
		//	{
		//		this.engine.color = Color.red;
		//	}
		//}
		//else if (this.wasPushingJoystick)
		//{
		//	this.wasPushingJoystick = false;
		//	if (AlignGame.ShouldComplete(this.MyNormTask.Data[base.ConsoleId]))
		//	{
		//		this.MyNormTask.NextStep();
		//		this.MyNormTask.Data[base.ConsoleId + 2] = 1;
		//		base.StartCoroutine(this.LockEngine());
		//		base.StartCoroutine(base.CoStartClose(0.75f));
		//	}
		//	else
		//	{
		//		this.engine.color = Color.red;
		//	}
		//}
		//this.myController.Update();
		//switch (this.myController.CheckDrag(this.col))
		//{
		//case DragState.TouchStart:
		//	this.pulseTimer = 0f;
		//	break;
		//case DragState.Dragging:
		//{
		//	Vector2 vector2 = this.myController.DragPosition - base.transform.position;
		//	float num4 = this.YRange.ReverseLerp(localPosition.y);
		//	localPosition.y = this.YRange.Clamp(vector2.y);
		//	float num5 = this.YRange.ReverseLerp(localPosition.y);
		//	localPosition.x = this.curve.Evaluate(num5);
		//	this.col.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(20f, -20f, num5));
		//	this.engine.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(45f, -45f, num5));
		//	this.MyNormTask.Data[base.ConsoleId] = AlignGame.ToByte(num5);
		//	this.centerline.material.SetColor("_Color", AlignGame.ShouldComplete(this.MyNormTask.Data[base.ConsoleId]) ? Color.green : Color.red);
		//	if (Mathf.Abs(num5 - num4) > 0.001f)
		//	{
		//		this.pulseTimer += Time.deltaTime * 25f;
		//		int num3 = (int)this.pulseTimer % 3;
		//		if (num3 > 1)
		//		{
		//			if (num3 == 2)
		//			{
		//				this.engine.color = Color.clear;
		//			}
		//		}
		//		else
		//		{
		//			this.engine.color = Color.red;
		//		}
		//	}
		//	else
		//	{
		//		this.engine.color = Color.red;
		//	}
		//	break;
		//}
		//case DragState.Released:
		//	if (AlignGame.ShouldComplete(this.MyNormTask.Data[base.ConsoleId]))
		//	{
		//		this.MyNormTask.NextStep();
		//		this.MyNormTask.Data[base.ConsoleId + 2] = 1;
		//		base.StartCoroutine(this.LockEngine());
		//		base.StartCoroutine(base.CoStartClose(0.75f));
		//	}
		//	else
		//	{
		//		this.engine.color = Color.red;
		//	}
		//	break;
		//}
		//this.col.transform.localPosition = localPosition;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00017717 File Offset: 0x00015917
	private IEnumerator LockEngine()
	{
		int num;
		for (int i = 0; i < 3; i = num)
		{
			this.guidelines[0].enabled = true;
			this.guidelines[1].enabled = true;
			yield return new WaitForSeconds(0.1f);
			this.guidelines[0].enabled = false;
			this.guidelines[1].enabled = false;
			yield return new WaitForSeconds(0.1f);
			num = i + 1;
		}
		Color green = new Color(0f, 0.7f, 0f);
		yield return new WaitForLerp(1f, delegate(float t)
		{
			this.engine.color = Color.Lerp(Color.white, green, t);
		});
		this.guidelines[0].enabled = true;
		this.guidelines[1].enabled = true;
		yield break;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00017726 File Offset: 0x00015926
	public static float FromByte(byte b)
	{
		return (float)b / 256f;
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00017730 File Offset: 0x00015930
	public static byte ToByte(float y)
	{
		return (byte)(y * 255f);
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0001773A File Offset: 0x0001593A
	public static bool IsSuccess(byte b)
	{
		return b > 0;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00017740 File Offset: 0x00015940
	public static bool ShouldComplete(byte b)
	{
		return (float)Mathf.Abs((int)(b - 128)) < 12.75f;
	}

	// Token: 0x0400041A RID: 1050
	private Controller myController = new Controller();

	// Token: 0x0400041B RID: 1051
	public FloatRange YRange = new FloatRange(-0.425f, 0.425f);

	// Token: 0x0400041C RID: 1052
	public AnimationCurve curve;

	// Token: 0x0400041D RID: 1053
	public LineRenderer centerline;

	// Token: 0x0400041E RID: 1054
	public LineRenderer[] guidelines;

	// Token: 0x0400041F RID: 1055
	public SpriteRenderer engine;

	// Token: 0x04000420 RID: 1056
	public Collider2D col;

	// Token: 0x04000421 RID: 1057
	private float pulseTimer;

	// Token: 0x04000422 RID: 1058
	private bool wasPushingJoystick;
}
