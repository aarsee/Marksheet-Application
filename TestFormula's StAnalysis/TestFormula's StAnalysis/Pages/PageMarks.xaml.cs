using System;
using System.Windows.Controls.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

using Microsoft.Win32;
using TestFormula_StAnalysis.Classes;

namespace TestFormula_StAnalysis.Pages
{
    /// <summary>
    /// Interaction logic for PageMarks.xaml
    /// </summary>
    public partial class PageMarks : Page
    {
        //public Standard Standard { get; protected set; }

        public Classes.Standard standard { get; private set; }

        DataPointSeries series1;

        int count=0;

        public PageMarks(Classes.Standard standard)
        {
            this.standard= standard;
            
            InitializeComponent();
            init();
        }

        private void init()
        {
            count = standard.Subjects.Count;
            dataGrid.ItemsSource = standard.Subjects;
            bind();

            UpdateChart();
            //chart.DataContext = standard.Subjects;
        }

        private void bind()
        {
            //Binding binding= new Binding("TotalScore");
            //binding.Source=standard;
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


        private  bool IsNumeric(string text)
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
            if(dataGrid.CurrentColumn.DisplayIndex>=1)
                e.Handled = !IsNumeric(e.Text);
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            //standard.AddSubject(SUBJECT.English);
            //UpdateChart();
        }

        private void UpdateChart()
        {
            chart1.Series.Clear();

            string ChartStyle1 = Settings.Set["StudentMarksChartStyle"];

            if (ChartStyle1 == "Bar")            series1 = new BarSeries();
            else if (ChartStyle1 == "Pie")       series1 = new PieSeries();
            else if (ChartStyle1 == "Area")      series1 = new AreaSeries();
            else if (ChartStyle1 == "Column")    series1 = new ColumnSeries();
            else if (ChartStyle1 == "Line")      series1 = new LineSeries();

            //string ChartStyle2 = Settings.Set["StudentPreformanceChartStyle"];

            //if (ChartStyle2 == "Bar")           series2 = new BarSeries();
            //else if (ChartStyle2 == "Pie")      series2 = new PieSeries();
            //else if (ChartStyle2 == "Area")     series2 = new AreaSeries();
            //else if (ChartStyle2 == "Column")   series2 = new ColumnSeries();
            //else if (ChartStyle2 == "Line")     series2 = new LineSeries();

            series1.DependentValuePath = "Score";
            series1.IndependentValuePath = "Name";
            series1.Title = "Marks scored";
            chart1.Series.Add(series1);

            //series2.DependentValuePath = "dPerformance";
            //series2.IndependentValuePath = "Name";
            //series2.Title = "Performance of student";
            //chart2.Series.Add(series2);

            Analyse();
        }

        private void Analyse()
        {
            series1.ItemsSource = null;
            series1.ItemsSource = standard.Subjects;

            //series2.ItemsSource = null;
            //series2.ItemsSource = standard.Subjects;

            chart1.Width = 250 + 75 * standard.Subjects.Count;
            //chart2.Width = 250 + 75 * standard.Subjects.Count;
            
        }

        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
