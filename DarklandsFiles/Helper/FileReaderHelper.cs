using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO; 
using DarklandsFiles.Class;
using DarklandsFiles.Properties;

namespace DarklandsFiles.Helper
{
    class FileReaderHelper
    {
        #region " test methods ... "

        public static void Test()
        {
            var f1 = @"C:\Games\darkedit\SAVES\DKSAVE0.SAV";
            var f2 = @"C:\Games\darkedit\SAVES\DKSAVE1.SAV";
            var f3 = @"C:\Games\darkedit\SAVES\DKSAVE2.SAV";
            if (!File.Exists(f2))
            {
                f2 = f1;
            }
            else if (!File.Exists(f3))
            {
                f3 = f1;
            }
            //make f3 the oldest
            if (File.GetLastWriteTime(f2) > File.GetLastWriteTime(f3))
            {
                var temp = f3;
                f3 = f2;
                f2 = temp;
            }
            var data2 = new List<byte>(File.ReadAllBytes(f2));
            var data3 = new List<byte>(File.ReadAllBytes(f3));
            var data4a = new List<int>();


            for (int i = 0; i < data2.Count; i++)
            {
                var b = data2[i];
                if (b == 12)
                {
                    data4a.Add(i);
                }
            }
            var data4c = new List<int>();
            for (int i = 0; i < data3.Count; i++)
            {
                var b = data3[i];
                if (b == 12 && data4a.Contains(i))
                {
                    data4c.Add(i);
                }
            }

            ClearUnknown(data2);
            ClearUnknown(data3);

            var list3 = new List<string>();
            //read the characters
            //var CharCount = ShortHelper.ParseUInt(data3, 241);
            //var CharIndex = 393;
            //for (int i = 0; i < 393 + (DarkCharacter.CharacterLength * CharCount); i++)
            //{
            //    if (data2[i] != data3[i])
            //    {
            //        list3.Add(string.Format(
            //            "Pos 1: {0} Val: {1}, {2}", i, data2[i], data3[i]));
            //    }
            //}
            //for (int i = index; i < 8000; i++)
            //{
            //    if (data2[i] != data3[i])
            //    {
            //        list3.Add(string.Format(
            //            "Pos 2: {0} Val: {1}, {2}", i, data2[i], data3[i]));
            //    }
            //} 
            var str = string.Empty;
            foreach (var data in list3)
            {
                str += data + Environment.NewLine;
            }
        }

        private static void ClearUnknown(List<byte> data2)
        {
            //CurrentLocationName
            for (int i = 0; i < 13; i++)
            {
                data2[i] = 0;
            }

            //clear SavedGame Name
            for (int i = 21; i < 21 + 40; i++)
            {
                data2[i] = 0;
            }
            //clear date data
            for (int i = 104; i < 104 + 8; i++)
            {
                data2[i] = 0;
            }
            //clear money data
            for (int i = 112; i < 118; i++)
            {
                data2[i] = 0;
            }
        }

        #endregion

        public static  void ReloadController( DarklandInfoController controller)
        {
            ReadFile(controller.FileName , controller);
        }

        public static  void ReadFile(string file,DarklandInfoController controller)
        {
            if (!File.Exists(file)) return;
            var oldCurrentDate = controller.CurrentDate;

            controller.FileName = file;

            controller.FileData.Clear();
            controller.FileData.AddRange(File.ReadAllBytes(file));

            //the current date
            controller.CurrentDate = DarkDate.Create2(controller.FileData, 104);
            if (!DarkDate.IsValid(controller.CurrentDate)) throw new Exception("invalid date");

            //read the current money
            controller.Florings = ShortHelper.ParseUInt(controller.FileData, 112);
            controller.Groschen = ShortHelper.ParseUInt(controller.FileData, 114);
            controller.Pfenniges = ShortHelper.ParseUInt(controller.FileData, 116);

            controller.SavedGameName = ListHelper.Read(controller.FileData, 21, 40);

            controller.Places.Clear();
            controller.Places.AddRange(PlaceHelper.ReadPlaces(controller.FileData));

            //get location   
            ReadTheLocation(controller);

            //read the characters color
            controller.CharactersColors.Clear();
            for (int i = 0; i < 4; i++)
            {
                var dataIndex = DarkCharacterColor.StartPoint + (DarkCharacterColor.Size * i);
                controller.CharactersColors.Add(new DarkCharacterColor(controller.FileData, dataIndex));
            }

            //read the characters
            var CharCount = ShortHelper.ParseUInt(controller.FileData, 241);
            var CharIndex = 393;
            for (int i = 0; i < CharCount; i++)
            {
                var CharacterLoc = CharIndex + (DarkCharacter.CharacterLength * i);
                var Character = new DarkCharacter(controller.FileData, CharacterLoc);
                if (DarkCharacter.IsValid(Character))
                {
                    controller.Characters.Add(Character);
                }
                else
                {
                    throw new Exception("ah");
                }
            }

            //read dark talks
            var amountOfDataLoc = 393 + (DarkCharacter.CharacterLength * CharCount);
            var amountOfData = ShortHelper.ParseUInt(controller.FileData, amountOfDataLoc);
            var index = 395 + (DarkCharacter.CharacterLength * CharCount);
            controller.DarkTalks.Clear();
            if (amountOfData > controller.DarkTalks.Capacity) controller.DarkTalks.Capacity = amountOfData;

            for (int i = 0; i < amountOfData; i++)
            {
                DarkTalk talk = new DarkTalk(controller.FileData, index + i * 48, controller.Places);
                if (DarkTalk.IsValid(talk))
                {
                    controller.DarkTalks.Add(talk);
                }
                else
                {
                    throw new Exception("ah");
                }
            }
            var oldQuestList = new List<DarkQuest>(controller.QuestList);

            FillQuestList(controller);

            controller.Loaded(oldQuestList, oldCurrentDate);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ReadTheLocation(DarklandInfoController controller)
        {
            var CurrentLocationName = ListHelper.Read(controller.FileData, 12);
            controller.CurrentLocation = controller.Places.Find(CurrentLocationName);
            if (controller.CurrentLocation == null)
            {
                var CurrentLocationX = ShortHelper.ParseUInt(controller.FileData, 126);
                var CurrentLocationY = ShortHelper.ParseUInt(controller.FileData, 128);
                controller.CurrentLocation = new DarkPlace(
                    CurrentLocationName,
                    new Point(CurrentLocationX, CurrentLocationY),
                    DarkCityTypes.None, DarkPlaceTypes.None);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void FillQuestList(DarklandInfoController controller)
        {
            controller.QuestList.Clear();
            if (controller.DarkTalks == null) return;
            foreach (var darkTalk in controller.DarkTalks)
            {
                DarkQuest quest = GetQuest(darkTalk);
                if (quest == null) continue;

                if(quest.QuestType == DarkQuestTypes.Witches &&
                   !Settings.Default.ShowWitches ) continue;

                controller.QuestList.Add(quest);
            }
        }

        /// <summary>
        /// creates a quest from a talk data
        /// </summary>
        private static DarkQuest GetQuest(DarkTalk darkTalk)
        {
            if (darkTalk.IsQuestRelated && !darkTalk.IsOtherQuest)
            {
                if (darkTalk.IsQuestCompleted)
                {
                    //return kind of quest
                    var quest2 = DarkQuest.CreateReturnQuest(
                        darkTalk.Date3, darkTalk.TargetPlace, darkTalk.QuestReturnTo);
                    return quest2;
                }
                //normal NOT STARTED quest
                DarkQuestTypes questType;
                if (darkTalk.TargetPlace == null) return null;
                if (darkTalk.TargetPlace.PlaceType == DarkPlaceTypes.RobberKnight)
                {
                    questType = DarkQuestTypes.RobberKnight;
                }
                else
                {
                    questType = DarkQuestTypes.Others;
                }
                var quest = DarkQuest.CreateGotoThenReturnQuest(
                    darkTalk.Date3, darkTalk.SourcePlace, darkTalk.TargetPlace,
                    darkTalk.QuestReturnTo, questType);
                return quest;
            }
            if (darkTalk.IsOtherQuest)
            {
                //non return quest
                DarkQuestTypes questType;
                if (darkTalk.OtherPlace.PlaceType == DarkPlaceTypes.WitchCult)
                {
                    questType = DarkQuestTypes.Witches;
                }
                else
                {
                    questType = DarkQuestTypes.Others;
                }
                var quest = DarkQuest.CreateGotoQuest(
                    darkTalk.Date3, darkTalk.OtherPlace,
                    darkTalk.QuestReturnTo, questType);
                return quest;
            }
            return null;
        }
   
    }
}
