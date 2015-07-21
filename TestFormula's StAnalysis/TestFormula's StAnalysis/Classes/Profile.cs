using System;
using System.Diagnostics;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using TestFormula_StAnalysis.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace TestFormula_StAnalysis.Classes
{
    public partial class Profile:INotifyPropertyChanged
    {
        public enum ProfileType{
            School_Student=6,
            MBA_Student=31
        }
        static double VERSION=1.0;

        private GENDER _Gender=GENDER.MALE;
        private string _Name = "";
        private string _InstituteName = "";
        private string _Address = "";
        private string _Interests = "";
        private string _WayOfStudy = "";
        private string _Aim = "";
        private string _FamilyBackground="";
        private ProfileType _profileType = ProfileType.School_Student;
        private double _version=VERSION;
        public BitmapImage RealImage { get; private set; }


        public string FileName { get; set; }
        private DateTime _lastAccessed;
        public BitmapImage Image {
            get {
                if (RealImage == null)
                {
                    if (Gender == GENDER.MALE)
                    {
                        return StaticClass.MaleAvatar;
                    }
                    else if(Gender==GENDER.FEMALE)
                    {
                        return StaticClass.FemaleAvatar;
                    }
                }
                return RealImage; 
            } 
            set {
                RealImage = value;
             }
        }
        public double Version{
            get {return _version;}
            set { 
                _version = value;
                RaisePropertyChanged();
            }
        }
        public GENDER Gender
        {
            get { return _Gender; }
            set
            {
                if (_Gender == value) return;
                _Gender = value;
                RaisePropertyChanged();
            }
        }
        public string Name { 
            get { return _Name; } 
            set {
                if (_Name == value) return;
                _Name = value;
                RaisePropertyChanged();
            } 
        }
        public string InstituteName { 
            get { return _InstituteName; }
            set { 
                if (_InstituteName == value) return;
                _InstituteName = value;
                RaisePropertyChanged();
            }
        }
        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address == value) return;
                _Address = value;
                RaisePropertyChanged();
            }
        }
        public string Interests
        {
            get { return _Interests; }
            set
            {
                if (_Interests == value) return;
                _Interests = value;
                RaisePropertyChanged();
            }
        }
        public string WayOfStudy
        {
            get { return _WayOfStudy; }
            set
            {
                if (_WayOfStudy == value) return;
                _WayOfStudy = value;
                RaisePropertyChanged();
            }
        }
        public string Aim
        {
            get { return _Aim; }
            set
            {
                if (_Aim == value) return;
                _Aim = value;
                RaisePropertyChanged();
            }
        }
        public ProfileType Type {
            get { return _profileType; }
            set { 
                _profileType = value;
                RaisePropertyChanged();
            }
        }
        public string FamilyBackground
        {
            get { return _FamilyBackground; }
            set
            {
                if (_FamilyBackground == value) return;
                _FamilyBackground = value;
                RaisePropertyChanged();
            }
        }
        public string sLastAccessed { get { return _lastAccessed.ToShortDateString(); }}

        public ObservableCollection<Standard> Standards { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public Profile(ProfileType profileType=ProfileType.School_Student)
        {
            init(profileType);
        }

        ~Profile(){
            _lastAccessed=DateTime.Now;
        }

        private void init(ProfileType type)
        {
            Standards = new ObservableCollection<Standard>();
            Name = "";
            InstituteName = "";
            Address = "";
            Interests = "";
            WayOfStudy = "";
            Gender = 0;
            _lastAccessed = DateTime.Now;
            FileName = "";
            Type = type;
        }

        public Standard AddStandard(int iStandard)
        {
            Standard standard = new Standard(iStandard);
            AddStandard(standard);
            return standard;
        }

        private void standard_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Standards");
            Console.WriteLine("In Profile, " + e.PropertyName);
        } 

        public void AddStandard(Standard standard)
        {
            if (GetStandard(standard.Value) != null) return;
            Standards.Add( standard);
            RaisePropertyChanged("Standards");
            standard.PropertyChanged += standard_PropertyChanged;
        }

        public Standard GetStandard(int iStandard)
        {
            foreach (Standard standard in Standards)
            {
                if (standard.Value == iStandard) return standard;
            }
            return null;
        }

        public bool StandardExists(int Standard)
        {
            return GetStandard(Standard)!=null;
        }

        public bool StandardExists(Standard Standard)
        {
            return Standards.Contains(Standard);
        }

        private void RaisePropertyChanged([CallerMemberName] string Caller="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Caller));
        }

        public static Profile LoadFile(string path) 
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File Not Found!!!");
                throw new FileNotFoundException();
            }
            if (!IsCorrectFormat(path))
            {
                Console.WriteLine("Bad File Format");
                throw new FileFormatException();
            }

            XElement ele = XElement.Load(path);
            int Type = int.Parse( ele.Element("ProfileType").Value);

            Profile profile = new Profile((ProfileType)Type);
            profile.Address = ele.Element("Address").Value;
            profile.Aim= ele.Element("Aim").Value;
            profile.FamilyBackground = ele.Element("FamilyBackground").Value;
            profile.Gender = (GENDER)int.Parse(ele.Element("Gender").Value);
            profile.InstituteName = ele.Element("InstituteName").Value;
            profile.Interests = ele.Element("Interests").Value;
            profile.Name = ele.Element("Name").Value;
            profile.Version = double.Parse( ele.Element("Version").Value);
            profile.WayOfStudy = ele.Element("WayOfStudy").Value;

            XElement ele2 = ele.Element("Standards");
            foreach (XElement eleStand in ele2.Elements())
            {
                profile.Standards.Add(Standard.ReadStandard(eleStand));
            }

            GC.Collect();

            return profile;
        }
        private static bool IsCorrectFormat(string Path)
        {
            try
            {
                XElement ele = XElement.Load(Path);
                ele.Element("AppName").Value = "TestFormula's StAnalysis";
            }
            catch (Exception) {
                return false; 
            }
            return true;
        }
        public static void SaveFile(Profile profile,string Path)
        {
            
            XElement ele = new XElement("Profile");
            ele.Add(new XElement("ProfileType", (int)profile.Type));
            ele.Add(new XElement("AppName", "TestFormula's StAnalysis"));
            ele.Add(new XElement("Address", profile.Address));
            ele.Add(new XElement("Aim", profile.Aim));
            ele.Add(new XElement("FamilyBackground", profile.FamilyBackground));
            ele.Add(new XElement("Gender", (int)profile.Gender));
            ele.Add(new XElement("InstituteName", profile.InstituteName));
            ele.Add(new XElement("Interests", profile.Interests));
            ele.Add(new XElement("Name", profile.Name));
            ele.Add(new XElement("Version", profile.Version));
            ele.Add(new XElement("WayOfStudy", profile.WayOfStudy));

            XElement ele2 = new XElement("Standards");
            foreach (Standard standard in profile.Standards)
            {
                ele2.Add(standard.GetXElement());
            }
            ele.Add(ele2);
            ele.Save(Path);

            GC.Collect();
        }

        public eSubject[] GetSubjects()
        {
            int start = (int)Type;
            int count = 0;
            if (Type == ProfileType.MBA_Student) count = MBASubject.COUNT;
            else if (Type == ProfileType.School_Student) count = SkSubject.COUNT;

            eSubject[] subs = new eSubject[count];
            for (int i = 0; i < count; i++)
            {
                int ind = start + i;
                subs[i] = (eSubject)ind;
            }
            
            return subs;
        }

    }
}
