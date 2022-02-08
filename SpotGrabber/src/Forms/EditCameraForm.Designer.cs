
namespace SpotGrabber.src.Forms
{
    partial class EditCameraForm
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
            this.ReloadCameraButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.LotSizeComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CameraAngleTextBox = new System.Windows.Forms.TextBox();
            this.VideoQualityComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.Mono = new SpotGrabber.MonoDraw();
            this.SuspendLayout();
            // 
            // ReloadCameraButton
            // 
            this.ReloadCameraButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ReloadCameraButton.Location = new System.Drawing.Point(12, 641);
            this.ReloadCameraButton.Name = "ReloadCameraButton";
            this.ReloadCameraButton.Size = new System.Drawing.Size(118, 31);
            this.ReloadCameraButton.TabIndex = 35;
            this.ReloadCameraButton.Text = "Reload Camera";
            this.ReloadCameraButton.UseVisualStyleBackColor = true;
            this.ReloadCameraButton.Click += new System.EventHandler(this.ReloadCameraButtonClick);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(870, 641);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(102, 31);
            this.CancelButton.TabIndex = 34;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateButton.Location = new System.Drawing.Point(737, 641);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(102, 31);
            this.UpdateButton.TabIndex = 33;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButtonClick);
            // 
            // LotSizeComboBox
            // 
            this.LotSizeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LotSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LotSizeComboBox.FormattingEnabled = true;
            this.LotSizeComboBox.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Large"});
            this.LotSizeComboBox.Location = new System.Drawing.Point(411, 596);
            this.LotSizeComboBox.Name = "LotSizeComboBox";
            this.LotSizeComboBox.Size = new System.Drawing.Size(173, 24);
            this.LotSizeComboBox.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(408, 576);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 31;
            this.label7.Text = "Lot Size:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 576);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 17);
            this.label6.TabIndex = 30;
            this.label6.Text = "Camera Angle:";
            // 
            // CameraAngleTextBox
            // 
            this.CameraAngleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CameraAngleTextBox.Location = new System.Drawing.Point(217, 596);
            this.CameraAngleTextBox.Name = "CameraAngleTextBox";
            this.CameraAngleTextBox.Size = new System.Drawing.Size(173, 22);
            this.CameraAngleTextBox.TabIndex = 29;
            // 
            // VideoQualityComboBox
            // 
            this.VideoQualityComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.VideoQualityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoQualityComboBox.FormattingEnabled = true;
            this.VideoQualityComboBox.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
            this.VideoQualityComboBox.Location = new System.Drawing.Point(799, 596);
            this.VideoQualityComboBox.Name = "VideoQualityComboBox";
            this.VideoQualityComboBox.Size = new System.Drawing.Size(173, 24);
            this.VideoQualityComboBox.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(796, 576);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 27;
            this.label5.Text = "Video Quality:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 576);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Location = new System.Drawing.Point(15, 596);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(173, 22);
            this.NameTextBox.TabIndex = 19;
            // 
            // Mono
            // 
            this.Mono.AllowDrop = true;
            this.Mono.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mono.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Mono.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Mono.Location = new System.Drawing.Point(12, 12);
            this.Mono.MouseHoverUpdatesOnly = false;
            this.Mono.Name = "Mono";
            this.Mono.Size = new System.Drawing.Size(960, 540);
            this.Mono.TabIndex = 18;
            this.Mono.Text = "Mono";
            // 
            // EditCameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 702);
            this.Controls.Add(this.ReloadCameraButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.LotSizeComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CameraAngleTextBox);
            this.Controls.Add(this.VideoQualityComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.Mono);
            this.MinimumSize = new System.Drawing.Size(1002, 749);
            this.Name = "EditCameraForm";
            this.Text = "Edit Cam";
            this.Load += new System.EventHandler(this.EditCamFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MonoDraw Mono;
        private System.Windows.Forms.Button ReloadCameraButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.ComboBox LotSizeComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox CameraAngleTextBox;
        private System.Windows.Forms.ComboBox VideoQualityComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameTextBox;
    }
}