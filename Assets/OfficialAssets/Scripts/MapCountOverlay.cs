using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class MapCountOverlay : MonoBehaviour
{
	// Token: 0x060004E2 RID: 1250 RVA: 0x0001EF85 File Offset: 0x0001D185
	public void Awake()
	{
		this.filter.useLayerMask = true;
		this.filter.layerMask = Constants.PlayersOnlyMask;
		this.filter.useTriggers = true;
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
	public void OnEnable()
	{
		this.BackgroundColor.SetColor(PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer) ? Palette.DisabledGrey : Color.green);
		this.timer = 1f;
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0001EFE4 File Offset: 0x0001D1E4
	public void OnDisable()
	{
		for (int i = 0; i < this.CountAreas.Length; i++)
		{
			this.CountAreas[i].UpdateCount(0);
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0001F014 File Offset: 0x0001D214
	public void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer < 0.1f)
		{
			return;
		}
		this.timer = 0f;
		if (!this.isSab && PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.isSab = true;
			this.BackgroundColor.SetColor(Palette.DisabledGrey);
			this.SabotageText.gameObject.SetActive(true);
			return;
		}
		if (this.isSab && !PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
		{
			this.isSab = false;
			this.BackgroundColor.SetColor(Color.green);
			this.SabotageText.gameObject.SetActive(false);
		}
		for (int i = 0; i < this.CountAreas.Length; i++)
		{
			CounterArea counterArea = this.CountAreas[i];
			if (!PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
			{
				PlainShipRoom plainShipRoom;
				if (ShipStatus.Instance.FastRooms.TryGetValue(counterArea.RoomType, out plainShipRoom) && plainShipRoom.roomArea)
				{
					int num = plainShipRoom.roomArea.OverlapCollider(this.filter, this.buffer);
					int num2 = num;
					for (int j = 0; j < num; j++)
					{
						Collider2D collider2D = this.buffer[j];
						if (!(collider2D.tag == "DeadBody"))
						{
							PlayerControl component = collider2D.GetComponent<PlayerControl>();
							if (!component || component.Data == null || component.Data.Disconnected || component.Data.IsDead)
							{
								num2--;
							}
						}
					}
					counterArea.UpdateCount(num2);
				}
				else
				{
					Debug.LogWarning("Couldn't find counter for:" + counterArea.RoomType.ToString());
				}
			}
			else
			{
				counterArea.UpdateCount(0);
			}
		}
	}

	// Token: 0x040005A7 RID: 1447
	public AlphaPulse BackgroundColor;

	// Token: 0x040005A8 RID: 1448
	public TextRenderer SabotageText;

	// Token: 0x040005A9 RID: 1449
	public CounterArea[] CountAreas;

	// Token: 0x040005AA RID: 1450
	private Collider2D[] buffer = new Collider2D[20];

	// Token: 0x040005AB RID: 1451
	private ContactFilter2D filter;

	// Token: 0x040005AC RID: 1452
	private float timer;

	// Token: 0x040005AD RID: 1453
	private bool isSab;
}
