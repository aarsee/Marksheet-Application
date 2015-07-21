using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFormula_StAnalysis
{
    class AppData
    {

        public static string Company = "Test Formula";
        public static string AppName = "TestFormula's StAnalysis";

        public static string AppPath
        {
            get
            {
                string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Company + "\\" + AppName + "\\";
                if (!System.IO.Directory.Exists(Path))
                {
                    System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Company + "\\");
                    System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Company + "\\" + AppName + "\\");
                }
                return Path ;

            }
        } 

    }
}
