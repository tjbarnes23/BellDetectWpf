using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;
using Microsoft.Win32;

namespace BellDetectWpf.Views
{
    public partial class CreateWavSpecPage : Page
    {
        readonly CreateWavSpecVM viewModel;

        public CreateWavSpecPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            CreateWavVM.CreateWav();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new WavSpecPage());
        }
    }
}
