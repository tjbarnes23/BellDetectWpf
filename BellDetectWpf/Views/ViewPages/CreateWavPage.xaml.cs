using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using Microsoft.Win32;

namespace BellDetectWpf.Views
{
    public partial class CreateWavPage : Page
    {
        readonly CreateWavVM viewModel;

        public CreateWavPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            CreateWavVM.CreateWav();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
