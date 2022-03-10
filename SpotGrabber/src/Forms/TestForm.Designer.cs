
namespace SpotGrabber
{
    partial class TestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.ClearRectsButton = new System.Windows.Forms.Button();
            this.SaveTemplateButton = new System.Windows.Forms.Button();
            this.LoadTemplateButton = new System.Windows.Forms.Button();
            this.Mono = new SpotGrabber.MonoDraw();
            this.ExportImagesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ClearRectsButton
            // 
            this.ClearRectsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearRectsButton.Location = new System.Drawing.Point(12, 462);
            this.ClearRectsButton.Name = "ClearRectsButton";
            this.ClearRectsButton.Size = new System.Drawing.Size(116, 38);
            this.ClearRectsButton.TabIndex = 1;
            this.ClearRectsButton.Text = "Clear Template";
            this.ClearRectsButton.UseVisualStyleBackColor = true;
            this.ClearRectsButton.Click += new System.EventHandler(this.ClearRectsButtonClick);
            // 
            // SaveTemplateButton
            // 
            this.SaveTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveTemplateButton.Location = new System.Drawing.Point(701, 462);
            this.SaveTemplateButton.Name = "SaveTemplateButton";
            this.SaveTemplateButton.Size = new System.Drawing.Size(116, 38);
            this.SaveTemplateButton.TabIndex = 3;
            this.SaveTemplateButton.Text = "Save Template";
            this.SaveTemplateButton.UseVisualStyleBackColor = true;
            this.SaveTemplateButton.Click += new System.EventHandler(this.SaveTemplateButtonClick);
            // 
            // LoadTemplateButton
            // 
            this.LoadTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadTemplateButton.Location = new System.Drawing.Point(823, 462);
            this.LoadTemplateButton.Name = "LoadTemplateButton";
            this.LoadTemplateButton.Size = new System.Drawing.Size(116, 38);
            this.LoadTemplateButton.TabIndex = 4;
            this.LoadTemplateButton.Text = "Load Template";
            this.LoadTemplateButton.UseVisualStyleBackColor = true;
            this.LoadTemplateButton.Click += new System.EventHandler(this.LoadTemplateButtonClick);
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
            this.Mono.Size = new System.Drawing.Size(927, 435);
            this.Mono.TabIndex = 2;
            this.Mono.Text = "Mono";
            this.Mono.DragDrop += new System.Windows.Forms.DragEventHandler(this.MonoDragDrop);
            this.Mono.DragEnter += new System.Windows.Forms.DragEventHandler(this.MonoDragEnter);
            // 
            // ExportImagesButton
            // 
            this.ExportImagesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportImagesButton.Location = new System.Drawing.Point(701, 514);
            this.ExportImagesButton.Name = "ExportImagesButton";
            this.ExportImagesButton.Size = new System.Drawing.Size(116, 47);
            this.ExportImagesButton.TabIndex = 5;
            this.ExportImagesButton.Text = "Export Images";
            this.ExportImagesButton.UseVisualStyleBackColor = true;
            this.ExportImagesButton.Click += new System.EventHandler(this.ExportImagesButtonClick);
            // 
            // TestForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 573);
            this.Controls.Add(this.ExportImagesButton);
            this.Controls.Add(this.LoadTemplateButton);
            this.Controls.Add(this.SaveTemplateButton);
            this.Controls.Add(this.Mono);
            this.Controls.Add(this.ClearRectsButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(480, 360);
            this.Name = "TestForm";
            this.Text = "SpotGrabber";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ClearRectsButton;
        private MonoDraw Mono;
        private System.Windows.Forms.Button SaveTemplateButton;
        private System.Windows.Forms.Button LoadTemplateButton;
        private System.Windows.Forms.Button ExportImagesButton;
    }
}

