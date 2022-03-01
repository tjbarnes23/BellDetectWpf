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
            MW = this;
        }

        // This property gives a static reference to the main window, for use when navigating between pages
        public static MainWindow MW { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
