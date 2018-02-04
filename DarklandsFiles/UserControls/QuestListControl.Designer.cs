namespace DarklandsFiles.UserControls
{
    partial class QuestListControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.questListBox = new System.Windows.Forms.ListBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hahaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // questListBox
            // 
            this.questListBox.ContextMenuStrip = this.contextMenu;
            this.questListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.questListBox.ItemHeight = 26;
            this.questListBox.Location = new System.Drawing.Point(0, 0);
            this.questListBox.Name = "questListBox";
            this.questListBox.Size = new System.Drawing.Size(148, 160);
            this.questListBox.TabIndex = 0;
            this.questListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.questListBox_DrawItem);
            this.questListBox.SelectedValueChanged += new System.EventHandler(this.QuestList_SelectedValueChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hahaToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(179, 26);
            // 
            // hahaToolStripMenuItem
            // 
            this.hahaToolStripMenuItem.Name = "hahaToolStripMenuItem";
            this.hahaToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.hahaToolStripMenuItem.Text = "Needed for first show";
            // 
            // QuestListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.questListBox);
            this.Name = "QuestListControl";
            this.Size = new System.Drawing.Size(148, 165);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox questListBox;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem hahaToolStripMenuItem;
    }
}
