using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TestFormula_StAnalysis.Classes;

namespace TestFormula_StAnalysis.Pages
{
    /// <summary>
    /// Interaction logic for PageStartUp.xaml
    /// </summary>
    public partial class PageStartUp : Page
    {
        MainWindow window;

        public PageStartUp(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            init();
        }

        private void init()
        {
            History.GetRecentHistory();
            foreach(string item in  History.RecentList){
                TextBlock txt= new TextBlock();
                string[] items = item.Split(new char[]{'|'});
                txt.Inlines.Clear();
                txt.Inlines.Add(new Hyperlink(new Run(items[0])){Command=ApplicationCommands.Open, CommandParameter=items[1]});
                listRecentFiles.Children.Add(txt);
            }
            if (listRecentFiles.Children.Count > 1)
            {
                listRecentFiles.Children.RemoveAt(0);
            }
        }

        //private void hNewProfile_Click(object sender, RoutedEventArgs e)
        //{
        //    window.mnuNewProfile.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
        //    CloseMe();
        //}

        //private void hOpenProfile_Click(object sender, RoutedEventArgs e)
        //{
        //    window.mnuOpen.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
        //}

        private void CloseMe()
        {
            window.CloseTab(this.Tag as string);
        }

        private void ShowOnStartup_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            Settings.SetValue("ShowStartUpPage", chk.IsChecked == true ? "true" : "false");
            Settings.Save();
        }
    }
}
