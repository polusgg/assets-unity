using System;
using System.Collections.Generic;
//using Rewired;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Token: 0x020001A1 RID: 417
public class VibrationManager : DestroyableSingleton<VibrationManager>
{
	// Token: 0x06000940 RID: 2368 RVA: 0x0003C96E File Offset: 0x0003AB6E
	private void Start()
	{
		this.cam = Camera.main;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0003C97B File Offset: 0x0003AB7B
	private void OnEnable()
	{
		SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0003C98E File Offset: 0x0003AB8E
	private void OnDisable()
	{
		SceneManager.activeSceneChanged -= new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0003C9A1 File Offset: 0x0003ABA1
	private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
	{
		VibrationManager.ClearAllVibration();
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0003C9A8 File Offset: 0x0003ABA8
	public static void ClearAllVibration()
	{
		DestroyableSingleton<VibrationManager>.Instance.currentLocalVibration.Clear();
		DestroyableSingleton<VibrationManager>.Instance.currentWorldVibration.Clear();
		DestroyableSingleton<VibrationManager>.Instance.currentVibration = Vector2.zero;
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0003C9D8 File Offset: 0x0003ABD8
	private void Update()
	{
		//if (!this.cam)
		//{
		//	this.cam = Camera.main;
		//}
		//if (RaycastAmbientSoundPlayer.players.Count > 0)
		//{
		//	Vector2 cameraPos = this.cam.transform.position;
		//	foreach (RaycastAmbientSoundPlayer raycastAmbientSoundPlayer in RaycastAmbientSoundPlayer.players)
		//	{
		//		if (raycastAmbientSoundPlayer.AmbientSound)
		//		{
		//			raycastAmbientSoundPlayer.t += Time.deltaTime;
		//			if (raycastAmbientSoundPlayer.t > raycastAmbientSoundPlayer.AmbientSound.length)
		//			{
		//				raycastAmbientSoundPlayer.t -= raycastAmbientSoundPlayer.AmbientSound.length;
		//			}
		//			if (raycastAmbientSoundPlayer.ambientVolume > 0.01f)
		//			{
		//				this.tempAmbientSoundVibration.clip = raycastAmbientSoundPlayer.AmbientSound;
		//				this.tempAmbientSoundVibration.duration = raycastAmbientSoundPlayer.AmbientSound.length;
		//				this.tempAmbientSoundVibration.intensity = raycastAmbientSoundPlayer.ambientVolume * 0.5f;
		//				this.tempAmbientSoundVibration.location = raycastAmbientSoundPlayer.transform.position;
		//				this.tempAmbientSoundVibration.radius = ((raycastAmbientSoundPlayer.AmbientMaxDist > 0f) ? raycastAmbientSoundPlayer.AmbientMaxDist : 10000f);
		//				this.tempAmbientSoundVibration.t = raycastAmbientSoundPlayer.t;
		//				this.singleFrameVibration += this.tempAmbientSoundVibration.UpdateIntensity(cameraPos, 0f);
		//				this.hasFrameVibration = true;
		//			}
		//		}
		//	}
		//}
		//if (this.currentLocalVibration.Count > 0 || this.currentWorldVibration.Count > 0 || this.hasFrameVibration)
		//{
		//	Player player = ReInput.players.GetPlayer(0);
		//	Vector2 vector = this.singleFrameVibration;
		//	this.singleFrameVibration = Vector2.zero;
		//	for (int i = this.currentLocalVibration.Count - 1; i >= 0; i--)
		//	{
		//		if (this.currentLocalVibration[i].Alive)
		//		{
		//			vector += this.currentLocalVibration[i].UpdateIntensity(Time.deltaTime);
		//		}
		//		else
		//		{
		//			this.currentLocalVibration.RemoveAt(i);
		//		}
		//	}
		//	Vector2 cameraPos2 = this.cam.transform.position;
		//	for (int j = this.currentWorldVibration.Count - 1; j >= 0; j--)
		//	{
		//		if (this.currentWorldVibration[j].Alive)
		//		{
		//			vector += this.currentWorldVibration[j].UpdateIntensity(cameraPos2, Time.deltaTime);
		//		}
		//		else
		//		{
		//			this.currentWorldVibration.RemoveAt(j);
		//		}
		//	}
		//	this.currentVibration = vector;
		//	vector.x = Mathf.Clamp01(vector.x);
		//	vector.y = Mathf.Clamp01(vector.y);
		//	if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
		//	{
		//		player.SetVibration(0, vector.x);
		//		player.SetVibration(1, vector.y);
		//	}
		//	this.zeroNextFrame = true;
		//}
		//else if (this.zeroNextFrame)
		//{
		//	Player player2 = ReInput.players.GetPlayer(0);
		//	player2.SetVibration(0, 0f);
		//	player2.SetVibration(1, 0f);
		//}
		//this.numVibrationsActive = this.currentLocalVibration.Count + this.currentWorldVibration.Count;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0003CD48 File Offset: 0x0003AF48
	public static void CancelVibration(AudioClip clipToCancel)
	{
		for (int i = DestroyableSingleton<VibrationManager>.Instance.currentLocalVibration.Count - 1; i >= 0; i--)
		{
			if (DestroyableSingleton<VibrationManager>.Instance.currentLocalVibration[i].clip == clipToCancel)
			{
				DestroyableSingleton<VibrationManager>.Instance.currentLocalVibration.RemoveAt(i);
				return;
			}
		}
		for (int j = DestroyableSingleton<VibrationManager>.Instance.currentLocalVibration.Count - 1; j >= 0; j--)
		{
			if (DestroyableSingleton<VibrationManager>.Instance.currentWorldVibration[j].clip == clipToCancel)
			{
				DestroyableSingleton<VibrationManager>.Instance.currentWorldVibration.RemoveAt(j);
				return;
			}
		}
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0003CDE9 File Offset: 0x0003AFE9
	public static void Vibrate(float left, float right)
	{
		DestroyableSingleton<VibrationManager>.Instance.singleFrameVibration += new Vector2(left, right);
		DestroyableSingleton<VibrationManager>.Instance.hasFrameVibration = true;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0003CE14 File Offset: 0x0003B014
	public static void Vibrate(float left, float right, float duration, VibrationManager.VibrationFalloff falloffType = VibrationManager.VibrationFalloff.None, AudioClip sourceClip = null, bool loopClip = false)
	{
		VibrationManager.LocalVibration localVibration = new VibrationManager.LocalVibration();
		localVibration.intensity = new Vector2(left, right);
		localVibration.duration = duration;
		localVibration.falloff = falloffType;
		localVibration.t = 0f;
		localVibration.clip = sourceClip;
		localVibration.loopClip = loopClip;
		localVibration.Init();
		DestroyableSingleton<VibrationManager>.Instance.currentLocalVibration.Add(localVibration);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0003CE74 File Offset: 0x0003B074
	public static void Vibrate(float intensity, Vector2 worldPosition, float radius)
	{
		DestroyableSingleton<VibrationManager>.Instance.tempSingleFrameWorldVibration.intensity = intensity;
		DestroyableSingleton<VibrationManager>.Instance.tempSingleFrameWorldVibration.location = worldPosition;
		DestroyableSingleton<VibrationManager>.Instance.tempSingleFrameWorldVibration.radius = radius;
		Vector2 cameraPos = DestroyableSingleton<VibrationManager>.Instance.cam.transform.position;
		DestroyableSingleton<VibrationManager>.Instance.singleFrameVibration += DestroyableSingleton<VibrationManager>.Instance.tempSingleFrameWorldVibration.UpdateIntensity(cameraPos, 0f);
		DestroyableSingleton<VibrationManager>.Instance.hasFrameVibration = true;
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0003CF00 File Offset: 0x0003B100
	public static void Vibrate(float intensity, Vector2 worldPosition, float radius, float duration, VibrationManager.VibrationFalloff falloffType = VibrationManager.VibrationFalloff.None, AudioClip sourceClip = null, bool loopClip = false)
	{
		VibrationManager.WorldVibration worldVibration = new VibrationManager.WorldVibration();
		worldVibration.intensity = intensity;
		worldVibration.location = worldPosition;
		worldVibration.duration = duration;
		worldVibration.falloff = falloffType;
		worldVibration.radius = radius;
		worldVibration.t = 0f;
		worldVibration.clip = sourceClip;
		worldVibration.loopClip = loopClip;
		worldVibration.Init();
		DestroyableSingleton<VibrationManager>.Instance.currentWorldVibration.Add(worldVibration);
	}

	// Token: 0x04000AAF RID: 2735
	private List<VibrationManager.LocalVibration> currentLocalVibration = new List<VibrationManager.LocalVibration>();

	// Token: 0x04000AB0 RID: 2736
	private List<VibrationManager.WorldVibration> currentWorldVibration = new List<VibrationManager.WorldVibration>();

	// Token: 0x04000AB1 RID: 2737
	private Vector2 singleFrameVibration = Vector2.zero;

	// Token: 0x04000AB2 RID: 2738
	private bool hasFrameVibration;

	// Token: 0x04000AB3 RID: 2739
	private bool zeroNextFrame;

	// Token: 0x04000AB4 RID: 2740
	public int numVibrationsActive;

	// Token: 0x04000AB5 RID: 2741
	public Vector2 currentVibration;

	// Token: 0x04000AB6 RID: 2742
	private Camera cam;

	// Token: 0x04000AB7 RID: 2743
	private VibrationManager.WorldVibration tempSingleFrameWorldVibration = new VibrationManager.WorldVibration();

	// Token: 0x04000AB8 RID: 2744
	private VibrationManager.WorldVibration tempAmbientSoundVibration = new VibrationManager.WorldVibration();

	// Token: 0x04000AB9 RID: 2745
	private static float[] samples = new float[2];

	// Token: 0x02000402 RID: 1026
	public enum VibrationFalloff
	{
		// Token: 0x04001B4B RID: 6987
		None,
		// Token: 0x04001B4C RID: 6988
		Linear,
		// Token: 0x04001B4D RID: 6989
		InverseLinear
	}

	// Token: 0x02000403 RID: 1027
	private class LocalVibration
	{
		// Token: 0x0600191D RID: 6429 RVA: 0x000762BF File Offset: 0x000744BF
		public void Init()
		{
			if (this.clip != null)
			{
				this.duration = Mathf.Max(this.duration, this.clip.length);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x000762EB File Offset: 0x000744EB
		public bool Alive
		{
			get
			{
				return this.t <= this.duration;
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00076300 File Offset: 0x00074500
		public Vector2 UpdateIntensity(float deltaTime)
		{
			float num = 1f;
			VibrationManager.VibrationFalloff vibrationFalloff = this.falloff;
			if (vibrationFalloff != VibrationManager.VibrationFalloff.Linear)
			{
				if (vibrationFalloff == VibrationManager.VibrationFalloff.InverseLinear)
				{
					num = Mathf.Clamp01(this.t / this.duration);
				}
			}
			else
			{
				float num2 = Mathf.Clamp01(this.t / this.duration);
				num = 1f - num2;
			}
			if (this.clip != null)
			{
				float num3 = this.t / this.clip.length;
				if (num3 > 1f)
				{
					num = 0f;
					this.t = this.duration;
				}
				else
				{
					int num4 = (int)(Mathf.Clamp01(num3) * (float)(this.clip.samples - 1));
					if (this.clip.GetData(VibrationManager.samples, num4))
					{
						num *= Mathf.Abs(VibrationManager.samples[0]);
					}
				}
			}
			this.t += deltaTime;
			if (this.t >= this.duration && this.loopClip)
			{
				this.t -= this.duration;
				if (this.t < 0f)
				{
					this.t = 0f;
				}
			}
			return this.intensity * num;
		}

		// Token: 0x04001B4E RID: 6990
		public Vector2 intensity;

		// Token: 0x04001B4F RID: 6991
		public float t;

		// Token: 0x04001B50 RID: 6992
		public float duration;

		// Token: 0x04001B51 RID: 6993
		public VibrationManager.VibrationFalloff falloff;

		// Token: 0x04001B52 RID: 6994
		public AudioClip clip;

		// Token: 0x04001B53 RID: 6995
		public bool loopClip;
	}

	// Token: 0x02000404 RID: 1028
	private class WorldVibration
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x0007642F File Offset: 0x0007462F
		public void Init()
		{
			if (this.clip != null)
			{
				this.duration = Mathf.Max(this.duration, this.clip.length);
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0007645B File Offset: 0x0007465B
		public bool Alive
		{
			get
			{
				return this.t <= this.duration;
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00076470 File Offset: 0x00074670
		public Vector2 UpdateIntensity(Vector2 cameraPos, float deltaTime)
		{
			//float num = 1f;
			//VibrationManager.VibrationFalloff vibrationFalloff = this.falloff;
			//if (vibrationFalloff != VibrationManager.VibrationFalloff.Linear)
			//{
			//	if (vibrationFalloff == VibrationManager.VibrationFalloff.InverseLinear)
			//	{
			//		num = Mathf.Clamp01(this.t / this.duration);
			//	}
			//}
			//else
			//{
			//	float num2 = Mathf.Clamp01(this.t / this.duration);
			//	num = 1f - num2;
			//}
			//Vector2 vector = this.location - cameraPos;
			//float magnitude = vector.magnitude;
			//vector /= magnitude;
			//Vector2 vector2;
			//vector2..ctor(1f - Mathf.Clamp01(vector.x), 1f - Mathf.Clamp01(-vector.x));
			//num *= 1f - Mathf.Clamp01(magnitude / this.radius);
			//if (this.clip != null)
			//{
			//	float num3 = this.t / this.clip.length;
			//	if (num3 > 1f)
			//	{
			//		num = 0f;
			//		this.t = this.duration;
			//	}
			//	else if (this.clip.channels == 2)
			//	{
			//		int num4 = (int)(Mathf.Clamp01(num3) * (float)(this.clip.samples / 2 - 1)) * 2;
			//		if (this.clip.GetData(VibrationManager.samples, num4))
			//		{
			//			vector2.x *= Mathf.Abs(VibrationManager.samples[0]);
			//			vector2.y *= Mathf.Abs(VibrationManager.samples[1]);
			//		}
			//	}
			//	else
			//	{
			//		int num5 = (int)(Mathf.Clamp01(num3) * (float)(this.clip.samples - 1));
			//		if (this.clip.GetData(VibrationManager.samples, num5))
			//		{
			//			num *= Mathf.Abs(VibrationManager.samples[0]);
			//		}
			//	}
			//}
			//this.t += deltaTime;
			//if (this.t >= this.duration && this.loopClip)
			//{
			//	this.t -= this.duration;
			//	if (this.t < 0f)
			//	{
			//		this.t = 0f;
			//	}
			//}
			//return this.intensity * num * vector2;
			return new Vector2();
		}

		// Token: 0x04001B54 RID: 6996
		public float intensity;

		// Token: 0x04001B55 RID: 6997
		public Vector2 location;

		// Token: 0x04001B56 RID: 6998
		public float radius;

		// Token: 0x04001B57 RID: 6999
		public float t;

		// Token: 0x04001B58 RID: 7000
		public float duration;

		// Token: 0x04001B59 RID: 7001
		public VibrationManager.VibrationFalloff falloff;

		// Token: 0x04001B5A RID: 7002
		public AudioClip clip;

		// Token: 0x04001B5B RID: 7003
		public bool loopClip;
	}
}
