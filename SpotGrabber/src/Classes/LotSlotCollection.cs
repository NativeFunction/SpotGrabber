using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ImageProcessor.Imaging.Filters;
using ImageProcessor.Processors;
using ImageProcessor.Imaging.Formats;

namespace SpotGrabber
{

    public class LotSlotCollection
    {
        Line currentLine = new Line();
        public List<DynamicRectangle> Rects = new List<DynamicRectangle>(4);

        Vector2 currentSelectPoint = default;
        ControlType isAdjRect = ControlType.None;
        DynamicRectangle adjRect = null;
        public Cursor ActiveCursor = Cursors.Cross;
        public Rectangle CurrentImageBounds = default;
        public string LotName = "UNDEFINED";
        float initRot = 0;

        public LotSlotCollection(string lotName)
        {
            LotName = lotName;
        }

        public LotSlotCollection(string xmlPath, Rectangle imageBounds, string lotName)
        {
            CurrentImageBounds = imageBounds;
            InputXML(LoadXML(xmlPath));
            LotName = lotName;
        }

        public LotSlotCollection(XmlNode lscNode, string lotName)
        {
            InputXML(lscNode);

            LotName = lotName;
        }

        public LotSlotCollection(string lotName, List<DynamicRectangle> _rects, int imageWidth, int imageHeight)
        {
            CurrentImageBounds.Height = imageHeight;
            CurrentImageBounds.Width = imageWidth;
            Rects = _rects;
            LotName = lotName;
        }


        public void Update(Rectangle imageBounds)
        {
            if (imageBounds != CurrentImageBounds)
            {
                Vector2 scaleP = new Vector2((float)imageBounds.Height / CurrentImageBounds.Height, (float)imageBounds.Width / CurrentImageBounds.Width);


                foreach (var rect in Rects)
                {
                    rect.Offset *= scaleP;
                    rect.Scale(scaleP);

                }


            }

            CurrentImageBounds = imageBounds;
            //sideLine1, connectLine, sideLine2, slotLine;
            if (Utils.ApplicationIsActivated() && Utils.IsInBounds(InputManager.GetMousePos(), imageBounds))
            {

                if (InputManager.IsMouseButtonJustPressed(MouseInputButtons.LeftButton))
                {

                    foreach (var rect in Rects)
                    {
                        var hh = InputManager.GetMousePosVec();
                        if (rect.ContainsInner(InputManager.GetMousePosVec()))//position
                        {
                            currentSelectPoint = InputManager.GetMousePosVec();
                            isAdjRect = ControlType.Position;
                            adjRect = rect;
                            break;
                        }
                        else if (rect.Contains(InputManager.GetMousePosVec()))//scale and rot
                        {
                            currentSelectPoint = InputManager.GetMousePosVec();
                            isAdjRect = rect.GetSelectAreaType(currentSelectPoint);
                            adjRect = rect;

                            Vector2 point = InputManager.GetMousePosVec() - adjRect.Offset;
                            initRot = (float)Math.Atan2(point.Y, point.X);
                            break;
                        }

                    }

                    if (isAdjRect == ControlType.None)
                    {
                        isAdjRect = ControlType.Line;
                        currentLine.Point1 = InputManager.GetMousePosVec();
                    }
                }
                //else if (InputManager.IsMouseButtonJustPressed(MouseInputButtons.RightButton))
                //{
                //
                //    foreach (var rect in Rects)
                //    {
                //        if (rect.Contains(InputManager.GetMousePosVec()))//scale and rot
                //        {
                //            currentSelectPoint = InputManager.GetMousePosVec();
                //            isAdjRect = ControlType.Rotate;
                //            adjRect = rect;
                //
                //            Vector2 point = InputManager.GetMousePosVec() - adjRect.Offset;
                //            initRot = (float)Math.Atan2(point.Y, point.X);
                //            break;
                //        }
                //    }
                //}
                else if (InputManager.IsMouseButtonJustReleased(MouseInputButtons.LeftButton))
                {
                    //end line segment
                    if (isAdjRect == ControlType.Line)
                    {
                        currentLine.Point2 = InputManager.GetMousePosVec();
                        AddLine(currentLine);

                        currentLine = new Line();
                    }

                    isAdjRect = ControlType.None;
                }
                //else if (InputManager.IsMouseButtonJustReleased(MouseInputButtons.RightButton))
                //{
                //    isAdjRect = ControlType.None;
                //}
                else if (InputManager.IsMouseButtonPressed(MouseInputButtons.LeftButton))
                {
                    //position
                    if (adjRect != null)
                    {
                        switch (isAdjRect)
                        {
                            case ControlType.Position:
                                adjRect.SetPositionLocal(InputManager.GetMousePosVec() - currentSelectPoint);
                                currentSelectPoint = InputManager.GetMousePosVec();
                                break;
                            case ControlType.ScaleCornerTopLeft:
                            case ControlType.ScaleCornerTopRight:
                            case ControlType.ScaleCornerBottomRight:
                            case ControlType.ScaleCornerBottomLeft:
                            case ControlType.ScaleMidLeft:
                            case ControlType.ScaleMidRight:
                            case ControlType.ScaleMidUp:
                            case ControlType.ScaleMidDown:
                                adjRect.ScaleToPoint(isAdjRect, currentSelectPoint, InputManager.GetMousePosVec() - currentSelectPoint);
                                currentSelectPoint = InputManager.GetMousePosVec();
                                break;
                            case ControlType.Rotate:
                                {
                                    Vector2 point = InputManager.GetMousePosVec() - adjRect.Offset;
                                    float rot = (float)Math.Atan2(point.Y, point.X);

                                    adjRect.Rotate(rot - initRot);

                                    initRot = (float)Math.Atan2(point.Y, point.X);
                                }
                                break;

                        }

                    }
                }
                //else if (InputManager.IsMouseButtonPressed(MouseInputButtons.RightButton))
                //{
                //    if (adjRect != null)
                //    {
                //        if (isAdjRect == ControlType.Rotate)
                //        {
                //            Vector2 point = InputManager.GetMousePosVec() - adjRect.Offset;
                //            float rot = (float)Math.Atan2(point.Y, point.X);
                //
                //            adjRect.Rotate(rot - initRot);
                //
                //            initRot = (float)Math.Atan2(point.Y, point.X);
                //        }
                //    }
                //}
                else if (InputManager.IsMouseButtonJustReleased(MouseInputButtons.LeftButton))
                {
                    if (isAdjRect == ControlType.Line)
                    {
                        currentLine = new Line();
                        isAdjRect = ControlType.None;
                    }
                }
            }
        }

        public void Clear()
        {
            currentLine = new Line();
            Rects.Clear();
        }

        public void AddLine(Line l)
        {
            var dr = new DynamicRectangle(l);
            if (dr.isLineValid())
            {
                Rects.Add(dr);
            }
        }

        public void Draw(SpriteBatch sb)
        {


            //draw current line
            if (currentLine.Point1 != new Vector2(-1, -1))
                sb.DrawLine(currentLine.Point1, InputManager.GetMousePosVec(), Color.Black);

            bool btn = !InputManager.IsMouseButtonPressed(MouseInputButtons.LeftButton);

            if (btn)
                ActiveCursor = Cursors.Cross;

            foreach (var rect in Rects)
            {
                rect.Draw(sb);
                
                if (btn)
                {
                    switch (rect.GetSelectAreaType(InputManager.GetMousePosVec()))
                    {
                        case ControlType.Rotate:
                            ActiveCursor = Cursors.Arrow;
                            break;
                        case ControlType.ScaleCornerTopLeft:
                        case ControlType.ScaleCornerBottomRight:
                            ActiveCursor = Cursors.SizeNWSE;
                            break;
                        case ControlType.ScaleCornerTopRight:
                        case ControlType.ScaleCornerBottomLeft:
                            ActiveCursor = Cursors.SizeNESW;
                            break;
                        case ControlType.ScaleMidLeft:
                        case ControlType.ScaleMidRight:
                            ActiveCursor = Cursors.SizeWE;
                            break;
                        case ControlType.ScaleMidUp:
                        case ControlType.ScaleMidDown:
                            ActiveCursor = Cursors.SizeNS;
                            break;
                        case ControlType.Position:
                            ActiveCursor = Cursors.SizeAll;
                            break;

                    }
                }

                //for right click rot
                //if (rect.Contains(InputManager.GetMousePosVec()) && InputManager.IsMouseButtonJustPressed(MouseInputButtons.RightButton))
                //{
                //    ActiveCursor = Cursors.Arrow;
                //}

            }


        }

        public bool HasData()
        {
            return Rects.Count > 0;
        }

        public void OutputXML(string path)
        {
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", ""));
            doc.AppendChild(doc.CreateWhitespace("\r\n"));
            XmlElement lscNode = (XmlElement)doc.CreateNode(XmlNodeType.Element, "LotSlotCollection", "");
            lscNode.SetAttribute("imageWidth", CurrentImageBounds.Width.ToString());
            lscNode.SetAttribute("imageHeight", CurrentImageBounds.Height.ToString());
            var drdNode = doc.CreateNode(XmlNodeType.Element, "DynamicRectangleData", "");
            doc.AppendChild(lscNode);
            lscNode.AppendChild(drdNode);


            for (int i = 0; i < Rects.Count; i++)
            {
                var rect = Rects[i];
                var itemNode = doc.CreateNode(XmlNodeType.Element, "Item", "");
                drdNode.AppendChild(itemNode);

                XmlElement element;

                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Offset", "");
                element.SetAttribute("x", rect.Offset.X.ToString());
                element.SetAttribute("y", rect.Offset.Y.ToString());
                itemNode.AppendChild(element);

                rect.Rect.Rotate(-rect.Rotation);

                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "TopLeft", "");
                element.SetAttribute("x", rect.Rect.Vertices[0].X.ToString());
                element.SetAttribute("y", rect.Rect.Vertices[0].Y.ToString());
                itemNode.AppendChild(element);
                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "TopRight", "");
                element.SetAttribute("x", rect.Rect.Vertices[1].X.ToString());
                element.SetAttribute("y", rect.Rect.Vertices[1].Y.ToString());
                itemNode.AppendChild(element);
                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "BottomLeft", "");
                element.SetAttribute("x", rect.Rect.Vertices[2].X.ToString());
                element.SetAttribute("y", rect.Rect.Vertices[2].Y.ToString());
                itemNode.AppendChild(element);
                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "BottomRight", "");
                element.SetAttribute("x", rect.Rect.Vertices[3].X.ToString());
                element.SetAttribute("y", rect.Rect.Vertices[3].Y.ToString());
                itemNode.AppendChild(element);
                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Rotation", "");
                element.SetAttribute("value", rect.Rotation.ToString());
                itemNode.AppendChild(element);

                rect.Rect.Rotate(rect.Rotation);
            }

            doc.Save(path);
        }

        public void AddXML(XmlDocument doc, XmlNode baseNode)
        {

            XmlElement lscNode = (XmlElement)doc.CreateNode(XmlNodeType.Element, "LotSlotCollection", "");
            lscNode.SetAttribute("imageWidth", CurrentImageBounds.Width.ToString());
            lscNode.SetAttribute("imageHeight", CurrentImageBounds.Height.ToString());
            var drdNode = doc.CreateNode(XmlNodeType.Element, "DynamicRectangleData", "");
            baseNode.AppendChild(lscNode);
            lscNode.AppendChild(drdNode);


            for (int i = 0; i < Rects.Count; i++)
            {
                var rect = Rects[i];
                var itemNode = doc.CreateNode(XmlNodeType.Element, "Item", "");
                drdNode.AppendChild(itemNode);

                XmlElement element;

                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Offset", "");
                element.SetAttribute("x", rect.Offset.X.ToString());
                element.SetAttribute("y", rect.Offset.Y.ToString());
                itemNode.AppendChild(element);

                rect.Rect.Rotate(-rect.Rotation);

                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "TopLeft", "");
                element.SetAttribute("x", rect.Rect.Vertices[0].X.ToString());
                element.SetAttribute("y", rect.Rect.Vertices[0].Y.ToString());
                itemNode.AppendChild(element);
                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "TopRight", "");
                element.SetAttribute("x", rect.Rect.Vertices[1].X.ToString());
                element.SetAttribute("y", rect.Rect.Vertices[1].Y.ToString());
                itemNode.AppendChild(element);
                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "BottomLeft", "");
                element.SetAttribute("x", rect.Rect.Vertices[2].X.ToString());
                element.SetAttribute("y", rect.Rect.Vertices[2].Y.ToString());
                itemNode.AppendChild(element);
                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "BottomRight", "");
                element.SetAttribute("x", rect.Rect.Vertices[3].X.ToString());
                element.SetAttribute("y", rect.Rect.Vertices[3].Y.ToString());
                itemNode.AppendChild(element);
                element = (XmlElement)doc.CreateNode(XmlNodeType.Element, "Rotation", "");
                element.SetAttribute("value", rect.Rotation.ToString());
                itemNode.AppendChild(element);

                rect.Rect.Rotate(rect.Rotation);
            }

        }

        XmlNode LoadXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc.DocumentElement.SelectSingleNode("/LotSlotCollection");
        }

        private void InputXML(XmlNode lscNode)
        {

            int.TryParse(lscNode.Attributes.GetNamedItem("imageWidth")?.InnerText, out int imageWidth);
            int.TryParse(lscNode.Attributes.GetNamedItem("imageHeight")?.InnerText, out int imageHeight);

            XmlNodeList drrNodes = lscNode.SelectNodes("DynamicRectangleData/Item");


            CurrentImageBounds.Height = imageHeight;
            CurrentImageBounds.Width = imageWidth;




            foreach (XmlNode node in drrNodes)
            {
                XmlNode temp;

                temp = node.SelectSingleNode("Offset");
                float.TryParse(temp?.Attributes.GetNamedItem("x")?.InnerText, out float OffsetX);
                float.TryParse(temp?.Attributes.GetNamedItem("y")?.InnerText, out float OffsetY);
                temp = node.SelectSingleNode("TopLeft");
                float.TryParse(temp?.Attributes.GetNamedItem("x")?.InnerText, out float TopLeftX);
                float.TryParse(temp?.Attributes.GetNamedItem("y")?.InnerText, out float TopLeftY);
                temp = node.SelectSingleNode("TopRight");
                float.TryParse(temp?.Attributes.GetNamedItem("x")?.InnerText, out float TopRightX);
                float.TryParse(temp?.Attributes.GetNamedItem("y")?.InnerText, out float TopRightY);
                temp = node.SelectSingleNode("BottomLeft");
                float.TryParse(temp?.Attributes.GetNamedItem("x")?.InnerText, out float BottomLeftX);
                float.TryParse(temp?.Attributes.GetNamedItem("y")?.InnerText, out float BottomLeftY);
                temp = node.SelectSingleNode("BottomRight");
                float.TryParse(temp?.Attributes.GetNamedItem("x")?.InnerText, out float BottomRightX);
                float.TryParse(temp?.Attributes.GetNamedItem("y")?.InnerText, out float BottomRightY);

                float.TryParse(node.SelectSingleNode("Rotation")?.Attributes.GetNamedItem("value")?.InnerText, out float rotation);


                DynamicRectangle dr = new DynamicRectangle(new Polygon(new List<Vector2> { new Vector2(TopLeftX, TopLeftY), new Vector2(TopRightX, TopRightY), new Vector2(BottomRightX, BottomRightY), new Vector2(BottomLeftX, BottomLeftY) }), rotation, new Vector2(OffsetX, OffsetY));

                Rects.Add(dr);

            }


        }

        public void ExportImages(string path, Texture2D image)
        {
            //get rect containing rot
            //rotate
            //cut and save

            for (int i = 0; i < Rects.Count; i++)
            {
                //scale percent
                float px = image.Width / (float)CurrentImageBounds.Width;
                float py = image.Height / (float)CurrentImageBounds.Height;

                Rects[i].Rect.Rotate(-Rects[i].Rotation);

                int rectWidthNoRot = (int)(Math.Ceiling(-Rects[i].Rect.Left + Rects[i].Rect.Right) * px);
                int rectHeightNoRot = (int)(Math.Ceiling(-Rects[i].Rect.Top + Rects[i].Rect.Bottom) * py);

                Rects[i].Rect.Rotate(Rects[i].Rotation);


                int snapshotWidth = (int)(Math.Ceiling(-Rects[i].Rect.Left + Rects[i].Rect.Right) * px);
                int snapshotHeight = (int)(Math.Ceiling(-Rects[i].Rect.Top + Rects[i].Rect.Bottom) * py);

                Color[] colors = new Color[snapshotWidth * snapshotHeight];
                image.GetData<Color>(0, new Rectangle((int)(Rects[i].Offset.X * px - snapshotWidth / 2), (int)(Rects[i].Offset.Y * py - snapshotHeight / 2), snapshotWidth, snapshotHeight), colors, 0, snapshotWidth * snapshotHeight);
                using (System.Drawing.Bitmap b = new System.Drawing.Bitmap(snapshotWidth, snapshotHeight))
                {


                    ImageProcessor.ImageFactory l = new ImageProcessor.ImageFactory();



                    for (int j = 0; j < colors.Length; j++)
                    {
                        b.SetPixel(j % snapshotWidth, snapshotWidth == 0 ? 0 : j / snapshotWidth, System.Drawing.Color.FromArgb(colors[j].A, colors[j].R, colors[j].G, colors[j].B));
                    }

                    l.Load(b);
                    l.Resolution(snapshotWidth, snapshotHeight);

                    l.Rotate(-MyMathHelper.RadToDeg(Rects[i].Rotation));


                    l.Crop(new System.Drawing.Rectangle(MyMathHelper.Clamp((l.Image.Width - rectWidthNoRot) / 2, 0, l.Image.Width), MyMathHelper.Clamp((l.Image.Height - rectHeightNoRot) / 2, 0, l.Image.Height), rectWidthNoRot, rectHeightNoRot));
                    l.Save(path + "_spot" + i.ToString() + ".jpg");


                    //b.Save(@"C:\Users\Rocko\Desktop\t.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        public void ExportImages(string path, System.Drawing.Bitmap image)
        {
            //get rect containing rot
            //rotate
            //cut and save
            if (image != null)
            {
                for (int i = 0; i < Rects.Count; i++)
                {
                    //scale percent
                    float px = image.Width / (float)CurrentImageBounds.Width;
                    float py = image.Height / (float)CurrentImageBounds.Height;

                    Rects[i].Rect.Rotate(-Rects[i].Rotation);

                    int rectWidthNoRot = (int)(Math.Ceiling(-Rects[i].Rect.Left + Rects[i].Rect.Right) * px);
                    int rectHeightNoRot = (int)(Math.Ceiling(-Rects[i].Rect.Top + Rects[i].Rect.Bottom) * py);

                    Rects[i].Rect.Rotate(Rects[i].Rotation);


                    int snapshotWidth = (int)(Math.Ceiling(-Rects[i].Rect.Left + Rects[i].Rect.Right) * px);
                    int snapshotHeight = (int)(Math.Ceiling(-Rects[i].Rect.Top + Rects[i].Rect.Bottom) * py);


                    var bmRect = new System.Drawing.Rectangle((int)(Rects[i].Offset.X * px - snapshotWidth / 2),
                    (int)(Rects[i].Offset.Y * py - snapshotHeight / 2), snapshotWidth, snapshotHeight);



                    using (System.Drawing.Bitmap b = new System.Drawing.Bitmap(snapshotWidth, snapshotHeight))
                    {

                        //image.Clone(bmRect, image.PixelFormat);
                        using (System.Drawing.Graphics gph = System.Drawing.Graphics.FromImage(b))
                        {
                            gph.DrawImage(image, new System.Drawing.Rectangle(0, 0, snapshotWidth, snapshotHeight),
                                bmRect,
                                System.Drawing.GraphicsUnit.Pixel);
                        }

                        //b.Save(@"C:\Users\Rocko\Desktop\" + i.ToString() + ".jpg",System.Drawing.Imaging.ImageFormat.Jpeg);

                        ImageProcessor.ImageFactory l = new ImageProcessor.ImageFactory();

                        l.Load(b);

                        l.Resolution(snapshotWidth, snapshotHeight);

                        l.Rotate(-MyMathHelper.RadToDeg(Rects[i].Rotation));


                        l.Crop(new System.Drawing.Rectangle(MyMathHelper.Clamp((l.Image.Width - rectWidthNoRot) / 2, 0, l.Image.Width), MyMathHelper.Clamp((l.Image.Height - rectHeightNoRot) / 2, 0, l.Image.Height), rectWidthNoRot, rectHeightNoRot));
                        l.Save(path + $"_spot" + i.ToString() + ".jpg");
                    }

                }
            }
        }

    }
}
