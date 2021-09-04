using System;
//using Rewired;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class InfectedOverlay : MonoBehaviour
{
	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001E80B File Offset: 0x0001CA0B
	public bool CanUseDoors
	{
		get
		{
			return ShipStatus.Instance.Type == ShipStatus.MapType.Pb || !this.SabSystem.AnyActive;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001E82A File Offset: 0x0001CA2A
	public bool CanUseSpecial
	{
		get
		{
			if (this.SabSystem.Timer <= 0f)
			{
				IActivatable activatable = this.doors;
				if (activatable == null || !activatable.IsActive)
				{
					return !this.SabSystem.AnyActive;
				}
			}
			return false;
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0001E864 File Offset: 0x0001CA64
	public void Start()
	{
		for (int i = 0; i < this.rooms.Length; i++)
		{
			this.rooms[i].Parent = this;
		}
		this.SabSystem = (SabotageSystemType)ShipStatus.Instance.Systems[SystemTypes.Sabotage];
		ISystemType systemType;
		if (ShipStatus.Instance.Systems.TryGetValue(SystemTypes.Doors, out systemType))
		{
			this.doors = (IActivatable)systemType;
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
	private void FixedUpdate()
	{
		float specialActive = (this.doors != null && this.doors.IsActive) ? 1f : this.SabSystem.PercentCool;
		for (int i = 0; i < this.rooms.Length; i++)
		{
			this.rooms[i].SetSpecialActive(specialActive);
			this.rooms[i].OOBUpdate();
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0001E933 File Offset: 0x0001CB33
	private void OnEnable()
	{
		this.allButtons = base.GetComponentsInChildren<ButtonBehavior>(true);
		if (!this.selectedButton)
		{
			this.SelectClosestButton(base.transform.position);
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0001E965 File Offset: 0x0001CB65
	private void DeselectCurrent()
	{
		if (this.selectedButton && this.selectedButton.spriteRenderer)
		{
			this.selectedButton.spriteRenderer.color = Color.white;
			this.selectedButton = null;
		}
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
	private void Select(ButtonBehavior newSelected)
	{
		if (ActiveInputManager.currentControlType != ActiveInputManager.InputType.Joystick)
		{
			return;
		}
		if (this.selectedButton)
		{
			this.DeselectCurrent();
		}
		this.selectedButton = newSelected;
		if (this.selectedButton && this.selectedButton.spriteRenderer)
		{
			this.selectedButton.spriteRenderer.color = Color.green;
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0001EA08 File Offset: 0x0001CC08
	private void SelectClosestButton(Vector2 anchorSpot)
	{
		ButtonBehavior buttonBehavior = null;
		float num = float.PositiveInfinity;
		foreach (ButtonBehavior buttonBehavior2 in this.allButtons)
		{
			Vector2 vector = buttonBehavior2.transform.position;
			float sqrMagnitude = (anchorSpot - vector).sqrMagnitude;
			if (sqrMagnitude < num || buttonBehavior == null)
			{
				num = sqrMagnitude;
				buttonBehavior = buttonBehavior2;
			}
		}
		if (buttonBehavior != null)
		{
			if (!buttonBehavior.spriteRenderer)
			{
				buttonBehavior.spriteRenderer = buttonBehavior.GetComponent<SpriteRenderer>();
			}
			this.Select(buttonBehavior);
		}
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0001EA9C File Offset: 0x0001CC9C
	private void Update()
	{
		//if (ActiveInputManager.currentControlType != ActiveInputManager.InputType.Joystick)
		//{
		//	this.DeselectCurrent();
		//}
		//Player player = ReInput.players.GetPlayer(0);
		//Vector2 vector;
		//vector..ctor(player.GetAxis(16), player.GetAxis(17));
		//float magnitude = vector.magnitude;
		//if (magnitude > 0.9f)
		//{
		//	if (this.currentCooldown > 0f)
		//	{
		//		this.currentCooldown -= Time.deltaTime;
		//	}
		//	else
		//	{
		//		ButtonBehavior buttonBehavior = null;
		//		float num = float.PositiveInfinity;
		//		Vector2 vector2 = this.selectedButton.transform.position;
		//		vector /= magnitude;
		//		Debug.DrawRay(vector2, vector, Color.green, 5f);
		//		foreach (ButtonBehavior buttonBehavior2 in this.allButtons)
		//		{
		//			if (buttonBehavior2 != this.selectedButton)
		//			{
		//				Vector2 vector3 = buttonBehavior2.transform.position;
		//				Vector2 vector4 = vector3 - vector2;
		//				float magnitude2 = vector4.magnitude;
		//				vector4 /= magnitude2;
		//				float num2 = Vector2.Dot(vector, vector4);
		//				float num3 = num2 / magnitude2;
		//				if (num2 > 0.7f && (buttonBehavior == null || num3 > num))
		//				{
		//					num = num3;
		//					buttonBehavior = buttonBehavior2;
		//					Debug.DrawLine(vector2, vector3, Color.cyan, 5f);
		//				}
		//				else
		//				{
		//					Debug.DrawLine(vector2, vector3, Color.red, 5f);
		//				}
		//			}
		//		}
		//		if (buttonBehavior != null)
		//		{
		//			this.DeselectCurrent();
		//			this.Select(buttonBehavior);
		//			this.currentCooldown = 0.5f;
		//		}
		//	}
		//}
		//else
		//{
		//	this.currentCooldown = 0f;
		//}
		//if (this.selectedButton && player.GetButtonDown(11))
		//{
		//	this.selectedButton.OnClick.Invoke();
		//}
	}

	// Token: 0x04000599 RID: 1433
	public MapRoom[] rooms;

	// Token: 0x0400059A RID: 1434
	private IActivatable doors;

	// Token: 0x0400059B RID: 1435
	private SabotageSystemType SabSystem;

	// Token: 0x0400059C RID: 1436
	public ButtonBehavior[] allButtons;

	// Token: 0x0400059D RID: 1437
	public ButtonBehavior selectedButton;

	// Token: 0x0400059E RID: 1438
	private const float selectCooldown = 0.5f;

	// Token: 0x0400059F RID: 1439
	private float currentCooldown;
}
