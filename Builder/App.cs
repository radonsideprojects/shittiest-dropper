using System;
using System.Windows;

namespace Builder
{
    internal class App
    {
        [STAThread]
        public static void Main()
        {
            Application application = new Application();
            application.Run(new Windows.MainWindow());
        }
    }
}
