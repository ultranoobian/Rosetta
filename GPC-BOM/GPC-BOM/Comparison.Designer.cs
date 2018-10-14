namespace GPC_BOM {
    partial class Comparison {
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
            this.dataGridPredicted = new System.Windows.Forms.DataGridView();
            this.lblText1 = new System.Windows.Forms.Label();
            this.tbPredicted = new System.Windows.Forms.TextBox();
            this.lblText2 = new System.Windows.Forms.Label();
            this.cbColumn = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPredicted)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridPredicted
            // 
            this.dataGridPredicted.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPredicted.Location = new System.Drawing.Point(12, 59);
            this.dataGridPredicted.Name = "dataGridPredicted";
            this.dataGridPredicted.Size = new System.Drawing.Size(360, 181);
            this.dataGridPredicted.TabIndex = 0;
            // 
            // lblText1
            // 
            this.lblText1.AutoSize = true;
            this.lblText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText1.Location = new System.Drawing.Point(13, 13);
            this.lblText1.Name = "lblText1";
            this.lblText1.Size = new System.Drawing.Size(337, 36);
            this.lblText1.TabIndex = 1;
            this.lblText1.Text = "The heuristic analysis has concluded that the data \r\nshown belongs to the categor" +
    "y:";
            // 
            // tbPredicted
            // 
            this.tbPredicted.Location = new System.Drawing.Point(229, 33);
            this.tbPredicted.Name = "tbPredicted";
            this.tbPredicted.ReadOnly = true;
            this.tbPredicted.Size = new System.Drawing.Size(140, 20);
            this.tbPredicted.TabIndex = 2;
            // 
            // lblText2
            // 
            this.lblText2.AutoSize = true;
            this.lblText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText2.Location = new System.Drawing.Point(13, 243);
            this.lblText2.Name = "lblText2";
            this.lblText2.Size = new System.Drawing.Size(356, 36);
            this.lblText2.TabIndex = 3;
            this.lblText2.Text = "If this is correct, please confirm. If not, please set the \r\ncorrect category.";
            // 
            // cbColumn
            // 
            this.cbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumn.FormattingEnabled = true;
            this.cbColumn.Location = new System.Drawing.Point(12, 291);
            this.cbColumn.Name = "cbColumn";
            this.cbColumn.Size = new System.Drawing.Size(220, 21);
            this.cbColumn.TabIndex = 4;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(239, 291);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(133, 23);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // Comparison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 322);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cbColumn);
            this.Controls.Add(this.lblText2);
            this.Controls.Add(this.tbPredicted);
            this.Controls.Add(this.lblText1);
            this.Controls.Add(this.dataGridPredicted);
            this.Name = "Comparison";
            this.Text = "Comparison";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPredicted)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridPredicted;
        private System.Windows.Forms.Label lblText1;
        private System.Windows.Forms.TextBox tbPredicted;
        private System.Windows.Forms.Label lblText2;
        private System.Windows.Forms.ComboBox cbColumn;
        private System.Windows.Forms.Button btnConfirm;

    }
}