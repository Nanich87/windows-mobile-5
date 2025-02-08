namespace GNN.GPSLogger
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.IO.Ports;
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
            serialPort.PortName = "COM1";
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

            menuItemStart.Enabled = true;
            menuItemStop.Enabled = false;
        }

        private void menuItemStart_Click(object sender, EventArgs e)
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

        private void menuItemStop_Click(object sender, EventArgs e)
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

        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = ReadExistingData();
            NMEAParser.Instance.Parse(data);

            labelSpeed.BeginInvoke((Action)(() =>
            {
                labelSpeed.Text = string.Format("{0:0.0}", NMEAParser.Instance.GetSpeed());
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