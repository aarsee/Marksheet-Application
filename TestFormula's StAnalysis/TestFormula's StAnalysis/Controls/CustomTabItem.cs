using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestFormula_StAnalysis.Pages;

namespace TestFormula_StAnalysis.Controls
{
    public class CustomTabItem:System.Windows.Controls.TabItem
    {
        TabCloseHeader tabCloseHeader;
        public System.Windows.Controls.Page Page=null;
        System.Windows.Controls.Frame frame;

        public CustomTabItem(System.Windows.Controls.Page Page)
        {
            init(Page);
        }

        private void init(System.Windows.Controls.Page Page)
        {
            this.Page = Page;
            frame = new System.Windows.Controls.Frame();
            frame.Content = Page;

            this.Content = frame;

            tabCloseHeader = new TabCloseHeader();
            tabCloseHeader.Close_Click += tabCloseHeader_Close_Click;
            this.Header = tabCloseHeader;

            Binding binding = new Binding("Title");
            binding.Source = Page;
            tabCloseHeader.txtHeader.SetBinding(System.Windows.Controls.TextBlock.TextProperty, binding);
        }

        void tabCloseHeader_Close_Click(object sender, EventArgs e)
        {
            System.Windows.Input.ApplicationCommands.Close.Execute(null, this);
        }

        public bool Close()
        {
            if (Page.GetType() == typeof(PageStudent))
            {
                PageStudent pageMe = Page as PageStudent;
                return pageMe.Close();
            }
            return true;
        }

    }
}
