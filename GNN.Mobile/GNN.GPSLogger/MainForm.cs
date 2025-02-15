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
    using System.Text;
    using System.Windows.Forms;
    using GNN.NMEAParser;

    public partial class MainForm : Form
    {
        private SerialPort serialPort;

        public MainForm()
        {
            InitializeComponent();

            serialPort = new SerialPort();
            serialPort.PortName = "COM2";
            serialPort.BaudRate = 57600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

            menuItemStart.Enabled = true;
            menuItemStop.Enabled = false;
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

        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = ReadExistingData();
            NMEAParser.Instance.Parse(data);

            labelLatitude.BeginInvoke((Action)(() =>
            {
                labelLatitude.Text = string.Format("{0:0.00000000}", NMEAParser.Instance.GetLatitude());
            }));

            labelLongitude.BeginInvoke((Action)(() =>
            {
                labelLongitude.Text = string.Format("{0:0.00000000}", NMEAParser.Instance.GetLongitude());
            }));

            labelSpeed.BeginInvoke((Action)(() =>
            {
                labelSpeed.Text = string.Format("{0:0.0}", NMEAParser.Instance.GetSpeed());
            }));

            labelDateTime.BeginInvoke((Action)(() =>
            {
                labelDateTime.Text = string.Format("{0:dd.MM.yy HH:mm:ss}", NMEAParser.Instance.GetDateTime());
            }));
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
    }
}