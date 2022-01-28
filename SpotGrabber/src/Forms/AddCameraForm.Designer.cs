
namespace SpotGrabber
{
    partial class AddCameraForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CameraURLTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ManufacturerComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PostalCodeTextBox = new System.Windows.Forms.TextBox();
            this.VideoQualityComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CameraAngleTextBox = new System.Windows.Forms.TextBox();
            this.LotSizeComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.Mono = new SpotGrabber.MonoDraw();
            this.LoadCameraButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(12, 668);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(173, 22);
            this.NameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 648);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 594);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Camera URL:";
            // 
            // CameraURLTextBox
            // 
            this.CameraURLTextBox.Location = new System.Drawing.Point(12, 614);
            this.CameraURLTextBox.Name = "CameraURLTextBox";
            this.CameraURLTextBox.Size = new System.Drawing.Size(458, 22);
            this.CameraURLTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(568, 596);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Manufacturer:";
            // 
            // ManufacturerComboBox
            // 
            this.ManufacturerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ManufacturerComboBox.FormattingEnabled = true;
            this.ManufacturerComboBox.Items.AddRange(new object[] {
            "Axis",
            "ChannelVision",
            "Hi3516",
            "Megapixel",
            "AxisMkII",
            "PanasonicHD"});
            this.ManufacturerComboBox.Location = new System.Drawing.Point(571, 614);
            this.ManufacturerComboBox.Name = "ManufacturerComboBox";
            this.ManufacturerComboBox.Size = new System.Drawing.Size(173, 24);
            this.ManufacturerComboBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(294, 648);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Postal Code:";
            // 
            // PostalCodeTextBox
            // 
            this.PostalCodeTextBox.Location = new System.Drawing.Point(297, 668);
            this.PostalCodeTextBox.Name = "PostalCodeTextBox";
            this.PostalCodeTextBox.Size = new System.Drawing.Size(173, 22);
            this.PostalCodeTextBox.TabIndex = 7;
            // 
            // VideoQualityComboBox
            // 
            this.VideoQualityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoQualityComboBox.FormattingEnabled = true;
            this.VideoQualityComboBox.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
            this.VideoQualityComboBox.Location = new System.Drawing.Point(794, 614);
            this.VideoQualityComboBox.Name = "VideoQualityComboBox";
            this.VideoQualityComboBox.Size = new System.Drawing.Size(173, 24);
            this.VideoQualityComboBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(791, 594);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Video Quality:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(568, 650);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Camera Angle:";
            // 
            // CameraAngleTextBox
            // 
            this.CameraAngleTextBox.Location = new System.Drawing.Point(571, 670);
            this.CameraAngleTextBox.Name = "CameraAngleTextBox";
            this.CameraAngleTextBox.Size = new System.Drawing.Size(173, 22);
            this.CameraAngleTextBox.TabIndex = 11;
            // 
            // LotSizeComboBox
            // 
            this.LotSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LotSizeComboBox.FormattingEnabled = true;
            this.LotSizeComboBox.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Large"});
            this.LotSizeComboBox.Location = new System.Drawing.Point(794, 668);
            this.LotSizeComboBox.Name = "LotSizeComboBox";
            this.LotSizeComboBox.Size = new System.Drawing.Size(173, 24);
            this.LotSizeComboBox.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(791, 648);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Lot Size:";
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.Location = new System.Drawing.Point(732, 705);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(102, 31);
            this.AddButton.TabIndex = 15;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButtonClick);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(863, 705);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(102, 31);
            this.CancelButton.TabIndex = 16;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // Mono
            // 
            this.Mono.AllowDrop = true;
            this.Mono.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Mono.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Mono.Location = new System.Drawing.Point(12, 12);
            this.Mono.MouseHoverUpdatesOnly = false;
            this.Mono.Name = "Mono";
            this.Mono.Size = new System.Drawing.Size(960, 540);
            this.Mono.TabIndex = 17;
            this.Mono.Text = "Mono";
            this.Mono.DragDrop += new System.Windows.Forms.DragEventHandler(this.MonoDragDrop);
            this.Mono.DragEnter += new System.Windows.Forms.DragEventHandler(this.MonoDragEnter);
            // 
            // LoadCameraButton
            // 
            this.LoadCameraButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadCameraButton.Location = new System.Drawing.Point(12, 705);
            this.LoadCameraButton.Name = "LoadCameraButton";
            this.LoadCameraButton.Size = new System.Drawing.Size(102, 31);
            this.LoadCameraButton.TabIndex = 18;
            this.LoadCameraButton.Text = "Load Camera";
            this.LoadCameraButton.UseVisualStyleBackColor = true;
            this.LoadCameraButton.Click += new System.EventHandler(this.LoadCameraButtonClick);
            // 
            // AddCameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 748);
            this.Controls.Add(this.LoadCameraButton);
            this.Controls.Add(this.Mono);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.LotSizeComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CameraAngleTextBox);
            this.Controls.Add(this.VideoQualityComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PostalCodeTextBox);
            this.Controls.Add(this.ManufacturerComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CameraURLTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameTextBox);
            this.Name = "AddCameraForm";
            this.Text = "Add Camera";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CameraURLTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ManufacturerComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PostalCodeTextBox;
        private System.Windows.Forms.ComboBox VideoQualityComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox CameraAngleTextBox;
        private System.Windows.Forms.ComboBox LotSizeComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button CancelButton;
        private MonoDraw Mono;
        private System.Windows.Forms.Button LoadCameraButton;
    }
}