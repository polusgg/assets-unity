using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerNet;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x020000C1 RID: 193
public class HudManager : DestroyableSingleton<HudManager>
{
	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001D6DB File Offset: 0x0001B8DB
	// (set) Token: 0x06000493 RID: 1171 RVA: 0x0001D6E3 File Offset: 0x0001B8E3
	public Coroutine ReactorFlash { get; set; }

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001D6EC File Offset: 0x0001B8EC
	// (set) Token: 0x06000495 RID: 1173 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
	public Coroutine OxyFlash { get; set; }

	// Token: 0x06000496 RID: 1174 RVA: 0x0001D6FD File Offset: 0x0001B8FD
	public void Start()
	{
		if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
		{
			this.SetTouchType(this.Joysticks.Length - 2);
			return;
		}
		this.SetTouchType(SaveManager.ControlMode);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0001D722 File Offset: 0x0001B922
	public void ShowTaskComplete()
	{
		base.StartCoroutine(this.CoTaskComplete());
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0001D731 File Offset: 0x0001B931
	private IEnumerator CoTaskComplete()
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.TaskCompleteSound, false, 1f);
		}
		this.TaskCompleteOverlay.gameObject.SetActive(true);
		yield return Effects.Slide2D(this.TaskCompleteOverlay, new Vector2(0f, -8f), Vector2.zero, 0.25f);
		for (float time = 0f; time < 0.75f; time += Time.deltaTime)
		{
			yield return null;
		}
		yield return Effects.Slide2D(this.TaskCompleteOverlay, Vector2.zero, new Vector2(0f, 8f), 0.25f);
		this.TaskCompleteOverlay.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0001D740 File Offset: 0x0001B940
	public void SetJoystickSize(float size)
	{
		if (this.joystick != null && this.joystick is VirtualJoystick)
		{
			VirtualJoystick virtualJoystick = (VirtualJoystick)this.joystick;
			virtualJoystick.transform.localScale = new Vector3(size, size, 1f);
			AspectPosition component = virtualJoystick.GetComponent<AspectPosition>();
			float num = Mathf.Lerp(0.65f, 1.1f, FloatRange.ReverseLerp(size, 0.5f, 1.5f));
			component.DistanceFromEdge = new Vector3(num, num, -10f);
			component.AdjustPosition();
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
	public void SetTouchType(int type)
	{
		if (this.joystick != null && !(this.joystick is KeyboardJoystick))
		{
			Object.Destroy((this.joystick as MonoBehaviour).gameObject);
		}
		MonoBehaviour monoBehaviour = Object.Instantiate<MonoBehaviour>(this.Joysticks[Mathf.Clamp(type + 1, 1, this.Joysticks.Length)]);
		monoBehaviour.transform.SetParent(base.transform, false);
		this.joystick = monoBehaviour.GetComponent<IVirtualJoystick>();
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0001D834 File Offset: 0x0001BA34
	public void OpenMap()
	{
		GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
		if (data == null)
		{
			return;
		}
		if (data.IsImpostor)
		{
			this.ShowMap(delegate(MapBehaviour m)
			{
				m.ShowInfectedMap();
			});
			return;
		}
		this.ShowMap(delegate(MapBehaviour m)
		{
			m.ShowNormalMap();
		});
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
	public void ShowMap(Action<MapBehaviour> mapAction)
	{
		if (this.isIntroDisplayed)
		{
			return;
		}
		if (!ShipStatus.Instance)
		{
			return;
		}
		if (!MapBehaviour.Instance)
		{
			MapBehaviour.Instance = Object.Instantiate<MapBehaviour>(ShipStatus.Instance.MapPrefab, base.transform);
			MapBehaviour.Instance.gameObject.SetActive(false);
		}
		mapAction(MapBehaviour.Instance);
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0001D908 File Offset: 0x0001BB08
	public void SetHudActive(bool isActive)
	{
		DestroyableSingleton<HudManager>.Instance.UseButton.gameObject.SetActive(isActive);
		DestroyableSingleton<HudManager>.Instance.UseButton.Refresh();
		DestroyableSingleton<HudManager>.Instance.ReportButton.gameObject.SetActive(isActive);
		PlayerControl localPlayer = PlayerControl.LocalPlayer;
		GameData.PlayerInfo playerInfo = (localPlayer != null) ? localPlayer.Data : null;
		DestroyableSingleton<HudManager>.Instance.KillButton.gameObject.SetActive(isActive && playerInfo.IsImpostor && !playerInfo.IsDead);
		DestroyableSingleton<HudManager>.Instance.TaskText.transform.parent.gameObject.SetActive(isActive);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0001D9AC File Offset: 0x0001BBAC
	public void Update()
	{
		this.taskDirtyTimer += Time.deltaTime;
		if (this.taskDirtyTimer > 0.25f)
		{
			this.taskDirtyTimer = 0f;
			if (!PlayerControl.LocalPlayer)
			{
				this.TaskText.Text = string.Empty;
				return;
			}
			GameData.PlayerInfo data = PlayerControl.LocalPlayer.Data;
			if (data == null)
			{
				return;
			}
			bool isImpostor = data.IsImpostor;
			this.tasksString.Clear();
			if (PlayerControl.LocalPlayer.myTasks.Count == 0)
			{
				this.tasksString.Append("None");
			}
			else
			{
				for (int i = 0; i < PlayerControl.LocalPlayer.myTasks.Count; i++)
				{
					PlayerTask playerTask = PlayerControl.LocalPlayer.myTasks[i];
					if (playerTask)
					{
						if (playerTask.TaskType == TaskTypes.FixComms && !isImpostor)
						{
							this.tasksString.Clear();
							playerTask.AppendTaskText(this.tasksString);
							break;
						}
						playerTask.AppendTaskText(this.tasksString);
					}
				}
				this.tasksString.TrimEnd();
			}
			this.TaskText.Text = this.tasksString.ToString();
		}
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
	public IEnumerator ShowEmblem(bool shhh)
	{
		if (shhh)
		{
			this.shhhEmblem.gameObject.SetActive(true);
			yield return this.shhhEmblem.PlayAnimation();
			this.shhhEmblem.gameObject.SetActive(false);
		}
		else
		{
			this.discussEmblem.gameObject.SetActive(true);
			yield return this.discussEmblem.PlayAnimation();
			this.discussEmblem.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0001DAE6 File Offset: 0x0001BCE6
	public void StartReactorFlash()
	{
		if (this.ReactorFlash == null)
		{
			this.ReactorFlash = base.StartCoroutine(this.CoReactorFlash());
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0001DB02 File Offset: 0x0001BD02
	public void StartOxyFlash()
	{
		if (this.OxyFlash == null)
		{
			this.OxyFlash = base.StartCoroutine(this.CoReactorFlash());
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0001DB1E File Offset: 0x0001BD1E
	public void ShowPopUp(string text)
	{
		this.Dialogue.Show(text);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0001DB2C File Offset: 0x0001BD2C
	public void StopReactorFlash()
	{
		if (this.ReactorFlash != null)
		{
			base.StopCoroutine(this.ReactorFlash);
			this.ReactorFlash = null;
			this.FullScreen.enabled = false;
			PlainShipRoom plainShipRoom = ShipStatus.Instance.AllRooms.FirstOrDefault((PlainShipRoom r) => r is ReactorShipRoom);
			if (plainShipRoom)
			{
				((ReactorShipRoom)plainShipRoom).StopMeltdown();
			}
			if (this.lightFlashHandle != null)
			{
				this.lightFlashHandle.Dispose();
				this.lightFlashHandle = null;
			}
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0001DBBC File Offset: 0x0001BDBC
	public void StopOxyFlash()
	{
		if (this.OxyFlash != null)
		{
			base.StopCoroutine(this.OxyFlash);
			this.FullScreen.enabled = false;
			this.OxyFlash = null;
			if (this.lightFlashHandle != null)
			{
				this.lightFlashHandle.Dispose();
				this.lightFlashHandle = null;
			}
		}
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0001DC0A File Offset: 0x0001BE0A
	public IEnumerator CoFadeFullScreen(Color source, Color target, float duration = 0.2f)
	{
		if (this.FullScreen.enabled && this.FullScreen.color == target)
		{
			yield break;
		}
		this.FullScreen.enabled = true;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			if (!this.FullScreen)
			{
				yield break;
			}
			this.FullScreen.color = Color.Lerp(source, target, t / duration);
			yield return null;
		}
		if (this.FullScreen)
		{
			this.FullScreen.color = target;
			if (target.a < 0.05f)
			{
				this.FullScreen.enabled = false;
			}
		}
		yield break;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0001DC2E File Offset: 0x0001BE2E
	private IEnumerator CoReactorFlash()
	{
		WaitForSeconds wait = new WaitForSeconds(1f);
		this.FullScreen.color = new Color(1f, 0f, 0f, 0.37254903f);
		for (;;)
		{
			this.FullScreen.enabled = !this.FullScreen.enabled;
			SoundManager.Instance.PlaySound(this.SabotageSound, false, 1f);
			if (this.lightFlashHandle == null)
			{
				this.lightFlashHandle = DestroyableSingleton<DualshockLightManager>.Instance.AllocateLight();
				this.lightFlashHandle.color = new Color(1f, 0f, 0f, 1f);
				this.lightFlashHandle.intensity = 1f;
			}
			this.lightFlashHandle.color.a = (this.FullScreen.enabled ? 1f : 0f);
			yield return wait;
		}
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0001DC3D File Offset: 0x0001BE3D
	public IEnumerator CoShowIntro(List<PlayerControl> yourTeam)
	{
		while (!ShipStatus.Instance)
		{
			yield return null;
		}
		this.isIntroDisplayed = true;
		DestroyableSingleton<HudManager>.Instance.FullScreen.transform.localPosition = new Vector3(0f, 0f, -250f);
		yield return DestroyableSingleton<HudManager>.Instance.ShowEmblem(true);
		IntroCutscene introCutscene = Object.Instantiate<IntroCutscene>(this.IntroPrefab, base.transform);
		yield return introCutscene.CoBegin(yourTeam, PlayerControl.LocalPlayer.Data.IsImpostor);
		PlayerControl.LocalPlayer.SetKillTimer(10f);
		((SabotageSystemType)ShipStatus.Instance.Systems[SystemTypes.Sabotage]).ForceSabTime(10f);
		yield return ShipStatus.Instance.PrespawnStep();
		yield return this.CoFadeFullScreen(Color.black, Color.clear, 0.2f);
		DestroyableSingleton<HudManager>.Instance.FullScreen.transform.localPosition = new Vector3(0f, 0f, -500f);
		this.isIntroDisplayed = false;
		yield break;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0001DC54 File Offset: 0x0001BE54
	public void OpenMeetingRoom(PlayerControl reporter)
	{
		if (MeetingHud.Instance)
		{
			return;
		}
		Debug.Log("Opening meeting room: " + ((reporter != null) ? reporter.ToString() : null));
		ShipStatus.Instance.RepairGameOverSystems();
		MeetingHud.Instance = Object.Instantiate<MeetingHud>(this.MeetingPrefab);
		MeetingHud.Instance.ServerStart(reporter.PlayerId);
		AmongUsClient.Instance.Spawn(MeetingHud.Instance, -2, SpawnFlags.None);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0001DCC6 File Offset: 0x0001BEC6
	public override void OnDestroy()
	{
		base.OnDestroy();
		if (this.lightFlashHandle != null)
		{
			this.lightFlashHandle.Dispose();
			this.lightFlashHandle = null;
		}
	}

	// Token: 0x0400055C RID: 1372
	public FollowerCamera PlayerCam;

	// Token: 0x0400055D RID: 1373
	public MeetingHud MeetingPrefab;

	// Token: 0x0400055E RID: 1374
	public KillButtonManager KillButton;

	// Token: 0x0400055F RID: 1375
	public UseButtonManager UseButton;

	// Token: 0x04000560 RID: 1376
	public ReportButtonManager ReportButton;

	// Token: 0x04000561 RID: 1377
	public TextRenderer GameSettings;

	// Token: 0x04000562 RID: 1378
	public GameObject TaskStuff;

	// Token: 0x04000563 RID: 1379
	public ChatController Chat;

	// Token: 0x04000564 RID: 1380
	public DialogueBox Dialogue;

	// Token: 0x04000565 RID: 1381
	public TextRenderer TaskText;

	// Token: 0x04000566 RID: 1382
	public Transform TaskCompleteOverlay;

	// Token: 0x04000567 RID: 1383
	private float taskDirtyTimer;

	// Token: 0x04000568 RID: 1384
	public MeshRenderer ShadowQuad;

	// Token: 0x04000569 RID: 1385
	public SpriteRenderer FullScreen;

	// Token: 0x0400056C RID: 1388
	public SpriteRenderer MapButton;

	// Token: 0x0400056D RID: 1389
	public KillOverlay KillOverlay;

	// Token: 0x0400056E RID: 1390
	public IVirtualJoystick joystick;

	// Token: 0x0400056F RID: 1391
	public MonoBehaviour[] Joysticks;

	// Token: 0x04000570 RID: 1392
	public DiscussBehaviour discussEmblem;

	// Token: 0x04000571 RID: 1393
	public ShhhBehaviour shhhEmblem;

	// Token: 0x04000572 RID: 1394
	public IntroCutscene IntroPrefab;

	// Token: 0x04000573 RID: 1395
	public OptionsMenuBehaviour GameMenu;

	// Token: 0x04000574 RID: 1396
	public NotificationPopper Notifier;

	// Token: 0x04000575 RID: 1397
	public RoomTracker roomTracker;

	// Token: 0x04000576 RID: 1398
	public AudioClip SabotageSound;

	// Token: 0x04000577 RID: 1399
	public AudioClip TaskCompleteSound;

	// Token: 0x04000578 RID: 1400
	public AudioClip TaskUpdateSound;

	// Token: 0x04000579 RID: 1401
	public GameObject[] consoleUIObjects;

	// Token: 0x0400057A RID: 1402
	private StringBuilder tasksString = new StringBuilder();

	// Token: 0x0400057B RID: 1403
	private bool isIntroDisplayed;

	// Token: 0x0400057C RID: 1404
	private DualshockLightManager.LightOverlayHandle lightFlashHandle;
}
