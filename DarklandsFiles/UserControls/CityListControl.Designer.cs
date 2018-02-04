namespace DarklandsFiles.UserControls
{
    partial class CityListControl
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
            this.CityList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // CityList
            // 
            this.CityList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CityList.FormattingEnabled = true;
            this.CityList.Location = new System.Drawing.Point(0, 0);
            this.CityList.Name = "CityList";
            this.CityList.Size = new System.Drawing.Size(148, 160);
            this.CityList.TabIndex = 0;
            this.CityList.SelectedValueChanged += new System.EventHandler(this.CityList_SelectedValueChanged);
            // 
            // CityListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CityList);
            this.Name = "CityListControl";
            this.Size = new System.Drawing.Size(148, 165);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox CityList;
    }
}
