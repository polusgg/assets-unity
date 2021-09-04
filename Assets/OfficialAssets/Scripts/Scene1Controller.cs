using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class Scene1Controller : SceneController
{
	// Token: 0x0600042D RID: 1069 RVA: 0x0001B36C File Offset: 0x0001956C
	public void OnDrawGizmos()
	{
		for (int i = 0; i < this.WayPoints.Length; i++)
		{
			Vector2 vector = this.WayPoints[i];
			Vector2 vector2 = this.WayPoints[(i + 1) % this.WayPoints.Length];
			Gizmos.DrawLine(vector, vector2);
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x0001B3C0 File Offset: 0x000195C0
	public void OnEnable()
	{
		this.backupCam.cullingMask = 0;
		base.StartCoroutine(this.RunPlayer(0));
		if (this.players.Length > 1)
		{
			base.StartCoroutine(this.RunPlayer(1));
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x0001B3F5 File Offset: 0x000195F5
	public void OnDisable()
	{
		this.backupCam.cullingMask = (int.MaxValue ^ LayerMask.GetMask(new string[]
		{
			"UI"
		}));
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x0001B41B File Offset: 0x0001961B
	private IEnumerator RunPlayer(int idx)
	{
		PlayerAnimator myPlayer = this.players[idx];
		for (;;)
		{
			int num;
			for (int i = 0; i < this.WayPoints.Length; i = num)
			{
				bool willInterrupt = i == 2 || i == 5;
				yield return myPlayer.WalkPlayerTo(this.WayPoints[i], willInterrupt, 0.1f);
				if (willInterrupt)
				{
					yield return this.DoUse(idx, (i == 2) ? 0 : 1);
				}
				num = i + 1;
			}
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0001B431 File Offset: 0x00019631
	private IEnumerator DoUse(int idx, int consoleid)
	{
		PlayerAnimator myPlayer = this.players[idx];
		yield return Scene1Controller.WaitForSeconds(0.2f);
		if (idx == 0)
		{
			yield return myPlayer.finger.MoveTo(myPlayer.UseButton.transform.position, 0.75f);
		}
		else
		{
			yield return myPlayer.finger.MoveTo(this.Consoles[consoleid].transform.position, 0.75f);
		}
		yield return Scene1Controller.WaitForSeconds(0.2f);
		yield return myPlayer.finger.DoClick(0.4f);
		yield return Scene1Controller.WaitForSeconds(0.2f);
		if (!(myPlayer.joystick is DemoKeyboardStick))
		{
			yield return myPlayer.finger.MoveTo(myPlayer.joystick.transform.position, 0.75f);
		}
		else
		{
			yield return Scene1Controller.WaitForSeconds(0.75f);
		}
		yield break;
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0001B44E File Offset: 0x0001964E
	public static IEnumerator WaitForSeconds(float duration)
	{
		for (float time = 0f; time < duration; time += Time.deltaTime)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x040004EF RID: 1263
	public PlayerAnimator[] players;

	// Token: 0x040004F0 RID: 1264
	public DummyConsole[] Consoles;

	// Token: 0x040004F1 RID: 1265
	public Vector2[] WayPoints;

	// Token: 0x040004F2 RID: 1266
	public Camera backupCam;
}
