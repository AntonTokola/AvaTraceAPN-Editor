namespace AvaTrace_APN_Editor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            apnTextBox = new TextBox();
            apnComboBox = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            startButton = new Button();
            applyApnAddressButton = new Button();
            statusText = new Label();
            scanAvaTraceUnit = new Button();
            currentApnAddress = new Label();
            unitIdentifier = new Label();
            loadingImage = new PictureBox();
            avaImage = new PictureBox();
            quitButton = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)loadingImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)avaImage).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(2, 125);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(554, 30);
            label1.TabIndex = 0;
            label1.Text = "Syötä uusi APN-osoite manuaalisesti, tai valitse alasvetolaatikosta valmis tietyn operaattorin osoite.\r\n\r\n";
            label1.Click += label1_Click;
            // 
            // apnTextBox
            // 
            apnTextBox.Location = new Point(249, 189);
            apnTextBox.Margin = new Padding(2);
            apnTextBox.Name = "apnTextBox";
            apnTextBox.Size = new Size(300, 23);
            apnTextBox.TabIndex = 1;
            apnTextBox.TextChanged += apnTextBox_TextChanged;
            // 
            // apnComboBox
            // 
            apnComboBox.FormattingEnabled = true;
            apnComboBox.Location = new Point(249, 158);
            apnComboBox.Margin = new Padding(2);
            apnComboBox.Name = "apnComboBox";
            apnComboBox.Size = new Size(300, 23);
            apnComboBox.TabIndex = 3;
            apnComboBox.SelectedIndexChanged += apnComboBox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(66, 191);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(155, 15);
            label3.TabIndex = 5;
            label3.Text = "Syötä osoite manuaalisesti:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(66, 160);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(124, 15);
            label4.TabIndex = 6;
            label4.Text = "Valitse osoite listasta:";
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            startButton.Location = new Point(118, 7);
            startButton.Margin = new Padding(2);
            startButton.Name = "startButton";
            startButton.Size = new Size(335, 32);
            startButton.TabIndex = 7;
            startButton.Text = "Aloita tarkistamalla Nmap-sovelluksen sijainti";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += button1_Click;
            // 
            // applyApnAddressButton
            // 
            applyApnAddressButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            applyApnAddressButton.Location = new Point(314, 223);
            applyApnAddressButton.Margin = new Padding(2);
            applyApnAddressButton.Name = "applyApnAddressButton";
            applyApnAddressButton.Size = new Size(234, 24);
            applyApnAddressButton.TabIndex = 8;
            applyApnAddressButton.Text = "Syötä osoite laitteelle";
            applyApnAddressButton.UseVisualStyleBackColor = true;
            applyApnAddressButton.Click += applyApnAddressButton_Click;
            // 
            // statusText
            // 
            statusText.AutoSize = true;
            statusText.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            statusText.Location = new Point(11, 9);
            statusText.Margin = new Padding(2, 0, 2, 0);
            statusText.Name = "statusText";
            statusText.Size = new Size(0, 19);
            statusText.TabIndex = 9;
            // 
            // scanAvaTraceUnit
            // 
            scanAvaTraceUnit.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            scanAvaTraceUnit.Location = new Point(159, 117);
            scanAvaTraceUnit.Margin = new Padding(2);
            scanAvaTraceUnit.Name = "scanAvaTraceUnit";
            scanAvaTraceUnit.Size = new Size(241, 58);
            scanAvaTraceUnit.TabIndex = 10;
            scanAvaTraceUnit.Text = "Suorita laitteen luku";
            scanAvaTraceUnit.UseVisualStyleBackColor = true;
            scanAvaTraceUnit.Click += scanAvaTraceUnit_Click;
            // 
            // currentApnAddress
            // 
            currentApnAddress.AutoSize = true;
            currentApnAddress.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            currentApnAddress.Location = new Point(11, 58);
            currentApnAddress.Name = "currentApnAddress";
            currentApnAddress.Size = new Size(0, 19);
            currentApnAddress.TabIndex = 11;
            currentApnAddress.Click += currentApnAddress_Click;
            // 
            // unitIdentifier
            // 
            unitIdentifier.AutoSize = true;
            unitIdentifier.BorderStyle = BorderStyle.Fixed3D;
            unitIdentifier.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            unitIdentifier.Location = new Point(372, 9);
            unitIdentifier.Name = "unitIdentifier";
            unitIdentifier.Size = new Size(2, 23);
            unitIdentifier.TabIndex = 13;
            // 
            // loadingImage
            // 
            loadingImage.Location = new Point(239, 80);
            loadingImage.Margin = new Padding(2);
            loadingImage.Name = "loadingImage";
            loadingImage.Size = new Size(89, 87);
            loadingImage.SizeMode = PictureBoxSizeMode.StretchImage;
            loadingImage.TabIndex = 14;
            loadingImage.TabStop = false;
            loadingImage.Click += loadingImage_Click;
            // 
            // avaImage
            // 
            avaImage.Location = new Point(172, 44);
            avaImage.Margin = new Padding(2);
            avaImage.Name = "avaImage";
            avaImage.Size = new Size(239, 218);
            avaImage.SizeMode = PictureBoxSizeMode.StretchImage;
            avaImage.TabIndex = 15;
            avaImage.TabStop = false;
            // 
            // quitButton
            // 
            quitButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            quitButton.Location = new Point(212, 211);
            quitButton.Margin = new Padding(2);
            quitButton.Name = "quitButton";
            quitButton.Size = new Size(141, 32);
            quitButton.TabIndex = 16;
            quitButton.Text = "Lopeta";
            quitButton.UseVisualStyleBackColor = true;
            quitButton.Click += quitButton_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(560, 270);
            panel1.TabIndex = 17;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(560, 270);
            Controls.Add(quitButton);
            Controls.Add(unitIdentifier);
            Controls.Add(currentApnAddress);
            Controls.Add(statusText);
            Controls.Add(applyApnAddressButton);
            Controls.Add(startButton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(apnComboBox);
            Controls.Add(apnTextBox);
            Controls.Add(scanAvaTraceUnit);
            Controls.Add(loadingImage);
            Controls.Add(avaImage);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "Form1";
            Text = "AvaTraceAPN Editor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)loadingImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)avaImage).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox apnTextBox;
        private ComboBox apnComboBox;
        private Label label3;
        private Label label4;
        private Button startButton;
        private Button applyApnAddressButton;
        private Label statusText;
        private Button scanAvaTraceUnit;
        private Label currentApnAddress;
        private Label unitIdentifier;
        private PictureBox loadingImage;
        private PictureBox avaImage;
        private Button quitButton;
        private Panel panel1;
    }
}