using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class RecordsMinigame : Minigame
{
	// Token: 0x06000131 RID: 305 RVA: 0x0000885C File Offset: 0x00006A5C
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		this.FoldersContent.SetActive(false);
		this.ShelfContent.SetActive(false);
		this.DrawerContent.SetActive(false);
		base.SetupInput(true);
		if (base.ConsoleId == 0)
		{
			this.FoldersContent.SetActive(true);
			for (int i = 0; i < this.Folders.Length; i++)
			{
				if (this.MyNormTask.Data[i] != 0)
				{
					this.Folders[i].gameObject.SetActive(false);
				}
			}
			return;
		}
		if (base.ConsoleId <= 4)
		{
			this.DrawerContent.SetActive(true);
			return;
		}
		if (base.ConsoleId <= 8)
		{
			this.ShelfContent.SetActive(true);
			Sprite sprite = this.BookCovers.Random<Sprite>();
			SpriteRenderer[] books = this.Books;
			for (int j = 0; j < books.Length; j++)
			{
				books[j].sprite = sprite;
			}
			this.targetBook = this.Books.Random<SpriteRenderer>();
			this.targetBook.enabled = false;
			this.targetBook.GetComponent<PassiveButton>().enabled = true;
			ControllerButtonBehavior component = this.targetBook.GetComponent<ControllerButtonBehavior>();
			component.enabled = true;
			Vector3 localPosition = this.bookInputPrompt.transform.localPosition;
			localPosition.x = this.targetBook.transform.localPosition.x;
			this.bookInputPrompt.transform.localPosition = localPosition;
			this.bookInputPrompt.GetComponentInChildren<ActionMapGlyphDisplay>(true).actionToDisplayMappedGlyphFor = component.Action;
			this.bookInputPrompt.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x000089EC File Offset: 0x00006BEC
	protected override IEnumerator CoAnimateOpen()
	{
		if (base.ConsoleId == 0)
		{
			this.TransType = TransitionType.SlideBottom;
		}
		else if (base.ConsoleId <= 4)
		{
			yield return this.CoOpenDrawer();
		}
		else if (base.ConsoleId <= 8)
		{
			this.TransType = TransitionType.SlideBottom;
		}
		yield return base.CoAnimateOpen();
		yield break;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x000089FB File Offset: 0x00006BFB
	protected override IEnumerator CoDestroySelf()
	{
		if (base.ConsoleId > 0 && base.ConsoleId <= 4)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.drawerClose, false, 1f);
			}
			yield return Effects.Slide2D(this.Drawer, Vector2.zero, new Vector2(0f, 7f), 0.3f);
		}
		yield return base.CoDestroySelf();
		yield break;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00008A0C File Offset: 0x00006C0C
	public void PlaceBook()
	{
		for (int i = 0; i < this.MyNormTask.Data.Length; i++)
		{
			if (this.MyNormTask.Data[i] != 0)
			{
				this.MyNormTask.Data[i] = byte.MaxValue;
			}
		}
		this.MyNormTask.NextStep();
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.recordBookPlace, false, 1f);
		}
		base.StartCoroutine(this.CoSlideBook());
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00008A88 File Offset: 0x00006C88
	private IEnumerator CoSlideBook()
	{
		this.targetBook.GetComponent<PassiveButton>().enabled = false;
		this.targetBook.enabled = true;
		yield return Effects.ColorFade(this.targetBook, Palette.ClearWhite, Color.white, 0.4f);
		yield return base.CoStartClose(0.35f);
		yield break;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00008A98 File Offset: 0x00006C98
	public void FileDocument()
	{
		for (int i = 0; i < this.MyNormTask.Data.Length; i++)
		{
			if (this.MyNormTask.Data[i] != 0)
			{
				this.MyNormTask.Data[i] = byte.MaxValue;
			}
		}
		this.MyNormTask.NextStep();
		this.slideFolderHotkey.enabled = false;
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.recordFilePlace, false, 1f);
		}
		base.StartCoroutine(this.CoSlideFolder());
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00008B20 File Offset: 0x00006D20
	private IEnumerator CoOpenDrawer()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.drawerOpen, false, 1f);
		}
		yield return Effects.Slide2D(this.Drawer, new Vector2(0f, 7f), Vector2.zero, 0.5f);
		yield break;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00008B2F File Offset: 0x00006D2F
	private IEnumerator CoSlideFolder()
	{
		this.DrawerFolder.gameObject.SetActive(true);
		yield return Effects.Slide2D(this.DrawerFolder.transform, new Vector2(0f, 5f), Vector2.zero, 0.4f);
		yield return base.CoStartClose(0.35f);
		yield break;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00008B40 File Offset: 0x00006D40
	public void GrabFolder(SpriteRenderer folder)
	{
		if (this.amClosing != Minigame.CloseState.None)
		{
			return;
		}
		int num = this.Folders.IndexOf(folder);
		folder.gameObject.SetActive(false);
		this.MyNormTask.Data[num] = IntRange.NextByte(1, 9);
		this.MyNormTask.UpdateArrow();
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.grabDocument, false, 1f);
		}
		base.StartCoroutine(base.CoStartClose(0.75f));
	}

	// Token: 0x04000173 RID: 371
	public GameObject FoldersContent;

	// Token: 0x04000174 RID: 372
	public SpriteRenderer[] Folders;

	// Token: 0x04000175 RID: 373
	public GameObject DrawerContent;

	// Token: 0x04000176 RID: 374
	public Transform Drawer;

	// Token: 0x04000177 RID: 375
	public SpriteRenderer DrawerFolder;

	// Token: 0x04000178 RID: 376
	public GameObject ShelfContent;

	// Token: 0x04000179 RID: 377
	public SpriteRenderer[] Books;

	// Token: 0x0400017A RID: 378
	public Sprite[] BookCovers;

	// Token: 0x0400017B RID: 379
	private SpriteRenderer targetBook;

	// Token: 0x0400017C RID: 380
	public AudioClip recordFilePlace;

	// Token: 0x0400017D RID: 381
	public AudioClip recordBookPlace;

	// Token: 0x0400017E RID: 382
	public AudioClip grabDocument;

	// Token: 0x0400017F RID: 383
	public AudioClip drawerOpen;

	// Token: 0x04000180 RID: 384
	public AudioClip drawerClose;

	// Token: 0x04000181 RID: 385
	public Transform bookInputPrompt;

	// Token: 0x04000182 RID: 386
	public ControllerButtonBehaviourComplex slideFolderHotkey;
}
