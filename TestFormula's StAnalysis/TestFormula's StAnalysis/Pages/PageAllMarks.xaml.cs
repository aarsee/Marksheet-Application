using System;
using System.Collections.Specialized;
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
    /// Interaction logic for PageAllMarks.xaml
    /// </summary>
    public partial class PageAllMarks : Page
    {
        Profile profile;
        public List<StandardItem> SubjectsSelected = new List<StandardItem>();
        public List<StandardItem> SubjectsAvailable = new List<StandardItem>();

        public PageAllMarks(Profile profile)
        {
            this.profile = profile;
            InitializeComponent();
            init();
        }

        private void init()
        {
            foreach (eSubject sub in profile.GetSubjects())
            {
                SubjectsAvailable.Add(new StandardItem( sub.GetValue()));
            }
            CreateColumns();
            CreateRows();
            
        }

        private void CreateColumns() {
            int i = 0;

            //stackPanel.Children.Add(new TextBlock());
            AddAnalyse(i, null,false);

            eSubject[] subs= profile.GetSubjects();
            foreach (eSubject sub in subs){
                AddColumn1(sub, "Subjects[" + i + "].sScore", "Subjects[" + i + "].Color");
                AddAnalyse(i+1, sub, true);
                i++;
            }
            

            AddColumn1("Total", "TotalScore", "Color").IsReadOnly = true;
            AddColumn1("%age", "sPercentage","Color").IsReadOnly = true;


            AddAnalyse(i+1,null, false);
            AddAnalyse(i+2,null, false);
        }

        void linkSubject_Click(object sender, RoutedEventArgs e)
        {
            eSubject sub = (eSubject)(sender as Hyperlink).Tag;
            PageStudent.OpenSubjectCommand.Execute(sub, null);
        }

        private DataGridTextColumn AddColumn1(object source,string sBinding, string Color)
        {
            DataGridTextColumn col = new DataGridTextColumn();
            col.Header = source;
            col.IsReadOnly = false;
            col.Binding = new Binding(sBinding);

            if (source.GetType() == typeof(eSubject))
            {
                Style styleReading = new Style(typeof(TextBlock));
                Setter s = new Setter();
                s.Property = TextBlock.ForegroundProperty;
                Binding b = new Binding(Color);
                s.Value = b;
                styleReading.Setters.Add(s);
                col.ElementStyle = styleReading;
            }
            col.MinWidth = 90;
            dataGrid.Columns.Add(col);
            return col;
        }

        //ind refers to the column index
        private void AddAnalyse(int ind,object tag,bool show)
        {
            TextBlock txt = new TextBlock();
            if (show)
            {
                Hyperlink link = new Hyperlink();
                link.Tag = tag;
                link.Click += linkSubject_Click;
                link.Inlines.Add(new Run() { Text = "Analyse" });
                txt.Inlines.Add(link);
            }
//            dataGrid.Columns[0].Width.DisplayValueActualWidth
            Console.WriteLine(ind);
            Binding binding = new Binding("Columns[" + (ind) + "].Width.DisplayValue");
            binding.ElementName = "dataGrid";
            txt.SetBinding(TextBlock.WidthProperty, binding);
            //txt.SetBinding(TextBlock.TextProperty, binding);
            txt.TextAlignment = TextAlignment.Center;
            stackPanel.Children.Add(txt);
        }

        private void CreateRows() {
            dataGrid.ItemsSource = profile.Standards;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int offset = (int)profile.Type;
            int end=0;
            if (offset == 6)
            {
                end = 12;
            }
            else if (offset == 31)
            {
                end = 33;
            }
            for (int i = offset; i <= end; i++)
            {
                if (!profile.StandardExists(i))
                {
                    profile.AddStandard(new Classes.Standard(i));
                    break;
                }
            }
        }

        private void StandardLink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            PageStudent.OpenStandardCommand.Execute(link.Tag, null);
        }

        private void dataGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point p=e.GetPosition(dataGrid);
            bool b = Keyboard.GetKeyStates(Key.LeftCtrl)==KeyStates.Down;
            b =( b | Keyboard.GetKeyStates(Key.RightCtrl) == KeyStates.Down);
            if (!b && p.Y <=30 && p.X>dataGrid.Columns[0].ActualWidth)
            {
                //e.Handled = true;
            }
        }
    }

    public class MyBkColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int n = 0;
                n = int.Parse(value.ToString());

                if (n <= 30) return Brushes.Red;
                else if (n < 75) return Brushes.Black;
                else if (n < 90) return Brushes.DodgerBlue;
                else return Brushes.DarkGreen;
            }
            catch(Exception){
                if (value.ToString().Equals("NA")) return Brushes.DarkRed;
            }

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StandardItem{
        public string Name { get; private set; }
        public int Value { get; private set; }

        public StandardItem(int Value)
        {
            this.Value = Value;
            if (Value == 6) Name = "VI";
            else if (Value == 7) Name = "VII";
            else if (Value == 8) Name = "VIII";
            else if (Value == 9) Name = "IX";
            else if (Value == 10) Name = "X";
            else if (Value == 11) Name = "XI";
            else if (Value == 12) Name = "XII";
            else if (Value == 31) Name = "Semester 1";
            else if (Value == 32) Name = "Semester 2";
            else if (Value == 33) Name = "Summer Training";
            else if (Value == 34) Name = "Semester 3";
            else if (Value == 35) Name = "Semester 4";
            else if (Value == 36) Name = "Semester 5";
            else if (Value == 37) Name = "Semester 6";
            else Name = "Unknown "+Value;
        }
    }
}
