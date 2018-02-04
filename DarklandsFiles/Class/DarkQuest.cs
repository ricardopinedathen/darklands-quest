using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DarklandsFiles.Helper;

namespace DarklandsFiles.Class
{
    public class DarkQuest
    {
        #region " static constructors ... "

        /// <summary>
        /// use to create a return to place kind of quest
        /// </summary> 
        public static DarkQuest CreateReturnQuest(
            DarkDate dateBefore,
            DarkPlace sourcePlace, DarkReturnToPlace questReturnTo)
        {
            return new DarkQuest(null, dateBefore, null, sourcePlace, questReturnTo, DarkQuestTypes.ReturnTo);
        }

        /// <summary>
        /// use to create a goto then after finish return to place kind of quest
        /// </summary> 
        public static DarkQuest CreateGotoThenReturnQuest(
            DarkDate dateBefore,
            DarkPlace sourcePlace, DarkPlace targetPlace, 
            DarkReturnToPlace questReturnTo, DarkQuestTypes darkQuestType)
        {
            return new DarkQuest(null, dateBefore, sourcePlace, targetPlace, questReturnTo, darkQuestType);
        }

        /// <summary>
        /// use to create a goto kind of quest
        /// </summary> 
        public static DarkQuest CreateGotoQuest(
            DarkDate dateAt, DarkPlace targetPlace,
            DarkReturnToPlace questReturnTo, DarkQuestTypes darkQuestType)
        {
            return new DarkQuest(dateAt, null, null, targetPlace, questReturnTo, darkQuestType);
        }

        #endregion

        #region " constructor ... "

        private DarkQuest(
            DarkDate dateAt, DarkDate dateBefore, 
            DarkPlace sourcePlace, DarkPlace targetPlace, DarkReturnToPlace questReturnTo, DarkQuestTypes darkQuestType)
        {
            DateAt = dateAt;
            QuestType = darkQuestType;
            QuestReturnTo = questReturnTo;
            TargetPlace = targetPlace;
            SourcePlace = sourcePlace;
            DateBefore = dateBefore;
        }

        #endregion

        public readonly DarkDate DateAt;
        public readonly DarkDate DateBefore;
        public readonly DarkQuestTypes QuestType;

        public readonly DarkPlace SourcePlace;
        public readonly DarkPlace TargetPlace;
        public readonly DarkReturnToPlace QuestReturnTo;
 
        #region " static helper ... "
        

        /// <summary>
        /// sorts the quest by locations
        /// </summary>
        public static ReadOnlyCollection<DarkQuest> SortQuestByDistance(
            IEnumerable<DarkQuest> quests, DarkPlace Location)
        {
            var placeList = new DarkPlaceList();
            var tempQuests = new Dictionary<DarkPlace, List<DarkQuest>>();
            foreach (var quest in quests)
            {
                var place = quest.TargetPlace;
                //add the place to l8r sort it
                if (!placeList.Contains(place))
                {
                    placeList.Add(place);
                }
                //create the place entry in the dictionary
                if (!tempQuests.ContainsKey(place))
                {
                    tempQuests.Add(place, new List<DarkQuest>());
                }

                //store the quest with the place as key
                tempQuests[place].Add(quest);
            }
            placeList.SortByDistance(Location);

            //get return value
            var result = new List<DarkQuest>();
            foreach (var place in placeList)
            {
                result.AddRange(tempQuests[place]);
            }
            return result.AsReadOnly();
        }


         
        #endregion
    }
    public enum DarkQuestTypes
    {
        None,
        ReturnTo,
        RobberKnight,
        Witches,
        Others,
    }
}
