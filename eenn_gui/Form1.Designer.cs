namespace eenn_gui
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.inputFile = new System.Windows.Forms.TextBox();
            this.outputFile = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.deployButton = new System.Windows.Forms.Button();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.inputBrowseButton = new System.Windows.Forms.Button();
            this.outputBrowseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cropSizeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.processorComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gpuComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input File Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output File Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // inputFile
            // 
            this.inputFile.AllowDrop = true;
            this.inputFile.Location = new System.Drawing.Point(225, 6);
            this.inputFile.Name = "inputFile";
            this.inputFile.Size = new System.Drawing.Size(1037, 31);
            this.inputFile.TabIndex = 2;
            // 
            // outputFile
            // 
            this.outputFile.AllowDrop = true;
            this.outputFile.Location = new System.Drawing.Point(225, 62);
            this.outputFile.Name = "outputFile";
            this.outputFile.Size = new System.Drawing.Size(1037, 31);
            this.outputFile.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 187);
            this.progressBar1.Maximum = 10000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1387, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // deployButton
            // 
            this.deployButton.Location = new System.Drawing.Point(1268, 126);
            this.deployButton.Name = "deployButton";
            this.deployButton.Size = new System.Drawing.Size(136, 31);
            this.deployButton.TabIndex = 5;
            this.deployButton.Text = "Deploy";
            this.deployButton.UseVisualStyleBackColor = true;
            this.deployButton.Click += new System.EventHandler(this.deployButton_Click);
            // 
            // progressTimer
            // 
            this.progressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
            // 
            // inputBrowseButton
            // 
            this.inputBrowseButton.Location = new System.Drawing.Point(1268, 6);
            this.inputBrowseButton.Name = "inputBrowseButton";
            this.inputBrowseButton.Size = new System.Drawing.Size(136, 31);
            this.inputBrowseButton.TabIndex = 6;
            this.inputBrowseButton.Text = "Browse";
            this.inputBrowseButton.UseVisualStyleBackColor = true;
            this.inputBrowseButton.Click += new System.EventHandler(this.inputBrowseButton_Click);
            // 
            // outputBrowseButton
            // 
            this.outputBrowseButton.Location = new System.Drawing.Point(1268, 62);
            this.outputBrowseButton.Name = "outputBrowseButton";
            this.outputBrowseButton.Size = new System.Drawing.Size(136, 31);
            this.outputBrowseButton.TabIndex = 7;
            this.outputBrowseButton.Text = "Browse";
            this.outputBrowseButton.UseVisualStyleBackColor = true;
            this.outputBrowseButton.Click += new System.EventHandler(this.outputBrowseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Crop Size:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cropSizeTextBox
            // 
            this.cropSizeTextBox.Location = new System.Drawing.Point(225, 126);
            this.cropSizeTextBox.MaxLength = 3;
            this.cropSizeTextBox.Name = "cropSizeTextBox";
            this.cropSizeTextBox.Size = new System.Drawing.Size(100, 31);
            this.cropSizeTextBox.TabIndex = 9;
            this.cropSizeTextBox.Text = "64";
            this.cropSizeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cropSizeTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(387, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Processor:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // processorComboBox
            // 
            this.processorComboBox.FormattingEnabled = true;
            this.processorComboBox.Items.AddRange(new object[] {
            "CPU"});
            this.processorComboBox.Location = new System.Drawing.Point(530, 126);
            this.processorComboBox.Name = "processorComboBox";
            this.processorComboBox.Size = new System.Drawing.Size(121, 33);
            this.processorComboBox.TabIndex = 11;
            this.processorComboBox.SelectedIndexChanged += new System.EventHandler(this.processorComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(740, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "GPU:";
            // 
            // gpuComboBox
            // 
            this.gpuComboBox.Enabled = false;
            this.gpuComboBox.FormattingEnabled = true;
            this.gpuComboBox.Location = new System.Drawing.Point(831, 126);
            this.gpuComboBox.Name = "gpuComboBox";
            this.gpuComboBox.Size = new System.Drawing.Size(431, 33);
            this.gpuComboBox.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 219);
            this.Controls.Add(this.gpuComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.processorComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cropSizeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputBrowseButton);
            this.Controls.Add(this.inputBrowseButton);
            this.Controls.Add(this.deployButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.outputFile);
            this.Controls.Add(this.inputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "EENN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inputFile;
        private System.Windows.Forms.TextBox outputFile;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button deployButton;
        private System.Windows.Forms.Timer progressTimer;
        private System.Windows.Forms.Button inputBrowseButton;
        private System.Windows.Forms.Button outputBrowseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cropSizeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox processorComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox gpuComboBox;
    }
}

