using System;
//using Rewired;
//using Rewired.ControllerExtensions;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020001A2 RID: 418
public class VirtualCursor : MonoBehaviour
{
	// Token: 0x1700008D RID: 141
	// (get) Token: 0x0600094D RID: 2381 RVA: 0x0003CFB4 File Offset: 0x0003B1B4
	public static bool CursorActive
	{
		get
		{
			return VirtualCursor.instance && VirtualCursor.instance.isActiveAndEnabled && SpecialInputHandler.disableVirtualCursorCount == 0;
		}
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0003CFD8 File Offset: 0x0003B1D8
	private void Awake()
	{
		if (VirtualCursor.instance)
		{
            UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		VirtualCursor.instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
		this.sr = base.GetComponentInChildren<SpriteRenderer>(true);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0003D010 File Offset: 0x0003B210
	private void OnEnable()
	{
		this.cam = Camera.main;
		if (this.cam)
		{
			this.SetScreenPosition(Vector2.zero);
		}
		this.framesVisible = 0;
		this.sr.enabled = false;
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0003D048 File Offset: 0x0003B248
	private void OnDestroy()
	{
		if (VirtualCursor.instance == this)
		{
			VirtualCursor.instance = null;
		}
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0003D060 File Offset: 0x0003B260
	private void Start()
	{
		this.cam = Camera.main;
		this.position = base.transform.position;
		if (this.cam)
		{
			float num = (float)Screen.width / (float)Screen.height;
			this.screenBounds = new Vector2(num * this.cam.orthographicSize, this.cam.orthographicSize);
		}
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0003D0C8 File Offset: 0x0003B2C8
	public void SetWorldPosition(Vector2 worldPos)
	{
		VirtualCursor.currentPosition = (base.transform.position = worldPos);
		this.position = worldPos - new Vector2(this.cam.transform.position.x, this.cam.transform.position.y);
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0003D134 File Offset: 0x0003B334
	public void SetScreenPosition(Vector2 screenPos)
	{
		this.position = screenPos;
		VirtualCursor.currentPosition = (base.transform.position = this.position + this.cam.transform.position);
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0003D180 File Offset: 0x0003B380
	private void Update()
	{
		//Player player = ReInput.players.GetPlayer(0);
		//Vector3 vector;
		//vector..ctor(player.GetAxis(VirtualCursor.horizontalAxis), player.GetAxis(VirtualCursor.verticalAxis), 0f);
		//VirtualCursor.joystickMoved = (vector.x != 0f || vector.y != 0f);
		//if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick && SpecialInputHandler.count == 0)
		//{
		//	if (this.framesVisible < 3)
		//	{
		//		this.framesVisible++;
		//	}
		//	else
		//	{
		//		this.sr.enabled = true;
		//	}
		//}
		//else
		//{
		//	this.framesVisible = 0;
		//	this.sr.enabled = false;
		//}
		//if (ControllerManager.Instance.IsUiControllerActive)
		//{
		//	base.gameObject.SetActive(false);
		//	return;
		//}
		//if (player.controllers.joystickCount > 0)
		//{
		//	IDualShock4Extension extension = player.controllers.Joysticks[0].GetExtension<IDualShock4Extension>();
		//	if (extension != null && extension.touchCount > 0)
		//	{
		//		Vector2 vector2;
		//		extension.GetTouchPosition(0, ref vector2);
		//		float num = (float)Screen.width / (float)Screen.height;
		//		if (this.setTouchPos)
		//		{
		//			Vector2 vector3 = vector2 - this.prevTouchPos;
		//			this.position.x = this.position.x + vector3.x * (4f * num);
		//			this.position.y = this.position.y + vector3.y * 4f;
		//		}
		//		this.setTouchPos = true;
		//		this.prevTouchPos = vector2;
		//	}
		//	else
		//	{
		//		this.setTouchPos = false;
		//	}
		//}
		//if (vector.magnitude > 0.03f)
		//{
		//	this.currentSpeed += Time.deltaTime * this.acceleration;
		//	if (this.currentSpeed > this.speed)
		//	{
		//		this.currentSpeed = this.speed;
		//	}
		//	this.position += vector * Time.deltaTime * this.currentSpeed;
		//}
		//else
		//{
		//	this.currentSpeed = 0f;
		//}
		//if (!this.cam)
		//{
		//	this.cam = Camera.main;
		//}
		//if (this.cam)
		//{
		//	float num2 = (float)Screen.width / (float)Screen.height;
		//	this.screenBounds = new Vector2(num2 * this.cam.orthographicSize, this.cam.orthographicSize);
		//	this.position.x = Mathf.Clamp(this.position.x, -this.screenBounds.x, this.screenBounds.x);
		//	this.position.y = Mathf.Clamp(this.position.y, -this.screenBounds.y, this.screenBounds.y);
		//	VirtualCursor.currentPosition = (base.transform.position = this.position + this.cam.transform.position);
		//}
		//VirtualCursor.buttonDown = player.GetButton(11);
	}

	// Token: 0x04000ABA RID: 2746
	public float speed = 5f;

	// Token: 0x04000ABB RID: 2747
	private float currentSpeed;

	// Token: 0x04000ABC RID: 2748
	public float acceleration = 1f;

	// Token: 0x04000ABD RID: 2749
	public Vector2 screenBounds;

	// Token: 0x04000ABE RID: 2750
	public Vector3 position;

	// Token: 0x04000ABF RID: 2751
	private const float deadzone = 0.03f;

	// Token: 0x04000AC0 RID: 2752
	private const float touchpadSensitivity = 4f;

	// Token: 0x04000AC1 RID: 2753
	public Camera cam;

	// Token: 0x04000AC2 RID: 2754
	public static Vector2 currentPosition;

	// Token: 0x04000AC3 RID: 2755
	public static bool buttonDown;

	// Token: 0x04000AC4 RID: 2756
	public static bool joystickMoved;

	// Token: 0x04000AC5 RID: 2757
	public static VirtualCursor instance;

	// Token: 0x04000AC6 RID: 2758
	public static int horizontalAxis = 9;

	// Token: 0x04000AC7 RID: 2759
	public static int verticalAxis = 10;

	// Token: 0x04000AC8 RID: 2760
	private int framesVisible;

	// Token: 0x04000AC9 RID: 2761
	private const int minFramesToAppear = 3;

	// Token: 0x04000ACA RID: 2762
	private SpriteRenderer sr;

	// Token: 0x04000ACB RID: 2763
	private Vector2 prevTouchPos;

	// Token: 0x04000ACC RID: 2764
	private bool setTouchPos;
}
