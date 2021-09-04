using System;
using System.Collections.Generic;
using InnerNet;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

// Token: 0x02000112 RID: 274
public class FindAGameManager : DestroyableSingleton<FindAGameManager>, IGameListHandler
{
	// Token: 0x060006C5 RID: 1733 RVA: 0x0002B2CD File Offset: 0x000294CD
	public void ResetTimer()
	{
		this.timer = 5f;
		this.RefreshSpinner.Appear();
		this.RefreshSpinner.StartPulse();
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0002B2F0 File Offset: 0x000294F0
	public void Start()
	{
		if (!AmongUsClient.Instance)
		{
			AmongUsClient.Instance = Object.FindObjectOfType<AmongUsClient>();
			if (!AmongUsClient.Instance)
			{
				SceneManager.LoadScene("MMOnline");
				return;
			}
		}
		AmongUsClient.Instance.GameListHandlers.Add(this);
		AmongUsClient.Instance.RequestGameList(false, SaveManager.GameSearchOptions);
		ControllerManager.Instance.NewScene(base.name, this.BackButton, this.DefaultButtonSelected, this.ControllerSelectable, false);
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0002B370 File Offset: 0x00029570
	public void Update()
	{
		//this.timer += Time.deltaTime;
		//GameOptionsData gameSearchOptions = SaveManager.GameSearchOptions;
		//if ((this.timer < 0f || this.timer > 5f) && gameSearchOptions.MapId != 0)
		//{
		//	this.RefreshSpinner.Appear();
		//}
		//else
		//{
		//	this.RefreshSpinner.Disappear();
		//}
		//if (Input.GetKeyUp(27))
		//{
		//	this.ExitGame();
		//}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0002B3DE File Offset: 0x000295DE
	public void RefreshList()
	{
		if (this.timer > 5f)
		{
			this.timer = -5f;
			this.RefreshSpinner.Play();
			AmongUsClient.Instance.RequestGameList(false, SaveManager.GameSearchOptions);
		}
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0002B413 File Offset: 0x00029613
	public override void OnDestroy()
	{
		if (AmongUsClient.Instance)
		{
			AmongUsClient.Instance.GameListHandlers.Remove(this);
		}
		base.OnDestroy();
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0002B438 File Offset: 0x00029638
	public void HandleList(InnerNetClient.TotalGameData totalGames, List<GameListing> availableGames)
	{
		//if (totalGames.PerMapTotals != null)
		//{
		//	string text = string.Empty;
		//	for (int i = 0; i < AmongUsClient.Instance.ShipPrefabs.Count; i++)
		//	{
		//		text += string.Format("Total {0} Games: {1}        ", GameOptionsData.MapNames[i], totalGames.PerMapTotals[i]);
		//	}
		//	text.TrimEnd(Array.Empty<char>());
		//	this.TotalText.Text = text;
		//}
		//try
		//{
		//	this.RefreshSpinner.Disappear();
		//	this.timer = 0f;
		//	availableGames.Sort(FindAGameManager.GameSorter.Instance);
		//	while (this.buttonPool.activeChildren.Count > availableGames.Count)
		//	{
		//		PoolableBehavior poolableBehavior = this.buttonPool.activeChildren[this.buttonPool.activeChildren.Count - 1];
		//		poolableBehavior.OwnerPool.Reclaim(poolableBehavior);
		//	}
		//	while (this.buttonPool.activeChildren.Count < availableGames.Count)
		//	{
		//		this.buttonPool.Get<PoolableBehavior>().transform.SetParent(this.TargetArea.Inner);
		//	}
		//	Vector3 vector;
		//	vector..ctor(0f, this.ButtonStart, -1f);
		//	for (int j = 0; j < this.buttonPool.activeChildren.Count; j++)
		//	{
		//		MatchMakerGameButton matchMakerGameButton = (MatchMakerGameButton)this.buttonPool.activeChildren[j];
		//		matchMakerGameButton.SetGame(availableGames[j]);
		//		matchMakerGameButton.transform.localPosition = vector;
		//		vector.y -= this.ButtonHeight;
		//		PassiveButton component = matchMakerGameButton.gameObject.GetComponent<PassiveButton>();
		//		ControllerManager.Instance.AddSelectableUiElement(component, false);
		//	}
		//	this.TargetArea.YBounds.max = Mathf.Max(0f, -vector.y - this.ButtonStart);
		//}
		//catch (Exception ex)
		//{
		//	Debug.LogError("FindAGameManager::HandleList: Exception: ");
		//	Debug.LogException(ex, this);
		//}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0002B638 File Offset: 0x00029838
	public void ExitGame()
	{
		AmongUsClient.Instance.ExitGame(DisconnectReasons.ExitGame);
	}

	// Token: 0x0400078D RID: 1933
	private const float RefreshTime = 5f;

	// Token: 0x0400078E RID: 1934
	private float timer;

	// Token: 0x0400078F RID: 1935
	public ObjectPoolBehavior buttonPool;

	// Token: 0x04000790 RID: 1936
	public SpinAnimator RefreshSpinner;

	// Token: 0x04000791 RID: 1937
	public Scroller TargetArea;

	// Token: 0x04000792 RID: 1938
	public TextRenderer TotalText;

	// Token: 0x04000793 RID: 1939
	public float ButtonStart = 1.75f;

	// Token: 0x04000794 RID: 1940
	public float ButtonHeight = 0.6f;

	// Token: 0x04000795 RID: 1941
	public const bool showPrivate = false;

	// Token: 0x04000796 RID: 1942
	[Header("Console Controller Navigation")]
	public UiElement BackButton;

	// Token: 0x04000797 RID: 1943
	public UiElement DefaultButtonSelected;

	// Token: 0x04000798 RID: 1944
	public List<UiElement> ControllerSelectable;

	// Token: 0x020003A6 RID: 934
	private class GameSorter : IComparer<GameListing>
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x000726CD File Offset: 0x000708CD
		public int Compare(GameListing x, GameListing y)
		{
			return -x.PlayerCount.CompareTo(y.PlayerCount);
		}

		// Token: 0x040019FE RID: 6654
		public static readonly FindAGameManager.GameSorter Instance = new FindAGameManager.GameSorter();
	}
}
