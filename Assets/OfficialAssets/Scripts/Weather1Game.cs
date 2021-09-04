using System;
using System.Collections.Generic;
//using Rewired;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x0200015B RID: 347
public class Weather1Game : Minigame
{
	// Token: 0x06000818 RID: 2072 RVA: 0x00034994 File Offset: 0x00032B94
	public void Start()
	{
		//Vector3Int vector3Int;
		//vector3Int..ctor(-9, 3, 0);
		//HashSet<Vector3Int> hashSet = new HashSet<Vector3Int>();
		//hashSet.Add(vector3Int);
		//this.SolveMaze(vector3Int, hashSet);
		//Matrix4x4 matrix4x = Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, 90f));
		//new List<Vector3Int>();
		//Vector3Int vector3Int2 = default(Vector3Int);
		//vector3Int2.x = -10;
		//while (vector3Int2.x <= 8)
		//{
		//	bool flag = vector3Int2.x % 2 == 0;
		//	vector3Int2.y = -3;
		//	int num;
		//	while (vector3Int2.y <= 3)
		//	{
		//		bool flag2 = vector3Int2.y % 2 == 0;
		//		if (this.PointIsValid(vector3Int2) && !hashSet.Contains(vector3Int2) && flag == flag2 && BoolRange.Next(0.75f))
		//		{
		//			this.BarrierMap.SetTile(vector3Int2, this.barrierTile);
		//			if (flag)
		//			{
		//				this.BarrierMap.SetTransformMatrix(vector3Int2, matrix4x);
		//			}
		//		}
		//		num = vector3Int2.y + 1;
		//		vector3Int2.y = num;
		//	}
		//	num = vector3Int2.x + 1;
		//	vector3Int2.x = num;
		//}
		//base.SetupInput(true);
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00034AAC File Offset: 0x00032CAC
	private bool SolveMaze(Vector3Int curPos, HashSet<Vector3Int> solution)
	{
		if (solution.Count > 50)
		{
			return false;
		}
		bool[] array = new bool[4];
		while (this.Contains<bool>(array, false))
		{
			int num = array.RandomIdx<bool>();
			while (array[num])
			{
				num = (num + 1) % array.Length;
			}
			array[num] = true;
			Vector3Int vector3Int = Weather1Game.Directions[num] + curPos;
			if (this.PointIsValid(vector3Int) && solution.Add(vector3Int))
			{
				if (vector3Int.x == 7 && vector3Int.y == -3)
				{
					return true;
				}
				if (this.SolveMaze(vector3Int, solution))
				{
					return true;
				}
				solution.Remove(vector3Int);
			}
		}
		return false;
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00034B44 File Offset: 0x00032D44
	public void Update()
	{
		//if (this.amClosing != Minigame.CloseState.None)
		//{
		//	return;
		//}
		//this.control.Update();
		//if (this.BarrierMap.GetTile(new Vector3Int(7, -3, 0)) == this.controlTile)
		//{
		//	this.pulseCircle1.enabled = false;
		//	this.pulseCircle2.enabled = false;
		//	this.MyNormTask.NextStep();
		//	base.StartCoroutine(base.CoStartClose(0.75f));
		//	return;
		//}
		//if (Controller.currentTouchType == Controller.TouchType.Joystick)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	if (player.GetButton(11))
		//	{
		//		if (!this.inControl)
		//		{
		//			this.inControl = true;
		//		}
		//		if (this.moveCooldown > 0f)
		//		{
		//			this.moveCooldown -= Time.deltaTime;
		//		}
		//		Vector2 axis2DRaw = player.GetAxis2DRaw(13, 14);
		//		Vector2 vector;
		//		vector..ctor(Mathf.Abs(axis2DRaw.x), Mathf.Abs(axis2DRaw.y));
		//		float num = Mathf.Max(vector.x, vector.y);
		//		float num2 = Mathf.Min(vector.x, vector.y);
		//		Vector3Int zero = Vector3Int.zero;
		//		bool flag = false;
		//		if (num > 0.7f && num2 < 0.2f)
		//		{
		//			if (vector.x > vector.y)
		//			{
		//				zero..ctor((int)Mathf.Sign(axis2DRaw.x), 0, 0);
		//				flag = true;
		//			}
		//			else
		//			{
		//				zero..ctor(0, (int)Mathf.Sign(axis2DRaw.y), 0);
		//				flag = true;
		//			}
		//		}
		//		else
		//		{
		//			this.moveCooldown = 0f;
		//		}
		//		if (flag && this.moveCooldown <= 0f)
		//		{
		//			this.moveCooldown = 0.15f;
		//			Vector3Int vector3Int = this.controlTilePos + zero;
		//			if (!this.BarrierMap.GetTile(vector3Int) && this.PointIsValid(vector3Int) && !this.AnythingBetween(this.controlTilePos, vector3Int))
		//			{
		//				this.FillLine(this.controlTilePos, vector3Int);
		//				this.controlTilePos = vector3Int;
		//				this.BarrierMap.SetTile(this.controlTilePos, this.controlTile);
		//				if (Constants.ShouldPlaySfx())
		//				{
		//					SoundManager.Instance.PlaySoundImmediate(this.NodeMove, false, 1f, 1f);
		//				}
		//			}
		//		}
		//	}
		//	else
		//	{
		//		this.moveCooldown = 0f;
		//		if (this.inControl)
		//		{
		//			this.inControl = false;
		//			Vector3Int vector3Int2 = default(Vector3Int);
		//			vector3Int2.x = -10;
		//			while (vector3Int2.x <= 8)
		//			{
		//				vector3Int2.y = -3;
		//				int num3;
		//				while (vector3Int2.y <= 3)
		//				{
		//					if (this.BarrierMap.GetTile(vector3Int2) == this.fillTile)
		//					{
		//						this.BarrierMap.SetTile(vector3Int2, null);
		//					}
		//					num3 = vector3Int2.y + 1;
		//					vector3Int2.y = num3;
		//				}
		//				num3 = vector3Int2.x + 1;
		//				vector3Int2.x = num3;
		//			}
		//			this.BarrierMap.SetTile(this.controlTilePos, null);
		//			this.controlTilePos.x = -9;
		//			this.controlTilePos.y = 3;
		//			this.BarrierMap.SetTile(this.controlTilePos, this.controlTile);
		//		}
		//	}
		//}
		//else if (this.control.AnyTouch)
		//{
		//	for (int i = 0; i < this.control.Touches.Length; i++)
		//	{
		//		Controller.TouchState touch = this.control.GetTouch(i);
		//		Vector3Int vector3Int3 = this.BarrierMap.WorldToCell(touch.Position);
		//		TileBase tile = this.BarrierMap.GetTile(vector3Int3);
		//		if (touch.TouchStart)
		//		{
		//			if (tile == this.controlTile)
		//			{
		//				this.inControl = true;
		//				this.controlTilePos = vector3Int3;
		//			}
		//		}
		//		else if (this.inControl && !tile && this.PointIsValid(vector3Int3) && !this.AnythingBetween(this.controlTilePos, vector3Int3))
		//		{
		//			this.FillLine(this.controlTilePos, vector3Int3);
		//			this.controlTilePos = vector3Int3;
		//			this.BarrierMap.SetTile(this.controlTilePos, this.controlTile);
		//			if (Constants.ShouldPlaySfx())
		//			{
		//				SoundManager.Instance.PlaySoundImmediate(this.NodeMove, false, 1f, 1f);
		//			}
		//		}
		//	}
		//}
		//else if (this.control.AnyTouchUp)
		//{
		//	for (int j = 0; j < this.control.Touches.Length; j++)
		//	{
		//		this.control.GetTouch(j);
		//		if (this.inControl)
		//		{
		//			Vector3Int vector3Int4 = default(Vector3Int);
		//			vector3Int4.x = -10;
		//			while (vector3Int4.x <= 8)
		//			{
		//				vector3Int4.y = -3;
		//				int num3;
		//				while (vector3Int4.y <= 3)
		//				{
		//					if (this.BarrierMap.GetTile(vector3Int4) == this.fillTile)
		//					{
		//						this.BarrierMap.SetTile(vector3Int4, null);
		//					}
		//					num3 = vector3Int4.y + 1;
		//					vector3Int4.y = num3;
		//				}
		//				num3 = vector3Int4.x + 1;
		//				vector3Int4.x = num3;
		//			}
		//			this.BarrierMap.SetTile(this.controlTilePos, null);
		//			this.controlTilePos.x = -9;
		//			this.controlTilePos.y = 3;
		//			this.BarrierMap.SetTile(this.controlTilePos, this.controlTile);
		//		}
		//	}
		//}
		//else
		//{
		//	this.inControl = false;
		//}
		//this.pulseCircle1.transform.position = this.BarrierMap.CellToWorld(this.controlTilePos) + new Vector3(0.16f, 0.16f, 0f);
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x000350C8 File Offset: 0x000332C8
	private void FillLine(Vector3Int controlTilePos, Vector3Int touchCellPos)
	{
		Vector3Int vector3Int = controlTilePos;
		this.BarrierMap.SetTile(vector3Int, this.fillTile);
		if (controlTilePos.x == touchCellPos.x)
		{
			int num = (int)Mathf.Sign((float)(touchCellPos.y - controlTilePos.y));
			while ((vector3Int.y += num) != touchCellPos.y)
			{
				this.BarrierMap.SetTile(vector3Int, this.fillTile);
			}
			return;
		}
		if (controlTilePos.y == touchCellPos.y)
		{
			int num2 = (int)Mathf.Sign((float)(touchCellPos.x - controlTilePos.x));
			while ((vector3Int.x += num2) != touchCellPos.x)
			{
				this.BarrierMap.SetTile(vector3Int, this.fillTile);
			}
		}
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00035198 File Offset: 0x00033398
	private bool AnythingBetween(Vector3Int controlTilePos, Vector3Int touchCellPos)
	{
		Vector3Int vector3Int = controlTilePos;
		if (controlTilePos.x == touchCellPos.x)
		{
			int num = (int)Mathf.Sign((float)(touchCellPos.y - controlTilePos.y));
			while ((vector3Int.y += num) != touchCellPos.y)
			{
				if (this.BarrierMap.GetTile(vector3Int) || !this.PointIsValid(vector3Int))
				{
					return true;
				}
			}
			return false;
		}
		if (controlTilePos.y == touchCellPos.y)
		{
			int num2 = (int)Mathf.Sign((float)(touchCellPos.x - controlTilePos.x));
			while ((vector3Int.x += num2) != touchCellPos.x)
			{
				if (this.BarrierMap.GetTile(vector3Int) || !this.PointIsValid(vector3Int))
				{
					return true;
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00035270 File Offset: 0x00033470
	private bool PointIsValid(Vector3Int touchCellPos)
	{
		bool flag = touchCellPos.x % 2 == 0;
		bool flag2 = touchCellPos.y % 2 == 0;
		return touchCellPos.x <= 8 && touchCellPos.x >= -10 && touchCellPos.y <= 3 && touchCellPos.y >= -3 && (!flag || flag2);
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x000352CC File Offset: 0x000334CC
	private bool Contains<T>(T[] self, T item) where T : IComparable
	{
		for (int i = 0; i < self.Length; i++)
		{
			if (self[i].Equals(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000984 RID: 2436
	private static int[] BarrierValidX = new int[]
	{
		-10,
		-8,
		-6,
		-4,
		-2,
		0,
		2,
		4,
		6,
		8
	};

	// Token: 0x04000985 RID: 2437
	private static int[] BarrierValidY = new int[]
	{
		-2,
		0,
		2
	};

	// Token: 0x04000986 RID: 2438
	private const int MinX = -10;

	// Token: 0x04000987 RID: 2439
	private const int MaxX = 8;

	// Token: 0x04000988 RID: 2440
	private const int MinY = -3;

	// Token: 0x04000989 RID: 2441
	private const int MaxY = 3;

	// Token: 0x0400098A RID: 2442
	public Tilemap BarrierMap;

	// Token: 0x0400098B RID: 2443
	public Tile fillTile;

	// Token: 0x0400098C RID: 2444
	public Tile controlTile;

	// Token: 0x0400098D RID: 2445
	public Tile barrierTile;

	// Token: 0x0400098E RID: 2446
	public SpriteRenderer pulseCircle1;

	// Token: 0x0400098F RID: 2447
	public SpriteRenderer pulseCircle2;

	// Token: 0x04000990 RID: 2448
	public AudioClip NodeMove;

	// Token: 0x04000991 RID: 2449
	private Controller control = new Controller();

	// Token: 0x04000992 RID: 2450
	private bool inControl;

	// Token: 0x04000993 RID: 2451
	private Vector3Int controlTilePos = new Vector3Int(-9, 3, 0);

	// Token: 0x04000994 RID: 2452
	private static Vector3Int[] Directions = new Vector3Int[]
	{
		Vector3Int.up,
		Vector3Int.down,
		Vector3Int.left,
		Vector3Int.right
	};

	// Token: 0x04000995 RID: 2453
	private float moveCooldown;
}
