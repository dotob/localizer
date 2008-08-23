using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Localizer
{
    /// <summary>
    /// Interaction logic for ResEditControl.xaml
    /// </summary>
    public partial class ResEditControl : UserControl
    {
        public ResEditControl()
        {
            InitializeComponent();
        }

        private void langGrid_Initialized(object sender, EventArgs e)
        {
            GridInfo gi = this.DataContext as GridInfo;
            if(gi!=null) {
                langGrid.Columns["Typ"].Visible = gi.ShowType;
            }
        }
    }
}
