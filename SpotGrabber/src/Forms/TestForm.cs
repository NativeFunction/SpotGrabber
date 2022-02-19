using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;

namespace SpotGrabber
{
    public partial class TestForm : Form
    {

        Bitmap bm = null;
        public TestForm()
        {
            InitializeComponent();
        }

        private void ClearRectsButtonClick(object sender, EventArgs e)
        {
            Mono.lsc?.Clear();

            //compare testing
            //string pBase = @"C:\Users\Rocko\Documents\Visual Studio 2019\Projects\SpotGrabber\SpotGrabber\bin\Debug\Data\Spots\";
            //
            //var i = Codeuctivity.ImageSharpCompare.CalcDiff(
            //    pBase + "Helena, MT_180222-1416_spot0.jpg",
            //    pBase + "Helena, MT_180222-1537_spot0.jpg");
            //
            //Debug.WriteLine($"{i.AbsoluteError}, {i.MeanError}, {i.PixelErrorCount}, {i.PixelErrorPercentage}");
            //
            //i = Codeuctivity.ImageSharpCompare.CalcDiff(
            //    pBase + "Helena, MT_180222-1416_spot1.jpg",
            //    pBase + "Helena, MT_180222-1537_spot1.jpg");
            //
            //Debug.WriteLine($"{i.AbsoluteError}, {i.MeanError}, {i.PixelErrorCount}, {i.PixelErrorPercentage}");
            //
            //
            //i = Codeuctivity.ImageSharpCompare.CalcDiff(
            //    pBase + "Helena, MT_180222-1416_spot2.jpg",
            //    pBase + "Helena, MT_180222-1537_spot2.jpg");
            //
            //Debug.WriteLine($"{i.AbsoluteError}, {i.MeanError}, {i.PixelErrorCount}, {i.PixelErrorPercentage}");
            //
            //
            //for (int j = 0; j < 100; j++)
            //{
            //    i = Codeuctivity.ImageSharpCompare.CalcDiff(
            //        pBase + $"Hicksville, NY_180222-1415_spot{j}.jpg",
            //        pBase + $"Hicksville, NY_180222-1537_spot{j}.jpg");
            //
            //    Debug.WriteLine($"{j} = {i.AbsoluteError}, {i.MeanError}, {i.PixelErrorCount}, {i.PixelErrorPercentage}");
            //}



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
                bm = new System.Drawing.Bitmap(imgs[0]);
            }
        }

        private void SaveTemplateButtonClick(object sender, EventArgs e)
        {
            if (Mono.lsc != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "out.xml";
                sfd.DefaultExt = "xml";
                sfd.AddExtension = true;
                sfd.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                sfd.ShowDialog();
                if (sfd.CheckPathExists && sfd.FileName != "")
                {
                    Mono.lsc.OutputXML(sfd.FileName);
                }
                sfd.Dispose();
            }
        }

        private void LoadTemplateButtonClick(object sender, EventArgs e)
        {
            if (Mono.lsc != null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.FileName = "out.xml";
                ofd.DefaultExt = "xml";
                ofd.AddExtension = true;
                ofd.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK && ofd.CheckPathExists && ofd.CheckFileExists && ofd.FileName != "")
                {
                    Mono.LoadTemplate(ofd.FileName);
                }
                ofd.Dispose();
            }
        }

        private void ExportImagesButtonClick(object sender, EventArgs e)
        {
            if (Mono.lsc != null)
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker = true };

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok && bm != null)
                {
                    //Mono.lsc.ExportImages(dialog.FileName, Mono.Image);

                    Mono.lsc.ExportImages(dialog.FileName+ $"/{Mono.lsc.LotName}", bm);

                }
                dialog.Dispose();
            }
        }

    }
}
