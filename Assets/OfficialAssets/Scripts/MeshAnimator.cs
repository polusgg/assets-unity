using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class MeshAnimator : MonoBehaviour
{
	// Token: 0x06000791 RID: 1937 RVA: 0x00030063 File Offset: 0x0002E263
	private void Start()
	{
		this.filter = base.GetComponent<MeshFilter>();
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00030074 File Offset: 0x0002E274
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > 1f / this.frameRate)
		{
			this.timer = 0f;
			this.frameId++;
			if (this.frameId >= this.Frames.Length)
			{
				this.frameId = 0;
			}
			this.filter.mesh = this.Frames[this.frameId];
		}
	}

	// Token: 0x04000889 RID: 2185
	private MeshFilter filter;

	// Token: 0x0400088A RID: 2186
	public Mesh[] Frames;

	// Token: 0x0400088B RID: 2187
	public float frameRate;

	// Token: 0x0400088C RID: 2188
	private float timer;

	// Token: 0x0400088D RID: 2189
	private int frameId;
}
