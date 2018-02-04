using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarklandsFiles.Class;

namespace DarklandsFiles
{
    /// <summary>
    /// im still not sure whats this but it got 3 dates, and
    /// so far occurs when u talk to medici fugger or hanse and get rejected,
    /// and contains 48 bytes
    /// </summary>
    public class DarkTalk
    {

        /// <summary>
        /// 48 bits
        /// </summary>
        public DarkTalk(List<byte> bytes, int index, DarkPlaceList Places)
        {

            //from 2 to 25 is dates
            Date1 = DarkDate.Create1(bytes, index + 2);
            Date2 = DarkDate.Create1(bytes, index + 10);
            Date3 = DarkDate.Create1(bytes, index + 18);

            // 26-27 return to location
            QuestReturnTo = (DarkReturnToPlace) ShortHelper.ParseUInt(bytes, index + 26);

            var unknown1 = ShortHelper.ParseUInt(bytes, index);
            var unknown2 = ShortHelper.ParseUInt(bytes, index + 32);
            var unknown3 = ShortHelper.ParseUInt(bytes, index + 34);
            var unknown4 = ShortHelper.ParseUInt(bytes, index + 38);
            var unknown5 = ShortHelper.ParseUInt(bytes, index + 40);
            var unknown7 = ShortHelper.ParseUInt(bytes, index + 44);
            var unknown8 = ShortHelper.ParseUInt(bytes, index + 46);
            
            if (IsQuestRelated || unknown2 == 95 || unknown2 == 99)
            {
                //36-37 is completed 
                IsQuestCompleted = ShortHelper.ParseUInt(bytes, index + 34) == 36;

                //28 to 31 is source and target
                var sourceId = ShortHelper.ParseUInt(bytes, index + 30);
                var targetId = ShortHelper.ParseUInt(bytes, index + 28);
                var otherPlaceId = ShortHelper.ParseUInt(bytes, index + 42);
                SourcePlace = Places.GetPlace(sourceId);
                TargetPlace = Places.GetPlace(targetId);
                OtherPlace = Places.GetPlace(otherPlaceId);
            }
            if (unknown2 == 99 && IsQuestRelated)
            {
                
            }
            if (TargetPlace != null && TargetPlace.Name.StartsWith("Aug"))
            {
            }

            //var Unknown = string.Empty;
            //Unknown += ShortHelper.ParseUInt(bytes, index).ToString("000") + ", ";
            //Unknown += ListHelper.GetString(bytes, index + 32, 48 - 32) ;
            //if (Date3.Day == 22 && Date3.Month == 8)
            //{
            //    Unknown += "Witch! ";
            //}
            //Unknown += ToString();
            //if (OtherPlace != null) Unknown += " OtherPlace: " + OtherPlace.Name;
            //strS += Unknown + Environment.NewLine + Environment.NewLine;
        }

        public static string strS = string.Empty;

        public DarkDate Date1 { get; private set; }
        public DarkDate Date2 { get; private set; }
        public DarkDate Date3 { get; private set; }
        public DarkPlace SourcePlace  { get; private set; }
        public DarkPlace TargetPlace { get; private set; }
        public DarkPlace OtherPlace { get; private set; }
        public DarkReturnToPlace QuestReturnTo { get; private set; }
        public bool IsQuestCompleted { get; private set; }

        public override string ToString()
        {
            var str = "IsQuest=" + IsQuestRelated;
            if (SourcePlace != null)
            {
                str += " Source=" + SourcePlace.Name;
                str += " " + SourcePlace.PlaceType;
            }
            if (TargetPlace != null)
            {
                str += " Target=" + TargetPlace.Name;
                str += " " + TargetPlace.PlaceType;
            }
            if (OtherPlace != null)
            {
                str += " OtherPlace=" + OtherPlace.Name;
                str += " " + OtherPlace.PlaceType;
            }
            return str;
        }

        public bool IsQuestRelated
        {
            get
            { 
                return QuestReturnTo != DarkReturnToPlace.None ;
            }
        }
        public bool IsOtherQuest
        {
            get
            {
                if(OtherPlace==null) return false; 
                return OtherPlace.PlaceType == DarkPlaceTypes.WitchCult ;
            }
        }
        
        public static bool IsValid(DarkTalk talk)
        { 
            if (!DarkDate.IsValid(talk.Date1)) return false;
            if (!DarkDate.IsValid(talk.Date2)) return false;
            if (!DarkDate.IsValid(talk.Date3)) return false;

            return true;
        }
    }
    public enum DarkReturnToPlace
    {
        None = 65534,
        Merchant = 0,
        ForeignTrader = 4,
        Pharmacist = 5,
        Medici = 6,
        Hanse = 7,
        Fugger = 8,
        Village = 9,
        TownHall = 10,

    }
}
