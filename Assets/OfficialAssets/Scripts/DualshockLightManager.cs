using System;
using System.Collections.Generic;
//using Rewired;
//using Rewired.ControllerExtensions;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class DualshockLightManager : DestroyableSingleton<DualshockLightManager>
{
	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000897 RID: 2199 RVA: 0x00038442 File Offset: 0x00036642
	// (set) Token: 0x06000898 RID: 2200 RVA: 0x0003844A File Offset: 0x0003664A
	public Color BaseColor
	{
		get
		{
			return this.baseColor;
		}
		set
		{
			this.baseColor = value;
		}
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00038454 File Offset: 0x00036654
	public DualshockLightManager.LightOverlayHandle AllocateLight()
	{
		DualshockLightManager.LightOverlayHandle lightOverlayHandle = new DualshockLightManager.LightOverlayHandle();
		this.overlays.Add(lightOverlayHandle);
		return lightOverlayHandle;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00038474 File Offset: 0x00036674
	private float GetExternalBrightnessFromElectrical()
	{
		if (ShipStatus.Instance)
		{
			float num = (float)((SwitchSystem)ShipStatus.Instance.Systems[SystemTypes.Electrical]).Value / 255f;
			return Mathf.Lerp(0.05f, 1f, num);
		}
		return 1f;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x000384C8 File Offset: 0x000366C8
	private void Update()
	{
		//Player player = ReInput.players.GetPlayer(0);
		//if (player.controllers.joystickCount > 0)
		//{
		//	IDualShock4Extension extension = player.controllers.Joysticks[0].GetExtension<IDualShock4Extension>();
		//	if (extension != null)
		//	{
		//		if (ShipStatus.Instance || LobbyBehaviour.Instance)
		//		{
		//			Color color = this.baseColor;
		//			color.a *= this.GetExternalBrightnessFromElectrical() * 0.5f;
		//			for (int i = this.lightOverlayFlashes.Count - 1; i >= 0; i--)
		//			{
		//				if (this.lightOverlayFlashes[i].Alive)
		//				{
		//					this.lightOverlayFlashes[i].Update(Time.deltaTime);
		//				}
		//				else
		//				{
		//					this.lightOverlayFlashes[i].Dispose();
		//					this.lightOverlayFlashes.RemoveAt(i);
		//				}
		//			}
		//			foreach (DualshockLightManager.LightOverlayHandle lightOverlayHandle in this.overlays)
		//			{
		//				Color color2 = lightOverlayHandle.color;
		//				color2.a = lightOverlayHandle.intensity;
		//				color = Color.Lerp(color, color2, lightOverlayHandle.color.a);
		//			}
		//			if (this.oldColor != color)
		//			{
		//				this.oldColor = color;
		//				if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
		//				{
		//					extension.SetLightColor(color);
		//					return;
		//				}
		//			}
		//		}
		//		else if (this.oldColor != Palette.Blue)
		//		{
		//			this.oldColor = Palette.Blue;
		//			if (ActiveInputManager.currentControlType == ActiveInputManager.InputType.Joystick)
		//			{
		//				extension.SetLightColor(Palette.Blue);
		//			}
		//		}
		//	}
		//}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00038668 File Offset: 0x00036868
	public static void Flash(Color c, float intensity, AudioClip clip)
	{
		DualshockLightManager.LightOverlayFlash lightOverlayFlash = new DualshockLightManager.LightOverlayFlash();
		lightOverlayFlash.handle = DestroyableSingleton<DualshockLightManager>.Instance.AllocateLight();
		lightOverlayFlash.handle.color = c;
		lightOverlayFlash.handle.intensity = intensity;
		lightOverlayFlash.clip = clip;
		lightOverlayFlash.Init();
		DestroyableSingleton<DualshockLightManager>.Instance.lightOverlayFlashes.Add(lightOverlayFlash);
	}

	// Token: 0x040009FB RID: 2555
	private const float lightIntensity = 0.5f;

	// Token: 0x040009FC RID: 2556
	private Color baseColor = new Color(0f, 0.5f, 1f, 1f);

	// Token: 0x040009FD RID: 2557
	private Color oldColor = Color.white;

	// Token: 0x040009FE RID: 2558
	private List<DualshockLightManager.LightOverlayHandle> overlays = new List<DualshockLightManager.LightOverlayHandle>();

	// Token: 0x040009FF RID: 2559
	private List<DualshockLightManager.LightOverlayFlash> lightOverlayFlashes = new List<DualshockLightManager.LightOverlayFlash>();

	// Token: 0x020003E4 RID: 996
	public class LightOverlayHandle
	{
		// Token: 0x060018E4 RID: 6372 RVA: 0x00075D4F File Offset: 0x00073F4F
		public void Dispose()
		{
			DestroyableSingleton<DualshockLightManager>.Instance.overlays.Remove(this);
		}

		// Token: 0x04001ADA RID: 6874
		public Color color;

		// Token: 0x04001ADB RID: 6875
		public float intensity = 1f;
	}

	// Token: 0x020003E5 RID: 997
	public class LightOverlayFlash
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x00075D75 File Offset: 0x00073F75
		public void Init()
		{
			if (this.clip != null)
			{
				this.duration = Mathf.Max(this.duration, this.clip.length);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00075DA1 File Offset: 0x00073FA1
		public bool Alive
		{
			get
			{
				return this.t <= this.duration;
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00075DB4 File Offset: 0x00073FB4
		public void Dispose()
		{
			if (this.handle != null)
			{
				this.handle.Dispose();
				this.handle = null;
			}
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00075DD0 File Offset: 0x00073FD0
		public void Update(float deltaTime)
		{
			float num = 1f;
			if (this.clip != null)
			{
				float num2 = this.t / this.clip.length;
				if (num2 > 1f)
				{
					num = 0f;
					this.t = this.duration;
				}
				else
				{
					int num3 = (int)(Mathf.Clamp01(num2) * (float)(this.clip.samples - 1));
					if (this.clip.GetData(DualshockLightManager.LightOverlayFlash.samples, num3))
					{
						num *= Mathf.Abs(DualshockLightManager.LightOverlayFlash.samples[0]);
					}
				}
			}
			this.t += deltaTime;
			this.handle.color.a = Mathf.Clamp01(num * 10f);
		}

		// Token: 0x04001ADC RID: 6876
		public DualshockLightManager.LightOverlayHandle handle;

		// Token: 0x04001ADD RID: 6877
		public AudioClip clip;

		// Token: 0x04001ADE RID: 6878
		public float t;

		// Token: 0x04001ADF RID: 6879
		public float duration;

		// Token: 0x04001AE0 RID: 6880
		private static float[] samples = new float[2];
	}
}
