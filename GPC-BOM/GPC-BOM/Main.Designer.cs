namespace GPC_BOM {
    partial class frmMain {
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.msmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.msmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msiDocumentation = new System.Windows.Forms.ToolStripMenuItem();
            this.msiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDragDrop = new System.Windows.Forms.Label();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnClearQueue = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.lblConvertOption = new System.Windows.Forms.Label();
            this.cbConvertOption = new System.Windows.Forms.ComboBox();
            this.menuStripMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msmFile,
            this.msmEdit,
            this.msmHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(784, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip";
            // 
            // msmFile
            // 
            this.msmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiOpen,
            this.msiExit});
            this.msmFile.Name = "msmFile";
            this.msmFile.Size = new System.Drawing.Size(37, 20);
            this.msmFile.Text = "File";
            // 
            // msiOpen
            // 
            this.msiOpen.Name = "msiOpen";
            this.msiOpen.Size = new System.Drawing.Size(129, 22);
            this.msiOpen.Text = "Open Files";
            this.msiOpen.Click += new System.EventHandler(this.msiOpen_Click);
            // 
            // msiExit
            // 
            this.msiExit.Name = "msiExit";
            this.msiExit.Size = new System.Drawing.Size(129, 22);
            this.msiExit.Text = "Exit";
            this.msiExit.Click += new System.EventHandler(this.msiExit_Click);
            // 
            // msmEdit
            // 
            this.msmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiSettings});
            this.msmEdit.Name = "msmEdit";
            this.msmEdit.Size = new System.Drawing.Size(39, 20);
            this.msmEdit.Text = "Edit";
            // 
            // msiSettings
            // 
            this.msiSettings.Name = "msiSettings";
            this.msiSettings.Size = new System.Drawing.Size(116, 22);
            this.msiSettings.Text = "Settings";
            this.msiSettings.Click += new System.EventHandler(this.msiSettings_Click);
            // 
            // msmHelp
            // 
            this.msmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiDocumentation,
            this.msiAbout});
            this.msmHelp.Name = "msmHelp";
            this.msmHelp.Size = new System.Drawing.Size(44, 20);
            this.msmHelp.Text = "Help";
            // 
            // msiDocumentation
            // 
            this.msiDocumentation.Name = "msiDocumentation";
            this.msiDocumentation.Size = new System.Drawing.Size(185, 22);
            this.msiDocumentation.Text = "View Documentation";
            this.msiDocumentation.Click += new System.EventHandler(this.msiDocumentation_Click);
            // 
            // msiAbout
            // 
            this.msiAbout.Name = "msiAbout";
            this.msiAbout.Size = new System.Drawing.Size(185, 22);
            this.msiAbout.Text = "About";
            this.msiAbout.Click += new System.EventHandler(this.msiAbout_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(104, 510);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(400, 20);
            this.tbPath.TabIndex = 3;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(610, 508);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(162, 23);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(12, 513);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(86, 13);
            this.lblPath.TabIndex = 5;
            this.lblPath.Text = "Output Location:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(510, 508);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(94, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressBar,
            this.tsStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(100, 16);
            this.tsProgressBar.Step = 20;
            // 
            // tsStatus
            // 
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(66, 17);
            this.tsStatus.Text = "Welcome...";
            // 
            // lblDragDrop
            // 
            this.lblDragDrop.AutoSize = true;
            this.lblDragDrop.BackColor = System.Drawing.SystemColors.Window;
            this.lblDragDrop.Font = new System.Drawing.Font("Calibri", 20F);
            this.lblDragDrop.Location = new System.Drawing.Point(50, 282);
            this.lblDragDrop.Name = "lblDragDrop";
            this.lblDragDrop.Size = new System.Drawing.Size(231, 33);
            this.lblDragDrop.TabIndex = 8;
            this.lblDragDrop.Text = "Drag and Drop Here";
            // 
            // lvFiles
            // 
            this.lvFiles.AllowDrop = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.Location = new System.Drawing.Point(12, 27);
            this.lvFiles.MultiSelect = false;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(320, 447);
            this.lvFiles.TabIndex = 9;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvFiles_DragDrop);
            this.lvFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvFiles_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 50;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Location";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Status";
            this.columnHeader4.Width = 80;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(93, 480);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(118, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove Selected";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(217, 480);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(115, 23);
            this.btnPreview.TabIndex = 11;
            this.btnPreview.Text = "Preview Selected";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnClearQueue
            // 
            this.btnClearQueue.Location = new System.Drawing.Point(12, 480);
            this.btnClearQueue.Name = "btnClearQueue";
            this.btnClearQueue.Size = new System.Drawing.Size(75, 23);
            this.btnClearQueue.TabIndex = 12;
            this.btnClearQueue.Text = "Clear Queue";
            this.btnClearQueue.UseVisualStyleBackColor = true;
            this.btnClearQueue.Click += new System.EventHandler(this.btnClearQueue_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Word, Excel or PDF Files|*.doc;*.docx;*.xls;*.xlsx;*.pdf";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.Title = "Choose file(s) for";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(338, 27);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(434, 447);
            this.dataGridView.TabIndex = 13;
            // 
            // lblConvertOption
            // 
            this.lblConvertOption.AutoSize = true;
            this.lblConvertOption.Location = new System.Drawing.Point(371, 485);
            this.lblConvertOption.Name = "lblConvertOption";
            this.lblConvertOption.Size = new System.Drawing.Size(133, 13);
            this.lblConvertOption.TabIndex = 14;
            this.lblConvertOption.Text = "Conversion Output Format:";
            // 
            // cbConvertOption
            // 
            this.cbConvertOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConvertOption.FormattingEnabled = true;
            this.cbConvertOption.Items.AddRange(new object[] {
            "Quotewin Single Level BOM Format (.csv)",
            "Quotewin Multi Level BOM Format (.csv)",
            "SAP Upload Format (.xls)"});
            this.cbConvertOption.Location = new System.Drawing.Point(510, 482);
            this.cbConvertOption.Name = "cbConvertOption";
            this.cbConvertOption.Size = new System.Drawing.Size(262, 21);
            this.cbConvertOption.TabIndex = 15;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.cbConvertOption);
            this.Controls.Add(this.lblConvertOption);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnClearQueue);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblDragDrop);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "frmMain";
            this.Text = "GPC BOM Converter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem msmFile;
        private System.Windows.Forms.ToolStripMenuItem msmEdit;
        private System.Windows.Forms.ToolStripMenuItem msmHelp;
        private System.Windows.Forms.ToolStripMenuItem msiOpen;
        private System.Windows.Forms.ToolStripMenuItem msiExit;
        private System.Windows.Forms.ToolStripMenuItem msiSettings;
        private System.Windows.Forms.ToolStripMenuItem msiDocumentation;
        private System.Windows.Forms.ToolStripMenuItem msiAbout;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.Label lblDragDrop;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnClearQueue;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblConvertOption;
        private System.Windows.Forms.ComboBox cbConvertOption;
    }
}

