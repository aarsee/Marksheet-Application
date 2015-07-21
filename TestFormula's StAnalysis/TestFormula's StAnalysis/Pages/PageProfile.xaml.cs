using System;
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

using Microsoft.Win32;
using TestFormula_StAnalysis.Classes;

namespace TestFormula_StAnalysis.Pages
{
    /// <summary>
    /// Interaction logic for PageProfile.xaml
    /// </summary>
    public partial class PageProfile : Page
    {

        private bool IsEditingOn=false;
        public Profile profile{get;protected set;}

        public PageProfile(Profile profile=null)
        {
            this.profile = profile;
            InitializeComponent();
            init();
        }

        private void init()
        {
                TurnEditing(true);
                txtName.Focus();
        }

        private void hTurnEditing_Click(object sender, RoutedEventArgs e)
        {
            TurnEditing(!IsEditingOn);
            if (!IsEditingOn)
            {
                profile.Name = txtName.Text;
                profile.InstituteName = txtSchool.Text;
                profile.Address = txtAddress.Text;

                profile.Gender = (GENDER)cmbSex.SelectedIndex;
                profile.Image = imgProfile.Source as BitmapImage;
            }
        }

        private void TurnEditing(bool value)
        {
            IsEditingOn = value;

            hTurnEditing.Text = value ? "Done" : "Turn editing on";

            txtInterest.IsReadOnly=
                txtWayOfStudy.IsReadOnly=
                txtAddress.IsReadOnly= 
                txtSchool.IsReadOnly= 
                txtName.IsReadOnly= !value;

            txtInterest.BorderBrush=
                txtWayOfStudy.BorderBrush=
                txtAddress.BorderBrush =
                txtSchool.BorderBrush =
                txtName.BorderBrush = value? Brushes.Gray:Brushes.Transparent;

//            cmbSex.IsDropDownOpen = value?;
            cmbSex.IsHitTestVisible = IsEditingOn;
        }
        


        private void hRemoveImage_Click(object sender, RoutedEventArgs e)
        {
            imgProfile.Source = StaticClass.MaleAvatar;
        }

        private void hChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png|All Files(*.*)|*.*";
            diag.FilterIndex = 0;
            if (diag.ShowDialog() == true)
            {
                string path = diag.FileName;
                try
                {
                    imgProfile.Source = new BitmapImage(new Uri(path));
                    CustomCommands.SaveRequired.Execute(profile.Name, null);
                }
                catch { }
            }
        }

        private void txtTextInput_Handler(object sender, TextCompositionEventArgs e)
        {
            //(((this.Parent as Frame).Parent as TabItem).Parent as PageStudent).SetSaveRequired(true);
        //    CustomCommands.SaveRequired.Execute(profile.Name, null);
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Focus();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander obj = sender as Expander;
            if (obj.IsExpanded)
            {
                obj.Header = "Less";
            }
            else
            {
                obj.Header = "More";
            }
        }

        private void linkTest_Click(object sender, RoutedEventArgs e)
        {
            PageStudent.PyschoTestCommand.Execute(null, null);
        }

        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    if (profile == null)
        //        profile = new Profile();

        //    profile.Address = txtAddress.Text;
        //    profile.Name = txtName.Text;
        //    profile.SchoolName = txtSchool.Text;
        //    profile.Gender = (GENDER)cmbSex.SelectedIndex;
        //    profile.Image = imgProfile.Source as BitmapImage;

        //    ApplicationCommands.Save.Execute(null, null);
        //}

        //private void btnDone_Click(object sender, RoutedEventArgs e)
        //{
        //    TurnEditing(false);
        //}
        //public void Save()
        //{
        //    if (SaveProfile != null)
        //    {
        //        SaveProfile(this, null);
        //    }
        //}

        //private void btnReset_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
