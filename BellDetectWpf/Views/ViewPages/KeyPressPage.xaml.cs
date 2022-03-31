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
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class KeyPressPage : Page
    {
        readonly KeyPressVM viewModel;

        public KeyPressPage()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private async void StartStop_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.RunKeyPresses();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MW.MainFrame.Navigate(new MainPage());
        }
    }
}
