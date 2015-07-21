using System;
using System.Collections.ObjectModel;
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
using System.Windows.Controls.DataVisualization.Charting;

namespace TestFormula_StAnalysis.Pages
{
    /// <summary>
    /// Interaction logic for PageSubjectAnalysis.xaml
    /// </summary>
    public partial class PageSubjectAnalysis : Page
    {
        DataPointSeries series1;

        public Profile profile { get; private set; }
        public int indSub { get; private set; }
        public string SubjectName { get; private set; }

        public ObservableCollection<KeyValuePair<string ,SkSubject>> Subjects { get; private set; }

        ObservableCollection<eSubject> GraphsInDisp = new ObservableCollection<eSubject>();
        ObservableCollection<eSubject> GraphsNotInDisp = new ObservableCollection<eSubject>();


        public PageSubjectAnalysis(Profile profile,int Sub)
        {
            this.profile = profile;
            this.indSub = Sub;
            preInit();
            InitializeComponent();
            init();
        }

        private void preInit()
        {
            SubjectName = Subject.GetSubjectByValue(indSub) + "";
            Subjects = new ObservableCollection<KeyValuePair<string,SkSubject>>();
        }

        private void init()
        {
            txtSubName.Text = SubjectName;
            cmbGraphStyle.Text = Settings.TryGetValue("SubjectAnalysisChartStyle", "Line"); 

            GraphsInDisp.Add((eSubject)indSub);
            eSubject[] subs = profile.GetSubjects();
            //int ind = 0;
            foreach (eSubject sub in subs)
            {
                if (sub.GetValue() != indSub)
                {
                    GraphsNotInDisp.Add(sub);
                }
            }

            cmbOtherSubjects.ItemsSource = GraphsNotInDisp;
            cmbDispSubjects.ItemsSource = GraphsInDisp;
            cmbOtherSubjects.SelectedIndex = 0;
            cmbDispSubjects.SelectedIndex = 0;

            //creating main subject marks column
            AddMarksColumn();
            dataGrid.ItemsSource = profile.Standards;
            UpdateChart();
        }

        private void AddMarksColumn()
        {
            AddSubjectForComparision(indSub,true);
            CalcAvg(0);
        }

        private void UpdateChart()
        {
            chart1.Series.Clear();

            string ChartStyle1 = cmbGraphStyle.Text;

            //if (ChartStyle1 == "Bar") series1 = new BarSeries();
            //else if (ChartStyle1 == "Pie") series1 = new PieSeries();
            //else if (ChartStyle1 == "Area") series1 = new AreaSeries();
            if (ChartStyle1 == "Column") series1 = new ColumnSeries();
            else if (ChartStyle1 == "Line") series1 = new LineSeries();

            int ind = indSub - (int)profile.Type;
            series1.DependentValuePath = "Subjects["+ind+"].Score";
            series1.IndependentValuePath = "sValue";
            series1.Title = "Marks scored in " + (eSubject)indSub;
            
            chart1.Series.Add(series1);

            Analyse();
        }

        private void Analyse()
        {
            series1.ItemsSource = null;
            series1.ItemsSource = profile.Standards;

            chart1.Width = 250 + 120 * profile.Standards.Count;
            CalcAvg();
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.DisplayIndex == 0) return;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                CalcAvg(e.Column.DisplayIndex-1);
            }
        }

        private void CalcAvg(int ind=0)
        {
            if (GraphsInDisp.Count <= 0) return;
            int indSub=GraphsInDisp[ind].GetValue();
            int sum=0, count=0;
            for (int i = 0; i < profile.Standards.Count; i++)
            {
                int val = profile.Standards[i].Subjects[ind].Score;
                if (val >= 0)
                {
                    count++;
                    sum += val;
                }
            }

            TextBlock txtAvg = (TextBlock)stackAvgs.Children[ind];
            if (count == 0)
                txtAvg.Text = "NA";
            else
                txtAvg.Text = (sum / count)+"";
        }

        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGrid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void expander_Changed(object sender, RoutedEventArgs e)
        {
            if(expander.IsExpanded)
                expander.Header = "Less";
            else
                expander.Header = "More";
        }

        private void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (cmbOtherSubjects.SelectedItem == null) return;
            eSubject subject = ((eSubject)cmbOtherSubjects.SelectedItem);

            GraphsInDisp.Add(subject);
            GraphsNotInDisp.Remove(subject);

            AddSubjectForComparision(subject.GetValue());
            AddSubjectInGraph(subject.GetValue());
            CalcAvg(GraphsInDisp.Count - 1);
            cmbOtherSubjects.SelectedIndex = 0;

        }

        private void AddSubjectForComparision(int indSub, bool isMain=false)
        {
            if(!isMain)
                txtSubName.Text += ", " + (eSubject)indSub;

            int ind = indSub-(int)profile.Type;
            DataGridTextColumn col = new DataGridTextColumn();
            Binding b = new Binding("Subjects[" + ind + "].sScore");
            col.Binding = b;
            col.Header = (eSubject)indSub;
            col.MinWidth = 100;
            dataGrid.Columns.Add(col);

            Style styleReading = new Style(typeof(TextBlock));
            Setter s = new Setter();
            s.Property = TextBlock.ForegroundProperty;
            Binding b2 = new Binding("Subjects[" + ind+ "].Color");
            s.Value = b2;
            styleReading.Setters.Add(s);
            col.ElementStyle = styleReading;

            TextBlock txtAvg = new TextBlock();
            Binding b3 = new Binding("ActualWidth");
            b3.Source=col;
            txtAvg.SetBinding(TextBlock.WidthProperty, b3);
            stackAvgs.Children.Add(txtAvg);
        }

        private void AddSubjectInGraph(int indSub)
        {
            DataPointSeries series0=null;
            //chart1.Series.Clear();

            string ChartStyle1 = cmbGraphStyle.Text;

            //if (ChartStyle1 == "Bar") series0 = new BarSeries();
            //else if (ChartStyle1 == "Pie") series0 = new PieSeries();
            //else if (ChartStyle1 == "Area") series0 = new AreaSeries();
            if (ChartStyle1 == "Column") series0 = new ColumnSeries();
            else if (ChartStyle1 == "Line") series0 = new LineSeries();

            int ind = indSub - (int)profile.Type;
            series0.DependentValuePath = "Subjects[" + ind + "].Score";
            series0.IndependentValuePath = "sValue";
            series0.Title = "Marks scored in "+(eSubject)indSub;

            series1.SeriesHost.Series.Add(series0);

            //chart1.Series.Add(series0);

            series0.ItemsSource = profile.Standards;
        }

        private void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            int ind=cmbDispSubjects.SelectedIndex;
            if (ind == 0) return;
            eSubject subject = ((eSubject)cmbDispSubjects.SelectedItem);

            GraphsNotInDisp.Add(subject);
            GraphsInDisp.Remove(subject);

            RemoveSubjectFromGraph(ind);
            RemoveSubjectFromGrid(ind);

            cmbDispSubjects.SelectedIndex = 0;
        }
        private void RemoveSubjectFromGraph(int ind)
        {
            series1.SeriesHost.Series.RemoveAt(ind);

            txtSubName.Text = string.Join(", ",GraphsInDisp.ToList());
            
        }
        private void RemoveSubjectFromGrid(int ind)
        {
            dataGrid.Columns.RemoveAt(ind+1);
            stackAvgs.Children.RemoveAt(ind);
        }
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            UpdateChart();
            foreach (int ind in GraphsInDisp)
            {
                if (ind == indSub) continue;
                AddSubjectInGraph(ind);
            }
        }
    }
}
