﻿using AForge.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotGrabber
{
    public partial class AddCameraForm : Form
    {

        CameraData Cam;


        public AddCameraForm(ref CameraData cam)
        {
            InitializeComponent();
            Cam = cam;
            Cam.Template = Mono.lsc;
        }

        private void MonoDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void MonoDragDrop(object sender, DragEventArgs e)
        {
            string[] imgs = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (imgs.Count() > 0)
            {
                Mono.LoadBackgroundImage(imgs[0]);
            }
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            Cam.Name = NameTextBox.Text;

            if (Enum.TryParse(ManufacturerComboBox.Text, out CameraManufacturer cm))
                Cam.Manufacturer = cm;

            Cam.Url = CameraURLTextBox.Text;
            Cam.PostalCode = PostalCodeTextBox.Text;

            if (Enum.TryParse(VideoQualityComboBox.Text, out CameraQuality cq))
                Cam.Quality = cq;

            if (int.TryParse(CameraAngleTextBox.Text, out int ang))
            {
                ang %= 360;
                if (ang < 0) ang += 360;

                Cam.Angle = ang;
            }

            if (Enum.TryParse(LotSizeComboBox.Text, out LotSize ls))
                Cam.LotSize = ls;

            //no need to update since we did not save capture
            //Cam.UpdateLastCaptureDate();

            Cam.Template = Mono.lsc;

            Close();

        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Cam.Name = "";
            Close();
        }

        

        private void LoadCameraButtonClick(object sender, EventArgs e)
        {
            if (CameraURLTextBox.Text != "" && ManufacturerComboBox.Text != "")
            {
                if (Enum.TryParse(ManufacturerComboBox.Text, out CameraManufacturer man))
                {
                    bool res = CameraData.DownloadCamImage(CameraURLTextBox.Text, man, (Bitmap bm, int i) =>
                    {
                        Mono.LoadBackgroundImage(bm, NameTextBox.Text);
                    });

                    if (!res)
                        MessageBox.Show("Failed to connect to url", "Error");
                }
            }
        }
    }
}
