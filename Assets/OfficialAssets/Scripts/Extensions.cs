using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x0200004E RID: 78
public static class Extensions
{
	// Token: 0x06000202 RID: 514 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
	public static string RemoveAll(this string self, params char[] chars)
	{
		StringBuilder stringBuilder = new StringBuilder(self.Length);
		foreach (char value in self)
		{
			if (!chars.Contains(value))
			{
				stringBuilder.Append(value);
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000EB44 File Offset: 0x0000CD44
	public static void TrimEnd(this StringBuilder self)
	{
		for (int i = self.Length - 1; i >= 0; i--)
		{
			char c = self[i];
			if (c != ' ' && c != '\t' && c != '\n' && c != '\r')
			{
				break;
			}
			int length = self.Length;
			self.Length = length - 1;
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0000EB90 File Offset: 0x0000CD90
	public static void DestroyAll<T>(this IList<T> self) where T : MonoBehaviour
	{
		for (int i = 0; i < self.Count; i++)
		{
            UnityEngine.Object.Destroy(self[i].gameObject);
		}
		self.Clear();
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000EBCA File Offset: 0x0000CDCA
	public static void AddUnique<T>(this IList<T> self, T item)
	{
		if (!self.Contains(item))
		{
			self.Add(item);
		}
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000EBDC File Offset: 0x0000CDDC
	public static string ToTextColor(this Color c)
	{
		return string.Concat(new string[]
		{
			"",
			Extensions.ByteHex[(int)((byte)(c.r * 255f))],
			Extensions.ByteHex[(int)((byte)(c.g * 255f))],
			Extensions.ByteHex[(int)((byte)(c.b * 255f))],
			Extensions.ByteHex[(int)((byte)(c.a * 255f))],
			"]"
		});
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000EC5C File Offset: 0x0000CE5C
	public static int ToInteger(this Color c, bool alpha)
	{
		if (alpha)
		{
			return (int)((byte)(c.r * 256f)) << 24 | (int)((byte)(c.g * 256f)) << 16 | (int)((byte)(c.b * 256f)) << 8 | (int)((byte)(c.a * 256f));
		}
		return (int)((byte)(c.r * 256f)) << 16 | (int)((byte)(c.g * 256f)) << 8 | (int)((byte)(c.b * 256f));
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000ECDA File Offset: 0x0000CEDA
	public static bool HasAnyBit(this int self, int bit)
	{
		return (self & bit) != 0;
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000ECE2 File Offset: 0x0000CEE2
	public static bool HasAnyBit(this byte self, byte bit)
	{
		return (self & bit) > 0;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000ECEA File Offset: 0x0000CEEA
	public static bool HasAnyBit(this ushort self, byte bit)
	{
		return (self & (ushort)bit) > 0;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000ECF2 File Offset: 0x0000CEF2
	public static bool HasBit(this byte self, byte bit)
	{
		return (self & bit) == bit;
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000ECFC File Offset: 0x0000CEFC
	public static int BitCount(this byte self)
	{
		int num = 0;
		for (int i = 0; i < 8; i++)
		{
			if ((1 << i & (int)self) != 0)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000ED28 File Offset: 0x0000CF28
	public static int IndexOf<T>(this T[] self, T item) where T : class
	{
		for (int i = 0; i < self.Length; i++)
		{
			if (self[i] == item)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000ED5C File Offset: 0x0000CF5C
	public static int IndexOfMin<T>(this T[] self, Func<T, float> comparer)
	{
		float num = float.MaxValue;
		int result = -1;
		for (int i = 0; i < self.Length; i++)
		{
			float num2 = comparer(self[i]);
			if (num2 <= num)
			{
				result = i;
				num = num2;
			}
		}
		return result;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000ED98 File Offset: 0x0000CF98
	public static int IndexOfMax<T>(this T[] self, Func<T, int> comparer, out bool tie)
	{
		tie = false;
		int num = int.MinValue;
		int result = -1;
		for (int i = 0; i < self.Length; i++)
		{
			int num2 = comparer(self[i]);
			if (num2 > num)
			{
				result = i;
				num = num2;
				tie = false;
			}
			else if (num2 == num)
			{
				tie = true;
				result = -1;
			}
		}
		return result;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
	public static void SetAll<T>(this IList<T> self, T value)
	{
		for (int i = 0; i < self.Count; i++)
		{
			self[i] = value;
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000EE0C File Offset: 0x0000D00C
	public static void AddAll<T>(this List<T> self, IList<T> other)
	{
		int num = self.Count + other.Count;
		if (self.Capacity < num)
		{
			self.Capacity = num;
		}
		for (int i = 0; i < other.Count; i++)
		{
			self.Add(other[i]);
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000EE58 File Offset: 0x0000D058
	public static void RemoveDupes<T>(this IList<T> self) where T : class
	{
		for (int i = 0; i < self.Count; i++)
		{
			T t = self[i];
			for (int j = self.Count - 1; j > i; j--)
			{
				if (self[j] == t)
				{
					self.RemoveAt(j);
				}
			}
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000EEAC File Offset: 0x0000D0AC
	public static IList<T> Shuffle<T>(this IList<T> self, int startAt = 0)
	{
		for (int i = startAt; i < self.Count - 1; i++)
		{
			T value = self[i];
			int index = UnityEngine.Random.Range(i, self.Count);
			self[i] = self[index];
			self[index] = value;
		}

		return self;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
	public static void Shuffle<T>(this System.Random r, IList<T> self)
	{
		for (int i = 0; i < self.Count; i++)
		{
			T value = self[i];
			int index = r.Next(self.Count);
			self[i] = self[index];
			self[index] = value;
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000EF44 File Offset: 0x0000D144
	public static T[] RandomSet<T>(this IList<T> self, int length)
	{
		T[] array = new T[length];
		self.RandomFill(array);
		return array;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000EF60 File Offset: 0x0000D160
	public static void RandomFill<T>(this IList<T> self, T[] target)
	{
		HashSet<int> hashSet = new HashSet<int>();
		for (int i = 0; i < target.Length; i++)
		{
			int num;
			do
			{
				num = self.RandomIdx<T>();
			}
			while (hashSet.Contains(num));
			target[i] = self[num];
			hashSet.Add(num);
			if (hashSet.Count == self.Count)
			{
				return;
			}
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000EFB6 File Offset: 0x0000D1B6
	public static int RandomIdx<T>(this IList<T> self)
	{
		return UnityEngine.Random.Range(0, self.Count);
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
	public static int RandomIdx<T>(this IEnumerable<T> self)
	{
		return UnityEngine.Random.Range(0, self.Count<T>());
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000EFD2 File Offset: 0x0000D1D2
	public static T Random<T>(this IEnumerable<T> self)
	{
		return self.ToArray<T>().Random<T>();
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
	public static T Random<T>(this IList<T> self)
	{
		if (self.Count > 0)
		{
			return self[UnityEngine.Random.Range(0, self.Count)];
		}
		return default(T);
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000F012 File Offset: 0x0000D212
	public static Vector2 Div(this Vector2 a, Vector2 b)
	{
		return new Vector2(a.x / b.x, a.y / b.y);
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000F033 File Offset: 0x0000D233
	public static Vector2 Mul(this Vector2 a, Vector2 b)
	{
		return new Vector2(a.x * b.x, a.y * b.y);
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000F054 File Offset: 0x0000D254
	public static Vector3 Mul(this Vector3 a, Vector3 b)
	{
		return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000F082 File Offset: 0x0000D282
	public static Vector3 Inv(this Vector3 a)
	{
		return new Vector3(1f / a.x, 1f / a.y, 1f / a.z);
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
	public static Rect Lerp(this Rect source, Rect target, float t)
	{
		Rect result = default(Rect);
		result.position = Vector2.Lerp(source.position, target.position, t);
		result.size = Vector2.Lerp(source.size, target.size, t);
		return result;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000F0FC File Offset: 0x0000D2FC
	public static void ForEach<T>(this IList<T> self, Action<T> todo)
	{
		for (int i = 0; i < self.Count; i++)
		{
			todo(self[i]);
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000F128 File Offset: 0x0000D328
	public static T Max<T>(this IList<T> self, Func<T, float> comparer)
	{
		T t = self.First<T>();
		float num = comparer(t);
		for (int i = 0; i < self.Count; i++)
		{
			T t2 = self[i];
			float num2 = comparer(t2);
			if (num < num2 || (num == num2 && UnityEngine.Random.value > 0.5f))
			{
				num = num2;
				t = t2;
			}
		}
		return t;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000F184 File Offset: 0x0000D384
	public static T Max<T>(this IList<T> self, Func<T, decimal> comparer)
	{
		T t = self.First<T>();
		decimal d = comparer(t);
		for (int i = 0; i < self.Count; i++)
		{
			T t2 = self[i];
			decimal num = comparer(t2);
			if (d < num || (d == num && UnityEngine.Random.value > 0.5f))
			{
				d = num;
				t = t2;
			}
		}
		return t;
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
	public static int Wrap(this int self, int max)
	{
		if (self >= 0)
		{
			return self % max;
		}
		return (self + -(self / max) * max + max) % max;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000F200 File Offset: 0x0000D400
	public static int LastIndexOf<T>(this T[] self, Predicate<T> pred)
	{
		for (int i = self.Length - 1; i > -1; i--)
		{
			if (pred(self[i]))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000F230 File Offset: 0x0000D430
	public static int IndexOf<T>(this T[] self, Predicate<T> pred)
	{
		for (int i = 0; i < self.Length; i++)
		{
			if (pred(self[i]))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000F260 File Offset: 0x0000D460
	public static Vector2 MapToRectangle(this Vector2 del, Vector2 widthAndHeight)
	{
		del = del.normalized;
		if (Mathf.Abs(del.x) > Mathf.Abs(del.y))
		{
			return new Vector2(Mathf.Sign(del.x) * widthAndHeight.x, del.y * widthAndHeight.y / 0.70710677f);
		}
		return new Vector2(del.x * widthAndHeight.x / 0.70710677f, Mathf.Sign(del.y) * widthAndHeight.y);
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000F2E3 File Offset: 0x0000D4E3
	public static float AngleSignedRad(this Vector2 vector1, Vector2 vector2)
	{
		return Mathf.Atan2(vector2.y, vector2.x) - Mathf.Atan2(vector1.y, vector1.x);
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000F308 File Offset: 0x0000D508
	public static float AngleSigned(this Vector2 vector1, Vector2 vector2)
	{
		return vector1.AngleSignedRad(vector2) * 57.29578f;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000F317 File Offset: 0x0000D517
	public static float AngleSigned(this Vector2 vector1)
	{
		return Mathf.Atan2(vector1.y, vector1.x);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000F32C File Offset: 0x0000D52C
	public static float WheelAngle(this Vector2 vector1, Vector2 vector2)
	{
		float num = vector1.AngleSignedRad(vector2) * 57.29578f;
		if (num > 180f)
		{
			num -= 360f;
		}
		if (num < -180f)
		{
			num += 360f;
		}
		return num;
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0000F368 File Offset: 0x0000D568
	public static Vector2 Rotate(this Vector2 self, float degrees)
	{
		float num = 0.017453292f * degrees;
		float num2 = Mathf.Cos(num);
		float num3 = Mathf.Sin(num);
		return new Vector2(self.x * num2 - num3 * self.y, self.x * num3 + num2 * self.y);
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
	public static Vector3 RotateZ(this Vector3 self, float degrees)
	{
		float num = 0.017453292f * degrees;
		float num2 = Mathf.Cos(num);
		float num3 = Mathf.Sin(num);
		return new Vector3(self.x * num2 - num3 * self.y, self.x * num3 + num2 * self.y, self.z);
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000F400 File Offset: 0x0000D600
	public static Vector3 RotateY(this Vector3 self, float degrees)
	{
		float num = 0.017453292f * degrees;
		float num2 = Mathf.Cos(num);
		float num3 = Mathf.Sin(num);
		return new Vector3(self.x * num2 - num3 * self.z, self.y, self.x * num3 + num2 * self.z);
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000F44E File Offset: 0x0000D64E
	public static bool TryToEnum<TEnum>(this string strEnumValue, out TEnum enumValue)
	{
		enumValue = default(TEnum);
		if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
		{
			return false;
		}
		enumValue = (TEnum)((object)Enum.Parse(typeof(TEnum), strEnumValue));
		return true;
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000F488 File Offset: 0x0000D688
	public static TEnum ToEnum<TEnum>(this string strEnumValue)
	{
		if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
		{
			return default(TEnum);
		}
		return (TEnum)((object)Enum.Parse(typeof(TEnum), strEnumValue));
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000F4C6 File Offset: 0x0000D6C6
	public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
	{
		if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
		{
			return defaultValue;
		}
		return (TEnum)((object)Enum.Parse(typeof(TEnum), strEnumValue));
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000F4F1 File Offset: 0x0000D6F1
	public static bool IsNullOrWhiteSpace(this string s)
	{
		if (s == null)
		{
			return true;
		}
		return !s.Any((char c) => !char.IsWhiteSpace(c));
	}

	// Token: 0x040002D9 RID: 729
	private static string[] ByteHex = (from x in Enumerable.Range(0, 256)
	select x.ToString("X2")).ToArray<string>();
}
