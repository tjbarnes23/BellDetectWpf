using System;
using System.Windows;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri(@"..\..\Views\ViewPages\MainPage.xaml", UriKind.Relative);
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            MainWinVM.FrameContentRendered(sender, e);
        }

            private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
