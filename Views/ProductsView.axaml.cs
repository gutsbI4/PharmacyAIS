using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;
using PharmacyAIS.Models;
using PharmacyAIS.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAIS.Views
{
    public partial class ProductsView : UserControl
    {
        private bool isUpdatingHeaderCheckBox;
        private List<object> _selectedRows;
        public ProductsView()
        {
            InitializeComponent();
            _selectedRows = new List<object>();
        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var checkBoxes = dataGrid.GetVisualDescendants().OfType<CheckBox>().ToList();

            CheckBox headerCheckBox = null;

            foreach (var checkBox in checkBoxes)
            {
                var columnHeader = checkBox.FindAncestorOfType<DataGridColumnHeader>();
                if (columnHeader != null)
                {
                    headerCheckBox = checkBox;
                    break;
                }
            }

            if (dataGrid.SelectedItems.Count == dataGrid.Items.OfType<object>().Count())
            {
                isUpdatingHeaderCheckBox = true;
                headerCheckBox.IsChecked = true;
                isUpdatingHeaderCheckBox = false;
            }
            else if (dataGrid.SelectedItems.Count > 0)
            {
                isUpdatingHeaderCheckBox = true;
                headerCheckBox.IsChecked = false;
                isUpdatingHeaderCheckBox = false;
            }
        }
        private void OnCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var dataGridRow = checkBox.FindAncestorOfType<DataGridRow>();
            var dataContext = dataGridRow.DataContext;
            dataGrid.SelectedItems.Add(dataContext);
        }

        private void OnCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var dataGridRow = checkBox.FindAncestorOfType<DataGridRow>();
            var dataContext = dataGridRow.DataContext;
            dataGrid.SelectedItems.Remove(dataContext);
        }
        private void HeaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            dataGrid.SelectAll();
            var daraGridRows = dataGrid.GetVisualDescendants().OfType<DataGridRow>();
            foreach (var item in daraGridRows)
            {
                var checkBox = item.FindDescendantOfType<CheckBox>();
                checkBox.IsChecked = true;
            }

        }

        private void HeaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (isUpdatingHeaderCheckBox) return;
            dataGrid.SelectedItems.Clear();
            var daraGridRows = dataGrid.GetVisualDescendants().OfType<DataGridRow>();
            foreach (var item in daraGridRows)
            {
                var checkBox = item.FindDescendantOfType<CheckBox>();
                checkBox.IsChecked = false;
            }
        }



    }
}
