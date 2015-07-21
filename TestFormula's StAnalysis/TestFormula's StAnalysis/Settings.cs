using System;
using System.Windows;
using System.Windows.Resources;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TestFormula_StAnalysis
{
    class Settings
    {

        private static string Path = AppData.AppPath + "Settings.txt";

        public static Dictionary<string, string> Set = new Dictionary<string, string>();

        public static void Load()
        {

            try
            {
                XElement xElem2 = XElement.Load(Path); //XElement.Load(...)
                Set = xElem2.Descendants("item")
                                    .ToDictionary(x => (string)x.Attribute("id"), x => (string)x.Attribute("value"));
            }
            catch(FileNotFoundException ex){
                CopyDefaultSettingsFile();
                Console.Write(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
           
        }

        public static void Save()
        {
            //System.IO.StreamWriter stream = new System.IO.StreamWriter(Path,false);
            //XmlSerializer serializer = new XmlSerializer(typeof(item[]),
            //                        new XmlRootAttribute() { ElementName = "items" });
            //serializer.Serialize(stream,
            //  Set.Select(kv => new item() { id = kv.Key, value = kv.Value }).ToArray());
            //stream.Close();

            try
            {
                XElement xElem = new XElement("items",
                        Set.Select(x => new XElement("item", new XAttribute("id", x.Key), new XAttribute("value", x.Value))));
                var xml = xElem.ToString(); //xElem.Save(...);
                xElem.Save(Path);
            }
            catch (Exception ex) { }
        }

        public static void SetValue(string Key, string Value)
        {
            if (!Set.ContainsKey(Key))
            {
                Set.Add(Key, Value);
            }
            else
            {
                Set[Key] = Value;
            }
        }

        private static void CopyDefaultSettingsFile(){
            if (Directory.Exists(AppData.AppPath))
            {

            }
            if (!File.Exists(Path))
            {
                Uri uri = new Uri("AppData\\Settings.txt", UriKind.Relative);
                StreamResourceInfo sourceStream = Application.GetResourceStream(uri);
                StreamWriter s = new StreamWriter(Path);
                sourceStream.Stream.CopyTo(s.BaseStream);
                s.Close();
            }
            //File.Copy("\\AppData\\Settings.txt", Path);
        }

        public static void Apply()
        {
            return;
            string theme = Set["AppTheme"];

            string[] paths = new string[] { 
                "Themes/Theme" + theme + ".xaml",
                "Styles/StyleGeneral.xaml",
                "Values/ResValues.xaml"};

            Application.Current.Resources.MergedDictionaries.Clear();
            foreach (string path in paths)
            {
                Uri uri = new Uri(path, UriKind.Relative);
                ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(uri);
                Application.Current.Resources.MergedDictionaries.Add(res);
            }

        }

        public static string TryGetValue(string key,string defaultValue=""){
            if (Set.ContainsKey(key))
                return Set[key];
            return defaultValue;
        }
    }

}
