using System;
using System.Runtime.CompilerServices;

// Token: 0x02000164 RID: 356
public static class PEW
{
	// Token: 0x020003DC RID: 988
	public static class Hardware
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00075BB6 File Offset: 0x00073DB6
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x00075BBD File Offset: 0x00073DBD
		public static PEW.Hardware.HardwareType hardwareType
		{
			get
			{
				return PEW.Hardware.type;
			}
			private set
			{
				PEW.Hardware.type = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00075BC5 File Offset: 0x00073DC5
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00075BCC File Offset: 0x00073DCC
		public static int hardwareTier
		{
			get
			{
				return PEW.Hardware.tier;
			}
			private set
			{
				PEW.Hardware.tier = value;
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00075BD4 File Offset: 0x00073DD4
		private static PEW.Hardware.HardwareType GetHardwareType()
		{
			return PEW.Hardware.HardwareType.Unknown;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00075BD7 File Offset: 0x00073DD7
		private static int GetHardwareTier()
		{
			return 4;
		}

		// Token: 0x04001ABC RID: 6844
		private static PEW.Hardware.HardwareType type = PEW.Hardware.GetHardwareType();

		// Token: 0x04001ABD RID: 6845
		private static int tier = PEW.Hardware.GetHardwareTier();

		// Token: 0x020004BA RID: 1210
		public enum HardwareType
		{
			// Token: 0x04001DA0 RID: 7584
			PC,
			// Token: 0x04001DA1 RID: 7585
			Switch,
			// Token: 0x04001DA2 RID: 7586
			XboxOne,
			// Token: 0x04001DA3 RID: 7587
			PS4,
			// Token: 0x04001DA4 RID: 7588
			Stadia,
			// Token: 0x04001DA5 RID: 7589
			Unknown
		}
	}

	// Token: 0x020003DD RID: 989
	[Obsolete("\nDon't reference this class directly, create a class that derives from it! Unity won't properly serialize classes with generics.\n\npublic class ConditionalWhatever : ConditionalValue<Whatever> { }; \n\n(Ignore if you're already doing that)")]
	public class ConditionalValue<T>
	{
		// Token: 0x060018D5 RID: 6357 RVA: 0x00075BF0 File Offset: 0x00073DF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Select()
		{
			return this.PC;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00075BF8 File Offset: 0x00073DF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator T(PEW.ConditionalValue<T> input)
		{
			return input.Select();
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00075C00 File Offset: 0x00073E00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator PEW.ConditionalValue<T>(T input)
		{
			return new PEW.ConditionalValue<T>
			{
				PC = input,
				Switch = input,
				XboxOne = input,
				XboxOneX = input,
				PS4 = input,
				PS4Pro = input,
				Stadia = input,
				Misc = input
			};
		}

		// Token: 0x04001ABE RID: 6846
		public T PC;

		// Token: 0x04001ABF RID: 6847
		public T Switch;

		// Token: 0x04001AC0 RID: 6848
		public T XboxOne;

		// Token: 0x04001AC1 RID: 6849
		public T XboxOneX;

		// Token: 0x04001AC2 RID: 6850
		public T PS4;

		// Token: 0x04001AC3 RID: 6851
		public T PS4Pro;

		// Token: 0x04001AC4 RID: 6852
		public T Stadia;

		// Token: 0x04001AC5 RID: 6853
		public T Misc;
	}
}
