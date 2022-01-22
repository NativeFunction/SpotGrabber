using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;
using SharpDX.WIC;
using SpotGrabber.src.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SpotGrabber
{
    public partial class LotGrabberForm : Form
    {



        XmlDocument camDoc = new XmlDocument();
        XmlElement ccNode;
        List<CameraData> cams = new List<CameraData>();
        int contextMenuIndex = 0;



        public LotGrabberForm()
        {
            InitializeComponent();


            if (!File.Exists("Data/Cams.xml"))
            {

                camDoc.AppendChild(camDoc.CreateXmlDeclaration("1.0", "UTF-8", ""));
                ccNode = (XmlElement)camDoc.CreateNode(XmlNodeType.Element, "CamCollection", "");

                camDoc.AppendChild(ccNode);

                Directory.CreateDirectory("Data");
                camDoc.Save("Data/Cams.xml");
            }
            else
            {
                camDoc.Load("Data/Cams.xml");
                ccNode = (XmlElement)camDoc.DocumentElement.SelectSingleNode("/CamCollection");

                XmlNodeList camNodes = ccNode.SelectNodes("/CamCollection/Item");

                foreach (XmlNode node in camNodes)
                {
                    CameraData cam = new CameraData(node);
                    cams.Add(cam);
                    CamTable.Rows.Add(cam.Name, Enum.GetName(typeof(CameraManufacturer), cam.Manufacturer), cam.PostalCode, Enum.GetName(typeof(CameraQuality), cam.Quality), $"≈ {cam.Angle}°", Enum.GetName(typeof(LotSize), cam.LotSize), cam.LastCaptureDate);

                }
            }


        }

        private void LotGrabberFormLoad(object sender, EventArgs e)
        {
            CamTable.ClearSelection();
        }

        private void AddCameraButtonClick(object sender, EventArgs e)
        {
            //CamTable.Rows.Add("Hi3516-302000", "Hi3516", "302000", "Good", "≈ 90°", "Large", "011022-0415");
            //CamTable.Rows.Insert(0, "one", "two", "three", "four");

            CameraData cam = new CameraData();
            AddCameraForm acf = new AddCameraForm(ref cam);
            acf.ShowDialog();

            if (cam.IsValid())
            {

                cam.CreateXML(camDoc, ccNode);


                camDoc.Save("Data/Cams.xml");
                cams.Add(cam);


                CamTable.Rows.Add(cam.Name, Enum.GetName(typeof(CameraManufacturer), cam.Manufacturer), cam.PostalCode, Enum.GetName(typeof(CameraQuality), cam.Quality), $"≈ {cam.Angle}°", Enum.GetName(typeof(LotSize), cam.LotSize), cam.LastCaptureDate);

            }


        }

        private void ExportSpotsButtonClick(object sender, EventArgs e)
        {
            Directory.CreateDirectory("Data/Spots");
            Directory.CreateDirectory("Data/Spots/Empty");//for later manual sort
            Directory.CreateDirectory("Data/Spots/Filled");

            foreach (var cam in cams)
            {
                cam.DownloadCamImage((System.Drawing.Bitmap bm) =>
                {
                    cam.Template.ExportImages($"Data/Spots/{cam.Name}{cam.LastCaptureDate}", bm);
                });
            }
        }


        private void CamTableCellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }


                var relativeMousePosition = CamTable.PointToClient(Cursor.Position);

                contextMenuIndex = e.RowIndex;
                CamTableContextMenu.Show(CamTable, relativeMousePosition);
            }
        }

        private void EditCamContextMenuClick(object sender, EventArgs e)
        {
            CameraData cam = new CameraData(cams[contextMenuIndex]);

            EditCameraForm acf = new EditCameraForm(cam);
            acf.ShowDialog();
            
            if (cam.IsValid())
            {
                cams[contextMenuIndex] = cam;
                XmlNode camNode = ccNode.SelectNodes("/CamCollection/Item")[contextMenuIndex];
                
                cam.UpdateXML(camDoc, camNode);
                
                camDoc.Save("Data/Cams.xml");
            }
        }

        private void LotGrabberForm_Click(object sender, EventArgs e)
        {
            CamTable.ClearSelection();
        }
    }
}
