using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestFormula_StAnalysis.Classes
{
    public  class Standard : INotifyPropertyChanged
    {
        public static int MAX_SCORE = 100;
        private int _TotalScore;
        private int _TotalDuration;
        private string _NetPerformance;
        private double _percentage;
        private Profile.ProfileType Type;

        public int Value { get; set; }
        public string sValue { 
            get {
                if (Value == 6) return "VI";
                else if (Value == 7) return "VII";
                else if (Value == 8) return "VIII";
                else if (Value == 9) return "IX";
                else if (Value == 10) return "X";
                else if (Value == 11) return "XI";
                else if (Value == 12) return "XII";
                else if (Value == 31) return "Semester 1";
                else if (Value == 32) return "Semester 2";
                else if (Value == 33) return "Summer Training";
                return Value + "";
        } }

        public int TotalScore { 
            get { return _TotalScore; } 
            private set {
                if (_TotalScore == value) return;
                _TotalScore = value; 
                RaisePropertyChanged(); 
            } 
        }

        public string sPercentage
        {
            get { return (100*_percentage).ToString("F2"); }
        }

        public double Percentage
        {
            get { return _percentage; }
            private set
            {
                if (_percentage == value) return ;
                _percentage = value;
                RaisePropertyChanged();
                RaisePropertyChanged("sPercentage");
            } 
        }

        public int TotalDuration { 
            get { return _TotalDuration; } 
            private set {
                if (_TotalDuration == value) return;
                _TotalDuration = value; 
                RaisePropertyChanged(); 
            }
        }

        public string NetPerformance { 
            get { return _NetPerformance; }
            private set
            {
                if (_NetPerformance == value) return;
                _NetPerformance = value; 
                RaisePropertyChanged(); 
            }
        }

        public ObservableCollection<Subject> Subjects { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Standard(int iStandard=0,bool isNew=true)
        {
            this.Value = iStandard;
            if (Value == 0) Value = 6;
            if (Value>= 6 && Value<= 12)
            {
                Type = Profile.ProfileType.School_Student;
            }
            else if (Value >= 31 && Value <= 33)
            {
                Type = Profile.ProfileType.MBA_Student;
            }
            init();
            if (isNew)
            {
                AddSubjects();
            }
        }


        private void init()
        {
            _TotalDuration = 0;
            _TotalScore = 0;
            _NetPerformance = "";

            Subjects = new ObservableCollection<Subject>();
        }

        private void AddSubjects()
        {
            int start = (int )Type;
            int count = 0;
            

            if (Type == Profile.ProfileType.MBA_Student)
            {
                count = MBASubject.COUNT;
            }
            else if (Type == Profile.ProfileType.School_Student)
            {
                count = SkSubject.COUNT;
            }



            for (int i = 0; i < count; i++)
            {
                int indSub = start + i;
                Subject s = new Subject((eSubject)indSub);
                s.PropertyChanged += subject_PropertyChanged;
                Subjects.Add(s);
            }
        }

        void subject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName== "Score")
            {
                UpdateTotalScore();
            }
            else if (e.PropertyName == "Duration")
            {
                //UpdateTotalDuration();
            }
            RaisePropertyChanged("Subjects");
            //Console.WriteLine("In Standard, "+e.PropertyName);
        }

        private void UpdateTotalScore()
        {
            if (Subjects.Count == 0) return;
            int sum = 0;
            int max = 0;
            foreach (Subject s in Subjects)
            {
                if (s.Score > 0)
                {
                    sum += s.Score;max+=s.MaxScore;
                }
            }

            TotalScore = sum;
            Percentage = (double)sum / max;
        }

        public XElement GetXElement()
        {
            XElement ele = new XElement("Standard");
            ele.Add(new XElement("iStandard", this.Value));

            XElement ele2 = new XElement("Subjects");
            foreach (Subject sub in Subjects)
            {
                ele2.Add(sub.GetXElement());
            }
            ele.Add(ele2);

            return ele;
        }

        public static Standard ReadStandard(XElement ele)
        {
            int iStandard =int.Parse( ele.Element("iStandard").Value);
            Standard standard = new Standard(iStandard,false);

            XElement ele2 = ele.Element("Subjects");
            foreach (XElement eleSub in ele2.Elements("Subject"))
            {
                standard.Subjects.Add(Subject.ReadSubject(eleSub));
            }

            return standard;
        }

        private void RaisePropertyChanged([CallerMemberName] string Caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Caller));
            }
        }
    }

    
}
