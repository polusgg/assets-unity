using System;
using UnityEngine;

// Token: 0x02000197 RID: 407
[RequireComponent(typeof(Scroller))]
public class ScrollToSelection : MonoBehaviour
{
	// Token: 0x06000922 RID: 2338 RVA: 0x0003C3A2 File Offset: 0x0003A5A2
	private void Awake()
	{
		this.scrollRect = base.GetComponent<Scroller>();
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0003C3B0 File Offset: 0x0003A5B0
	private void OnEnable()
	{
		this.childElements = base.GetComponentsInChildren<UIScrollbarHelper>(false);
		this.scrollRect = base.GetComponent<Scroller>();
		this.wantedValue = 0f;
		for (int i = 0; i < this.childElements.Length; i++)
		{
			this.childElements[i].index = i;
		}
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0003C404 File Offset: 0x0003A604
	private void Start()
	{
		this.childElements = base.GetComponentsInChildren<UIScrollbarHelper>(false);
		for (int i = 0; i < this.childElements.Length; i++)
		{
			this.childElements[i].index = i;
		}
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0003C440 File Offset: 0x0003A640
	private void Update()
	{
		if (!this.scrollRect.enabled)
		{
			this.onePage = true;
			return;
		}
		this.cursorEnabled = (Controller.currentTouchType > Controller.TouchType.Joystick);
		if (this.cursorEnabled)
		{
			this.wantedValue = this.scrollRect.Inner.localPosition.y;
			return;
		}
		this.onePage = false;
		GameObject gameObject = ControllerManager.Instance.CurrentUiState.CurrentSelection ? ControllerManager.Instance.CurrentUiState.CurrentSelection.gameObject : null;
		if (this.lastSelectedObject != gameObject)
		{
			if (gameObject && gameObject.transform.IsChildOf(base.transform))
			{
				this.ScrollToRect(gameObject.transform);
			}
			this.lastSelectedObject = gameObject;
			if (!gameObject)
			{
				this.killScroll = true;
			}
		}
		if (this.scrollRect.allowY)
		{
			if (!this.killScroll && this.scrollRect.Inner.localPosition.y != this.wantedValue)
			{
				Vector3 localPosition = this.scrollRect.Inner.localPosition;
				localPosition.y = Mathf.Lerp(localPosition.y, this.wantedValue, Time.unscaledDeltaTime * 3f);
				this.scrollRect.Inner.localPosition = localPosition;
				return;
			}
			if (this.killScroll)
			{
				this.wantedValue = this.scrollRect.Inner.localPosition.y;
			}
		}
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
	private void ScrollToRect(Transform targetRectTransform)
	{
		if (this.scrollRect.allowY)
		{
			UIScrollbarHelper uiscrollbarHelper = targetRectTransform.GetComponent<UIScrollbarHelper>();
			if (uiscrollbarHelper == null)
			{
				uiscrollbarHelper = targetRectTransform.GetComponentInParent<UIScrollbarHelper>();
			}
			float num = (this.scrollRect.Colliders.Length != 0) ? this.scrollRect.Colliders[0].transform.localPosition.y : 0f;
			if (uiscrollbarHelper == null)
			{
				this.wantedValue = -this.scrollRect.Inner.transform.localPosition.y + num;
				this.killScroll = true;
				return;
			}
			if (this.childElements.Length == 0)
			{
				this.childElements = base.GetComponentsInChildren<UIScrollbarHelper>(false);
				for (int i = 0; i < this.childElements.Length; i++)
				{
					this.childElements[i].index = i;
				}
				if (this.childElements.Length == 0)
				{
					return;
				}
			}
			this.killScroll = false;
			this.wantedValue = Mathf.Clamp(-uiscrollbarHelper.transform.localPosition.y + num, this.scrollRect.YBounds.min, this.scrollRect.YBounds.max);
		}
	}

	// Token: 0x04000A9A RID: 2714
	private Scroller scrollRect;

	// Token: 0x04000A9B RID: 2715
	private UIScrollbarHelper[] childElements;

	// Token: 0x04000A9C RID: 2716
	public float wantedValue;

	// Token: 0x04000A9D RID: 2717
	private GameObject lastSelectedObject;

	// Token: 0x04000A9E RID: 2718
	public bool cursorEnabled;

	// Token: 0x04000A9F RID: 2719
	public bool killScroll;

	// Token: 0x04000AA0 RID: 2720
	public bool onePage;
}
