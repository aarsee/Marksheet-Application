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
    /// Interaction logic for PageStudentMarks.xaml
    /// </summary>
    public partial class PageStudentMarks : Page
    {
        Profile profile;
        Dictionary<int, TabItem> dicTabs = new Dictionary<int, TabItem>();

        public PageStudentMarks(Profile profile)
        {
            this.profile = profile;
            InitializeComponent();
            init();
        }

        private void init(){
            foreach (Classes.Standard standard in profile.Standards)
            {
                AddTab(standard);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            int ind = cmbStandard.SelectedIndex+6;
            if (profile.StandardExists(ind))
            {
                StackPanel stack = new StackPanel();
                txtLog.Inlines.Clear();
                txtLog.Inlines.Add(cmbStandard.SelectedItem+ " standard already exists.");
                txtLog.Inlines.Add(new LineBreak());
                txtLog.Inlines.Add("Click ");
                Hyperlink link = new Hyperlink(new  Run("here"));
                link.Click += link_Click;
                link.Tag = ind;
                txtLog.Inlines.Add(link);
                txtLog.Inlines.Add(" to go to the "+cmbStandard.SelectedItem+" standard tab.");
                stack.Orientation = Orientation.Horizontal;
                stack.Children.Add( new TextBlock() { Text = "Standard already exists." });
                stack.Children.Add(new TextBlock() { });
            }
            else
            {
                txtLog.Inlines.Clear();
                Classes.Standard standard=profile.AddStandard(ind);
                AddTab(standard);
            }
        }

        void link_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = dicTabs[(int)(sender as Hyperlink).Tag];
        }

        private void AddTab(Classes.Standard standard)
        {
            TabItem tabItem=AddTab(new PageStandardAnalysis(standard), standard.sValue + " Standard");
            dicTabs.Add(standard.Value, tabItem); 
            tabControl.SelectedItem = tabItem;
        }

        private TabItem AddTab(Page page,string Header)
        {
            if (page == null) return null;

            Frame frame = new Frame();
            frame.Content = page;

            TabItem tabItem = new TabItem();
            tabItem.Header = Header;
            tabItem.Content = frame;

            tabControl.Items.Insert(tabControl.Items.Count-1,tabItem);

            return tabItem;
        }
    }
}
