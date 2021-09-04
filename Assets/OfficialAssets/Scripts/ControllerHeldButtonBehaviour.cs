using System;
//using Rewired;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016E RID: 366
public class ControllerHeldButtonBehaviour : MonoBehaviour
{
	// Token: 0x06000868 RID: 2152 RVA: 0x00036B63 File Offset: 0x00034D63
	private void Start()
	{
		//this.player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00036B78 File Offset: 0x00034D78
	private void Update()
	{
		//if (this.player.GetButton((int)this.Action))
		//{
		//	if (!this.alreadyPressed)
		//	{
		//		this.holdTimer += Time.unscaledDeltaTime;
		//		if (this.targetCooldownSprite)
		//		{
		//			float num = Mathf.Clamp01(this.holdTimer / this.holdDuration);
		//			this.targetCooldownSprite.SetCooldownNormalizedUvs();
		//			this.targetCooldownSprite.material.SetFloat("_Percent", 1f - num);
		//		}
		//		if (this.holdTimer >= this.holdDuration)
		//		{
		//			this.alreadyPressed = true;
		//			ButtonBehavior component = base.GetComponent<ButtonBehavior>();
		//			if (component)
		//			{
		//				Button.ButtonClickedEvent onClick = component.OnClick;
		//				if (onClick == null)
		//				{
		//					return;
		//				}
		//				onClick.Invoke();
		//				return;
		//			}
		//			else
		//			{
		//				PassiveButton component2 = base.GetComponent<PassiveButton>();
		//				if (component2)
		//				{
		//					Button.ButtonClickedEvent onClick2 = component2.OnClick;
		//					if (onClick2 == null)
		//					{
		//						return;
		//					}
		//					onClick2.Invoke();
		//					return;
		//				}
		//			}
		//		}
		//	}
		//}
		//else
		//{
		//	if (this.holdTimer > 0f)
		//	{
		//		this.targetCooldownSprite.material.SetFloat("_Percent", 0f);
		//	}
		//	this.alreadyPressed = false;
		//	this.holdTimer = 0f;
		//}
	}

	// Token: 0x040009CA RID: 2506
	public RewiredConstsEnum.Action Action;

	// Token: 0x040009CB RID: 2507
	//private Player player;

	// Token: 0x040009CC RID: 2508
	public SpriteRenderer targetCooldownSprite;

	// Token: 0x040009CD RID: 2509
	public float holdDuration;

	// Token: 0x040009CE RID: 2510
	private float holdTimer;

	// Token: 0x040009CF RID: 2511
	private bool alreadyPressed;
}
