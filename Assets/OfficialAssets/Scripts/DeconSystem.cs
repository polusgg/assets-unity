using System;
using System.Collections;
using Hazel;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class DeconSystem : MonoBehaviour, ISystemType
{
	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060003E0 RID: 992 RVA: 0x00019DC5 File Offset: 0x00017FC5
	// (set) Token: 0x060003E1 RID: 993 RVA: 0x00019DCD File Offset: 0x00017FCD
	public DeconSystem.States CurState { get; private set; }

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x060003E2 RID: 994 RVA: 0x00019DD6 File Offset: 0x00017FD6
	// (set) Token: 0x060003E3 RID: 995 RVA: 0x00019DDE File Offset: 0x00017FDE
	public bool IsDirty { get; private set; }

	// Token: 0x060003E4 RID: 996 RVA: 0x00019DE8 File Offset: 0x00017FE8
	public void Detoriorate(float dt)
	{
		if (this.sprayers == null && this.CurState.HasFlag(DeconSystem.States.Closed))
		{
			this.sprayers = base.StartCoroutine(this.CoRunSprayers());
		}
		int num = Mathf.CeilToInt(this.timer);
		this.timer = Mathf.Max(0f, this.timer - dt);
		int num2 = Mathf.CeilToInt(this.timer);
		if (num != num2)
		{
			if (num2 == 0)
			{
				if (this.CurState.HasFlag(DeconSystem.States.Enter))
				{
					this.CurState = ((this.CurState & ~DeconSystem.States.Enter) | DeconSystem.States.Closed);
					this.timer = this.DeconTime;
				}
				else if (this.CurState.HasFlag(DeconSystem.States.Closed))
				{
					this.CurState = ((this.CurState & ~DeconSystem.States.Closed) | DeconSystem.States.Exit);
					this.timer = this.DoorOpenTime;
				}
				else if (this.CurState.HasFlag(DeconSystem.States.Exit))
				{
					this.CurState = DeconSystem.States.Idle;
				}
			}
			this.UpdateDoorsViaState();
			this.IsDirty = true;
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00019F02 File Offset: 0x00018102
	private IEnumerator CoRunSprayers()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlayDynamicSound("DeconSpray", this.SpraySound, false, new DynamicSound.GetDynamicsFunction(this.SoundDynamics), true);
		}
		this.Particles.ForEach(delegate(ParticleSystem p)
		{
			p.Play();
		});
		yield return Effects.Wait(this.DeconTime);
		this.sprayers = null;
		yield break;
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00019F14 File Offset: 0x00018114
	private void SoundDynamics(AudioSource source, float dt)
	{
		if (this.sprayers == null || !PlayerControl.LocalPlayer)
		{
			source.volume = 0f;
			return;
		}
		Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
		if (this.RoomArea && this.RoomArea.OverlapPoint(truePosition))
		{
			float num = this.timer / this.DeconTime;
			if ((double)num > 0.5)
			{
				source.volume = 1f - (num - 0.5f) / 0.5f;
			}
			else
			{
				source.volume = 1f;
			}
			float num2 = source.volume * 0.075f;
			VibrationManager.Vibrate(num2, num2);
			return;
		}
		source.volume = 0f;
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00019FC6 File Offset: 0x000181C6
	public void OpenDoor(bool upper)
	{
		if (this.CurState == DeconSystem.States.Idle)
		{
			ShipStatus.Instance.RpcRepairSystem(this.TargetSystem, upper ? 2 : 1);
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00019FE7 File Offset: 0x000181E7
	public void OpenFromInside(bool upper)
	{
		if (this.CurState == DeconSystem.States.Idle)
		{
			ShipStatus.Instance.RpcRepairSystem(this.TargetSystem, upper ? 3 : 4);
		}
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0001A008 File Offset: 0x00018208
	public void RepairDamage(PlayerControl player, byte amount)
	{
		if (this.CurState != DeconSystem.States.Idle)
		{
			return;
		}
		switch (amount)
		{
		case 1:
			this.CurState = (DeconSystem.States.Enter | DeconSystem.States.HeadingUp);
			this.timer = this.DoorOpenTime;
			break;
		case 2:
			this.CurState = DeconSystem.States.Enter;
			this.timer = this.DoorOpenTime;
			break;
		case 3:
			this.CurState = (DeconSystem.States.Exit | DeconSystem.States.HeadingUp);
			this.timer = this.DoorOpenTime;
			break;
		case 4:
			this.CurState = DeconSystem.States.Exit;
			this.timer = this.DoorOpenTime;
			break;
		}
		this.UpdateDoorsViaState();
		this.IsDirty = true;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0001A099 File Offset: 0x00018299
	public void Serialize(MessageWriter writer, bool initialState)
	{
		writer.Write((byte)Mathf.CeilToInt(this.timer));
		writer.Write((byte)this.CurState);
		this.IsDirty = initialState;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0001A0C0 File Offset: 0x000182C0
	public void Deserialize(MessageReader reader, bool initialState)
	{
		this.timer = (float)reader.ReadByte();
		this.CurState = (DeconSystem.States)reader.ReadByte();
		this.UpdateDoorsViaState();
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001A0E4 File Offset: 0x000182E4
	private void UpdateDoorsViaState()
	{
		int num = Mathf.CeilToInt(this.timer);
		if (this.CurState.HasFlag(DeconSystem.States.Enter))
		{
			bool flag = this.CurState.HasFlag(DeconSystem.States.HeadingUp);
			this.LowerDoor.SetDoorway(flag);
			this.UpperDoor.SetDoorway(!flag);
			if (this.FloorText)
			{
				this.FloorText.SetSecond((float)num, this.DoorOpenTime);
				return;
			}
		}
		else if (this.CurState.HasFlag(DeconSystem.States.Closed) || this.CurState == DeconSystem.States.Idle)
		{
			this.LowerDoor.SetDoorway(false);
			this.UpperDoor.SetDoorway(false);
			if (this.FloorText)
			{
				this.FloorText.SetSecond(this.DeconTime - (float)num, this.DeconTime);
				return;
			}
		}
		else if (this.CurState.HasFlag(DeconSystem.States.Exit))
		{
			bool flag2 = this.CurState.HasFlag(DeconSystem.States.HeadingUp);
			this.LowerDoor.SetDoorway(!flag2);
			this.UpperDoor.SetDoorway(flag2);
			if (this.FloorText)
			{
				this.FloorText.SetSecond((float)num, this.DoorOpenTime);
				return;
			}
		}
		else
		{
			Debug.LogWarning("What is this state: " + this.CurState.ToString());
		}
	}

	// Token: 0x0400048D RID: 1165
	private const byte HeadUpCmd = 1;

	// Token: 0x0400048E RID: 1166
	private const byte HeadDownCmd = 2;

	// Token: 0x0400048F RID: 1167
	private const byte HeadUpInsideCmd = 3;

	// Token: 0x04000490 RID: 1168
	private const byte HeadDownInsideCmd = 4;

	// Token: 0x04000491 RID: 1169
	public SomeKindaDoor UpperDoor;

	// Token: 0x04000492 RID: 1170
	public SomeKindaDoor LowerDoor;

	// Token: 0x04000493 RID: 1171
	public float DoorOpenTime = 3f;

	// Token: 0x04000494 RID: 1172
	public float DeconTime = 3f;

	// Token: 0x04000495 RID: 1173
	public AudioClip SpraySound;

	// Token: 0x04000496 RID: 1174
	public ParticleSystem[] Particles;

	// Token: 0x04000497 RID: 1175
	public SystemTypes TargetSystem = SystemTypes.Decontamination;

	// Token: 0x04000499 RID: 1177
	private float timer;

	// Token: 0x0400049A RID: 1178
	public Collider2D RoomArea;

	// Token: 0x0400049B RID: 1179
	public DecontamNumController FloorText;

	// Token: 0x0400049C RID: 1180
	private Coroutine sprayers;

	// Token: 0x0200033B RID: 827
	[Flags]
	public enum States : byte
	{
		// Token: 0x04001853 RID: 6227
		Idle = 0,
		// Token: 0x04001854 RID: 6228
		Enter = 1,
		// Token: 0x04001855 RID: 6229
		Closed = 2,
		// Token: 0x04001856 RID: 6230
		Exit = 4,
		// Token: 0x04001857 RID: 6231
		HeadingUp = 8
	}
}
