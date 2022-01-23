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
        }


        private void EditCamFormLoad(object sender, EventArgs e)
        {
            LoadCamera();
        }

        void LoadCamera()
        {
            Cam.DownloadCamImage((Bitmap bm, int i) =>
            {
                Mono.LoadBackgroundImage(bm, Cam.Name, Cam.Template);
            });
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

            Cam.UpdateLastCaptureDate();

            Cam.Template = Mono.lsc;

            Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ReloadCameraButtonClick(object sender, EventArgs e)
        {
            LoadCamera();
        }


    }
}
