namespace GNN.Terminal
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

    public partial class MainForm : Form
    {
        private SerialPort serialPort;

        public MainForm()
        {
            InitializeComponent();

            serialPort = new SerialPort();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

            menuItemStart.Enabled = false;
            menuItemStop.Enabled = false;
        }

        private void OpenPort()
        {
            if (serialPort.IsOpen)
            {
                return;
            }

            try
            {
                serialPort.Open();

                buttonSetup.Enabled = false;
                menuItemStart.Enabled = false;
                menuItemStop.Enabled = true;
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
        }

        private void ClosePort()
        {
            if (!serialPort.IsOpen)
            {
                return;
            }

            try
            {
                serialPort.Close();

                buttonSetup.Enabled = true;
                menuItemStart.Enabled = true;
                menuItemStop.Enabled = false;
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
        }

        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            textBoxDataReceived.BeginInvoke((Action)(() =>
            {
                if (!string.IsNullOrEmpty(textBoxDataReceived.Text) && textBoxDataReceived.Text.Length > 1000)
                {
                    textBoxDataReceived.Text = string.Empty;
                }

                try
                {
                    textBoxDataReceived.Text = serialPort.ReadExisting() + textBoxDataReceived.Text;
                }
                catch (InvalidOperationException)
                {
                }
            }));
        }

        private void ButtonSetup_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                return;
            }

            var form = new DeviceForm();
            if (form.ShowDialog() == DialogResult.Yes)
            {
                serialPort.PortName = form.PortName;
                serialPort.BaudRate = form.BaudRate;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;

                OpenPort();
            }
        }

        private void MenuItemStart_Click(object sender, EventArgs e)
        {
            OpenPort();
        }

        private void MenuItemStop_Click(object sender, EventArgs e)
        {
            ClosePort();
        }
    }
}