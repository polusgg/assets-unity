using System;
using System.Collections.Generic;
//using Rewired;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000191 RID: 401
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RadialMenu : MonoBehaviour
{
	// Token: 0x06000905 RID: 2309 RVA: 0x0003B15C File Offset: 0x0003935C
	private void OnEnable()
	{
		this.mf = base.GetComponent<MeshFilter>();
		this.mr = base.GetComponent<MeshRenderer>();
		this.mbp = new MaterialPropertyBlock();
		RadialMenu.instances++;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0003B18D File Offset: 0x0003938D
	private void OnDisable()
	{
		RadialMenu.instances--;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0003B19B File Offset: 0x0003939B
	public void ButtonMouseOver(int which)
	{
		if (Controller.currentTouchType != Controller.TouchType.Joystick)
		{
			this.mouseSelectedButton = which;
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0003B1AB File Offset: 0x000393AB
	public void ButtonMouseExit()
	{
		if (Controller.currentTouchType != Controller.TouchType.Joystick)
		{
			this.mouseSelectedButton = -1;
		}
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0003B1BC File Offset: 0x000393BC
	private void Update()
	{
		// this.cont.Update();
		// Player player = ReInput.players.GetPlayer(0);
		// Vector2 vector = player.GetAxis2DRaw(13, 14);
		// int num = -1;
		// if (Controller.currentTouchType != Controller.TouchType.Joystick)
		// {
		// 	num = this.mouseSelectedButton;
		// 	if (this.arrowRotateHandle.gameObject.activeSelf)
		// 	{
		// 		this.arrowRotateHandle.gameObject.SetActive(false);
		// 	}
		// }
		// else
		// {
		// 	this.mouseSelectedButton = -1;
		// 	if (!this.arrowRotateHandle.gameObject.activeSelf)
		// 	{
		// 		this.arrowRotateHandle.gameObject.SetActive(true);
		// 	}
		// }
		// float magnitude = vector.magnitude;
		// if (magnitude > 0.025f)
		// {
		// 	this.arrowScaleHandle.localScale = new Vector3(magnitude, 1f);
		// 	vector = vector.normalized;
		// 	float num2 = Mathf.Atan2(vector.y, vector.x);
		// 	if (num2 < 0f)
		// 	{
		// 		num2 += 6.2831855f;
		// 	}
		// 	this.arrowRotateHandle.localRotation = Quaternion.Euler(0f, 0f, num2 * 57.29578f);
		// 	if (magnitude > 0.5f)
		// 	{
		// 		num2 += -1.5707964f;
		// 		num = (int)((float)this.radialDivisions * (num2 / 6.2831855f + 0.5f));
		// 		if (num < 0)
		// 		{
		// 			num += this.radialDivisions;
		// 		}
		// 		else if (num >= this.radialDivisions)
		// 		{
		// 			num -= this.radialDivisions;
		// 		}
		// 	}
		// }
		// else
		// {
		// 	this.arrowScaleHandle.localScale = new Vector3(0f, 1f);
		// }
		// if (this.prevSelectedButton != num)
		// {
		// 	if (this.prevSelectedButton != -1)
		// 	{
		// 		this.cachedButtons[this.prevSelectedButton].button.OnMouseOut.Invoke();
		// 	}
		// 	if (num != -1)
		// 	{
		// 		this.cachedButtons[num].button.OnMouseOver.Invoke();
		// 		if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
		// 		{
		// 			if (!this.inputGlyph.gameObject.activeSelf)
		// 			{
		// 				this.inputGlyph.gameObject.SetActive(true);
		// 			}
		// 		}
		// 		else if (this.inputGlyph.gameObject.activeSelf)
		// 		{
		// 			this.inputGlyph.gameObject.SetActive(false);
		// 		}
		// 		Vector3 buttonDir = this.cachedButtons[num].buttonDir;
		// 		buttonDir.z = this.inputGlyph.localPosition.z;
		// 		this.inputGlyph.localPosition = buttonDir;
		// 	}
		// 	else if (this.inputGlyph.gameObject.activeSelf)
		// 	{
		// 		this.inputGlyph.gameObject.SetActive(false);
		// 	}
		// }
		// if (num != -1 && player.GetButtonDown(11))
		// {
		// 	this.cachedButtons[num].button.ReceiveClickDown();
		// }
		// this.mr.GetPropertyBlock(this.mbp);
		// this.mbp.SetInt("_SelectedSlice", num);
		// this.mr.SetPropertyBlock(this.mbp);
		// this.prevSelectedButton = num;
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0003B48C File Offset: 0x0003968C
	public RadialMenu.CachedButtonObject[] CreateButtonsForStrings(string[] strings)
	{
		this.CacheButtons(strings.Length);
		this.BuildMesh(strings.Length);
		this.AlignTexts(strings);
		RadialMenu.CachedButtonObject[] array = new RadialMenu.CachedButtonObject[strings.Length];
		for (int i = 0; i < strings.Length; i++)
		{
			array[i] = this.cachedButtons[i];
		}
		return array;
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0003B4E0 File Offset: 0x000396E0
	private void CacheButtons(int numButtons)
	{
		while (this.cachedButtons.Count < numButtons)
		{
			int tempButtonCount = this.cachedButtons.Count;
			RadialMenu.CachedButtonObject cachedButtonObject = new RadialMenu.CachedButtonObject(Object.Instantiate<GameObject>(this.perButtonTemplateObject, base.transform), this.cachedButtons.Count);
			this.cachedButtons.Add(cachedButtonObject);
			cachedButtonObject.button.OnMouseOver.AddListener(delegate()
			{
				this.ButtonMouseOver(tempButtonCount);
			});
			cachedButtonObject.button.OnMouseOut.AddListener(delegate()
			{
				this.ButtonMouseExit();
			});
		}
		for (int i = 0; i < numButtons; i++)
		{
			if (!this.cachedButtons[i].gameObject.activeSelf)
			{
				this.cachedButtons[i].gameObject.SetActive(true);
			}
		}
		for (int j = numButtons; j < this.cachedButtons.Count; j++)
		{
			this.cachedButtons[j].gameObject.SetActive(false);
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0003B5F0 File Offset: 0x000397F0
	public float GetButtonMaxStringWidth(int whichButton, float scale)
	{
		//RadialMenu.CachedButtonObject cachedButtonObject = this.cachedButtons[whichButton];
		//float y = cachedButtonObject.textHandle.transform.localPosition.y;
		//float num = Mathf.Sqrt(1f - y * y);
		//Vector3 vector = this.buttonLeftEdges[whichButton];
		//Vector3 vector2 = this.buttonRightEdges[whichButton];
		//Vector3 vector3;
		//vector3..ctor(-num, y);
		//Vector3 vector4;
		//vector4..ctor(num, y);
		//if (vector2.x < vector.x)
		//{
		//	Vector3 vector5 = vector;
		//	vector = vector2;
		//	vector2 = vector5;
		//}
		//if (Mathf.Abs(vector.y) > 0.001f)
		//{
		//	float num2 = y / vector.y;
		//	if (num2 > 0f && num2 < 1f)
		//	{
		//		vector3 = vector * num2;
		//	}
		//}
		//if (Mathf.Abs(vector2.y) > 0.001f)
		//{
		//	float num3 = y / vector2.y;
		//	if (num3 > 0f && num3 < 1f)
		//	{
		//		vector4 = vector2 * num3;
		//	}
		//}
		//if (Mathf.Abs(y) <= 0.001f)
		//{
		//	if (cachedButtonObject.textHandle.transform.localPosition.x > 0f)
		//	{
		//		vector3 = Vector3.zero;
		//		vector4 = Vector3.right;
		//	}
		//	else
		//	{
		//		vector3 = -Vector3.right;
		//		vector4 = Vector3.zero;
		//	}
		//}
		//float num4 = Mathf.Abs((vector3.x + vector4.x) * 0.5f - cachedButtonObject.textHandle.transform.localPosition.x) * 2f;
		//return 0.95f * (Mathf.Abs(vector3.x - vector4.x) - num4) / scale;

		return 1f;
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0003B788 File Offset: 0x00039988
	private void AlignTexts(string[] strings)
	{
		float num = 1f / base.transform.localScale.x;
		this.horizontalSqueezedWidth = Mathf.Min(1f, 6.2831855f / (float)strings.Length);
		for (int i = 0; i < strings.Length; i++)
		{
			Transform textHandle = this.cachedButtons[i].textHandle;
			textHandle.localPosition = this.buttonCenters[i];
			int length = strings[i].Length;
			Vector2 normalized = textHandle.localPosition.normalized;
			float num2 = Mathf.Abs(normalized.y);
			num2 = num2 * num2 * num2;
			Vector3 localPosition = textHandle.localPosition;
			localPosition.x += (1f - num2) * 0.1f * Mathf.Sign(localPosition.x);
			textHandle.localPosition = localPosition;
			float buttonMaxStringWidth = this.GetButtonMaxStringWidth(i, num);
			Vector2 sizeDelta = this.cachedButtons[i].trRT.sizeDelta;
			sizeDelta.x = 19f + buttonMaxStringWidth;
			this.cachedButtons[i].trRT.sizeDelta = sizeDelta;
			this.cachedButtons[i].tr.text = strings[i];
			this.cachedButtons[i].buttonCollider.points = this.cachedButtons[i].colliderPoints.ToArray();
			this.cachedButtons[i].ResetIcon();
			RadialMenu.CachedButtonObject value = this.cachedButtons[i];
			value.buttonDir = this.buttonCenters[i].normalized;
			if (this.cachedButtons[i].isNew)
			{
				value.isNew = false;
				textHandle.localScale *= num;
			}
			this.cachedButtons[i] = value;
			if (this.angleText)
			{
				if ((double)Mathf.Abs(textHandle.localPosition.x) <= 0.001)
				{
					textHandle.localRotation = Quaternion.identity;
				}
				else
				{
					float num3;
					if (textHandle.localPosition.x < 0f)
					{
						num3 = Mathf.Atan2(-textHandle.localPosition.y, -textHandle.localPosition.x);
					}
					else
					{
						num3 = Mathf.Atan2(textHandle.localPosition.y, textHandle.localPosition.x);
					}
					textHandle.localRotation = Quaternion.Euler(0f, 0f, num3 * 57.29578f);
				}
			}
		}
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0003BA10 File Offset: 0x00039C10
	private void BuildMesh(int newRadialDivisions)
	{
		//if (!this.mesh)
		//{
		//	this.mesh = new Mesh();
		//}
		//else
		//{
		//	this.mesh.Clear();
		//}
		//this.radialDivisions = newRadialDivisions;
		//this.buttonCenters = new Vector3[this.radialDivisions];
		//this.buttonLeftEdges = new Vector3[this.radialDivisions];
		//this.buttonRightEdges = new Vector3[this.radialDivisions];
		//float num = 6.2831855f / (float)this.radialDivisions;
		//List<Vector3> list = new List<Vector3>();
		//List<int> list2 = new List<int>();
		//List<Color> list3 = new List<Color>();
		//List<Vector2> list4 = new List<Vector2>();
		//float num2 = 360f / (float)this.radialDivisions;
		//float num3 = 6.2831855f / (float)this.radialDivisions;
		//float num4 = 1f / (float)(this.radialDivisions - 1);
		//int num5 = (int)(num2 / this.sliceDegreesPerVert);
		//num5 = Mathf.Max(1, num5 - 2);
		//float num6 = (this.radialDivisions != 1) ? 0f : 100f;
		//for (int i = 0; i < this.radialDivisions; i++)
		//{
		//	float num7 = (float)i * num4;
		//	float num8 = (float)i * num + -1.5707964f;
		//	float num9 = (float)(i + 1) * num + -1.5707964f;
		//	float num10 = (num8 + num9) * 0.5f;
		//	this.buttonCenters[i] = new Vector3(Mathf.Cos(num10), Mathf.Sin(num10)) * this.textPositionDistance;
		//	this.cachedButtons[i].colliderPoints.Clear();
		//	Color item = Color.HSVToRGB(num8 / 6.2831855f, 1f, 1f);
		//	int count = list.Count;
		//	int num11 = count + 1;
		//	int num12 = num11 + 1;
		//	list.Add(Vector3.zero);
		//	list3.Add(item);
		//	list4.Add(new Vector2(0f, num7));
		//	list.Add(new Vector3(Mathf.Cos(num8), Mathf.Sin(num8)));
		//	list.Add(new Vector3(Mathf.Cos(num9), Mathf.Sin(num9)));
		//	list3.Add(item);
		//	list3.Add(item);
		//	list4.Add(new Vector2(num6, num7));
		//	list4.Add(new Vector2(num6, num7));
		//	this.buttonLeftEdges[i] = list[num12];
		//	this.buttonRightEdges[i] = list[num11];
		//	Vector3 vector;
		//	vector..ctor(-list[num11].y, list[num11].x, 0f);
		//	Vector3 vector2;
		//	vector2..ctor(list[num12].y, -list[num12].x, 0f);
		//	if (num5 == 0)
		//	{
		//		list2.Add(count);
		//		list2.Add(num12);
		//		list2.Add(num11);
		//	}
		//	else
		//	{
		//		int item2 = num11;
		//		for (int j = 0; j <= num5; j++)
		//		{
		//			int count2 = list.Count;
		//			float num13 = Mathf.Lerp(num8, num9, ((float)j + 1f) / (float)(num5 + 2));
		//			Vector3 vector3;
		//			vector3..ctor(Mathf.Cos(num13), Mathf.Sin(num13));
		//			float num14 = Mathf.Min(Vector3.Dot(vector3, vector), Vector3.Dot(vector3, vector2));
		//			num14 *= 4.712389f;
		//			if (this.radialDivisions == 1)
		//			{
		//				num14 = 100f;
		//			}
		//			list.Add(vector3);
		//			list3.Add(item);
		//			list4.Add(new Vector2(num14, num7));
		//			list2.Add(count);
		//			list2.Add(count2);
		//			list2.Add(item2);
		//			item2 = count2;
		//		}
		//		list2.Add(count);
		//		list2.Add(num12);
		//		list2.Add(item2);
		//	}
		//	this.cachedButtons[i].colliderPoints.Add(list[count]);
		//	this.cachedButtons[i].colliderPoints.Add(list[num11]);
		//	for (int k = 0; k <= num5; k++)
		//	{
		//		this.cachedButtons[i].colliderPoints.Add(list[count + 3 + k]);
		//	}
		//	this.cachedButtons[i].colliderPoints.Add(list[num12]);
		//}
		//this.mesh.SetVertices(list);
		//this.mesh.SetTriangles(list2.ToArray(), 0);
		//this.mesh.SetColors(list3);
		//this.mesh.SetUVs(0, list4.ToArray());
		//if (!this.mf)
		//{
		//	this.mf = base.GetComponent<MeshFilter>();
		//}
		//if (!this.mr)
		//{
		//	this.mr = base.GetComponent<MeshRenderer>();
		//}
		//if (this.mbp == null)
		//{
		//	this.mbp = new MaterialPropertyBlock();
		//}
		//this.mf.sharedMesh = this.mesh;
		//this.mr.GetPropertyBlock(this.mbp);
		//this.mbp.SetInt("_NumSlices", this.radialDivisions - 1);
		//this.mr.SetPropertyBlock(this.mbp);
	}

	// Token: 0x04000A79 RID: 2681
	public Mesh mesh;

	// Token: 0x04000A7A RID: 2682
	public GameObject perButtonTemplateObject;

	// Token: 0x04000A7B RID: 2683
	private MeshFilter mf;

	// Token: 0x04000A7C RID: 2684
	private MeshRenderer mr;

	// Token: 0x04000A7D RID: 2685
	private int radialDivisions = -1;

	// Token: 0x04000A7E RID: 2686
	public float sliceDegreesPerVert = 10f;

	// Token: 0x04000A7F RID: 2687
	public float textPositionDistance = 0.5f;

	// Token: 0x04000A80 RID: 2688
	public bool angleText;

	// Token: 0x04000A81 RID: 2689
	public int testRadialDivs = 5;

	// Token: 0x04000A82 RID: 2690
	private MaterialPropertyBlock mbp;

	// Token: 0x04000A83 RID: 2691
	private Controller cont = new Controller();

	// Token: 0x04000A84 RID: 2692
	public Transform arrowRotateHandle;

	// Token: 0x04000A85 RID: 2693
	public Transform arrowScaleHandle;

	// Token: 0x04000A86 RID: 2694
	public Transform inputGlyph;

	// Token: 0x04000A87 RID: 2695
	public List<RadialMenu.CachedButtonObject> cachedButtons = new List<RadialMenu.CachedButtonObject>();

	// Token: 0x04000A88 RID: 2696
	[NonSerialized]
	public Vector3[] buttonCenters;

	// Token: 0x04000A89 RID: 2697
	[NonSerialized]
	public Vector3[] buttonLeftEdges;

	// Token: 0x04000A8A RID: 2698
	[NonSerialized]
	public Vector3[] buttonRightEdges;

	// Token: 0x04000A8B RID: 2699
	private const float startOffset = -1.5707964f;

	// Token: 0x04000A8C RID: 2700
	public static int instances;

	// Token: 0x04000A8D RID: 2701
	[HideInInspector]
	public int prevSelectedButton = -1;

	// Token: 0x04000A8E RID: 2702
	[HideInInspector]
	public int mouseSelectedButton = -1;

	// Token: 0x04000A8F RID: 2703
	private const int cutoffLength = 12;

	// Token: 0x04000A90 RID: 2704
	public float horizontalSqueezedWidth;

	// Token: 0x020003FE RID: 1022
	public struct CachedButtonObject
	{
		// Token: 0x06001917 RID: 6423 RVA: 0x00076128 File Offset: 0x00074328
		public CachedButtonObject(GameObject go, int bIndex)
		{
			this.tr = go.GetComponentInChildren<TextMeshPro>(true);
			this.textHandle = go.transform.GetChild(0);
			this.trRT = this.tr.GetComponent<RectTransform>();
			this.button = go.GetComponent<PassiveButton>();
			this.iconSR = go.GetComponentInChildren<SpriteRenderer>(true);
			this.buttonCollider = this.button.GetComponent<PolygonCollider2D>();
			this.rolloverHandler = go.GetComponent<ButtonRolloverHandler>();
			this.gameObject = go;
			this.isNew = true;
			this.colliderPoints = new List<Vector2>();
			this.buttonIndex = bIndex;
			this.baseTextPos = this.tr.transform.localPosition;
			this.buttonDir = Vector3.zero;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000761DC File Offset: 0x000743DC
		public void AddIcon(Sprite iconSprite)
		{
			this.iconSR.enabled = true;
			this.iconSR.sprite = iconSprite;
			Vector3 vector = this.textHandle.localPosition;
			float num = Mathf.Abs(vector.y);
			float num2 = Mathf.Lerp(0.35f, 0.15f, num);
			vector.z = 0f;
			vector = vector.normalized;
			this.tr.transform.localPosition = this.baseTextPos + vector * num2;
			this.iconSR.transform.localPosition = vector * -num2;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00076278 File Offset: 0x00074478
		public void ResetIcon()
		{
			this.iconSR.enabled = false;
			this.tr.transform.localPosition = this.baseTextPos;
		}

		// Token: 0x04001B16 RID: 6934
		public TextMeshPro tr;

		// Token: 0x04001B17 RID: 6935
		public Transform textHandle;

		// Token: 0x04001B18 RID: 6936
		public RectTransform trRT;

		// Token: 0x04001B19 RID: 6937
		public GameObject gameObject;

		// Token: 0x04001B1A RID: 6938
		public PassiveButton button;

		// Token: 0x04001B1B RID: 6939
		public ButtonRolloverHandler rolloverHandler;

		// Token: 0x04001B1C RID: 6940
		public bool isNew;

		// Token: 0x04001B1D RID: 6941
		public PolygonCollider2D buttonCollider;

		// Token: 0x04001B1E RID: 6942
		public List<Vector2> colliderPoints;

		// Token: 0x04001B1F RID: 6943
		public int buttonIndex;

		// Token: 0x04001B20 RID: 6944
		public SpriteRenderer iconSR;

		// Token: 0x04001B21 RID: 6945
		private Vector3 baseTextPos;

		// Token: 0x04001B22 RID: 6946
		public Vector3 buttonDir;
	}
}
