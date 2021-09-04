using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public interface IFocusHolder
{
	// Token: 0x060004AB RID: 1195
	void GiveFocus();

	// Token: 0x060004AC RID: 1196
	void LoseFocus();

	// Token: 0x060004AD RID: 1197
	bool CheckCollision(Vector2 pt);
}
