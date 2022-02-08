using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotGrabber.src.Forms
{
    public partial class EditCameraForm : Form
    {
        CameraData Cam;

        public EditCameraForm(CameraData cam)
        {
            InitializeComponent();
            Cam = cam;

            CameraAngleTextBox.Text = Cam.Angle.ToString();
            NameTextBox.Text = Cam.Name;
            PostalCodeTextBox.Text = Cam.PostalCode;

            LotSizeComboBox.SelectedIndex = (int)Cam.LotSize;
            VideoQualityComboBox.SelectedIndex = (int)Cam.Quality;

            Opacity = 0;
        }


        private void EditCamFormLoad(object sender, EventArgs e)
        {
            if (!LoadCamera())
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
            else
                Opacity = 100;
        }

        bool LoadCamera()
        {
            bool res = Cam.DownloadCamImage((Bitmap bm, int i) =>
            {
                Mono.LoadBackgroundImage(bm, Cam.Name, Cam.Template);
            });

            if (!res)
                MessageBox.Show("Could not connect to camera", "Error");
            return res;
        }

        private void UpdateButtonClick(object sender, EventArgs e)
        {

            Cam.Name = NameTextBox.Text;
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

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ReloadCameraButtonClick(object sender, EventArgs e)
        {
            LoadCamera();
        }


    }
}
