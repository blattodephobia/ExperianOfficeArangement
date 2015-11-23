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
            this.ValidateDragEvent(e);
        }

        private void ValidateDragEvent(DragEventArgs e)
        {
            InteriorObjectBase obj = null;
            if (e.Data.GetDataPresent(ArrangementPageViewModel.DataFormat))
            {
                obj = e.Data.GetData(ArrangementPageViewModel.DataFormat) as InteriorObjectBase;
            }
            else if (e.Data.GetDataPresent(ArrangedFieldViewModel.DataFormat))
            {
                obj = (e.Data.GetData(ArrangedFieldViewModel.DataFormat) as ArrangedFieldViewModel)?.ArrangedItems.FirstOrDefault();
            }

            ICommand arrangeCommand = (this.DataContext as ArrangedFieldViewModel)?.ArrangeObjectCommand;
            if (arrangeCommand == null || !arrangeCommand.CanExecute(obj))
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            ArrangedFieldViewModel target = (sender as FrameworkElement).DataContext as ArrangedFieldViewModel;
            if (e.Data.GetDataPresent(ArrangementPageViewModel.DataFormat))
            {
                ICommand arrangeCommand = target?.ArrangeObjectCommand;
                InteriorObjectBase obj = e.Data.GetData(ArrangementPageViewModel.DataFormat) as InteriorObjectBase;

                if (arrangeCommand?.CanExecute(obj) ?? false) arrangeCommand.Execute(obj);
            }
            else if (e.Data.GetDataPresent(ArrangedFieldViewModel.DataFormat))
            {
                ArrangedFieldViewModel source = e.Data.GetData(ArrangedFieldViewModel.DataFormat) as ArrangedFieldViewModel;
                ICommand arrangeCommand = target?.ArrangeObjectCommand;
                if (arrangeCommand?.CanExecute(source.ArrangedItems.FirstOrDefault()) ?? false)
                {
                    arrangeCommand.Execute(source.ArrangedItems.FirstOrDefault());
                    source.ArrangedItems.Clear();
                }
            }
        }

        private void UserControl_DragOver(object sender, DragEventArgs e)
        {
            this.ValidateDragEvent(e);
        }

        private void ContentControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject dragData = new DataObject(ArrangedFieldViewModel.DataFormat, (sender as ContentControl).DataContext);
                DragDrop.DoDragDrop(sender as DependencyObject, dragData, DragDropEffects.Move);
            }
        }
    }
}
