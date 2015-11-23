using ExperianOfficeArrangement.Models;
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

namespace ExperianOfficeArrangement.Views
{
    /// <summary>
    /// Interaction logic for ArrangementPageView.xaml
    /// </summary>
    public partial class ArrangementPageView : UserControl
    {
        public ArrangementPageView()
        {
            InitializeComponent();
        }

        private void Polygon_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject dragData = new DataObject(InteriorObjectBase.DataFormat, (sender as ContentControl).Content);
                DragDrop.DoDragDrop(sender as DependencyObject, dragData, DragDropEffects.Copy);
            }
        }
    }
}
