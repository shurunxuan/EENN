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
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.temperatureProgressBar = new System.Windows.Forms.ProgressBar();
            this.memoryProgressBar = new System.Windows.Forms.ProgressBar();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.memoryLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.utilProgressBar = new System.Windows.Forms.ProgressBar();
            this.utilLabel = new System.Windows.Forms.Label();
            this.gpuTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.logConsole = new System.Windows.Forms.TextBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input File Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output File Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // inputFile
            // 
            this.inputFile.AllowDrop = true;
            this.inputFile.Location = new System.Drawing.Point(208, 43);
            this.inputFile.Name = "inputFile";
            this.inputFile.Size = new System.Drawing.Size(1036, 31);
            this.inputFile.TabIndex = 2;
            // 
            // outputFile
            // 
            this.outputFile.AllowDrop = true;
            this.outputFile.Location = new System.Drawing.Point(208, 99);
            this.outputFile.Name = "outputFile";
            this.outputFile.Size = new System.Drawing.Size(1036, 31);
            this.outputFile.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 594);
            this.progressBar1.Maximum = 10000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1387, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // deployButton
            // 
            this.deployButton.Location = new System.Drawing.Point(1191, 221);
            this.deployButton.Name = "deployButton";
            this.deployButton.Size = new System.Drawing.Size(213, 305);
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
            this.inputBrowseButton.Location = new System.Drawing.Point(1250, 33);
            this.inputBrowseButton.Name = "inputBrowseButton";
            this.inputBrowseButton.Size = new System.Drawing.Size(136, 50);
            this.inputBrowseButton.TabIndex = 6;
            this.inputBrowseButton.Text = "Browse";
            this.inputBrowseButton.UseVisualStyleBackColor = true;
            this.inputBrowseButton.Click += new System.EventHandler(this.inputBrowseButton_Click);
            // 
            // outputBrowseButton
            // 
            this.outputBrowseButton.Location = new System.Drawing.Point(1250, 89);
            this.outputBrowseButton.Name = "outputBrowseButton";
            this.outputBrowseButton.Size = new System.Drawing.Size(136, 50);
            this.outputBrowseButton.TabIndex = 7;
            this.outputBrowseButton.Text = "Browse";
            this.outputBrowseButton.UseVisualStyleBackColor = true;
            this.outputBrowseButton.Click += new System.EventHandler(this.outputBrowseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1186, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Crop Size:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cropSizeTextBox
            // 
            this.cropSizeTextBox.Location = new System.Drawing.Point(1304, 184);
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
            this.label4.Location = new System.Drawing.Point(17, 42);
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
            this.processorComboBox.Location = new System.Drawing.Point(138, 39);
            this.processorComboBox.Name = "processorComboBox";
            this.processorComboBox.Size = new System.Drawing.Size(187, 33);
            this.processorComboBox.TabIndex = 11;
            this.processorComboBox.SelectedIndexChanged += new System.EventHandler(this.processorComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "GPU:";
            // 
            // gpuComboBox
            // 
            this.gpuComboBox.Enabled = false;
            this.gpuComboBox.FormattingEnabled = true;
            this.gpuComboBox.Location = new System.Drawing.Point(22, 115);
            this.gpuComboBox.Name = "gpuComboBox";
            this.gpuComboBox.Size = new System.Drawing.Size(303, 33);
            this.gpuComboBox.TabIndex = 13;
            this.gpuComboBox.SelectedIndexChanged += new System.EventHandler(this.gpuComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "GPU Utilization:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.inputFile);
            this.groupBox1.Controls.Add(this.outputFile);
            this.groupBox1.Controls.Add(this.inputBrowseButton);
            this.groupBox1.Controls.Add(this.outputBrowseButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1392, 166);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Files";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.temperatureProgressBar);
            this.groupBox2.Controls.Add(this.memoryProgressBar);
            this.groupBox2.Controls.Add(this.temperatureLabel);
            this.groupBox2.Controls.Add(this.memoryLabel);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.utilProgressBar);
            this.groupBox2.Controls.Add(this.utilLabel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.gpuComboBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.processorComboBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 404);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Processors";
            // 
            // temperatureProgressBar
            // 
            this.temperatureProgressBar.Location = new System.Drawing.Point(22, 362);
            this.temperatureProgressBar.Name = "temperatureProgressBar";
            this.temperatureProgressBar.Size = new System.Drawing.Size(303, 36);
            this.temperatureProgressBar.TabIndex = 20;
            // 
            // memoryProgressBar
            // 
            this.memoryProgressBar.Location = new System.Drawing.Point(22, 276);
            this.memoryProgressBar.Name = "memoryProgressBar";
            this.memoryProgressBar.Size = new System.Drawing.Size(303, 36);
            this.memoryProgressBar.TabIndex = 20;
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.ForeColor = System.Drawing.Color.Blue;
            this.temperatureLabel.Location = new System.Drawing.Point(186, 334);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(45, 25);
            this.temperatureLabel.TabIndex = 19;
            this.temperatureLabel.Text = "0 C";
            this.temperatureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // memoryLabel
            // 
            this.memoryLabel.AutoSize = true;
            this.memoryLabel.Location = new System.Drawing.Point(186, 248);
            this.memoryLabel.Name = "memoryLabel";
            this.memoryLabel.Size = new System.Drawing.Size(49, 25);
            this.memoryLabel.TabIndex = 19;
            this.memoryLabel.Text = "0 %";
            this.memoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 334);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 25);
            this.label11.TabIndex = 18;
            this.label11.Text = "Temprature:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 248);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(163, 25);
            this.label9.TabIndex = 18;
            this.label9.Text = "Memory Usage:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // utilProgressBar
            // 
            this.utilProgressBar.Location = new System.Drawing.Point(22, 193);
            this.utilProgressBar.Name = "utilProgressBar";
            this.utilProgressBar.Size = new System.Drawing.Size(303, 36);
            this.utilProgressBar.TabIndex = 17;
            // 
            // utilLabel
            // 
            this.utilLabel.AutoSize = true;
            this.utilLabel.Location = new System.Drawing.Point(186, 165);
            this.utilLabel.Name = "utilLabel";
            this.utilLabel.Size = new System.Drawing.Size(49, 25);
            this.utilLabel.TabIndex = 15;
            this.utilLabel.Text = "0 %";
            this.utilLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gpuTimer
            // 
            this.gpuTimer.Interval = 1000;
            this.gpuTimer.Tick += new System.EventHandler(this.gpuTimer_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.logConsole);
            this.groupBox3.Location = new System.Drawing.Point(366, 184);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(819, 404);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log Console";
            // 
            // logConsole
            // 
            this.logConsole.Location = new System.Drawing.Point(6, 30);
            this.logConsole.Multiline = true;
            this.logConsole.Name = "logConsole";
            this.logConsole.ReadOnly = true;
            this.logConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logConsole.Size = new System.Drawing.Size(807, 368);
            this.logConsole.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(1191, 532);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(213, 56);
            this.exitButton.TabIndex = 18;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 629);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cropSizeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deployButton);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "EENN";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar temperatureProgressBar;
        private System.Windows.Forms.ProgressBar memoryProgressBar;
        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.Label memoryLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ProgressBar utilProgressBar;
        private System.Windows.Forms.Label utilLabel;
        private System.Windows.Forms.Timer gpuTimer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox logConsole;
        private System.Windows.Forms.Button exitButton;
    }
}

