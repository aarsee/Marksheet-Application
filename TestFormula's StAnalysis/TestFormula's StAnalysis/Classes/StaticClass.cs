using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestFormula_StAnalysis.Classes
{
    class StaticClass
    {
        private static BitmapImage _MaleAvatar;
        private static BitmapImage _FemaleAvatar;

        public static BitmapImage MaleAvatar{
            get{
                if(_MaleAvatar==null){
                    _MaleAvatar = new BitmapImage(new Uri("../Images/male_avatar.png", UriKind.Relative));
                }
                return _MaleAvatar;
            }
        }
        public static BitmapImage FemaleAvatar
        {
            get
            {
                if (_FemaleAvatar == null)
                {
                    _FemaleAvatar = new BitmapImage(new Uri("../Images/female_avatar.png", UriKind.Relative));
                }
                return _FemaleAvatar;
            }
        }

    }
}
