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
            APNAddressInfoText = new Label();
            apnTextBox = new TextBox();
            apnComboBox = new ComboBox();
            addAddressManually = new Label();
            pickUpAddressFromList = new Label();
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
            engLanguageRadioButton = new RadioButton();
            finLanguageRadioButton = new RadioButton();
            confirmedImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)loadingImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)avaImage).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)confirmedImage).BeginInit();
            SuspendLayout();
            // 
            // APNAddressInfoText
            // 
            APNAddressInfoText.AutoSize = true;
            APNAddressInfoText.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            APNAddressInfoText.Location = new Point(3, 208);
            APNAddressInfoText.Name = "APNAddressInfoText";
            APNAddressInfoText.Size = new Size(0, 25);
            APNAddressInfoText.TabIndex = 0;
            APNAddressInfoText.Click += label1_Click;
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
            // addAddressManually
            // 
            addAddressManually.AutoSize = true;
            addAddressManually.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            addAddressManually.Location = new Point(94, 318);
            addAddressManually.Name = "addAddressManually";
            addAddressManually.Size = new Size(0, 25);
            addAddressManually.TabIndex = 5;
            // 
            // pickUpAddressFromList
            // 
            pickUpAddressFromList.AutoSize = true;
            pickUpAddressFromList.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            pickUpAddressFromList.Location = new Point(94, 267);
            pickUpAddressFromList.Name = "pickUpAddressFromList";
            pickUpAddressFromList.Size = new Size(0, 25);
            pickUpAddressFromList.TabIndex = 6;
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            startButton.Location = new Point(173, 12);
            startButton.Name = "startButton";
            startButton.Size = new Size(479, 53);
            startButton.TabIndex = 7;
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
            loadingImage.Location = new Point(341, 133);
            loadingImage.Name = "loadingImage";
            loadingImage.Size = new Size(127, 145);
            loadingImage.SizeMode = PictureBoxSizeMode.StretchImage;
            loadingImage.TabIndex = 14;
            loadingImage.TabStop = false;
            loadingImage.Click += loadingImage_Click;
            // 
            // avaImage
            // 
            avaImage.Location = new Point(246, 73);
            avaImage.Name = "avaImage";
            avaImage.Size = new Size(341, 363);
            avaImage.SizeMode = PictureBoxSizeMode.StretchImage;
            avaImage.TabIndex = 15;
            avaImage.TabStop = false;
            // 
            // quitButton
            // 
            quitButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            quitButton.Location = new Point(303, 352);
            quitButton.Name = "quitButton";
            quitButton.Size = new Size(201, 53);
            quitButton.TabIndex = 16;
            quitButton.UseVisualStyleBackColor = true;
            quitButton.Click += quitButton_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(engLanguageRadioButton);
            panel1.Controls.Add(finLanguageRadioButton);
            panel1.Controls.Add(confirmedImage);
            panel1.Controls.Add(APNAddressInfoText);
            panel1.Controls.Add(startButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 450);
            panel1.TabIndex = 17;
            // 
            // engLanguageRadioButton
            // 
            engLanguageRadioButton.AutoSize = true;
            engLanguageRadioButton.Location = new Point(16, 137);
            engLanguageRadioButton.Margin = new Padding(4, 5, 4, 5);
            engLanguageRadioButton.Name = "engLanguageRadioButton";
            engLanguageRadioButton.Size = new Size(93, 29);
            engLanguageRadioButton.TabIndex = 9;
            engLanguageRadioButton.TabStop = true;
            engLanguageRadioButton.Text = "English";
            engLanguageRadioButton.UseVisualStyleBackColor = true;
            engLanguageRadioButton.CheckedChanged += engLanguageRadioButton_CheckedChanged;
            // 
            // finLanguageRadioButton
            // 
            finLanguageRadioButton.AutoSize = true;
            finLanguageRadioButton.Location = new Point(16, 95);
            finLanguageRadioButton.Margin = new Padding(4, 5, 4, 5);
            finLanguageRadioButton.Name = "finLanguageRadioButton";
            finLanguageRadioButton.Size = new Size(92, 29);
            finLanguageRadioButton.TabIndex = 8;
            finLanguageRadioButton.TabStop = true;
            finLanguageRadioButton.Text = "Finnish";
            finLanguageRadioButton.UseVisualStyleBackColor = true;
            finLanguageRadioButton.CheckedChanged += finLanguageRadioButton_CheckedChanged;
            // 
            // confirmedImage
            // 
            confirmedImage.Location = new Point(351, 12);
            confirmedImage.Margin = new Padding(4, 5, 4, 5);
            confirmedImage.Name = "confirmedImage";
            confirmedImage.Size = new Size(31, 37);
            confirmedImage.SizeMode = PictureBoxSizeMode.StretchImage;
            confirmedImage.TabIndex = 1;
            confirmedImage.TabStop = false;
            confirmedImage.Click += pictureBox1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(quitButton);
            Controls.Add(unitIdentifier);
            Controls.Add(currentApnAddress);
            Controls.Add(statusText);
            Controls.Add(applyApnAddressButton);
            Controls.Add(pickUpAddressFromList);
            Controls.Add(addAddressManually);
            Controls.Add(apnComboBox);
            Controls.Add(apnTextBox);
            Controls.Add(scanAvaTraceUnit);
            Controls.Add(loadingImage);
            Controls.Add(avaImage);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "AvaTraceAPN Editor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)loadingImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)avaImage).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)confirmedImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label APNAddressInfoText;
        private TextBox apnTextBox;
        private ComboBox apnComboBox;
        private Label addAddressManually;
        private Label pickUpAddressFromList;
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
        private PictureBox confirmedImage;
        private RadioButton engLanguageRadioButton;
        private RadioButton finLanguageRadioButton;
    }
}