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
    public partial class WealthInfoControl : Control
    {
        public WealthInfoControl()
        {
            InitializeComponent();
            Bitmap backImg = Resources.Wealth2;
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
            Bitmap backImg = Resources.Wealth2;

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

            Point nameLoc = new Point(10, 45);
            var words = Controller.Florings + " Florings";
            DrawString(graphics, words, nameLoc);

            nameLoc = new Point(10, 70);
            words = Controller.Groschen + " Groschen";
            DrawString(graphics, words, nameLoc);

            nameLoc = new Point(10, 90);
            words = Controller.Pfenniges + " Pfenniges";
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
