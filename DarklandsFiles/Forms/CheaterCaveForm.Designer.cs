namespace DarklandsFiles.Forms
{
    partial class CheaterCaveForm
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
            this.components = new System.ComponentModel.Container();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCityReputation = new System.Windows.Forms.Button();
            this.btnFlorins = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(DarklandsFiles.DarklandInfoController);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(179, 77);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCityReputation
            // 
            this.btnCityReputation.Location = new System.Drawing.Point(159, 6);
            this.btnCityReputation.Name = "btnCityReputation";
            this.btnCityReputation.Size = new System.Drawing.Size(141, 28);
            this.btnCityReputation.TabIndex = 4;
            this.btnCityReputation.Text = "Give City Reputation";
            this.btnCityReputation.UseVisualStyleBackColor = true;
            this.btnCityReputation.Click += new System.EventHandler(this.btnCityReputation_Click);
            // 
            // btnFlorins
            // 
            this.btnFlorins.Location = new System.Drawing.Point(12, 6);
            this.btnFlorins.Name = "btnFlorins";
            this.btnFlorins.Size = new System.Drawing.Size(141, 28);
            this.btnFlorins.TabIndex = 4;
            this.btnFlorins.Text = "Give 1000 Florings";
            this.btnFlorins.UseVisualStyleBackColor = true;
            this.btnFlorins.Click += new System.EventHandler(this.btnFlorins_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(32, 77);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CheaterCaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 117);
            this.Controls.Add(this.btnFlorins);
            this.Controls.Add(this.btnCityReputation);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "CheaterCaveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cheater Cave";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Button btnCityReputation;
        private System.Windows.Forms.Button btnFlorins;
        private System.Windows.Forms.Button btnSave;
    }
}