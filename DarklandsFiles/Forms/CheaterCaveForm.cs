using System;
using System.Windows.Forms;
using DarklandsFiles.Helper;

namespace DarklandsFiles.Forms
{
    public partial class CheaterCaveForm : Form
    {
        public CheaterCaveForm(DarklandInfoController controller)
        {
            InitializeComponent();
            Controller = controller;
            bindingSource.DataSource = Controller;
        }

        private readonly DarklandInfoController Controller;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FileWriterHelper.SaveGame(Controller.FileName, Controller);
            Close();
        }

        private void btnFlorins_Click(object sender, EventArgs e)
        {
            if (Controller == null) return;
            Controller.Florings = 1000;
            MessageBox.Show("Now you are fucking rich, go buy some horses.",
                "Cheater!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
       
        }
        private void btnCityReputation_Click(object sender, EventArgs e)
        {
            if (Controller == null) return;
            foreach (var place in Controller.Places)
            {
                if (!place.IsCity) continue;
                place.Reputation = 120;
            }
            MessageBox.Show("Now you are a local hero in all city.",
                "Cheater!!!",MessageBoxButtons.OK,MessageBoxIcon.Hand  );
        }


    }
}
