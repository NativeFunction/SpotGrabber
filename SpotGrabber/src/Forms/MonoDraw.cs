using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;

namespace SpotGrabber
{
    class MonoDraw : MonoGameControl
    {
        public Texture2D Image;
        
        Texture2D temp;
        public LotSlotCollection lsc;


        Rectangle imageBounds = new Rectangle();

        protected override void Initialize()
        {
            base.Initialize();
            InputManager.Initialize(Editor.graphics);
            temp = new Texture2D(Editor.graphics, 1, 1, false, SurfaceFormat.Color);
            temp.SetData(new[] { Color.White });

            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            InputManager.Update(gameTime);

            lsc?.Update(imageBounds);

        }

        protected override void Draw()
        {
            this.Cursor = lsc?.ActiveCursor;
            base.Draw();

            Editor.spriteBatch.Begin();

            DrawBackground();

            if (Image != null)
            {
                lsc?.Draw(Editor.spriteBatch);
            }

            Editor.spriteBatch.End();
        }

        public void DrawBackground()
        {
            if (Image != null)
            {
                int vpWidth = Editor.graphics.Viewport.Width;
                int vpHeight = Editor.graphics.Viewport.Height;
                int height = 0, width = 0;

                if (Editor.graphics.Viewport.AspectRatio <= (float)Image.Width / Image.Height)
                {
                    //height larger
                    width = vpWidth;
                    height = (int)(vpWidth * ((Image.Width == 0) ? 0 : (float)Image.Height / Image.Width));
                }
                else
                {
                    //width larger
                    height = vpHeight;
                    width = (int)(vpHeight * ((Image.Height == 0) ? 0 : (float)Image.Width / Image.Height));
                }

                imageBounds = new Rectangle(0, 0, width, height);

                Editor.spriteBatch.Draw(Image, new Rectangle(0, 0, width, height), Color.White);
            }
        }

        public void LoadTemplate(string path)
        {
            if (lsc != null)
            {
                lsc = new LotSlotCollection(path, imageBounds, lsc.LotName);
            }
        }

        public void LoadBackgroundImage(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            Image = Texture2D.FromStream(Editor.graphics, fileStream);
            fileStream.Dispose();

            lsc = new LotSlotCollection(Path.GetFileNameWithoutExtension(path));
        }

        public void LoadBackgroundImage(System.Drawing.Bitmap bm, string lotName, LotSlotCollection collection = null)
        {
            using (MemoryStream s = new MemoryStream())
            {
                bm.Save(s, System.Drawing.Imaging.ImageFormat.Jpeg);
                s.Seek(0, SeekOrigin.Begin);
                Image = Texture2D.FromStream(Editor.graphics, s);
                if (collection == null)
                    lsc = new LotSlotCollection(lotName);
                else
                    lsc = collection;
            }
        }
    }
}
