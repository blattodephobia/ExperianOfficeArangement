using ExperianOfficeArrangement.Models;
using ExperianOfficeArrangement.ViewModels;
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
    /// Interaction logic for ArrangedFieldView.xaml
    /// </summary>
    public partial class ArrangedFieldView : UserControl
    {
        public ArrangedFieldView()
        {
            InitializeComponent();
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(InteriorObjectBase.DataFormat))
            {
                ICommand arrangeCommand = (this.DataContext as ArrangedFieldViewModel)?.ArrangeObjectCommand;
                InteriorObjectBase obj = e.Data.GetData(InteriorObjectBase.DataFormat) as InteriorObjectBase;
                if (arrangeCommand == null || !arrangeCommand.CanExecute(obj))
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            e.Handled = true;
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(InteriorObjectBase.DataFormat))
            {
                ICommand arrangeCommand = (this.DataContext as ArrangedFieldViewModel)?.ArrangeObjectCommand;
                InteriorObjectBase obj = e.Data.GetData(InteriorObjectBase.DataFormat) as InteriorObjectBase;
                if (arrangeCommand != null && arrangeCommand.CanExecute(obj))
                {
                    arrangeCommand.Execute(obj);
                }
            }
        }

        private void UserControl_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(InteriorObjectBase.DataFormat))
            {
                ICommand arrangeCommand = (this.DataContext as ArrangedFieldViewModel)?.ArrangeObjectCommand;
                InteriorObjectBase obj = e.Data.GetData(InteriorObjectBase.DataFormat) as InteriorObjectBase;
                if (arrangeCommand == null || !arrangeCommand.CanExecute(obj))
                {
                    e.Effects = DragDropEffects.None;
                }
            }
        }
    }
}
