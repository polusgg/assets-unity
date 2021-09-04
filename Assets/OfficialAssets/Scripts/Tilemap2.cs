using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class Tilemap2 : MonoBehaviour
{
	// Token: 0x06000813 RID: 2067 RVA: 0x000348E4 File Offset: 0x00032AE4
	internal void SetTile(Vector3Int vec, int tileId)
	{
		int num = vec.x + vec.y * this.Width;
		this.tileData[num].SpriteId = tileId;
		this.dirty = true;
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x0003491D File Offset: 0x00032B1D
	internal void SetTransformMatrix(Vector3Int vec, Matrix4x4 rot90)
	{
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x0003491F File Offset: 0x00032B1F
	internal MonoBehaviour GetTile(Vector3Int touchCellPos)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00034928 File Offset: 0x00032B28
	internal Vector3Int WorldToCell(Vector2 worldPos)
	{
		Vector2 vector = worldPos;// - base.transform.transform.position;
		return new Vector3Int(Mathf.RoundToInt(vector.x / (float)this.Width), Mathf.RoundToInt(vector.y / (float)this.Height), 0);
	}

	// Token: 0x0400097F RID: 2431
	public Sprite[] sprites;

	// Token: 0x04000980 RID: 2432
	private Tile2[] tileData;

	// Token: 0x04000981 RID: 2433
	public int Width = 1;

	// Token: 0x04000982 RID: 2434
	public int Height = 1;

	// Token: 0x04000983 RID: 2435
	private bool dirty;
}
