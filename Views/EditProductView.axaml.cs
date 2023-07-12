using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using PharmacyAIS.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAIS.Views
{
    public partial class EditProductView : UserControl
    {
        public EditProductView()
        {
            InitializeComponent();
            AddHandler(DragDrop.DropEvent,DragDrop_Drop);
            borderDragInDrop.AddHandler(PointerEnterEvent, Border_PointerEnter);
            borderDragInDrop.AddHandler(PointerLeaveEvent, Border_PointerLeave);
            AddHandler(DragDrop.DragEnterEvent, DragDrop_Enter);
        }
        private async void Border_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            var image = await OpenFileDialogForImage();
            var viewmodel = DataContext as EditProductViewModel;
            if (viewmodel != null && image != null) viewmodel.EditImage(image);
            var text = borderDragInDrop.FindDescendantOfType<TextBlock>();
            text.Text = "Перетащите изображение или нажмите сюда, чтобы выбрать";
            borderDragInDrop.BorderBrush = Brushes.Black;
        }
        private void DragDrop_Drop(object? sender, DragEventArgs e)
        {
            var file = e.Data.GetFileNames()?.FirstOrDefault();
            var viewmodel = DataContext as EditProductViewModel;
            string? fileExtension = Path.GetExtension(file)?.ToLowerInvariant();
            if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" ||
                fileExtension == ".gif" || fileExtension == ".bmp") && fileExtension != null && viewmodel != null && file != null) 
            {
                viewmodel.EditImage(file);
                var text = borderDragInDrop.FindDescendantOfType<TextBlock>();
                text.Text = "Перетащите изображение или нажмите сюда, чтобы выбрать";
                borderDragInDrop.BorderBrush = Brushes.Black;
            } 
            else
            {
                var text = borderDragInDrop.FindDescendantOfType<TextBlock>();
                text.Text = "Неподходящее расширение файла, скиньте изображение";
                borderDragInDrop.BorderBrush = Brushes.Red;
            }
        }
        private void DragDrop_Enter(object? sender, DragEventArgs e)
        {
            borderDragInDrop.BorderDashArray = new Avalonia.Collections.AvaloniaList<double>(5,5);
        }
        private void Border_PointerEnter(object? sender, PointerEventArgs e)
        {
            borderDragInDrop.BorderDashArray = new Avalonia.Collections.AvaloniaList<double>(5, 5);
        }
        private void Border_PointerLeave(object? sender, PointerEventArgs e)
        {
            borderDragInDrop.BorderDashArray = null;
        }
        private async Task<string?> OpenFileDialogForImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Выберите изображение",
                AllowMultiple = false,
                Filters = new List<FileDialogFilter>
                {new FileDialogFilter { Name = "Изображения", Extensions = new List<string> { "jpg", "jpeg", "png", "bmp", "gif" } }}
            };
            Window window = ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow;

            string[] result = await openFileDialog.ShowAsync(window);
            if (result != null && result.Length > 0)
            {
                return result[0];
            }
            else return null;
        }



    }
}
