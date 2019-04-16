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
        App currentApp = Application.Current as App;

        public MainWindow()
        {
            InitializeComponent();
                       
        }


        /// <summary>
        /// Common functionality for all Stylus, Touch, and StylusButton Event handlers
        /// </summary>
        /// <param name="e"></param>
        private void GeneralStylusEventHandler(StylusEventArgs e)
        {
            StylusPoint point = e.GetStylusPoints(this).Last();
            currentApp.tabletState.Update(point);
            e.Handled = true;
        }

        private void GeneralTouchEventHandler(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(this);
            currentApp.tabletState.Update(point);
            e.Handled = true;
        }

        private void GeneralButtonEventHandler(StylusButtonEventArgs e, bool isPressed)
        {
            StylusButton myStylusButton = e.StylusButton;

            if (myStylusButton.Guid == StylusPointProperties.BarrelButton.Id)
            {
                currentApp.tabletState.Update("/input/stylus/barrel/click", isPressed);
            }
            else if (myStylusButton.Guid == StylusPointProperties.TipButton.Id)
            {
                currentApp.tabletState.Update("/input/stylus/eraser/click", isPressed);
            }
            e.Handled = true;
        }

        /// <summary>
        /// All handlers for application stylus/touch/button logging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_StylusDown(object sender, StylusDownEventArgs e)
        {
            currentApp.tabletState.Update("/input/stylus/surface/touch", true);
            GeneralStylusEventHandler(e);
        }

        private void InkCanvas_StylusUp(object sender, StylusEventArgs e)
        {
            currentApp.tabletState.Update("/input/stylus/surface/touch", false);
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
            currentApp.tabletState.Update("/input/finger/1/surface/touch", true);
            GeneralTouchEventHandler(e);
        }

        private void InkCanvas_TouchUp(object sender, TouchEventArgs e)
        {
            currentApp.tabletState.Update("/input/finger/1/surface/touch", false);
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

        //close the entire app when the capture window closes
        private void Window_Closed(object sender, EventArgs e)
        {
            currentApp.Shutdown();
        }
    }
}
