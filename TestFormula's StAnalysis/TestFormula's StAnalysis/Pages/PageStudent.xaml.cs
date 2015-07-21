using System;
using Microsoft.Win32;
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

using TestFormula_StAnalysis.Controls;
using TestFormula_StAnalysis.Classes;

namespace TestFormula_StAnalysis.Pages
{
    /// <summary>
    /// Interaction logic for PageStudent.xaml
    /// </summary>
    public partial class PageStudent : Page
    {
        public static RoutedUICommand OpenStandardCommand = new RoutedUICommand("Open Standard", "OpenStandard", typeof(PageStudent));
        public static RoutedUICommand OpenSubjectCommand = new RoutedUICommand("Open Subject", "OpenSubject", typeof(PageStudent));
        public static RoutedUICommand PyschoTestCommand = new RoutedUICommand("Pyschometric Test", "PyschometricTest", typeof(PageStudent));

       public Profile profile { get; private set; }
       public bool SaveRequired=false;

       bool[] isStandardOpen= new bool[10];
       bool[] isSubjectOpen = new bool[20];

       Dictionary<string, object> dicTabs = new Dictionary<string, object>();

       PageProfile pageProfile;
//       PageStudentMarks pageStuMarks;
       PageAllMarks pageAllMarks;

       public PageStudent(Profile profile)
        {
            this.profile = profile;
            InitializeComponent();
            init(profile.Type);
        }

       public PageStudent(string Name,Profile.ProfileType Type)
       {
           this.profile = null;
           InitializeComponent();
           init(Type,Name);
       }

        private void init(Profile.ProfileType Type,string Name="")
        {
            if (profile == null)
            {
                profile = new Profile(Type);
                profile.Name = Name;
                SaveRequired = true;
            }
            UpdateTitle();
            profile.PropertyChanged += profile_PropertyChanged;

            pageProfile = new PageProfile(profile);
            pageAllMarks = new PageAllMarks(profile);
            AddTab(pageProfile , "Profile");
            AddTab(pageAllMarks, "All Marks");
        }

        void profile_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
                    UpdateTitle();
            SetSaveRequired(true);
        }

        private void UpdateTitle()
        {
            this.Title = profile.Name;
            if (this.Title == "") this.Title = "Unnamed Profile";
            if (SaveRequired)
                this.Title += "*";
        }

        private TabItem AddTab(Page page, string Header,bool focus=false)
        {
            TabItem tabItem;
            if (!dicTabs.ContainsKey(Header))
            {
                tabItem = AddTab2(page,Header);
                dicTabs.Add(Header, tabItem);
            }
            else
            {
                tabItem = dicTabs[Header] as TabItem;
            }

            if (focus)
            {
                tabControl.SelectedItem = tabItem;
            }
            return tabItem;
        }

        private TabItem AddTab2(Page page, string Header)
        {
            Frame frame = new Frame();
            frame.Content = page;


            TabItem tabItem = new TabItem();
            tabItem.Header = Header;
            tabItem.Content = frame;

            tabControl.Items.Add(tabItem);
            return tabItem;
        }

        public bool SaveProfile(bool doNew=false)
        {
            string path=profile.FileName;
            if (doNew)
                path= "";

            doNew = false;
            if (path == "" || !System.IO.File.Exists(path))
            {
                SaveFileDialog diag = new SaveFileDialog();
                diag.Filter = "Student Pofile(*.tfp)|*.tfp|All Files|*.*";
                diag.FilterIndex = 0;
                diag.DefaultExt = "*.tfp";
                diag.CheckPathExists = true;
                diag.ValidateNames = true;
                diag.OverwritePrompt = true;
                if (diag.ShowDialog() == true)
                {
                    path = diag.FileName;
                }
                doNew = true;
                History.AddToRecent(path, profile.Name);
            }
            if (path == "") return false;
            Profile.SaveFile(profile, path);
            SetSaveRequired(false);
            return doNew;
        }

        public void  SetSaveRequired(bool value)
        {
            SaveRequired = value;
            UpdateTitle();
            CustomCommands.SaveRequired.Execute(profile.Name, null);
            //(((this.Parent as Frame).Parent as TabItem).Header as TabCloseHeader).Header = profile.Name + "*";
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == OpenStandardCommand
                    || e.Command == OpenSubjectCommand
                    || e.Command == PyschoTestCommand) e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == OpenStandardCommand)
            {
                OpenStandardTab((int)e.Parameter);
            }
            else if (e.Command == OpenSubjectCommand)
            {
                OpenSubjectTab((eSubject)e.Parameter);
            }
            else if (e.Command == PyschoTestCommand)
            {
                AddTab(new PagePyschoTest(), "Test", true);
            }
        }

        private void OpenSubjectTab(eSubject s)
        {
            AddTab(new PageSubjectAnalysis(profile,s.GetValue()),"Subject "+s,true);
        }

        private void OpenStandardTab(int Standard)
        {
            AddTab(new PageStandardAnalysis(profile.Standards[Standard-(int)profile.Type]), Standard + " standard",true);
        }

        public bool Close(){
            if (SaveRequired)
            {
                MessageBoxResult result=MessageBox.Show("Do you want to save the changes to "+profile.Name,AppData.AppName,MessageBoxButton.YesNoCancel,MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SaveProfile();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
