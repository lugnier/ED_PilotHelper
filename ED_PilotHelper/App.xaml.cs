using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace ED_PilotHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
 
    {
        internal static HttpClient Client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjY1MTIxQDMxMzgyZTMxMmUzMGxoRnp0ejZVY3dteHUyTmFsbGNkNms1WEhQTXZNZXhQaDI4dVh1ci94aGM9");

            
        }


    }
}
