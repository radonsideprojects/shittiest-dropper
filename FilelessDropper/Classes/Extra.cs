using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;

namespace Dropper.Classes
{
    internal class Extra
    {
        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
