using System;
//using Rewired;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class WireMinigame : Minigame
{
	// Token: 0x0600036E RID: 878 RVA: 0x000167F4 File Offset: 0x000149F4

	private bool TaskIsForThisPanel()
	{
		return this.MyNormTask.taskStep < this.MyNormTask.Data.Length && !this.MyNormTask.IsComplete && (int)this.MyNormTask.Data[this.MyNormTask.taskStep] == base.ConsoleId;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0001684C File Offset: 0x00014A4C
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		IntRange.FillRandomRange(this.ExpectedWires);
		for (int i = 0; i < this.LeftNodes.Length; i++)
		{
			this.ActualWires[i] = -1;
			int num = (int)this.ExpectedWires[i];
			Wire wire = this.LeftNodes[i];
			wire.SetColor(WireMinigame.colors[num], this.Symbols[num]);
			wire.WireId = (sbyte)i;
			this.RightNodes[i].SetColor(WireMinigame.colors[i], this.Symbols[i]);
			this.RightNodes[i].WireId = (sbyte)i;
			int num2 = (int)this.ActualWires[i];
			if (num2 > -1)
			{
				wire.ConnectRight(this.RightNodes[num2]);
			}
			else
			{
				wire.ResetLine(Vector3.zero, true);
			}
		}
		this.UpdateLights();
		base.SetupInput(true);
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00016928 File Offset: 0x00014B28
	public void Update()
	{
        if (!this.TaskIsForThisPanel())
        {
            return;
        }
        this.myController.Update();
        this.selectedWireUI.gameObject.SetActive(Controller.currentTouchType == Controller.TouchType.Joystick);

            this.prevButtonDown = false;
            if (this.prevSelectedWireIndex != -1)
            {
                Wire wire3 = this.LeftNodes[this.prevSelectedWireIndex];
                if (this.ActualWires[(int)wire3.WireId] == -1)
                {
                    wire3.ResetLine(wire3.BaseWorldPos, true);
                }
                else if (Constants.ShouldPlaySfx())
                {
                    SoundManager.Instance.PlaySound(this.WireSounds.Random<AudioClip>(), false, 1f);
                }
                this.CheckTask();
                this.prevSelectedWireIndex = -1;
            }
            //base.transform.position;
            for (int j = 0; j < this.LeftNodes.Length; j++)
            {
                Wire wire4 = this.LeftNodes[j];
                DragState dragState = this.myController.CheckDrag(wire4.hitbox);
                if (dragState != DragState.Dragging)
                {
                    if (dragState == DragState.Released)
                    {
                        if (this.ActualWires[(int)wire4.WireId] == -1)
                        {
                            wire4.ResetLine(wire4.BaseWorldPos, true);
                        }
                        else if (Constants.ShouldPlaySfx())
                        {
                            SoundManager.Instance.PlaySound(this.WireSounds.Random<AudioClip>(), false, 1f);
                        }
                        this.CheckTask();
                    }
                }
                else
                {
                    Vector2 vector3 = this.myController.DragPosition;
                    WireNode wireNode2 = this.CheckRightSide(vector3);
                    if (wireNode2)
                    {
                        vector3 = wireNode2.transform.position;
                        this.ActualWires[(int)wire4.WireId] = wireNode2.WireId;
                    }
                    else
                    {
                        vector3 -= wire4.BaseWorldPos.normalized * 0.05f;
                        this.ActualWires[(int)wire4.WireId] = -1;
                    }
                    wire4.ResetLine(vector3, false);
                }
            }
        this.UpdateLights();
    }

	// Token: 0x06000371 RID: 881 RVA: 0x00016E6C File Offset: 0x0001506C
	private void UpdateLights()
	{
		for (int i = 0; i < this.ActualWires.Length; i++)
		{
			Color color = Color.yellow;
			color *= 1f - Mathf.PerlinNoise((float)i, Time.time * 35f) * 0.3f;
			color.a = 1f;
			if (this.ActualWires[i] != this.ExpectedWires[i])
			{
				this.RightLights[(int)this.ExpectedWires[i]].color = new Color(0.2f, 0.2f, 0.2f);
			}
			else
			{
				this.RightLights[(int)this.ExpectedWires[i]].color = color;
			}
			this.LeftLights[i].color = color;
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00016F28 File Offset: 0x00015128
	private WireNode CheckRightSide(Vector2 pos)
	{
		for (int i = 0; i < this.RightNodes.Length; i++)
		{
			WireNode wireNode = this.RightNodes[i];
			if (wireNode.hitbox.OverlapPoint(pos))
			{
				return wireNode;
			}
		}
		return null;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00016F64 File Offset: 0x00015164
	private void CheckTask()
	{
		bool flag = true;
		for (int i = 0; i < this.ActualWires.Length; i++)
		{
			if (this.ActualWires[i] != this.ExpectedWires[i])
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			this.MyNormTask.NextStep();
			this.Close();
		}
	}

	// Token: 0x04000403 RID: 1027
	private static readonly Color[] colors = new Color[]
	{
		Color.red,
		new Color(0.15f, 0.15f, 1f, 1f),
		Color.yellow,
		Color.magenta
	};

	// Token: 0x04000404 RID: 1028
	public Sprite[] Symbols;

	// Token: 0x04000405 RID: 1029
	public Wire[] LeftNodes;

	// Token: 0x04000406 RID: 1030
	public WireNode[] RightNodes;

	// Token: 0x04000407 RID: 1031
	public SpriteRenderer[] LeftLights;

	// Token: 0x04000408 RID: 1032
	public SpriteRenderer[] RightLights;

	// Token: 0x04000409 RID: 1033
	private Controller myController = new Controller();

	// Token: 0x0400040A RID: 1034
	private sbyte[] ExpectedWires = new sbyte[4];

	// Token: 0x0400040B RID: 1035
	private sbyte[] ActualWires = new sbyte[4];

	// Token: 0x0400040C RID: 1036
	public AudioClip[] WireSounds;

	// Token: 0x0400040D RID: 1037
	private int prevSelectedWireIndex = -1;

	// Token: 0x0400040E RID: 1038
	private int selectedWireIndex;

	// Token: 0x0400040F RID: 1039
	private bool prevButtonDown;

	// Token: 0x04000410 RID: 1040
	private float inputCooldown;

	// Token: 0x04000411 RID: 1041
	public Vector2 controllerWirePos = Vector2.zero;

	// Token: 0x04000412 RID: 1042
	private const float controllerWireSpeed = 7f;

	// Token: 0x04000413 RID: 1043
	public GameObject[] selectingWireGlyphs;

	// Token: 0x04000414 RID: 1044
	public GameObject[] movingWireGlyphs;

	// Token: 0x04000415 RID: 1045
	public Transform selectedWireUI;
}
