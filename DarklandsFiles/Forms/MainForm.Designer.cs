using DarklandsFiles.Class;

namespace DarklandsFiles
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnTest = new System.Windows.Forms.Button();
            this.tabMapImage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheat = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.externalMenu1 = new DarklandsFiles.UserControls.ExternalMenu();
            this.questListControl = new DarklandsFiles.UserControls.QuestListControl();
            this.cityListControl = new DarklandsFiles.UserControls.CityListControl();
            this.dateInfoControl = new DarklandsFiles.UserControls.DateInfoControl();
            this.mapInfoControl = new DarklandsFiles.UserControls.MapInfoControl();
            this.darkMap = new DarklandsFiles.UserControls.DarkMapControl();
            this.wealthInfoControl = new DarklandsFiles.UserControls.WealthInfoControl();
            this.tabMapImage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(12, 2);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(55, 22);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Darklands saved files|*.sav";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(256, 2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(55, 22);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tabMapImage
            // 
            this.tabMapImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMapImage.Controls.Add(this.tabPage1);
            this.tabMapImage.Controls.Add(this.tabPage2);
            this.tabMapImage.Controls.Add(this.tabPage3);
            this.tabMapImage.Location = new System.Drawing.Point(3, 4);
            this.tabMapImage.Name = "tabMapImage";
            this.tabMapImage.SelectedIndex = 0;
            this.tabMapImage.Size = new System.Drawing.Size(478, 31);
            this.tabMapImage.TabIndex = 2;
            this.tabMapImage.SelectedIndexChanged += new System.EventHandler(this.tabMapImage_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(470, 5);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Old Map";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(470, 5);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mix Map";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(470, 5);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Green Map";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackgroundImage = global::DarklandsFiles.Properties.Resources.backGround;
            this.panel1.Controls.Add(this.darkMap);
            this.panel1.Controls.Add(this.tabMapImage);
            this.panel1.Location = new System.Drawing.Point(381, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 512);
            this.panel1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer2.Location = new System.Drawing.Point(12, 130);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.BackgroundImage = global::DarklandsFiles.Properties.Resources.backGround;
            this.splitContainer2.Panel1.Controls.Add(this.questListControl);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.BackgroundImage = global::DarklandsFiles.Properties.Resources.backGround;
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.cityListControl);
            this.splitContainer2.Panel2MinSize = 20;
            this.splitContainer2.Size = new System.Drawing.Size(362, 583);
            this.splitContainer2.SplitterDistance = 343;
            this.splitContainer2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Quest List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "City List";
            // 
            // btnCheat
            // 
            this.btnCheat.Location = new System.Drawing.Point(134, 2);
            this.btnCheat.Name = "btnCheat";
            this.btnCheat.Size = new System.Drawing.Size(55, 22);
            this.btnCheat.TabIndex = 0;
            this.btnCheat.Text = "Cheat";
            this.btnCheat.UseVisualStyleBackColor = true;
            this.btnCheat.Click += new System.EventHandler(this.btnCheat_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(73, 2);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(55, 22);
            this.btnOptions.TabIndex = 0;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // externalMenu1
            // 
            this.externalMenu1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.externalMenu1.Controller = null;
            this.externalMenu1.Location = new System.Drawing.Point(273, 49);
            this.externalMenu1.Name = "externalMenu1";
            this.externalMenu1.Size = new System.Drawing.Size(25, 26);
            this.externalMenu1.TabIndex = 9;
            this.externalMenu1.Visible = false;
            // 
            // questListControl
            // 
            this.questListControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.questListControl.BackColor = System.Drawing.SystemColors.Window;
            this.questListControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.questListControl.Controller = null;
            this.questListControl.Location = new System.Drawing.Point(0, 20);
            this.questListControl.Name = "questListControl";
            this.questListControl.Size = new System.Drawing.Size(362, 320);
            this.questListControl.TabIndex = 9;
            // 
            // cityListControl
            // 
            this.cityListControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cityListControl.BackColor = System.Drawing.SystemColors.Window;
            this.cityListControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cityListControl.Controller = null;
            this.cityListControl.Location = new System.Drawing.Point(0, 27);
            this.cityListControl.Name = "cityListControl";
            this.cityListControl.Size = new System.Drawing.Size(362, 209);
            this.cityListControl.TabIndex = 6;
            // 
            // dateInfoControl
            // 
            this.dateInfoControl.Controller = null;
            this.dateInfoControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateInfoControl.Location = new System.Drawing.Point(12, 30);
            this.dateInfoControl.Name = "dateInfoControl";
            this.dateInfoControl.Size = new System.Drawing.Size(362, 94);
            this.dateInfoControl.TabIndex = 8;
            this.dateInfoControl.Text = "dateInfoControl1";
            // 
            // mapInfoControl
            // 
            this.mapInfoControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mapInfoControl.Controller = null;
            this.mapInfoControl.Location = new System.Drawing.Point(537, 2);
            this.mapInfoControl.Name = "mapInfoControl";
            this.mapInfoControl.Size = new System.Drawing.Size(328, 122);
            this.mapInfoControl.TabIndex = 4;
            this.mapInfoControl.Text = "mapInfoControl1";
            // 
            // darkMap
            // 
            this.darkMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.darkMap.BackColor = System.Drawing.Color.DarkRed;
            this.darkMap.Controller = null;
            this.darkMap.Location = new System.Drawing.Point(0, 26);
            this.darkMap.MapIndex = 0;
            this.darkMap.Name = "darkMap";
            this.darkMap.Size = new System.Drawing.Size(484, 486);
            this.darkMap.TabIndex = 1;
            this.darkMap.Text = "darkMapControl1";
            // 
            // wealthInfoControl
            // 
            this.wealthInfoControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wealthInfoControl.Controller = null;
            this.wealthInfoControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wealthInfoControl.Location = new System.Drawing.Point(388, 2);
            this.wealthInfoControl.Name = "wealthInfoControl";
            this.wealthInfoControl.Size = new System.Drawing.Size(143, 126);
            this.wealthInfoControl.TabIndex = 10;
            this.wealthInfoControl.Text = "wealthInfoControl1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DarklandsFiles.Properties.Resources.backGround;
            this.ClientSize = new System.Drawing.Size(877, 725);
            this.Controls.Add(this.externalMenu1);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.dateInfoControl);
            this.Controls.Add(this.mapInfoControl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnCheat);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.wealthInfoControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Darkland\'s PDA";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.tabMapImage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DarklandsFiles.UserControls.DarkMapControl darkMap;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TabControl tabMapImage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private DarklandsFiles.UserControls.MapInfoControl mapInfoControl;
        private DarklandsFiles.UserControls.CityListControl cityListControl;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private DarklandsFiles.UserControls.DateInfoControl dateInfoControl;
        private System.Windows.Forms.Button btnCheat;
        private System.Windows.Forms.Button btnOptions;
        private DarklandsFiles.UserControls.QuestListControl questListControl;
        private System.Windows.Forms.Label label2;
        private DarklandsFiles.UserControls.ExternalMenu externalMenu1;
        private DarklandsFiles.UserControls.WealthInfoControl wealthInfoControl;
    }
}

