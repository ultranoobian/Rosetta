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
            this.lblCooltip = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.lblProcess = new System.Windows.Forms.Label();
            this.tbProcess = new System.Windows.Forms.TextBox();
            this.lblMPN = new System.Windows.Forms.Label();
            this.tbMPN = new System.Windows.Forms.TextBox();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.tbManufacturer = new System.Windows.Forms.TextBox();
            this.lblDesignator = new System.Windows.Forms.Label();
            this.tbDesignator = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.tbQuantity = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lblCPN = new System.Windows.Forms.Label();
            this.tbCPN = new System.Windows.Forms.TextBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbWebScrapers = new System.Windows.Forms.GroupBox();
            this.lblElement14 = new System.Windows.Forms.Label();
            this.lblRS = new System.Windows.Forms.Label();
            this.lblMouser = new System.Windows.Forms.Label();
            this.lblDigikey = new System.Windows.Forms.Label();
            this.numMouser = new System.Windows.Forms.NumericUpDown();
            this.numRS = new System.Windows.Forms.NumericUpDown();
            this.numElement14 = new System.Windows.Forms.NumericUpDown();
            this.numDigikey = new System.Windows.Forms.NumericUpDown();
            this.btnDefault = new System.Windows.Forms.Button();
            this.gbColumnSearching = new System.Windows.Forms.GroupBox();
            this.cbPreferredMode = new System.Windows.Forms.ComboBox();
            this.lblPreferred = new System.Windows.Forms.Label();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.numWebTimeout = new System.Windows.Forms.NumericUpDown();
            this.gbColumns.SuspendLayout();
            this.gbWebScrapers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMouser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numElement14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDigikey)).BeginInit();
            this.gbColumnSearching.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWebTimeout)).BeginInit();
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
            this.gbColumns.Text = "Column Identifiers (Basic)";
            // 
            // lblCooltip
            // 
            this.lblCooltip.AutoSize = true;
            this.lblCooltip.Location = new System.Drawing.Point(6, 280);
            this.lblCooltip.Name = "lblCooltip";
            this.lblCooltip.Size = new System.Drawing.Size(367, 26);
            this.lblCooltip.TabIndex = 18;
            this.lblCooltip.Text = "Cool tip: All column identifiers are case insensitive when compared. \r\nHeuristic " +
    "analysis will also override these decisions with what it thinks is best.";
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
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(6, 24);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(36, 13);
            this.lblLevel.TabIndex = 1;
            this.lblLevel.Text = "Level:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(397, 527);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbWebScrapers
            // 
            this.gbWebScrapers.Controls.Add(this.numWebTimeout);
            this.gbWebScrapers.Controls.Add(this.lblTimeout);
            this.gbWebScrapers.Controls.Add(this.lblElement14);
            this.gbWebScrapers.Controls.Add(this.lblRS);
            this.gbWebScrapers.Controls.Add(this.lblMouser);
            this.gbWebScrapers.Controls.Add(this.lblDigikey);
            this.gbWebScrapers.Controls.Add(this.numMouser);
            this.gbWebScrapers.Controls.Add(this.numRS);
            this.gbWebScrapers.Controls.Add(this.numElement14);
            this.gbWebScrapers.Controls.Add(this.numDigikey);
            this.gbWebScrapers.Location = new System.Drawing.Point(12, 330);
            this.gbWebScrapers.Name = "gbWebScrapers";
            this.gbWebScrapers.Size = new System.Drawing.Size(460, 131);
            this.gbWebScrapers.TabIndex = 3;
            this.gbWebScrapers.TabStop = false;
            this.gbWebScrapers.Text = "Web Scraping Preference";
            // 
            // lblElement14
            // 
            this.lblElement14.AutoSize = true;
            this.lblElement14.Location = new System.Drawing.Point(6, 50);
            this.lblElement14.Name = "lblElement14";
            this.lblElement14.Size = new System.Drawing.Size(60, 13);
            this.lblElement14.TabIndex = 7;
            this.lblElement14.Text = "Element14:";
            // 
            // lblRS
            // 
            this.lblRS.AutoSize = true;
            this.lblRS.Location = new System.Drawing.Point(215, 50);
            this.lblRS.Name = "lblRS";
            this.lblRS.Size = new System.Drawing.Size(87, 13);
            this.lblRS.TabIndex = 6;
            this.lblRS.Text = "RS Components:";
            // 
            // lblMouser
            // 
            this.lblMouser.AutoSize = true;
            this.lblMouser.Location = new System.Drawing.Point(215, 21);
            this.lblMouser.Name = "lblMouser";
            this.lblMouser.Size = new System.Drawing.Size(45, 13);
            this.lblMouser.TabIndex = 5;
            this.lblMouser.Text = "Mouser:";
            // 
            // lblDigikey
            // 
            this.lblDigikey.AutoSize = true;
            this.lblDigikey.Location = new System.Drawing.Point(6, 21);
            this.lblDigikey.Name = "lblDigikey";
            this.lblDigikey.Size = new System.Drawing.Size(46, 13);
            this.lblDigikey.TabIndex = 4;
            this.lblDigikey.Text = "DigiKey:";
            // 
            // numMouser
            // 
            this.numMouser.Location = new System.Drawing.Point(308, 19);
            this.numMouser.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numMouser.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMouser.Name = "numMouser";
            this.numMouser.Size = new System.Drawing.Size(120, 20);
            this.numMouser.TabIndex = 3;
            this.numMouser.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numRS
            // 
            this.numRS.Location = new System.Drawing.Point(308, 48);
            this.numRS.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numRS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRS.Name = "numRS";
            this.numRS.Size = new System.Drawing.Size(120, 20);
            this.numRS.TabIndex = 2;
            this.numRS.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // numElement14
            // 
            this.numElement14.Location = new System.Drawing.Point(76, 48);
            this.numElement14.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numElement14.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numElement14.Name = "numElement14";
            this.numElement14.Size = new System.Drawing.Size(120, 20);
            this.numElement14.TabIndex = 1;
            this.numElement14.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numDigikey
            // 
            this.numDigikey.Location = new System.Drawing.Point(76, 19);
            this.numDigikey.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numDigikey.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDigikey.Name = "numDigikey";
            this.numDigikey.Size = new System.Drawing.Size(120, 20);
            this.numDigikey.TabIndex = 0;
            this.numDigikey.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(12, 527);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(100, 23);
            this.btnDefault.TabIndex = 4;
            this.btnDefault.Text = "Restore Defaults";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // gbColumnSearching
            // 
            this.gbColumnSearching.Controls.Add(this.lblPreferred);
            this.gbColumnSearching.Controls.Add(this.cbPreferredMode);
            this.gbColumnSearching.Location = new System.Drawing.Point(12, 467);
            this.gbColumnSearching.Name = "gbColumnSearching";
            this.gbColumnSearching.Size = new System.Drawing.Size(460, 54);
            this.gbColumnSearching.TabIndex = 5;
            this.gbColumnSearching.TabStop = false;
            this.gbColumnSearching.Text = "Searching Parameters";
            // 
            // cbPreferredMode
            // 
            this.cbPreferredMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPreferredMode.FormattingEnabled = true;
            this.cbPreferredMode.Items.AddRange(new object[] {
            "Basic Excel search based on Column Identifiers",
            "Intelligent Heuristic Classification (Type 1)",
            "Intelligent Heuristic Classification (Type 2)"});
            this.cbPreferredMode.Location = new System.Drawing.Point(159, 19);
            this.cbPreferredMode.MaxDropDownItems = 2;
            this.cbPreferredMode.Name = "cbPreferredMode";
            this.cbPreferredMode.Size = new System.Drawing.Size(295, 21);
            this.cbPreferredMode.TabIndex = 0;
            // 
            // lblPreferred
            // 
            this.lblPreferred.AutoSize = true;
            this.lblPreferred.Location = new System.Drawing.Point(6, 22);
            this.lblPreferred.Name = "lblPreferred";
            this.lblPreferred.Size = new System.Drawing.Size(147, 13);
            this.lblPreferred.TabIndex = 1;
            this.lblPreferred.Text = "Preferred Search Mechanism:";
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(6, 86);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(179, 13);
            this.lblTimeout.TabIndex = 2;
            this.lblTimeout.Text = "Web Scraping Timeout (in seconds):";
            // 
            // numWebTimeout
            // 
            this.numWebTimeout.Location = new System.Drawing.Point(205, 84);
            this.numWebTimeout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numWebTimeout.Name = "numWebTimeout";
            this.numWebTimeout.Size = new System.Drawing.Size(97, 20);
            this.numWebTimeout.TabIndex = 8;
            this.numWebTimeout.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 562);
            this.Controls.Add(this.gbColumnSearching);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.gbWebScrapers);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbColumns);
            this.Name = "Settings";
            this.Text = "Settings";
            this.gbColumns.ResumeLayout(false);
            this.gbColumns.PerformLayout();
            this.gbWebScrapers.ResumeLayout(false);
            this.gbWebScrapers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMouser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numElement14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDigikey)).EndInit();
            this.gbColumnSearching.ResumeLayout(false);
            this.gbColumnSearching.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWebTimeout)).EndInit();
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
        private System.Windows.Forms.GroupBox gbWebScrapers;
        private System.Windows.Forms.Label lblCooltip;
        private System.Windows.Forms.NumericUpDown numDigikey;
        private System.Windows.Forms.Label lblElement14;
        private System.Windows.Forms.Label lblRS;
        private System.Windows.Forms.Label lblMouser;
        private System.Windows.Forms.Label lblDigikey;
        private System.Windows.Forms.NumericUpDown numMouser;
        private System.Windows.Forms.NumericUpDown numRS;
        private System.Windows.Forms.NumericUpDown numElement14;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.GroupBox gbColumnSearching;
        private System.Windows.Forms.Label lblPreferred;
        private System.Windows.Forms.ComboBox cbPreferredMode;
        private System.Windows.Forms.NumericUpDown numWebTimeout;
        private System.Windows.Forms.Label lblTimeout;


    }
}