using Innovative.SolarCalculator;
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
        bool exportSpotsLoop = false;
        Timer ExportSpotsLoopTimer;
        EventHandler ExportSpotsLoopEvent;


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
                    CamTable.Rows.Add(cam.Name, cam.Latitude, cam.Longitude, Enum.GetName(typeof(CameraManufacturer), cam.Manufacturer), Enum.GetName(typeof(CameraQuality), cam.Quality), $"≈ {cam.Angle}°", Enum.GetName(typeof(LotSize), cam.LotSize), cam.LastCaptureDate, cam.Template.Rects.Count.ToString());

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

            //RefreshCamTable();

            CameraData cam = new CameraData();
            AddCameraForm acf = new AddCameraForm(ref cam);
            acf.ShowDialog();

            if (cam.IsValid())
            {

                cam.CreateXML(camDoc, ccNode);


                camDoc.Save("Data/Cams.xml");
                cams.Add(cam);


                CamTable.Rows.Add(cam.Name, cam.Latitude, cam.Longitude, Enum.GetName(typeof(CameraManufacturer), cam.Manufacturer), Enum.GetName(typeof(CameraQuality), cam.Quality), $"≈ {cam.Angle}°", Enum.GetName(typeof(LotSize), cam.LotSize), cam.LastCaptureDate, cam.Template.Rects.Count.ToString());

            }


        }

        private void ExportSpots(bool onlyGrabDaytime = false)
        {
            Directory.CreateDirectory("Data/Spots");
            Directory.CreateDirectory("Data/Spots/Empty");//for later manual sort
            Directory.CreateDirectory("Data/Spots/Filled");

            XmlNodeList camNodes = ccNode.SelectNodes("/CamCollection/Item");
            
            for(int i = 0; i < cams.Count(); i++)
            {
                var cam = cams[i];
                
                if(onlyGrabDaytime)
                {
                    DateTime utc = DateTime.UtcNow;
                    SolarTimes solarTimes = new SolarTimes(DateTime.Now, new Innovative.Geometry.Angle(cam.Latitude), new Innovative.Geometry.Angle(cam.Longitude));
                    DateTime sunrise = solarTimes.Sunrise.ToUniversalTime();
                    DateTime sunset = solarTimes.Sunset.ToUniversalTime();


                    if (utc < sunrise || utc > sunset)
                    {
                        if (i == cams.Count - 1)
                        {
                            camDoc.Save("Data/Cams.xml");
                            RefreshCamTable();
                        }
                        continue;
                    }
                }


                cam.DownloadCamImage((System.Drawing.Bitmap bm, int index) =>
                {
                    cam.UpdateLastCaptureDate();
                    cam.UpdateXMLLastCaptureDate(camNodes[index]);
                    cam.Template.ExportImages($"Data/Spots/{cam.Name}_{cam.LastCaptureDate}", bm);

                    if (index == cams.Count - 1)
                    {
                        camDoc.Save("Data/Cams.xml");
                        RefreshCamTable();
                    }

                }, i);

            }
        }

        private void ExportSpotsButtonClick(object sender, EventArgs e)
        {
            ExportSpots(DaytimeExportCheckBox.Checked);
        }

        private void ExportSpotsLoopButton_Click(object sender, EventArgs e)
        {

            this.AddCameraButton.Enabled = exportSpotsLoop;
            this.CamTable.Enabled = exportSpotsLoop;

            exportSpotsLoop = !exportSpotsLoop;

            if (exportSpotsLoop)
            {
                ExportSpotsLoopTimer = new Timer();


                ExportSpotsLoopTimer.Tick += ExportSpotsLoopEvent = new EventHandler(ExportSpotsButtonClick);

                ExportSpotsLoopTimer.Interval = 60 * 60 * 1000;//60 min
                ExportSpotsLoopTimer.Enabled = true;
                ExportSpotsLoopTimer.Start();

                ExportSpotsLoopButton.Text = "Export Spots Every Hour: On";

            }
            else
            {
                ExportSpotsLoopTimer.Enabled = false;
                ExportSpotsLoopTimer.Stop();
                ExportSpotsLoopTimer.Tick -= ExportSpotsLoopEvent;
                ExportSpotsLoopEvent = null;

                ExportSpotsLoopButton.Text = "Export Spots Every Hour: Off";
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

            var rects = cam.Template.CloneRects();

            if (acf.ShowDialog() == DialogResult.OK && cam.IsValid())
            {
                cams[contextMenuIndex] = cam;
                XmlNode camNode = ccNode.SelectNodes("/CamCollection/Item")[contextMenuIndex];

                cam.UpdateXML(camDoc, camNode);

                camDoc.Save("Data/Cams.xml");
                RefreshCamTable();
            }
            else
            {
                //cancel undo delete rect
                cams[contextMenuIndex].Template.Rects = rects;
            }
        }

        private void LotGrabberForm_Click(object sender, EventArgs e)
        {
            CamTable.ClearSelection();
        }

        void RefreshCamTable()
        {
            for (int i = 0; i < cams.Count; i++)
            {

                CamTable.Rows[i].Cells[0].Value = cams[i].Name;
                CamTable.Rows[i].Cells[1].Value = cams[i].Latitude;
                CamTable.Rows[i].Cells[2].Value = cams[i].Longitude;
                CamTable.Rows[i].Cells[3].Value = Enum.GetName(typeof(CameraManufacturer), cams[i].Manufacturer);
                CamTable.Rows[i].Cells[4].Value = Enum.GetName(typeof(CameraQuality), cams[i].Quality);
                CamTable.Rows[i].Cells[5].Value = $"≈ {cams[i].Angle}°";
                CamTable.Rows[i].Cells[6].Value = Enum.GetName(typeof(LotSize), cams[i].LotSize);
                CamTable.Rows[i].Cells[7].Value = cams[i].LastCaptureDate;
                CamTable.Rows[i].Cells[8].Value = cams[i].Template.Rects.Count.ToString();

            }


        }

    }
}
