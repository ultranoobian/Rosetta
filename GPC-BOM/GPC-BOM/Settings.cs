using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPC_BOM {
    public partial class Settings : Form {
        public Settings() {
            InitializeComponent();

            #region Retrieve Setting Values

            // Retrieve current setting values for Level
            string level = "";
            foreach (string item in Properties.Settings.Default.cLevel) {
                level = level + item + ";";
            }
            this.tbLevel.Text = level;

            // Retrieve current setting values for CPN
            string cpn = "";
            foreach (string item in Properties.Settings.Default.cCPN) {
                cpn = cpn + item + ";";
            }
            this.tbCPN.Text = cpn;

            // Retrieve current setting values for Description
            string description = "";
            foreach (string item in Properties.Settings.Default.cDescription) {
                description = description + item + ";";
            }
            this.tbDescription.Text = description;

            // Retrieve current setting values for Quantity
            string quantity = "";
            foreach (string item in Properties.Settings.Default.cQuantity) {
                quantity = quantity + item + ";";
            }
            this.tbQuantity.Text = quantity;

            // Retrieve current setting values for Designator
            string designator = "";
            foreach (string item in Properties.Settings.Default.cDesignator) {
                designator = designator + item + ";";
            }
            this.tbDesignator.Text = designator;

            // Retrieve current setting values for Manufacturer
            string manufacturer = "";
            foreach (string item in Properties.Settings.Default.cManufacturer) {
                manufacturer = manufacturer + item + ";";
            }
            this.tbManufacturer.Text = manufacturer;

            // Retrieve current setting values for MPN
            string mpn = "";
            foreach (string item in Properties.Settings.Default.cMPN) {
                mpn = mpn + item + ";";
            }
            this.tbMPN.Text = mpn;

            // Retrieve current setting values for Process
            string process = "";
            foreach (string item in Properties.Settings.Default.cProcess) {
                process = process + item + ";";
            }
            this.tbProcess.Text = process;

            // Retrieve current setting values for Notes
            string notes = "";
            foreach (string item in Properties.Settings.Default.cNotes) {
                notes = notes + item + ";";
            }
            this.tbNotes.Text = notes;

            // Retrieve current setting values for web scraping order
            List<string> webOrder = new List<string>();
            webOrder.Add(Properties.Settings.Default.webOrder1);
            webOrder.Add(Properties.Settings.Default.webOrder2);
            webOrder.Add(Properties.Settings.Default.webOrder3);
            webOrder.Add(Properties.Settings.Default.webOrder4);
            for (int i = 0; i < webOrder.Count; i++) {
                switch (webOrder[i]) {
                    case "NaiveDigikey":
                        this.numDigikey.Value = i + 1;
                        break;
                    case "NaiveMouser":
                        this.numMouser.Value = i + 1;
                        break;
                    case "NaiveElement14":
                        this.numElement14.Value = i + 1;
                        break;
                    case "NaiveRS":
                        this.numRS.Value = i + 1;
                        break;
                }
            }

            #endregion
        }

        private void btnSave_Click(object sender, EventArgs e) {
            // Check to ensure that settings are valid
            List<int> orderList = new List<int> {(int)this.numDigikey.Value, (int)this.numElement14.Value, (int)this.numMouser.Value, (int)this.numRS.Value};
            bool orderUnique = orderList.Distinct().Count() == orderList.Count();
            if (!orderUnique) {
                MessageBox.Show("Please double-check the order of the web scrapers to ensure that assigned values are unique.");
                return;
            }
            // Overwrite settings by splitting each string to an array, then convert array to collection
            #region Overwrite settings

            // Save setting values for Level
            string[] levelArray = this.tbLevel.Text.ToLower().Split(';');
            Properties.Settings.Default.cLevel.Clear();
            foreach (string item in levelArray) {
                Properties.Settings.Default.cLevel.Add(item);
            }

            // Save setting values for CPN
            string[] cpnArray = this.tbCPN.Text.ToLower().Split(';');
            Properties.Settings.Default.cCPN.Clear();
            foreach (string item in cpnArray) {
                Properties.Settings.Default.cCPN.Add(item);
            }

            // Save setting values for Description
            string[] descriptionArray = this.tbDescription.Text.ToLower().Split(';');
            Properties.Settings.Default.cDescription.Clear();
            foreach (string item in descriptionArray) {
                Properties.Settings.Default.cDescription.Add(item);
            }

            // Save setting values for Quantity
            string[] quantityArray = this.tbQuantity.Text.ToLower().Split(';');
            Properties.Settings.Default.cQuantity.Clear();
            foreach (string item in quantityArray) {
                Properties.Settings.Default.cQuantity.Add(item);
            }

            // Save setting values for Designator
            string[] designatorArray = this.tbDesignator.Text.ToLower().Split(';');
            Properties.Settings.Default.cDesignator.Clear();
            foreach (string item in designatorArray) {
                Properties.Settings.Default.cDesignator.Add(item);
            }

            // Save setting values for Manufacturer
            string[] manufacturerArray = this.tbManufacturer.Text.ToLower().Split(';');
            Properties.Settings.Default.cManufacturer.Clear();
            foreach (string item in manufacturerArray) {
                Properties.Settings.Default.cManufacturer.Add(item);
            }

            // Save setting values for MPN
            string[] mpnArray = this.tbMPN.Text.ToLower().Split(';');
            Properties.Settings.Default.cMPN.Clear();
            foreach (string item in mpnArray) {
                Properties.Settings.Default.cMPN.Add(item);
            }

            // Save setting values for Process
            string[] processArray = this.tbProcess.Text.ToLower().Split(';');
            Properties.Settings.Default.cProcess.Clear();
            foreach (string item in processArray) {
                Properties.Settings.Default.cProcess.Add(item);
            }

            // Save setting values for Notes
            string[] notesArray = this.tbNotes.Text.ToLower().Split(';');
            Properties.Settings.Default.cNotes.Clear();
            foreach (string item in notesArray) {
                Properties.Settings.Default.cNotes.Add(item);
            }

            // Save setting values for web scraping order
            switch ((int)this.numDigikey.Value) {
                case 1:
                    Properties.Settings.Default.webOrder1 = "NaiveDigikey";
                    break;
                case 2:
                    Properties.Settings.Default.webOrder2 = "NaiveDigikey";
                    break;
                case 3:
                    Properties.Settings.Default.webOrder3 = "NaiveDigikey";
                    break;
                case 4:
                    Properties.Settings.Default.webOrder4 = "NaiveDigikey";
                    break;
            }
            switch ((int)this.numMouser.Value) {
                case 1:
                    Properties.Settings.Default.webOrder1 = "NaiveMouser";
                    break;
                case 2:
                    Properties.Settings.Default.webOrder2 = "NaiveMouser";
                    break;
                case 3:
                    Properties.Settings.Default.webOrder3 = "NaiveMouser";
                    break;
                case 4:
                    Properties.Settings.Default.webOrder4 = "NaiveMouser";
                    break;
            }
            switch ((int)this.numElement14.Value) {
                case 1:
                    Properties.Settings.Default.webOrder1 = "NaiveElement14";
                    break;
                case 2:
                    Properties.Settings.Default.webOrder2 = "NaiveElement14";
                    break;
                case 3:
                    Properties.Settings.Default.webOrder3 = "NaiveElement14";
                    break;
                case 4:
                    Properties.Settings.Default.webOrder4 = "NaiveElement14";
                    break;
            }
            switch ((int)this.numRS.Value) {
                case 1:
                    Properties.Settings.Default.webOrder1 = "NaiveRS";
                    break;
                case 2:
                    Properties.Settings.Default.webOrder2 = "NaiveRS";
                    break;
                case 3:
                    Properties.Settings.Default.webOrder3 = "NaiveRS";
                    break;
                case 4:
                    Properties.Settings.Default.webOrder4 = "NaiveRS";
                    break;
            }

            // Save central setting store to file, refresh
            // This is usually in \%userprofile%\AppData\Local <or> Roaming\...
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            #endregion

            this.Close();
        }

        private void btnDefault_Click(object sender, EventArgs e) {
            DialogResult dr = MessageBox.Show("Are you sure?",
                      "Please Confirm", MessageBoxButtons.YesNo);
            switch (dr) {
                case DialogResult.Yes:
                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                    this.Close();
                    break;
                case DialogResult.No:
                    break;
            }
        }

    }
}
