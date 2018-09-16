namespace excel
{
	partial class Form1
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.Output = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(30, 669);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(128, 59);
			this.button1.TabIndex = 0;
			this.button1.Text = "Create";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button2.Location = new System.Drawing.Point(985, 669);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(128, 60);
			this.button2.TabIndex = 1;
			this.button2.Text = "Convert";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(164, 668);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(128, 60);
			this.button3.TabIndex = 2;
			this.button3.Text = "Non";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(595, 9);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 33;
			this.dataGridView1.Size = new System.Drawing.Size(518, 620);
			this.dataGridView1.TabIndex = 4;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// listBox1
			// 
			this.listBox1.AllowDrop = true;
			this.listBox1.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 22;
			this.listBox1.Location = new System.Drawing.Point(30, 9);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(533, 620);
			this.listBox1.TabIndex = 9;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
			this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(342, 674);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 18);
			this.label2.TabIndex = 11;
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox1.Location = new System.Drawing.Point(325, 700);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(645, 29);
			this.textBox1.TabIndex = 12;
			// 
			// Output
			// 
			this.Output.AutoSize = true;
			this.Output.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Output.Location = new System.Drawing.Point(322, 666);
			this.Output.Name = "Output";
			this.Output.Size = new System.Drawing.Size(176, 24);
			this.Output.TabIndex = 13;
			this.Output.Text = "Output Location:";
			this.Output.Click += new System.EventHandler(this.label3_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.Window;
			this.label1.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(116, 330);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(340, 49);
			this.label1.TabIndex = 14;
			this.label1.Text = "Drag and Drop here";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1140, 764);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Output);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "BOM Prototype";
			this.Load += new System.EventHandler(this.Form1_Load_1);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label Output;
		private System.Windows.Forms.Label label1;
	}
}

