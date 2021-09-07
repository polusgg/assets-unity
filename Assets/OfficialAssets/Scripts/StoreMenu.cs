using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PowerTools;
using UnityEngine;
//using UnityEngine.Purchasing;
//using UnityEngine.Purchasing.Extension;
//using UnityEngine.Purchasing.Security;

// Token: 0x02000135 RID: 309
public class StoreMenu : DestroyableSingleton<StoreMenu>//, IStoreListener
{
	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000753 RID: 1875 RVA: 0x0002E050 File Offset: 0x0002C250
	// (set) Token: 0x06000754 RID: 1876 RVA: 0x0002E058 File Offset: 0x0002C258
	public PurchaseStates PurchaseState { get; private set; }

	// Token: 0x06000755 RID: 1877 RVA: 0x0002E064 File Offset: 0x0002C264
	public void Start()
	{
		//this.controllerNavMenu = base.GetComponent<ControllerNavMenu>();
		//base.gameObject.SetActive(false);
		//this.PetSlot.gameObject.SetActive(false);
		//ItchIoPurchasingModule itchIoPurchasingModule = new ItchIoPurchasingModule();
		//foreach (PetBehaviour petBehaviour in DestroyableSingleton<HatManager>.Instance.AllPets)
		//{
		//	if (petBehaviour.ItchId != 0)
		//	{
		//		itchIoPurchasingModule.IdTranslator.Add(petBehaviour.ProdId, petBehaviour.ItchId);
		//		itchIoPurchasingModule.UrlTranslator.Add(petBehaviour.ProdId, petBehaviour.ItchUrl);
		//	}
		//}
		//ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(itchIoPurchasingModule, Array.Empty<IPurchasingModule>());
		//foreach (PetBehaviour petBehaviour2 in DestroyableSingleton<HatManager>.Instance.AllPets)
		//{
		//	if (!petBehaviour2.Free && !petBehaviour2.NotInStore && !string.IsNullOrEmpty(petBehaviour2.ProdId))
		//	{
		//		configurationBuilder.AddProduct(petBehaviour2.ProdId, 1);
		//	}
		//}
		//foreach (MapBuyable mapBuyable in DestroyableSingleton<HatManager>.Instance.AllMaps)
		//{
		//	configurationBuilder.AddProduct(mapBuyable.ProdId, 1);
		//}
		//UnityPurchasing.Initialize(this, configurationBuilder);
		//this.PurchaseBackground.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		//this.PriceText.Color = new Color(0.8f, 0.8f, 0.8f, 1f);
		//this.PriceText.Text = "";
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0002E244 File Offset: 0x0002C444
	public void Update()
	{
		this.TopArrow.enabled = !this.Scroller.AtTop;
		this.BottomArrow.enabled = !this.Scroller.AtBottom;
		if (DestroyableSingleton<HudManager>.InstanceExists)
		{
			Vector3 position = DestroyableSingleton<HudManager>.Instance.transform.position;
			position.z -= 100f;
			base.transform.position = position;
			return;
		}
		base.transform.position = new Vector3(0f, 0f, -100f);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0002E2D6 File Offset: 0x0002C4D6
	public void Open()
	{
		//if (this.controller != null)
		//{
		//	this.ShowAllButtons();
		//}
		//base.gameObject.SetActive(true);
		//if (this.controllerNavMenu)
		//{
		//	this.controllerNavMenu.OpenMenu();
		//}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0002E30A File Offset: 0x0002C50A
	public void RestorePurchases()
	{
		//this.extensions.GetExtension<ItchIoPurchasingModule>().RestorePurchases(delegate
		//{
		//	this.RestorePurchasesButton.Text = "Purchases Restored";
		//});
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0002E328 File Offset: 0x0002C528
	private void DestroySliderObjects()
	{
		//for (int i = 0; i < this.AllObjects.Count; i++)
		//{
		//	Object.Destroy(this.AllObjects[i]);
		//}
		//this.AllObjects.Clear();
		//this.controllerNavMenu.DefaultButtonSelected = null;
		//this.controllerNavMenu.ControllerSelectable.Clear();
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0002E383 File Offset: 0x0002C583
	private void FinishRestoring()
	{
		this.ShowAllButtons();
		this.RestorePurchasesButton.Text = "Purchases Restored";
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0002E39C File Offset: 0x0002C59C
	public void SetProduct(PurchaseButton button)
	{
		if (this.PurchaseState == PurchaseStates.Started)
		{
			return;
		}
		if (!button || button.Product == null)
		{
			return;
		}
		try
		{
			this.CurrentButton = button;
			if (this.CurrentButton.Product is HatBehaviour)
			{
				HatBehaviour hatBehaviour = (HatBehaviour)this.CurrentButton.Product;
				this.HatSlot.gameObject.SetActive(true);
				this.SkinSlot.gameObject.SetActive(false);
				this.PetSlot.gameObject.SetActive(false);
				this.HatSlot.SetHat(hatBehaviour, 0);
				this.ItemName.Text = (string.IsNullOrWhiteSpace(hatBehaviour.StoreName) ? hatBehaviour.name : hatBehaviour.StoreName);
				if (hatBehaviour.RelatedSkin)
				{
					TextRenderer itemName = this.ItemName;
					itemName.Text += " (Includes skin!)";
					this.SkinSlot.gameObject.SetActive(true);
					PlayerControl.SetSkinImage(hatBehaviour.RelatedSkin, this.SkinSlot);
				}
			}
			else if (this.CurrentButton.Product is SkinData)
			{
				SkinData skinData = (SkinData)this.CurrentButton.Product;
				this.SkinSlot.gameObject.SetActive(true);
				this.HatSlot.gameObject.SetActive(true);
				this.PetSlot.gameObject.SetActive(false);
				this.HatSlot.SetHat(skinData.RelatedHat, 0);
				PlayerControl.SetSkinImage(skinData, this.SkinSlot);
				this.ItemName.Text = (string.IsNullOrWhiteSpace(skinData.StoreName) ? skinData.name : skinData.StoreName);
			}
			else if (this.CurrentButton.Product is PetBehaviour)
			{
				PetBehaviour petBehaviour = (PetBehaviour)this.CurrentButton.Product;
				this.SkinSlot.gameObject.SetActive(false);
				HatBehaviour hatByProdId = DestroyableSingleton<HatManager>.Instance.GetHatByProdId(petBehaviour.ProdId);
				if (hatByProdId)
				{
					this.HatSlot.gameObject.SetActive(true);
					this.HatSlot.SetHat(hatByProdId, 0);
				}
				else
				{
					this.HatSlot.gameObject.SetActive(false);
				}
				this.PetSlot.gameObject.SetActive(true);
				SpriteRenderer component = this.PetSlot.GetComponent<SpriteRenderer>();
				component.material = new Material(petBehaviour.rend.sharedMaterial);
				PlayerControl.SetPlayerMaterialColors((int)SaveManager.BodyColor, component);
				this.PetSlot.Play(petBehaviour.idleClip, 1f);
				// this.ItemName.Text = (string.IsNullOrWhiteSpace(petBehaviour.StoreName) ? petBehaviour.name : petBehaviour.StoreName);
			}
			else if (this.CurrentButton.Product is MapBuyable)
			{
				MapBuyable mapBuyable = (MapBuyable)this.CurrentButton.Product;
				this.SkinSlot.gameObject.SetActive(false);
				this.HatSlot.gameObject.SetActive(false);
				this.PetSlot.gameObject.SetActive(false);
				this.ItemName.Text = mapBuyable.StoreName;
			}
			else
			{
				this.HatSlot.gameObject.SetActive(false);
				this.SkinSlot.gameObject.SetActive(false);
				this.PetSlot.gameObject.SetActive(false);
				this.ItemName.Text = "Remove All Ads";
			}
			if (button.Purchased)
			{
				this.PurchaseBackground.color = new Color(0.5f, 0.5f, 0.5f, 1f);
				this.PriceText.Color = new Color(0.8f, 0.8f, 0.8f, 1f);
				this.PriceText.Text = "Owned";
			}
			else
			{
				this.PurchaseBackground.color = Color.white;
				this.PriceText.Color = Color.white;
				this.PriceText.Text = button.Price;
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Could set product: " + button.ProductId);
			throw ex;
		}
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0002E7C4 File Offset: 0x0002C9C4
	public void BuyProduct()
	{
		if (!this.CurrentButton || this.CurrentButton.Purchased || this.PurchaseState == PurchaseStates.Started)
		{
			return;
		}
		base.StartCoroutine(this.WaitForPurchaseAds(this.CurrentButton));
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0002E7FD File Offset: 0x0002C9FD
	public IEnumerator WaitForPurchaseAds(PurchaseButton button)
	{
		//this.PurchaseState = PurchaseStates.Started;
		//this.controller.InitiatePurchase(button.ProductId);
		//while (this.PurchaseState == PurchaseStates.Started)
		//{
		//	yield return null;
		//}
		//if (this.PurchaseState == PurchaseStates.Success)
		//{
		//	foreach (PurchaseButton purchaseButton in from p in this.AllObjects
		//	select p.GetComponent<PurchaseButton>() into h
		//	where h && h.ProductId == button.ProductId
		//	select h)
		//	{
		//		purchaseButton.SetPurchased();
		//	}
		//}
		//this.SetProduct(button);
		yield break;
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0002E814 File Offset: 0x0002CA14
	public void Close()
	{
		//HatsTab hatsTab = Object.FindObjectOfType<HatsTab>();
		//if (hatsTab)
		//{
		//	hatsTab.OnDisable();
		//	hatsTab.OnEnable();
		//}
		//base.gameObject.SetActive(false);
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0002E848 File Offset: 0x0002CA48
	private void ShowAllButtons()
	{
		//this.DestroySliderObjects();
		//string text = "";
		//try
		//{
		//	text = "Couldn't fetch products";
		//	Product[] all = this.controller.products.all;
		//	text = "Couldn't validate products";
		//	for (int i = 0; i < all.Length; i++)
		//	{
		//		try
		//		{
		//			Product product = all[i];
		//			if (product != null && product.hasReceipt)
		//			{
		//				Debug.Log("Validating: " + product.definition.id);
		//				SaveManager.SetPurchased(product.definition.id);
		//			}
		//		}
		//		catch (InvalidSignatureException ex)
		//		{
		//			Debug.LogError("Invalid signature: " + ex.Message);
		//		}
		//	}
		//	text = "Couldn't place products";
		//	Vector3 vector;
		//	vector..ctor(this.XRange.Lerp(0.5f), this.ItemListStartY);
		//	this.RestorePurchasesObj.SetActive(true);
		//	vector.y += -0.45f;
		//	text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.PetFailFetchData, Array.Empty<object>());
		//	vector.y += -0.375f;
		//	List<MapBuyable> allMaps = DestroyableSingleton<HatManager>.Instance.AllMaps;
		//	vector = this.InsertMapsFromList(vector, all, allMaps);
		//	text = "Couldn't fetch pet data";
		//	vector.y += -0.375f;
		//	PetBehaviour[] array = (from h in DestroyableSingleton<HatManager>.Instance.AllPets
		//	where !h.Free && !h.NotInStore
		//	select h into p
		//	orderby p.StoreName
		//	select p).ToArray<PetBehaviour>();
		//	vector = this.InsertBanner(vector, DestroyableSingleton<TranslationController>.Instance.GetImage(ImageNames.ShopPetsBanner));
		//	Vector3 position = vector;
		//	Product[] allProducts = all;
		//	IBuyable[] hats = array;
		//	vector = this.InsertHatsFromList(position, allProducts, hats);
		//	text = "Couldn't finalize menu";
		//	this.Scroller.YBounds.max = Mathf.Max(0f, -vector.y - 2.5f);
		//	try
		//	{
		//		this.LoadingText.gameObject.SetActive(false);
		//	}
		//	catch
		//	{
		//	}
		//}
		//catch (Exception ex2)
		//{
		//	string str = "Exception: ";
		//	string str2 = text;
		//	string str3 = ": ";
		//	Exception ex3 = ex2;
		//	Debug.Log(str + str2 + str3 + ((ex3 != null) ? ex3.ToString() : null));
		//	this.DestroySliderObjects();
		//	this.LoadingText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.LoadingFailed, Array.Empty<object>()) + "\r\n" + text;
		//	this.LoadingText.gameObject.SetActive(true);
		//}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0002EAF0 File Offset: 0x0002CCF0
	private Vector3 InsertHortLine(Vector3 position)
	{
		//position.x = 1.2f;
		//SpriteRenderer spriteRenderer = Object.Instantiate<SpriteRenderer>(this.HortLinePrefab, this.Scroller.Inner);
		//spriteRenderer.transform.localPosition = position;
		//spriteRenderer.gameObject.SetActive(true);
		//position.y += -0.33749998f;
		return position;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x0002EB48 File Offset: 0x0002CD48
	//private Vector3 InsertMapsFromList(Vector3 position, Product[] allProducts, List<MapBuyable> maps)
	//{
	//	position.y += -0.1875f;
	//	for (int i = maps.Count - 1; i >= 0; i--)
	//	{
	//		MapBuyable mapItem = maps[i];
	//		Product product = allProducts.FirstOrDefault((Product p) => mapItem.ProdId == p.definition.id);
	//		if (product != null && product.definition != null && product.availableToPurchase)
	//		{
	//			position.x = this.XRange.Lerp(0.5f);
	//			IBuyable[] bundleSkins = (from h in DestroyableSingleton<HatManager>.Instance.AllSkins
	//			where h.ProdId == mapItem.ProdId
	//			select h).Cast<IBuyable>().ToArray<IBuyable>();
	//			if (mapItem.IncludeHats)
	//			{
	//				bundleSkins = bundleSkins.Concat(from h in DestroyableSingleton<HatManager>.Instance.AllHats
	//				where h.ProdId == mapItem.ProdId && !bundleSkins.Contains(h.RelatedSkin)
	//				select h).ToArray<IBuyable>();
	//			}
	//			this.InsertProduct(position, product, mapItem);
	//			position.y += -1.05f;
	//			position = this.InsertHatsFromList(position, allProducts, bundleSkins);
	//			position.y += -0.1875f;
	//			if (i > 0)
	//			{
	//				position.y += -0.375f;
	//			}
	//		}
	//	}
	//	return position;
	//}

	// Token: 0x06000762 RID: 1890 RVA: 0x0002EC94 File Offset: 0x0002CE94
	//private Vector3 InsertHatsFromList(Vector3 position, Product[] allProducts, IBuyable[] hats)
	//{
	//	int num = 0;
	//	for (int i = 0; i < hats.Length; i++)
	//	{
	//		IBuyable item = hats[i];
	//		Product product = allProducts.FirstOrDefault((Product p) => item.ProdId == p.definition.id);
	//		if (product != null && product.definition != null && product.availableToPurchase)
	//		{
	//			int num2 = num % this.NumPerRow;
	//			position.x = this.XRange.Lerp((float)num2 / ((float)this.NumPerRow - 1f));
	//			if (num2 == 0 && num > 1)
	//			{
	//				position.y += -0.75f;
	//			}
	//			this.InsertProduct(position, product, item);
	//			num++;
	//		}
	//	}
	//	position.y += -0.75f;
	//	return position;
	//}

	// Token: 0x06000763 RID: 1891 RVA: 0x0002ED58 File Offset: 0x0002CF58
	//private PurchaseButton InsertProduct(Vector3 position, Product product, IBuyable item)
	//{
	//	PurchaseButton purchaseButton = Object.Instantiate<PurchaseButton>(this.PurchasablePrefab, this.Scroller.Inner);
	//	this.AllObjects.Add(purchaseButton.gameObject);
	//	purchaseButton.transform.localPosition = position;
	//	purchaseButton.Parent = this;
	//	PurchaseButton purchaseButton2 = purchaseButton;
	//	string id = product.definition.id;
	//	ProductMetadata metadata = product.metadata;
	//	string name;
	//	if (metadata == null)
	//	{
	//		name = null;
	//	}
	//	else
	//	{
	//		string localizedTitle = metadata.localizedTitle;
	//		name = ((localizedTitle != null) ? localizedTitle.Replace("(Among Us)", "") : null);
	//	}
	//	ProductMetadata metadata2 = product.metadata;
	//	purchaseButton2.SetItem(item, id, name, (metadata2 != null) ? metadata2.localizedPriceString : null, product.hasReceipt || SaveManager.GetPurchase(product.definition.id));
	//	UiElement component = purchaseButton.GetComponent<UiElement>();
	//	if (component)
	//	{
	//		if (!this.controllerNavMenu.DefaultButtonSelected)
	//		{
	//			this.controllerNavMenu.DefaultButtonSelected = component;
	//		}
	//		this.controllerNavMenu.ControllerSelectable.Add(component);
	//	}
	//	return purchaseButton;
	//}

	// Token: 0x06000764 RID: 1892 RVA: 0x0002EE48 File Offset: 0x0002D048
	private Vector3 InsertBanner(Vector3 position, Sprite s)
	{
        //position.x = this.XRange.Lerp(0.5f);
        //SpriteRenderer spriteRenderer = Object.Instantiate<SpriteRenderer>(this.BannerPrefab, this.Scroller.Inner);
        //spriteRenderer.sprite = s;
        //spriteRenderer.transform.localPosition = position;
        //position.y += -spriteRenderer.sprite.bounds.size.y;
        //this.AllObjects.Add(spriteRenderer.gameObject);
        return position;
    }

	// Token: 0x06000765 RID: 1893 RVA: 0x0002EECC File Offset: 0x0002D0CC
	//public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	//{
	//	this.controller = controller;
	//	this.extensions = extensions;
	//	if (this.controller == null || this.controller.products == null)
	//	{
	//		this.LoadingText.Text = "Product controller\r\nfailed to load";
	//		return;
	//	}
	//	this.ShowAllButtons();
	//}

	// Token: 0x06000766 RID: 1894 RVA: 0x0002EF08 File Offset: 0x0002D108
	//public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	//{
	//	Debug.Log("Completed Purchase: " + e.purchasedProduct.definition.id);
	//	this.PurchaseState = PurchaseStates.Success;
	//	SaveManager.SetPurchased(e.purchasedProduct.definition.id);
	//	return 0;
	//}

	// Token: 0x06000767 RID: 1895 RVA: 0x0002EF48 File Offset: 0x0002D148
	//public void OnInitializeFailed(InitializationFailureReason error)
	//{
	//	this.RestorePurchasesObj.SetActive(false);
	//	this.LoadingText.gameObject.SetActive(true);
	//	if (error == 1)
	//	{
	//		this.LoadingText.Text = "Coming Soon!";
	//		return;
	//	}
	//	if (error == null)
	//	{
	//		this.LoadingText.Text = "Loading Failed:\r\nAmong Us must be started from itch.io launcher to view products.";
	//		return;
	//	}
	//	this.LoadingText.Text = "Loading Failed:\r\n" + error.ToString();
	//}

	//// Token: 0x06000768 RID: 1896 RVA: 0x0002EFC0 File Offset: 0x0002D1C0
	//public void OnPurchaseFailed(Product i, PurchaseFailureReason error)
	//{
	//	if (error == 4)
	//	{
	//		this.PurchaseState = PurchaseStates.NotStarted;
	//		return;
	//	}
	//	if (error == 2)
	//	{
	//		this.DestroySliderObjects();
	//		this.LoadingText.gameObject.SetActive(true);
	//		this.LoadingText.Text = "Coming Soon!";
	//	}
	//	else if (error == null)
	//	{
	//		this.DestroySliderObjects();
	//		this.LoadingText.gameObject.SetActive(true);
	//		this.LoadingText.Text = "Steam overlay is required for in-game purchasing. You can still buy and install DLC in Steam.";
	//	}
	//	else
	//	{
	//		this.DestroySliderObjects();
	//		this.LoadingText.gameObject.SetActive(true);
	//		this.LoadingText.Text = "Purchase Failed:\r\n" + error.ToString();
	//	}
	//	Debug.LogError("Failed: " + error.ToString());
	//	this.PurchaseState = PurchaseStates.Fail;
	//}

	// Token: 0x04000843 RID: 2115
	public HatParent HatSlot;

	// Token: 0x04000844 RID: 2116
	public SpriteRenderer SkinSlot;

	// Token: 0x04000845 RID: 2117
	public SpriteAnim PetSlot;

	// Token: 0x04000846 RID: 2118
	public TextRenderer ItemName;

	// Token: 0x04000847 RID: 2119
	public SpriteRenderer PurchaseBackground;

	// Token: 0x04000848 RID: 2120
	public TextRenderer PriceText;

	// Token: 0x04000849 RID: 2121
	public PurchaseButton PurchasablePrefab;

	// Token: 0x0400084A RID: 2122
	public SpriteRenderer HortLinePrefab;

	// Token: 0x0400084B RID: 2123
	public TextRenderer LoadingText;

	// Token: 0x0400084C RID: 2124
	public TextRenderer RestorePurchasesButton;

	// Token: 0x0400084D RID: 2125
	public GameObject RestorePurchasesObj;

	// Token: 0x0400084E RID: 2126
	public SpriteRenderer BannerPrefab;

	// Token: 0x0400084F RID: 2127
	public Sprite HolidayBanner;

	// Token: 0x04000850 RID: 2128
	public SpriteRenderer TopArrow;

	// Token: 0x04000851 RID: 2129
	public SpriteRenderer BottomArrow;

	// Token: 0x04000852 RID: 2130
	public const string BoughtAdsProductId = "bought_ads";

	// Token: 0x04000853 RID: 2131
	//private IStoreController controller;

	// Token: 0x04000854 RID: 2132
	//private IExtensionProvider extensions;

	// Token: 0x04000856 RID: 2134
	public Scroller Scroller;

	// Token: 0x04000857 RID: 2135
	public float ItemListStartY = 2f;

	// Token: 0x04000858 RID: 2136
	public FloatRange XRange = new FloatRange(-1f, 1f);

	// Token: 0x04000859 RID: 2137
	public int NumPerRow = 4;

	// Token: 0x0400085A RID: 2138
	private PurchaseButton CurrentButton;

	// Token: 0x0400085B RID: 2139
	private List<GameObject> AllObjects = new List<GameObject>();

	// Token: 0x0400085C RID: 2140
	private ControllerNavMenu controllerNavMenu;

	// Token: 0x0400085D RID: 2141
	private const float NormalHeight = -0.45f;

	// Token: 0x0400085E RID: 2142
	private const float BoxHeight = -0.75f;
}
