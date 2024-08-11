namespace GNN.Terminal
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu bottomMenu;

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
            this.bottomMenu = new System.Windows.Forms.MainMenu();
            this.menuItemStart = new System.Windows.Forms.MenuItem();
            this.menuItemStop = new System.Windows.Forms.MenuItem();
            this.textBoxDataReceived = new System.Windows.Forms.TextBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelDataReceived = new System.Windows.Forms.Label();
            this.buttonSetup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bottomMenu
            // 
            this.bottomMenu.MenuItems.Add(this.menuItemStart);
            this.bottomMenu.MenuItems.Add(this.menuItemStop);
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
            // textBoxDataReceived
            // 
            this.textBoxDataReceived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDataReceived.Location = new System.Drawing.Point(4, 30);
            this.textBoxDataReceived.Multiline = true;
            this.textBoxDataReceived.Name = "textBoxDataReceived";
            this.textBoxDataReceived.Size = new System.Drawing.Size(233, 200);
            this.textBoxDataReceived.TabIndex = 2;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(4, 236);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(155, 21);
            this.textBoxInput.TabIndex = 1;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(165, 236);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(72, 20);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send";
            // 
            // labelDataReceived
            // 
            this.labelDataReceived.Location = new System.Drawing.Point(4, 7);
            this.labelDataReceived.Name = "labelDataReceived";
            this.labelDataReceived.Size = new System.Drawing.Size(100, 20);
            this.labelDataReceived.Text = "Data:";
            // 
            // buttonSetup
            // 
            this.buttonSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetup.Location = new System.Drawing.Point(164, 4);
            this.buttonSetup.Name = "buttonSetup";
            this.buttonSetup.Size = new System.Drawing.Size(72, 20);
            this.buttonSetup.TabIndex = 1;
            this.buttonSetup.Text = "Setup";
            this.buttonSetup.Click += new System.EventHandler(this.ButtonSetup_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.buttonSetup);
            this.Controls.Add(this.labelDataReceived);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.textBoxDataReceived);
            this.Menu = this.bottomMenu;
            this.Name = "MainForm";
            this.Text = "Terminal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDataReceived;
        private System.Windows.Forms.MenuItem menuItemStart;
        private System.Windows.Forms.MenuItem menuItemStop;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label labelDataReceived;
        private System.Windows.Forms.Button buttonSetup;
    }
}

