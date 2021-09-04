using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

namespace UnityEngine.SignInWithApple
{
	// Token: 0x02000253 RID: 595
	public class SignInWithApple : MonoBehaviour
	{
		// Token: 0x06000DC9 RID: 3529 RVA: 0x0005385C File Offset: 0x00051A5C
		[MonoPInvokeCallback(typeof(SignInWithApple.LoginCompleted))]
		private static void LoginCompletedCallback(int result, [MarshalAs(UnmanagedType.Struct)] UserInfo info)
		{
			SignInWithApple.CallbackArgs args = default(SignInWithApple.CallbackArgs);
			if (result != 0)
			{
				args.userInfo = new UserInfo
				{
					idToken = info.idToken,
					displayName = info.displayName,
					email = info.email,
					userId = info.userId,
					userDetectionStatus = info.userDetectionStatus
				};
			}
			else
			{
				args.error = info.error;
			}
			SignInWithApple.s_LoginCompletedCallback(args);
			SignInWithApple.s_LoginCompletedCallback = null;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000538E8 File Offset: 0x00051AE8
		[MonoPInvokeCallback(typeof(SignInWithApple.GetCredentialStateCompleted))]
		private static void GetCredentialStateCallback([MarshalAs(UnmanagedType.SysInt)] UserCredentialState state)
		{
			SignInWithApple.CallbackArgs args = new SignInWithApple.CallbackArgs
			{
				credentialState = state
			};
			SignInWithApple.s_CredentialStateCallback(args);
			SignInWithApple.s_CredentialStateCallback = null;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00053918 File Offset: 0x00051B18
		public void GetCredentialState(string userID)
		{
			this.GetCredentialState(userID, new SignInWithApple.Callback(this.TriggerCredentialStateEvent));
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0005392D File Offset: 0x00051B2D
		public void GetCredentialState(string userID, SignInWithApple.Callback callback)
		{
			if (userID == null)
			{
				throw new InvalidOperationException("Credential state fetch called without a user id.");
			}
			if (SignInWithApple.s_CredentialStateCallback != null)
			{
				throw new InvalidOperationException("Credential state fetch called while another request is in progress");
			}
			SignInWithApple.s_CredentialStateCallback = callback;
			this.GetCredentialStateInternal(userID);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0005395C File Offset: 0x00051B5C
		private void GetCredentialStateInternal(string userID)
		{
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0005395E File Offset: 0x00051B5E
		public void Login()
		{
			this.Login(new SignInWithApple.Callback(this.TriggerOnLoginEvent));
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00053972 File Offset: 0x00051B72
		public void Login(SignInWithApple.Callback callback)
		{
			if (SignInWithApple.s_LoginCompletedCallback != null)
			{
				throw new InvalidOperationException("Login called while another login is in progress");
			}
			SignInWithApple.s_LoginCompletedCallback = callback;
			this.LoginInternal();
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00053992 File Offset: 0x00051B92
		private void LoginInternal()
		{
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00053994 File Offset: 0x00051B94
		private void TriggerOnLoginEvent(SignInWithApple.CallbackArgs args)
		{
			if (args.error != null)
			{
				this.TriggerOnErrorEvent(args);
				return;
			}
			SignInWithApple.s_EventQueue.Enqueue(delegate
			{
				if (this.onLogin != null)
				{
					this.onLogin.Invoke(args);
				}
			});
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000539E8 File Offset: 0x00051BE8
		private void TriggerCredentialStateEvent(SignInWithApple.CallbackArgs args)
		{
			if (args.error != null)
			{
				this.TriggerOnErrorEvent(args);
				return;
			}
			SignInWithApple.s_EventQueue.Enqueue(delegate
			{
				if (this.onCredentialState != null)
				{
					this.onCredentialState.Invoke(args);
				}
			});
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00053A3C File Offset: 0x00051C3C
		private void TriggerOnErrorEvent(SignInWithApple.CallbackArgs args)
		{
			SignInWithApple.s_EventQueue.Enqueue(delegate
			{
				if (this.onError != null)
				{
					this.onError.Invoke(args);
				}
			});
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00053A73 File Offset: 0x00051C73
		public void Update()
		{
			while (SignInWithApple.s_EventQueue.Count > 0)
			{
				SignInWithApple.s_EventQueue.Dequeue()();
			}
		}

		// Token: 0x040011E1 RID: 4577
		private static SignInWithApple.Callback s_LoginCompletedCallback;

		// Token: 0x040011E2 RID: 4578
		private static SignInWithApple.Callback s_CredentialStateCallback;

		// Token: 0x040011E3 RID: 4579
		private static readonly Queue<Action> s_EventQueue = new Queue<Action>();

		// Token: 0x040011E4 RID: 4580
		[Header("Event fired when login is complete.")]
		public SignInWithAppleEvent onLogin;

		// Token: 0x040011E5 RID: 4581
		[Header("Event fired when the users credential state has been retrieved.")]
		public SignInWithAppleEvent onCredentialState;

		// Token: 0x040011E6 RID: 4582
		[Header("Event fired when there is an error.")]
		public SignInWithAppleEvent onError;

		// Token: 0x02000460 RID: 1120
		public struct CallbackArgs
		{
			// Token: 0x04001C86 RID: 7302
			public UserCredentialState credentialState;

			// Token: 0x04001C87 RID: 7303
			public UserInfo userInfo;

			// Token: 0x04001C88 RID: 7304
			public string error;
		}

		// Token: 0x02000461 RID: 1121
		// (Invoke) Token: 0x06001AAD RID: 6829
		public delegate void Callback(SignInWithApple.CallbackArgs args);

		// Token: 0x02000462 RID: 1122
		// (Invoke) Token: 0x06001AB1 RID: 6833
		private delegate void LoginCompleted(int result, UserInfo info);

		// Token: 0x02000463 RID: 1123
		// (Invoke) Token: 0x06001AB5 RID: 6837
		private delegate void GetCredentialStateCompleted(UserCredentialState state);
	}
}
