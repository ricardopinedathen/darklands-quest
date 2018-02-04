using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using DarklandsFiles.Class;
using DarklandsFiles.Controller;
using DarklandsFiles.Helper;

namespace DarklandsFiles
{
    public class DarklandInfoController
    {
        public DarklandInfoController()
        {
            QuestState = new DarklandInfoQuestState();
        }

        private DarkPlace selectedPlace;
        
        public string FileName { get; set; }
        public string SavedGameName { get; set; }

        private DarklandInfoQuestState QuestState;

        public DarkPlace CurrentLocation { get; set; }
        public DarkDate CurrentDate { get; set; }
        public int Groschen { get; set; }
        public int Pfenniges { get; set; }
        public int Florings { get; set; }

        public readonly DarklandInfoNewQuestController NewQuestController = new DarklandInfoNewQuestController() ;

        #region " gui properties ... "

        public DarkQuest SelectedQuest
        {
            get { return DarkQuestLink.FindQuest(QuestState.SelectedQuest, QuestList); }
            set
            {
                if (QuestState.SelectedQuest != value)
                {
                    QuestState.SelectedQuest = DarkQuestLink.Create(value);
                    OnSelectedQuestChanged();
                }
            }
        }
        public DarkPlace SelectedPlace
        {
            get
            {
                return selectedPlace;
            }
            set
            {
                if (selectedPlace != value)
                {
                    selectedPlace = value;
                    OnSelectedPlaceChanged();
                }
            }
        }

        #endregion

        #region " public events  ... "

        /// <summary>
        /// SelectedPlaceChanged event
        /// </summary>
        public event Action SelectedPlaceChanged;
        private void OnSelectedPlaceChanged()
        {
            if (SelectedPlaceChanged != null) SelectedPlaceChanged();
        }

        /// <summary>
        /// SelectedQuestChanged event
        /// </summary>
        public event Action SelectedQuestChanged;
        private void OnSelectedQuestChanged()
        {
            if (SelectedQuestChanged != null) SelectedQuestChanged();
        }


        /// <summary>
        /// GameInfoChanged event, raised when the game data is changed 
        /// </summary>
        public event Action GameInfoChanged;
        private void OnGameInfoChanged()
        {
            if (GameInfoChanged != null) GameInfoChanged();
        }

        /// <summary>
        /// QuestListChanged event
        /// </summary>
        public event Action QuestListChanged;
        private void OnQuestListChanged()
        {
            QuestState.RemoveItemsThatDontExist(QuestList,Places );
            if (QuestListChanged != null) QuestListChanged();
        }

        /// <summary>
        /// City List Changed Event
        /// </summary>
        public event Action CityListChanged;
        private void OnCityListChanged()
        {
            if (CityListChanged != null) CityListChanged();
        }

        #endregion

        public DarkPlaceList Places = new DarkPlaceList();

        public List<byte> FileData = new List<byte>();

        public List<DarkQuest> QuestList = new List<DarkQuest>();

        public List<DarkTalk> DarkTalks = new List<DarkTalk>();

        public List<DarkCharacter> Characters = new List<DarkCharacter>();

        public List<DarkCharacterColor> CharactersColors = new List<DarkCharacterColor>();

        /// <summary>
        /// after the controller is loaded outside of it call this to raise all events
        /// </summary>
        public void Loaded(List<DarkQuest> oldQuestList,DarkDate oldQuestDate)
        {
            FindNewQuests(oldQuestDate, oldQuestList);

            FixWeirdMerchantQuests();

            SelectedPlace = CurrentLocation;

            OnSelectedQuestChanged();
            OnQuestListChanged();
            OnCityListChanged();
            OnGameInfoChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FindNewQuests(DarkDate oldQuestDate, List<DarkQuest> oldQuestList)
        {
            NewQuestController.Clear();
            if (oldQuestDate == null) return;

            var timeStamp = CurrentDate - oldQuestDate;
            if (timeStamp.TotalDays <  50 &&
                timeStamp.TotalDays > -50)
            {
                NewQuestController.FindNewQuest(QuestList, oldQuestList);
            }
        }

        /// <summary>
        /// fix a =b u g= where merchant quest data is found but its not really a quest
        /// </summary>
        private void FixWeirdMerchantQuests()
        {
            var questToRemove = new List<DarkQuest>();
            foreach (var quest in QuestList)
            {
                if (quest.QuestReturnTo != DarkReturnToPlace.Merchant &&
                    (int)quest.QuestReturnTo != 1 &&
                    (int)quest.QuestReturnTo != 2) continue;
                var timeStamp = quest.DateBefore - CurrentDate;
                if (timeStamp.TotalDays > 5) continue;
                if (!NewQuestController.Contains(quest)) continue;

                //kill this QUEST
                questToRemove.Add(quest);
            }

            foreach (var quest in questToRemove)
            {
                NewQuestController.Remove(quest);
                QuestList.Remove(quest);
            }
        }

        /// <summary>
        /// selects the closest city to the current location
        /// </summary>
        public void SelectClosestCity()
        {
            var closestDist = (30 * 30);//a 30x30 area is the minimun dist
            var Location = MapHelper.GetScreenPoint(  CurrentLocation) ;
            DarkPlace closestPlace = Places.FindClosestCity(Location, closestDist);
            SelectedPlace = closestPlace;
        }

        #region " public quest state methods ... "

        /// <summary>
        /// loads the quest state from a string
        /// </summary>
        public void LoadQuestState(string state)
        {
            if (string.IsNullOrEmpty(state)) return;
            try
            {
                var reader = new XmlSerializer(typeof (DarklandInfoQuestState));
                using (var stringReader = new StringReader(state))
                {
                    QuestState = (DarklandInfoQuestState) reader.Deserialize(stringReader);
                }
            }
            catch (Exception)
            {
                QuestState = new DarklandInfoQuestState();
            }
            var selected = SelectedQuest;
            OnQuestListChanged();
            SelectedQuest = selected;
        }

        /// <summary>
        /// returns the quest state in a string
        /// </summary>
        public string GetCurrentQuestState()
        {
            var reader = new XmlSerializer(typeof (DarklandInfoQuestState));
            using (var stringReader = new StringWriter())
            {
                reader.Serialize(stringReader, QuestState);
                return stringReader.ToString();
            }
        }

        #endregion

        #region " public quest filter methods  ... "

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<DarkQuest> GetNonIgnoredQuestList()
        {
            return QuestList.FindAll(IsQuestNotIgnored).AsReadOnly();
        }
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<DarkQuest> GetFavoriteQuests()
        {
            return QuestList.FindAll(IsQuestFavorite).AsReadOnly();
        }
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<DarkQuest> GetNotIgnoredNotFavoriteQuestList()
        {
            return QuestList.FindAll(IsQuestNotFavoriteAndNotIgnored).AsReadOnly();
        }
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<DarkQuest> GetIgnoredQuestList()
        {
            return QuestList.FindAll(IsQuestIgnored).AsReadOnly();
        }

        #endregion

        #region " public quest methods ... "

        /// <summary>
        /// 
        /// </summary>
        public void BlockSelectedQuest()
        {
            if (SelectedQuest == null) return;
            if (QuestState.FavoriteQuestContains(SelectedQuest))
            {
                QuestState.RemoveQuestFromFavorites(SelectedQuest);
            }
            if (!QuestState.IgnoredQuestContains( SelectedQuest))
            {
                QuestState.IgnoreQuest(SelectedQuest);
            }
            OnQuestListChanged();
        }

        /// <summary>
        /// adds the first new quest to favorites
        /// </summary>
        public void AddNewQuestToFavorites()
        {
            var quest = NewQuestController.NewQuest;
            if (quest == null) return;
            if (!QuestState.AddQuestToFavorites(quest)) return;
            OnQuestListChanged();
        }

        /// <summary>
        /// activates a quest by its type or specific
        /// </summary>
        public void ActivateSelectedQuest()
        { 
            if (!QuestState.ActivateQuest(SelectedQuest)) return;
            OnQuestListChanged();
        }

        /// <summary>
        /// activates all the quest 
        /// </summary>
        public void ActivateAllQuest()
        {
            QuestState.ActivateAllQuest();
            OnQuestListChanged();
        }

        /// <summary>
        /// ignores all the quest by type
        /// </summary>
        public void IgnoreSelectedQuestType()
        {
            if (!QuestState.IgnoreQuestType( SelectedQuest)) return;
            OnQuestListChanged();
        }

        /// <summary>
        /// ignores the specific quest
        /// </summary>
        public void IgnoreSelectedQuest ()
        {
            if (!QuestState.IgnoreQuest(SelectedQuest)) return;
            OnQuestListChanged();
        }

        /// <summary>
        /// adds the specifi quest to the favorite list
        /// </summary>
        public void AddSelectedQuestToFavorites()
        {
            if (!QuestState.AddQuestToFavorites(SelectedQuest)) return;
            OnQuestListChanged();
        }

        /// <summary>
        /// removes the specific quest from the favorite list
        /// </summary>
        public void RemoveSelectedQuestFromFavorites()
        {
            if (!QuestState.RemoveQuestFromFavorites(SelectedQuest)) return;
            OnQuestListChanged();
        }

        #endregion

        #region " quest predicates ... "

        //
        public bool IsQuestIgnored(DarkQuest quest)
        {
            return (QuestState.IgnoredQuestContains(quest));
        }
        //
        public bool IsQuestFavorite(DarkQuest quest)
        {
            return (QuestState.FavoriteQuestContains(quest));
        }
        //
        public bool IsQuestNotFavoriteAndNotIgnored(DarkQuest quest)
        {
            return !(QuestState.FavoriteQuestContains(quest) ||
                     QuestState.IgnoredQuestContains(quest));
        }
        //
        public bool IsQuestNotIgnored(DarkQuest quest)
        {
            return (!QuestState.IgnoredQuestContains(quest));
        } 
        //
        public bool IsSelectedQuestIgnored()
        {
            return QuestState.IgnoredQuestContains(SelectedQuest);
        }
        //
        public bool IsSelectedQuestFavorite()
        {
            return QuestState.FavoriteQuestContains(SelectedQuest);
        }


        #endregion

    }
}