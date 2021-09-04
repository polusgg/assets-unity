using System;
using UnityEngine;

// Token: 0x0200020F RID: 527
public interface ISoundPlayer
{
	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000C6A RID: 3178
	// (set) Token: 0x06000C6B RID: 3179
	string Name { get; set; }

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000C6C RID: 3180
	// (set) Token: 0x06000C6D RID: 3181
	AudioSource Player { get; set; }

	// Token: 0x06000C6E RID: 3182
	void Update(float dt);
}
