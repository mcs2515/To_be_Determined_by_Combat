namespace DataEditor
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
            this.labelDirectory = new System.Windows.Forms.Label();
            this.labelDirectoryName = new System.Windows.Forms.Label();
            this.labelVariable = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonNextFile = new System.Windows.Forms.Button();
            this.buttonNextValue = new System.Windows.Forms.Button();
            this.labelVariableNumber = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.labelGuide = new System.Windows.Forms.Label();
            this.labelGuideBody = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonSaveToFile = new System.Windows.Forms.Button();
            this.buttonPreviousValue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelDirectory
            // 
            this.labelDirectory.AutoSize = true;
            this.labelDirectory.Location = new System.Drawing.Point(68, 24);
            this.labelDirectory.Name = "labelDirectory";
            this.labelDirectory.Size = new System.Drawing.Size(52, 13);
            this.labelDirectory.TabIndex = 0;
            this.labelDirectory.Text = "Directory:";
            // 
            // labelDirectoryName
            // 
            this.labelDirectoryName.AutoSize = true;
            this.labelDirectoryName.Location = new System.Drawing.Point(216, 24);
            this.labelDirectoryName.Name = "labelDirectoryName";
            this.labelDirectoryName.Size = new System.Drawing.Size(89, 13);
            this.labelDirectoryName.TabIndex = 1;
            this.labelDirectoryName.Text = "<DirectoryName>";
            // 
            // labelVariable
            // 
            this.labelVariable.AutoSize = true;
            this.labelVariable.Location = new System.Drawing.Point(68, 130);
            this.labelVariable.Name = "labelVariable";
            this.labelVariable.Size = new System.Drawing.Size(48, 13);
            this.labelVariable.TabIndex = 4;
            this.labelVariable.Text = "Variable:";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(125, 207);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(156, 20);
            this.textBoxValue.TabIndex = 5;
            // 
            // buttonNextFile
            // 
            this.buttonNextFile.Location = new System.Drawing.Point(71, 53);
            this.buttonNextFile.Name = "buttonNextFile";
            this.buttonNextFile.Size = new System.Drawing.Size(360, 55);
            this.buttonNextFile.TabIndex = 6;
            this.buttonNextFile.Text = "Initialize";
            this.buttonNextFile.UseVisualStyleBackColor = true;
            this.buttonNextFile.Click += new System.EventHandler(this.buttonNextFile_Click);
            // 
            // buttonNextValue
            // 
            this.buttonNextValue.Location = new System.Drawing.Point(185, 158);
            this.buttonNextValue.Name = "buttonNextValue";
            this.buttonNextValue.Size = new System.Drawing.Size(75, 23);
            this.buttonNextValue.TabIndex = 7;
            this.buttonNextValue.Text = "Next Value";
            this.buttonNextValue.UseVisualStyleBackColor = true;
            this.buttonNextValue.Click += new System.EventHandler(this.buttonNextValue_Click);
            // 
            // labelVariableNumber
            // 
            this.labelVariableNumber.AutoSize = true;
            this.labelVariableNumber.Location = new System.Drawing.Point(122, 130);
            this.labelVariableNumber.Name = "labelVariableNumber";
            this.labelVariableNumber.Size = new System.Drawing.Size(94, 13);
            this.labelVariableNumber.TabIndex = 8;
            this.labelVariableNumber.Text = "<VariableNumber>";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(68, 210);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(37, 13);
            this.labelValue.TabIndex = 9;
            this.labelValue.Text = "Value:";
            // 
            // labelGuide
            // 
            this.labelGuide.AutoSize = true;
            this.labelGuide.Location = new System.Drawing.Point(581, 53);
            this.labelGuide.Name = "labelGuide";
            this.labelGuide.Size = new System.Drawing.Size(76, 13);
            this.labelGuide.TabIndex = 10;
            this.labelGuide.Text = "Variable Guide";
            // 
            // labelGuideBody
            // 
            this.labelGuideBody.Location = new System.Drawing.Point(433, 74);
            this.labelGuideBody.Name = "labelGuideBody";
            this.labelGuideBody.Size = new System.Drawing.Size(363, 298);
            this.labelGuideBody.TabIndex = 11;
            this.labelGuideBody.Text = "<GuideText>";
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(356, 205);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 12;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonSaveToFile
            // 
            this.buttonSaveToFile.Location = new System.Drawing.Point(71, 315);
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.buttonSaveToFile.Size = new System.Drawing.Size(360, 54);
            this.buttonSaveToFile.TabIndex = 13;
            this.buttonSaveToFile.Text = "SAVE TO FILE";
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            this.buttonSaveToFile.Click += new System.EventHandler(this.buttonSaveToFile_Click);
            // 
            // buttonPreviousValue
            // 
            this.buttonPreviousValue.Location = new System.Drawing.Point(71, 158);
            this.buttonPreviousValue.Name = "buttonPreviousValue";
            this.buttonPreviousValue.Size = new System.Drawing.Size(75, 23);
            this.buttonPreviousValue.TabIndex = 14;
            this.buttonPreviousValue.Text = "Prev Value";
            this.buttonPreviousValue.UseVisualStyleBackColor = true;
            this.buttonPreviousValue.Click += new System.EventHandler(this.buttonPreviousValue_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 388);
            this.Controls.Add(this.buttonPreviousValue);
            this.Controls.Add(this.buttonSaveToFile);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.labelGuideBody);
            this.Controls.Add(this.labelGuide);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.labelVariableNumber);
            this.Controls.Add(this.buttonNextValue);
            this.Controls.Add(this.buttonNextFile);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.labelVariable);
            this.Controls.Add(this.labelDirectoryName);
            this.Controls.Add(this.labelDirectory);
            this.Name = "Form1";
            this.Text = "Data Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDirectory;
        private System.Windows.Forms.Label labelDirectoryName;
        private System.Windows.Forms.Label labelVariable;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Button buttonNextFile;
        private System.Windows.Forms.Button buttonNextValue;
        private System.Windows.Forms.Label labelVariableNumber;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Label labelGuide;
        private System.Windows.Forms.Label labelGuideBody;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonSaveToFile;
        private System.Windows.Forms.Button buttonPreviousValue;
    }
}

