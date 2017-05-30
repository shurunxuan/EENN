using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

        private readonly Deploy _deploy;
        private Thread _deployThread;
        private uint _currentGpuDevice = 32767;
        private uint _gpuSlowdownTemperature;
        private uint _gpuShutdownTemperature;

        private const int ScClose = 0xF060;
        private const int MfEnabled = 0x00000000;
        private const int MfGrayed = 0x00000001;
        private const int MfDisabled = 0x00000002;

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);

        [DllImport("User32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, int uIdEnableItem, int uEnable);


        public void WriteToLog(string str)
        {
            logConsole.Text += @"[" + DateTime.Now + @"]   " + str + @"
";
        }

        public Form1()
        {
            _deploy = new Deploy();

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
            var progress = (int)(get_progress() * 100);
            progressBar1.Value = progress;
            if (progress != 10000) return;
            _deployThread.Join();
            WriteToLog(_deploy.TimeElapsed / 1000.0 + " seconds elapsed.");
            // Stop getting progress
            progressTimer.Enabled = false;

            // Slow down monitoring GPU
            gpuTimer.Interval = 1000;

            // Enable textBoxes and buttons
            exitButton.Enabled = true;
            deployButton.Enabled = true;
            inputFile.Enabled = true;
            outputFile.Enabled = true;


            WriteToLog("Reconstruction finished.");
            MessageBox.Show(@"Done!");
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
            _deploy.InputFile = inputFile.Text;
            WriteToLog("Use \"" + _deploy.InputFile + "\" as input.");
            if (outputFile.Text == "")
            {
                MessageBox.Show(@"Specify an output file name!", @"ERROR");
                return;
            }
            _deploy.OutputFile = outputFile.Text;
            WriteToLog("Use \"" + _deploy.OutputFile + "\" as output.");
            _deploy.Prototxt = ".\\model\\deploy.prototxt";
            _deploy.Caffemodel = ".\\model\\EENN.caffemodel";
            if (processorComboBox.SelectedIndex < 0)
            {
                MessageBox.Show(@"Select a processor!", @"ERROR");
                return;
            }
            _deploy.UsingGpu = processorComboBox.SelectedIndex == 1;
            if (_deploy.UsingGpu)
            {
                if (gpuComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Select a GPU!", @"ERROR");
                    return;
                }
                _deploy.GpuDevice = (uint)gpuComboBox.SelectedIndex;
                WriteToLog("Using GPU " + _deploy.GpuDevice + ": " + gpuComboBox.Text);
            }
            else
            {
                _deploy.GpuDevice = 0;
                WriteToLog("Using CPU");
            }

            _deploy.CropSize = uint.Parse(cropSizeTextBox.Text);
            if (_deploy.CropSize <= 20)
            {
                MessageBox.Show(@"Crop size should be bigger than 20", @"ERROR");
                return;
            }
            _deploy.OutputLog = false;

            WriteToLog("Reconstruction started.");

            // Start thread
            _deployThread = new Thread(_deploy.Run);
            _deployThread.Start();

            // Start getting progress
            progressTimer.Enabled = true;

            // Fast up monitoring GPU
            gpuTimer.Enabled = true;
            gpuTimer.Interval = 500;

            // Disable textBoxes and buttons
            exitButton.Enabled = false;
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
            gpuComboBox.Enabled = processorComboBox.Text == @"GPU";
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
            if (_currentGpuDevice == 32767) return;
            uint gpuTemperature = get_gpu_temperature(_currentGpuDevice);
            uint gpuUtilization = get_gpu_utilization(_currentGpuDevice);
            uint gpuMemory = get_gpu_memory_usage(_currentGpuDevice);

            if (gpuTemperature > temperatureProgressBar.Maximum) return;
            if (gpuUtilization > utilProgressBar.Maximum) return;
            if (gpuMemory > memoryProgressBar.Maximum) return;

            utilLabel.Text = gpuUtilization + @" %";
            memoryLabel.Text = gpuMemory + @" %";
            temperatureLabel.Text = gpuTemperature + @" C";
            if (gpuTemperature >= 0.5 * (_gpuShutdownTemperature - _gpuSlowdownTemperature) + _gpuSlowdownTemperature)
                temperatureLabel.ForeColor = Color.Red;
            else if (gpuTemperature >= 0.7 * _gpuSlowdownTemperature)
                temperatureLabel.ForeColor = Color.Olive;
            else
                temperatureLabel.ForeColor = Color.Blue;

            utilProgressBar.Value = (int)gpuUtilization;
            memoryProgressBar.Value = (int)gpuMemory;
            temperatureProgressBar.Value = (int)gpuTemperature;

        }

        private void gpuComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentGpuDevice = (uint)gpuComboBox.SelectedIndex;
            // Get GPU Temperature Threshod
            _gpuShutdownTemperature = get_gpu_shutdown_temperature(_currentGpuDevice);
            _gpuSlowdownTemperature = get_gpu_slowdown_temperature(_currentGpuDevice);
            temperatureProgressBar.Maximum = (int)_gpuShutdownTemperature;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var hMenu = GetSystemMenu(Handle, 0);
            EnableMenuItem(hMenu, ScClose, (MfDisabled + MfGrayed) | MfEnabled);

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            bool usingGpu, uint gpuDevice, uint cropSize, bool outputLog);

        public string Prototxt;
        public string Caffemodel;
        public string InputFile;
        public string OutputFile;
        public bool UsingGpu;
        public uint GpuDevice;
        public uint CropSize;
        public bool OutputLog;

        public long TimeElapsed;
        public bool Completed;

        public void Run()
        {
            TimeElapsed = 0;
            Completed = false;
            var st = new Stopwatch();
            st.Start();

            deploy(Prototxt, Caffemodel, InputFile, OutputFile, UsingGpu, GpuDevice, CropSize, OutputLog);

            st.Stop();
            TimeElapsed = st.ElapsedMilliseconds;
            Completed = true;
        }
    }
}
