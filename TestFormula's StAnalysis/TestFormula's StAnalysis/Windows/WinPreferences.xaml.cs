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
    /// Interaction logic for WinPreferences.xaml
    /// </summary>
    public partial class WinPreferences : Window
    {

        private string SelectedTheme { get; set; }
        private string SelectedStudentMarksChartStyle { get;set; }

        public WinPreferences()
        {
            InitializeComponent();
            init(); 
        }

        private void init()
        {
            LoadSettings();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string txt = (cmbTheme.SelectedItem as string);

            //Application.Current.Resources.Source = themeUri;
            //Debug.Print(Resources.MergedDictionaries.Count+"");
            SaveSettings();
            Settings.Apply();

            this.Close();
        }

        private void SaveSettings()
        {
            Settings.SetValue("ShowStartUpPage", chkShowOnStartUp.IsChecked == true ? "true" : "false");
            Settings.SetValue("SubjectsComparisionChartStyle", cmbSubjectComparisionChartStyle.Text as string);
            Settings.SetValue("StudentMarksChartStyle", cmbStudentMarksChartStyle.Text as string);
            Settings.SetValue("AppTheme",cmbTheme.SelectedItem as string);
            Settings.Save();
        }

        private void LoadSettings()
        {
            chkShowOnStartUp.IsChecked= Settings.Set["ShowStartUpPage"]=="true";
            cmbSubjectComparisionChartStyle.Text = Settings.Set["SubjectsComparisionChartStyle"];
            cmbStudentMarksChartStyle.Text=Settings.Set["StudentMarksChartStyle"] ;
            cmbTheme.SelectedItem=Settings.Set["AppTheme"];
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
