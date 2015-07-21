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
using System.Windows.Shapes;

namespace TestFormula_StAnalysis.Windows
{
    /// <summary>
    /// Interaction logic for WinNewProfile.xaml
    /// </summary>
    public partial class WinNewProfile : Window
    {

        object[] result=null;
        ItemProfileType[] ProfilesTypes;

        public WinNewProfile()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            ProfilesTypes = new ItemProfileType[] 
                        { new ItemProfileType("School Student",6,"../Images/school_student.png"), 
                            new ItemProfileType("MBA Student",31,"../Images/mba_student.png") };
            listBox.ItemsSource = ProfilesTypes;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
            
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Trim() == "") { txtName.Focus(); return; }
            DialogResult = true;
            result = new object[]{ (listBox.SelectedItem as ItemProfileType).Type, txtName.Text};
            this.Close();
        }

        public object[] GetResult()
        {
            return result;
        }
    }

    public class ItemProfileType
    {
        public string Name{get;set;}
        public int Type { get; set; }
        public Uri ImageSrc { get; private set; }
        public string ImageLoc { get; private set; }


        public ItemProfileType(string Name,int Type, string ImageLoc)
        {
            this.Name=Name;
            this.Type = Type;
            ImageSrc = new Uri(ImageLoc,UriKind.Relative);
            this.ImageLoc = ImageLoc;
        }
    }
}
