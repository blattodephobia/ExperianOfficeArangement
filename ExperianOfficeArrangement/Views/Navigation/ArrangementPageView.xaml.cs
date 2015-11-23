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
                DataObject dragData = new DataObject(ArrangementPageViewModel.DataFormat, (sender as ContentControl).Content);
                DragDrop.DoDragDrop(sender as DependencyObject, dragData, DragDropEffects.Copy);
            }
        }

        private void RecycleBinDrop(object sender, DragEventArgs e)
        {
            if (e.Effects == DragDropEffects.Move)
            {
                if (e.Data.GetDataPresent(ArrangedFieldViewModel.DataFormat))
                {
                    ArrangedFieldViewModel source = e.Data.GetData(ArrangedFieldViewModel.DataFormat) as ArrangedFieldViewModel;
                    source.ArrangedItems.Clear();
                }
            }
        }

        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            this.ValidateDragEvent(e);
        }

        private void Image_DragOver(object sender, DragEventArgs e)
        {
            this.ValidateDragEvent(e);
        }

        private void ValidateDragEvent(DragEventArgs e)
        {
            if (!(e.Effects == DragDropEffects.Move) || !e.Data.GetDataPresent(ArrangedFieldViewModel.DataFormat)) e.Effects = DragDropEffects.None;
            e.Handled = true;
        }
    }
}
