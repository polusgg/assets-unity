using System;
using UnityEngine;

// Token: 0x02000163 RID: 355
public class ConditionalMove : MonoBehaviour
{
	// Token: 0x06000848 RID: 2120 RVA: 0x00035E1C File Offset: 0x0003401C
	private void Awake()
	{
		ConditionalMove.MoveForPlatforms[] array = this.moveForPlatforms;
		for (int i = 0; i < array.Length; i++)
		{
		}
	}

	// Token: 0x040009B9 RID: 2489
	public ConditionalMove.MoveForPlatforms[] moveForPlatforms = new ConditionalMove.MoveForPlatforms[0];

	// Token: 0x020003DB RID: 987
	[Serializable]
	public struct MoveForPlatforms
	{
		// Token: 0x04001ABA RID: 6842
		public RuntimePlatform runtimePlatform;

		// Token: 0x04001ABB RID: 6843
		public Vector3 offset;
	}
}
