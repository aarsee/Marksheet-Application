using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml.Linq;
using TestFormula_StAnalysis.Classes;

namespace TestFormula_StAnalysis.Pages
{
    /// <summary>
    /// Interaction logic for PageTest.xaml
    /// </summary>
    public partial class PageTest : Page
    {
        string[] streams = new string[] { "Science", "Commerce", "Arts", "Humanities" };
        int h, m, s;

        int CurQues=0;
        int CountQues = 20;

        private static string Path = AppData.AppPath + "TestQues.xml";
        public ObservableCollection<Ques> listQuestion { get; set; } 

        Timer timer;

        public PageTest()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
//            grid1.Visibility = System.Windows.Visibility.Visible;
 //           grid1.IsEnabled = true;
            //grid2.Visibility = 
                grid3.Visibility = System.Windows.Visibility.Hidden;
            grid2.IsEnabled = grid3.IsEnabled= false;
            listQuestion = new ObservableCollection<Ques>();
            LoadQuestion();
        }

        void Start()
        {
            h = m = s = 0;

            timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            LoadQues(1);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            s++;
            if (s >= 60) { s = 0; m++; }
            if (m >= 60) { m = 0; h++; }

            Dispatcher.Invoke(new Action(() => {
                txtTime.Text = (h == 0 ? "" : h.ToString("d2") + ":") + string.Format("{0:d2}:{1:d2}", m, s);
            }));
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (radOpt1.IsChecked == true || radOpt2.IsChecked == true)
            {
                listQuestion[CurQues - 1].SelectedIndex = (radOpt1.IsChecked == true ? 0 : 1);
                LoadQues(CurQues + 1);
            }
            if(!radOpt1.IsFocused && !radOpt2.IsFocused)
                radOpt1.Focus();
        }

        private void LoadQues(int ind)
        {
            if (ind == listQuestion.Count)
            {
                btnDone_Click(null, null);
                return;
            }
            CurQues = ind;
            txtStatus.Text = ind+"/"+CountQues;

            txtQues.Text = "Q "+ind+". "+ listQuestion[ind - 1].Question;
            radOpt1.Content = listQuestion[ind - 1].Options[0];
            radOpt2.Content = listQuestion[ind - 1].Options[1];

            radOpt2.IsChecked=radOpt1.IsChecked = false;
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            grid2.Visibility = System.Windows.Visibility.Collapsed;
            grid2.IsEnabled = false;
            grid3.Visibility = System.Windows.Visibility.Visible;
            grid3.IsEnabled = true;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //grid1.Visibility = System.Windows.Visibility.Collapsed;
            //grid1.IsEnabled = false;
            grid2.Visibility = System.Windows.Visibility.Visible;
            grid2.IsEnabled = true;
            Start();
        }

        private void LoadQuestion()
        {
        here:
            CopyDefaultTestFile();
            listQuestion.Clear();
            try
            {
                XElement ele = XElement.Load(Path);
                foreach (XElement ele1 in ele.Elements("Question"))
                {
                    listQuestion.Add(Ques.LoadQues(ele1));
                }
            }
            catch(System.IO.FileNotFoundException){

                goto here;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.InnerException);
            }
        }


        private void CopyDefaultTestFile()
        {
            if (!File.Exists(Path))
            {
                Uri uri = new Uri("AppData\\TestQues.xml", UriKind.Relative);
                StreamResourceInfo sourceStream = Application.GetResourceStream(uri);
                StreamWriter s = new StreamWriter(Path);
                sourceStream.Stream.CopyTo(s.BaseStream);
                s.Close();
            }
            //File.Copy("\\AppData\\Settings.txt", Path);
        }

    }
}
