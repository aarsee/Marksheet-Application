using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace TestFormula_StAnalysis.Classes
{
    public class Subject : INotifyPropertyChanged
    {
        protected int _id;

        protected string _Name;
        int _maxscore = Standard.MAX_SCORE;
        int _score;
        int _duration;
        string _performance;
        double _dPerformance;
        Brush _color;

        public event PropertyChangedEventHandler PropertyChanged;


        public eSubject ID
        {
            get { return (eSubject)_id; }
            set
            {
                if (_id == (int)value) return;
                _id = (int)value;
                Name = value.ToString();
                RaisePropertyChanged();
                UpdatePerformances();
            }
        }


        public string Name
        {
            get
            {
                return ((eSubject)_id).ToString();
                //return _Name; 
            }
            set
            {
                if (_Name == value) return;
                _Name = value;
                RaisePropertyChanged();
                UpdatePerformances();
            }
        }


        public int MaxScore
        {
            get { return _maxscore; }
            set
            {
                if (_maxscore == value || value < 0) return;
                _maxscore = value;
                RaisePropertyChanged();
                UpdatePerformances();
            }
        }

        public string sScore
        {
            get
            {
                if (_score <= -1)
                    return "NA";
                else
                    return _score + "";
            }
            set
            {
                try
                {
                    int n = int.Parse(value);
                    if (_score == n) return;
                    Score = n;
                    RaisePropertyChanged();
                    UpdatePerformances();
                }
                catch (Exception)
                {
                    RaisePropertyChanged();
                }
            }
        }

        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChanged();
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                if (_score == value || value > MaxScore) return;
                if (_score < 0)
                {
                    Color = Brushes.DarkRed;
                }
                if (value <= 30) Color = Brushes.Red;
                else if (value < 75) Color = Brushes.Black;
                else if (value < 90) Color = Brushes.DarkBlue;
                else Color = Brushes.Green;
                _score = value;
                RaisePropertyChanged();
                UpdatePerformances();
            }
        }
        public int Duration
        {
            get { return _duration; }
            set
            {
                if (_duration == value || value < 0) return;
                _duration = value;
                RaisePropertyChanged();
                UpdatePerformances();
            }
        }
        public string Performance
        {
            get { return _performance; }
            set
            {
                if (_performance == value) return;
                _performance = value;
                RaisePropertyChanged();
            }
        }
        public double dPerformance
        {
            get { return _dPerformance; }
            set
            {
                if (_dPerformance == value) return;
                _dPerformance = value;
                RaisePropertyChanged();
            }
        }

        public Subject(eSubject ID)
        {
            this.ID = ID;
            Name = "";
            Score = -1;
            Color = Brushes.DarkRed;
            Duration = 0;
            Performance = "";
        }

        public string GetScore()
        {
            if (_score == -1)
            {
                return "N/A";
            }
            return _score + "";
        }

        protected void UpdatePerformances()
        {
            if (Duration == 0)
            {
                dPerformance = 10;
                Performance = "Seriously ???";
            }
            else
            {
                dPerformance = 10 * ((double)Score / MaxScore) / (double)Duration;
                Performance = dPerformance.ToString("F2") + "";
            }
        }

        public XElement GetXElement()
        {
            XElement ele = new XElement("Subject");
            ele.Add(new XElement("Performance", this.dPerformance));
            ele.Add(new XElement("Duration", this.Duration));
            ele.Add(new XElement("ID", this._id));
            ele.Add(new XElement("MaxScore", this.MaxScore));
            ele.Add(new XElement("Name", this._Name));
            ele.Add(new XElement("Score", this.Score));

            return ele;
        }

        public static Subject ReadSubject(XElement ele)
        {
            Subject sub = new Subject((eSubject)int.Parse(ele.Element("ID").Value));
            sub.dPerformance=int.Parse(ele.Element("Performance").Value);
            sub.Duration = int.Parse(ele.Element("Duration").Value);
            //sub.SetId(int.Parse(ele.Element("ID").Value));
            sub.MaxScore = int.Parse(ele.Element("MaxScore").Value);
            sub.Score = int.Parse(ele.Element("Score").Value);
            sub.SetName( ele.Element("Name").Value);

            return sub;
        }

        public void SetId(int ID)
        {
            _id = ID;
        }

        public void SetName(string Name)
        {
            _Name = Name;
        }

        public static eSubject GetSubjectByValue(int Value)
        {
            return (eSubject)Value;
            //return (eSubject)Enum.GetValues(typeof(eSubject)).GetValue(Value);
        }

        protected void RaisePropertyChanged([CallerMemberName] string Caller = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Caller));
        }

    }
}
