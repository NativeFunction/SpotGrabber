using AForge.Video;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace SpotGrabber
{
    public class CameraData
    {
        public string Name = "";
        public CameraManufacturer Manufacturer = CameraManufacturer.None;
        public string Url = "";
        public CameraQuality Quality = CameraQuality.None;
        public int Angle = 0;
        public LotSize LotSize = LotSize.None;
        public string LastCaptureDate = "000000-0000";
        public LotSlotCollection Template;

        public CameraData() { }

        public CameraData(CameraData cam) 
        {
            Name = cam.Name;
            Manufacturer = cam.Manufacturer;
            Url = cam.Url;
            Quality = cam.Quality;
            Angle = cam.Angle;
            LotSize = cam.LotSize;
            LastCaptureDate = cam.LastCaptureDate;
            Template = new LotSlotCollection(cam.Template.LotName, cam.Template.Rects, cam.Template.CurrentImageBounds.Width, cam.Template.CurrentImageBounds.Height);

        }

        public CameraData(XmlNode camNode)
        {
            XmlNode temp;
            int tempInt = 0;

            temp = camNode.SelectSingleNode("Name");
            Name = temp?.InnerText;
            temp = camNode.SelectSingleNode("Manufacturer");
            Enum.TryParse(temp?.InnerText, out Manufacturer);
            temp = camNode.SelectSingleNode("Url");
            Url = temp?.Attributes.GetNamedItem("value")?.InnerText;
            temp = camNode.SelectSingleNode("Quality");
            if (int.TryParse(temp?.Attributes.GetNamedItem("value")?.InnerText, out tempInt))
                Quality = (CameraQuality)tempInt;
            temp = camNode.SelectSingleNode("Angle");
            int.TryParse(temp?.Attributes.GetNamedItem("value")?.InnerText, out Angle);
            temp = camNode.SelectSingleNode("LotSize");
            if (int.TryParse(temp?.Attributes.GetNamedItem("value")?.InnerText, out tempInt))
                LotSize = (LotSize)tempInt;
            temp = camNode.SelectSingleNode("LastCaptureDate");
            LastCaptureDate = temp?.InnerText;

            Template = new LotSlotCollection(camNode.SelectSingleNode("LotSlotCollection"), Name);

        }

        public bool IsValid()
        {
            return Name != "" && Manufacturer != CameraManufacturer.None && Url != ""
                && Quality != CameraQuality.None && Angle >= 0 && Angle <= 360 && LotSize != LotSize.None && Template.HasData();
        }

        public void CreateXML(XmlDocument doc, XmlNode baseNode)
        {
            XmlElement itemNode = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Item", "");
            baseNode.AppendChild(itemNode);

            XmlElement element;

            element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Name", "");
            element.InnerText = Name;
            itemNode.AppendChild(element);

            element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Manufacturer", "");
            element.InnerText = Manufacturer.ToString();
            itemNode.AppendChild(element);

            element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Url", "");
            element.SetAttribute("value", Url);
            itemNode.AppendChild(element);

            element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Quality", "");
            element.SetAttribute("value", ((int)Quality).ToString());
            itemNode.AppendChild(element);

            element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Angle", "");
            element.SetAttribute("value", Angle.ToString());
            itemNode.AppendChild(element);

            element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "LotSize", "");
            element.SetAttribute("value", ((int)LotSize).ToString());
            itemNode.AppendChild(element);

            element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "LastCaptureDate", "");
            element.InnerText = LastCaptureDate;
            itemNode.AppendChild(element);

            Template.AddXML(doc, itemNode);
        }

        public void UpdateXML(XmlDocument doc, XmlNode baseNode)
        {
            //base node = cam item
            XmlElement element;

            element = (XmlElement)baseNode.SelectSingleNode("Name");
            element.InnerText = Name;

            //no need to update non-updatable options
            //element = (XmlElement)baseNode.SelectSingleNode("Manufacturer");
            //element.InnerText = Manufacturer.ToString();

            //element = (XmlElement)baseNode.SelectSingleNode("Url");
            //element.SetAttribute("value", Url);


            element = (XmlElement)baseNode.SelectSingleNode("Quality");
            element.SetAttribute("value", ((int)Quality).ToString());

            element = (XmlElement)baseNode.SelectSingleNode("Angle");
            element.SetAttribute("value", Angle.ToString());

            element = (XmlElement)baseNode.SelectSingleNode("LotSize");
            element.SetAttribute("value", ((int)LotSize).ToString());

            element = (XmlElement)baseNode.SelectSingleNode("LastCaptureDate");
            element.InnerText = LastCaptureDate;

            baseNode.RemoveChild(baseNode.SelectSingleNode("LotSlotCollection"));

            Template.AddXML(doc, baseNode);
        }

        public void UpdateXMLLastCaptureDate(XmlNode baseNode)
        {
            //base node = cam item
            XmlElement element;

            element = (XmlElement)baseNode.SelectSingleNode("LastCaptureDate");
            element.InnerText = LastCaptureDate;

        }


        static public bool CheckWebsite(string URL)
        {
            try
            {
                using (_ = WebRequest.Create(URL).GetResponse()) { }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DownloadCamImage(Action<System.Drawing.Bitmap, int> func, int index = 0)
        {
            return DownloadCamImage(Url, Manufacturer, func, index);
        }

        static public bool DownloadCamImage(string url, CameraManufacturer man, Action<System.Drawing.Bitmap, int> func, int index = 0)
        {
            if (url != "" && man != CameraManufacturer.None)
            {
                if (CheckWebsite(url))
                {
                    switch (man)
                    {
                        case CameraManufacturer.PanasonicHD:
                        case CameraManufacturer.ChannelVision:
                        case CameraManufacturer.Axis:
                        case CameraManufacturer.AxisMkII:
                            MJPEGStream stream = new MJPEGStream(url);
                            NewFrameEventHandler l = null;

                            l = (object sender, NewFrameEventArgs e) =>
                            {
                                func(e.Frame, index);
                                ((MJPEGStream)sender).NewFrame -= l;
                                ((MJPEGStream)sender).Stop();
                            };


                            stream.NewFrame += l;

                            stream.Start();
                            return true;
                    }
                }
            }
            return false;
        }

        public void UpdateLastCaptureDate()
        {
            var date = DateTime.Now;
            LastCaptureDate = $"{date:dd}{date:MM}{date:yy}-{date:HH}{date:mm}";
        }

    }
}
