using System;
using UnityEngine;

// Token: 0x020001D2 RID: 466
public abstract class OptionBehaviour : MonoBehaviour
{
	// Token: 0x06000AFF RID: 2815 RVA: 0x00045C88 File Offset: 0x00043E88
	public virtual float GetFloat()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00045C8F File Offset: 0x00043E8F
	public virtual int GetInt()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x00045C96 File Offset: 0x00043E96
	public virtual bool GetBool()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x00045CA0 File Offset: 0x00043EA0
	public void SetAsPlayer()
	{
		PassiveButton[] componentsInChildren = base.GetComponentsInChildren<PassiveButton>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x04000C65 RID: 3173
	public StringNames Title;

	// Token: 0x04000C66 RID: 3174
	public Action<OptionBehaviour> OnValueChanged;
}
