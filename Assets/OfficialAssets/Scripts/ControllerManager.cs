using System;
using System.Collections.Generic;
//using Rewired;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Token: 0x02000172 RID: 370
public class ControllerManager : MonoBehaviour
{
	// Token: 0x17000087 RID: 135
	// (get) Token: 0x0600086E RID: 2158 RVA: 0x00036D08 File Offset: 0x00034F08
	public bool IsUiControllerActive
	{
		get
		{
			return this.CurrentUiState.BackButton != null || this.CurrentUiState.CurrentSelection != null || !string.IsNullOrEmpty(this.CurrentUiState.MenuName);
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00036D48 File Offset: 0x00034F48
	private void Awake()
	{
		//if (ControllerManager.Instance)
		//{
		//	Object.Destroy(this);
		//	return;
		//}
		//Object.DontDestroyOnLoad(base.gameObject);
		//if (!ControllerManager.Instance)
		//{
		//	ControllerManager.Instance = this;
		//}
		//this.CurrentUiState = new ControllerUiElementsState();
		//this.CurrentUiStateStack = new List<ControllerUiElementsState>();
		//SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00036DAC File Offset: 0x00034FAC
	private void SceneManager_activeSceneChanged(Scene from, Scene to)
	{
		Debug.LogError("Scene changed, clearing stack");
		this.CurrentUiStateStack.Clear();
		this.ResetAll();
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00036DC9 File Offset: 0x00034FC9
	private void Start()
	{
		if (VirtualCursor.instance != null)
		{
			VirtualCursor.instance.gameObject.SetActive(false);
		}
		//this.player = ReInput.players.GetPlayer(this.playerId);
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00036E00 File Offset: 0x00035000
	public void NewScene(string menuName, UiElement backButton, UiElement defaultSelection, List<UiElement> selectableElements, bool gridNav = false)
	{
		GameObject gameObject = GameObject.Find("DisconnectPopup");
		DisconnectPopup disconnectPopup = null;
		if (gameObject != null)
		{
			DisconnectPopup component = gameObject.GetComponent<DisconnectPopup>();
			if (component != null && component.isActiveAndEnabled)
			{
				disconnectPopup = component;
				Debug.LogWarning("DisconnectPopup menu found, restore after new scene.");
			}
		}
		this.ResetAll();
		this.CurrentUiState.EnforceGridNavigation = gridNav;
		this.CurrentUiState.BackButton = backButton;
		this.CurrentUiState.MenuName = menuName;
		this.CurrentUiState.IsScene = true;
		this.SetDefaultSelection(defaultSelection, selectableElements);
		if (VirtualCursor.instance)
		{
			VirtualCursor.instance.gameObject.SetActive(false);
		}
		if (disconnectPopup != null && disconnectPopup.isActiveAndEnabled)
		{
			this.OpenOverlayMenu("DisconnectPopup", disconnectPopup.BackButton);
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00036EC5 File Offset: 0x000350C5
	public void OpenOverlayMenu(string menuName, UiElement backButton)
	{
		this.OpenOverlayMenu(menuName, backButton, null, new List<UiElement>(), false);
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00036ED6 File Offset: 0x000350D6
	public void OpenOverlayMenu(string menuName, UiElement backButton, UiElement defaultSelection)
	{
		this.OpenOverlayMenu(menuName, backButton, defaultSelection, new List<UiElement>(), false);
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00036EE8 File Offset: 0x000350E8
	public void OpenOverlayMenu(string menuName, UiElement backButton, UiElement defaultSelection, List<UiElement> selectableElements, bool gridNav = false)
	{
		if (this.CurrentUiState.MenuName == menuName)
		{
			Debug.LogError("Attempted to open an already-open menu");
			return;
		}
		if (!(this.CurrentUiState.CurrentSelection == null) || !(this.CurrentUiState.BackButton == null) || this.CurrentUiState.IsScene)
		{
			this.CurrentUiStateStack.Add(this.CurrentUiState);
		}
		else
		{
			Debug.LogError("Didn't add current UI state " + this.CurrentUiState.MenuName + " to stack due to lack of selection/back button");
		}
		this.CurrentUiState = new ControllerUiElementsState();
		this.CurrentUiState.EnforceGridNavigation = gridNav;
		this.CurrentUiState.BackButton = backButton;
		this.CurrentUiState.MenuName = menuName;
		this.SetDefaultSelection(defaultSelection, selectableElements);
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00036FB4 File Offset: 0x000351B4
	public void SetDefaultSelection(UiElement defaultSelection, List<UiElement> selectableElements = null)
	{
		if (selectableElements != null)
		{
			if (selectableElements.Count == 0)
			{
				this.CurrentUiState.SelectableUiElements.Clear();
			}
			else
			{
				this.CurrentUiState.SelectableUiElements.AddRange(selectableElements);
			}
		}
		if (defaultSelection == null)
		{
			return;
		}
		if (!this.CurrentUiState.SelectableUiElements.Contains(defaultSelection))
		{
			this.CurrentUiState.SelectableUiElements.Add(defaultSelection);
		}
		this.HighlightSelection(defaultSelection);
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x00037024 File Offset: 0x00035224
	public void SetBackButton(UiElement backButton)
	{
		if (backButton != null)
		{
			this.CurrentUiState.BackButton = backButton;
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0003703B File Offset: 0x0003523B
	public void AddSelectableUiElement(UiElement uiElement, bool forceSelect = false)
	{
		if (uiElement == null)
		{
			return;
		}
		if (!this.CurrentUiState.SelectableUiElements.Contains(uiElement))
		{
			this.CurrentUiState.SelectableUiElements.Add(uiElement);
		}
		if (forceSelect)
		{
			this.HighlightSelection(uiElement);
		}
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x00037078 File Offset: 0x00035278
	public void RemoveSelectableUiElement(UiElement uiElement)
	{
		if (uiElement == null)
		{
			return;
		}
		this.CurrentUiState.SelectableUiElements.Remove(uiElement);
		if (this.CurrentUiState.CurrentSelection == uiElement)
		{
			Debug.Log("RemoveCurrentSelection: " + this.CurrentUiState.CurrentSelection.name);
			this.CurrentUiState.CurrentSelection = null;
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x000370E0 File Offset: 0x000352E0
	public void ClearDestroyedSelectableUiElements()
	{
		if (this.CurrentUiState == null || this.CurrentUiState.SelectableUiElements == null || this.CurrentUiState.SelectableUiElements.Count == 0)
		{
			return;
		}
		this.CurrentUiState.SelectableUiElements.RemoveAll((UiElement item) => item == null);
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00037148 File Offset: 0x00035348
	public void CloseOverlayMenu(string menuName)
	{
		Debug.Log("Menu: " + menuName + " => CloseOverlayMenu()");
		bool flag = false;
		if (this.CurrentUiState.CurrentSelection != null)
		{
			this.CurrentUiState.CurrentSelection.ReceiveMouseOut();
		}
		if (this.CurrentUiStateStack.Count > 0)
		{
			if (string.Equals(this.CurrentUiState.MenuName, menuName, StringComparison.OrdinalIgnoreCase))
			{
				flag = true;
				this.CurrentUiState = this.CurrentUiStateStack[this.CurrentUiStateStack.Count - 1];
				this.CurrentUiStateStack.RemoveAt(this.CurrentUiStateStack.Count - 1);
				this.HighlightSelection(this.CurrentUiState.CurrentSelection);
			}
			else
			{
				Debug.LogWarning("CurrentUiState.MenuName=" + this.CurrentUiState.MenuName + " does not match menu attempting to be closed: " + menuName);
			}
		}
		else if (this.IsUiControllerActive && string.Equals(this.CurrentUiState.MenuName, menuName, StringComparison.OrdinalIgnoreCase))
		{
			flag = true;
			this.ResetAll();
		}
		if (!flag)
		{
			List<ControllerUiElementsState> list = new List<ControllerUiElementsState>();
			foreach (ControllerUiElementsState controllerUiElementsState in this.CurrentUiStateStack)
			{
				if (string.Equals(controllerUiElementsState.MenuName, menuName, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(controllerUiElementsState);
				}
			}
			if (list.Count > 0)
			{
				using (List<ControllerUiElementsState>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ControllerUiElementsState controllerUiElementsState2 = enumerator.Current;
						Debug.LogWarning("Found menu: " + controllerUiElementsState2.MenuName + " in lower stack, removing.");
						this.CurrentUiStateStack.Remove(controllerUiElementsState2);
					}
					return;
				}
			}
			Debug.LogWarning("Attempting to close: " + menuName + ", but menu was never opened.");
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00037324 File Offset: 0x00035524
	public void ResetAll()
	{
		Debug.Log("ResetAll()");
		if (this.CurrentUiState.CurrentSelection != null)
		{
			this.CurrentUiState.CurrentSelection.ReceiveMouseOut();
		}
		this.CurrentUiState.Reset();
		this.CurrentUiStateStack.Clear();
		this.deltaSinceLastUiHighlight = 0f;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00037380 File Offset: 0x00035580
	private void Update()
	{
		//if (!this.IsUiControllerActive)
		//{
		//	return;
		//}
		//if (RadialMenu.instances != 0)
		//{
		//	return;
		//}
		//if (!this.CurrentUiState.CurrentSelection)
		//{
		//	this.PickTopSelectable();
		//}
		//if (this.player.GetButtonDown("MenuCancel"))
		//{
		//	PassiveUiElement passiveUiElement = this.CurrentUiState.BackButton as PassiveUiElement;
		//	if (passiveUiElement != null)
		//	{
		//		passiveUiElement.ReceiveClickDown();
		//	}
		//	else
		//	{
		//		ButtonBehavior buttonBehavior = this.CurrentUiState.BackButton as ButtonBehavior;
		//		if (buttonBehavior != null)
		//		{
		//			buttonBehavior.ReceiveClick();
		//		}
		//	}
		//}
		//if (this.CurrentUiState.CurrentSelection == null)
		//{
		//	return;
		//}
		//this.IsButtonHeld = this.player.GetButton("MenuConfirm");
		//if (this.player.GetButtonDown("MenuConfirm"))
		//{
		//	PassiveUiElement passiveUiElement2 = this.CurrentUiState.CurrentSelection as PassiveUiElement;
		//	if (passiveUiElement2 != null && passiveUiElement2.isActiveAndEnabled)
		//	{
		//		passiveUiElement2.ReceiveClickDown();
		//	}
		//	else
		//	{
		//		ButtonBehavior buttonBehavior2 = this.CurrentUiState.CurrentSelection as ButtonBehavior;
		//		if (buttonBehavior2 != null && buttonBehavior2.isActiveAndEnabled)
		//		{
		//			buttonBehavior2.ReceiveClick();
		//		}
		//	}
		//}
		//this.deltaSinceLastUiHighlight += Time.deltaTime;
		//bool flag = false;
		//bool buttonDown = this.player.GetButtonDown("MenuUp");
		//bool buttonDown2 = this.player.GetButtonDown("MenuDown");
		//bool buttonDown3 = this.player.GetButtonDown("MenuRight");
		//bool buttonDown4 = this.player.GetButtonDown("MenuLeft");
		//float num = 0f;
		//float num2 = 0f;
		//if (buttonDown || buttonDown2)
		//{
		//	num2 = (float)(buttonDown ? 1 : -1);
		//	flag = true;
		//}
		//else if (buttonDown3 || buttonDown4)
		//{
		//	num = (float)(buttonDown3 ? 1 : -1);
		//	flag = true;
		//}
		//else
		//{
		//	num = this.player.GetAxis("MenuHorizontal");
		//	num2 = this.player.GetAxis("MenuVertical");
		//}
		//if (num != 0f || num2 != 0f)
		//{
		//	SlideBar slideBar = this.CurrentUiState.CurrentSelection as SlideBar;
		//	if (slideBar != null)
		//	{
		//		if (slideBar.Vertical && num2 != 0f && (double)Mathf.Abs(num2) > 0.3)
		//		{
		//			this.VerticalAxisInputForSlideBar(num2, slideBar, flag);
		//			return;
		//		}
		//		if (num != 0f && (double)Mathf.Abs(num) > 0.3)
		//		{
		//			this.HorizontalAxisInputForSlideBar(num, slideBar, flag);
		//			return;
		//		}
		//	}
		//	if (this.deltaSinceLastUiHighlight > 0.25f || !this.inputDetectedLastFrame || flag)
		//	{
		//		UiElement uiElement = null;
		//		Vector2 vector = this.CurrentUiState.CurrentSelection.transform.position;
		//		Vector2 vector2 = new Vector3(num, num2);
		//		if (!this.CurrentUiState.EnforceGridNavigation)
		//		{
		//			foreach (RaycastHit2D raycastHit2D in Physics2D.CircleCastAll(vector, 0.15f, vector2))
		//			{
		//				if (raycastHit2D.collider != null && raycastHit2D.transform.gameObject != this.CurrentUiState.CurrentSelection.gameObject)
		//				{
		//					UiElement component = raycastHit2D.transform.gameObject.GetComponent<UiElement>();
		//					if (component != null && this.CurrentUiState.SelectableUiElements.Contains(component))
		//					{
		//						uiElement = component;
		//						break;
		//					}
		//				}
		//			}
		//		}
		//		AxisDirection direction = this.FindClosestDirection(vector2);
		//		if (uiElement == null)
		//		{
		//			uiElement = this.FindUiElementByGridDirection(vector, direction);
		//		}
		//		if (uiElement != null)
		//		{
		//			this.HighlightSelection(uiElement);
		//		}
		//	}
		//	this.inputDetectedLastFrame = true;
		//	return;
		//}
		//this.deltaSinceLastUiHighlight = 0f;
		//this.inputDetectedLastFrame = false;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x00037728 File Offset: 0x00035928
	private AxisDirection FindClosestDirection(Vector2 direction)
	{
		float num = float.MinValue;
		AxisDirection result = AxisDirection.None;
		foreach (KeyValuePair<Vector2, AxisDirection> keyValuePair in this.directions)
		{
			float num2 = Vector2.Dot(direction, keyValuePair.Key);
			if (num2 > num)
			{
				num = num2;
				result = keyValuePair.Value;
			}
		}
		return result;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0003779C File Offset: 0x0003599C
	private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2, Vector2 direction)
	{
		Vector2 vector = vec2 - vec1;
		float num = (vec2.y < vec1.y) ? -1f : 1f;
		return Vector2.Angle(direction, vector) * num;
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x000377D8 File Offset: 0x000359D8
	private UiElement FindUiElementByGridDirection(Vector2 origin, AxisDirection direction)
	{
		if (direction == AxisDirection.None)
		{
			return null;
		}
		UiElement uiElement = null;
		if (direction == AxisDirection.Up)
		{
			Dictionary<UiElement, SelectableUiElementCalculation> dictionary = new Dictionary<UiElement, SelectableUiElementCalculation>();
			float num = float.MaxValue;
			foreach (UiElement uiElement2 in this.CurrentUiState.SelectableUiElements)
			{
				if (!(uiElement2 == null) && uiElement2.isActiveAndEnabled && !(uiElement2 == this.CurrentUiState.CurrentSelection))
				{
					float y = uiElement2.gameObject.transform.position.y;
					if (y > origin.y)
					{
						float angle = Mathf.Abs(this.AngleBetweenVector2(origin, uiElement2.gameObject.transform.position, Vector2.up));
						float num2 = y - origin.y;
						if (num2 < num && num2 != 0f)
						{
							num = num2;
						}
						dictionary.Add(uiElement2, new SelectableUiElementCalculation
						{
							Angle = angle,
							Distance = num2
						});
					}
				}
			}
			num += 0.1f;
			float num3 = float.MaxValue;
			using (Dictionary<UiElement, SelectableUiElementCalculation>.Enumerator enumerator2 = dictionary.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<UiElement, SelectableUiElementCalculation> keyValuePair = enumerator2.Current;
					if (keyValuePair.Value.Distance <= num && (double)keyValuePair.Value.Distance < (double)num + 0.25 && keyValuePair.Value.Angle < num3)
					{
						num3 = keyValuePair.Value.Angle;
						uiElement = keyValuePair.Key;
					}
				}
				goto IL_547;
			}
		}
		if (direction == AxisDirection.Down)
		{
			Dictionary<UiElement, SelectableUiElementCalculation> dictionary2 = new Dictionary<UiElement, SelectableUiElementCalculation>();
			float num4 = float.MaxValue;
			foreach (UiElement uiElement3 in this.CurrentUiState.SelectableUiElements)
			{
				if (!(uiElement3 == null) && uiElement3.isActiveAndEnabled && !(uiElement3 == this.CurrentUiState.CurrentSelection))
				{
					float y2 = uiElement3.gameObject.transform.position.y;
					if (y2 < origin.y)
					{
						float angle2 = Mathf.Abs(this.AngleBetweenVector2(origin, uiElement3.gameObject.transform.position, Vector2.down));
						float num5 = origin.y - y2;
						if (num5 < num4 && num5 != 0f)
						{
							num4 = num5;
						}
						dictionary2.Add(uiElement3, new SelectableUiElementCalculation
						{
							Angle = angle2,
							Distance = num5
						});
					}
				}
			}
			num4 += 0.1f;
			float num6 = float.MaxValue;
			using (Dictionary<UiElement, SelectableUiElementCalculation>.Enumerator enumerator2 = dictionary2.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<UiElement, SelectableUiElementCalculation> keyValuePair2 = enumerator2.Current;
					if (keyValuePair2.Value.Distance <= num4 && (double)keyValuePair2.Value.Distance < (double)num4 + 0.25 && keyValuePair2.Value.Angle < num6)
					{
						num6 = keyValuePair2.Value.Angle;
						uiElement = keyValuePair2.Key;
					}
				}
				goto IL_547;
			}
		}
		if (direction == AxisDirection.Left)
		{
			float num7 = float.MaxValue;
			float num8 = float.MaxValue;
			using (List<UiElement>.Enumerator enumerator = this.CurrentUiState.SelectableUiElements.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UiElement uiElement4 = enumerator.Current;
					if (!(uiElement4 == null) && uiElement4.isActiveAndEnabled && !(uiElement4 == this.CurrentUiState.CurrentSelection))
					{
						float x = uiElement4.gameObject.transform.position.x;
						if (x < origin.x)
						{
							float num9 = Mathf.Abs(Mathf.Abs(origin.y) - Mathf.Abs(uiElement4.gameObject.transform.position.y));
							if ((double)num9 <= 0.25)
							{
								float num10 = origin.x - x;
								if (num10 < num7 && (num9 < num8 || num9 == 0f))
								{
									num7 = num10;
									num8 = num9;
									uiElement = uiElement4;
								}
							}
						}
					}
				}
				goto IL_547;
			}
		}
		if (direction == AxisDirection.Right)
		{
			float num11 = float.MaxValue;
			float num12 = float.MaxValue;
			foreach (UiElement uiElement5 in this.CurrentUiState.SelectableUiElements)
			{
				if (!(uiElement5 == null) && uiElement5.isActiveAndEnabled && !(uiElement5 == this.CurrentUiState.CurrentSelection))
				{
					float x2 = uiElement5.gameObject.transform.position.x;
					if (x2 > origin.x)
					{
						float num13 = Mathf.Abs(Mathf.Abs(origin.y) - Mathf.Abs(uiElement5.gameObject.transform.position.y));
						if ((double)num13 <= 0.25)
						{
							float num14 = x2 - origin.x;
							if (num14 < num11 && (num13 < num12 || num13 == 0f))
							{
								num11 = num14;
								num12 = num13;
								uiElement = uiElement5;
							}
						}
					}
				}
			}
		}
		IL_547:
		if (uiElement != null)
		{
			Debug.Log("FindUiElementByGridDirection: " + uiElement.gameObject.name);
		}
		return uiElement;
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00037D9C File Offset: 0x00035F9C
	[Obsolete("FindUiElementByDistance is deprecated, please use FindUiElementByGridDirection instead.")]
	private UiElement FindUiElementByDistance(Vector2 origin, AxisDirection direction)
	{
		UiElement uiElement = null;
		float num = float.MinValue;
		if (direction == AxisDirection.Up)
		{
			float y = origin.y;
			using (List<UiElement>.Enumerator enumerator = this.CurrentUiState.SelectableUiElements.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UiElement uiElement2 = enumerator.Current;
					if (uiElement2.isActiveAndEnabled)
					{
						Vector2 vector = uiElement2.transform.position;
						if (y < vector.y)
						{
							float num2 = Vector2.Distance(origin, vector);
							if (uiElement == null || num2 < num)
							{
								uiElement = uiElement2;
								num = num2;
							}
						}
					}
				}
				return uiElement;
			}
		}
		if (direction == AxisDirection.Down)
		{
			float y2 = origin.y;
			using (List<UiElement>.Enumerator enumerator = this.CurrentUiState.SelectableUiElements.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UiElement uiElement3 = enumerator.Current;
					if (uiElement3.isActiveAndEnabled)
					{
						Vector2 vector2 = uiElement3.transform.position;
						if (y2 > vector2.y)
						{
							float num3 = Vector2.Distance(origin, vector2);
							if (uiElement == null || num3 < num)
							{
								uiElement = uiElement3;
								num = num3;
							}
						}
					}
				}
				return uiElement;
			}
		}
		if (direction == AxisDirection.Left)
		{
			float x = origin.x;
			using (List<UiElement>.Enumerator enumerator = this.CurrentUiState.SelectableUiElements.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UiElement uiElement4 = enumerator.Current;
					if (uiElement4.isActiveAndEnabled)
					{
						Vector2 vector3 = uiElement4.transform.position;
						if (x > vector3.x)
						{
							float num4 = Vector2.Distance(origin, vector3);
							if (uiElement == null || num4 < num)
							{
								uiElement = uiElement4;
								num = num4;
							}
						}
					}
				}
				return uiElement;
			}
		}
		if (direction == AxisDirection.Right)
		{
			float x2 = origin.x;
			foreach (UiElement uiElement5 in this.CurrentUiState.SelectableUiElements)
			{
				if (uiElement5.isActiveAndEnabled)
				{
					Vector2 vector4 = uiElement5.transform.position;
					if (x2 < vector4.x)
					{
						float num5 = Vector2.Distance(origin, vector4);
						if (uiElement == null || num5 < num)
						{
							uiElement = uiElement5;
							num = num5;
						}
					}
				}
			}
		}
		return uiElement;
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00038014 File Offset: 0x00036214
	private void VerticalAxisInputForSlideBar(float v, SlideBar slideBar, bool dpadDetected)
	{
		if (this.deltaSinceLastUiHighlight > 0.1f || dpadDetected)
		{
			if (v > 0f)
			{
				slideBar.ControllerIncrease();
			}
			else
			{
				slideBar.ControllerDecrease();
			}
			this.deltaSinceLastUiHighlight = 0f;
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00038048 File Offset: 0x00036248
	private void HorizontalAxisInputForSlideBar(float h, SlideBar slideBar, bool dpadDetected)
	{
		if (this.deltaSinceLastUiHighlight > 0.1f || dpadDetected)
		{
			if (h > 0f)
			{
				slideBar.ControllerIncrease();
			}
			else
			{
				slideBar.ControllerDecrease();
			}
			this.deltaSinceLastUiHighlight = 0f;
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0003807C File Offset: 0x0003627C
	private void HighlightSelection(UiElement selection)
	{
		if (!(selection == null) && selection.isActiveAndEnabled)
		{
			if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
			{
				if (this.CurrentUiState.CurrentSelection)
				{
					this.CurrentUiState.CurrentSelection.ReceiveMouseOut();
				}
				if (DestroyableSingleton<PassiveButtonManager>.InstanceExists)
				{
					DestroyableSingleton<PassiveButtonManager>.Instance.LoseFocusForAll();
				}
				selection.ReceiveMouseOver();
			}
			this.CurrentUiState.CurrentSelection = selection;
			this.deltaSinceLastUiHighlight = 0f;
			return;
		}
		if (selection)
		{
			Debug.LogError("Failed to highlight, selection.isActiveAndEnabled=" + selection.isActiveAndEnabled.ToString());
			return;
		}
		Debug.LogError("Failed to highlight, selection is null");
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x00038122 File Offset: 0x00036322
	public void SetCurrentSelected(UiElement selection)
	{
		this.HighlightSelection(selection);
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0003812C File Offset: 0x0003632C
	public void PickTopSelectable()
	{
		float num = -10000f;
		UiElement uiElement = null;
		foreach (UiElement uiElement2 in this.CurrentUiState.SelectableUiElements)
		{
			if (uiElement2 && uiElement2.isActiveAndEnabled && uiElement2.transform.position.y > num)
			{
				num = uiElement2.transform.position.y;
				uiElement = uiElement2;
			}
		}
		if (uiElement)
		{
			this.HighlightSelection(uiElement);
		}
	}

	// Token: 0x040009DE RID: 2526
	public static ControllerManager Instance;

	// Token: 0x040009DF RID: 2527
	[HideInInspector]
	[SerializeField]
	private int playerId;

	// Token: 0x040009E0 RID: 2528
	//[SerializeField]
	//private Player player;

	// Token: 0x040009E1 RID: 2529
	private const string DISCONNECT_POPUP_MENUNAME = "DisconnectPopup";

	// Token: 0x040009E2 RID: 2530
	private const float DELTA_SINCE_LAST_BUTTON_HIGHLIGHT_THRESHOLD = 0.25f;

	// Token: 0x040009E3 RID: 2531
	private const float DELTA_SINCE_LAST_SLIDER_MOVEMENT_THRESHOLD = 0.1f;

	// Token: 0x040009E4 RID: 2532
	private bool inputDetectedLastFrame;

	// Token: 0x040009E5 RID: 2533
	[HideInInspector]
	public bool IsButtonHeld;

	// Token: 0x040009E6 RID: 2534
	public ControllerUiElementsState CurrentUiState;

	// Token: 0x040009E7 RID: 2535
	private List<ControllerUiElementsState> CurrentUiStateStack;

	// Token: 0x040009E8 RID: 2536
	private float deltaSinceLastUiHighlight;

	// Token: 0x040009E9 RID: 2537
	private bool disabledVirtualCursor;

	// Token: 0x040009EA RID: 2538
	private Dictionary<Vector2, AxisDirection> directions = new Dictionary<Vector2, AxisDirection>
	{
		{
			Vector2.up,
			AxisDirection.Up
		},
		{
			Vector2.down,
			AxisDirection.Down
		},
		{
			Vector2.left,
			AxisDirection.Left
		},
		{
			Vector2.right,
			AxisDirection.Right
		}
	};
}
