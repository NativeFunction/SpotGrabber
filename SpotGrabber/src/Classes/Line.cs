using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotGrabber
{
    public class Line
    {
        public Vector2 Point1 = new Vector2(-1, -1);
        public Vector2 Point2 = new Vector2(-1, -1);

        public Line() { }

        public Line(Vector2 p1, Vector2 p2)
        {
            Point1 = p1;
            Point2 = p2;
        }

        public Line(Line l)
        {
            Point1 = l.Point1;
            Point2 = l.Point2;
        }
    }
}
