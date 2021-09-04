using System;
using Hazel;
using InnerNet;
using UnityEngine;

// Token: 0x020000F3 RID: 243
[DisallowMultipleComponent]
public class CustomNetworkTransform : InnerNetObject
{
	// Token: 0x06000600 RID: 1536 RVA: 0x000270E8 File Offset: 0x000252E8
	private void Awake()
	{
		this.body = base.GetComponent<Rigidbody2D>();
		this.targetSyncPosition = (this.prevPosSent = base.transform.position);
		this.targetSyncVelocity = (this.prevVelSent = Vector2.zero);
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x00027134 File Offset: 0x00025334
	public void OnEnable()
	{
		DummyBehaviour component = base.GetComponent<DummyBehaviour>();
		if (component && component.enabled)
		{
			base.enabled = false;
		}
		base.SetDirtyBit(3U);
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x00027168 File Offset: 0x00025368
	public void Halt()
	{
		//ushort minSid = this.lastSequenceId + 1;
		//this.SnapTo(base.transform.position, minSid);
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00027198 File Offset: 0x00025398
	public void RpcSnapTo(Vector2 position)
	{
		//ushort minSid = this.lastSequenceId + 5;
		//if (AmongUsClient.Instance.AmClient)
		//{
		//	this.SnapTo(position, minSid);
		//}
		//MessageWriter messageWriter = AmongUsClient.Instance.StartRpc(this.NetId, 21, 1);
		//this.WriteVector2(position, messageWriter);
		//messageWriter.Write(this.lastSequenceId);
		//messageWriter.EndMessage();
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x000271F4 File Offset: 0x000253F4
	public void SnapTo(Vector2 position)
	{
		//ushort minSid = this.lastSequenceId + 3;
		//this.SnapTo(position, minSid);
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00027214 File Offset: 0x00025414
	private void SnapTo(Vector2 position, ushort minSid)
	{
		if (!NetHelpers.SidGreaterThan(minSid, this.lastSequenceId))
		{
			return;
		}
		this.lastSequenceId = minSid;
		Transform transform = base.transform;
		this.body.position = position;
		this.targetSyncPosition = position;
		transform.position = position;
		this.targetSyncVelocity = (this.body.velocity = Vector2.zero);
		this.prevPosSent = position;
		this.prevVelSent = Vector2.zero;
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0002728C File Offset: 0x0002548C
	private void FixedUpdate()
	{
		if (base.AmOwner)
		{
			if (this.HasMoved())
			{
				base.SetDirtyBit(3U);
				return;
			}
		}
		else
		{
			if (this.interpolateMovement != 0f)
			{
				Vector2 vector = this.targetSyncPosition - this.body.position;
				if (vector.sqrMagnitude >= 0.0001f)
				{
					float num = this.interpolateMovement / this.sendInterval;
					vector.x *= num;
					vector.y *= num;
					if (PlayerControl.LocalPlayer)
					{
						vector = Vector2.ClampMagnitude(vector, PlayerControl.LocalPlayer.MyPhysics.TrueSpeed);
					}
					this.body.velocity = vector;
				}
				else
				{
					this.body.velocity = Vector2.zero;
				}
			}
			this.targetSyncPosition += this.targetSyncVelocity * Time.fixedDeltaTime * 0.1f;
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0002737C File Offset: 0x0002557C
	private bool HasMoved()
	{
		float num;
		if (this.body != null)
		{
			num = Vector2.Distance(this.body.position, this.prevPosSent);
		}
		else
		{
			num = Vector2.Distance(base.transform.position, this.prevPosSent);
		}
		if (num > 0.0001f)
		{
			return true;
		}
		if (this.body != null)
		{
			num = Vector2.Distance(this.body.velocity, this.prevVelSent);
		}
		return num > 0.0001f;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0002740C File Offset: 0x0002560C
	public override void HandleRpc(byte callId, MessageReader reader)
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		if (callId == 21)
		{
			Vector2 position = this.ReadVector2(reader);
			ushort minSid = reader.ReadUInt16();
			this.SnapTo(position, minSid);
		}
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00027440 File Offset: 0x00025640
	public override bool Serialize(MessageWriter writer, bool initialState)
	{
		if (initialState)
		{
			writer.Write(this.lastSequenceId);
			this.WriteVector2(this.body.position, writer);
			this.WriteVector2(this.body.velocity, writer);
			return true;
		}
		if (!base.isActiveAndEnabled)
		{
			base.ClearDirtyBits();
			return false;
		}
		this.lastSequenceId += 1;
		writer.Write(this.lastSequenceId);
		this.WriteVector2(this.body.position, writer);
		this.WriteVector2(this.body.velocity, writer);
		this.prevPosSent = this.body.position;
		this.prevVelSent = this.body.velocity;
		this.DirtyBits -= 1U;
		return true;
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x00027504 File Offset: 0x00025704
	public override void Deserialize(MessageReader reader, bool initialState)
	{
		if (initialState)
		{
			this.lastSequenceId = reader.ReadUInt16();
			this.targetSyncPosition = (base.transform.position = this.ReadVector2(reader));
			this.targetSyncVelocity = this.ReadVector2(reader);
			return;
		}
		if (base.AmOwner)
		{
			return;
		}
		ushort newSid = reader.ReadUInt16();
		if (!NetHelpers.SidGreaterThan(newSid, this.lastSequenceId))
		{
			return;
		}
		this.lastSequenceId = newSid;
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.targetSyncPosition = this.ReadVector2(reader);
		this.targetSyncVelocity = this.ReadVector2(reader);
		if (Vector2.Distance(this.body.position, this.targetSyncPosition) > this.snapThreshold)
		{
			if (this.body)
			{
				this.body.position = this.targetSyncPosition;
				this.body.velocity = this.targetSyncVelocity;
			}
			else
			{
				base.transform.position = this.targetSyncPosition;
			}
		}
		if (this.interpolateMovement == 0f && this.body)
		{
			this.body.position = this.targetSyncPosition;
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0002762C File Offset: 0x0002582C
	private void WriteVector2(Vector2 vec, MessageWriter writer)
	{
		ushort num = (ushort)(this.XRange.ReverseLerp(vec.x) * 65535f);
		ushort num2 = (ushort)(this.YRange.ReverseLerp(vec.y) * 65535f);
		writer.Write(num);
		writer.Write(num2);
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0002767C File Offset: 0x0002587C
	private Vector2 ReadVector2(MessageReader reader)
	{
		float v = (float)reader.ReadUInt16() / 65535f;
		float v2 = (float)reader.ReadUInt16() / 65535f;
		return new Vector2(this.XRange.Lerp(v), this.YRange.Lerp(v2));
	}

	// Token: 0x040006B6 RID: 1718
	private const float LocalMovementThreshold = 0.0001f;

	// Token: 0x040006B7 RID: 1719
	private const float LocalVelocityThreshold = 0.0001f;

	// Token: 0x040006B8 RID: 1720
	private const float MoveAheadRatio = 0.1f;

	// Token: 0x040006B9 RID: 1721
	private readonly FloatRange XRange = new FloatRange(-50f, 50f);

	// Token: 0x040006BA RID: 1722
	private readonly FloatRange YRange = new FloatRange(-50f, 50f);

	// Token: 0x040006BB RID: 1723
	[SerializeField]
	private float sendInterval = 0.1f;

	// Token: 0x040006BC RID: 1724
	[SerializeField]
	private float snapThreshold = 5f;

	// Token: 0x040006BD RID: 1725
	[SerializeField]
	private float interpolateMovement = 1f;

	// Token: 0x040006BE RID: 1726
	private Rigidbody2D body;

	// Token: 0x040006BF RID: 1727
	private Vector2 targetSyncPosition;

	// Token: 0x040006C0 RID: 1728
	private Vector2 targetSyncVelocity;

	// Token: 0x040006C1 RID: 1729
	private ushort lastSequenceId;

	// Token: 0x040006C2 RID: 1730
	private Vector2 prevPosSent;

	// Token: 0x040006C3 RID: 1731
	private Vector2 prevVelSent;
}
