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

        private void FilterFIRCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.RunFIR();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveFIR();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
