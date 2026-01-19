namespace GNN.Terminal
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO.Ports;
    using System.Text;
    using System.Windows.Forms;
    using GNN.Common;

    public partial class DeviceForm : Form
    {
        public DeviceForm()
        {
            InitializeComponent();

            var ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                comboBoxPort.Items.Add(port);
            }

            var configFile = string.Format("{0}.config", System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            ConfigurationManager.Init(configFile);

            var lastPortName = ConfigurationManager.AppSettings["portName"];
            var lastPortIndex = Array.IndexOf(ports, lastPortName);
            comboBoxPort.SelectedIndex = lastPortIndex != -1 ? lastPortIndex : 0;

            var baudRates = new string[] { "2400", "4800", "9600", "19200", "28800", "38400", "57600", "76800", "115200" };
            foreach (var baudRate in baudRates)
            {
                comboBoxBaudRate.Items.Add(baudRate);
            }
            
            var lastBaudRate = ConfigurationManager.AppSettings["baudRate"];
            var lastBaudRateIndex = Array.IndexOf(baudRates, lastBaudRate);
            comboBoxBaudRate.SelectedIndex = lastBaudRateIndex != -1 ? lastBaudRateIndex : 0;
        }

        public string PortName { get; private set; }

        public int BaudRate { get; private set; }

        public bool SaveToFile { get; private set; }

        public string FilePath { get; private set; }

        private void MenuItemStart_Click(object sender, EventArgs e)
        {
            PortName = comboBoxPort.SelectedItem.ToString();
            BaudRate = int.Parse(comboBoxBaudRate.SelectedItem.ToString());

            ConfigurationManager.AppSettings["portName"] = PortName;
            ConfigurationManager.AppSettings["baudRate"] = BaudRate.ToString();
            ConfigurationManager.Save();

            DialogResult = DialogResult.Yes;
        }

        private void CheckBoxSaveToFile_CheckStateChanged(object sender, EventArgs e)
        {
            SaveToFile = checkBoxSaveToFile.Checked;
        }

        private void ButtonSelectSaveFolder_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = saveFileDialog.FileName;
            }
        }
    }
}