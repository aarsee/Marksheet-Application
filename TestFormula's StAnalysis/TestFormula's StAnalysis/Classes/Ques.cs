using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace TestFormula_StAnalysis.Classes
{

    public class Ques:INotifyPropertyChanged
    {
        int ind;
        private string _ques;
        int ticked = -1;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Question
        {
            get { return _ques; }
            set { 
                _ques = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> Options { get; set; }

        public int Index { get { return ind; } private set { } }

        public int SelectedIndex { 
            get { return ticked; } 
            set { ticked = value;
            RaisePropertyChanged();
            } 
        }

        public Ques(int index)
        {
            ind = index;
            Options = new ObservableCollection<string>();
        }
        
        private void RaisePropertyChanged([CallerMemberName] string Caller = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Caller));
        }

        public XElement GetXElement()
        {
            XElement ele = new XElement("Question");
            ele.Add(new XAttribute("Index", Index));
            ele.Add(new XElement("Question", Question));
            foreach (string option in Options)
            {
                ele.Add(new XElement("Option", option));
            }
            return ele;
        }

        public static Ques LoadQues(XElement ele)
        {
            Ques q= new Ques(int.Parse(ele.Attribute("Index").Value));
            q.Question = ele.Element("Question").Value;
            foreach (XElement eleOp in ele.Elements("Option"))
            {
                q.Options.Add(eleOp.Value);
            }

            return q;
        }
    }
}
