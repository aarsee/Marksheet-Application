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

namespace TestFormula_StAnalysis.Controls
{
    /// <summary>
    /// Interaction logic for TabCloseHeader.xaml
    /// </summary>
    public partial class TabCloseHeader : UserControl
    {
        public event EventHandler Close_Click;

        public string Header{
            get { return txtHeader.Text; }
            set { txtHeader.Text = value; }
        }

        public TabCloseHeader()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            //Binding binding = new Binding("Header");
            //binding.Source = this;
            //txtHeader.SetBinding(TextBlock.TextProperty, binding);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (Close_Click != null)
                Close_Click(this,e);
        }
    }
}
