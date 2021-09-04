using System;
using System.Collections;
using UnityEngine;

namespace PowerTools
{
	// Token: 0x02000286 RID: 646
	public class WaitForAnimationFinish : IEnumerator
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x0005FB70 File Offset: 0x0005DD70
		public WaitForAnimationFinish(SpriteAnim animator, AnimationClip clip)
		{
			this.animator = animator;
			this.clip = clip;
			this.animator.Play(this.clip, 1f);
			this.animator.Time = 0f;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0005FBBE File Offset: 0x0005DDBE
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0005FBC4 File Offset: 0x0005DDC4
		public bool MoveNext()
		{
			if (this.first)
			{
				this.first = false;
				return true;
			}
			bool result;
			try
			{
				result = this.animator.IsPlaying(this.clip);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0005FC10 File Offset: 0x0005DE10
		public void Reset()
		{
			this.first = true;
			this.animator.Play(this.clip, 1f);
		}

		// Token: 0x040014C3 RID: 5315
		private SpriteAnim animator;

		// Token: 0x040014C4 RID: 5316
		private AnimationClip clip;

		// Token: 0x040014C5 RID: 5317
		private bool first = true;
	}
}
