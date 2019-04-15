using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/* Reference of the possible input components:

    Dimensions: (possibly unused by openvr)
    "/input/surface/size/y"
    "/input/surface/size/x"

    Scalar:
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
    "/input/finger/1/surface/touch"
*/

namespace TabletNotifier
{
    public class TabletStateClient
    {

        public AsynchronousClient client;
        public Dictionary<string, double> d_Components = new Dictionary<string, double>
        {
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

        public Dictionary<string, bool> b_Components = new Dictionary<string, bool>
        {
            {"/input/stylus/barrel/click", false},
            {"/input/stylus/eraser/click", false},
            {"/input/stylus/surface/touch", false},
            {"/input/finger/1/surface/touch", false}
        };


        public TabletStateClient()
        {
            client = new AsynchronousClient();

        }
        

        public void update(StylusPoint point)
        {
            update("/input/stylus/position/x", point.X);
            update("/input/stylus/position/y", point.Y);
            update("/input/stylus/surface/value", point.PressureFactor);
        }

        public void update(TouchPoint point)
        {
            
            update("/input/finger/1/position/x", point.Position.X);
            update("/input/finger/1/position/y", point.Position.Y);
            update("/input/finger/1/size/x", point.Size.Width);
            update("/input/finger/1/size/y", point.Size.Height);
        }

        public void update(string key, double value)
        {
            if (d_Components.ContainsKey(key))
            {
                d_Components[key] = value;
            }
        }

        public void update(string key, bool value)
        {
            if (b_Components.ContainsKey(key))
            {
                b_Components[key] = value;
            }
        }



    }
}
