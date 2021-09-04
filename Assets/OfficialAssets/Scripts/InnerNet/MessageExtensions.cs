using System;
using Hazel;

namespace InnerNet
{
	// Token: 0x0200029E RID: 670
	public static class MessageExtensions
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x00063422 File Offset: 0x00061622
		public static void WriteNetObject(this MessageWriter self, InnerNetObject obj)
		{
			if (!obj)
			{
				self.Write(0);
				return;
			}
			self.WritePacked(obj.NetId);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00063440 File Offset: 0x00061640
		public static T ReadNetObject<T>(this MessageReader self) where T : InnerNetObject
		{
			uint netId = self.ReadPackedUInt32();
			return AmongUsClient.Instance.FindObjectByNetId<T>(netId);
		}
	}
}
