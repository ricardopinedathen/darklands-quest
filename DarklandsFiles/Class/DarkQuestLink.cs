using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarklandsFiles.Class
{
    /// <summary>
    /// this class defines things to look in a dark quest to find it and select it across multiple files
    /// </summary>
    [Serializable]
    public class DarkQuestLink  
    {
        public static  DarkQuestLink Create(DarkQuest quest)
        {
            if(quest== null) return null;
            return new DarkQuestLink(quest );
        }
        /// <summary>
        /// constructor for serialization
        /// </summary>
        private DarkQuestLink(   )
        {
        }

        private DarkQuestLink(DarkQuest quest)
        { 
            if (quest.TargetPlace != null)
            {
                TargetName = quest.TargetPlace.Name;
            }
            if (quest.SourcePlace != null)
            {
                SourceName = quest.SourcePlace.Name;
            }
            QuestType = quest.QuestType;
            QuestReturnTo = quest.QuestReturnTo;
        }

        public string TargetName { get; set; }
        public string SourceName { get; set; }
        public DarkQuestTypes QuestType { get; set; }
        public DarkReturnToPlace QuestReturnTo { get; set; }

        #region " operators ... "

        public static bool operator ==(DarkQuestLink a, DarkQuest b)
        {
            if (ReferenceEquals(a, null))
            {
                if (ReferenceEquals(b, null))
                {
                    return true;
                }
                //else
                return false;
            }
            //else
            if (ReferenceEquals(b, null))
            {
                return false;
            }

            if (!ComparePlaceAndPlaceName(b.TargetPlace, a.TargetName)) return false;
            if (!ComparePlaceAndPlaceName(b.SourcePlace, a.SourceName)) return false;
            if (a.QuestReturnTo != b.QuestReturnTo) return false;
            if (a.QuestType != b.QuestType) return false;
            return true;
        }
        public static bool operator !=(DarkQuestLink a, DarkQuest b)
        {
            return !(a == b);
        } 

        #endregion

        private static bool ComparePlaceAndPlaceName(DarkPlace place, string placeName)
        {
            if (place != null)
            {
                if (string.IsNullOrEmpty(placeName))
                {
                    return false;
                }
                if (place.Name != placeName)
                {
                    return false;
                }
            }
            else if (!string.IsNullOrEmpty(placeName))
            {
                return false;
            }
            return true;
        }

        #region " public static helpers ... "

        /// <summary>
        /// finds a quest link in a quest link list using a quest
        /// </summary>
        public static DarkQuestLink FindQuestLink(DarkQuest quest, IEnumerable<DarkQuestLink> list)
        {
            foreach (var questLink in list)
            {
                if (questLink == quest)
                {
                    return questLink;
                }
            }
            return null;
        }
        /// <summary>
        /// finds a quest in a quest list using a quest link
        /// </summary>
        public static DarkQuest FindQuest (DarkQuestLink questLink, IEnumerable<DarkQuest> list)
        {
            foreach (var quest  in list)
            {
                if (questLink == quest)
                {
                    return quest;
                }
            }
            return null;
        }

        #endregion

        #region " Equality members ... "

        public bool Equals(DarkQuestLink obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.TargetName, TargetName) &&
                   Equals(obj.SourceName, SourceName) &&
                   Equals(obj.QuestType, QuestType) &&
                   Equals(obj.QuestReturnTo, QuestReturnTo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(DarkQuestLink)) return false;
            return Equals((DarkQuestLink)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (TargetName != null ? TargetName.GetHashCode() : 0);
                result = (result * 397) ^ (SourceName != null ? SourceName.GetHashCode() : 0);
                result = (result * 397) ^ QuestType.GetHashCode();
                result = (result * 397) ^ QuestReturnTo.GetHashCode();
                return result;
            }
        }
        #endregion
    }
}
