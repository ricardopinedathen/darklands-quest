namespace DarklandsFiles.Forms
{
    partial class OptionsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkAutoDelete = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChangeCharColors = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chkShowWitches = new System.Windows.Forms.CheckBox();
            this.txtQuestFontSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuestFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // chkAutoDelete
            // 
            this.chkAutoDelete.AutoSize = true;
            this.chkAutoDelete.Location = new System.Drawing.Point(12, 12);
            this.chkAutoDelete.Name = "chkAutoDelete";
            this.chkAutoDelete.Size = new System.Drawing.Size(241, 17);
            this.chkAutoDelete.TabIndex = 1;
            this.chkAutoDelete.Text = "Auto Move Files, keeps last 2 modified games";
            this.chkAutoDelete.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(130, 124);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 28);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(277, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChangeCharColors
            // 
            this.btnChangeCharColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeCharColors.Location = new System.Drawing.Point(12, 58);
            this.btnChangeCharColors.Name = "btnChangeCharColors";
            this.btnChangeCharColors.Size = new System.Drawing.Size(202, 28);
            this.btnChangeCharColors.TabIndex = 5;
            this.btnChangeCharColors.Text = "Change Character colors from file";
            this.btnChangeCharColors.UseVisualStyleBackColor = true;
            this.btnChangeCharColors.Click += new System.EventHandler(this.btnChangeCharColors_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Darklands saved files|*.sav";
            // 
            // chkShowWitches
            // 
            this.chkShowWitches.AutoSize = true;
            this.chkShowWitches.Location = new System.Drawing.Point(12, 35);
            this.chkShowWitches.Name = "chkShowWitches";
            this.chkShowWitches.Size = new System.Drawing.Size(134, 17);
            this.chkShowWitches.TabIndex = 1;
            this.chkShowWitches.Text = "Show next Witch Party";
            this.chkShowWitches.UseVisualStyleBackColor = true;
            // 
            // txtQuestFontSize
            // 
            this.txtQuestFontSize.Location = new System.Drawing.Point(286, 34);
            this.txtQuestFontSize.Name = "txtQuestFontSize";
            this.txtQuestFontSize.Size = new System.Drawing.Size(71, 20);
            this.txtQuestFontSize.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Quest Font Size";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 164);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQuestFontSize);
            this.Controls.Add(this.btnChangeCharColors);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkShowWitches);
            this.Controls.Add(this.chkAutoDelete);
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.txtQuestFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAutoDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChangeCharColors;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chkShowWitches;
        private System.Windows.Forms.NumericUpDown txtQuestFontSize;
        private System.Windows.Forms.Label label1;
    }
}