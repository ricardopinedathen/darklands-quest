using System.Drawing;
using DarklandsFiles.Class;

namespace DarklandsFiles.Helper
{
    class MapHelper
    {
        public static Point GetScreenPoint(DarkPlace place     )
        {
            return GetScreenPoint(place.Location,  Size.Empty );
        }
        public static Point GetScreenPoint(DarkPlace place, Size traslation)
        {
            return GetScreenPoint(place.Location, traslation);
        }
        /// <summary>
        /// converts a map point to screen point
        /// </summary>
        public static Point GetScreenPoint(Point Location, Size traslation)
        {
            var x = ((Location.X + 12) * 1.41);
            var y = ((Location.Y - 20) * 0.47);
            if (traslation != Size.Empty)
            {
                x -= (double)traslation.Width / 2;
                y -= (double)traslation.Height / 2;
            }
            return new Point((int)x, (int)y);
        }

        /// <summary>
        /// converts a map point to screen point
        /// </summary>
        public static Point GetMapPoint(Point Location)
        {
            var x = ((Location.X  / 1.41)- 12);
            var y = ((Location.Y  / 0.47)+ 20);
            return new Point((int)x, (int)y);
        }
    }
}
