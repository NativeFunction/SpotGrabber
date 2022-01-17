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
    public enum CameraManufacturer
    {
        Axis,
        Hi3516,
        Megapixel,
        AxisMkII,
        PanasonicHD,
        None
    }

    public enum CameraQuality
    {
        Low,
        Medium,
        High,
        None
    }

    public enum LotSize
    {
        Small,
        Medium,
        Large,
        None
    }

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

            var date = DateTime.Now;
            Cam.LastCaptureDate = $"{date:dd}{date:MM}{date:yy}-{date:HH}{date:mm}";


            Cam.Template = Mono.lsc;

            Close();

        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Cam.Name = "";
            Close();
        }

        public bool CheckWebsite(string URL)
        {
            
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.GetResponse();
                return true;
            }
            catch //If exception thrown then couldn't get response from address
            {
                return false;
            }

        }

        void GetAxisSnapshot(object sender, NewFrameEventArgs e)
        {
            Mono.LoadBackgroundImage(e.Frame, "");

            //e.Frame.Save(@"C:\Users\Rocko\Desktop\test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            ((MJPEGStream)sender).Stop();
            ((MJPEGStream)sender).NewFrame -= GetAxisSnapshot;
        }

        private void LoadCameraButtonClick(object sender, EventArgs e)
        {
            if (CameraURLTextBox.Text != "" && ManufacturerComboBox.Text != "")
            {


                if (CheckWebsite(CameraURLTextBox.Text))
                {
                    MJPEGStream stream = new MJPEGStream(CameraURLTextBox.Text);
                    stream.NewFrame += GetAxisSnapshot;
                    stream.Start();
                    
                }
            }
        }
    }
}
