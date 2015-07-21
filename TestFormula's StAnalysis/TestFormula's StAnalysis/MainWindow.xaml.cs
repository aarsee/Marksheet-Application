using System;
using System.IO;
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
using TestFormula_StAnalysis.Controls;
using TestFormula_StAnalysis.Windows;
using TestFormula_StAnalysis.Pages;

using Microsoft.Win32;

namespace TestFormula_StAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int countNewProfiles = 0;
        Dictionary<string, Object> dicTabs = new Dictionary<string, object>();
        static bool ParamsUsed = false;

        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            Settings.Load();

            bool success = false;
            if (ParamsUsed == false)
            {
                ParamsUsed = true;
                string[] s=Environment.GetCommandLineArgs();
                if (s.Length > 1)
                {
                    if (OpenProfile(s[1]))
                    {
                        success=true;
                    }
                }
            }

            if(!success && Settings.Set["ShowStartUpPage"]=="true")
                AddTab("PageStartup", new PageStartUp(this));
            UpdateRecentList();
        }


        private void btnMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //-------------------------------
            //      File Menu
            //
            #region "File Menu's Item" Click Event Decaltion
            if (sender.Equals(mnuNewProfile))
            {
                CreateNewProfile();
            }
            else if (sender.Equals(mnuOpen))
            {
                OpenProfile();
            }
            else if (sender.Equals(mnuSave))
            {

            }
            else if (sender.Equals(mnuSaveAs))
            {

            }
            else if (sender.Equals(mnuExit))
            {
                this.Close();
            }
            #endregion
            //------------------------------------------
            //          Edit Menu
            //
            #region "Edit Menu's Item" Click Event Declaration
            else if (sender.Equals(mnuUndo))
            {

            }
            else if (sender.Equals(mnuRedo))
            {

            }
            else if (sender.Equals(mnuCut))
            {

            }
            else if (sender.Equals(mnuCopy))
            {

            }
            else if (sender.Equals(mnuPaste))
            {

            }
            else if (sender.Equals(mnuDelete))
            {

            }
            else if (sender.Equals(mnuFind))
            {

            }
            else if (sender.Equals(mnuFindNext))
            {

            }
            else if (sender.Equals(mnuReplace))
            {

            }
            #endregion
            //----------------------------
            //          View Menu
            //
            #region "View Menu's Item" Click Event Declaration
            else if (sender.Equals(mnuToolbars))
            {

            }
            #endregion
            //----------------------------
            //          Tools Menu
            //
            #region "Tools Menu's Item" Click Event Declaration
            else if (sender.Equals(mnuCustomize))
            {

            }
            else if (sender.Equals(mnuPreferences))
            {
                WinPreferences winPreferences = new WinPreferences();
                winPreferences.ShowDialog();
            }
            #endregion
            //----------------------------
            //          Help Menu
            //
            #region "Help Menu's Item" Click Event Declaration
            else if (sender.Equals(mnuViewHelp))
            {

            }
            else if (sender.Equals(mnuFeedback))
            {
                tabControl.SelectedItem = AddTab("Feedback", new PageFeedback());
            }
            else if (sender.Equals(mnuTechnicalSupport))
            {

            }
            else if (sender.Equals(mnuAbout))
            {
                WinAbout winAbout = new WinAbout();
                winAbout.ShowDialog();
            }
            #endregion
        }

        private void UpdateRecentList()
        {
            for (int i = 0; i < mnuRecentList.Items.Count - 2; i++)
            {
                mnuRecentList.Items.RemoveAt(0);
            }

            int ind = 0;
            foreach (string item in History.RecentList)
            {
                string[] items = item.Split(new char[] { '|' });
                MenuItem mnuItem = new MenuItem();
                mnuItem.Header = items[0];
                mnuItem.Command = ApplicationCommands.Open;
                mnuItem.CommandParameter = items[1];
                mnuItem.InputGestureText = "";
                mnuRecentList.Items.Insert(ind, mnuItem);
                ind++;
                if (ind >= 10)
                {
                    break;
                }
            }
        }

        private void CreateNewProfile()
        {
            WinNewProfile winNewProfile = new WinNewProfile();
            Nullable<bool> result = winNewProfile.ShowDialog();
            if (result==true)
            {
                object[] ret=winNewProfile.GetResult();

                //countNewProfiles++;
                string key = GenKey();
                PageStudent pageprofile = new PageStudent(ret[1] as string, (Profile.ProfileType)ret[0]);
                TabItem tab = AddTab(key, pageprofile, ret[1] as string);
                tabControl.SelectedItem = tab;
            }
            
            
        }

        private void OpenProfile()
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "Student Pofile(*.tfp)|*.tfp|All Files|*.*";
            diag.FilterIndex = 0;
            diag.CheckFileExists = true;
            if (diag.ShowDialog() == true)
            {
                string path = diag.FileName;
                OpenProfile(path);
            }
        }

        private bool OpenProfile(string Path)
        {
            Profile profile = null;
            if (dicTabs.ContainsKey(Path))
            {
                //tab already opened
                tabControl.SelectedItem = dicTabs[Path] as TabItem;
            }
            else
            {
                try
                {
                    profile = Profile.LoadFile(Path);
                }
                catch (FileNotFoundException) {
                    MessageBox.Show("File \""+Path+"\" not found!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                catch (FileFormatException) {
                    MessageBox.Show("\"" + Path + "\" File format not supported!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                profile.FileName = Path;
                tabControl.SelectedItem = AddTab(profile.FileName, new PageStudent(profile), profile.Name);
            }

            History.AddToRecent(Path, profile.Name);
            UpdateRecentList();
            return true;
        }

        private CustomTabItem AddTab(string key, Page page, string Header = "")
        {
            key = key.ToUpper();
            if (dicTabs.Keys.Contains(key))
                return dicTabs[key] as CustomTabItem;

            CustomTabItem tabItem = new CustomTabItem(page);
            tabItem.Tag = key;

            tabControl.Items.Add(tabItem);
            dicTabs.Add(key, tabItem);

            return tabItem;
        }

        void tabHeader_Close_Click(object sender, EventArgs e)
        {
            CloseTab((sender as TabCloseHeader).Tag as string);
        }

        public void CloseTab(string key)
        {
            if (!dicTabs.Keys.Contains(key)) return;

            CloseTab(dicTabs[key] as CustomTabItem);
            
        }

        public bool CloseTab(CustomTabItem tab)
        {
            if (tab == null) return false;
            if (tab.Close())
            {
                tabControl.Items.Remove(tab);
                dicTabs.Remove(tab.Tag as string);
                return true;
            }
            return false;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool Ctrl = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
            bool Shift = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
            bool Alt = Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt);

            if (!Ctrl && !Shift && !Alt)
            {
                //if (e.Key == Key.F1)
                //{
                //    btnMenuItem_Click(mnuViewHelp, null);
                //}
                if (e.Key == Key.F3)
                {
                    btnMenuItem_Click(mnuFindNext, null);
                }
                else if (e.Key == Key.F4)
                {
                    btnMenuItem_Click(mnuPreferences, null);
                }
            }
            else if (Ctrl && !Shift && !Alt)
            {
                if (e.Key == Key.W)
                {
                    CloseTab(tabControl.SelectedItem as CustomTabItem);
                }
                else if (e.Key == Key.F1)
                {
                    //btnMenuItem_Click(mnuAbout, null);
                }
            }
        }

        private void Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.New)
            {
                CreateNewProfile();
            }
            else if (e.Command == ApplicationCommands.Open)
            {
                if (e.Parameter == null)
                {
                    OpenProfile();
                }
                else
                {
                    OpenProfile(e.Parameter as string);
                }
            }
            else if (e.Command == ApplicationCommands.Save)
            {
                if (((tabControl.SelectedItem as CustomTabItem).Page as PageStudent).SaveProfile())
                    UpdateRecentList();
            }
            else if (e.Command == ApplicationCommands.SaveAs)
            {
                if (((tabControl.SelectedItem as CustomTabItem).Page as PageStudent).SaveProfile(true))
                    UpdateRecentList();
            }
            else if (e.Command == CustomCommands.SaveRequired)
            {
                ((tabControl.SelectedItem as TabItem).Header as TabCloseHeader).Header = (e.Parameter as string) + "*";
            }
            else if (e.Command == ApplicationCommands.Close)
            {
                CloseTab((e.OriginalSource as CustomTabItem).Tag as string);
            }
        }

        private void CommandCanExecute_Handler(object sender, CanExecuteRoutedEventArgs e)
        {
            bool exec = false;
            if (e.Command == ApplicationCommands.New ||
                e.Command == ApplicationCommands.Open ||
                e.Command == ApplicationCommands.Help ||
                e.Command == CustomCommands.About)
            {
                exec= true;
            }
            else if (e.Command == ApplicationCommands.Save ||
                     e.Command == ApplicationCommands.SaveAs)
            {
                CustomTabItem tabItem = tabControl.SelectedItem as CustomTabItem;
                if (tabItem != null)
                {
                    try{
                        PageStudent page = tabItem.Page as PageStudent;
                        if (page != null)
                        {
                            exec = true;
                            if (e.Command == ApplicationCommands.Save)
                            {
                                exec = page.SaveRequired;
                            }
                        }
                        
                    }
                    catch{}
                }
            }
            else if (e.Command == ApplicationCommands.Close)
            {
                if(tabControl.SelectedItem!=null)
                    exec = true;
            }
            e.CanExecute = exec;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < dicTabs.Count; )
            {
                CustomTabItem item = dicTabs.Values.ToList()[0] as CustomTabItem;
                if (!CloseTab(item))
                {
                    e.Cancel = true;
                    break;
                }
            }
            History.WriteRecentHistory();
        }

        public string GenKey(int len=10)
        {
            Random ran= new Random();
            string s = "";
            for (int i = 0; i < len; i++)
            {
                s+=(char)((int)'a'+ran.Next(0,25));
            }
            return s;
        }
    }
}
