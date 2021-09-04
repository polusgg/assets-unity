using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

// Token: 0x02000212 RID: 530
public class SoundManager : MonoBehaviour
{
	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0004CE2B File Offset: 0x0004B02B
	public static SoundManager Instance
	{
		get
		{
			if (!SoundManager._Instance)
			{
				SoundManager._Instance = (Object.FindObjectOfType<SoundManager>() ?? new GameObject("SoundManager").AddComponent<SoundManager>());
			}
			return SoundManager._Instance;
		}
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x0004CE5B File Offset: 0x0004B05B
	public void Start()
	{
		if (SoundManager._Instance && SoundManager._Instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		SoundManager._Instance = this;
		this.UpdateVolume();
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0004CE9C File Offset: 0x0004B09C
	public void Update()
	{
		for (int i = 0; i < this.soundPlayers.Count; i++)
		{
			this.soundPlayers[i].Update(Time.deltaTime);
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0004CED5 File Offset: 0x0004B0D5
	private void UpdateVolume()
	{
		this.ChangeSfxVolume(SaveManager.SfxVolume);
		this.ChangeMusicVolume(SaveManager.MusicVolume);
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0004CEF0 File Offset: 0x0004B0F0
	public void ChangeSfxVolume(float volume)
	{
		if (volume <= 0f)
		{
			SoundManager.SfxVolume = -80f;
		}
		else
		{
			SoundManager.SfxVolume = Mathf.Log10(volume) * 20f;
		}
		//this.musicMixer.audioMixer.SetFloat("SfxVolume", SoundManager.SfxVolume);
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0004CF40 File Offset: 0x0004B140
	public void ChangeMusicVolume(float volume)
	{
		if (volume <= 0f)
		{
			SoundManager.MusicVolume = -80f;
		}
		else
		{
			SoundManager.MusicVolume = Mathf.Log10(volume) * 20f;
		}
		//this.musicMixer.audioMixer.SetFloat("MusicVolume", SoundManager.MusicVolume);
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0004CF90 File Offset: 0x0004B190
	public void StopNamedSound(string name)
	{
		for (int i = 0; i < this.soundPlayers.Count; i++)
		{
			ISoundPlayer soundPlayer = this.soundPlayers[i];
			if (soundPlayer.Name.Equals(name))
			{
				Object.Destroy(soundPlayer.Player);
				this.soundPlayers.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0004CFE8 File Offset: 0x0004B1E8
	public void StopSound(AudioClip clip)
	{
		AudioSource audioSource;
		if (this.allSources.TryGetValue(clip, out audioSource))
		{
			this.allSources.Remove(clip);
			audioSource.Stop();
			Object.Destroy(audioSource);
		}
		for (int i = 0; i < this.soundPlayers.Count; i++)
		{
			ISoundPlayer soundPlayer = this.soundPlayers[i];
			if (soundPlayer.Player.clip == clip)
			{
				Object.Destroy(soundPlayer.Player);
				this.soundPlayers.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0004D06C File Offset: 0x0004B26C
	public void StopAllSound()
	{
		for (int i = 0; i < this.soundPlayers.Count; i++)
		{
			Object.Destroy(this.soundPlayers[i].Player);
		}
		this.soundPlayers.Clear();
		foreach (KeyValuePair<AudioClip, AudioSource> keyValuePair in this.allSources)
		{
			AudioSource value = keyValuePair.Value;
			value.volume = 0f;
			value.Stop();
			Object.Destroy(keyValuePair.Value);
		}
		this.allSources.Clear();
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0004D120 File Offset: 0x0004B320
	public AudioSource PlayNamedSound(string name, AudioClip sound, bool loop, bool playAsSfx = false)
	{
		return this.PlayDynamicSound(name, sound, loop, delegate(AudioSource a, float b)
		{
		}, playAsSfx);
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0004D14C File Offset: 0x0004B34C
	public AudioSource GetNamedAudioSource(string name)
	{
		return this.PlayDynamicSound(name, null, false, delegate(AudioSource a, float b)
		{
		}, true);
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0004D178 File Offset: 0x0004B378
	public AudioSource PlayDynamicSound(string name, AudioClip clip, bool loop, DynamicSound.GetDynamicsFunction volumeFunc, bool playAsSfx = false)
	{
		DynamicSound dynamicSound = null;
		for (int i = 0; i < this.soundPlayers.Count; i++)
		{
			ISoundPlayer soundPlayer = this.soundPlayers[i];
			if (soundPlayer.Name == name && soundPlayer is DynamicSound)
			{
				dynamicSound = (DynamicSound)soundPlayer;
				break;
			}
		}
		if (dynamicSound == null)
		{
			dynamicSound = new DynamicSound();
			dynamicSound.Name = name;
			dynamicSound.Player = base.gameObject.AddComponent<AudioSource>();
			dynamicSound.Player.outputAudioMixerGroup = ((loop && !playAsSfx) ? this.musicMixer : this.sfxMixer);
			dynamicSound.Player.playOnAwake = false;
			this.soundPlayers.Add(dynamicSound);
		}
		dynamicSound.Player.loop = loop;
		dynamicSound.SetTarget(clip, volumeFunc);
		return dynamicSound.Player;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0004D240 File Offset: 0x0004B440
	public void CrossFadeSound(string name, AudioClip clip, float maxVolume, float duration = 1.5f)
	{
		CrossFader crossFader = null;
		for (int i = 0; i < this.soundPlayers.Count; i++)
		{
			ISoundPlayer soundPlayer = this.soundPlayers[i];
			if (soundPlayer.Name == name && soundPlayer is CrossFader)
			{
				crossFader = (CrossFader)soundPlayer;
				break;
			}
		}
		if (crossFader == null)
		{
			crossFader = new CrossFader();
			crossFader.Name = name;
			crossFader.MaxVolume = maxVolume;
			crossFader.Player = base.gameObject.AddComponent<AudioSource>();
			crossFader.Player.outputAudioMixerGroup = this.musicMixer;
			crossFader.Player.playOnAwake = false;
			crossFader.Player.loop = true;
			this.soundPlayers.Add(crossFader);
		}
		crossFader.SetTarget(clip);
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0004D2F8 File Offset: 0x0004B4F8
	public AudioSource PlaySoundImmediate(AudioClip clip, bool loop, float volume = 1f, float pitch = 1f)
	{
		if (clip == null)
		{
			Debug.LogWarning("Missing audio clip");
			return null;
		}
		AudioSource audioSource;
		if (this.allSources.TryGetValue(clip, out audioSource))
		{
			audioSource.pitch = pitch;
			audioSource.loop = loop;
			audioSource.Play();
		}
		else
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = (loop ? this.musicMixer : this.sfxMixer);
			audioSource.playOnAwake = false;
			audioSource.volume = volume;
			audioSource.pitch = pitch;
			audioSource.loop = loop;
			audioSource.clip = clip;
			audioSource.Play();
			this.allSources.Add(clip, audioSource);
		}
		return audioSource;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0004D39C File Offset: 0x0004B59C
	public bool SoundIsPlaying(AudioClip clip)
	{
		AudioSource audioSource;
		return this.allSources.TryGetValue(clip, out audioSource) && !audioSource.isPlaying;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0004D3C4 File Offset: 0x0004B5C4
	public AudioSource PlaySound(AudioClip clip, bool loop, float volume = 1f)
	{
		if (clip == null)
		{
			Debug.LogWarning("Missing audio clip");
			return null;
		}
		AudioSource audioSource;
		if (this.allSources.TryGetValue(clip, out audioSource))
		{
			if (!audioSource.isPlaying)
			{
				audioSource.volume = volume;
				audioSource.loop = loop;
				audioSource.Play();
			}
		}
		else
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = (loop ? this.musicMixer : this.sfxMixer);
			audioSource.playOnAwake = false;
			audioSource.volume = volume;
			audioSource.loop = loop;
			audioSource.clip = clip;
			audioSource.Play();
			this.allSources.Add(clip, audioSource);
		}
		return audioSource;
	}

	// Token: 0x04000E03 RID: 3587
	private static SoundManager _Instance;

	// Token: 0x04000E04 RID: 3588
	public AudioMixerGroup musicMixer;

	// Token: 0x04000E05 RID: 3589
	public AudioMixerGroup sfxMixer;

	// Token: 0x04000E06 RID: 3590
	public static float MusicVolume = 1f;

	// Token: 0x04000E07 RID: 3591
	public static float SfxVolume = 1f;

	// Token: 0x04000E08 RID: 3592
	private Dictionary<AudioClip, AudioSource> allSources = new Dictionary<AudioClip, AudioSource>();

	// Token: 0x04000E09 RID: 3593
	public List<ISoundPlayer> soundPlayers = new List<ISoundPlayer>();
}
