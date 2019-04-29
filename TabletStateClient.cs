using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VVVV.Utils.OSC;
using System.Diagnostics;

/* Reference of the possible input components:

    Dimensions: (possibly unused by openvr)
    "/input/surface/size/y"
    "/input/surface/size/x"

    Scalar:
    "/input/mouse/position/x"
    "/input/mouse/position/y"
    "/input/stylus/position/x"
    "/input/stylus/position/y"
    "/input/finger/1/position/x" 
    "/input/finger/1/position/y"
    "/input/finger/1/size/x" 
    "/input/finger/1/size/y" 
    "/input/stylus/surface/value"

    Boolean:
    "/input/stylus/barrel/click"
    "/input/stylus/eraser/click"
    "/input/stylus/surface/touch"
    "/input/mouse/surface/touch"
    "/input/finger/1/surface/touch"
*/

namespace TabletNotifier
{
    public class TabletStateClient
    {

        public AsynchronousClient client;
        public OSCTransmitter oscTransmitter;
        public string TabletName { get; private set; }
        public bool IsConnectedOSC { get; private set; }

        //Dictionary for possible double components
        public Dictionary<string, double> d_Components = new Dictionary<string, double>
        {
            {"/input/mouse/position/x", 0d},
            {"/input/mouse/position/y", 0d},
            {"/input/stylus/position/x", 0d},
            {"/input/stylus/position/y", 0d},
            {"/input/stylus/surface/value", 0d},
            {"/input/finger/1/position/x", 0d},
            {"/input/finger/1/position/y", 0d},
            {"/input/finger/1/size/x", 0d },
            {"/input/finger/1/size/y", 0d },
            {"/input/surface/size/y", 0f },
            {"/input/surface/size/x", 0f }
        };

        //Dictionary for possible boolean components
        public Dictionary<string, bool> b_Components = new Dictionary<string, bool>
        {
            {"/input/stylus/barrel/click", false},
            {"/input/stylus/eraser/click", false},
            {"/input/stylus/surface/touch", false},
            {"/input/mouse/surface/touch", false},
            {"/input/finger/1/surface/touch", false}
        };


        public TabletStateClient()
        {
            IsConnectedOSC = false;

        }


        //when a mouse point is received, update the mouse coordinates
        public void Update(Point point)
        {
            Update("/input/mouse/position/x", point.X);
            Update("/input/mouse/position/y", point.Y);
        }

        //when a stylus point is received, update the stylus pressure and coordinates
        public void Update(StylusPoint point)
        {
            Update("/input/stylus/position/x", point.X);
            Update("/input/stylus/position/y", point.Y);
            Update("/input/stylus/surface/value", point.PressureFactor);
        }

        //when a touch point is received, update the touch size and coordinates
        public void Update(TouchPoint point)
        {
            
            Update("/input/finger/1/position/x", point.Position.X);
            Update("/input/finger/1/position/y", point.Position.Y);
            Update("/input/finger/1/size/x", point.Size.Width);
            Update("/input/finger/1/size/y", point.Size.Height);
        }


        /// <summary>
        /// Update method for setting double key/value of dictionary.
        /// Also notifies a server of the value change
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>        
        public void Update(string key, double value)
        {
            if (d_Components.ContainsKey(key))
            {
                d_Components[key] = value;

                if (IsConnectedOSC)
                {
                    SendOSCMessage(key, value);
                }
            }
        }

        /// <summary>
        /// Update method for setting boolean key/value of dictionary.
        /// Also notifies a server of the value change
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>    
        public void Update(string key, bool value)
        {
            if (b_Components.ContainsKey(key))
            {
                b_Components[key] = value;

                if (IsConnectedOSC)
                {
                    SendOSCMessage(key, value);
                }
            }
        }



        public void Connect(string name, string ip, Int32 port)
        {
            TabletName = name;
            oscTransmitter = new OSCTransmitter(ip, port);
            IsConnectedOSC = true;
        }

        public void SendOSCMessage(string key, bool value)
        {
            string address = $"/{TabletName}{key}";
            OSCMessage msg = new OSCMessage(address, value, false);
            oscTransmitter.Send(msg);

        }
        public void SendOSCMessage(string key, double value)
        {
            string address = $"/{TabletName}{key}";
            OSCMessage msg = new OSCMessage(address, value);
            oscTransmitter.Send(msg);

        }

    }
}
