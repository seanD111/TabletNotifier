using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TabletNotifier
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Setup : Window
    {
        App currentApp = Application.Current as App;
        public Setup()
        {
            InitializeComponent();
        }

        private void Btn_connect_Click(object sender, RoutedEventArgs e)
        {
            int port = int.Parse(tb_port.Text);
            string ip = tb_ip.Text;
            string name = tb_tabletName.Text;

            //TODO: better port/ip/connection error handling
            if (!string.IsNullOrEmpty(name))
            {
                currentApp.tabletState.Connect(name, ip, port);

                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();

            }

        }
    }
}
