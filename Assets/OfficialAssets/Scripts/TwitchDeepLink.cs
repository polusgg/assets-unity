using System;
using UnityEngine;

// Token: 0x0200024A RID: 586
public class TwitchDeepLink : MonoBehaviour
{
	// Token: 0x06000DB1 RID: 3505 RVA: 0x00053025 File Offset: 0x00051225
	private void OnEnable()
	{
		//if (EOSManager.Instance.IsMinor() || !CanOpenTwitch.CheckUrl("twitch://broadcast?game_id=510218"))
		//{
		//	base.gameObject.SetActive(false);
		//}
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0005304B File Offset: 0x0005124B
	public void OnClick()
	{
		Application.OpenURL("twitch://broadcast?game_id=510218");
	}
}
