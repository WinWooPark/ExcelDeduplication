using ExcelDeduplication.Models;
using ExcelDeduplication.ViewModels;
using System.Windows;

namespace ExcelDeduplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainSystem _mainSystem;
        private MainViewModels _mainViewModels;
        public App()
        {
            _mainViewModels = new MainViewModels();
            _mainSystem = new MainSystem(_mainViewModels);            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.DataContext = _mainViewModels;

            MainWindow.Show();
            base.OnStartup(e);
            
        }
    }

}
