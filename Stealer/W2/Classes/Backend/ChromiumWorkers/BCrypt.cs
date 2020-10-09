using System;
using System.Runtime.InteropServices;

namespace ChromeRecovery
{
	// Token: 0x02000005 RID: 5
	public static class BCrypt//Infected-Zone.com
	{
		// Token: 0x06000013 RID: 19
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptOpenAlgorithmProvider(out IntPtr phAlgorithm, [MarshalAs(UnmanagedType.LPWStr)] string pszAlgId, [MarshalAs(UnmanagedType.LPWStr)] string pszImplementation, uint dwFlags);

		// Token: 0x06000014 RID: 20
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, uint flags);

		// Token: 0x06000015 RID: 21
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptGetProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbOutput, int cbOutput, ref int pcbResult, uint flags);

		// Token: 0x06000016 RID: 22
		[DllImport("bcrypt.dll", EntryPoint = "BCryptSetProperty")]
		internal static extern uint BCryptSetAlgorithmProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbInput, int cbInput, int dwFlags);

		// Token: 0x06000017 RID: 23
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptImportKey(IntPtr hAlgorithm, IntPtr hImportKey, [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType, out IntPtr phKey, IntPtr pbKeyObject, int cbKeyObject, byte[] pbInput, int cbInput, uint dwFlags);

		// Token: 0x06000018 RID: 24
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptDestroyKey(IntPtr hKey);

		// Token: 0x06000019 RID: 25
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptEncrypt(IntPtr hKey, byte[] pbInput, int cbInput, ref BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, int cbIV, byte[] pbOutput, int cbOutput, ref int pcbResult, uint dwFlags);

		// Token: 0x0600001A RID: 26
		[DllImport("bcrypt.dll")]
		internal static extern uint BCryptDecrypt(IntPtr hKey, byte[] pbInput, int cbInput, ref BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, int cbIV, byte[] pbOutput, int cbOutput, ref int pcbResult, int dwFlags);

		// Token: 0x04000005 RID: 5
		public const uint ERROR_SUCCESS = 0U;

		// Token: 0x04000006 RID: 6
		public const uint BCRYPT_PAD_PSS = 8U;

		// Token: 0x04000007 RID: 7
		public const uint BCRYPT_PAD_OAEP = 4U;

		// Token: 0x04000008 RID: 8
		public static readonly byte[] BCRYPT_KEY_DATA_BLOB_MAGIC = BitConverter.GetBytes(1296188491);

		// Token: 0x04000009 RID: 9
		public static readonly string BCRYPT_OBJECT_LENGTH = "ObjectLength";

		// Token: 0x0400000A RID: 10
		public static readonly string BCRYPT_CHAIN_MODE_GCM = "ChainingModeGCM";

		// Token: 0x0400000B RID: 11
		public static readonly string BCRYPT_AUTH_TAG_LENGTH = "AuthTagLength";

		// Token: 0x0400000C RID: 12
		public static readonly string BCRYPT_CHAINING_MODE = "ChainingMode";

		// Token: 0x0400000D RID: 13
		public static readonly string BCRYPT_KEY_DATA_BLOB = "KeyDataBlob";

		// Token: 0x0400000E RID: 14
		public static readonly string BCRYPT_AES_ALGORITHM = "AES";

		// Token: 0x0400000F RID: 15
		public static readonly string MS_PRIMITIVE_PROVIDER = "Microsoft Primitive Provider";

		// Token: 0x04000010 RID: 16
		public static readonly int BCRYPT_AUTH_MODE_CHAIN_CALLS_FLAG = 1;

		// Token: 0x04000011 RID: 17
		public static readonly int BCRYPT_INIT_AUTH_MODE_INFO_VERSION = 1;

		// Token: 0x04000012 RID: 18
		public static readonly uint STATUS_AUTH_TAG_MISMATCH = 3221266434U;

		// Token: 0x02000009 RID: 9
		public struct BCRYPT_PSS_PADDING_INFO
		{
			// Token: 0x06000030 RID: 48 RVA: 0x000044DD File Offset: 0x000026DD
			public BCRYPT_PSS_PADDING_INFO(string pszAlgId, int cbSalt)
			{
				this.pszAlgId = pszAlgId;
				this.cbSalt = cbSalt;
			}

			// Token: 0x0400001D RID: 29
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszAlgId;

			// Token: 0x0400001E RID: 30
			public int cbSalt;
		}

		// Token: 0x0200000A RID: 10
		public struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO : IDisposable
		{
			// Token: 0x06000031 RID: 49 RVA: 0x000044F0 File Offset: 0x000026F0
			public BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO(byte[] iv, byte[] aad, byte[] tag)
			{
				this = default(BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO);
				this.dwInfoVersion = BCrypt.BCRYPT_INIT_AUTH_MODE_INFO_VERSION;
				this.cbSize = Marshal.SizeOf(typeof(BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO));
				bool flag = iv != null;
				if (flag)
				{
					this.cbNonce = iv.Length;
					this.pbNonce = Marshal.AllocHGlobal(this.cbNonce);
					Marshal.Copy(iv, 0, this.pbNonce, this.cbNonce);
				}
				bool flag2 = aad != null;
				if (flag2)
				{
					this.cbAuthData = aad.Length;
					this.pbAuthData = Marshal.AllocHGlobal(this.cbAuthData);
					Marshal.Copy(aad, 0, this.pbAuthData, this.cbAuthData);
				}
				bool flag3 = tag != null;
				if (flag3)
				{
					this.cbTag = tag.Length;
					this.pbTag = Marshal.AllocHGlobal(this.cbTag);
					Marshal.Copy(tag, 0, this.pbTag, this.cbTag);
					this.cbMacContext = tag.Length;
					this.pbMacContext = Marshal.AllocHGlobal(this.cbMacContext);
				}
			}

			// Token: 0x06000032 RID: 50 RVA: 0x000045E8 File Offset: 0x000027E8
			public void Dispose()
			{
				bool flag = this.pbNonce != IntPtr.Zero;
				if (flag)
				{
					Marshal.FreeHGlobal(this.pbNonce);
				}
				bool flag2 = this.pbTag != IntPtr.Zero;
				if (flag2)
				{
					Marshal.FreeHGlobal(this.pbTag);
				}
				bool flag3 = this.pbAuthData != IntPtr.Zero;
				if (flag3)
				{
					Marshal.FreeHGlobal(this.pbAuthData);
				}
				bool flag4 = this.pbMacContext != IntPtr.Zero;
				if (flag4)
				{
					Marshal.FreeHGlobal(this.pbMacContext);
				}
			}

			// Token: 0x0400001F RID: 31
			public int cbSize;

			// Token: 0x04000020 RID: 32
			public int dwInfoVersion;

			// Token: 0x04000021 RID: 33
			public IntPtr pbNonce;

			// Token: 0x04000022 RID: 34
			public int cbNonce;

			// Token: 0x04000023 RID: 35
			public IntPtr pbAuthData;

			// Token: 0x04000024 RID: 36
			public int cbAuthData;

			// Token: 0x04000025 RID: 37
			public IntPtr pbTag;

			// Token: 0x04000026 RID: 38
			public int cbTag;

			// Token: 0x04000027 RID: 39
			public IntPtr pbMacContext;

			// Token: 0x04000028 RID: 40
			public int cbMacContext;

			// Token: 0x04000029 RID: 41
			public int cbAAD;

			// Token: 0x0400002A RID: 42
			public long cbData;

			// Token: 0x0400002B RID: 43
			public int dwFlags;
		}

		// Token: 0x0200000B RID: 11
		public struct BCRYPT_KEY_LENGTHS_STRUCT
		{
			// Token: 0x0400002C RID: 44
			public int dwMinLength;

			// Token: 0x0400002D RID: 45
			public int dwMaxLength;

			// Token: 0x0400002E RID: 46
			public int dwIncrement;
		}

		// Token: 0x0200000C RID: 12
		public struct BCRYPT_OAEP_PADDING_INFO
		{
			// Token: 0x06000033 RID: 51 RVA: 0x00004676 File Offset: 0x00002876
			public BCRYPT_OAEP_PADDING_INFO(string alg)
			{
				this.pszAlgId = alg;
				this.pbLabel = IntPtr.Zero;
				this.cbLabel = 0;
			}

			// Token: 0x0400002F RID: 47
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszAlgId;

			// Token: 0x04000030 RID: 48
			public IntPtr pbLabel;

			// Token: 0x04000031 RID: 49
			public int cbLabel;
		}
	}
}
