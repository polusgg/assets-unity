using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class WeaponsMinigame : Minigame
{
	// Token: 0x06000DC2 RID: 3522 RVA: 0x000532F0 File Offset: 0x000514F0
	public override void Begin(PlayerTask task)
	{
		base.SetupInput(false);
		base.Begin(task);
		this.ScoreText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.AstDestroyed, new object[]
		{
			this.MyNormTask.taskStep
		});
		this.TimeToSpawn.Next();
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x0005334A File Offset: 0x0005154A
	protected override IEnumerator CoAnimateOpen()
	{
		for (float timer = 0f; timer < 0.1f; timer += Time.deltaTime)
		{
			float num = timer / 0.1f;
			base.transform.localScale = new Vector3(num, 0.1f, num);
			yield return null;
		}
		for (float timer = 0.010000001f; timer < 0.1f; timer += Time.deltaTime)
		{
			float num2 = timer / 0.1f;
			base.transform.localScale = new Vector3(1f, num2, 1f);
			yield return null;
		}
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		yield break;
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x00053359 File Offset: 0x00051559
	protected override IEnumerator CoDestroySelf()
	{
		//for (float timer = 0.010000001f; timer < 0.1f; timer += Time.deltaTime)
		//{
		//	float num = 1f - timer / 0.1f;
		//	base.transform.localScale = new Vector3(1f, num, 1f);
		//	yield return null;
		//}
		//for (float timer = 0f; timer < 0.1f; timer += Time.deltaTime)
		//{
		//	float num2 = 1f - timer / 0.1f;
		//	base.transform.localScale = new Vector3(num2, 0.1f, num2);
		//	yield return null;
		//}
		//Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00053368 File Offset: 0x00051568
	public void FixedUpdate()
	{
		this.Background.color = Color.Lerp(Palette.ClearWhite, Color.white, Mathf.Sin(Time.time * 3f) * 0.1f + 0.79999995f);
		if (this.MyNormTask && this.MyNormTask.IsComplete)
		{
			return;
		}
		this.Timer += Time.fixedDeltaTime;
		if (this.Timer >= this.TimeToSpawn.Last)
		{
			this.Timer = 0f;
			this.TimeToSpawn.Next();
			if (this.asteroidPool.InUse < this.MyNormTask.MaxStep - this.MyNormTask.TaskStep)
			{
				Asteroid ast = this.asteroidPool.Get<Asteroid>();
				ast.transform.localPosition = new Vector3(this.XSpan.max, this.YSpan.Next(), -1f);
				ast.TargetPosition = new Vector3(this.XSpan.min, this.YSpan.Next(), -1f);
				ast.GetComponent<ButtonBehavior>().OnClick.AddListener(delegate()
				{
					this.BreakApart(ast);
				});
			}
		}
		this.myController.Update();
		bool flag = this.myController.CheckHover(this.BackgroundCol);
		if (Controller.currentTouchType == Controller.TouchType.Joystick)
		{
			if (flag)
			{
				Vector3 vector = this.myController.HoverPosition - (Vector2) base.transform.position;
				vector.z = -2f;
				this.TargetReticle.transform.localPosition = vector;
				vector.z = 0f;
				this.TargetLines.SetPosition(1, vector);
			}
			else
			{
				Bounds bounds = this.BackgroundCol.bounds;
				Vector3 vector2 = this.myController.HoverPosition;
				vector2.x = Mathf.Clamp(vector2.x, bounds.min.x, bounds.max.x);
				vector2.y = Mathf.Clamp(vector2.y, bounds.min.y, bounds.max.y);
				VirtualCursor.instance.SetWorldPosition(vector2);
				vector2 -= base.transform.position;
				vector2.z = -2f;
				this.TargetReticle.transform.localPosition = vector2;
				vector2.z = 0f;
				this.TargetLines.SetPosition(1, vector2);
			}
		}
		if (this.myController.CheckDrag(this.BackgroundCol) == DragState.TouchStart)
		{
			if (Constants.ShouldPlaySfx())
			{
				SoundManager.Instance.PlaySound(this.ShootSound, false, 1f);
			}
			Vector3 vector3 = this.myController.DragPosition - (Vector2) base.transform.position;
			vector3.z = -2f;
			this.TargetReticle.transform.localPosition = vector3;
			vector3.z = 0f;
			this.TargetLines.SetPosition(1, vector3);
			if (ShipStatus.Instance.WeaponsImage && !ShipStatus.Instance.WeaponsImage.IsPlaying())
			{
				PlayerControl.LocalPlayer.RpcPlayAnimation(6);
			}
		}
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x000536DC File Offset: 0x000518DC
	public void BreakApart(Asteroid ast)
	{
		if (Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(this.ExplodeSounds.Random<AudioClip>(), false, 1f).pitch = FloatRange.Next(0.8f, 1.2f);
		}
		if (!this.MyNormTask.IsComplete)
		{
			base.StartCoroutine(ast.CoBreakApart());
			if (this.MyNormTask)
			{
				this.MyNormTask.NextStep();
				this.ScoreText.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.AstDestroyed, new object[]
				{
					this.MyNormTask.taskStep
				});
			}
			if (this.MyNormTask && this.MyNormTask.IsComplete)
			{
				base.StartCoroutine(base.CoStartClose(0.75f));
				foreach (PoolableBehavior poolableBehavior in this.asteroidPool.activeChildren)
				{
					Asteroid asteroid = (Asteroid)poolableBehavior;
					if (!(asteroid == ast))
					{
						base.StartCoroutine(asteroid.CoBreakApart());
					}
				}
			}
		}
	}

	// Token: 0x040011C5 RID: 4549
	public FloatRange XSpan = new FloatRange(-1.15f, 1.15f);

	// Token: 0x040011C6 RID: 4550
	public FloatRange YSpan = new FloatRange(-1.15f, 1.15f);

	// Token: 0x040011C7 RID: 4551
	public FloatRange TimeToSpawn;

	// Token: 0x040011C8 RID: 4552
	public ObjectPoolBehavior asteroidPool;

	// Token: 0x040011C9 RID: 4553
	public TextRenderer ScoreText;

	// Token: 0x040011CA RID: 4554
	public SpriteRenderer TargetReticle;

	// Token: 0x040011CB RID: 4555
	public LineRenderer TargetLines;

	// Token: 0x040011CC RID: 4556
	private Vector3 TargetCenter;

	// Token: 0x040011CD RID: 4557
	public Collider2D BackgroundCol;

	// Token: 0x040011CE RID: 4558
	public SpriteRenderer Background;

	// Token: 0x040011CF RID: 4559
	public Controller myController = new Controller();

	// Token: 0x040011D0 RID: 4560
	private float Timer;

	// Token: 0x040011D1 RID: 4561
	public AudioClip ShootSound;

	// Token: 0x040011D2 RID: 4562
	public AudioClip[] ExplodeSounds;
}
