#define DEBUGTEST

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using System;
using System.Collections.Generic;
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

        public void ScaleToPoint(Vector2 scaleToPoint)
        {
            scaleToPoint.X = MyMathHelper.Clamp(scaleToPoint.X, -10f, 10f);
            scaleToPoint.Y = MyMathHelper.Clamp(scaleToPoint.Y, -10f, 10f);

            Vector2 scale = new Vector2(scaleToPoint.X / (-Rect.Left + Rect.Right), scaleToPoint.Y / (-Rect.Top + Rect.Bottom));

            scale.X = MyMathHelper.Clamp(scale.X, -.1f, .1f);
            scale.Y = MyMathHelper.Clamp(scale.Y, -.1f, .1f);

            Rect.Rotate(-Rotation);

            Rect.Scale(scale);

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
                                case 4:
                                case 6:
                                    return ControlType.ScaleMidY;
                                case 5:
                                case 7:
                                    return ControlType.ScaleMidX;
                                case 0:
                                case 2:
                                    return ControlType.ScaleCornerBS;
                                case 1:
                                case 3:
                                    return ControlType.ScaleCornerFS;
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
