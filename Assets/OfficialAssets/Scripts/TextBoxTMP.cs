using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000EA RID: 234
public class TextBoxTMP : MonoBehaviour, IFocusHolder
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00025F9C File Offset: 0x0002419C
	public float TextHeight
	{
		get
		{
			return this.outputText.GetNotDumbRenderedHeight();
		}
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00025FA9 File Offset: 0x000241A9
	public void Start()
	{
		this.colliders = base.GetComponents<Collider2D>();
		DestroyableSingleton<PassiveButtonManager>.Instance.RegisterOne(this);
		if (this.Pipe)
		{
			this.Pipe.enabled = false;
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00025FDB File Offset: 0x000241DB
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

	// Token: 0x060005D9 RID: 1497 RVA: 0x0002600A File Offset: 0x0002420A
	public void Clear()
	{
		this.SetText(string.Empty, string.Empty);
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0002601C File Offset: 0x0002421C
	public void Update()
	{
		//if (!base.enabled)
		//{
		//	return;
		//}
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

	// Token: 0x060005DB RID: 1499 RVA: 0x0002612C File Offset: 0x0002432C
	public void GiveFocus()
	{
		//if (!base.enabled)
		//{
		//	return;
		//}
		//Input.imeCompositionMode = 1;
		//if (this.hasFocus)
		//{
		//	return;
		//}
		//if (this.ClearOnFocus)
		//{
		//	this.text = string.Empty;
		//	this.compoText = string.Empty;
		//	this.outputText.text = string.Empty;
		//}
		//this.hasFocus = true;
		//if (TouchScreenKeyboard.isSupported)
		//{
		//	this.keyboard = TouchScreenKeyboard.Open(this.text);
		//}
		//if (this.Background)
		//{
		//	this.Background.color = Color.green;
		//}
		//this.pipeBlinkTimer = 0f;
		//if (this.Pipe)
		//{
		//	this.Pipe.transform.localPosition = this.outputText.CursorPos();
		//}
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x000261F4 File Offset: 0x000243F4
	public void LoseFocus()
	{
		//if (!this.hasFocus)
		//{
		//	return;
		//}
		//Input.imeCompositionMode = 2;
		//if (this.compoText.Length > 0)
		//{
		//	this.SetText(this.text + this.compoText, "");
		//	this.compoText = string.Empty;
		//}
		//this.hasFocus = false;
		//if (this.keyboard != null)
		//{
		//	this.keyboard.active = false;
		//	this.keyboard = null;
		//}
		//if (this.Background)
		//{
		//	this.Background.color = Color.white;
		//}
		//if (this.Pipe)
		//{
		//	this.Pipe.enabled = false;
		//}
		//this.OnFocusLost.Invoke();
		//this.sendButtonGlyph.enabled = true;
		//this.quickChatGlyph.enabled = false;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x000262C0 File Offset: 0x000244C0
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

	// Token: 0x060005DE RID: 1502 RVA: 0x000262F4 File Offset: 0x000244F4
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
		//	this.outputText.text = this.text + "<color=#FF0000>" + this.compoText + "</color>";
		//	this.outputText.ForceMeshUpdate(true, true);
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
		//	this.Pipe.transform.localPosition = this.outputText.CursorPos();
		//}
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x000264A4 File Offset: 0x000246A4
	public bool IsCharAllowed(char i)
	{
		if (this.IpMode)
		{
			return (i >= '0' && i <= '9') || i == '.';
		}
		return i == ' ' || (i >= 'A' && i <= 'Z') || (i >= 'a' && i <= 'z') || (i >= '0' && i <= '9') || (i >= 'À' && i <= 'ÿ') || (i >= 'Ѐ' && i <= 'џ') || (i >= '぀' && i <= '㆟') || (i >= 'ⱡ' && i <= '힣') || (this.AllowSymbols && TextBoxTMP.SymbolChars.Contains(i)) || (this.AllowEmail && TextBoxTMP.EmailChars.Contains(i));
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00026570 File Offset: 0x00024770
	public void AddText(string text)
	{
		if (this.text.Length > 0 && this.text[this.text.Length - 1] == ' ')
		{
			this.SetText(this.text + text + " ", "");
			return;
		}
		this.SetText(this.text + " " + text + " ", "");
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000265E5 File Offset: 0x000247E5
	public void Backspace()
	{
		this.SetText(this.text + "\b", "");
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00026604 File Offset: 0x00024804
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

	// Token: 0x0400067C RID: 1660
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

	// Token: 0x0400067D RID: 1661
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

	// Token: 0x0400067E RID: 1662
	public bool allowAllCharacters;

	// Token: 0x0400067F RID: 1663
	public string text;

	// Token: 0x04000680 RID: 1664
	private string compoText = "";

	// Token: 0x04000681 RID: 1665
	public int characterLimit = -1;

	// Token: 0x04000682 RID: 1666
	[SerializeField]
	public TextMeshPro outputText;

	// Token: 0x04000683 RID: 1667
	public SpriteRenderer Background;

	// Token: 0x04000684 RID: 1668
	public MeshRenderer Pipe;

	// Token: 0x04000685 RID: 1669
	private float pipeBlinkTimer;

	// Token: 0x04000686 RID: 1670
	public bool ClearOnFocus;

	// Token: 0x04000687 RID: 1671
	public bool ForceUppercase;

	// Token: 0x04000688 RID: 1672
	public Button.ButtonClickedEvent OnEnter;

	// Token: 0x04000689 RID: 1673
	public Button.ButtonClickedEvent OnChange;

	// Token: 0x0400068A RID: 1674
	public Button.ButtonClickedEvent OnFocusLost;

	// Token: 0x0400068B RID: 1675
	private TouchScreenKeyboard keyboard;

	// Token: 0x0400068C RID: 1676
	public bool AllowSymbols;

	// Token: 0x0400068D RID: 1677
	public bool AllowEmail;

	// Token: 0x0400068E RID: 1678
	public bool IpMode;

	// Token: 0x0400068F RID: 1679
	public bool AllowPaste;

	// Token: 0x04000690 RID: 1680
	private Collider2D[] colliders;

	// Token: 0x04000691 RID: 1681
	private bool hasFocus;

	// Token: 0x04000692 RID: 1682
	private StringBuilder tempTxt = new StringBuilder();

	// Token: 0x04000693 RID: 1683
	public SpriteRenderer sendButtonGlyph;

	// Token: 0x04000694 RID: 1684
	public SpriteRenderer quickChatGlyph;
}
