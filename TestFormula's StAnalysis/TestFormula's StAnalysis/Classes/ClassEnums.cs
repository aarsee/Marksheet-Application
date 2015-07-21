using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFormula_StAnalysis.Classes
{
    class ClassEnums
    {
    }


    public enum GENDER : int
    {
        MALE = 0, FEMALE = 1
    }

    public enum eSubject : int
    {
        Maths=6, 
        English, 
        Hindi, 
        Sanskrit, 
        Science, 
        Social_Science,
        Arts,
        Sports,
        General_Knowledge,


        Marketing = 31,
        sub2,
        sub3,
        sub4,
        sub5,
        sub6,
        sub7,
        sub8
    }


    public static class eSubjectExtension
    {
        public static int GetValue(this eSubject s)
        {
            return (int)s;
        }

        public static eSubject GeteSubject(int Value){
            return (eSubject)Enum.GetValues(typeof(eSubject)).GetValue(Value);
        }
    }
}
