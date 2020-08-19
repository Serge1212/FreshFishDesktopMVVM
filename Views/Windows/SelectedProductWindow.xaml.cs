using FreshFishDesktopMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FreshFishDesktopMVVM.Views
{
    /// <summary>
    /// Interaction logic for SelectedProduct.xaml
    /// </summary>
    public partial class SelectedProductWindow : Window
    {
        public SelectedProductWindow()
        {
            InitializeComponent();
        }

        private void Button_close_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is IClosable c)
            {
                c.Close += () =>
                {
                    Close();
                };
            }
        }
    }
}
