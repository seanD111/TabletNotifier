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

/* See TabletStateClient.cs for state components that can be updated
*/
        

namespace TabletNotifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TabletStateClient tabletState = new TabletStateClient();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void GeneralStylusEventHandler(StylusEventArgs e)
        {
            StylusPoint point = e.GetStylusPoints(this).Last();
            tabletState.update(point);
            e.Handled = true;
        }

        private void GeneralTouchEventHandler(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(this);
            tabletState.update(point);
            e.Handled = true;
        }

        private void GeneralButtonEventHandler(StylusButtonEventArgs e, bool isPressed)
        {
            StylusButton myStylusButton = e.StylusButton;

            if (myStylusButton.Guid == StylusPointProperties.BarrelButton.Id)
            {
                tabletState.update("/input/stylus/barrel/click", isPressed);
            }
            else if (myStylusButton.Guid == StylusPointProperties.TipButton.Id)
            {
                tabletState.update("/input/stylus/eraser/click", isPressed);
            }
            e.Handled = true;
        }

        private void InkCanvas_StylusDown(object sender, StylusDownEventArgs e)
        {
            tabletState.update("/input/stylus/surface/touch", true);
            GeneralStylusEventHandler(e);
        }

        private void InkCanvas_StylusUp(object sender, StylusEventArgs e)
        {
            tabletState.update("/input/stylus/surface/touch", false);
            GeneralStylusEventHandler(e);
        }

        private void InkCanvas_StylusMove(object sender, StylusEventArgs e)
        {
            GeneralStylusEventHandler(e);
        }

        private void InkCanvas_StylusInAirMove(object sender, StylusEventArgs e)
        {
            GeneralStylusEventHandler(e);
        }

        private void InkCanvas_TouchDown(object sender, TouchEventArgs e)
        {
            tabletState.update("/input/finger/1/surface/touch", true);
            GeneralTouchEventHandler(e);
        }

        private void InkCanvas_TouchUp(object sender, TouchEventArgs e)
        {
            tabletState.update("/input/finger/1/surface/touch", false);
            GeneralTouchEventHandler(e);
        }

        private void InkCanvas_TouchMove(object sender, TouchEventArgs e)
        {
            GeneralTouchEventHandler(e);
        }

        private void InkCanvas_StylusButtonDown(object sender, StylusButtonEventArgs e)
        {
            GeneralButtonEventHandler(e, true);
        }

        private void InkCanvas_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            GeneralButtonEventHandler(e, false);
        }

    }
}
