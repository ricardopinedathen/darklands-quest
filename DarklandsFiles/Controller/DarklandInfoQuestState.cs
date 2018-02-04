using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarklandsFiles.Class;

namespace DarklandsFiles.Controller
{
    [Serializable]
    public class DarklandInfoQuestState
    {
        public DarklandInfoQuestState()
        {
            IgnoredQuestTypeList = new List<DarkPlaceTypes>();
            IgnoredQuestList = new List<DarkQuestLink>();
            FavoriteQuestList = new List<DarkQuestLink>();
        }

        public DarkQuestLink SelectedQuest { get; set; }
        public List<DarkPlaceTypes> IgnoredQuestTypeList { get; set; }
        public List<DarkQuestLink> IgnoredQuestList { get; set; }
        public List<DarkQuestLink> FavoriteQuestList { get; set; }

        /// <summary>
        /// removes the quest links if they arent pointint to anything in the given list
        /// </summary>
        public void RemoveItemsThatDontExist(List<DarkQuest> list, DarkPlaceList placeList)
        { 
            RemoveItemsThatDontExist(FavoriteQuestList, list,placeList );
            RemoveItemsThatDontExist(IgnoredQuestList, list, placeList);
        }

        /// <summary>
        /// activates the given ignored quest 
        /// </summary>
        public bool ActivateQuest(DarkQuest quest)
        {
            if (quest == null) return false ;
            if (!IgnoredQuestContains(quest)) return false;

            if (quest.TargetPlace != null)
            {
                IgnoredQuestTypeList.Remove(quest.TargetPlace.PlaceType);
            }

            var link = FindIgnoredQuestLink(quest);
            if (link != null)
            {
                IgnoredQuestList.Remove(link);
            }
            return true;
        }

        /// <summary>
        /// activates all ignored quests
        /// </summary>
        public void ActivateAllQuest()
        {
            IgnoredQuestTypeList.Clear();
            IgnoredQuestList.Clear();
        }

        /// <summary>
        /// ignores all the quest by type
        /// </summary>
        public bool IgnoreQuestType(DarkQuest quest)
        {
            if (quest == null) return false;
            if (IgnoredQuestContains(quest)) return false;

            IgnoredQuestTypeList.Add(quest.TargetPlace.PlaceType);
            return true;
        }
        /// <summary>
        /// ignores the specific quest
        /// </summary>
        public bool IgnoreQuest(DarkQuest quest )
        {
            if (quest == null) return false;
            if (IgnoredQuestContains(quest)) return false;
            if (FavoriteQuestContains(quest)) return false;

            IgnoredQuestList.Add(  DarkQuestLink.Create( quest));
            return true;
        }

        /// <summary>
        /// removes the specific quest from the favorite list
        /// </summary>
        public bool RemoveQuestFromFavorites(DarkQuest quest)
        {
            if (quest == null) return false;

            var link = FindFavoriteQuestLink(quest);
            if (link == null) return false;

            FavoriteQuestList.Remove(link);
            return true;
        }
        /// <summary>
        /// adds the specifi quest to the favorite list
        /// </summary>
        public bool AddQuestToFavorites(DarkQuest quest)
        {
            if (quest == null) return false;
            if (FavoriteQuestContains(quest)) return false ;

            var ignoredLink = FindIgnoredQuestLink(quest);
            if (ignoredLink != null)
            {
                IgnoredQuestList.Remove(ignoredLink);
            }

            FavoriteQuestList.Add(DarkQuestLink.Create( quest));
            return true;
        }

        /// <summary>
        /// returns true if a quest is ignored
        /// </summary>
        public bool IgnoredQuestContains(DarkQuest quest)
        {
            if (FavoriteQuestContains(quest)) return false;
            if (IgnoredQuestTypeList.Contains(quest.TargetPlace.PlaceType)) return true;
            if (FindIgnoredQuestLink(quest) != null) return true;
            return false;
        }

        public bool FavoriteQuestContains(DarkQuest quest)
        {
            if (FindFavoriteQuestLink(quest) != null) return true;
            return false;
        }

        #region " private methods  ... "

        private DarkQuestLink FindIgnoredQuestLink(DarkQuest quest)
        {
            return DarkQuestLink.FindQuestLink(quest, IgnoredQuestList);
        }

        private DarkQuestLink FindFavoriteQuestLink(DarkQuest quest)
        {
            return DarkQuestLink.FindQuestLink(quest, FavoriteQuestList);
        }

        #endregion

        /// <summary>
        /// remove items that dont exist in the given list
        /// </summary>
        private static void RemoveItemsThatDontExist(
            List<DarkQuestLink> LinkQuestList, List<DarkQuest> QuestList, DarkPlaceList placeList)
        {
            //first get a list of all the items that we should remove
            List<DarkQuestLink> LinksToRemove = new List<DarkQuestLink>();
            foreach (var item in LinkQuestList)
            {
                if (DarkQuestLink.FindQuest(item, QuestList) == null)
                {
                    LinksToRemove.Add(item);
                }
            }
            foreach (var linkToRemove in LinksToRemove)
            {
                //check if the quest became a return to kind of quest
                if ((linkToRemove.QuestType == DarkQuestTypes.RobberKnight ||
                     linkToRemove.QuestType == DarkQuestTypes.Others) &&
                    !string.IsNullOrEmpty(  linkToRemove.SourceName))
                {
                    //create the return quest link of woot the quest should be
                    var returnPlace = placeList.Find(linkToRemove.SourceName);
                    var returnQuest = DarkQuest.CreateReturnQuest(DarkDate.Empty, returnPlace, linkToRemove.QuestReturnTo );
                    var item = DarkQuestLink.Create(returnQuest);

                    //find it new return quest that it should be
                    if (DarkQuestLink.FindQuest(item, QuestList) != null)
                    {
                        //add the transformed quest to the list
                        LinkQuestList.Add(item);
                    }
                }

                //remove the old link
                LinkQuestList.Remove(linkToRemove);
            }
        }
    }
}
