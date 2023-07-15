using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System.ComponentModel;
using Avalonia.VisualTree;
using PharmacyAIS.Models;
using PharmacyAIS.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAIS.Views
{
    public partial class ProductsView : UserControl
    {
        private bool isUpdatingHeaderCheckBox;
        public ProductsView()
        {
            InitializeComponent();
            
        }
        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            int selectedIndex = comboBox.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    dataGrid.Columns[2].Sort(ListSortDirection.Ascending);
                    break;
                case 1:
                    dataGrid.Columns[2].Sort(ListSortDirection.Descending);
                    break;
                case 2:
                    dataGrid.Columns[6].Sort(ListSortDirection.Ascending);
                    break;
                case 3:
                    dataGrid.Columns[6].Sort(ListSortDirection.Descending);
                    break;
            }

        }
        private void DataGrid_CellPointerPressed(object sender, DataGridCellPointerPressedEventArgs e)
        {
            var datagridrow = e.Row.DataContext;
            dataGrid.SelectedItems.Add(datagridrow);
            var viewmodel = DataContext as ProductsViewModel;
            if (viewmodel != null) viewmodel.EditRowCommand();
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
