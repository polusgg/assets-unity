using System;
using System.Collections;
//using Rewired;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class AcceptDivertPowerGame : Minigame
{
	// Token: 0x06000343 RID: 835 RVA: 0x000152D8 File Offset: 0x000134D8
	public void Start()
	{
		this.LeftWires = this.LeftWireParent.GetComponentsInChildren<LineRenderer>();
		this.RightWires = this.RightWireParent.GetComponentsInChildren<LineRenderer>();
		for (int i = 0; i < this.LeftWires.Length; i++)
		{
			this.LeftWires[i].material.SetColor("_Color", Color.yellow);
		}
		base.SetupInput(true);
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0001533D File Offset: 0x0001353D
	public void DoSwitch()
	{
		if (this.done)
		{
			return;
		}
		this.done = true;
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.SwitchSound, false, 1f);
		}
		base.StartCoroutine(this.CoDoSwitch());
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0001537A File Offset: 0x0001357A
	private IEnumerator CoDoSwitch()
	{
		yield return new WaitForLerp(0.25f, delegate(float t)
		{
			this.Switch.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, 90f, t));
		});
		this.LeftWires[0].SetPosition(1, new Vector3(1.265f, 0f, 0f));
		for (int i = 0; i < this.RightWires.Length; i++)
		{
			this.RightWires[i].enabled = true;
			this.RightWires[i].material.SetColor("_Color", Color.yellow);
		}
		for (int j = 0; j < this.LeftWires.Length; j++)
		{
			this.LeftWires[j].material.SetColor("_Color", Color.yellow);
		}
		if (this.MyNormTask)
		{
			this.MyNormTask.NextStep();
		}
		base.StartCoroutine(base.CoStartClose(0.75f));
		yield break;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0001538C File Offset: 0x0001358C
	public void Update()
	{
		//if (!this.done)
		//{
		//	Vector2 axis2DRaw = ReInput.players.GetPlayer(0).GetAxis2DRaw(13, 14);
		//	if (axis2DRaw.sqrMagnitude > 0.9f)
		//	{
		//		Vector2 normalized = axis2DRaw.normalized;
		//		if (this.prevHadInput)
		//		{
		//			float num = Vector2.SignedAngle(this.prevStickDir, normalized);
		//			this.rotateAngle += num;
		//			if (Mathf.Abs(this.rotateAngle) > 45f)
		//			{
		//				this.DoSwitch();
		//			}
		//		}
		//		this.prevStickDir = normalized;
		//		this.prevHadInput = true;
		//	}
		//	else
		//	{
		//		this.rotateAngle = 0f;
		//		this.prevHadInput = false;
		//	}
		//}
		//for (int i = 0; i < this.LeftWires.Length; i++)
		//{
		//	Vector2 textureOffset = this.LeftWires[i].material.GetTextureOffset("_MainTex");
		//	textureOffset.x -= Time.deltaTime * 3f;
		//	this.LeftWires[i].material.SetTextureOffset("_MainTex", textureOffset);
		//}
		//for (int j = 0; j < this.RightWires.Length; j++)
		//{
		//	Vector2 textureOffset2 = this.RightWires[j].material.GetTextureOffset("_MainTex");
		//	textureOffset2.x += Time.deltaTime * 3f;
		//	this.RightWires[j].material.SetTextureOffset("_MainTex", textureOffset2);
		//}
	}

	// Token: 0x040003B5 RID: 949
	private LineRenderer[] LeftWires;

	// Token: 0x040003B6 RID: 950
	private LineRenderer[] RightWires;

	// Token: 0x040003B7 RID: 951
	public GameObject RightWireParent;

	// Token: 0x040003B8 RID: 952
	public GameObject LeftWireParent;

	// Token: 0x040003B9 RID: 953
	public SpriteRenderer Switch;

	// Token: 0x040003BA RID: 954
	public AudioClip SwitchSound;

	// Token: 0x040003BB RID: 955
	private bool done;

	// Token: 0x040003BC RID: 956
	private bool prevHadInput;

	// Token: 0x040003BD RID: 957
	private float rotateAngle;

	// Token: 0x040003BE RID: 958
	private Vector2 prevStickDir;
}
