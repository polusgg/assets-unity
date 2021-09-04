using System;
//using Rewired;
using UnityEngine;

// Token: 0x0200017E RID: 382
[RequireComponent(typeof(Scroller))]
public class ManualScrollHelper : MonoBehaviour
{
	// Token: 0x060008B7 RID: 2231 RVA: 0x00038CCA File Offset: 0x00036ECA
	private void Start()
	{
		this.scroller = base.GetComponent<Scroller>();
		if (!this.scroller)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00038CEC File Offset: 0x00036EEC
	private void Update()
	{
		//Vector3 localPosition = this.scroller.Inner.transform.localPosition;
		//Player player = ReInput.players.GetPlayer(0);
		//if (this.doVertical)
		//{
		//	float axisRaw = player.GetAxisRaw((int)this.verticalAxis);
		//	localPosition.y -= axisRaw * Time.deltaTime * this.scrollSpeed;
		//}
		//if (this.doHorizontal)
		//{
		//	float axisRaw2 = player.GetAxisRaw((int)this.horizontalAxis);
		//	localPosition.x -= axisRaw2 * Time.deltaTime * this.scrollSpeed;
		//}
		//localPosition.x = this.scroller.XBounds.Clamp(localPosition.x);
		//localPosition.y = this.scroller.YBounds.Clamp(localPosition.y);
		//this.scroller.Inner.transform.localPosition = localPosition;
	}

	// Token: 0x04000A0A RID: 2570
	public bool doVertical = true;

	// Token: 0x04000A0B RID: 2571
	public RewiredConstsEnum.Action verticalAxis;

	// Token: 0x04000A0C RID: 2572
	public bool doHorizontal;

	// Token: 0x04000A0D RID: 2573
	public RewiredConstsEnum.Action horizontalAxis;

	// Token: 0x04000A0E RID: 2574
	public float scrollSpeed = 3f;

	// Token: 0x04000A0F RID: 2575
	private Scroller scroller;
}
