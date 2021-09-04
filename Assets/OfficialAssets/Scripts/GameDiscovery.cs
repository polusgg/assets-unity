using System;
using System.Collections.Generic;
using System.Linq;
using Hazel.Udp;
using InnerNet;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000114 RID: 276
public class GameDiscovery : MonoBehaviour
{
	// Token: 0x060006D2 RID: 1746 RVA: 0x0002B712 File Offset: 0x00029912
	public void Start()
	{
		InnerDiscover component = base.GetComponent<InnerDiscover>();
		component.OnPacketGet += this.Receive;
		component.StartAsClient();
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0002B734 File Offset: 0x00029934
	public void Update()
	{
		//float time = Time.time;
		//string[] array = this.received.Keys.ToArray<string>();
		//Vector3 vector;
		//vector..ctor(0f, this.YStart, -1f);
		//foreach (string key in array)
		//{
		//	JoinGameButton joinGameButton = this.received[key];
		//	if (time - joinGameButton.timeRecieved > 3f)
		//	{
		//		this.received.Remove(key);
		//		Object.Destroy(joinGameButton.gameObject);
		//	}
		//	else
		//	{
		//		joinGameButton.transform.localPosition = vector;
		//		vector.y += this.YOffset;
		//	}
		//}
		//this.TargetArea.YBounds.max = Mathf.Max(0f, -vector.y - 2f * this.YStart);
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0002B80C File Offset: 0x00029A0C
	private void Receive(BroadcastPacket packet)
	{
		string[] array = packet.Data.Split(new char[]
		{
			'~'
		});
		string address = packet.GetAddress();
		JoinGameButton joinGameButton;
		if (this.received.TryGetValue(address, out joinGameButton))
		{
			joinGameButton.timeRecieved = Time.time;
			joinGameButton.SetGameName(array);
			return;
		}
		if (array[1].Equals("Open"))
		{
			this.CreateButtonForAddess(address, array);
		}
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0002B874 File Offset: 0x00029A74
	private void CreateButtonForAddess(string fromAddress, string[] gameNameParts)
	{
		JoinGameButton joinGameButton = null;
		bool flag = false;
		if (!this.received.TryGetValue(fromAddress, out joinGameButton))
		{
			joinGameButton = Object.Instantiate<JoinGameButton>(this.ButtonPrefab, this.ItemLocation);
			flag = true;
			Debug.Log("GameDiscovery.CreateButtonForAddess: Instantiate(" + fromAddress + ")");
		}
		if (flag)
		{
			joinGameButton.transform.localPosition = new Vector3(0f, this.YStart + (float)(this.ItemLocation.childCount - 1) * this.YOffset, -1f);
			joinGameButton.netAddress = fromAddress;
			joinGameButton.GetComponentInChildren<MeshRenderer>().material.SetInt("_Mask", 4);
		}
		joinGameButton.timeRecieved = Time.time;
		joinGameButton.SetGameName(gameNameParts);
		if (flag)
		{
			ControllerManager.Instance.AddSelectableUiElement(joinGameButton.GetComponent<PassiveButton>(), false);
		}
		this.received[fromAddress] = joinGameButton;
	}

	// Token: 0x0400079B RID: 1947
	public JoinGameButton ButtonPrefab;

	// Token: 0x0400079C RID: 1948
	public Transform ItemLocation;

	// Token: 0x0400079D RID: 1949
	public float YStart = 0.56f;

	// Token: 0x0400079E RID: 1950
	public float YOffset = -0.75f;

	// Token: 0x0400079F RID: 1951
	public Scroller TargetArea;

	// Token: 0x040007A0 RID: 1952
	private Dictionary<string, JoinGameButton> received = new Dictionary<string, JoinGameButton>();
}
