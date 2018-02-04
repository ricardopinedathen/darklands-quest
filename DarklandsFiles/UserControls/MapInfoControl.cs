using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DarklandsFiles.Class;
using DarklandsFiles.Properties;

namespace DarklandsFiles.UserControls
{
    public partial class MapInfoControl : Control
    {
        public MapInfoControl()
        {
            InitializeComponent();
            Bitmap backImg = Resources.MapInfo2;
            Width = backImg.Width;
            Height = backImg.Height;

            letterFont = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
        }

        private readonly Font letterFont;

        private DarklandInfoController controller;
        public DarklandInfoController Controller
        {
            get { return controller; }
            set
            {
                controller = value;
                if (controller != null)
                {
                    controller.SelectedPlaceChanged += controller_SelectedPlaceChanged;
                }
            }
        }

        void controller_SelectedPlaceChanged()
        {
            Invalidate();
        }

        /// <summary>
        /// must override to prevent the background from drawing
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        /// <summary>
        /// paint method
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            BufferedGraphicsContext context = new BufferedGraphicsContext();
            var buffer = context.Allocate(pe.Graphics, pe.ClipRectangle);
            var bufferGraphics = buffer.Graphics;

            //draw the background
            Bitmap backImg = Resources.MapInfo2;

            //draw the image
            bufferGraphics.DrawImage(
                backImg,
                0, 0,
                Width, Height);
            
            //draw place info
            DrawPlaceInfo(bufferGraphics );

            buffer.Render();

        }

        /// <summary>
        /// draws the place info
        /// </summary>
        private void DrawPlaceInfo(Graphics graphics)
        { 
            //select current place
            DarkPlace currentPlace;
            if (Controller != null)
            {
                 currentPlace = Controller.SelectedPlace;
            }
            else
            {
                currentPlace = null;
            }

            Point nameLoc = new Point(20, 60);
            if (currentPlace == null)
            {
                //Unknown location
                DrawString(graphics, nameLoc, "...");
                return;
            }

            if (currentPlace.PlaceType == DarkPlaceTypes.City)
            {
                //draw city
                DrawString(graphics, nameLoc, currentPlace.Name);

                nameLoc = new Point(20, 90);
                DrawString(graphics, nameLoc, "Reputation: " + currentPlace.Reputation);
                return;
            }
            //else draw non city places
            DrawString(graphics, nameLoc, currentPlace.Name + " - Not a city");
        }

        private void DrawString(Graphics graphics, Point nameLoc,string words)
        {
            graphics.DrawString(
                words,
                letterFont,
                Brushes.Yellow,
                nameLoc);
        }
    }
}
