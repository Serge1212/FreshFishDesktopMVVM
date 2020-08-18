using FreshFishDesktopMVVM.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreshFishDesktopMVVM.Views.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            BordermenuClose.Visibility = Visibility.Visible;
            BordermenuOpen.Visibility = Visibility.Collapsed;
            MainFrame.IsEnabled = false;
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            BordermenuClose.Visibility = Visibility.Collapsed;
            BordermenuOpen.Visibility = Visibility.Visible;
            MainFrame.IsEnabled = true;
        }
    }
}
