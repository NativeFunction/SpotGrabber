
namespace SpotGrabber
{
    partial class LotGrabberForm
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
            this.AddCameraButton = new System.Windows.Forms.Button();
            this.CamTable = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ManufacturerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostalCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QualityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AngleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCaptureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExportSpotsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CamTable)).BeginInit();
            this.SuspendLayout();
            // 
            // AddCameraButton
            // 
            this.AddCameraButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddCameraButton.Location = new System.Drawing.Point(12, 416);
            this.AddCameraButton.Name = "AddCameraButton";
            this.AddCameraButton.Size = new System.Drawing.Size(116, 38);
            this.AddCameraButton.TabIndex = 4;
            this.AddCameraButton.Text = "Add Camera";
            this.AddCameraButton.UseVisualStyleBackColor = true;
            this.AddCameraButton.Click += new System.EventHandler(this.AddCameraButtonClick);
            // 
            // CamTable
            // 
            this.CamTable.AllowUserToAddRows = false;
            this.CamTable.AllowUserToDeleteRows = false;
            this.CamTable.AllowUserToResizeRows = false;
            this.CamTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CamTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CamTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.ManufacturerColumn,
            this.PostalCodeColumn,
            this.QualityColumn,
            this.AngleColumn,
            this.LotSizeColumn,
            this.LastCaptureColumn});
            this.CamTable.GridColor = System.Drawing.SystemColors.Control;
            this.CamTable.Location = new System.Drawing.Point(12, 12);
            this.CamTable.Name = "CamTable";
            this.CamTable.ReadOnly = true;
            this.CamTable.RowHeadersVisible = false;
            this.CamTable.RowHeadersWidth = 51;
            this.CamTable.RowTemplate.Height = 24;
            this.CamTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CamTable.Size = new System.Drawing.Size(927, 374);
            this.CamTable.TabIndex = 5;
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 6;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Width = 132;
            // 
            // ManufacturerColumn
            // 
            this.ManufacturerColumn.HeaderText = "Manufacturer";
            this.ManufacturerColumn.MinimumWidth = 6;
            this.ManufacturerColumn.Name = "ManufacturerColumn";
            this.ManufacturerColumn.ReadOnly = true;
            this.ManufacturerColumn.Width = 132;
            // 
            // PostalCodeColumn
            // 
            this.PostalCodeColumn.HeaderText = "Postal Code";
            this.PostalCodeColumn.MinimumWidth = 6;
            this.PostalCodeColumn.Name = "PostalCodeColumn";
            this.PostalCodeColumn.ReadOnly = true;
            this.PostalCodeColumn.Width = 132;
            // 
            // QualityColumn
            // 
            this.QualityColumn.HeaderText = "Quality";
            this.QualityColumn.MinimumWidth = 6;
            this.QualityColumn.Name = "QualityColumn";
            this.QualityColumn.ReadOnly = true;
            this.QualityColumn.Width = 132;
            // 
            // AngleColumn
            // 
            this.AngleColumn.HeaderText = "Angle";
            this.AngleColumn.MinimumWidth = 6;
            this.AngleColumn.Name = "AngleColumn";
            this.AngleColumn.ReadOnly = true;
            this.AngleColumn.Width = 132;
            // 
            // LotSizeColumn
            // 
            this.LotSizeColumn.HeaderText = "Lot Size";
            this.LotSizeColumn.MinimumWidth = 6;
            this.LotSizeColumn.Name = "LotSizeColumn";
            this.LotSizeColumn.ReadOnly = true;
            this.LotSizeColumn.Width = 132;
            // 
            // LastCaptureColumn
            // 
            this.LastCaptureColumn.HeaderText = "Last Capture";
            this.LastCaptureColumn.MinimumWidth = 6;
            this.LastCaptureColumn.Name = "LastCaptureColumn";
            this.LastCaptureColumn.ReadOnly = true;
            this.LastCaptureColumn.Width = 132;
            // 
            // ExportSpotsButton
            // 
            this.ExportSpotsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportSpotsButton.Location = new System.Drawing.Point(823, 416);
            this.ExportSpotsButton.Name = "ExportSpotsButton";
            this.ExportSpotsButton.Size = new System.Drawing.Size(116, 38);
            this.ExportSpotsButton.TabIndex = 6;
            this.ExportSpotsButton.Text = "Export Spots";
            this.ExportSpotsButton.UseVisualStyleBackColor = true;
            this.ExportSpotsButton.Click += new System.EventHandler(this.ExportSpotsButtonClick);
            // 
            // LotGrabberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 573);
            this.Controls.Add(this.ExportSpotsButton);
            this.Controls.Add(this.CamTable);
            this.Controls.Add(this.AddCameraButton);
            this.MinimumSize = new System.Drawing.Size(480, 360);
            this.Name = "LotGrabberForm";
            this.Text = "LotGrabber";
            ((System.ComponentModel.ISupportInitialize)(this.CamTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddCameraButton;
        private System.Windows.Forms.DataGridView CamTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ManufacturerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostalCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QualityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AngleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotSizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCaptureColumn;
        private System.Windows.Forms.Button ExportSpotsButton;
    }
}