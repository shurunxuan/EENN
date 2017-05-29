using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eenn_gui
{

    public partial class Form1 : Form
    {
        [DllImport("eenn_utils.dll", EntryPoint = "set_progress")]
        static extern int set_progress(float value);

        [DllImport("eenn_utils.dll", EntryPoint = "get_progress")]
        static extern float get_progress();

        [DllImport("eenn_utils.dll", EntryPoint = "init_nvml")]
        static extern bool init_nvml();

        [DllImport("eenn_utils.dll", EntryPoint = "get_gpu_count")]
        static extern int get_gpu_count();

        [DllImport("eenn_utils.dll", EntryPoint = "get_gpu_name")]
        static extern IntPtr get_gpu_name(uint index);

        private Deploy deploy;

        public Form1()
        {
            deploy = new Deploy();

            InitializeComponent();

            bool result = init_nvml();
            if (result)
            {
                int count = get_gpu_count();
                if (count == -1)
                    return;
                processorComboBox.Items.Add("GPU");
                for (uint i = 0; i < (uint)count; ++i)
                {
                    byte[] name = new byte[64];
                    IntPtr nameptr = get_gpu_name(i);
                    Marshal.Copy(nameptr, name, 0, 64);
                    string namestr = Encoding.Default.GetString(name);
                    gpuComboBox.Items.Add(namestr);
                }
            }
        }

        private void progressTimer_Tick(object sender, EventArgs e)
        {
            int progress = (int)(get_progress() * 100);
            progressBar1.Value = progress;
            if (progress >= 9999) // Deploy finished
            {
                // Stop getting progress
                progressTimer.Enabled = false;

                // Enable textBoxes and buttons
                deployButton.Enabled = true;
                inputFile.Enabled = true;
                outputFile.Enabled = true;

                MessageBox.Show(@"Done!");
            }
        }

        private void deployButton_Click(object sender, EventArgs e)
        {
            set_progress(0);
            progressBar1.Value = 0;

            // Set input parameters
            if (!File.Exists(inputFile.Text))
            {
                MessageBox.Show(@"Specify a valid input file name!", @"ERROR");
                return;
            }
            deploy.InputFile = inputFile.Text;
            if (outputFile.Text == "")
            {
                MessageBox.Show(@"Specify an output file name!", @"ERROR");
                return;
            }
            deploy.OutputFile = outputFile.Text;
            deploy.Prototxt = ".\\model\\deploy.prototxt";
            deploy.Caffemodel = ".\\model\\EENN.caffemodel";
            if (processorComboBox.SelectedIndex < 0)
            {
                MessageBox.Show(@"Select a processor!", @"ERROR");
                return;
            }
            deploy.UsingGpu = processorComboBox.SelectedIndex == 1;
            if (deploy.UsingGpu)
            {
                if (gpuComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Select a GPU!", @"ERROR");
                    return;
                }
                deploy.GpuDevice = (uint) gpuComboBox.SelectedIndex;
            }
            else
            {
                deploy.GpuDevice = 0;
            }
            deploy.CropSize = uint.Parse(cropSizeTextBox.Text);
            if (deploy.CropSize <= 20)
            {
                MessageBox.Show(@"Crpo size should be bigger than 20", @"ERROR");
                return;
            }
            deploy.OutputLog = false;


            // Start thread
            Thread deployThread = new Thread(deploy.Run);
            deployThread.Start();

            // Start getting progress
            progressTimer.Enabled = true;

            // Disable textBoxes and buttons
            deployButton.Enabled = false;
            inputFile.Enabled = false;
            outputFile.Enabled = false;
        }

        private void cropSizeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void processorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (processorComboBox.Text == @"GPU")
            {
                gpuComboBox.Enabled = true;
            }
            else
            {
                gpuComboBox.Enabled = false;
            }
        }

        private void inputBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog =
                new OpenFileDialog
                {
                    Filter = @"Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png",
                    RestoreDirectory = true
                };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFile.Text = openFileDialog.FileName;
            }
        }

        private void outputBrowseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog =
                new SaveFileDialog
                {
                    Filter = @"Portable Network Graphics (*.png)|*.png|JPEG (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp",
                    RestoreDirectory = true
                };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputFile.Text = saveFileDialog.FileName;
            }
        }
    }

    public class Deploy
    {
        [DllImport("eenn_utils.dll", EntryPoint = "deploy")]
        private static extern int deploy(
            [MarshalAs(UnmanagedType.LPStr)]string proto,
            [MarshalAs(UnmanagedType.LPStr)]string model,
            [MarshalAs(UnmanagedType.LPStr)]string input,
            [MarshalAs(UnmanagedType.LPStr)]string output,
            bool using_gpu, uint gpu_device, uint crop_size, bool output_log);

        public string Prototxt;
        public string Caffemodel;
        public string InputFile;
        public string OutputFile;
        public bool UsingGpu;
        public uint GpuDevice;
        public uint CropSize;
        public bool OutputLog;

        public void Run()
        {
            int hr = deploy(Prototxt, Caffemodel, InputFile, OutputFile, UsingGpu, GpuDevice, CropSize, OutputLog);
        }
    }
}
