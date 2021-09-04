using System;
using System.Collections;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Hazel;
using Hazel.Dtls;
using InnerNet;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class AuthManager : DestroyableSingleton<AuthManager>
{
	// Token: 0x06000195 RID: 405 RVA: 0x0000D0F1 File Offset: 0x0000B2F1
	public IEnumerator CoConnect(string targetIp, ushort targetPort)
	{
		//if (this.connection != null)
		//{
		//	this.connection.DataReceived -= this.Connection_DataReceived;
		//	this.connection.Disconnected -= this.Connection_Disconnected;
		//	this.connection.Dispose();
		//	this.connection = null;
		//}
		//IPAddress address = IPAddress.Parse(targetIp);
		//this.connection = new DtlsUnityConnection(new UnityLogger(), new IPEndPoint(address, (int)targetPort), 0);
		//this.connection.DataReceived += this.Connection_DataReceived;
		//this.connection.Disconnected += this.Connection_Disconnected;
		//X509Certificate2 certificate = new X509Certificate2(CryptoHelpers.DecodePEM("\r\n-----BEGIN CERTIFICATE-----\r\nMIIDbTCCAlWgAwIBAgIUf8xD1G/d5NK1MTjQAYGqd1AmBvcwDQYJKoZIhvcNAQEL\r\nBQAwRTELMAkGA1UEBhMCQVUxEzARBgNVBAgMClNvbWUtU3RhdGUxITAfBgNVBAoM\r\nGEludGVybmV0IFdpZGdpdHMgUHR5IEx0ZDAgFw0yMTAyMDIxNzE4MDFaGA8yMjk0\r\nMTExODE3MTgwMVowRTELMAkGA1UEBhMCQVUxEzARBgNVBAgMClNvbWUtU3RhdGUx\r\nITAfBgNVBAoMGEludGVybmV0IFdpZGdpdHMgUHR5IEx0ZDCCASIwDQYJKoZIhvcN\r\nAQEBBQADggEPADCCAQoCggEBAL7GFDbZdXwPYXeHWRi2GfAXkaLCgxuSADfa1pI2\r\nvJkvgMTK1miSt3jNSg/o6VsjSOSL461nYmGCF6Ho3fMhnefOhKaaWu0VxF0GR1bd\r\ne836YWzhWINQRwmoVD/Wx1NUjLRlTa8g/W3eE5NZFkWI70VOPRJpR9SqjNHwtPbm\r\nKi41PVgJIc3m/7cKOEMrMYNYoc6E9ehwLdJLQ5olJXnMoGjHo2d59hC8KW2V1dY9\r\nsacNPUjbFZRWeQ0eJ7kbn8m3a5EuF34VEC7DFcP4NCWWI7HO5/KYE+mUNn0qxgua\r\nr32qFnoaKZr9dXWRWJSm2XecBgqQmeF/90gdbohNNHGC/iMCAwEAAaNTMFEwHQYD\r\nVR0OBBYEFAJAdUS5AZE3U3SPQoG06Ahq3wBbMB8GA1UdIwQYMBaAFAJAdUS5AZE3\r\nU3SPQoG06Ahq3wBbMA8GA1UdEwEB/wQFMAMBAf8wDQYJKoZIhvcNAQELBQADggEB\r\nALUoaAEuJf4kQ1bYVA2ax2QipkUM8PL9zoNiDjUw6ZlwMFi++XCQm8XDap45aaeZ\r\nMnXGBqIBWElezoH6BNSbdGwci/ZhxXHG/qdHm7zfCTNaLBe2+sZkGic1x6bZPFtK\r\nZUjGy7LmxsXOxqGMgPhAV4JbN1+LTmOkOutfHiXKe4Z1zu09mOo9sWfGCkbIyERX\r\nQQILBYSIkg3hU4R4xMOjvxcDrOZja6fSNyi2sgidTfe5OCKC2ovU7OmsQqzb7mFv\r\ne+7kpIUp6AZNc49n6GWtGeOoL7JUAqMOIO+R++YQN7/dgaGDPuu0PpmgI2gPLNW1\r\nZwHJ755zQQRX528xg9vfykY=\r\n-----END CERTIFICATE-----\r\n"));
		//X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
		//x509Certificate2Collection.Add(certificate);
		//this.connection.SetValidServerCertificates(x509Certificate2Collection);
		//this.connection.ConnectAsync(this.BuildData());
		//while (this.connection != null && this.connection.State == 1)
		//{
		//	yield return null;
		//}
		yield break;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000D10E File Offset: 0x0000B30E
	public IEnumerator CoWaitForNonce(float timeout = 5f)
	{
		//for (float timer = 0f; timer < timeout; timer += Time.deltaTime)
		//{
		//	if (this.connection == null || this.connection.State != 2)
		//	{
		//		yield break;
		//	}
		//	if (this.LastNonceReceived != null)
		//	{
		//		yield break;
		//	}
		//	yield return null;
		//}
		yield break;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000D124 File Offset: 0x0000B324
	private void FixedUpdate()
	{
		DtlsUnityConnection dtlsUnityConnection = this.connection;
		if (dtlsUnityConnection != null)
		{
			dtlsUnityConnection.FixedUpdate();
		}
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000D141 File Offset: 0x0000B341
	public override void OnDestroy()
	{
		if (this.connection != null)
		{
			this.connection.Dispose();
			this.connection = null;
		}
		base.OnDestroy();
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000D163 File Offset: 0x0000B363
	private void Connection_Disconnected(object sender, DisconnectedEventArgs e)
	{
		this.connection.Dispose();
		this.connection = null;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000D178 File Offset: 0x0000B378
	private void Connection_DataReceived(DataReceivedEventArgs obj)
	{
		MessageReader message = obj.Message;
		try
		{
			MessageReader messageReader = message.ReadMessage();
			if (messageReader.Tag == 1)
			{
				this.LastNonceReceived = new uint?(messageReader.ReadUInt32());
				this.connection.Disconnect("Job done", null);
			}
		}
		finally
		{
			message.Recycle();
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
	private byte[] BuildData()
	{
		MessageWriter messageWriter = MessageWriter.Get(0);
		messageWriter.Write(Constants.GetBroadcastVersion());
		messageWriter.Write((byte)Constants.GetPlatformType());
		//messageWri/*ter.Write(EOSManager.Instance*/.ClientId ?? "");
		return messageWriter.ToByteArray(false);
	}

	// Token: 0x0400026B RID: 619
	private const string ServerCertification = "\r\n-----BEGIN CERTIFICATE-----\r\nMIIDbTCCAlWgAwIBAgIUf8xD1G/d5NK1MTjQAYGqd1AmBvcwDQYJKoZIhvcNAQEL\r\nBQAwRTELMAkGA1UEBhMCQVUxEzARBgNVBAgMClNvbWUtU3RhdGUxITAfBgNVBAoM\r\nGEludGVybmV0IFdpZGdpdHMgUHR5IEx0ZDAgFw0yMTAyMDIxNzE4MDFaGA8yMjk0\r\nMTExODE3MTgwMVowRTELMAkGA1UEBhMCQVUxEzARBgNVBAgMClNvbWUtU3RhdGUx\r\nITAfBgNVBAoMGEludGVybmV0IFdpZGdpdHMgUHR5IEx0ZDCCASIwDQYJKoZIhvcN\r\nAQEBBQADggEPADCCAQoCggEBAL7GFDbZdXwPYXeHWRi2GfAXkaLCgxuSADfa1pI2\r\nvJkvgMTK1miSt3jNSg/o6VsjSOSL461nYmGCF6Ho3fMhnefOhKaaWu0VxF0GR1bd\r\ne836YWzhWINQRwmoVD/Wx1NUjLRlTa8g/W3eE5NZFkWI70VOPRJpR9SqjNHwtPbm\r\nKi41PVgJIc3m/7cKOEMrMYNYoc6E9ehwLdJLQ5olJXnMoGjHo2d59hC8KW2V1dY9\r\nsacNPUjbFZRWeQ0eJ7kbn8m3a5EuF34VEC7DFcP4NCWWI7HO5/KYE+mUNn0qxgua\r\nr32qFnoaKZr9dXWRWJSm2XecBgqQmeF/90gdbohNNHGC/iMCAwEAAaNTMFEwHQYD\r\nVR0OBBYEFAJAdUS5AZE3U3SPQoG06Ahq3wBbMB8GA1UdIwQYMBaAFAJAdUS5AZE3\r\nU3SPQoG06Ahq3wBbMA8GA1UdEwEB/wQFMAMBAf8wDQYJKoZIhvcNAQELBQADggEB\r\nALUoaAEuJf4kQ1bYVA2ax2QipkUM8PL9zoNiDjUw6ZlwMFi++XCQm8XDap45aaeZ\r\nMnXGBqIBWElezoH6BNSbdGwci/ZhxXHG/qdHm7zfCTNaLBe2+sZkGic1x6bZPFtK\r\nZUjGy7LmxsXOxqGMgPhAV4JbN1+LTmOkOutfHiXKe4Z1zu09mOo9sWfGCkbIyERX\r\nQQILBYSIkg3hU4R4xMOjvxcDrOZja6fSNyi2sgidTfe5OCKC2ovU7OmsQqzb7mFv\r\ne+7kpIUp6AZNc49n6GWtGeOoL7JUAqMOIO+R++YQN7/dgaGDPuu0PpmgI2gPLNW1\r\nZwHJ755zQQRX528xg9vfykY=\r\n-----END CERTIFICATE-----\r\n";

	// Token: 0x0400026C RID: 620
	private const byte AuthTagNonceMessage = 1;

	// Token: 0x0400026D RID: 621
	public DtlsUnityConnection connection;

	// Token: 0x0400026E RID: 622
	public uint? LastNonceReceived;
}
