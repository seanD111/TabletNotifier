using System;
using System.Collections.Generic;
using System.Diagnostics;
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
           
        }

        private void GeneralMouseEventHandler(MouseEventArgs e)
        {
            Point point = e.GetPosition(this);
            currentApp.tabletState.Update(point);
            
        }

        private void GeneralTouchEventHandler(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(this);
            currentApp.tabletState.Update(point);            
        }

        private void GeneralButtonEventHandler(StylusButtonEventArgs e, bool isPressed)
        {
            StylusButton myStylusButton = e.StylusButton;

            if (myStylusButton.Guid == StylusPointProperties.BarrelButton.Id)
            {
                currentApp.tabletState.Update("/input/stylus/barrel/click", isPressed);
            }
            else if (myStylusButton.Guid == StylusPointProperties.SecondaryTipButton.Id)
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
            if (IsStylus(e))
            {
                currentApp.tabletState.Update("/input/stylus/surface/touch", true);
                GeneralStylusEventHandler(e);
            }
        }

        private void InkCanvas_StylusUp(object sender, StylusEventArgs e)
        {
            if (IsStylus(e))
            {
                currentApp.tabletState.Update("/input/stylus/surface/touch", false);
                GeneralStylusEventHandler(e);

            }
        }

        private void InkCanvas_StylusMove(object sender, StylusEventArgs e)
        {
            if (IsStylus(e))
            {
                GeneralStylusEventHandler(e);
            }
        }

        private void InkCanvas_StylusInAirMove(object sender, StylusEventArgs e)
        {
            if (IsStylus(e))
            {
                GeneralStylusEventHandler(e);
            }
        }

        private void InkCanvas_TouchDown(object sender, TouchEventArgs e)
        {
            if (IsTouch(e))
            {

                currentApp.tabletState.Update("/input/finger/1/surface/touch", true);
                GeneralTouchEventHandler(e);
            }
        }

        private void InkCanvas_TouchUp(object sender, TouchEventArgs e)
        {
            if (IsTouch(e))
            {
                currentApp.tabletState.Update("/input/finger/1/surface/touch", false);
                GeneralTouchEventHandler(e);
            }
        }

        private void InkCanvas_TouchMove(object sender, TouchEventArgs e)
        {
            if (IsTouch(e))
            {
                GeneralTouchEventHandler(e);
            }
        }

        private void InkCanvas_StylusButtonDown(object sender, StylusButtonEventArgs e)
        {
            GeneralButtonEventHandler(e, true);
        }

        private void InkCanvas_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            GeneralButtonEventHandler(e, false);
        }

        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMouse(e))
            {
                currentApp.tabletState.Update("/input/mouse/surface/touch", true);
                GeneralMouseEventHandler(e);
            }
        }

        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouse(e))
            {
                GeneralMouseEventHandler(e);

            }
        }

        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouse(e))
            {
                currentApp.tabletState.Update("/input/mouse/surface/touch", false);
                GeneralMouseEventHandler(e);
            }
        }

        private bool IsMouse(MouseEventArgs e)
        {
            if(e.StylusDevice== null && e.MouseDevice != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsTouch(TouchEventArgs e)
        {
            Debug.WriteLine(e.GetType());
            if (e.TouchDevice != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool IsStylus(StylusEventArgs e)
        {
            if (e.StylusDevice != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //close the entire app when the capture window closes
        private void Window_Closed(object sender, EventArgs e)
        {
            currentApp.Shutdown();
        }

    }
}
