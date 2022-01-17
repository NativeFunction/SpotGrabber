using AForge.Video;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
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
        public string PostalCode = "";
        public CameraQuality Quality = CameraQuality.None;
        public int Angle = 0;
        public LotSize LotSize = LotSize.None;
        public string LastCaptureDate = "000000-0000";
        public LotSlotCollection Template;

        public CameraData() { }

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
            temp = camNode.SelectSingleNode("PostalCode");
            PostalCode = temp?.Attributes.GetNamedItem("value")?.InnerText;
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
            return Name != "" && Manufacturer != CameraManufacturer.None && Url != "" && PostalCode != ""
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

            element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "PostalCode", "");
            element.SetAttribute("value", PostalCode);
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

        public bool CheckWebsite(string URL)
        {
            try
            {
                WebRequest.Create(URL).GetResponse();
                return true;
            }
            catch
            {
                return false;
            }

        }


        public void DownloadCamImage(Action<System.Drawing.Bitmap> func)
        {
            if (Url != "" && Manufacturer != CameraManufacturer.None)
            {
                if (CheckWebsite(Url))
                {
                    switch (Manufacturer)
                    {
                        case CameraManufacturer.Axis:
                            MJPEGStream stream = new MJPEGStream(Url);
                            bool needImage = true;
                            stream.NewFrame += (object sender, NewFrameEventArgs e) => 
                            {
                                if (needImage)
                                {
                                    func(e.Frame);
                                    stream.Stop();
                                }
                                else
                                    needImage = false;
                            };
                            stream.Start();
                            break;
                    }
                }
            }
        }
    }
}
