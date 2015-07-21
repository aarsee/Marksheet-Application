using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
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
    /// Interaction logic for PagePyschoTest.xaml
    /// </summary>
    public partial class PagePyschoTest : Page
    {
        string[] streams = new string[] { "Science", "Commerce", "Arts", "Humanities" };
        int h, m, s;

        int CurQues = 0;
        int CountQues = 20;

        private static string Path = AppData.AppPath + "TestQues.xml";
        public ObservableCollection<Ques> listQuestion { get; set; }

        Timer timer;

        public PagePyschoTest()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            page1.Visibility = System.Windows.Visibility.Visible;
            page1.IsEnabled = true;
            page2.Visibility = page3.Visibility = System.Windows.Visibility.Hidden;
            page2.IsEnabled = page3.IsEnabled = false;
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

            try
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    txtTime.Text = (h == 0 ? "" : h.ToString("d2") + ":") + string.Format("{0:d2}:{1:d2}", m, s);
                }));
            }
            catch (Exception) { }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (radOpt1.IsChecked == true || radOpt2.IsChecked == true)
            {
                listQuestion[CurQues - 1].SelectedIndex = (radOpt1.IsChecked == true ? 0 : 1);
                LoadQues(CurQues + 1);
            }
            if (!radOpt1.IsFocused && !radOpt2.IsFocused)
                radOpt1.Focus();
        }

        private void LoadQues(int ind)
        {
            if (ind >listQuestion.Count)
            {
                btnDone_Click(null, null);
                return;
            }
            CurQues = ind;
            txtStatus.Text = ind + "/" + CountQues;


            img1.Source = new BitmapImage(new Uri(@"..\Images\q\" + ind + "a.png", UriKind.Relative));
            img2.Source = new BitmapImage(new Uri(@"..\Images\q\" + ind + "b.png", UriKind.Relative));

            txtQues.Text = "Q " + ind + ". " + listQuestion[ind - 1].Question;
            optA.Text = "A. " + listQuestion[ind - 1].Options[0];
            optB.Text = "B. " + listQuestion[ind - 1].Options[1];

            radOpt2.IsChecked = radOpt1.IsChecked = false;
        }


        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            page2.Visibility = System.Windows.Visibility.Collapsed;
            page2.IsEnabled = false;
            page3.Visibility = System.Windows.Visibility.Visible;
            page3.IsEnabled = true;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            page1.Visibility = System.Windows.Visibility.Collapsed;
            page1.IsEnabled = false;
            page2.Visibility = System.Windows.Visibility.Visible;
            page2.IsEnabled = true;
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
            catch (System.IO.FileNotFoundException)
            {

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

        private void radOpt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //(sender as RadioButton).IsChecked = true;
            btnNext_Click(null, null);
            e.Handled = true;
        }

        private void BorderFinish_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnDone_Click(null, null);
        }

        private void BorderNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnNext_Click(null, null);
        }

        public static bool ResourceExists(string resourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return ResourceExists(assembly, resourcePath);
        }

        public static bool ResourceExists(Assembly assembly, string resourcePath)
        {
            return GetResourcePaths(assembly)
                .Contains(resourcePath);
        }

        public static IEnumerable<object> GetResourcePaths(Assembly assembly)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var resourceName = assembly.GetName().Name + ".g";
            var resourceManager = new ResourceManager(resourceName, assembly);

            try
            {
                var resourceSet = resourceManager.GetResourceSet(culture, true, true);

                foreach (System.Collections.DictionaryEntry resource in resourceSet)
                {
                    yield return resource.Key;
                }
            }
            finally
            {
                resourceManager.ReleaseAllResources();
            }

        }
    }
}
