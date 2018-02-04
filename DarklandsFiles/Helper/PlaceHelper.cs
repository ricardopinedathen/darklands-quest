using System.Collections.Generic;
using DarklandsFiles.Class;

namespace DarklandsFiles.Helper
{
    class PlaceHelper
    {   

        /// <summary>
        /// fills the city list
        /// </summary>
        public static DarkPlaceList ReadPlaces(List<byte> data)
        {
            var Cities = new DarkPlaceList();

            //find index of first city
            int index = ListHelper.Find(data, "Groningen") - 38;
          
            DarkPlace place;
            do
            {
                place = new DarkPlace(data,index );
                if (DarkPlace.IsValid(place))
                {
                    Cities.Add(place);
                }
                index += 58;
            }
            while (DarkPlace.IsValid(place) );

            return Cities;
        }


    }
}