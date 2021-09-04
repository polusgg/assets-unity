using System;
using UnityEngine;

// Token: 0x0200015F RID: 351
[RequireComponent(typeof(SpriteRenderer))]
public class ActionMapGlyphDisplay : MonoBehaviour
{
	// Token: 0x06000835 RID: 2101 RVA: 0x000359F8 File Offset: 0x00033BF8
	private void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
		this.UpdateGlyphDisplay();
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Combine(ActiveInputManager.CurrentInputSourceChanged, new Action(this.UpdateGlyphDisplay));
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00035A2C File Offset: 0x00033C2C
	private void OnDestroy()
	{
		ActiveInputManager.CurrentInputSourceChanged = (Action)Delegate.Remove(ActiveInputManager.CurrentInputSourceChanged, new Action(this.UpdateGlyphDisplay));
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00035A50 File Offset: 0x00033C50
	public void UpdateGlyphDisplay()
	{
		if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
		{
			if (this.sr)
			{
				this.sr.gameObject.SetActive(true);
				GlyphCollection.ErrorCode errorCode;
				Sprite sprite = GlyphCollection.FindGlyph((int)this.actionToDisplayMappedGlyphFor, out errorCode);
				if (errorCode != GlyphCollection.ErrorCode.NoController)
				{
					this.sr.sprite = sprite;
					return;
				}
				this.sr.gameObject.SetActive(false);
				return;
			}
		}
		else if (this.sr)
		{
			this.sr.gameObject.SetActive(false);
		}
	}

	// Token: 0x040009AE RID: 2478
	public RewiredConstsEnum.Action actionToDisplayMappedGlyphFor;

	// Token: 0x040009AF RID: 2479
	private SpriteRenderer sr;
}
