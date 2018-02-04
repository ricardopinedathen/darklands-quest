using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DarklandsFiles.Class;

namespace DarklandsFiles.Controller
{
    /// <summary>
    /// a helper controller to manage the new quest list
    /// </summary>
    public class DarklandInfoNewQuestController
    {
        public DarklandInfoNewQuestController()
        {
            newQuestTimer = new Timer();
            newQuestTimer.Interval = 100;
            newQuestTimer.Tick += newQuestTimer_Tick;
        }

          
        private readonly List<DarkQuest> newQuestList = new List<DarkQuest>();
        private readonly Timer newQuestTimer;
        
        public int CurrentNewQuestLife { get; private set; }
        public const int MaxNewQuestLife = 70;

        #region " public events ... "
 
        /// <summary>
        /// NewQuestChanged event
        /// </summary>
        public event Action NewQuestChanged;
        private void OnNewQuestChanged()
        {
            if (NewQuestChanged != null) NewQuestChanged();
        }

        #endregion

        #region " events ... "

        void newQuestTimer_Tick(object sender, EventArgs e)
        {
            if (newQuestList.Count == 0)
            {
                newQuestTimer.Enabled = false;
                return;
            }

            if (CurrentNewQuestLife < 0)
            {
                newQuestList.RemoveAt(0);
                SetNewQuest();
            }
            else
            {
                CurrentNewQuestLife--;
            }
        }

        #endregion

        public void Clear()
        {
            newQuestList.Clear();
            NewQuest = null;
            CurrentNewQuestLife = 0;
        }

        public bool Contains(DarkQuest quest)
        {
            return newQuestList.Contains(quest);
        }

        public void Remove(DarkQuest quest)
        {
            newQuestList.Remove(quest);
            if(NewQuest == quest)
            {
                SetNewQuest();
            }
        }

        /// <summary>
        /// this is the first quest that was added, when u have one file loaded then you load another file,
        /// if the file have a new quest a ref is moved to this variable
        /// </summary>
        public DarkQuest NewQuest { get; private set; }

        /// <summary>
        /// finds the new quest comparing the current quest list with an older one
        /// </summary>
        public void FindNewQuest(List<DarkQuest> questList, List<DarkQuest> oldQuestList)
        { 
            newQuestList.Clear();
            if (oldQuestList.Count == 0) return;

            foreach (var quest in questList)
            {
                var link = DarkQuestLink.Create(quest);
                if (DarkQuestLink.FindQuest(link, oldQuestList) == null)
                {
                    newQuestList.Add(quest);
                }
            }
            SetNewQuest();
            OnNewQuestChanged();
        }

        private void SetNewQuest()
        {
            if (newQuestList.Count > 0)
            {
                CurrentNewQuestLife = MaxNewQuestLife;
                NewQuest = newQuestList[0];
                newQuestTimer.Enabled = true;
            }
            else
            {
                NewQuest = null;
                newQuestTimer.Enabled = false ;
            }
        }
    }
}
