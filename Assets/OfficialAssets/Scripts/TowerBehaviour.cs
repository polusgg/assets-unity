using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class TowerBehaviour : MonoBehaviour
{
	// Token: 0x060000A8 RID: 168 RVA: 0x0000437C File Offset: 0x0000257C
	public void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer < this.frameTime)
		{
			this.circle.color = Color.white;
			this.middle1.color = (this.middle2.color = (this.outer1.color = (this.outer2.color = Color.black)));
			return;
		}
		if (this.timer < 2f * this.frameTime)
		{
			this.middle1.color = (this.middle2.color = Color.white);
			this.circle.color = (this.outer1.color = (this.outer2.color = Color.black));
			return;
		}
		if (this.timer < 3f * this.frameTime)
		{
			this.outer1.color = (this.outer2.color = Color.white);
			this.middle1.color = (this.middle2.color = (this.circle.color = Color.black));
			return;
		}
		this.timer = 0f;
	}

	// Token: 0x04000080 RID: 128
	public float timer;

	// Token: 0x04000081 RID: 129
	public float frameTime = 0.2f;

	// Token: 0x04000082 RID: 130
	public SpriteRenderer circle;

	// Token: 0x04000083 RID: 131
	public SpriteRenderer middle1;

	// Token: 0x04000084 RID: 132
	public SpriteRenderer middle2;

	// Token: 0x04000085 RID: 133
	public SpriteRenderer outer1;

	// Token: 0x04000086 RID: 134
	public SpriteRenderer outer2;
}
