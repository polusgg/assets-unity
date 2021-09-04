using System;
using System.Collections;
using System.Linq;
using PowerTools;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class DripBehaviour : MonoBehaviour
{
	// Token: 0x0600033A RID: 826 RVA: 0x000151E6 File Offset: 0x000133E6
	public void Start()
	{
		base.StartCoroutine(this.Run());
	}

	// Token: 0x0600033B RID: 827 RVA: 0x000151F5 File Offset: 0x000133F5
	private IEnumerator Run()
	{
		//yield return Effects.Wait(this.FirstWait.Next());
		//for (;;)
		//{
		//	Vector3 pos = this.SpawnArea.Next();
		//	base.transform.localPosition = pos;
		//	if (this.FixDepth)
		//	{
		//		pos = base.transform.position;
		//		pos.z = pos.y / 1000f;
		//		base.transform.position = pos;
		//	}
		//	if (!this.IgnoreAreas.Any((Collider2D i) => i.OverlapPoint(pos)))
		//	{
		//		this.myAnim.Play(null, 1f);
		//		while (this.myAnim.IsPlaying(null))
		//		{
		//			yield return null;
		//		}
		//		yield return Effects.Wait(this.Frequency.Next());
		//	}
		//}
		yield break;
	}

	// Token: 0x040003AA RID: 938
	public Vector2Range SpawnArea;

	// Token: 0x040003AB RID: 939
	public FloatRange FirstWait = new FloatRange(0f, 3f);

	// Token: 0x040003AC RID: 940
	public FloatRange Frequency = new FloatRange(0.75f, 3f);

	// Token: 0x040003AD RID: 941
	public SpriteAnim myAnim;

	// Token: 0x040003AE RID: 942
	public Collider2D[] IgnoreAreas;

	// Token: 0x040003AF RID: 943
	public bool FixDepth = true;
}
