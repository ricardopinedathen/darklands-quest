using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DarklandsFiles.Helper;
using DarklandsFiles.Properties;

namespace DarklandsFiles.Forms
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            chkAutoDelete.Checked = Settings.Default.AutoDelete;
            chkShowWitches.Checked = Settings.Default.ShowWitches;
            txtQuestFontSize.Value = Settings.Default.FontSize;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.Default.AutoDelete = chkAutoDelete.Checked;
            Settings.Default.ShowWitches = chkShowWitches.Checked;
            Settings.Default.ShowWitches = chkShowWitches.Checked;
            Settings.Default.FontSize =  (int)txtQuestFontSize.Value ;
            Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnChangeCharColors_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "File with color data";
            openFileDialog1.ShowDialog();
            var file1 = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(file1)) return;
            if (!File.Exists(file1)) return;

            openFileDialog1.Title = "File to override colors";
            openFileDialog1.ShowDialog();
            var file2 = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(file2)) return;
            if (!File.Exists(file2)) return;

            var controller1 = new DarklandInfoController();
            FileReaderHelper.ReadFile( file1,controller1 );

            var controller2 = new DarklandInfoController();
            FileReaderHelper.ReadFile(file2, controller2);
            controller2.CharactersColors = controller1.CharactersColors;
            FileWriterHelper.SaveGame(file2, controller2);

            MessageBox.Show("Saved the colors, and a backup was created");
        }

    }
}
