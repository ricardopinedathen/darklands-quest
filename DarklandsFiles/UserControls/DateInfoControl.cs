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
    public partial class DateInfoControl : Control
    {
        public DateInfoControl()
        {
            InitializeComponent();
            Bitmap backImg = Resources.TimeDate  ;
            Width = backImg.Width;
            Height = backImg.Height;
            letterFont =  new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
                    controller.GameInfoChanged += controller_GameInfoChanged;
                }
            }
        }

        void controller_GameInfoChanged()
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
            Bitmap backImg = Resources.TimeDate;

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
            if (Controller == null) return;
            if (Controller.CurrentDate == null) return;

            Point nameLoc = new Point(75, 55);
            var words = Controller.CurrentDate.Time.ToString()   ;
            DrawString(graphics, words, nameLoc);

            nameLoc = new Point(230, 40);
            words = Controller.CurrentDate.MonthName();  
            DrawString(graphics, words, nameLoc);

            nameLoc = new Point(230, 65);
            words = Controller.CurrentDate.Day + ", " + Controller.CurrentDate.Year;
            DrawString(graphics, words, nameLoc);
             
        }

        private void DrawString(Graphics graphics, string words, Point nameLoc)
        {
            graphics.DrawString(
                words,
                letterFont,
                Brushes.Yellow,
                nameLoc);
        }
    }
}
