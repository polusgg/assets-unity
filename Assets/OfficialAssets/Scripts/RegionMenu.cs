using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class RegionMenu : MonoBehaviour
{
	// Token: 0x06000737 RID: 1847 RVA: 0x0002DCAF File Offset: 0x0002BEAF
	private void Awake()
	{
		this.defaultButtonSelected = null;
		this.controllerSelectable = new List<UiElement>();
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0002DCC4 File Offset: 0x0002BEC4
	public void OnEnable()
	{
		this.controllerSelectable.Clear();
		int num = 0;
		IRegionInfo[] defaultRegions = ServerManager.DefaultRegions;
		for (int i = 0; i < defaultRegions.Length; i++)
		{
			IRegionInfo regionInfo = defaultRegions[i];
			IRegionInfo region = regionInfo;
			ServerListButton serverListButton = this.ButtonPool.Get<ServerListButton>();
			serverListButton.transform.localPosition = new Vector3(0f, 2f - 0.5f * (float)num, 0f);
			serverListButton.SetTextTranslationId(regionInfo.TranslateName, regionInfo.Name);
			serverListButton.Text.Text = DestroyableSingleton<TranslationController>.Instance.GetStringWithDefault(regionInfo.TranslateName, regionInfo.Name, Array.Empty<object>());
			serverListButton.Text.RefreshMesh();
			serverListButton.Button.OnClick.AddListener(delegate()
			{
				this.ChooseOption(region);
			});
			serverListButton.SetSelected(DestroyableSingleton<ServerManager>.Instance.CurrentRegion.Equals(regionInfo));
			if (DestroyableSingleton<ServerManager>.Instance.CurrentRegion.Equals(regionInfo))
			{
				this.defaultButtonSelected = serverListButton.Button;
			}
			this.controllerSelectable.Add(serverListButton.Button);
			num++;
		}
		if (this.defaultButtonSelected == null && this.controllerSelectable.Count > 0)
		{
			this.defaultButtonSelected = this.controllerSelectable[0];
		}
		ControllerManager.Instance.OpenOverlayMenu(base.name, this.BackButton, this.defaultButtonSelected, this.controllerSelectable, false);
		num++;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0002DE4E File Offset: 0x0002C04E
	private void OpenCustomRegion()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0002DE55 File Offset: 0x0002C055
	public void OnDisable()
	{
		this.ButtonPool.ReclaimAll();
		ControllerManager.Instance.CloseOverlayMenu(base.name);
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0002DE72 File Offset: 0x0002C072
	public void Open()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0002DE80 File Offset: 0x0002C080
	public void ChooseOption(IRegionInfo region)
	{
		DestroyableSingleton<ServerManager>.Instance.SetRegion(region);
		this.RegionText.Text = DestroyableSingleton<TranslationController>.Instance.GetStringWithDefault(region.TranslateName, region.Name, Array.Empty<object>());
		this.Close();
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0002DEB9 File Offset: 0x0002C0B9
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400082C RID: 2092
	public ObjectPoolBehavior ButtonPool;

	// Token: 0x0400082D RID: 2093
	public TextRenderer RegionText;

	// Token: 0x0400082E RID: 2094
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x0400082F RID: 2095
	private UiElement defaultButtonSelected;

	// Token: 0x04000830 RID: 2096
	private List<UiElement> controllerSelectable;
}
