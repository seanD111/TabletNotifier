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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TabletNotifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InkCanvas_StylusDown(object sender, StylusDownEventArgs e)
        {

        }

        private void InkCanvas_StylusMove(object sender, StylusEventArgs e)
        {

        }

        private void InkCanvas_StylusInAirMove(object sender, StylusEventArgs e)
        {

        }

        private void InkCanvas_StylusUp(object sender, StylusEventArgs e)
        {

        }

        private void InkCanvas_TouchDown(object sender, TouchEventArgs e)
        {

        }

        private void InkCanvas_TouchUp(object sender, TouchEventArgs e)
        {

        }

        private void InkCanvas_TouchMove(object sender, TouchEventArgs e)
        {

        }
    }
}
