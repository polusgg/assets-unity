using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class RoomTracker : MonoBehaviour
{
	// Token: 0x060005A0 RID: 1440 RVA: 0x0002505A File Offset: 0x0002325A
	public void Awake()
	{
		this.filter = default(ContactFilter2D);
		this.filter.layerMask = Constants.PlayersOnlyMask;
		this.filter.useLayerMask = true;
		this.filter.useTriggers = false;
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00025098 File Offset: 0x00023298
	public void OnDisable()
	{
		this.LastRoom = null;
		Vector3 localPosition = this.text.transform.localPosition;
		localPosition.y = this.TargetY;
		this.text.transform.localPosition = localPosition;
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x000250DC File Offset: 0x000232DC
	public void FixedUpdate()
	{
		PlainShipRoom[] array = null;
		if (LobbyBehaviour.Instance)
		{
			PlainShipRoom[] allRooms = LobbyBehaviour.Instance.AllRooms;
			array = allRooms;
		}
		if (ShipStatus.Instance)
		{
			array = ShipStatus.Instance.AllRooms;
		}
		if (array == null)
		{
			return;
		}
		PlainShipRoom plainShipRoom = null;
		if (this.LastRoom)
		{
			int hitCount = this.LastRoom.roomArea.OverlapCollider(this.filter, this.buffer);
			if (RoomTracker.CheckHitsForPlayer(this.buffer, hitCount))
			{
				plainShipRoom = this.LastRoom;
			}
		}
		if (!plainShipRoom)
		{
			foreach (PlainShipRoom plainShipRoom2 in array)
			{
				if (plainShipRoom2.roomArea)
				{
					int hitCount2 = plainShipRoom2.roomArea.OverlapCollider(this.filter, this.buffer);
					if (RoomTracker.CheckHitsForPlayer(this.buffer, hitCount2))
					{
						plainShipRoom = plainShipRoom2;
					}
				}
			}
		}
		if (plainShipRoom)
		{
			if (this.LastRoom != plainShipRoom)
			{
				this.LastRoom = plainShipRoom;
				if (this.slideInRoutine != null)
				{
					base.StopCoroutine(this.slideInRoutine);
				}
				if (plainShipRoom.RoomId != SystemTypes.Hallway)
				{
					this.slideInRoutine = base.StartCoroutine(this.CoSlideIn(plainShipRoom.RoomId));
					return;
				}
				this.slideInRoutine = base.StartCoroutine(this.SlideOut());
				return;
			}
		}
		else if (this.LastRoom)
		{
			this.LastRoom = null;
			if (this.slideInRoutine != null)
			{
				base.StopCoroutine(this.slideInRoutine);
			}
			this.slideInRoutine = base.StartCoroutine(this.SlideOut());
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0002525C File Offset: 0x0002345C
	private IEnumerator CoSlideIn(SystemTypes newRoom)
	{
		yield return this.SlideOut();
		Vector3 tempPos = this.text.transform.localPosition;
		Color tempColor = Color.white;
		this.text.Text = DestroyableSingleton<TranslationController>.Instance.GetString(newRoom);
		float timer = 0f;
		while (timer < 0.25f)
		{
			timer = Mathf.Min(0.25f, timer + Time.deltaTime);
			float num = timer / 0.25f;
			tempPos.y = Mathf.SmoothStep(this.TargetY, this.SourceY, num);
			tempColor.a = Mathf.Lerp(0f, 1f, num);
			this.text.transform.localPosition = tempPos;
			this.text.Color = tempColor;
			yield return null;
		}
		yield break;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00025272 File Offset: 0x00023472
	private IEnumerator SlideOut()
	{
		Vector3 tempPos = this.text.transform.localPosition;
		Color tempColor = Color.white;
		float timer = FloatRange.ReverseLerp(tempPos.y, this.SourceY, this.TargetY) * 0.1f;
		while (timer < 0.1f)
		{
			timer = Mathf.Min(0.1f, timer + Time.deltaTime);
			float num = timer / 0.1f;
			tempPos.y = Mathf.SmoothStep(this.SourceY, this.TargetY, num);
			tempColor.a = Mathf.Lerp(1f, 0f, num);
			this.text.transform.localPosition = tempPos;
			this.text.Color = tempColor;
			yield return null;
		}
		yield break;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00025284 File Offset: 0x00023484
	private static bool CheckHitsForPlayer(Collider2D[] buffer, int hitCount)
	{
		if (!PlayerControl.LocalPlayer)
		{
			return false;
		}
		for (int i = 0; i < hitCount; i++)
		{
			if (buffer[i].gameObject == PlayerControl.LocalPlayer.gameObject)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400063A RID: 1594
	public TextRenderer text;

	// Token: 0x0400063B RID: 1595
	public float SourceY = -2.5f;

	// Token: 0x0400063C RID: 1596
	public float TargetY = -3.25f;

	// Token: 0x0400063D RID: 1597
	private Collider2D playerCollider;

	// Token: 0x0400063E RID: 1598
	private ContactFilter2D filter;

	// Token: 0x0400063F RID: 1599
	private Collider2D[] buffer = new Collider2D[10];

	// Token: 0x04000640 RID: 1600
	public PlainShipRoom LastRoom;

	// Token: 0x04000641 RID: 1601
	private Coroutine slideInRoutine;
}
