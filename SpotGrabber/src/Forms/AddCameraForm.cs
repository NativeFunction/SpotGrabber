using AForge.Video;
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

            if (NameTextBox.Text != "")
                Cam.Name = NameTextBox.Text;
            else
            {
                MessageBox.Show("Camera name is not set", "Error");
                return;
            }



            if (ManufacturerComboBox.Text != "" && Enum.TryParse(ManufacturerComboBox.Text, out CameraManufacturer cm))
                Cam.Manufacturer = cm;
            else
            {
                MessageBox.Show("Camera manufacturer is not set", "Error");
                return;
            }


            if (CameraURLTextBox.Text != "")
                Cam.Url = CameraURLTextBox.Text;
            else
            {
                MessageBox.Show("Camera url is not set", "Error");
                return;
            }



            if (VideoQualityComboBox.Text != "" && Enum.TryParse(VideoQualityComboBox.Text, out CameraQuality cq))
                Cam.Quality = cq;
            else
            {
                MessageBox.Show("Camera quality is not set", "Error");
                return;
            }

            if (CameraAngleTextBox.Text != "" && int.TryParse(CameraAngleTextBox.Text, out int ang))
            {
                ang %= 360;
                if (ang < 0) ang += 360;

                Cam.Angle = ang;
            }
            else
            {
                MessageBox.Show("Camera angle is not set", "Error");
                return;
            }

            if (LotSizeComboBox.Text != "" && Enum.TryParse(LotSizeComboBox.Text, out LotSize ls))
                Cam.LotSize = ls;
            else
            {
                MessageBox.Show("Camera lot size is not set", "Error");
                return;
            }

            if(Mono.IsBackgroundLoaded() && Mono.lsc.Rects.Count != 0)
                Cam.Template = Mono.lsc;
            else
            {
                MessageBox.Show("Camera template is not set", "Error");
                return;
            }

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
