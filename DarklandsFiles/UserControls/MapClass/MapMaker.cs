using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DarklandsFiles.Class;
using DarklandsFiles.Helper;

namespace DarklandsFiles.UserControls.MapClass
{
    class MapMaker
    {
        public static Image CreateMapImage(Image  mapImage, DarkPlaceList places,Brush BackgroundDarkenBrush)
        {
            Bitmap image = new Bitmap(mapImage.Width, mapImage.Height);

            Graphics graphics = Graphics.FromImage(image);

            graphics.DrawImage(mapImage, 0, 0, image.Width, image.Height );

            DrawCities(graphics,places.GetCities());

            //darken the images a bit
            graphics.FillRectangle(
                BackgroundDarkenBrush,
                0, 0,
                image.Width, image.Height);

            graphics.Dispose();

            return image;
        }

        /// <summary>
        /// draw all the citys
        /// </summary>
        private static void DrawCities(Graphics graphics, IEnumerable<DarkPlace> Places)
        {
            if ( Places == null) return;

            foreach (var place in Places)
            {
                Size placeSize = CitySizes.GetCitySize(place);
                Point realPoint = MapHelper.GetScreenPoint(place, placeSize);

                graphics.FillRectangle(
                    Brushes.GreenYellow, new Rectangle(realPoint, placeSize));
                graphics.DrawRectangle(
                    Pens.Lime, new Rectangle(realPoint, placeSize));
            }
        }
    }
}
