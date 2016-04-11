using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEditor {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            FileManager.Initialize();
            labelDirectoryName.Text = FileManager.baseFilePath;
        }

        private void buttonNextFile_Click(object sender, EventArgs e) {
            FileManager.Initialize();
            FileManager.ReadData();
            labelGuideBody.Text = FileManager.UpdateGuideText();
            if (FileManager.varData.Count == 0) {
                MessageBox.Show("No data found.");
            } else {
                textBoxValue.Text = FileManager.ReadNextVariable(1);
                labelVariableNumber.Text = (FileManager.varIndex + 1) + " - " + FileManager.varNames[FileManager.varIndex];
            }
        }

        private void buttonNextValue_Click(object sender, EventArgs e) {
            if (FileManager.varData.Count == 0) {
                MessageBox.Show("Read file before reading data.");
            } else {
                textBoxValue.Text = FileManager.ReadNextVariable(1);
                labelVariableNumber.Text = (FileManager.varIndex + 1) + " - " + FileManager.varNames[FileManager.varIndex];
            }
        }

        private void buttonApply_Click(object sender, EventArgs e) {
            FileManager.SaveVar(textBoxValue.Text);
        }

        private void buttonSaveToFile_Click(object sender, EventArgs e) {
            FileManager.SaveFile();
        }

        private void buttonPreviousValue_Click(object sender, EventArgs e) {
            if (FileManager.varData.Count == 0) {
                MessageBox.Show("Read file before reading data.");
            } else {
                textBoxValue.Text = FileManager.ReadNextVariable(-1);
                labelVariableNumber.Text = (FileManager.varIndex + 1) + " - " + FileManager.varNames[FileManager.varIndex];
            }
        }
    }
}
