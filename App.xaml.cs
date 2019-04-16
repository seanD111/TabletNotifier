using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TabletNotifier
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TabletStateClient tabletState = new TabletStateClient();

        //on application close, try to clean up connections
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (tabletState.IsConnected)
            {
                tabletState.oscTransmitter.Close();
            }
        }
    }
}
