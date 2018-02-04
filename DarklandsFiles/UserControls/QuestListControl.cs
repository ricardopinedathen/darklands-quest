using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DarklandsFiles.Class;
using System.Drawing;
using DarklandsFiles.Helper;
using DarklandsFiles.UserControls.MapClass;
using DarklandsFiles.Properties;

namespace DarklandsFiles.UserControls
{
    public partial class QuestListControl : UserControl
    {

        public QuestListControl()
        {
            InitializeComponent();
            SelectedBursh = (new Pen(Color.FromArgb(200, 200, 255))).Brush;
            AlternateBursh = (new Pen(Color.FromArgb(205, 255, 205))).Brush;

            contextMenu.Opening += contextMenu_Opening;
            contextMenu.ItemClicked += contextMenu_ItemClicked;

            SetFontSize(Settings.Default.FontSize );
        }

        private Font NormalFont;
        private Font WarningFont;
         
        private readonly Brush SelectedBursh;
        private readonly Brush AlternateBursh;

        private const string IgnoredQuestType = "Ignore quest type";
        private const string IgnoredQuest  = "Ignore quest";
        private const string UnIgnoredQuest = "Activate";
        private const string UnIgnoredAllQuest = "Activate All";
        private const string MarkQuestAsFavorite = "Add to Favorites";
        private const string UnMarkQuestAsFavorite = "Remove from Favorites";
        
        private DarklandInfoController controller;

        private MapImages Images = new MapImages( );

        private bool ChangingSelection;

        public DarklandInfoController Controller
        {
            get { return controller; }
            set
            {
                if (controller != null)
                {
                    controller.SelectedQuestChanged -= controller_SelectedChanged; 
                    controller.QuestListChanged -= controller_QuestListChanged;
                }
                controller = value;
                if (controller != null)
                {
                    controller.SelectedQuestChanged += controller_SelectedChanged; 
                    controller.QuestListChanged +=controller_QuestListChanged; 
                }
                controller = value;
                SortByDistToCurrent();
            }
        }


        #region " events ... "

        private void controller_SelectedChanged()
        {
            if (ChangingSelection) return;

            //was null
            if (controller.SelectedQuest == null)
            {
                questListBox.SelectedItem = null;
                return;
            }

            //find the selected quest in the list
            foreach (DarkTalkItem item in questListBox.Items)
            {
                if (item.Quest == controller.SelectedQuest)
                {
                    questListBox.SelectedItem = item;
                    return;
                }
            }
            //else didnt find it
            questListBox.SelectedItem = null;
        }

        void controller_QuestListChanged()
        {
            SortByDistToCurrent();
        }


        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var index = questListBox.SelectedIndex;
            switch (e.ClickedItem.Text)
            {
                case IgnoredQuestType:
                    controller.IgnoreSelectedQuestType();
                    questListBox.SelectedIndex = index;
                    break;

                case IgnoredQuest:
                    controller.IgnoreSelectedQuest ();
                    questListBox.SelectedIndex = index;
                    break;

                case UnIgnoredQuest:
                    controller.ActivateSelectedQuest();
                    break;

                case UnIgnoredAllQuest:
                    controller.ActivateAllQuest();
                    break;

                case MarkQuestAsFavorite:
                    controller.AddSelectedQuestToFavorites();
                    break;

                case UnMarkQuestAsFavorite:
                    controller.RemoveSelectedQuestFromFavorites();
                    break;
                    
                    
            }
        }

        void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var location = questListBox.PointToClient(MousePosition);
            SelectQuestAtPoint(location);
            FillContextMenu();
        }  

        private void QuestList_SelectedValueChanged(object sender, EventArgs e)
        {
            ChangingSelection = true;
            if (questListBox.SelectedItem == null)
            {
                controller.SelectedQuest = null;
            }
            else
            {
                controller.SelectedQuest = ((DarkTalkItem)questListBox.SelectedItem).Quest;
            }
            ChangingSelection = false;
        }

        #endregion

        #region " drawing  ... "

        private void questListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) return;
            var item = questListBox.Items[e.Index] as DarkTalkItem;
            if (item == null) return;

            var newBounds = new Rectangle(e.Bounds.Location, new Size(e.Bounds.Width - 1, e.Bounds.Height - 1));
            //draw selected
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(SelectedBursh, newBounds);
                e.Graphics.DrawRectangle(Pens.Gray, newBounds);
            }
            else
            {
                e.DrawBackground();

                //draw alternate background
                if ((int) (e.Index/2F) == (e.Index/2f))
                {
                    e.Graphics.FillRectangle(AlternateBursh, newBounds);
                }
            }

            //draw quest Icon
            var quest = item.Quest;
            if (quest != null)
            {
                if (quest.QuestType == DarkQuestTypes.ReturnTo)
                {
                    DrawImage(e.Graphics, Images.Money, e.Bounds);
                }
                else if (quest.QuestType == DarkQuestTypes.RobberKnight)
                {
                    DrawImage(e.Graphics, Images.Castle, e.Bounds);
                }
                else if (quest.QuestType == DarkQuestTypes.Others)
                {
                    if (quest.TargetPlace.PlaceType == DarkPlaceTypes.City)
                    {
                        DrawImage(e.Graphics, Images.City, e.Bounds);
                    }
                    else if (quest.TargetPlace.PlaceType == DarkPlaceTypes.Lake)
                    {
                        DrawImage(e.Graphics, Images.Lake, e.Bounds);
                    }
                    else if (quest.TargetPlace.PlaceType == DarkPlaceTypes.Cave ||
                             quest.TargetPlace.PlaceType == DarkPlaceTypes.Cave2)
                    {
                        DrawImage(e.Graphics, Images.Cave, e.Bounds);
                    }
                    else if (quest.TargetPlace.PlaceType == DarkPlaceTypes.PaganAltar)
                    {
                        DrawImage(e.Graphics, Images.PaganAltar, e.Bounds);
                    }
                    else if (quest.TargetPlace.PlaceType == DarkPlaceTypes.Spring)
                    {
                        DrawImage(e.Graphics, Images.Spring, e.Bounds);
                    }
                    else if (quest.TargetPlace.PlaceType == DarkPlaceTypes.Shrine)
                    {
                        DrawImage(e.Graphics, Images.Shrine, e.Bounds);
                    } 
                }
                else if (quest.QuestType == DarkQuestTypes.Witches)
                {
                    DrawImage(e.Graphics, Images.Witchs, e.Bounds);
                }

                //is quest ignored
                if (controller.IsQuestIgnored(quest))
                {
                    DrawImage(e.Graphics, Images.Ignored, e.Bounds);
                }
                //is quest favorite
                if (controller.IsQuestFavorite(quest))
                {
                    DrawImage(e.Graphics, Images.Favorite, e.Bounds);
                }
            }

            //get the location of the item
            var fontHeight = (int) (e.Graphics.MeasureString("M", questListBox.Font).Height);
            var location1 = new Point(22, e.Bounds.Y);

            //draw the item
            if (item.IsAboutToExpire())
            {
                DrawWarningString(e.Graphics, item.ToString(), location1);
            }
            else
            {
                DrawString(e.Graphics, item.ToString(), location1);
            }
        }

        private static void DrawImage(Graphics g, Bitmap image, Rectangle bounds)
        {
            var top =  (bounds.Height - image.Height)/2;
            g.DrawImage(image, 1, bounds.Y + top, image.Width, image.Height);
        }

        private void DrawString(Graphics Graphics, string str, Point location)
        {
            Graphics.DrawString(str, NormalFont, Brushes.Black, location);
        }
        private void DrawWarningString(Graphics Graphics, string str, Point location)
        {
            Graphics.DrawString(str, WarningFont, Brushes.Crimson, Point.Add(  location,new Size( 1,0)));
        }

        #endregion

        #region " sort fill methods ... "

        /// <summary>
        /// 
        /// </summary>
        public void SortByName()
        {
            questListBox.Items.Clear();
            if (controller.QuestList == null) return;

            AddQuestToList(controller.GetFavoriteQuests());
            AddQuestToList(controller.QuestList);
            AddQuestToList(controller.GetIgnoredQuestList());

            //sort
            questListBox.Sorted = true;
            questListBox.Sorted = false;
           
            InsertLastQuests();

            SelectFirstQuest();
        } 

        /// <summary>
        /// 
        /// </summary>
        public void SortByDistToCurrent()
        {
            //clear
            questListBox.Items.Clear();

            if (controller == null) return;
            if (controller.QuestList == null) return;

            //create list of quest with dist
            AddQuestToList(SortQuestByDistance(controller.GetFavoriteQuests()));
            AddQuestToList(SortQuestByDistance(controller.GetNotIgnoredNotFavoriteQuestList()));
            AddQuestToList(SortQuestByDistance(controller.GetIgnoredQuestList()));

            InsertLastQuests();

            SelectFirstQuest();
        }

        private IEnumerable<DarkQuest> SortQuestByDistance(IEnumerable<DarkQuest> quests)
        {
            return DarkQuest.SortQuestByDistance(quests, controller.CurrentLocation);
        }

        #endregion
      
        /// <summary>
        /// changes the font size of the quest list
        /// </summary>
        public void SetFontSize(int fontSize)
        {
            if (fontSize < 7) fontSize = 7;
            if (fontSize > 72) fontSize = 72;

            NormalFont = new Font("Microsoft Sans Serif", fontSize, FontStyle.Regular);
            WarningFont = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold);
            
            var g = CreateGraphics();
            questListBox.ItemHeight = (int)(g.MeasureString("A", NormalFont).Height*2) ;
        }


        /// <summary>
        /// Fills the menu with the possible options for the selected item
        /// </summary>
        private void FillContextMenu()
        {
            contextMenu.Items.Clear();
            if (Controller.SelectedQuest != null)
            {

                //favorite / remove favorite
                if (Controller.IsSelectedQuestFavorite())
                {
                    contextMenu.Items.Add(UnMarkQuestAsFavorite);
                }
                else
                {
                    contextMenu.Items.Add(MarkQuestAsFavorite);

                    //ignore / unignore
                    if (Controller.IsSelectedQuestIgnored())
                    {
                        contextMenu.Items.Add(UnIgnoredQuest);
                    }
                    else
                    {
                        contextMenu.Items.Add(IgnoredQuest);
                        contextMenu.Items.Add(IgnoredQuestType);
                    }
                }
            }
            contextMenu.Items.Add(UnIgnoredAllQuest);
        }

        /// <summary>
        /// selects a quest from the list at a point
        /// </summary>
        private void SelectQuestAtPoint(Point location)
        {
            for (int i = 0; i < questListBox.Items.Count; i++)
            {
                var itemRect = questListBox.GetItemRectangle(i);
                var mouseRect = new Rectangle(location, new Size());
                if (itemRect.IntersectsWith(mouseRect))
                {
                    questListBox.SetSelected(i, true);
                    return;
                }
            }
            questListBox.SelectedItem = null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SelectFirstQuest()
        {
            if (questListBox.Items.Count > 1)
                questListBox.SelectedIndex = 1;
            else
                questListBox.SelectedIndex = 0; 
        }

        /// <summary>
        /// 
        /// </summary>
        private void InsertLastQuests()
        {  
            questListBox.Items.Insert(0, new DarkTalkItem(null, null));
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddQuestToList(IEnumerable<DarkQuest> questList)
        {
            foreach (var quest in questList)
            {
                questListBox.Items.Add(new DarkTalkItem(quest, controller));
            }
        }
         

         

        #region " helper list object class ... "

        /// <summary>
        /// a helper class used to show what we want in the list
        /// </summary>
        private class DarkTalkItem
        {
            public DarkTalkItem(DarkQuest quest,   DarklandInfoController controller)
            {
                Quest = quest;
                Controller = controller;
            }

            private readonly DarklandInfoController Controller;

            public readonly DarkQuest Quest;

            public override string ToString()
            {  
                    return GetLine1() + Environment.NewLine +
                           GetLine2(); 
            }

            public string GetLine1()
            {
                if (Quest == null) return "None";
                try
                {
                    //ReturnTo kind of quest
                    if (Quest.QuestType == DarkQuestTypes.ReturnTo)
                    {
                        return GetDescriptionForReturnQuestType_Line1() ;
                    }
                    //else
                    return GetDescriptionForGoToQuest_Line1() ;
                }
                catch (Exception)
                {
                    return "Error";
                }
            }
            public string GetLine2()
            {
                if (Quest == null) return string.Empty ;
                try
                {
                    //ReturnTo kind of quest
                    if (Quest.QuestType == DarkQuestTypes.ReturnTo)
                    {
                        return GetDescriptionForReturnQuestType_Line2();
                    }
                    //else
                    return GetDescriptionForGoToQuest_Line2();
                }
                catch (Exception)
                {
                    return "Error";
                }
            }

            public bool IsAboutToExpire()
            {
                if (Quest  == null) return false;
                if (Quest.DateBefore == null) return false;
                int months = GetMonthsTillDate(Quest.DateBefore);
                if(months < 4) return true;
                return false;
            }

            /// <summary>
            /// go to quests type Description
            /// </summary> 
            private string GetDescriptionForGoToQuest_Line1()
            {
                var dist = Quest.TargetPlace.GetDistToPlace(Controller.CurrentLocation);
                // go to quests
                var str  = "Go to: " + Quest.TargetPlace.PlaceType + " " +
                       Quest.TargetPlace.Name + " Dist: " + dist  ;
                return str;
            }
            private string GetDescriptionForGoToQuest_Line2()
            {
                var str = string.Empty;
                //dates
                if (Quest.DateAt != null)
                {
                    str += GetAtDateDescription ()  + " ";
                }
                else if (Quest.DateBefore != null &&
                         (Quest.DateBefore.Year - Controller.CurrentDate.Year) < 5)
                {
                    str += GetBeforeDateDescription();
                }

                //write returns
                if (Quest.SourcePlace != null) str += "Return to: " + Quest.SourcePlace.Name;
                if (Quest.QuestReturnTo != DarkReturnToPlace.None) str += " (" + Quest.QuestReturnTo + ")";
                return str;
            }

            /// <summary>
            /// Return Quest Type Description
            /// </summary>
            private string GetDescriptionForReturnQuestType_Line1()
            {
                var str = "Return to: " + Quest.QuestReturnTo + " at " +
                       Quest.TargetPlace.PlaceType + " " + Quest.TargetPlace.Name  ; 
                return str;
            }
            private string GetDescriptionForReturnQuestType_Line2()
            {
                var dist = Quest.TargetPlace.GetDistToPlace(Controller.CurrentLocation);
                var str = "Dist: " + dist.ToString("000") + " " + GetBeforeDateDescription();
                return str;
            }
            
            private string GetBeforeDateDescription()
            {
                if (Quest.DateBefore == null) return string.Empty;
                int months = GetMonthsTillDate(Quest.DateBefore);
                if (months > 0)
                {
                    return "Before: " + months + " months ";
                }
                //else
                return "Before: " + Quest.DateBefore +  " ";
            }
            private string GetAtDateDescription()
            {
                if (Quest.DateBefore == null) return string.Empty;
                int months = GetMonthsTillDate(Quest.DateAt);
                if (months > 0)
                {
                    return "At: " + months + " months ";
                }
                //else
                return "At: " + Quest.DateBefore + " ";
            }

            private  int GetMonthsTillDate (DarkDate date)
            { 
                var months = (date.Year - Controller.CurrentDate.Year) * 12;
                months += date.MonthValue() - Controller.CurrentDate.MonthValue();
                if (Controller.CurrentDate.Day > date.Day)
                {
                    months --;
                }
                return months;
            }
        }

        #endregion
         

    }
}
