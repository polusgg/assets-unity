using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class NormalPlayerTask : PlayerTask
{
	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0004FF24 File Offset: 0x0004E124
	public override int TaskStep
	{
		get
		{
			return this.taskStep;
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0004FF2C File Offset: 0x0004E12C
	public override bool IsComplete
	{
		get
		{
			return this.taskStep >= this.MaxStep;
		}
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0004FF40 File Offset: 0x0004E140
	public override void Initialize()
	{
		if (this.Arrow && !base.Owner.AmOwner)
		{
			this.Arrow.gameObject.SetActive(false);
		}
		this.HasLocation = true;
		this.LocationDirty = true;
		TaskTypes taskType = this.TaskType;
		if (taskType <= TaskTypes.EnterIdCode)
		{
			switch (taskType)
			{
			case TaskTypes.PrimeShields:
			{
				this.Data = new byte[1];
				int num = 0;
				for (int i = 0; i < 7; i++)
				{
					byte b = (byte)(1 << i);
					if (BoolRange.Next(0.7f))
					{
						byte[] data = this.Data;
						int num2 = 0;
						data[num2] |= b;
						num++;
					}
				}
				byte[] data2 = this.Data;
				int num3 = 0;
				data2[num3] &= 118;
				return;
			}
			case TaskTypes.FuelEngines:
				this.Data = new byte[2];
				return;
			case TaskTypes.ChartCourse:
				this.Data = new byte[4];
				return;
			case TaskTypes.StartReactor:
				this.Data = new byte[6];
				return;
			case TaskTypes.SwipeCard:
			case TaskTypes.ClearAsteroids:
			case TaskTypes.UploadData:
			case TaskTypes.EmptyChute:
			case TaskTypes.EmptyGarbage:
				break;
			case TaskTypes.InspectSample:
				this.Data = new byte[2];
				return;
			case TaskTypes.AlignEngineOutput:
				this.Data = new byte[4];
				this.Data[0] = (byte)(IntRange.RandomSign() * IntRange.Next(25, 127) + 127);
				this.Data[1] = (byte)(IntRange.RandomSign() * IntRange.Next(25, 127) + 127);
				return;
			case TaskTypes.FixWiring:
			{
				this.Data = new byte[this.MaxStep];
				List<global::Console> list = (from t in ShipStatus.Instance.AllConsoles
				where t.TaskTypes.Contains(TaskTypes.FixWiring)
				select t).ToList<global::Console>();
				List<global::Console> list2 = new List<global::Console>(list);
				for (int j = 0; j < this.Data.Length; j++)
				{
					int index = list2.RandomIdx<global::Console>();
					this.Data[j] = (byte)list2[index].ConsoleId;
					list2.RemoveAt(index);
				}
				Array.Sort<byte>(this.Data);
				global::Console console = list.First((global::Console v) => v.ConsoleId == (int)this.Data[0]);
				this.StartAt = console.Room;
				break;
			}
			default:
				if (taskType != TaskTypes.EnterIdCode)
				{
					return;
				}
				this.Data = BitConverter.GetBytes(IntRange.Next(1, 99999));
				return;
			}
		}
		else
		{
			if (taskType == TaskTypes.WaterPlants)
			{
				this.Data = new byte[4];
				return;
			}
			switch (taskType)
			{
			case TaskTypes.OpenWaterways:
				this.Data = new byte[3];
				return;
			case TaskTypes.ReplaceWaterJug:
				this.Data = new byte[1];
				return;
			case TaskTypes.RepairDrill:
			case TaskTypes.AlignTelescope:
			case TaskTypes.RecordTemperature:
			case TaskTypes.RebootWifi:
			case TaskTypes.PolishRuby:
			case TaskTypes.MakeBurger:
			case TaskTypes.UnlockSafe:
			case TaskTypes.PutAwayPistols:
			case TaskTypes.DressMannequin:
				break;
			case TaskTypes.ResetBreakers:
			{
				this.Data = new byte[7];
				byte b2 = 0;
				while ((int)b2 < this.Data.Length)
				{
					this.Data[(int)b2] = b2;
					b2 += 1;
				}
				this.Data.Shuffle(0);
				return;
			}
			case TaskTypes.Decontaminate:
				this.Data = new byte[1];
				this.Data[0] = IntRange.NextByte(10, 30);
				return;
			case TaskTypes.SortRecords:
				this.Data = new byte[4];
				return;
			case TaskTypes.FixShower:
			{
				float value = BoolRange.Next(0.5f) ? FloatRange.Next(0f, 0.1f) : (1f - FloatRange.Next(0f, 0.1f));
				this.Data = BitConverter.GetBytes(value);
				return;
			}
			case TaskTypes.CleanToilet:
				this.Data = new byte[1];
				this.Data[0] = IntRange.NextByte(0, 4);
				return;
			case TaskTypes.PickUpTowels:
			{
				this.Data = new byte[8];
				int[] array = Enumerable.Range(0, 14).ToArray<int>();
				array.Shuffle(0);
				byte b3 = 0;
				while ((int)b3 < this.Data.Length)
				{
					this.Data[(int)b3] = (byte)array[(int)b3];
					b3 += 1;
				}
				return;
			}
			case TaskTypes.RewindTapes:
			{
				this.Data = new byte[8];
				float num4 = (float)(IntRange.Next(6, 18) * 3600 + IntRange.Next(0, 60) * 60 + IntRange.Next(0, 60));
				BitConverter.GetBytes(num4).CopyTo(this.Data, 0);
				BitConverter.GetBytes(num4 + (float)(IntRange.RandomSign() * (IntRange.Next(5, 7) * 60 + IntRange.Next(0, 60)))).CopyTo(this.Data, 4);
				return;
			}
			case TaskTypes.StartFans:
			{
				this.Data = new byte[4];
				byte b4 = 0;
				while ((int)b4 < this.Data.Length)
				{
					this.Data[(int)b4] = IntRange.NextByte(0, 4);
					b4 += 1;
				}
				this.Data[(int)IntRange.NextByte(0, 4)] = IntRange.NextByte(1, 4);
				return;
			}
			default:
				return;
			}
		}
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x000503CC File Offset: 0x0004E5CC
	public void NextStep()
	{
		this.taskStep++;
		this.UpdateArrow();
		if (this.taskStep >= this.MaxStep)
		{
			this.taskStep = this.MaxStep;
			if (PlayerControl.LocalPlayer)
			{
				if (DestroyableSingleton<HudManager>.InstanceExists)
				{
					DestroyableSingleton<HudManager>.Instance.ShowTaskComplete();
					StatsManager instance = StatsManager.Instance;
					uint num = instance.TasksCompleted;
					instance.TasksCompleted = num + 1U;
					if (PlayerTask.AllTasksCompleted(PlayerControl.LocalPlayer))
					{
						StatsManager instance2 = StatsManager.Instance;
						num = instance2.CompletedAllTasks;
						instance2.CompletedAllTasks = num + 1U;
					}
				}
				PlayerControl.LocalPlayer.RpcCompleteTask(base.Id);
				return;
			}
		}
		else if (this.ShowTaskStep && Constants.ShouldPlaySfx())
		{
			SoundManager.Instance.PlaySound(DestroyableSingleton<HudManager>.Instance.TaskUpdateSound, false, 1f);
		}
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x00050498 File Offset: 0x0004E698
	public virtual void UpdateArrow()
	{
		if (!this.Arrow)
		{
			return;
		}
		if (!base.Owner.AmOwner)
		{
			this.Arrow.gameObject.SetActive(false);
			return;
		}
		if (!this.IsComplete)
		{
			if (PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
			{
				this.arrowSuspended = true;
			}
			else
			{
				this.Arrow.gameObject.SetActive(true);
			}
			if (this.TaskType == TaskTypes.FixWiring)
			{
				global::Console console4 = base.FindSpecialConsole((global::Console c) => c.TaskTypes.Contains(TaskTypes.FixWiring) && c.ConsoleId == (int)this.Data[this.taskStep]);
				this.Arrow.target = console4.transform.position;
				this.StartAt = console4.Room;
			}
			else if (this.TaskType == TaskTypes.AlignEngineOutput)
			{
				if (AlignGame.IsSuccess(this.Data[2]))
				{
					this.Arrow.target = base.FindSpecialConsole((global::Console c) => c.TaskTypes.Contains(TaskTypes.AlignEngineOutput) && c.ConsoleId == 0).transform.position;
					this.StartAt = SystemTypes.UpperEngine;
				}
				else
				{
					this.Arrow.target = base.FindSpecialConsole((global::Console console) => console.TaskTypes.Contains(TaskTypes.AlignEngineOutput) && console.ConsoleId == 1).transform.position;
					this.StartAt = SystemTypes.LowerEngine;
				}
			}
			else if (this.TaskType == TaskTypes.StartFans)
			{
				global::Console console2 = base.FindSpecialConsole((global::Console c) => this.ValidConsole(c) && c.ConsoleId == 1);
				this.Arrow.target = console2.transform.position;
				this.StartAt = console2.Room;
			}
			else
			{
				global::Console console3 = base.FindObjectPos();
				if (console3)
				{
					this.Arrow.target = console3.transform.position;
					this.StartAt = console3.Room;
				}
			}
			this.LocationDirty = true;
			return;
		}
		this.Arrow.gameObject.SetActive(false);
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x00050678 File Offset: 0x0004E878
	protected virtual void FixedUpdate()
	{
		if (this.TimerStarted == NormalPlayerTask.TimerState.Started)
		{
			this.TaskTimer -= Time.deltaTime;
			if (this.TaskTimer <= 0f)
			{
				this.TaskTimer = 0f;
				this.TimerStarted = NormalPlayerTask.TimerState.Finished;
			}
		}
		if (this.Arrow)
		{
			if (this.Arrow.isActiveAndEnabled && PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
			{
				this.arrowSuspended = true;
				this.Arrow.gameObject.SetActive(false);
				return;
			}
			if (this.arrowSuspended && !PlayerTask.PlayerHasTaskOfType<IHudOverrideTask>(PlayerControl.LocalPlayer))
			{
				this.arrowSuspended = false;
				this.Arrow.gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0005072C File Offset: 0x0004E92C
	public override bool ValidConsole(global::Console console)
	{
		if (this.TaskType == TaskTypes.ResetBreakers)
		{
			if (!console.TaskTypes.Contains(TaskTypes.ResetBreakers))
			{
				return false;
			}
			int num = Array.IndexOf<byte>(this.Data, (byte)console.ConsoleId);
			return this.taskStep <= num;
		}
		else if (this.TaskType == TaskTypes.SortRecords)
		{
			if (!console.TaskTypes.Contains(this.TaskType))
			{
				return false;
			}
			int num2 = this.Data.IndexOf((byte b) => b != 0 && b != byte.MaxValue);
			if (num2 != -1)
			{
				return console.ConsoleId == (int)this.Data[num2];
			}
			return console.ConsoleId == 0;
		}
		else
		{
			if (this.TaskType == TaskTypes.CleanToilet)
			{
				return console.TaskTypes.Contains(this.TaskType) && console.ConsoleId == (int)this.Data[0];
			}
			if (this.TaskType == TaskTypes.EmptyGarbage)
			{
				return console.ValidTasks.Any((TaskSet set) => this.TaskType == set.taskType && set.taskStep.Contains(this.taskStep)) && ((this.taskStep == 0 && console.Room == this.StartAt) || this.taskStep == 1);
			}
			if (this.TaskType == TaskTypes.FixWiring)
			{
				return console.TaskTypes.Contains(this.TaskType) && console.ConsoleId == (int)this.Data[this.taskStep];
			}
			if (this.TaskType == TaskTypes.AlignEngineOutput)
			{
				return console.TaskTypes.Contains(this.TaskType) && console.ConsoleId == this.taskStep;
			}
			if (this.TaskType == TaskTypes.FuelEngines)
			{
				return console.ValidTasks.Any((TaskSet set) => this.TaskType == set.taskType && set.taskStep.Contains((int)this.Data[1]));
			}
			if (this.TaskType == TaskTypes.RecordTemperature)
			{
				return console.Room == this.StartAt && console.TaskTypes.Any((TaskTypes tt) => tt == this.TaskType);
			}
			return console.TaskTypes.Any((TaskTypes tt) => tt == this.TaskType) || console.ValidTasks.Any((TaskSet set) => this.TaskType == set.taskType && set.taskStep.Contains(this.taskStep));
		}
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0005093C File Offset: 0x0004EB3C
	public override void Complete()
	{
		this.taskStep = this.MaxStep;
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0005094C File Offset: 0x0004EB4C
	public override void AppendTaskText(StringBuilder sb)
	{
		bool flag = this.ShouldYellowText();
		if (flag)
		{
			if (this.IsComplete)
			{
				sb.Append("[00DD00FF]");
			}
			else
			{
				sb.Append("[FFFF00FF]");
			}
		}
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(this.StartAt));
		sb.Append(": ");
		sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(this.TaskType));
		if (this.ShowTaskTimer && this.TimerStarted == NormalPlayerTask.TimerState.Started)
		{
			sb.Append(" (");
			sb.Append(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.SecondsAbbv, new object[]
			{
				(int)this.TaskTimer
			}));
			sb.Append(")");
		}
		else if (this.ShowTaskStep)
		{
			sb.Append(" (");
			sb.Append(this.taskStep);
			sb.Append("/");
			sb.Append(this.MaxStep);
			sb.Append(")");
		}
		if (flag)
		{
			sb.Append("[]");
		}
		sb.AppendLine();
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x00050A71 File Offset: 0x0004EC71
	private bool ShouldYellowText()
	{
		return (this.TaskType == TaskTypes.FuelEngines && this.Data[1] > 0) || this.taskStep > 0 || this.TimerStarted > NormalPlayerTask.TimerState.NotStarted;
	}

	// Token: 0x04000E81 RID: 3713
	public int taskStep;

	// Token: 0x04000E82 RID: 3714
	public int MaxStep;

	// Token: 0x04000E83 RID: 3715
	public bool ShowTaskStep = true;

	// Token: 0x04000E84 RID: 3716
	public bool ShowTaskTimer;

	// Token: 0x04000E85 RID: 3717
	public NormalPlayerTask.TimerState TimerStarted;

	// Token: 0x04000E86 RID: 3718
	public float TaskTimer;

	// Token: 0x04000E87 RID: 3719
	public byte[] Data;

	// Token: 0x04000E88 RID: 3720
	public ArrowBehaviour Arrow;

	// Token: 0x04000E89 RID: 3721
	protected bool arrowSuspended;

	// Token: 0x02000454 RID: 1108
	public enum TimerState
	{
		// Token: 0x04001C62 RID: 7266
		NotStarted,
		// Token: 0x04001C63 RID: 7267
		Started,
		// Token: 0x04001C64 RID: 7268
		Finished
	}
}
