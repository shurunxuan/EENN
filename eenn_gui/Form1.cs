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

        [DllImport("eenn_utils.dll", EntryPoint = "get_gpu_slowdown_temperature")]
        static extern uint get_gpu_slowdown_temperature(uint index);

        [DllImport("eenn_utils.dll", EntryPoint = "get_gpu_shutdown_temperature")]
        static extern uint get_gpu_shutdown_temperature(uint index);

        [DllImport("eenn_utils.dll", EntryPoint = "get_gpu_temperature")]
        static extern uint get_gpu_temperature(uint index);

        [DllImport("eenn_utils.dll", EntryPoint = "get_gpu_utilization")]
        static extern uint get_gpu_utilization(uint index);

        [DllImport("eenn_utils.dll", EntryPoint = "get_gpu_memory_usage")]
        static extern uint get_gpu_memory_usage(uint index);

        private Deploy deploy;
        private uint currentGpuDevice = 32767;
        private uint gpu_slowdown_temperature;
        private uint gpu_shutdown_temperature;

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

                // Slow down monitoring GPU
                gpuTimer.Interval = 1000;

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
                deploy.GpuDevice = (uint)gpuComboBox.SelectedIndex;
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

            // Fast up monitoring GPU
            gpuTimer.Enabled = true;
            gpuTimer.Interval = 500;

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

        private void gpuTimer_Tick(object sender, EventArgs e)
        {
            if (currentGpuDevice == 32767) return;
            uint gpu_temperature = get_gpu_temperature(currentGpuDevice);
            uint gpu_utilization = get_gpu_utilization(currentGpuDevice);
            uint gpu_memory = get_gpu_memory_usage(currentGpuDevice);

            utilLabel.Text = gpu_utilization + @" %";
            memoryLabel.Text = gpu_memory + @" %";
            temperatureLabel.Text = gpu_temperature + @" C";
            if (gpu_temperature >= 0.5 * (gpu_shutdown_temperature - gpu_slowdown_temperature) + gpu_slowdown_temperature)
                temperatureLabel.ForeColor = Color.Red;
            else if (gpu_temperature >= 0.7 * gpu_slowdown_temperature)
                temperatureLabel.ForeColor = Color.Olive;
            else
                temperatureLabel.ForeColor = Color.Blue;

            utilProgressBar.Value = (int)gpu_utilization;
            memoryProgressBar.Value = (int)gpu_memory;
            temperatureProgressBar.Value = (int)gpu_temperature;

        }

        private void gpuComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentGpuDevice = (uint)gpuComboBox.SelectedIndex;
            // Get GPU Temperature Threshod
            gpu_shutdown_temperature = get_gpu_shutdown_temperature(currentGpuDevice);
            gpu_slowdown_temperature = get_gpu_slowdown_temperature(currentGpuDevice);
            temperatureProgressBar.Maximum = (int)gpu_shutdown_temperature;
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
