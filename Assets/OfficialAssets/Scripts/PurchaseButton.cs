using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class PurchaseButton : MonoBehaviour
{
	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600072F RID: 1839 RVA: 0x0002D898 File Offset: 0x0002BA98
	// (set) Token: 0x06000730 RID: 1840 RVA: 0x0002D8A0 File Offset: 0x0002BAA0
	public StoreMenu Parent { get; set; }

	// Token: 0x06000731 RID: 1841 RVA: 0x0002D8AC File Offset: 0x0002BAAC
	public void SetItem(IBuyable product, string productId, string name, string price, bool purchased)
	{
		this.Product = product;
		this.Purchased = purchased;
		this.Name = name;
		this.Price = price;
		this.ProductId = productId;
		base.name = productId;
		this.PurchasedIcon.enabled = this.Purchased;
		if (this.Product is HatBehaviour)
		{
			HatBehaviour hat = (HatBehaviour)this.Product;
			this.NameText.gameObject.SetActive(false);
			this.HatImage.SetHat(hat, 0);
			this.SetSquare();
			return;
		}
		if (this.Product is SkinData)
		{
			SkinData skin = (SkinData)this.Product;
			this.NameText.gameObject.SetActive(false);
			this.CrewHeadImage.sprite = this.MannequinFrame;
			this.CrewHeadImage.transform.localPosition = new Vector3(0f, 0f, -0.01f);
			this.CrewHeadImage.transform.localScale = Vector3.one * 0.3f;
			this.HatImage.FrontLayer.transform.localPosition = new Vector3(0f, 0f, -0.01f);
			this.HatImage.FrontLayer.transform.localScale = Vector3.one * 2f;
			PlayerControl.SetSkinImage(skin, this.HatImage.FrontLayer);
			this.SetSquare();
			return;
		}
		if (this.Product is PetBehaviour)
		{
			PetBehaviour petBehaviour = (PetBehaviour)this.Product;
			this.NameText.gameObject.SetActive(false);
			this.CrewHeadImage.enabled = false;
			this.HatImage.FrontLayer.material = new Material(petBehaviour.rend.sharedMaterial);
			PlayerControl.SetPetImage(petBehaviour, (int)SaveManager.BodyColor, this.HatImage.FrontLayer);
			this.SetSquare();
			return;
		}
		if (this.Product is MapBuyable)
		{
			MapBuyable mapBuyable = (MapBuyable)this.Product;
			this.NameText.Text = "";
			this.NameText.Centered = false;
			this.NameText.scaleToFit = true;
			this.NameText.maxWidth = 2.8f;
			this.NameText.transform.localPosition = new Vector3(-1.4f, -1.5f, -0.01f);
			this.NameText.Color = Color.black;
			this.NameText.OutlineColor = Color.clear;
			this.HatImage.FrontLayer.sprite = mapBuyable.StoreImage;
			this.CrewHeadImage.enabled = false;
			this.SetBig();
			this.Background.enabled = false;
			return;
		}
		this.NameText.Text = this.Name;
		this.HatImage.gameObject.SetActive(false);
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0002DB7C File Offset: 0x0002BD7C
	private void SetBig()
	{
		this.Background.size = new Vector2(2.8f, 1.4f);
		this.Background.GetComponent<BoxCollider2D>().size = new Vector2(2.8f, 1.4f);
		this.SelectionHighlight.size = new Vector2(2.85f, 1.4499999f);
		this.PurchasedIcon.transform.localPosition = new Vector3(1.1f, -0.45f, -2f);
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0002DC00 File Offset: 0x0002BE00
	private void SetSquare()
	{
		this.Background.size = new Vector2(0.7f, 0.7f);
		this.Background.GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 0.7f);
		this.SelectionHighlight.size = new Vector2(0.75f, 0.75f);
		this.PurchasedIcon.transform.localPosition = new Vector3(0f, 0f, -1f);
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0002DC84 File Offset: 0x0002BE84
	internal void SetPurchased()
	{
		this.Purchased = true;
		this.PurchasedIcon.enabled = true;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0002DC99 File Offset: 0x0002BE99
	public void DoPurchase()
	{
		this.Parent.SetProduct(this);
	}

	// Token: 0x0400081D RID: 2077
	private const float BorderSize = 0.7f;

	// Token: 0x0400081E RID: 2078
	private const float BorderSelectHighlightOffset = 0.05f;

	// Token: 0x04000820 RID: 2080
	public SpriteRenderer PurchasedIcon;

	// Token: 0x04000821 RID: 2081
	public TextRenderer NameText;

	// Token: 0x04000822 RID: 2082
	public SpriteRenderer CrewHeadImage;

	// Token: 0x04000823 RID: 2083
	public HatParent HatImage;

	// Token: 0x04000824 RID: 2084
	public Sprite MannequinFrame;

	// Token: 0x04000825 RID: 2085
	public SpriteRenderer Background;

	// Token: 0x04000826 RID: 2086
	public SpriteRenderer SelectionHighlight;

	// Token: 0x04000827 RID: 2087
	public IBuyable Product;

	// Token: 0x04000828 RID: 2088
	public bool Purchased;

	// Token: 0x04000829 RID: 2089
	public string Name;

	// Token: 0x0400082A RID: 2090
	public string Price;

	// Token: 0x0400082B RID: 2091
	public string ProductId;
}
