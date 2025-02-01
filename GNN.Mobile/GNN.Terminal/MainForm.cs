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

        private bool saveToFile;

        private string filePath;

        private FileStream output;

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

            if (saveToFile && Directory.Exists(filePath))
            {
                output = File.Open(filePath, FileMode.Append, FileAccess.Write);
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
                MessageBox.Show(
                   ex.Message,
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
            }
            finally
            {
                CloseStream();
            }
        }

        private void CloseStream()
        {
            if (output != null)
            {
                output.Close();
                output.Dispose();
                output = null;
            }
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

        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = ReadExistingData();
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            WriteToFile(data);

            textBoxDataReceived.BeginInvoke((Action)(() =>
            {
                if (!string.IsNullOrEmpty(textBoxDataReceived.Text) && textBoxDataReceived.Text.Length > 1000)
                {
                    textBoxDataReceived.Text = string.Empty;
                }

                textBoxDataReceived.Text = data + textBoxDataReceived.Text;
            }));
        }

        private void ButtonSetup_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                return;
            }

            var deviceForm = new DeviceForm();
            if (deviceForm.ShowDialog() == DialogResult.Yes)
            {
                serialPort.PortName = deviceForm.PortName;
                serialPort.BaudRate = deviceForm.BaudRate;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;

                if (deviceForm.SaveToFile && Directory.Exists(deviceForm.FilePath))
                {
                    saveToFile = deviceForm.SaveToFile;
                    filePath = deviceForm.FilePath;
                    output = File.Open(filePath, FileMode.Append, FileAccess.Write);
                }

                OpenPort();
            }
        }


        private void ButtonSend_Click(object sender, EventArgs e)
        {
            var input = textBoxInput.Text;
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.WriteLine(input);
                    textBoxDataReceived.Text = textBoxDataReceived.Text + Environment.NewLine + input;
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show(
                       ex.Message,
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                }
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