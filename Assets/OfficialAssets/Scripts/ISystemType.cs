using System;
using Hazel;

// Token: 0x02000205 RID: 517
public interface ISystemType
{
	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06000C39 RID: 3129
	bool IsDirty { get; }

	// Token: 0x06000C3A RID: 3130
	void Detoriorate(float deltaTime);

	// Token: 0x06000C3B RID: 3131
	void RepairDamage(PlayerControl player, byte amount);

	// Token: 0x06000C3C RID: 3132
	void Serialize(MessageWriter writer, bool initialState);

	// Token: 0x06000C3D RID: 3133
	void Deserialize(MessageReader reader, bool initialState);
}
