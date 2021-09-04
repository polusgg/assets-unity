using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E8 RID: 232
public class TextBox : MonoBehaviour, IFocusHolder
{
	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060005BE RID: 1470 RVA: 0x00025730 File Offset: 0x00023930
	public float TextHeight
	{
		get
		{
			return this.outputText.Height;
		}
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0002573D File Offset: 0x0002393D
	public void Start()
	{
		this.colliders = base.GetComponents<Collider2D>();
		DestroyableSingleton<PassiveButtonManager>.Instance.RegisterOne(this);
		if (this.Pipe)
		{
			this.Pipe.enabled = false;
		}
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0002576F File Offset: 0x0002396F
	public void OnDestroy()
	{
		if (this.keyboard != null)
		{
			this.keyboard.active = false;
			this.keyboard = null;
		}
		if (DestroyableSingleton<PassiveButtonManager>.InstanceExists)
		{
			DestroyableSingleton<PassiveButtonManager>.Instance.RemoveOne(this);
		}
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0002579E File Offset: 0x0002399E
	public void Clear()
	{
		this.SetText(string.Empty, string.Empty);
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x000257B0 File Offset: 0x000239B0
	public void Update()
	{
		//if (!this.hasFocus)
		//{
		//	return;
		//}
		//if (this.AllowPaste && (Input.GetKey(306) || Input.GetKey(305)) && Input.GetKeyDown(118))
		//{
		//	string clipboardString = ClipboardHelper.GetClipboardString();
		//	if (!string.IsNullOrWhiteSpace(clipboardString))
		//	{
		//		this.SetText(this.text + clipboardString, "");
		//	}
		//}
		//string inputString = Input.inputString;
		//if (inputString.Length > 0 || this.compoText != Input.compositionString)
		//{
		//	if (this.text == null || this.text == "Enter Name")
		//	{
		//		this.text = "";
		//	}
		//	this.SetText(this.text + inputString, Input.compositionString);
		//}
		//if (this.Pipe && this.hasFocus)
		//{
		//	this.pipeBlinkTimer += Time.deltaTime * 2f;
		//	this.Pipe.enabled = ((int)this.pipeBlinkTimer % 2 == 0);
		//}
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x000258B8 File Offset: 0x00023AB8
	public void GiveFocus()
	{
		Input.imeCompositionMode = (IMECompositionMode)1;
		if (this.hasFocus)
		{
			return;
		}
		if (this.ClearOnFocus)
		{
			this.text = string.Empty;
			this.compoText = string.Empty;
			this.outputText.Text = string.Empty;
		}
		this.hasFocus = true;
		if (TouchScreenKeyboard.isSupported)
		{
			this.keyboard = TouchScreenKeyboard.Open(this.text);
		}
		if (this.Background)
		{
			this.Background.color = Color.green;
		}
		this.pipeBlinkTimer = 0f;
		if (this.Pipe)
		{
			this.Pipe.transform.localPosition = this.outputText.CursorPos;
		}
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00025974 File Offset: 0x00023B74
	public void LoseFocus()
	{
		if (!this.hasFocus)
		{
			return;
		}
		Input.imeCompositionMode = (IMECompositionMode)2;
		if (this.compoText.Length > 0)
		{
			this.SetText(this.text + this.compoText, "");
			this.compoText = string.Empty;
		}
		this.hasFocus = false;
		if (this.keyboard != null)
		{
			this.keyboard.active = false;
			this.keyboard = null;
		}
		if (this.Background)
		{
			this.Background.color = Color.white;
		}
		if (this.Pipe)
		{
			this.Pipe.enabled = false;
		}
		this.OnFocusLost.Invoke();
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00025A28 File Offset: 0x00023C28
	public bool CheckCollision(Vector2 pt)
	{
		for (int i = 0; i < this.colliders.Length; i++)
		{
			if (this.colliders[i].OverlapPoint(pt))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00025A5C File Offset: 0x00023C5C
	public void SetText(string input, string inputCompo = "")
	{
		//bool flag = false;
		//char c = ' ';
		//this.tempTxt.Clear();
		//foreach (char c2 in input)
		//{
		//	if (c != ' ' || c2 != ' ')
		//	{
		//		if (c2 == '\r' || c2 == '\n')
		//		{
		//			flag = true;
		//		}
		//		if (c2 == '\b')
		//		{
		//			this.tempTxt.Length = Math.Max(this.tempTxt.Length - 1, 0);
		//		}
		//		if (this.ForceUppercase)
		//		{
		//			c2 = char.ToUpperInvariant(c2);
		//		}
		//		if (this.IsCharAllowed(c2))
		//		{
		//			this.tempTxt.Append(c2);
		//			c = c2;
		//		}
		//	}
		//}
		//if (!this.tempTxt.ToString().Equals(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.EnterName, Array.Empty<object>()), StringComparison.OrdinalIgnoreCase) && this.characterLimit > 0)
		//{
		//	this.tempTxt.Length = Math.Min(this.tempTxt.Length, this.characterLimit);
		//}
		//input = this.tempTxt.ToString();
		//if (!input.Equals(this.text) || !inputCompo.Equals(this.compoText))
		//{
		//	this.text = input;
		//	this.compoText = inputCompo;
		//	this.outputText.Text = this.text + "[FF0000FF]" + this.compoText + "[]";
		//	this.outputText.RefreshMesh();
		//	if (this.keyboard != null)
		//	{
		//		this.keyboard.text = this.text;
		//	}
		//	this.OnChange.Invoke();
		//}
		//if (flag)
		//{
		//	this.OnEnter.Invoke();
		//}
		//if (this.Pipe)
		//{
		//	this.Pipe.transform.localPosition = this.outputText.CursorPos;
		//}
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00025C08 File Offset: 0x00023E08
	public bool IsCharAllowed(char i)
	{
		if (this.IpMode)
		{
			return (i >= '0' && i <= '9') || i == '.';
		}
		return i == ' ' || (i >= 'A' && i <= 'Z') || (i >= 'a' && i <= 'z') || (i >= '0' && i <= '9') || (i >= 'À' && i <= 'ÿ') || (i >= 'Ѐ' && i <= 'џ') || (i >= '぀' && i <= '㆟') || (i >= 'ⱡ' && i <= '힣') || (this.AllowSymbols && TextBox.SymbolChars.Contains(i)) || (this.AllowEmail && TextBox.EmailChars.Contains(i));
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00025CD4 File Offset: 0x00023ED4
	public void AddText(string text)
	{
		if (this.text.Length > 0 && this.text[this.text.Length - 1] == ' ')
		{
			this.SetText(this.text + text + " ", "");
			return;
		}
		this.SetText(this.text + " " + text + " ", "");
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00025D49 File Offset: 0x00023F49
	public void Backspace()
	{
		this.SetText(this.text + "\b", "");
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00025D68 File Offset: 0x00023F68
	public void ClearLastWord()
	{
		if (this.text.Length > 2)
		{
			int num = this.text.Length - 2;
			while (num > 0 && (this.text[num] != ' ' || this.text[num + 1] == ' '))
			{
				num--;
			}
			this.SetText(this.text.Substring(0, num), "");
			return;
		}
		this.SetText("", "");
	}

	// Token: 0x04000664 RID: 1636
	public static readonly HashSet<char> SymbolChars = new HashSet<char>
	{
		'?',
		'!',
		',',
		'.',
		'\'',
		':',
		';',
		'(',
		')',
		'/',
		'\\',
		'%',
		'^',
		'&',
		'-',
		'='
	};

	// Token: 0x04000665 RID: 1637
	public static readonly HashSet<char> EmailChars = new HashSet<char>
	{
		'!',
		'~',
		'@',
		'.',
		'-',
		'_',
		'+'
	};

	// Token: 0x04000666 RID: 1638
	public bool allowAllCharacters;

	// Token: 0x04000667 RID: 1639
	public string text;

	// Token: 0x04000668 RID: 1640
	private string compoText = "";

	// Token: 0x04000669 RID: 1641
	public int characterLimit = -1;

	// Token: 0x0400066A RID: 1642
	[SerializeField]
	public TextRenderer outputText;

	// Token: 0x0400066B RID: 1643
	public SpriteRenderer Background;

	// Token: 0x0400066C RID: 1644
	public MeshRenderer Pipe;

	// Token: 0x0400066D RID: 1645
	private float pipeBlinkTimer;

	// Token: 0x0400066E RID: 1646
	public bool ClearOnFocus;

	// Token: 0x0400066F RID: 1647
	public bool ForceUppercase;

	// Token: 0x04000670 RID: 1648
	public Button.ButtonClickedEvent OnEnter;

	// Token: 0x04000671 RID: 1649
	public Button.ButtonClickedEvent OnChange;

	// Token: 0x04000672 RID: 1650
	public Button.ButtonClickedEvent OnFocusLost;

	// Token: 0x04000673 RID: 1651
	private TouchScreenKeyboard keyboard;

	// Token: 0x04000674 RID: 1652
	public bool AllowSymbols;

	// Token: 0x04000675 RID: 1653
	public bool AllowEmail;

	// Token: 0x04000676 RID: 1654
	public bool IpMode;

	// Token: 0x04000677 RID: 1655
	public bool AllowPaste;

	// Token: 0x04000678 RID: 1656
	private Collider2D[] colliders;

	// Token: 0x04000679 RID: 1657
	private bool hasFocus;

	// Token: 0x0400067A RID: 1658
	private StringBuilder tempTxt = new StringBuilder();
}
