using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace excel
{
	public partial class Form2 : Form
	{
		public Action Worker { get; set; }

		public Form2(Action worker)
		{
			InitializeComponent();
			if (worker == null)
				throw new ArgumentNullException();
			Worker = worker;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Task.Factory.StartNew(Worker).ContinueWith(t => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void progressBar1_Click(object sender, EventArgs e)
		{

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			progressBar1.Increment(10);
			label2.Text = progressBar1.Value.ToString() + "%";
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			timer1.Start();
		}
	}
}
