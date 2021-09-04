using System;
using UnityEngine;

namespace PowerTools
{
	// Token: 0x02000284 RID: 644
	public class SpriteAnimNodeSync : MonoBehaviour
	{
		// Token: 0x06001243 RID: 4675 RVA: 0x0005F5DC File Offset: 0x0005D7DC
		public void LateUpdate()
		{
			bool flag = false;
			if (this.ParentRenderer)
			{
				flag = this.ParentRenderer.flipX;
				if (this.Renderer)
				{
					this.Renderer.flipX = this.ParentRenderer.flipX;
				}
				else
				{
					// flag = flag;
				}
			}
			else if (this.Renderer)
			{
				flag = this.Renderer.flipX;
			}
			Vector3 localPosition = base.transform.localPosition;
			Vector3 localPosition2 = this.Parent.GetPosition(this.NodeId, false);
			localPosition.x = localPosition2.x;
			localPosition.y = localPosition2.y;
			base.transform.localPosition = localPosition;
			float angle = this.Parent.GetAngle(this.NodeId);
			if (flag)
			{
				base.transform.eulerAngles = new Vector3(0f, 0f, -angle);
				return;
			}
			base.transform.eulerAngles = new Vector3(0f, 0f, angle);
		}

		// Token: 0x040014A9 RID: 5289
		public int NodeId;

		// Token: 0x040014AA RID: 5290
		public SpriteAnimNodes Parent;

		// Token: 0x040014AB RID: 5291
		public SpriteRenderer ParentRenderer;

		// Token: 0x040014AC RID: 5292
		public SpriteRenderer Renderer;
	}
}
