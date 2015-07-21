using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestFormula_StAnalysis.Pages
{
    /// <summary>
    /// Interaction logic for PageStandardAnalysis.xaml
    /// </summary>
    public partial class PageStandardAnalysis : Page
    {
        public Classes.Standard standard { get; private set; }

        DataPointSeries series1;

        int count = 0;

        public PageStandardAnalysis(Classes.Standard standard)
        {
            this.standard = standard;
            InitializeComponent();
            init();
        }

        private void init()
        {
            count = standard.Subjects.Count;
            dataGrid.ItemsSource = standard.Subjects;
            bind();

            cmbGraph.Text = Settings.Set["StudentMarksChartStyle"];

            UpdateChart();
            //chart.DataContext = standard.Subjects;
        }

        private void bind()
        {
            //Binding binding = new Binding("TotalScore");
            //binding.Source = standard;
            //txtTotalScore.SetBinding(TextBlock.TextProperty, binding);
        }

        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsNumeric(text)) e.CancelCommand();
            }
            else e.CancelCommand();
        }


        private bool IsNumeric(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (count != standard.Subjects.Count)
            {
                count = standard.Subjects.Count;
                Analyse();
            }
        }

        private void dataGrid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (dataGrid.CurrentColumn.DisplayIndex >= 1)
                e.Handled = !IsNumeric(e.Text);
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            if(series1!=null)
                chart1.Series.Remove(series1);

            string ChartStyle1 = cmbGraph.Text;

            if (ChartStyle1 == "Pie") series1 = new PieSeries();
            else if (ChartStyle1 == "Line") series1 = new LineSeries();
            else if (ChartStyle1 == "Column") series1 = new ColumnSeries();
            else series1 = new ColumnSeries();
            //else if (ChartStyle1 == "Bar") series1 = new BarSeries();
            //else if (ChartStyle1 == "Area") series1 = new AreaSeries();

            series1.DependentValuePath = "Score";
            series1.IndependentValuePath = "Name";
            series1.Title = "Marks scored";
            chart1.Series.Add(series1);

            Analyse();
        }

        private void Analyse()
        {
            series1.ItemsSource = null;
            series1.ItemsSource = standard.Subjects;

            GC.Collect();

            chart1.Width = 250 + 75 * standard.Subjects.Count;
        }

        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            UpdateChart();
        }
    }
}