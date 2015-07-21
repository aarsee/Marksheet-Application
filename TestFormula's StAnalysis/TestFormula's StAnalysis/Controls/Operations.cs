using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestFormula_StAnalysis.Controls
{
    class SubOperations:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture){

            Debug.Print("value : "+value+"\tparameter : "+parameter);
            if (parameter == null) parameter = "0";
            string param = parameter as string;
            double num1 = (double)value;
            double num2 =num1- double.Parse(parameter as string);


            if (num2 < 0) num2 = 0;

            if (targetType == num1.GetType())
                return num2;
            else
                return "" + num2;
            
        }

        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture){
            string param = parameter as string;
            char sym = param[0];
            int num1 = (int)value;
            int num2 = int.Parse(param.Substring(1));

            return num1 + num2;
        }
        
    }

    class AddOperations : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = parameter as string;
            char sym = param[0];
            int num1 = (int)value;
            int num2 = int.Parse(param.Substring(1));

            return num1 + num2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

    }



    class MulOperations : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = parameter as string;
            char sym = param[0];
            int num1 = (int)value;
            int num2 = int.Parse(param.Substring(1));

            return num1 * num2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

    }

    class DivOperations : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = parameter as string;
            char sym = param[0];
            int num1 = (int)value;
            int num2 = int.Parse(param.Substring(1));

            if (num2==0)
                throw new DivideByZeroException();
            return num1 / num2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

    }
}
