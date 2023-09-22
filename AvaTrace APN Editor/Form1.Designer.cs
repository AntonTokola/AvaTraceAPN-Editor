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
            ((System.ComponentModel.ISupportInitialize)loadingImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)avaImage).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(-7, 210);
            label1.Name = "label1";
            label1.Size = new Size(862, 50);
            label1.TabIndex = 0;
            label1.Text = "Syötä uusi APN-osoite manuaalisesti, tai valitse alasvetolaatikosta valmis tietyn operaattorin osoite.\r\n\r\n";
            label1.Click += label1_Click;
            // 
            // apnTextBox
            // 
            apnTextBox.Location = new Point(356, 315);
            apnTextBox.Name = "apnTextBox";
            apnTextBox.Size = new Size(427, 31);
            apnTextBox.TabIndex = 1;
            apnTextBox.TextChanged += apnTextBox_TextChanged;
            // 
            // apnComboBox
            // 
            apnComboBox.FormattingEnabled = true;
            apnComboBox.Location = new Point(356, 263);
            apnComboBox.Name = "apnComboBox";
            apnComboBox.Size = new Size(427, 33);
            apnComboBox.TabIndex = 3;
            apnComboBox.SelectedIndexChanged += apnComboBox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(94, 318);
            label3.Name = "label3";
            label3.Size = new Size(243, 25);
            label3.TabIndex = 5;
            label3.Text = "Syötä osoite manuaalisesti:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(94, 267);
            label4.Name = "label4";
            label4.Size = new Size(195, 25);
            label4.TabIndex = 6;
            label4.Text = "Valitse osoite listasta:";
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            startButton.Location = new Point(169, 12);
            startButton.Name = "startButton";
            startButton.Size = new Size(478, 53);
            startButton.TabIndex = 7;
            startButton.Text = "Aloita tarkistamalla Nmap-sovelluksen sijainti";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += button1_Click;
            // 
            // applyApnAddressButton
            // 
            applyApnAddressButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            applyApnAddressButton.Location = new Point(449, 372);
            applyApnAddressButton.Name = "applyApnAddressButton";
            applyApnAddressButton.Size = new Size(334, 40);
            applyApnAddressButton.TabIndex = 8;
            applyApnAddressButton.Text = "Syötä osoite laitteelle";
            applyApnAddressButton.UseVisualStyleBackColor = true;
            applyApnAddressButton.Click += applyApnAddressButton_Click;
            // 
            // statusText
            // 
            statusText.AutoSize = true;
            statusText.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            statusText.Location = new Point(16, 15);
            statusText.Name = "statusText";
            statusText.Size = new Size(0, 28);
            statusText.TabIndex = 9;
            // 
            // scanAvaTraceUnit
            // 
            scanAvaTraceUnit.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            scanAvaTraceUnit.Location = new Point(227, 195);
            scanAvaTraceUnit.Name = "scanAvaTraceUnit";
            scanAvaTraceUnit.Size = new Size(344, 97);
            scanAvaTraceUnit.TabIndex = 10;
            scanAvaTraceUnit.Text = "Suorita laitteen luku";
            scanAvaTraceUnit.UseVisualStyleBackColor = true;
            scanAvaTraceUnit.Click += scanAvaTraceUnit_Click;
            // 
            // currentApnAddress
            // 
            currentApnAddress.AutoSize = true;
            currentApnAddress.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            currentApnAddress.Location = new Point(16, 97);
            currentApnAddress.Margin = new Padding(4, 0, 4, 0);
            currentApnAddress.Name = "currentApnAddress";
            currentApnAddress.Size = new Size(0, 28);
            currentApnAddress.TabIndex = 11;
            currentApnAddress.Click += currentApnAddress_Click;
            // 
            // unitIdentifier
            // 
            unitIdentifier.AutoSize = true;
            unitIdentifier.BorderStyle = BorderStyle.Fixed3D;
            unitIdentifier.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            unitIdentifier.Location = new Point(531, 15);
            unitIdentifier.Margin = new Padding(4, 0, 4, 0);
            unitIdentifier.Name = "unitIdentifier";
            unitIdentifier.Size = new Size(2, 34);
            unitIdentifier.TabIndex = 13;
            // 
            // loadingImage
            // 
            loadingImage.Location = new Point(342, 138);
            loadingImage.Name = "loadingImage";
            loadingImage.Size = new Size(122, 122);
            loadingImage.SizeMode = PictureBoxSizeMode.Zoom;
            loadingImage.TabIndex = 14;
            loadingImage.TabStop = false;
            loadingImage.Click += loadingImage_Click;
            // 
            // avaImage
            // 
            avaImage.Location = new Point(245, 74);
            avaImage.Name = "avaImage";
            avaImage.Size = new Size(341, 364);
            avaImage.SizeMode = PictureBoxSizeMode.StretchImage;
            avaImage.TabIndex = 15;
            avaImage.TabStop = false;
            // 
            // quitButton
            // 
            quitButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            quitButton.Location = new Point(303, 352);
            quitButton.Name = "quitButton";
            quitButton.Size = new Size(201, 54);
            quitButton.TabIndex = 16;
            quitButton.Text = "Lopeta";
            quitButton.UseVisualStyleBackColor = true;
            quitButton.Click += quitButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(quitButton);
            Controls.Add(avaImage);
            Controls.Add(loadingImage);
            Controls.Add(unitIdentifier);
            Controls.Add(currentApnAddress);
            Controls.Add(scanAvaTraceUnit);
            Controls.Add(statusText);
            Controls.Add(applyApnAddressButton);
            Controls.Add(startButton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(apnComboBox);
            Controls.Add(apnTextBox);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "AvaTraceAPN Editor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)loadingImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)avaImage).EndInit();
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
    }
}