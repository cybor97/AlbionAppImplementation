using Albion.Data;
using System.Diagnostics;
using System.Windows;

namespace Albion.Client
{
    public partial class App : Application
    {
        public static Account Account { get; set; } = new Account();

        public App()
        {
            InitializeComponent();

            var mainWindow = new MainWindow();
            if (new AuthWindow().ShowDialog() ?? false)
                mainWindow.Show();
            else
                Process.GetCurrentProcess().Kill();
        }
    }
}
