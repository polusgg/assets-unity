using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Microsoft.Xbox
{
	// Token: 0x020002A8 RID: 680
	public class XboxSdk : MonoBehaviour
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x000634D8 File Offset: 0x000616D8
		public static XboxSdk Helpers
		{
			get
			{
				if (XboxSdk._xboxHelpers == null)
				{
					XboxSdk[] array = Object.FindObjectsOfType<XboxSdk>();
					if (array.Length != 0)
					{
						XboxSdk._xboxHelpers = array[0];
						XboxSdk._xboxHelpers._Initialize();
					}
					else
					{
						XboxSdk._LogError("Error: Could not find Xbox prefab. Make sure you have added the Xbox prefab to your scene.");
					}
				}
				return XboxSdk._xboxHelpers;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001304 RID: 4868 RVA: 0x00063520 File Offset: 0x00061720
		// (remove) Token: 0x06001305 RID: 4869 RVA: 0x00063558 File Offset: 0x00061758
		public event XboxSdk.OnGameSaveLoadedHandler OnGameSaveLoaded;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001306 RID: 4870 RVA: 0x00063590 File Offset: 0x00061790
		// (remove) Token: 0x06001307 RID: 4871 RVA: 0x000635C8 File Offset: 0x000617C8
		public event XboxSdk.OnErrorHandler OnError;

		// Token: 0x06001308 RID: 4872 RVA: 0x000635FD File Offset: 0x000617FD
		private void Start()
		{
			this._Initialize();
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00063605 File Offset: 0x00061805
		private void _Initialize()
		{
			if (XboxSdk._initialized)
			{
				return;
			}
			XboxSdk._initialized = true;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00063620 File Offset: 0x00061820
		public void SignIn()
		{
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00063622 File Offset: 0x00061822
		public void Save(byte[] data)
		{
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00063624 File Offset: 0x00061824
		public void LoadSaveData()
		{
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00063626 File Offset: 0x00061826
		public void UnlockAchievement(string achievementId)
		{
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00063628 File Offset: 0x00061828
		private void Update()
		{
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0006362C File Offset: 0x0006182C
		protected static bool Succeeded(int hresult, string operationFriendlyName)
		{
			bool result = false;
			if (HR.SUCCEEDED(hresult))
			{
				result = true;
			}
			else
			{
				string text = hresult.ToString("X8");
				string text2 = operationFriendlyName + " failed.";
				XboxSdk._LogError(string.Format("{0} Error code: hr=0x{1}", text2, text));
				if (XboxSdk.Helpers.OnError != null)
				{
					XboxSdk.Helpers.OnError(XboxSdk.Helpers, new ErrorEventArgs(text, text2));
				}
			}
			return result;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00063699 File Offset: 0x00061899
		private static void _LogError(string message)
		{
			Debug.Log(message);
		}

		// Token: 0x040015A1 RID: 5537
		public Text dlcOutputTextBox;

		// Token: 0x040015A2 RID: 5538
		[Header("You can find the value of the scid in your MicrosoftGame.config")]
		public string scid;

		// Token: 0x040015A3 RID: 5539
		public Text gamertagLabel;

		// Token: 0x040015A4 RID: 5540
		public bool signInOnStart = true;

		// Token: 0x040015A5 RID: 5541
		private static XboxSdk _xboxHelpers;

		// Token: 0x040015A6 RID: 5542
		private static bool _initialized;

		// Token: 0x040015A7 RID: 5543
		private const string _GAME_SAVE_CONTAINER_NAME = "x_game_save_default_container";

		// Token: 0x040015A8 RID: 5544
		private const string _GAME_SAVE_BLOB_NAME = "x_game_save_default_blob";

		// Token: 0x020004B0 RID: 1200
		// (Invoke) Token: 0x06001C29 RID: 7209
		public delegate void OnGameSaveLoadedHandler(object sender, GameSaveLoadedArgs e);

		// Token: 0x020004B1 RID: 1201
		// (Invoke) Token: 0x06001C2D RID: 7213
		public delegate void OnErrorHandler(object sender, ErrorEventArgs e);
	}
}
