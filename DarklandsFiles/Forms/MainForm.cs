using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using DarklandsFiles.Class;
using DarklandsFiles.Forms;
using DarklandsFiles.Helper;
using DarklandsFiles.Properties;

namespace DarklandsFiles
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Controller = new DarklandInfoController(); 
            FormText = Text;
            folderWatcher = new FileSystemWatcher();
            folderWatcher.Filter = "*.sav"; 
            folderWatcher.Created += folderWatcher_Created;

            darkMap.Controller = Controller;
            cityListControl.Controller = Controller;
            questListControl.Controller = Controller;
            wealthInfoControl.Controller = Controller;
            dateInfoControl.Controller = Controller;
            externalMenu1.Controller = Controller;
            mapInfoControl.Controller = Controller;

            //select last save path
            if(Settings.Default.LastFolder != null &&
               Directory.Exists(Settings.Default.LastFolder))
            {
                openFileDialog1.InitialDirectory = Settings.Default.LastFolder;

                //open last modified file
                OpenLastModifiedFile();

                Controller.LoadQuestState(Settings.Default.QuestState);
            }

            FormClosed += MainForm_FormClosed;


        }

         


        private readonly DarklandInfoController Controller;
        private readonly string FormText;
        private readonly FileSystemWatcher folderWatcher;
        private readonly Random rnd = new Random( );

        #region " events ... "
        

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.QuestState = Controller.GetCurrentQuestState();
            Settings.Default.Save();
        }

        void folderWatcher_Created(object sender, FileSystemEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);

            DeleteOldModifiedFiles();

            Invoke(new Action(OpenLastModifiedFile));
        }
  
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(file)) return;
            OpenFile(file);
        }

        private void OpenFile(string file)
        {
            FileReaderHelper.ReadFile(file, Controller);
 
            Text = FormText + " " + Controller.SavedGameName + " - " + file;

            var info = new FileInfo(file);
            if (info.Directory != null)
            {
                var dir = info.Directory.FullName;
                Settings.Default.LastFolder = dir;
            }
            else
            {
                Settings.Default.LastFolder = null;
            }
            Settings.Default.Save();
            DeleteOldModifiedFiles();

            //activate the folder watcher
            try
            {
                folderWatcher.Path = Settings.Default.LastFolder + @"\";
                folderWatcher.EnableRaisingEvents = true;
            }
            catch (Exception)
            {
            }
        }

        private void OpenLastModifiedFile()
        { 
            string dir = Settings.Default.LastFolder;
            if (!Directory.Exists(dir)) return;

            var files = new List<string>(Directory.GetFiles(dir, "*.Sav"));
            files.Sort(LastModifiedComparison);
            OpenFile(files[0]);
        }

        #region " DeleteOldModifiedFiles ... "

        private void DeleteOldModifiedFiles( )
        {
            if (!Settings.Default.AutoDelete) return;
            string dir = Settings.Default.LastFolder;
            if (!Directory.Exists(dir)) return;
            
            DeleteOldModifiedFiles(dir, "*.Sav");
            DeleteOldModifiedFiles(dir, "*.BSV");
        }

        private void DeleteOldModifiedFiles(string dir,string searchPattern)
        {
            var backDir = dir + @"\backup\";
            if (!Directory.Exists(backDir)) Directory.CreateDirectory(backDir);

            var files = new List<string>(Directory.GetFiles(dir, searchPattern));
            files.Sort(LastModifiedComparison);
            for (int i = files.Count-1; i > 1; i--)
            {
                var fileinfo = new FileInfo(files[i]);
                var newName = rnd.Next(999999999).ToString("000000000") + fileinfo.Name;
                if(File.Exists(  backDir + newName))File.Delete(backDir + newName ) ;
                File.Move(files[i], backDir + newName); 
            }
        }

        static int LastModifiedComparison(string x, string y)
        {
            var infoX = new FileInfo(x);
            var infoY = new FileInfo(y);
            var OAResult = infoY.LastWriteTime.ToOADate()-infoX.LastWriteTime.ToOADate() ;
            if (OAResult < 0) return -1;
            if (OAResult == 0) return 0;
            return  1;
        }

        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            FileReaderHelper.Test(); 
        }

        private void tabMapImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            darkMap.MapIndex = tabMapImage.SelectedIndex;
        }

        #endregion

        private void btnCheat_Click(object sender, EventArgs e)
        {
            CheaterCaveForm frm = new CheaterCaveForm(Controller );
            frm.ShowDialog();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            OptionsForm frm = new OptionsForm();
            var result =   frm.ShowDialog();  
            if(result != DialogResult.OK ) return;

            FileReaderHelper.ReloadController(Controller);

            try
            {
                folderWatcher.EnableRaisingEvents = Settings.Default.AutoDelete ;
               questListControl.SetFontSize(Settings.Default.FontSize);
            }
            catch (Exception)
            {
            }
        }
 

        #region " events to hide the city list when form its not focus ... "

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = true;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = false ;
        }

        #endregion

    }
}

