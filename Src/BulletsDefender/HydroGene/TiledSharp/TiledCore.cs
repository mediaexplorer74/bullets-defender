// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxDocument
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
    public class TmxDocument
    {
        public string TmxDirectory { get; private set; }

        protected XDocument ReadXml(string filepath)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string[] array = new string[0];
            if (entryAssembly != (Assembly)null)
                array = entryAssembly.GetManifestResourceNames();
            string fileResPath = filepath.Replace(Path.DirectorySeparatorChar.ToString(), ".");
            string name = Array.Find<string>(array, (Predicate<string>)(s => s.EndsWith(fileResPath)));
            XDocument xdocument;
            if (name != null)
            {
                using (Stream manifestResourceStream = entryAssembly.GetManifestResourceStream(name))
                {
                    using (XmlReader reader = XmlReader.Create(manifestResourceStream))
                        xdocument = XDocument.Load(reader);
                }
                this.TmxDirectory = string.Empty;
            }
            else
            {
                xdocument = XDocument.Load(filepath);
                this.TmxDirectory = Path.GetDirectoryName(filepath);
            }
            return xdocument;
        }
    }
}
