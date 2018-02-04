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
    public partial class ExternalMenu : UserControl
    {
        public ExternalMenu()
        {
            InitializeComponent();

            KeyWatcher = new Timer();
            KeyWatcher.Interval = 1;
            KeyWatcher.Enabled = true;
            KeyWatcher.Tick += KeyWatcher_Tick;
            cityButtons.Add(lblClosestCity1);
            cityButtons.Add(lblClosestCity2);
            cityButtons.Add(lblClosestCity3);
            cityButtons.Add(lblClosestCity4);
            cityButtons.Add(lblClosestCity5);
        }

        private readonly Timer KeyWatcher;
        private bool Active;
        private bool CanActive;
        private Point OldMousePosition;
        private Point LastMousePosition;
        private int TimesFailedToMove;
        private readonly DarkPlaceList cities = new DarkPlaceList();
        private readonly List<Label> cityButtons = new List<Label>();

        private DarklandInfoController controller;
        public DarklandInfoController Controller
        {
            get { return controller; }
            set
            {
                controller = value;
                if (controller != null)
                {
                    controller.GameInfoChanged += controller_GameInfoChanged;
                }
            }
        }

        void controller_GameInfoChanged()
        {
            LoadClosestCities();
        }

        /// <summary>
        /// loads the list of closest places
        /// </summary>
        private void LoadClosestCities()
        {
            cities.Clear();
            if (controller == null) return;
            cities.AddRange(controller.Places.GetCities());
            cities.SortByDistance(controller.CurrentLocation);
            for (int i = 0; i < 5; i++)
            {
                cityButtons[i].Text = GetCityText(cities[i]);
            }
        }

        private string GetCityText(DarkPlace place)
        {
            var dist = place.GetDistToPlace(controller.CurrentLocation);
            return string.Format("{0}{3} Rep: {1}, Dist: {2}",
                place.Name, place.Reputation, dist, Environment.NewLine);
        }

        void KeyWatcher_Tick(object sender, EventArgs e)
        {
            if (Controller == null) return;
            var mods = Keys.Control | Keys.Shift;
            var curMods = ModifierKeys;
            if (curMods == mods)
            {
                if (!CanActive) return;
                if (!Active)
                {
                    //activating
                    Width = 500;
                    Height = 350;
                    Visible = true;
                    Active = true;
                    TimesFailedToMove = 0;
                    ResetMouse();
                    Focus();
                    lblSymbols.Visible = false;
                }
                else
                {
                    MoveOurMouse();
                }
            }
            else
            {
                CanActive = true;
                Active = false;
                Visible = false;
            }
        }

        private void MoveOurMouse()
        {
            if (MousePosition == OldMousePosition && LastMousePosition != OldMousePosition)
            {
                TimesFailedToMove++;
                if (TimesFailedToMove > 3)
                {
                    lblWarning.Visible = true;
                }
            }
            LastMousePosition = MousePosition;

            //first get the movement made
            var TotalMoment = Size.Subtract((Size)MousePosition, (Size)OldMousePosition);
            lblMouse.Location = Point.Add(GetCenterMousePoint(), TotalMoment);

            if (Controller == null) return;

            //verify buttons
            var mouseRect = lblMouse.Bounds;
            if (mouseRect.IntersectsWith(lblAddNewQuestToFav.Bounds))
            {
                Controller.AddNewQuestToFavorites();
                ResetMouse();
                Deactivate();
                return;
            }
            if (mouseRect.IntersectsWith(lblBlockSelected.Bounds))
            {
                Controller.BlockSelectedQuest();
                ResetMouse();
                Deactivate();
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                if (mouseRect.IntersectsWith(cityButtons[i].Bounds))
                {
                    Controller.SelectedPlace = cities[i];
                    ResetMouse();
                    Deactivate();
                    return;
                }
            }
            if (mouseRect.IntersectsWith(lblShowSymbols.Bounds))
            { 
                ResetMouse();
                lblSymbols.Visible = true;
                lblSymbols.BringToFront();
                lblSymbols.Size = lblSymbols.Image.Size;
                lblSymbols.Location = new Point(0, 0);
                Size = lblSymbols.Image.Size;
                CanActive = false;
                return;
            }
        }


        private void Deactivate()
        {
            CanActive = false;
            Visible = false;
        }

        private void ResetMouse()
        {
            OldMousePosition = MousePosition;
            LastMousePosition = MousePosition;
            lblMouse.Location = GetCenterMousePoint();
            lblWarning.Visible = false;
        }

        private Point GetCenterMousePoint()
        {
            return new Point(Width / 2 + lblMouse.Width / 2, Height / 2 + lblMouse.Height / 2);
        }

    }
}
