using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000DA RID: 218
public class ButtonRolloverHandler : MonoBehaviour
{
	// Token: 0x0600055A RID: 1370 RVA: 0x00023E98 File Offset: 0x00022098
	public void Awake()
	{
		PassiveButton component = base.GetComponent<PassiveButton>();
		if (component != null)
		{
			component.OnMouseOver.AddListener(new UnityAction(this.DoMouseOver));
			component.OnMouseOut.AddListener(new UnityAction(this.DoMouseOut));
			return;
		}
		ButtonBehavior component2 = base.GetComponent<ButtonBehavior>();
		if (component2 != null)
		{
			component2.OnMouseOver.AddListener(new UnityAction(this.DoMouseOver));
			component2.OnMouseOut.AddListener(new UnityAction(this.DoMouseOut));
			return;
		}
		SlideBar component3 = base.GetComponent<SlideBar>();
		if (component3 != null)
		{
			component3.OnMouseOver.AddListener(new UnityAction(this.DoMouseOver));
			component3.OnMouseOut.AddListener(new UnityAction(this.DoMouseOut));
			return;
		}
		if (this.UseObjectsOutColor)
		{
			if (this.Target != null)
			{
				this.OutColor = this.Target.color;
			}
			if (this.TargetText != null)
			{
				this.OutColor = this.TargetText.Color;
			}
			if (this.TargetMesh != null)
			{
				this.OutColor = this.TargetMesh.material.color;
			}
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00023FCC File Offset: 0x000221CC
	public void DoMouseOver()
	{
		if (this.Target != null)
		{
			this.Target.color = this.OverColor;
		}
		if (this.TargetText != null)
		{
			this.TargetText.Color = this.OverColor;
		}
		if (this.TargetMesh != null)
		{
			this.TargetMesh.material.SetColor("_Color", this.OverColor);
		}
		if (this.HoverSound)
		{
			SoundManager.Instance.PlaySound(this.HoverSound, false, 1f);
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00024064 File Offset: 0x00022264
	public void DoMouseOut()
	{
		if (this.Target != null)
		{
			this.Target.color = this.OutColor;
		}
		if (this.TargetText != null)
		{
			this.TargetText.Color = this.OutColor;
		}
		if (this.TargetMesh != null)
		{
			this.TargetMesh.material.SetColor("_Color", this.OutColor);
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000240D8 File Offset: 0x000222D8
	public void SetDisabledColors()
	{
		this.ChangeOutColor(Color.gray);
		this.OverColor = Color.gray;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x000240F0 File Offset: 0x000222F0
	public void SetEnabledColors()
	{
		this.ChangeOutColor(Color.white);
		this.OverColor = Color.green;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00024108 File Offset: 0x00022308
	public void ChangeOutColor(Color color)
	{
		this.OutColor = color;
		this.DoMouseOut();
	}

	// Token: 0x0400060C RID: 1548
	public SpriteRenderer Target;

	// Token: 0x0400060D RID: 1549
	public TextRenderer TargetText;

	// Token: 0x0400060E RID: 1550
	public MeshRenderer TargetMesh;

	// Token: 0x0400060F RID: 1551
	public Color OverColor = Color.green;

	// Token: 0x04000610 RID: 1552
	public Color OutColor = Color.white;

	// Token: 0x04000611 RID: 1553
	public bool UseObjectsOutColor;

	// Token: 0x04000612 RID: 1554
	public AudioClip HoverSound;
}
