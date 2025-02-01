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

            comboBoxPort.SelectedIndex = 0;
            comboBoxBaudRate.SelectedIndex = 0;
        }

        public string PortName { get; private set; }

        public int BaudRate { get; private set; }

        public bool SaveToFile { get; private set; }

        public string FilePath { get; private set; }

        private void MenuItemStart_Click(object sender, EventArgs e)
        {
            PortName = comboBoxPort.SelectedItem.ToString();
            BaudRate = int.Parse(comboBoxBaudRate.SelectedItem.ToString());

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