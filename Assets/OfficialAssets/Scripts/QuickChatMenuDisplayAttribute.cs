using System;

// Token: 0x0200018B RID: 395
[AttributeUsage(AttributeTargets.Field)]
public class QuickChatMenuDisplayAttribute : Attribute
{
	// Token: 0x060008F6 RID: 2294 RVA: 0x0003A6D1 File Offset: 0x000388D1
	public QuickChatMenuDisplayAttribute(QuickChatMenuItem.QuickChatMenuItemType displayType)
	{
		this.propertyMenuType = displayType;
	}

	// Token: 0x04000A61 RID: 2657
	public QuickChatMenuItem.QuickChatMenuItemType propertyMenuType;
}
