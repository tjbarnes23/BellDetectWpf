using System;
using System.Windows;
using System.Windows.Controls;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class FIRPage : Page
    {
        readonly FIRVM viewModel;

        public FIRPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.RunFIR();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveFIR();
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveAsFIR();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
