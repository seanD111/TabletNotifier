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

public class TabletInfo
{
    
}

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

        private void InkCanvas_GeneralHandler(object sender, StylusDownEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
        private void InkCanvas_GeneralHandler(object sender, StylusEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
        private void InkCanvas_GeneralHandler(object sender, TouchEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
        private void InkCanvas_GeneralHandler(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

    }
}
