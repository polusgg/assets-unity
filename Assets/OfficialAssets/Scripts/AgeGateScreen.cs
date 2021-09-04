using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class AgeGateScreen : MonoBehaviour
{
	// Token: 0x06000677 RID: 1655 RVA: 0x0002A002 File Offset: 0x00028202
	private void OnDisable()
	{
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0002A014 File Offset: 0x00028214
	public IEnumerator Show()
	{
		this.monthText.Text = DestroyableSingleton<TranslationController>.Instance.GetMonthStringViaNumber(SaveManager.BirthDateMonth);
		this.dayText.Text = SaveManager.BirthDateDay.ToString();
		this.yearText.Text = SaveManager.BirthDateYear.ToString();
		base.gameObject.SetActive(true);
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.DefaultSelection, this.selectableObjects, false);
		while (base.gameObject.activeSelf)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0002A024 File Offset: 0x00028224
	public void Close()
	{
		if (this.ShakeIfInvalid())
		{
			return;
		}
		SaveManager.BirthDateSetDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
		base.GetComponent<TransitionOpen>().Close();
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0002A05C File Offset: 0x0002825C
	public bool ShakeIfInvalid()
	{
		if (this.yearText.Text == DateTime.UtcNow.Year.ToString())
		{
			base.StartCoroutine(Effects.SwayX(base.transform, 0.75f, 0.25f));
			return true;
		}
		return false;
	}

	// Token: 0x04000754 RID: 1876
	public TextRenderer monthText;

	// Token: 0x04000755 RID: 1877
	public TextRenderer dayText;

	// Token: 0x04000756 RID: 1878
	public TextRenderer yearText;

	// Token: 0x04000757 RID: 1879
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000758 RID: 1880
	public UiElement DefaultSelection;

	// Token: 0x04000759 RID: 1881
	public List<UiElement> selectableObjects;
}
