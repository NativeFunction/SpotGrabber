
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
            this.components = new System.ComponentModel.Container();
            this.AddCameraButton = new System.Windows.Forms.Button();
            this.CamTable = new System.Windows.Forms.DataGridView();
            this.ExportSpotsButton = new System.Windows.Forms.Button();
            this.CamTableContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditCamContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportSpotsLoopButton = new System.Windows.Forms.Button();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ManufacturerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QualityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AngleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCaptureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpotCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DaytimeExportCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.CamTable)).BeginInit();
            this.CamTableContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddCameraButton
            // 
            this.AddCameraButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddCameraButton.Location = new System.Drawing.Point(12, 412);
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
            this.CamTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CamTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CamTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CamTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.Latitude,
            this.Longitute,
            this.ManufacturerColumn,
            this.QualityColumn,
            this.AngleColumn,
            this.LotSizeColumn,
            this.LastCaptureColumn,
            this.SpotCount});
            this.CamTable.GridColor = System.Drawing.SystemColors.Control;
            this.CamTable.Location = new System.Drawing.Point(12, 12);
            this.CamTable.MultiSelect = false;
            this.CamTable.Name = "CamTable";
            this.CamTable.ReadOnly = true;
            this.CamTable.RowHeadersVisible = false;
            this.CamTable.RowHeadersWidth = 51;
            this.CamTable.RowTemplate.Height = 24;
            this.CamTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CamTable.Size = new System.Drawing.Size(927, 394);
            this.CamTable.TabIndex = 5;
            this.CamTable.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CamTableCellMouseUp);
            // 
            // ExportSpotsButton
            // 
            this.ExportSpotsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportSpotsButton.Location = new System.Drawing.Point(596, 412);
            this.ExportSpotsButton.Name = "ExportSpotsButton";
            this.ExportSpotsButton.Size = new System.Drawing.Size(116, 38);
            this.ExportSpotsButton.TabIndex = 6;
            this.ExportSpotsButton.Text = "Export Spots";
            this.ExportSpotsButton.UseVisualStyleBackColor = true;
            this.ExportSpotsButton.Click += new System.EventHandler(this.ExportSpotsButtonClick);
            // 
            // CamTableContextMenu
            // 
            this.CamTableContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CamTableContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditCamContextMenu});
            this.CamTableContextMenu.Name = "CamTableContextMenu";
            this.CamTableContextMenu.Size = new System.Drawing.Size(139, 28);
            // 
            // EditCamContextMenu
            // 
            this.EditCamContextMenu.Name = "EditCamContextMenu";
            this.EditCamContextMenu.Size = new System.Drawing.Size(138, 24);
            this.EditCamContextMenu.Text = "Edit Cam";
            this.EditCamContextMenu.Click += new System.EventHandler(this.EditCamContextMenuClick);
            // 
            // ExportSpotsLoopButton
            // 
            this.ExportSpotsLoopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportSpotsLoopButton.Location = new System.Drawing.Point(738, 412);
            this.ExportSpotsLoopButton.Name = "ExportSpotsLoopButton";
            this.ExportSpotsLoopButton.Size = new System.Drawing.Size(201, 38);
            this.ExportSpotsLoopButton.TabIndex = 7;
            this.ExportSpotsLoopButton.Text = "Export Spots Every Hour: Off ";
            this.ExportSpotsLoopButton.UseVisualStyleBackColor = true;
            this.ExportSpotsLoopButton.Click += new System.EventHandler(this.ExportSpotsLoopButton_Click);
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 6;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NameColumn.Width = 132;
            // 
            // Latitude
            // 
            this.Latitude.HeaderText = "Latitude";
            this.Latitude.MinimumWidth = 6;
            this.Latitude.Name = "Latitude";
            this.Latitude.ReadOnly = true;
            this.Latitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Latitude.Width = 125;
            // 
            // Longitute
            // 
            this.Longitute.HeaderText = "Longitute";
            this.Longitute.MinimumWidth = 6;
            this.Longitute.Name = "Longitute";
            this.Longitute.ReadOnly = true;
            this.Longitute.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Longitute.Width = 125;
            // 
            // ManufacturerColumn
            // 
            this.ManufacturerColumn.HeaderText = "Manufacturer";
            this.ManufacturerColumn.MinimumWidth = 6;
            this.ManufacturerColumn.Name = "ManufacturerColumn";
            this.ManufacturerColumn.ReadOnly = true;
            this.ManufacturerColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ManufacturerColumn.Width = 132;
            // 
            // QualityColumn
            // 
            this.QualityColumn.HeaderText = "Quality";
            this.QualityColumn.MinimumWidth = 6;
            this.QualityColumn.Name = "QualityColumn";
            this.QualityColumn.ReadOnly = true;
            this.QualityColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QualityColumn.Width = 132;
            // 
            // AngleColumn
            // 
            this.AngleColumn.HeaderText = "Angle";
            this.AngleColumn.MinimumWidth = 6;
            this.AngleColumn.Name = "AngleColumn";
            this.AngleColumn.ReadOnly = true;
            this.AngleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AngleColumn.Width = 132;
            // 
            // LotSizeColumn
            // 
            this.LotSizeColumn.HeaderText = "Lot Size";
            this.LotSizeColumn.MinimumWidth = 6;
            this.LotSizeColumn.Name = "LotSizeColumn";
            this.LotSizeColumn.ReadOnly = true;
            this.LotSizeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LotSizeColumn.Width = 132;
            // 
            // LastCaptureColumn
            // 
            this.LastCaptureColumn.HeaderText = "Last Capture";
            this.LastCaptureColumn.MinimumWidth = 6;
            this.LastCaptureColumn.Name = "LastCaptureColumn";
            this.LastCaptureColumn.ReadOnly = true;
            this.LastCaptureColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LastCaptureColumn.Width = 132;
            // 
            // SpotCount
            // 
            this.SpotCount.HeaderText = "Spot Count";
            this.SpotCount.MinimumWidth = 6;
            this.SpotCount.Name = "SpotCount";
            this.SpotCount.ReadOnly = true;
            this.SpotCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SpotCount.Width = 125;
            // 
            // DaytimeExportCheckBox
            // 
            this.DaytimeExportCheckBox.AutoSize = true;
            this.DaytimeExportCheckBox.Location = new System.Drawing.Point(738, 462);
            this.DaytimeExportCheckBox.Name = "DaytimeExportCheckBox";
            this.DaytimeExportCheckBox.Size = new System.Drawing.Size(204, 21);
            this.DaytimeExportCheckBox.TabIndex = 8;
            this.DaytimeExportCheckBox.Text = "Only export daytime images";
            this.DaytimeExportCheckBox.UseVisualStyleBackColor = true;
            // 
            // LotGrabberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 495);
            this.Controls.Add(this.DaytimeExportCheckBox);
            this.Controls.Add(this.ExportSpotsLoopButton);
            this.Controls.Add(this.ExportSpotsButton);
            this.Controls.Add(this.CamTable);
            this.Controls.Add(this.AddCameraButton);
            this.MinimumSize = new System.Drawing.Size(480, 360);
            this.Name = "LotGrabberForm";
            this.Text = "LotGrabber";
            this.Load += new System.EventHandler(this.LotGrabberFormLoad);
            this.Click += new System.EventHandler(this.LotGrabberForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.CamTable)).EndInit();
            this.CamTableContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddCameraButton;
        private System.Windows.Forms.DataGridView CamTable;
        private System.Windows.Forms.Button ExportSpotsButton;
        private System.Windows.Forms.ContextMenuStrip CamTableContextMenu;
        private System.Windows.Forms.ToolStripMenuItem EditCamContextMenu;
        private System.Windows.Forms.Button ExportSpotsLoopButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitute;
        private System.Windows.Forms.DataGridViewTextBoxColumn ManufacturerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QualityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AngleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotSizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCaptureColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpotCount;
        private System.Windows.Forms.CheckBox DaytimeExportCheckBox;
    }
}