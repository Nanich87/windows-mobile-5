namespace GNN.Terminal
{
    partial class DeviceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemStart = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemStart);
            // 
            // menuItemStart
            // 
            this.menuItemStart.Text = "Start";
            this.menuItemStart.Click += new System.EventHandler(this.MenuItemStart_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.Text = "Port:";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPort.Location = new System.Drawing.Point(92, 3);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(145, 22);
            this.comboBoxPort.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.Text = "Baud Rate:";
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBaudRate.Items.Add("2400");
            this.comboBoxBaudRate.Items.Add("4800");
            this.comboBoxBaudRate.Items.Add("9600");
            this.comboBoxBaudRate.Items.Add("19200");
            this.comboBoxBaudRate.Items.Add("28800");
            this.comboBoxBaudRate.Items.Add("38400");
            this.comboBoxBaudRate.Items.Add("57600");
            this.comboBoxBaudRate.Items.Add("76800");
            this.comboBoxBaudRate.Items.Add("115200");
            this.comboBoxBaudRate.Location = new System.Drawing.Point(92, 31);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(145, 22);
            this.comboBoxBaudRate.TabIndex = 3;
            // 
            // DeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxPort);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "DeviceForm";
            this.Text = "Device";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.MenuItem menuItemStart;
    }
}