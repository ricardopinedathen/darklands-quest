using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using DarklandsFiles.Helper;

namespace DarklandsFiles.Class
{
    public class DarkPlace
    {
        #region " constructors ... "

        public DarkPlace(List<byte> data, int index)
        {
            //review thoes 8 bytes after the name
            Name = ListHelper.Read(data, index + 38, 12);
            Reputation = ShortHelper.ParseInt(data, index + 18);
            DataIndex = index;
            PlaceType = (DarkPlaceTypes)ShortHelper.ParseUInt(data, index);
            CityTypes = (DarkCityTypes)data[index + 17];
            var x = ShortHelper.ParseUInt(data, index + 4);
            var y = ShortHelper.ParseUInt(data, index + 6);
            Location = new Point(x, y);
        }
        public DarkPlace(string name, Point location, DarkCityTypes cityTypes, DarkPlaceTypes placeType)
        {
            Name = name;
            PlaceType = placeType;
            CityTypes = cityTypes;
            Location = location;
        }

        #endregion

        private readonly  int DataIndex;

        public readonly string Name;
        public int Reputation;
        public readonly DarkPlaceTypes PlaceType;
        public readonly DarkCityTypes CityTypes;
        public readonly Point Location;


        /// <summary>
        /// 
        /// </summary>
        public void SaveCity(List<byte> data)
        {
            if (DataIndex == 0) return;
            ShortHelper.WriteUInt(data, DataIndex + 18, Reputation);
        }

        public bool IsCity
        {
            get
            {
                return PlaceType == DarkPlaceTypes.City;
            }
        }

        public static bool IsValid(DarkPlace place)
        {
            if (string.IsNullOrEmpty(place.Name)) return false;
            return true;
        }

        public override string ToString()
        {
            if (PlaceType == DarkPlaceTypes.City)
            {
                return "City: " + Name + " Reputation: " + Reputation ;
            }
            return Name + " Type: " + CityTypes;
        }

        public int GetDistToPlace(  DarkPlace placeB)
        {
            return GetDistToPlace(this, placeB);
        }

        /// <summary>
        /// returns the screen distance to a place
        /// </summary> 
        public static int GetDistToPlace(DarkPlace placeA, DarkPlace placeB)
        {
            var source = MapHelper.GetScreenPoint(placeB.Location, Size.Empty);
            var target = MapHelper.GetScreenPoint(placeA.Location, Size.Empty);
            var x = target.X - source.X;
            var y = target.Y - source.Y;
            var result = (int)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            return result;
        }
    }
    public class DarkPlaceList : List<DarkPlace>
    {  
        public DarkPlace Find(string PlaceName)
        {
            PlaceName = PlaceName.ToUpper();
            return Find(delegate(DarkPlace place)
            {
                if (place.Name.ToUpper() == PlaceName) return true;
                return false;
            });
        }

        /// <summary>
        /// returns the closest city to a point
        /// </summary>
        public DarkPlace FindClosestCity(Point ScreenLocation, int closestDistPower)
        {
            var places = GetCities( );
            return GetClosestPlace(ScreenLocation, closestDistPower, places);
        }

        /// <summary>
        /// returns the closest place from a list of places
        /// </summary>
        public static DarkPlace GetClosestPlace(Point ScreenLocation, int closestDistPower, IEnumerable<DarkPlace> Places)
        {
            DarkPlace closestPlace = null;
            foreach (var place in Places)
            {
                var placeLoc = MapHelper.GetScreenPoint(place);
                var dist = new Point(ScreenLocation.X - placeLoc.X, ScreenLocation.Y - placeLoc.Y);
                var distPower = (dist.X * dist.X) + (dist.Y * dist.Y);

                if (distPower < closestDistPower)
                {
                    closestDistPower = distPower;
                    closestPlace = place;
                }
            }
            return closestPlace;
        }

        /// <summary>
        /// returns a city or null
        /// </summary> 
        public DarkPlace GetPlace(int index)
        {
            if(index== 0 ) return null;
            if (index > Count) return null;
            if (index == Count) return this[0];
            return  this[index];
        }

        /// <summary>
        /// returns a list of the place filtered by the place type 'City'
        /// </summary>
        public ReadOnlyCollection<DarkPlace> GetCities( )
        {
            return GetByType(DarkPlaceTypes.City);
        }
        /// <summary>
        /// returns a list of the place filtered by the place type
        /// </summary>
        public ReadOnlyCollection<DarkPlace> GetByType(DarkPlaceTypes placeType)
        {
            var result = new List<DarkPlace>();
            foreach (var place in this)
            {
                if(place.PlaceType==placeType )
                {
                    result.Add(place);
                }
            }
            return result.AsReadOnly();
        }

        public void SortByDistance(DarkPlace Place)
        {
            Sort(new DistanceComparer(Place));
        }


        private class DistanceComparer:IComparer <DarkPlace>
        {
            public DistanceComparer(DarkPlace place)
            {
                Place = place;
            }
            private readonly DarkPlace Place;

            public int Compare(DarkPlace x, DarkPlace y)
            {
                var distX = x.GetDistToPlace(Place);
                var distY = y.GetDistToPlace(Place);
                if (distX > distY) return 1;
                if (distX < distY) return -1;
                return 0;
            }
        }
    }

    public enum DarkCityTypes
    {
        None,
        Large1 = 8,
        Large2 = 7,
        Moderate1 = 6,
        Moderate2 = 5,
        Small1 = 4,
        Small2 = 3,

    }

    public enum DarkPlaceTypes
    {
        None = -1,
        City = 0,
        Unknown1 = 1,
        RobberKnight = 2,
        Unknown3 = 3,
        Unknown4 = 4,
        Cave = 5,
        Unknown5 = 6,
        Village = 8,
        Tomb = 13,
        Lair = 15,
        Spring = 16,
        Lake = 17,
        Shrine = 18,
        Cave2 = 19,
        PaganAltar = 20,
        WitchCult = 21,
        Lugum = 22,
        AlpineCave = 24,
        Halberstadt = 25,
    }
}