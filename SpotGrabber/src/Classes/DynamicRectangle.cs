#define DEBUGTEST

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotGrabber
{
    public class DynamicRectangle
    {
        public Line Line;
        public Polygon Rect { get; private set; }//top left, top right, bottom right, bottom left
        public Polygon InnerRect { get; private set; }
        public List<Polygon> InteractionRects = new List<Polygon>(8);
        //top left, top right, bottom right, bottom left, top center, center right, center bottom, center left

        public float Rotation = 0;
        public Vector2 Offset = default;

        public DynamicRectangle(Line line)
        {
            SetLine(line);
        }
        public DynamicRectangle(Polygon rect, float rotation, Vector2 offset)
        {
            Rect = rect;
            Rotation = rotation;
            Offset = offset;

            BuildRect();
        }

        void SetLine(Line line)
        {
            Line = line;

            float centerx = 0;
            float centery = 0;

            if (line.Point1.X < line.Point2.X && line.Point1.Y < line.Point2.Y)//top left start
            {
                centerx = (line.Point2.X - line.Point1.X) / 2;
                centery = (line.Point2.Y - line.Point1.Y) / 2;
                Offset = new Vector2(line.Point1.X + centerx, line.Point1.Y + centery);
            }
            else if (line.Point1.X >= line.Point2.X && line.Point1.Y < line.Point2.Y)//top right
            {
                centerx = (line.Point1.X - line.Point2.X) / 2;
                centery = (line.Point2.Y - line.Point1.Y) / 2;
                Offset = new Vector2(line.Point2.X + centerx, line.Point1.Y + centery);
            }
            else if (line.Point1.X >= line.Point2.X && line.Point1.Y >= line.Point2.Y)//bottom right
            {
                centerx = (line.Point1.X - line.Point2.X) / 2;
                centery = (line.Point1.Y - line.Point2.Y) / 2;
                Offset = new Vector2(line.Point2.X + centerx, line.Point2.Y + centery);
            }
            else if (line.Point1.X < line.Point2.X && line.Point1.Y >= line.Point2.Y)//bottom left
            {
                centerx = (line.Point2.X - line.Point1.X) / 2;
                centery = (line.Point1.Y - line.Point2.Y) / 2;
                Offset = new Vector2(line.Point1.X + centerx, line.Point2.Y + centery);
            }

            Rect = new Polygon(new List<Vector2> {
                new Vector2(-centerx, -centery), new Vector2(centerx, -centery),
                new Vector2(centerx, centery), new Vector2(-centerx, centery) });
            Rotation = 0f;


            //Rotation = (float)Math.PI / 4f;
            BuildRect();




        }

        public bool isLineValid()
        {
            return -Rect.Left + Rect.Right > 10f && -Rect.Top + Rect.Bottom > 10f;
        }

        public void Rotate(float rot)
        {
            Rect.Rotate(-Rotation);
            Rotation += rot;
            BuildRect();
        }

        public void SetRect(List<Vector2> l)
        {
            Rect.Rotate(-Rotation);
            Rect = new Polygon(l);
            BuildRect();
        }

        private void BuildRect()
        {
            InnerRect = new Polygon(new List<Vector2>(Rect.Vertices));


            InnerRect.Scale(new Vector2(-.2f, -.2f));

            float interactionCenterx = (-InnerRect.Left + InnerRect.Right) / 6;
            float interactionCentery = (-InnerRect.Top + InnerRect.Bottom) / 6;

            InteractionRects.Clear();
            float centerx = Rect.Vertices[2].X;
            float centery = Rect.Vertices[2].Y;

            //top left
            InteractionRects.Add(new Polygon(new List<Vector2> { new Vector2(-centerx, -centery), new Vector2(InnerRect.Vertices[0].X, -centery), InnerRect.Vertices[0], new Vector2(-centerx, InnerRect.Vertices[0].Y) }));
            //top right
            InteractionRects.Add(new Polygon(new List<Vector2> { new Vector2(InnerRect.Vertices[1].X, -centery), new Vector2(centerx, -centery), new Vector2(centerx, InnerRect.Vertices[1].Y), InnerRect.Vertices[1] }));
            //bottom right
            InteractionRects.Add(new Polygon(new List<Vector2> { InnerRect.Vertices[2], new Vector2(centerx, InnerRect.Vertices[2].Y), new Vector2(centerx, centery), new Vector2(InnerRect.Vertices[2].X, centery) }));
            //bottom left
            InteractionRects.Add(new Polygon(new List<Vector2> { new Vector2(-centerx, InnerRect.Vertices[3].Y), InnerRect.Vertices[3], new Vector2(InnerRect.Vertices[3].X, centery), new Vector2(-centerx, centery) }));


            //top center
            InteractionRects.Add(new Polygon(new List<Vector2> { new Vector2(-interactionCenterx, Rect.Vertices[0].Y), new Vector2(interactionCenterx, Rect.Vertices[0].Y), new Vector2(interactionCenterx, InnerRect.Vertices[0].Y), new Vector2(-interactionCenterx, InnerRect.Vertices[0].Y) }));
            //right center
            InteractionRects.Add(new Polygon(new List<Vector2> { new Vector2(Rect.Vertices[1].X, -interactionCentery), new Vector2(InnerRect.Vertices[1].X, -interactionCentery), new Vector2(InnerRect.Vertices[1].X, interactionCentery), new Vector2(Rect.Vertices[1].X, interactionCentery) }));
            //bottom center
            InteractionRects.Add(new Polygon(new List<Vector2> { new Vector2(-interactionCenterx, Rect.Vertices[3].Y), new Vector2(interactionCenterx, Rect.Vertices[3].Y), new Vector2(interactionCenterx, InnerRect.Vertices[3].Y), new Vector2(-interactionCenterx, InnerRect.Vertices[3].Y) }));
            //left center
            InteractionRects.Add(new Polygon(new List<Vector2> { new Vector2(Rect.Vertices[0].X, -interactionCentery), new Vector2(InnerRect.Vertices[0].X, -interactionCentery), new Vector2(InnerRect.Vertices[0].X, interactionCentery), new Vector2(Rect.Vertices[0].X, interactionCentery) }));

            Rect.Rotate(Rotation);
            InnerRect.Rotate(Rotation);
            foreach (var rect in InteractionRects)
                rect.Rotate(Rotation);
        }


        public void SetPositionLocal(Vector2 pos)
        {
            Offset += pos;
        }

        public void ScaleToPoint(ControlType scaleType, Vector2 startPoint, Vector2 scaleToPoint)
        {
            startPoint -= Offset;

            Rect.Rotate(-Rotation);

            //var temp = scaleToPoint / 2;
            //scaleToPoint = MyMathHelper.RotateVector2ByAngle(scaleToPoint/2, Rotation);

            Vector2 scaleToPointDiv2 = scaleToPoint / 2;

            List<Vector2> scaledRect;
            

            switch (scaleType)
            {
                case ControlType.ScaleCornerTopLeft:
                    //top left sel
                    scaleToPointDiv2 = MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation) / 2;
                    scaledRect = new List<Vector2> {
                        Rect.Vertices[0] + scaleToPointDiv2,
                        Rect.Vertices[1] + new Vector2(-scaleToPointDiv2.X, scaleToPointDiv2.Y),
                        Rect.Vertices[2] - scaleToPointDiv2,
                        Rect.Vertices[3] + new Vector2(scaleToPointDiv2.X, -scaleToPointDiv2.Y) };
                    Offset += scaleToPoint / 2;
                    break;
                case ControlType.ScaleCornerTopRight:
                    //top right sel
                    scaleToPointDiv2 = MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation) / 2;
                    scaledRect = new List<Vector2> {
                        Rect.Vertices[0] + new Vector2(-scaleToPointDiv2.X, scaleToPointDiv2.Y),
                        Rect.Vertices[1] + scaleToPointDiv2,
                        Rect.Vertices[2] + new Vector2(scaleToPointDiv2.X, -scaleToPointDiv2.Y),
                        Rect.Vertices[3] - scaleToPointDiv2
                    };
                    Offset += scaleToPoint / 2;
                    break;
                default:
                case ControlType.ScaleCornerBottomRight:
                    // bottom right sel
                    scaleToPointDiv2 = MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation) / 2;
                    scaledRect = new List<Vector2> {
                        Rect.Vertices[0] - scaleToPointDiv2,
                        Rect.Vertices[1] + new Vector2(scaleToPointDiv2.X, -scaleToPointDiv2.Y),
                        Rect.Vertices[2] + scaleToPointDiv2,
                        Rect.Vertices[3] + new Vector2(-scaleToPointDiv2.X, scaleToPointDiv2.Y)
                    };
                    Offset += scaleToPoint / 2;
                    break;
                case ControlType.ScaleCornerBottomLeft:
                    //bottom left sel
                    scaleToPointDiv2 = MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation) / 2;
                    scaledRect = new List<Vector2> {
                        Rect.Vertices[0] + new Vector2(scaleToPointDiv2.X, -scaleToPointDiv2.Y),
                        Rect.Vertices[1] - scaleToPointDiv2,
                        Rect.Vertices[2] + new Vector2(-scaleToPointDiv2.X, scaleToPointDiv2.Y),
                        Rect.Vertices[3] + scaleToPointDiv2
                    };
                    Offset += scaleToPoint / 2;
                    break;
                case ControlType.ScaleMidRight:
                    //mid right
                    scaleToPoint = MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation);
                    scaleToPointDiv2 = new Vector2(scaleToPoint.X / 2, 0f);

                    scaledRect = new List<Vector2> {
                        default,
                        Rect.Vertices[1] + new Vector2(scaleToPointDiv2.X, 0),
                        Rect.Vertices[2] + new Vector2(scaleToPointDiv2.X, 0),
                        default,
                    };

                    scaledRect[0] = -scaledRect[2];
                    scaledRect[3] = -scaledRect[1];

                    scaleToPoint.Y = 0;
                    Offset += (MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation) / 2) * new Vector2(1,-1);
                    break;
                case ControlType.ScaleMidLeft:
                    scaleToPoint = MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation);
                    scaleToPointDiv2 = new Vector2(scaleToPoint.X / 2, 0f);

                    scaledRect = new List<Vector2> {
                        Rect.Vertices[0] + new Vector2(scaleToPointDiv2.X, 0),
                        default,
                        default,
                        Rect.Vertices[3] + new Vector2(scaleToPointDiv2.X, 0),
                    };

                    scaledRect[2] = -scaledRect[0];
                    scaledRect[1] = -scaledRect[3];

                    scaleToPoint.Y = 0;
                    Offset += (MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation) / 2) * new Vector2(1, -1);
                    break;
                case ControlType.ScaleMidUp:
                    scaleToPoint = MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation);
                    scaleToPointDiv2 = new Vector2(0f, scaleToPoint.Y / 2);

                    scaledRect = new List<Vector2> {
                        Rect.Vertices[0] + new Vector2(0, scaleToPointDiv2.Y),
                        Rect.Vertices[1] + new Vector2(0, scaleToPointDiv2.Y),
                        default,
                        default,
                    };

                    scaledRect[2] = -scaledRect[0];
                    scaledRect[3] = -scaledRect[1];

                    scaleToPoint.X = 0;
                    Offset += (MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation) / 2) * new Vector2(-1, 1);
                    break;
                case ControlType.ScaleMidDown:
                    scaleToPoint = MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation);
                    scaleToPointDiv2 = new Vector2(0f, scaleToPoint.Y / 2);

                    scaledRect = new List<Vector2> {
                        default,
                        default,
                        Rect.Vertices[2] + new Vector2(0, scaleToPointDiv2.Y),
                        Rect.Vertices[3] + new Vector2(0, scaleToPointDiv2.Y)
                    };

                    scaledRect[0] = -scaledRect[2];
                    scaledRect[1] = -scaledRect[3];

                    scaleToPoint.X = 0;
                    Offset += (MyMathHelper.RotateVector2ByAngle(scaleToPoint, Rotation) / 2) * new Vector2(-1, 1);
                    break;
            }

            Rect = new Polygon(scaledRect);

            BuildRect();

        }

        public void Scale(Vector2 scale)
        {
            Rect.Rotate(-Rotation);

            List<Vector2> vert = new List<Vector2>(Rect.Vertices);
            for (int i = 0; i < vert.Count; i++)
            {
                vert[i] *= scale;
            }

            Rect = new Polygon(vert);

            BuildRect();

        }

        public bool Contains(Vector2 pos)
        {
            return Rect.Contains(pos - Offset);
        }
        public bool ContainsInner(Vector2 pos)
        {
            return InnerRect.Contains(pos - Offset);
        }

        //scale: corner, mid
        //rotate: the rest


        public ControlType GetSelectAreaType(Vector2 pos)
        {
            Vector2 aPos = pos - Offset;

            if (!ContainsInner(pos))
            {
                if (Contains(pos))
                {
                    for (int i = 0; i < InteractionRects.Count; i++)
                    {

                        if (InteractionRects[i].Contains(pos - Offset))
                        {
                            //top left, top right, bottom right, bottom left,
                            //top center, center right, center bottom, center left
                            switch (i)
                            {
                                
                                case 0:
                                    return ControlType.ScaleCornerTopLeft;
                                case 1:
                                    return ControlType.ScaleCornerTopRight;
                                case 2:
                                    return ControlType.ScaleCornerBottomRight;
                                case 3:
                                    return ControlType.ScaleCornerBottomLeft;
                                case 4:
                                    return ControlType.ScaleMidUp;
                                case 5:
                                    return ControlType.ScaleMidRight;
                                case 6:
                                    return ControlType.ScaleMidDown;
                                case 7:
                                    return ControlType.ScaleMidLeft;
                            }
                        }
                    }
                    return ControlType.Rotate;

                }
                return ControlType.None;
            }
            else
                return ControlType.Position;
        }

        public void Draw(SpriteBatch sb)
        {

            sb.DrawPolygon(Offset, Rect, Color.Black);

#if DEBUGTEST
            Polygon rect = Rect.TransformedCopy(Vector2.Zero, 0, new Vector2(-.2f, -.2f));
            //Polygon rectRot = new Polygon(new List<Vector2> { new Vector2() });

            foreach (var recti in InteractionRects)
                //{
                //if(InteractionRects[0] != null)
                sb.DrawPolygon(Offset, recti, Color.Yellow);
            //}

            sb.DrawPolygon(Offset, InnerRect, Color.Red); //move rect


#endif



        }
    }

}
