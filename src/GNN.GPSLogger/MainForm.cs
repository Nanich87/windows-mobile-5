namespace GNN.GPSLogger
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.IO;
    using System.IO.Ports;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using GNN.Common;
    using GNN.NMEAParser;
    using Microsoft.Win32;

    public partial class MainForm : Form
    {
        private SerialPort serialPort;

        private FileStream output;

        [DllImport("CoreDLL")]
        public static extern void SystemIdleTimerReset();

        public MainForm()
        {
            InitializeComponent();

            var configFilePath = string.Format("{0}.config", System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            ConfigurationManager.Init(configFilePath);

            var portName = ConfigurationManager.AppSettings["portName"];
            var baudRate = int.Parse(ConfigurationManager.AppSettings["baudRate"]);

            serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

            menuItemStart.Enabled = true;
            menuItemStop.Enabled = false;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            // Set the interval on our timer and start the
            // timer. It will run for the duration of the
            // program
            var interval = ShortestTimeoutInterval();
            resetTimer.Interval = interval;
            resetTimer.Enabled = true;
            resetTimer.Tick += ResetTimer_Tick;
        }

        private void MainForm_Closed(object sender, System.EventArgs e)
        {
            resetTimer.Enabled = false;
            resetTimer.Tick -= ResetTimer_Tick;
        }

        private void MenuItemFile_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                output = File.Open(saveFileDialog.FileName, FileMode.Append, FileAccess.Write);
            }
        }

        private void MenuItemStart_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                return;
            }

            var success = false;

            try
            {
                serialPort.Open();
                success = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }

            if (success)
            {
                menuItemStart.Enabled = false;
                menuItemStop.Enabled = true;
            }
        }

        private void MenuItemStop_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                return;
            }

            var success = false;

            try
            {
                serialPort.Close();
                success = true;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                   ex.Message,
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
            }

            if (success)
            {
                menuItemStop.Enabled = false;
                menuItemStart.Enabled = true;
            }
        }

        private void MenuItemExport_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                   ex.Message,
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Application.Exit();
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = ReadExistingData();
            NMEAParser.Instance.Parse(data);

            var latitude = string.Format("{0:0.00000000}", NMEAParser.Instance.GetLatitude());

            labelLatitude.BeginInvoke((Action)(() =>
            {
                labelLatitude.Text = latitude;
            }));

            var longitude = string.Format("{0:0.00000000}", NMEAParser.Instance.GetLongitude());

            labelLongitude.BeginInvoke((Action)(() =>
            {
                labelLongitude.Text = longitude;
            }));

            var altitude = string.Format("{0:0.000}", NMEAParser.Instance.GetAltitude());

            labelAltitude.BeginInvoke((Action)(() =>
            {
                labelAltitude.Text = altitude;
            }));

            labelSpeed.BeginInvoke((Action)(() =>
            {
                labelSpeed.Text = string.Format("{0:0.0}", NMEAParser.Instance.GetSpeed());
            }));

            var dateTime = string.Format("{0:dd.MM.yyyy HH:mm:ss}", NMEAParser.Instance.GetDateTime());

            labelDateTime.BeginInvoke((Action)(() =>
            {
                labelDateTime.Text = dateTime;
            }));

            var point = string.Format("{0} {1} {2} {3}{4}", latitude, longitude, altitude, dateTime, Environment.NewLine);
            WriteToFile(point);
        }

        private void ResetTimer_Tick(object sender, EventArgs e)
        {
            SystemIdleTimerReset();
        }

        private string ReadExistingData()
        {
            string data = null;

            try
            {
                data = serialPort.ReadExisting();
            }
            catch (InvalidOperationException)
            {
            }

            return data;
        }

        private void WriteToFile(string data)
        {
            try
            {
                if (output != null)
                {
                    var buffer = Encoding.Default.GetBytes(data);
                    output.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
            }
        }

        // Look in the registry to see what the shortest timeout
        // period is. Note that Zero is a special value with respect
        // to timeouts. It indicates that a timeout will not occur.
        // As long as SystemIdleTimeerReset is called on intervals
        // that are shorter than the smallest non-zero timeout value
        // then the device will not sleep from idleness. This does
        // not prevent the device from sleeping due to the power
        // button being pressed.
        private int ShortestTimeoutInterval()
        {
            var retVal = 1000;
            var key = Registry.LocalMachine.OpenSubKey(@"\SYSTEM\CurrentControlSet\Control\Power");
            var oBatteryTimeout = key.GetValue("BattPowerOff");
            var oACTimeOut = key.GetValue("ExtPowerOff");
            var oScreenPowerOff = key.GetValue("ScreenPowerOff");

            if (oBatteryTimeout is int)
            {
                var v = (int)oBatteryTimeout;
                if (v > 0)
                {
                    retVal = Math.Min(retVal, v);
                }
            }

            if (oACTimeOut is int)
            {
                var v = (int)oACTimeOut;
                if (v > 0)
                {
                    retVal = Math.Min(retVal, v);
                }
            }

            if (oScreenPowerOff is int)
            {
                var v = (int)oScreenPowerOff;
                if (v > 0)
                {
                    retVal = Math.Min(retVal, v);
                }
            }

            //Since the interval is in seconds and out timer
            //operates in milliseconds the value needs to be multiplied
            //by 1000 to get the appropriate millisecond value. I've
            //multiplied by 900 instead so that I ensure that I call
            //SystemIdleTimerReset before the timeout is reached.
            return retVal * 900;
        }
    }
}