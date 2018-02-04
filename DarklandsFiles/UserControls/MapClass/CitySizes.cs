using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DarklandsFiles.Class;

namespace DarklandsFiles.UserControls.MapClass
{
    /// <summary>
    /// helper to get city sizes
    /// </summary> 
    class CitySizes
    {
        static readonly Size place1Size = new Size(2, 2);
        static readonly Size place2Size = new Size(4, 4);
        static readonly Size place3Size = new Size(6, 6);

        public static Size GetCitySize(DarkPlace place)
        {
            if (place.CityTypes == DarkCityTypes.Small1 ||
                place.CityTypes == DarkCityTypes.Small2)
            {
                return place1Size;
            }
            if (place.CityTypes == DarkCityTypes.Moderate1 ||
                place.CityTypes == DarkCityTypes.Moderate2)
            {
                return place2Size;
            }
            if (place.CityTypes == DarkCityTypes.Large1 ||
                place.CityTypes == DarkCityTypes.Large2)
            {
                return place3Size;
            }
            throw new InvalidOperationException();
        }
    }
}
