using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFormula_StAnalysis
{
    class History
    {
        public static List<string> RecentList = new List<string>();

        public static void GetRecentHistory()
        {
            try
            {
                String[] lines = File.ReadAllLines(AppData.AppPath + "Recent.txt");
                foreach (string line in lines)
                {
                    string l = line.Trim();
                    RecentList.Add(l);
                }
            }
            catch { }
        }

        public static void WriteRecentHistory()
        {
            try
            {
                File.WriteAllLines(AppData.AppPath + "Recent.txt",RecentList.ToArray());
            }
            catch { }
        }

        public static void AddToRecent(string Path,string Name)
        {
            if (!System.IO.File.Exists(Path)) return;
            foreach (string item in RecentList)
            {
                string path = item.Split(new char[] { '|' })[1];
                if (path.ToLower() == Path.ToLower())
                {
                    RecentList.Remove(item);
                    break;
                }
            }
            try
            {
                RecentList.Insert(0,Name + "|" + Path);
                WriteRecentHistory();
            }
            catch { }
        }


    }
}
