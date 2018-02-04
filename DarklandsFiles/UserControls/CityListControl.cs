using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DarklandsFiles.Class;

namespace DarklandsFiles.UserControls
{
    public partial class CityListControl : UserControl
    {

        public CityListControl()
        {
            InitializeComponent();
        }
         
        private DarklandInfoController controller;

        public DarklandInfoController Controller
        {
            get { return controller; }
            set
            {
                controller = value;
                if (controller != null)
                {
                    controller.CityListChanged += controller_CityListChanged;
                }
            }
        }

        void controller_CityListChanged()
        {
            FillCityList();
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillCityList()
        {
            CityList.Items.Clear();
            if (Controller.Places == null) return;
            foreach (var place in Controller.Places)
            {
                if(place.PlaceType != DarkPlaceTypes.City ) continue;

                CityList.Items.Add(new CityItem(place));
            }
            //sort
            CityList.Sorted = true;
            CityList.Sorted = false ;
            CityList.Items.Insert( 0,new CityItem(null));
        }

        /// <summary>
        /// a helper class used to show what we want in the list
        /// </summary>
        private class CityItem
        {
            public CityItem(DarkPlace place)
            {
                Place = place;
            }

            public readonly DarkPlace Place;

            public override  string ToString()
            {
                if (Place == null) return "None";
                return Place.Name;
            }
        }

        private void CityList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CityList.SelectedItem== null)
            {
                Controller.SelectedPlace = null;
                return;
            }
            var city = ((CityItem) CityList.SelectedItem).Place;
            Controller.SelectedPlace = city;
        }
    }
}
