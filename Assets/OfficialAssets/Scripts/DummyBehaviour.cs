using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001A5 RID: 421
public class DummyBehaviour : MonoBehaviour
{
	// Token: 0x0600095B RID: 2395 RVA: 0x0003D555 File Offset: 0x0003B755
	public void Start()
	{
		this.myPlayer = base.GetComponent<PlayerControl>();
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0003D564 File Offset: 0x0003B764
	public void Update()
	{
		GameData.PlayerInfo data = this.myPlayer.Data;
		if (data == null || data.IsDead)
		{
			return;
		}
		if (MeetingHud.Instance)
		{
			if (!this.voted)
			{
				this.voted = true;
				base.StartCoroutine(this.DoVote());
				return;
			}
		}
		else
		{
			this.voted = false;
		}
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x0003D5B9 File Offset: 0x0003B7B9
	private IEnumerator DoVote()
	{
		yield return new WaitForSeconds(this.voteTime.Next());
		sbyte suspectIdx = -1;
		int num = 0;
		while (num < 100 && num != 99)
		{
			int num2 = IntRange.Next(-1, GameData.Instance.PlayerCount);
			if (num2 < 0)
			{
				suspectIdx = (sbyte)num2;
				break;
			}
			GameData.PlayerInfo playerInfo = GameData.Instance.AllPlayers[num2];
			if (!playerInfo.IsDead)
			{
				suspectIdx = (sbyte)playerInfo.PlayerId;
				break;
			}
			num++;
		}
		MeetingHud.Instance.CmdCastVote(this.myPlayer.PlayerId, suspectIdx);
		yield break;
	}

	// Token: 0x04000AD1 RID: 2769
	private PlayerControl myPlayer;

	// Token: 0x04000AD2 RID: 2770
	private FloatRange voteTime = new FloatRange(3f, 8f);

	// Token: 0x04000AD3 RID: 2771
	private bool voted;
}
