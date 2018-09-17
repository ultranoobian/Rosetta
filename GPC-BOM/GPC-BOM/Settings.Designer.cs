namespace GPC_BOM {
    partial class Settings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tbLevel = new System.Windows.Forms.TextBox();
            this.gbColumns = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblCPN = new System.Windows.Forms.Label();
            this.tbCPN = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.tbQuantity = new System.Windows.Forms.TextBox();
            this.lblDesignator = new System.Windows.Forms.Label();
            this.tbDesignator = new System.Windows.Forms.TextBox();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.tbManufacturer = new System.Windows.Forms.TextBox();
            this.lblMPN = new System.Windows.Forms.Label();
            this.tbMPN = new System.Windows.Forms.TextBox();
            this.lblProcess = new System.Windows.Forms.Label();
            this.tbProcess = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.lblCooltip = new System.Windows.Forms.Label();
            this.gbColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLevel
            // 
            this.tbLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLevel.Location = new System.Drawing.Point(85, 19);
            this.tbLevel.Name = "tbLevel";
            this.tbLevel.Size = new System.Drawing.Size(369, 22);
            this.tbLevel.TabIndex = 0;
            // 
            // gbColumns
            // 
            this.gbColumns.Controls.Add(this.lblCooltip);
            this.gbColumns.Controls.Add(this.lblNotes);
            this.gbColumns.Controls.Add(this.tbNotes);
            this.gbColumns.Controls.Add(this.lblProcess);
            this.gbColumns.Controls.Add(this.tbProcess);
            this.gbColumns.Controls.Add(this.lblMPN);
            this.gbColumns.Controls.Add(this.tbMPN);
            this.gbColumns.Controls.Add(this.lblManufacturer);
            this.gbColumns.Controls.Add(this.tbManufacturer);
            this.gbColumns.Controls.Add(this.lblDesignator);
            this.gbColumns.Controls.Add(this.tbDesignator);
            this.gbColumns.Controls.Add(this.lblQuantity);
            this.gbColumns.Controls.Add(this.tbQuantity);
            this.gbColumns.Controls.Add(this.lblDescription);
            this.gbColumns.Controls.Add(this.tbDescription);
            this.gbColumns.Controls.Add(this.lblCPN);
            this.gbColumns.Controls.Add(this.tbCPN);
            this.gbColumns.Controls.Add(this.lblLevel);
            this.gbColumns.Controls.Add(this.tbLevel);
            this.gbColumns.Location = new System.Drawing.Point(12, 12);
            this.gbColumns.Name = "gbColumns";
            this.gbColumns.Size = new System.Drawing.Size(460, 312);
            this.gbColumns.TabIndex = 1;
            this.gbColumns.TabStop = false;
            this.gbColumns.Text = "Column Identifiers";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(397, 427);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(6, 24);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(36, 13);
            this.lblLevel.TabIndex = 1;
            this.lblLevel.Text = "Level:";
            // 
            // lblCPN
            // 
            this.lblCPN.AutoSize = true;
            this.lblCPN.Location = new System.Drawing.Point(6, 52);
            this.lblCPN.Name = "lblCPN";
            this.lblCPN.Size = new System.Drawing.Size(32, 13);
            this.lblCPN.TabIndex = 3;
            this.lblCPN.Text = "CPN:";
            // 
            // tbCPN
            // 
            this.tbCPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCPN.Location = new System.Drawing.Point(85, 47);
            this.tbCPN.Name = "tbCPN";
            this.tbCPN.Size = new System.Drawing.Size(369, 22);
            this.tbCPN.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 80);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description:";
            // 
            // tbDescription
            // 
            this.tbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDescription.Location = new System.Drawing.Point(85, 75);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(369, 22);
            this.tbDescription.TabIndex = 4;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(6, 108);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 7;
            this.lblQuantity.Text = "Quantity:";
            // 
            // tbQuantity
            // 
            this.tbQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQuantity.Location = new System.Drawing.Point(85, 103);
            this.tbQuantity.Name = "tbQuantity";
            this.tbQuantity.Size = new System.Drawing.Size(369, 22);
            this.tbQuantity.TabIndex = 6;
            // 
            // lblDesignator
            // 
            this.lblDesignator.AutoSize = true;
            this.lblDesignator.Location = new System.Drawing.Point(6, 136);
            this.lblDesignator.Name = "lblDesignator";
            this.lblDesignator.Size = new System.Drawing.Size(61, 13);
            this.lblDesignator.TabIndex = 9;
            this.lblDesignator.Text = "Designator:";
            // 
            // tbDesignator
            // 
            this.tbDesignator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDesignator.Location = new System.Drawing.Point(85, 131);
            this.tbDesignator.Name = "tbDesignator";
            this.tbDesignator.Size = new System.Drawing.Size(369, 22);
            this.tbDesignator.TabIndex = 8;
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Location = new System.Drawing.Point(6, 164);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(73, 13);
            this.lblManufacturer.TabIndex = 11;
            this.lblManufacturer.Text = "Manufacturer:";
            // 
            // tbManufacturer
            // 
            this.tbManufacturer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbManufacturer.Location = new System.Drawing.Point(85, 159);
            this.tbManufacturer.Name = "tbManufacturer";
            this.tbManufacturer.Size = new System.Drawing.Size(369, 22);
            this.tbManufacturer.TabIndex = 10;
            // 
            // lblMPN
            // 
            this.lblMPN.AutoSize = true;
            this.lblMPN.Location = new System.Drawing.Point(6, 192);
            this.lblMPN.Name = "lblMPN";
            this.lblMPN.Size = new System.Drawing.Size(34, 13);
            this.lblMPN.TabIndex = 13;
            this.lblMPN.Text = "MPN:";
            // 
            // tbMPN
            // 
            this.tbMPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMPN.Location = new System.Drawing.Point(85, 187);
            this.tbMPN.Name = "tbMPN";
            this.tbMPN.Size = new System.Drawing.Size(369, 22);
            this.tbMPN.TabIndex = 12;
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(6, 220);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(48, 13);
            this.lblProcess.TabIndex = 15;
            this.lblProcess.Text = "Process:";
            // 
            // tbProcess
            // 
            this.tbProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProcess.Location = new System.Drawing.Point(85, 215);
            this.tbProcess.Name = "tbProcess";
            this.tbProcess.Size = new System.Drawing.Size(369, 22);
            this.tbProcess.TabIndex = 14;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(6, 248);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(38, 13);
            this.lblNotes.TabIndex = 17;
            this.lblNotes.Text = "Notes:";
            // 
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNotes.Location = new System.Drawing.Point(85, 243);
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(369, 22);
            this.tbNotes.TabIndex = 16;
            // 
            // gbMisc
            // 
            this.gbMisc.Location = new System.Drawing.Point(12, 330);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Size = new System.Drawing.Size(460, 91);
            this.gbMisc.TabIndex = 3;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Miscellaneous";
            // 
            // lblCooltip
            // 
            this.lblCooltip.AutoSize = true;
            this.lblCooltip.Location = new System.Drawing.Point(6, 287);
            this.lblCooltip.Name = "lblCooltip";
            this.lblCooltip.Size = new System.Drawing.Size(324, 13);
            this.lblCooltip.TabIndex = 18;
            this.lblCooltip.Text = "Cool tip: All column identifiers are case insensitive when compared. ";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.gbMisc);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbColumns);
            this.Name = "Settings";
            this.Text = "Settings";
            this.gbColumns.ResumeLayout(false);
            this.gbColumns.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbLevel;
        private System.Windows.Forms.GroupBox gbColumns;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.TextBox tbProcess;
        private System.Windows.Forms.Label lblMPN;
        private System.Windows.Forms.TextBox tbMPN;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.TextBox tbManufacturer;
        private System.Windows.Forms.Label lblDesignator;
        private System.Windows.Forms.TextBox tbDesignator;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox tbQuantity;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label lblCPN;
        private System.Windows.Forms.TextBox tbCPN;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbMisc;
        private System.Windows.Forms.Label lblCooltip;


    }
}