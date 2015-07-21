using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestFormula_StAnalysis.Classes
{
    class CustomCommands
    {


        public static readonly RoutedUICommand About= new RoutedUICommand("About","About",typeof(MainWindow));
        public static readonly RoutedUICommand SaveRequired = new RoutedUICommand("Save Required", "SaveRequired", typeof(MainWindow));

    }
}
