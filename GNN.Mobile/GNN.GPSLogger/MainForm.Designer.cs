namespace GNN.GPSLogger
{
    partial class MainForm
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
            this.menuItemStop = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelSpeedometer = new System.Windows.Forms.Label();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.labelLongitude = new System.Windows.Forms.Label();
            this.labelN = new System.Windows.Forms.Label();
            this.labelE = new System.Windows.Forms.Label();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.resetTimer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemStart);
            this.mainMenu1.MenuItems.Add(this.menuItemStop);
            this.mainMenu1.MenuItems.Add(this.menuItemExit);
            // 
            // menuItemStart
            // 
            this.menuItemStart.Text = "Start";
            this.menuItemStart.Click += new System.EventHandler(this.MenuItemStart_Click);
            // 
            // menuItemStop
            // 
            this.menuItemStop.Text = "Stop";
            this.menuItemStop.Click += new System.EventHandler(this.MenuItemStop_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);
            // 
            // labelSpeed
            // 
            this.labelSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSpeed.Font = new System.Drawing.Font("Tahoma", 48F, System.Drawing.FontStyle.Regular);
            this.labelSpeed.Location = new System.Drawing.Point(3, 24);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(234, 80);
            this.labelSpeed.Text = "000.0";
            this.labelSpeed.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSpeedometer
            // 
            this.labelSpeedometer.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSpeedometer.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.labelSpeedometer.Location = new System.Drawing.Point(0, 0);
            this.labelSpeedometer.Name = "labelSpeedometer";
            this.labelSpeedometer.Size = new System.Drawing.Size(240, 20);
            this.labelSpeedometer.Text = "Speedometer";
            this.labelSpeedometer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelLatitude
            // 
            this.labelLatitude.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.labelLatitude.Location = new System.Drawing.Point(103, 104);
            this.labelLatitude.Name = "labelLatitude";
            this.labelLatitude.Size = new System.Drawing.Size(125, 20);
            this.labelLatitude.Text = "00.00000000";
            this.labelLatitude.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelLongitude
            // 
            this.labelLongitude.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.labelLongitude.Location = new System.Drawing.Point(103, 124);
            this.labelLongitude.Name = "labelLongitude";
            this.labelLongitude.Size = new System.Drawing.Size(125, 20);
            this.labelLongitude.Text = "000.00000000";
            this.labelLongitude.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelN
            // 
            this.labelN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelN.Location = new System.Drawing.Point(57, 104);
            this.labelN.Name = "labelN";
            this.labelN.Size = new System.Drawing.Size(40, 20);
            this.labelN.Text = "N:";
            // 
            // labelE
            // 
            this.labelE.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelE.Location = new System.Drawing.Point(57, 124);
            this.labelE.Name = "labelE";
            this.labelE.Size = new System.Drawing.Size(40, 20);
            this.labelE.Text = "E:";
            // 
            // labelDateTime
            // 
            this.labelDateTime.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.labelDateTime.Location = new System.Drawing.Point(3, 248);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(234, 20);
            this.labelDateTime.Text = "---";
            this.labelDateTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.labelE);
            this.Controls.Add(this.labelN);
            this.Controls.Add(this.labelLongitude);
            this.Controls.Add(this.labelLatitude);
            this.Controls.Add(this.labelSpeedometer);
            this.Controls.Add(this.labelSpeed);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "GPS Logger";
            this.ResumeLayout(false);
            this.Load += new System.EventHandler(MainForm_Load);
            this.Closed += new System.EventHandler(MainForm_Closed);
        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemStart;
        private System.Windows.Forms.MenuItem menuItemStop;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelSpeedometer;
        private System.Windows.Forms.Label labelLatitude;
        private System.Windows.Forms.Label labelLongitude;
        private System.Windows.Forms.Label labelN;
        private System.Windows.Forms.Label labelE;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.Label labelDateTime;
        private System.Windows.Forms.Timer resetTimer;
    }
}

