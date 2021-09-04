using System;
using System.Collections;
using System.Linq;
//using Rewired;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class PhotosMinigame : Minigame
{
	// Token: 0x06000150 RID: 336 RVA: 0x000091C4 File Offset: 0x000073C4
	public override void Begin(PlayerTask task)
	{
		base.Begin(task);
		byte[] data = this.MyNormTask.Data;
		if (data != null && data.Length == 6)
		{
			for (int i = 0; i < this.photos.Length; i++)
			{
				this.photos[i].transform.localPosition = this.ReadPosition(i);
			}
		}
		else
		{
			this.MyNormTask.Data = new byte[6];
			for (int j = 0; j < this.photos.Length; j++)
			{
				this.WritePosition(j, this.photos[j]);
			}
		}
		PlayerControl.LocalPlayer.SetPlayerMaterialColors(this.selectorHand);
		base.SetupInput(false);
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00009269 File Offset: 0x00007469
	public IEnumerator Start()
	{
		float z = 0f;
		yield return null;
		foreach (GamePhotoBehaviour gamePhotoBehaviour in this.photos)
		{
			gamePhotoBehaviour.zOffset = z;
			gamePhotoBehaviour.transform.SetLocalZ(1f + z);
			z -= 0.01f;
			gamePhotoBehaviour.Image.sprite = this.PhotoContents.Random<Sprite>();
			bool flag = gamePhotoBehaviour.Hitbox.IsTouching(this.PoolHitbox);
			gamePhotoBehaviour.TargetColor = (flag ? GamePhotoBehaviour.InWaterPink : Color.white);
			gamePhotoBehaviour.Frame.color = gamePhotoBehaviour.TargetColor;
			gamePhotoBehaviour.Image.color = Color.Lerp(gamePhotoBehaviour.TargetColor, Palette.ClearWhite, this.MyNormTask.TaskTimer / 60f);
		}
		yield break;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00009278 File Offset: 0x00007478
	private void Update()
	{
        //	if (this.amClosing != Minigame.CloseState.None)
        //	{
        //		return;
        //	}
        //	this.controller.Update();
        //	foreach (GamePhotoBehaviour gamePhotoBehaviour in this.photos)
        //	{
        //		gamePhotoBehaviour.Frame.color = gamePhotoBehaviour.TargetColor;
        //		gamePhotoBehaviour.Image.color = Color.Lerp(gamePhotoBehaviour.TargetColor, Palette.ClearWhite, this.MyNormTask.TaskTimer / 60f);
        //	}
        //	if (Controller.currentTouchType == Controller.TouchType.Joystick)
        //	{
        //		bool button = ReInput.players.GetPlayer(0).GetButton(11);
        //		Vector3 position = this.selectorObject.transform.position;
        //		position.x = VirtualCursor.currentPosition.x;
        //		position.y = VirtualCursor.currentPosition.y;
        //		this.selectorObject.transform.position = position;
        //		if (button)
        //		{
        //			if (!this.prevHadButton)
        //			{
        //				float num = 0f;
        //				this.currentlyGrabbedObject = -1;
        //				for (int j = 0; j < this.photos.Length; j++)
        //				{
        //					float sqrMagnitude = (this.photos[j].transform.localPosition - this.selectorObject.transform.localPosition).sqrMagnitude;
        //					if (sqrMagnitude <= 1f && (this.currentlyGrabbedObject == -1 || sqrMagnitude < num))
        //					{
        //						num = sqrMagnitude;
        //						this.currentlyGrabbedObject = j;
        //					}
        //				}
        //				if (this.currentlyGrabbedObject != -1)
        //				{
        //					GamePhotoBehaviour gamePhotoBehaviour2 = this.photos[this.currentlyGrabbedObject];
        //					gamePhotoBehaviour2.StopAllCoroutines();
        //					gamePhotoBehaviour2.StartCoroutine(gamePhotoBehaviour2.Pickup());
        //					this.FixZ(gamePhotoBehaviour2);
        //				}
        //			}
        //			else if (this.currentlyGrabbedObject != -1)
        //			{
        //				GamePhotoBehaviour gamePhotoBehaviour3 = this.photos[this.currentlyGrabbedObject];
        //				Vector3 position2 = gamePhotoBehaviour3.transform.position;
        //				position2.x = position.x;
        //				position2.y = position.y;
        //				gamePhotoBehaviour3.transform.position = position2;
        //			}
        //			this.prevHadButton = true;
        //		}
        //		else
        //		{
        //			if (this.prevHadButton && this.currentlyGrabbedObject != -1)
        //			{
        //				GamePhotoBehaviour gamePhotoBehaviour4 = this.photos[this.currentlyGrabbedObject];
        //				bool inWater = gamePhotoBehaviour4.Hitbox.IsTouching(this.PoolHitbox);
        //				gamePhotoBehaviour4.StopAllCoroutines();
        //				gamePhotoBehaviour4.StartCoroutine(gamePhotoBehaviour4.Drop(inWater));
        //				this.WritePosition(this.currentlyGrabbedObject, gamePhotoBehaviour4);
        //			}
        //			this.prevHadButton = false;
        //			this.currentlyGrabbedObject = -1;
        //		}
        //		if (this.currentlyGrabbedObject != -1 && this.selectorObject.gameObject.activeSelf)
        //		{
        //			this.selectorObject.gameObject.SetActive(false);
        //		}
        //		else if (this.currentlyGrabbedObject == -1 && !this.selectorObject.gameObject.activeSelf)
        //		{
        //			this.selectorObject.gameObject.SetActive(true);
        //		}
        //	}
        //	else
        //	{
        //		if (this.selectorObject.gameObject.activeSelf)
        //		{
        //			this.selectorObject.gameObject.SetActive(false);
        //		}
        //		if (this.currentlyGrabbedObject != -1)
        //		{
        //			GamePhotoBehaviour gamePhotoBehaviour5 = this.photos[this.currentlyGrabbedObject];
        //			bool inWater2 = gamePhotoBehaviour5.Hitbox.IsTouching(this.PoolHitbox);
        //			gamePhotoBehaviour5.StopAllCoroutines();
        //			gamePhotoBehaviour5.StartCoroutine(gamePhotoBehaviour5.Drop(inWater2));
        //			this.WritePosition(this.currentlyGrabbedObject, gamePhotoBehaviour5);
        //			this.currentlyGrabbedObject = -1;
        //			this.prevHadButton = false;
        //		}
        //		for (int k = 0; k < this.photos.Length; k++)
        //		{
        //			GamePhotoBehaviour gamePhotoBehaviour6 = this.photos[k];
        //			switch (this.controller.CheckDrag(gamePhotoBehaviour6.Hitbox))
        //			{
        //			case DragState.TouchStart:
        //				gamePhotoBehaviour6.StopAllCoroutines();
        //				gamePhotoBehaviour6.StartCoroutine(gamePhotoBehaviour6.Pickup());
        //				this.FixZ(gamePhotoBehaviour6);
        //				break;
        //			case DragState.Dragging:
        //			{
        //				Vector3 position3 = this.controller.DragPosition;
        //				position3.z = gamePhotoBehaviour6.transform.position.z;
        //				gamePhotoBehaviour6.transform.position = position3;
        //				break;
        //			}
        //			case DragState.Released:
        //			{
        //				bool inWater3 = gamePhotoBehaviour6.Hitbox.IsTouching(this.PoolHitbox);
        //				gamePhotoBehaviour6.StopAllCoroutines();
        //				gamePhotoBehaviour6.StartCoroutine(gamePhotoBehaviour6.Drop(inWater3));
        //				this.WritePosition(k, gamePhotoBehaviour6);
        //				break;
        //			}
        //			}
        //		}
        //	}
        //	if (this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.NotStarted && !this.controller.AnyTouch && this.photos.All((GamePhotoBehaviour p) => p.Hitbox.IsTouching(this.PoolHitbox)))
        //	{
        //		this.MyNormTask.TimerStarted = NormalPlayerTask.TimerState.Started;
        //		base.StartCoroutine(base.CoStartClose(0.75f));
        //		return;
        //	}
        //	if (this.MyNormTask.TimerStarted == NormalPlayerTask.TimerState.Finished && !this.controller.AnyTouch && this.photos.All((GamePhotoBehaviour p) => !p.Hitbox.IsTouching(this.PoolHitbox)))
        //	{
        //		this.MyNormTask.NextStep();
        //		this.Close();
        //	}
    }

    // Token: 0x06000153 RID: 339 RVA: 0x00009740 File Offset: 0x00007940
    private void FixZ(GamePhotoBehaviour current)
	{
		if (current.zOffset != (float)this.photos.Length / -100f)
		{
			current.zOffset = (float)this.photos.Length / -100f;
			foreach (GamePhotoBehaviour gamePhotoBehaviour in this.photos)
			{
				if (!(gamePhotoBehaviour == current))
				{
					gamePhotoBehaviour.zOffset += 0.01f;
					gamePhotoBehaviour.transform.SetLocalZ(1f + gamePhotoBehaviour.zOffset);
				}
			}
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000097C4 File Offset: 0x000079C4
	private Vector3 ReadPosition(int i)
	{
		Vector3 result = default(Vector3);
		result.x = (float)this.MyNormTask.Data[i * 2] / 255f * 10f - 5f;
		result.y = (float)this.MyNormTask.Data[i * 2 + 1] / 255f * 10f - 5f;
		result.z = 1f;
		return result;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000983C File Offset: 0x00007A3C
	private void WritePosition(int i, GamePhotoBehaviour p)
	{
		Vector3 localPosition = p.transform.localPosition;
		this.MyNormTask.Data[i * 2] = (byte)((localPosition.x + 5f) / 10f * 255f);
		this.MyNormTask.Data[i * 2 + 1] = (byte)((localPosition.y + 5f) / 10f * 255f);
	}

	// Token: 0x040001A8 RID: 424
	public GamePhotoBehaviour[] photos;

	// Token: 0x040001A9 RID: 425
	public Sprite[] PhotoContents;

	// Token: 0x040001AA RID: 426
	public Collider2D PoolHitbox;

	// Token: 0x040001AB RID: 427
	private Controller controller = new Controller();

	// Token: 0x040001AC RID: 428
	public Transform selectorObject;

	// Token: 0x040001AD RID: 429
	public SpriteRenderer selectorHand;

	// Token: 0x040001AE RID: 430
	private bool prevHadButton;

	// Token: 0x040001AF RID: 431
	private int currentlyGrabbedObject = -1;
}
