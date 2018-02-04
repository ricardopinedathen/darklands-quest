using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DarklandsFiles.Class;
using DarklandsFiles.Controller;
using DarklandsFiles.Helper;
using DarklandsFiles.UserControls.MapClass;

namespace DarklandsFiles.UserControls
{
    public partial class DarkMapControl : Control
    {
        public DarkMapControl()
        {
            InitializeComponent();
            AnimationTimer = new Timer();
            AnimationTimer.Interval = 30;
            AnimationTimer.Tick += AnimationTimer_Tick;
            letterFont = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            
            BackgroundDarkenBrush = (new Pen(Color.FromArgb(50, 0, 0, 0))).Brush;
        }

        private readonly Font letterFont;
 
        private int mapIndex;
        private DarkPlace placeHover;
        private DarklandInfoController controller;

        private int SelectionSize;
        private readonly Timer AnimationTimer;
        private readonly Brush BackgroundDarkenBrush;

        private Image backImage;
        private static readonly MapImages mapImages = new MapImages();

        #region " properties ... "

        public DarkPlace PlaceHover
        {
            get { return placeHover; }
            private set
            {
                if (PlaceHover == value) return;
                placeHover = value;
                ChangeSelectedPlaceToHover();
                Invalidate();
            }
        }
          
  
        /// <summary>
        /// the map image to use
        /// </summary>
        public int MapIndex
        {
            get { return mapIndex; }
            set
            {
                mapIndex = value;
                CreateBackImage();
                Invalidate();
            }
        }
        public DarklandInfoController Controller
        {
            get { return controller; }
            set
            {
                if (controller != null)
                {
                    controller.SelectedQuestChanged -= controller_SelectedChanged;
                    controller.SelectedPlaceChanged -= controller_SelectedChanged;
                    controller.NewQuestController.NewQuestChanged -= controller_SelectedChanged;
                }
                controller = value;
                if (controller != null)
                {
                    controller.SelectedQuestChanged += controller_SelectedChanged;
                    controller.SelectedPlaceChanged += controller_SelectedChanged;
                    controller.NewQuestController.NewQuestChanged += controller_SelectedChanged;
                    controller.CityListChanged += controller_CityListChanged;
                }
                CreateBackImage();
                Invalidate();
            }
        } 
 
        #endregion

        private void ActivateAnimationTimer()
        { 
            AnimationTimer.Enabled =
                (Controller.SelectedPlace != null) ||
                (Controller.SelectedQuest != null) ||
                (Controller.NewQuestController.NewQuest != null);
        }

        void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (Controller.SelectedPlace == null &&
                Controller.SelectedQuest == null &&
                Controller.NewQuestController.NewQuest == null) return;

            SelectionSize++;
            if (SelectionSize > 20)
            {
                SelectionSize = 9;
            }
            Invalidate();
        }

        #region " controller events ... "

        private void controller_SelectedChanged( )
        {
            ActivateAnimationTimer();
            Invalidate();
        }

        void controller_CityListChanged()
        {
            CreateBackImage();
        }

        #endregion

        #region " mouse events ... "

        
        protected override void OnMouseLeave(EventArgs e)
        {
            PlaceHover = null;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //find the closest place to the mouse current position
            if (Controller.Places == null) return;
            
            //get values to get closest place
            var closestDist = (20*20) ;//a 20x20 area is the minimun dist

            DarkPlace closestPlace = controller.Places.FindClosestCity(e.Location, closestDist);

            PlaceHover = closestPlace;
             
            base.OnMouseMove(e);
        }

        #endregion

        private void ChangeSelectedPlaceToHover()
        {
            if (PlaceHover != null)
            {
                Controller.SelectedPlace = PlaceHover;
            }
            else
            {
                Controller.SelectedPlace = Controller.CurrentLocation;
            }
        }
 

        #region " draw methods ... "

        /// <summary>
        /// creates the background image
        /// </summary>
        private void CreateBackImage()
        {
            Image mapImage;
            if (mapIndex == 0)
                mapImage = mapImages.map;
            else if (mapIndex == 1)
                mapImage = mapImages.map2;
            else
                mapImage = mapImages.map3;

            if (controller != null)
            {
                DarkPlaceList places = controller.Places;
                backImage = MapMaker.CreateMapImage(mapImage, places, BackgroundDarkenBrush);
            }
            else
            {
                backImage = mapImage;
            }
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
            bufferGraphics.FillRectangle(Brushes.White, pe.ClipRectangle);

            //draw the image
            if (backImage != null)
            {
                bufferGraphics.DrawImage(
                    backImage,
                    0, 0,
                    backImage.Width, backImage.Height);
            }

            //draw controller objects
            DrawControllerObjects(bufferGraphics);

            //draw mouse over location
            if (PlaceHover != null)
            {
                var circleSize = new Size(7, 7);
                Point realPoint = MapHelper.GetScreenPoint(PlaceHover, circleSize);

                bufferGraphics.DrawEllipse(
                    Pens.Yellow, new Rectangle(realPoint, circleSize));

                DrawCityName(bufferGraphics,PlaceHover );
            }

            buffer.Render();
            buffer.Dispose();
        }

        /// <summary>
        /// draws the objects that are related to the controller
        /// </summary>
        private void DrawControllerObjects(Graphics bufferGraphics)
        {
            if (controller == null) return;

            //draw current location
            if (controller.CurrentLocation != null)
            {
                var point = MapHelper.GetScreenPoint(controller.CurrentLocation, mapImages.currentPos.Size);
                point = Point.Add(point, new Size(-3, -3));
                bufferGraphics.DrawImage(
                    mapImages.currentPos,
                    point.X,
                    point.Y);
            }

            //draw selected location
            if (Controller.SelectedPlace != null)
            {
                var circleSize = new Size(SelectionSize, SelectionSize);
                Point realPoint = MapHelper.GetScreenPoint(Controller.SelectedPlace, circleSize);

                bufferGraphics.DrawEllipse(
                    Pens.White, new Rectangle(realPoint, circleSize));
            }

            //draw quest list
            var newQuest = Controller.NewQuestController.NewQuest;
            if (Controller.QuestList != null)
            {
                var circleSize = new Size(7, 7);
                foreach (var quest in Controller.GetNonIgnoredQuestList())
                {
                    if (quest == Controller.SelectedQuest) continue;
                    if (quest == newQuest) continue;
                    DrawQuest(bufferGraphics, circleSize, quest, controller.IsQuestFavorite(quest));
                }
            }

            //draw new quest
            if (newQuest != null &&
                Controller.SelectedQuest != newQuest)
            {
                var size = (int)(30* ((double)Controller.NewQuestController.CurrentNewQuestLife /
                                DarklandInfoNewQuestController.MaxNewQuestLife));

                var circleSize = new Size(size + 5, size + 5);
                DrawQuest(bufferGraphics, circleSize, newQuest,
                          controller.IsQuestFavorite(newQuest));

                WriteNewQuestAtPlace(bufferGraphics, newQuest.TargetPlace);
                WriteNewQuestAtPlace(bufferGraphics, newQuest.SourcePlace);
            }

            //draw selected quest
            if (Controller.SelectedQuest != null)
            {
                var circleSize = new Size(SelectionSize + 1, SelectionSize + 1);
                DrawQuest(bufferGraphics, circleSize, Controller.SelectedQuest,
                          controller.IsQuestFavorite(controller.SelectedQuest));
            }
        }

        /// <summary>
        /// writes "new quest" at the given place
        /// </summary>
        private void WriteNewQuestAtPlace(Graphics bufferGraphics, DarkPlace place)
        {
            if (place != null)
            {
                var p = new Point(
                    place.Location.X,
                    place.Location.Y);
                DrawString(bufferGraphics, "New Quest", p,10);
            }
        }

        /// <summary>
        /// Draws a quest
        /// </summary>
        private static void DrawQuest(
            Graphics bufferGraphics, Size circleSize, DarkQuest Quest, bool IsFavorite)
        {
            DarkPlace TargetPlace;
            DarkPlace SourcePlace; 
            var imageSize = new Size(
                circleSize.Width + 11, 17 + circleSize.Height);
            if (Quest.QuestType == DarkQuestTypes.ReturnTo)
            {
                TargetPlace = null;
                SourcePlace = Quest.TargetPlace;
            }
            else
            {
                TargetPlace = Quest.TargetPlace;
                SourcePlace = Quest.SourcePlace;
            }
 
            //TargetPlace
            if (TargetPlace != null)
            {
                Point realPoint = MapHelper.GetScreenPoint(TargetPlace, circleSize);
                if (Quest.QuestType == DarkQuestTypes.Witches)
                {
                    //witches
                    bufferGraphics.DrawEllipse(Pens.Red, new Rectangle(realPoint, circleSize));
                }
                else if (Quest.QuestType == DarkQuestTypes.RobberKnight)
                {
                    //castle
                    bufferGraphics.DrawImage(mapImages.Castle,
                        realPoint.X - 9, realPoint.Y - 12, imageSize.Width, imageSize.Height);
                } 
                else
                {
                    //Red X
                    bufferGraphics.DrawLine(Pens.Red, realPoint, Point.Add(realPoint, circleSize));
                    bufferGraphics.DrawLine(Pens.Red,
                                            Point.Add(realPoint, new Size(circleSize.Width, 0)),
                                            Point.Add(realPoint, new Size(0, circleSize.Height)));

                    realPoint = Point.Add(realPoint, new Size(1, 0));
                    bufferGraphics.DrawLine(Pens.Crimson, realPoint, Point.Add(realPoint, circleSize));
                    bufferGraphics.DrawLine(Pens.Crimson,
                                            Point.Add(realPoint, new Size(circleSize.Width, 0)),
                                            Point.Add(realPoint, new Size(0, circleSize.Height)));
                }

                DrawFavoriteStar(bufferGraphics, realPoint, IsFavorite);
            }
            //SourcePlace
            if (SourcePlace != null)
            {
                Point realPoint = MapHelper.GetScreenPoint(SourcePlace, circleSize);
                bufferGraphics.DrawRectangle(
                    Pens.Crimson, new Rectangle(realPoint, circleSize));

                if (Quest.QuestType == DarkQuestTypes.ReturnTo)
                {
                    //i reduced the money image cuz looks big
                    bufferGraphics.DrawImage(mapImages.Money,
                        realPoint.X + 10, realPoint.Y, imageSize.Width - 8, imageSize.Height - 8);

                }

                DrawFavoriteStar(bufferGraphics, realPoint, IsFavorite);
            }
        }

        /// <summary>
        /// draws the favorite star in the given point
        /// </summary>
        private static void DrawFavoriteStar(
            Graphics bufferGraphics, Point realPoint, bool IsFavorite)
        {
            if (IsFavorite)
            {
                bufferGraphics.DrawImage(mapImages.FavoriteImage,
                                         realPoint.X - 10, realPoint.Y,
                                         mapImages.FavoriteImage.Width, mapImages.FavoriteImage.Height);
            }
        }

        /// <summary>
        /// draws the given place name
        /// </summary>
        private  void DrawCityName(Graphics bufferGraphics, DarkPlace place)
        {
            var str = string.Format(
                "{0} - Rep: {1}",
                place.Name,
                place.Reputation);
            DrawString(bufferGraphics, str, place.Location, 10); 
        }
      
        #endregion

        private void DrawString(Graphics graphics, string words, Point nameLoc,int margin)
        {
            nameLoc = MapHelper.GetScreenPoint(nameLoc, Size.Empty);
            var measure = graphics.MeasureString(words, letterFont);

            //center the text vertically
            nameLoc = Point.Add(nameLoc, new Size(margin,- (int)(measure.Height / 2)));

            // make sure that if we pass the right border of the image that
            // then we draw in the left part of the city
            if(nameLoc.X + measure.Width >  Width )
            {
                nameLoc = Point.Add(nameLoc, new Size((int)(-measure.Width - (margin*2)), 0)); 
            }

            graphics.DrawString(
                words,
                letterFont,
                Brushes.Black ,
                nameLoc);

            var p2 = Point.Add(nameLoc, new Size(1, 1));
            graphics.DrawString(
                words,
                letterFont,
                Brushes.Yellow,
                p2);
        }
          
    }
}
