using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WebUI.Infrastructure
{
    public class Captcha : IDisposable
    {
        public static string CaptchaValueKey = "CaptchaText";
        public string Text { get; private set; }
        public Bitmap Image { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly string fontName;
        private readonly Random random = new Random();

        public Captcha(string s, int width, int height, string pFontName)
        {
            Text = s;
            SetDimensions(width, height);
            fontName = pFontName ?? FontFamily.GenericSerif.Name;
            GenerateImage();
        }

        ~Captcha()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Image.Dispose();
        }

        private void SetDimensions(int width, int height)
        {
            Width = (width <= 0) ? 100 : width;
            Height = (height <= 0) ? 100 : height;
        }

        private void GenerateImage()
        {
            var bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width, Height);

            var brush = new LinearGradientBrush(rect, Color.Yellow, Color.Orange, 2f);
            graphics.FillRectangle(brush, rect);

            var font = CreateFont(rect, graphics);
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            var path = new GraphicsPath();
            path.AddString(Text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            var hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.DarkBlue, Color.CornflowerBlue);
            graphics.FillPath(hatchBrush, path);

            var m = Math.Max(rect.Width, rect.Height);
            for (var i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                var x = random.Next(rect.Width);
                var y = random.Next(rect.Height);
                var w = random.Next(m / 40);
                var h = random.Next(m / 40);
                graphics.FillEllipse(hatchBrush, x, y, w, h);
            }

            font.Dispose();
            hatchBrush.Dispose();
            graphics.Dispose();

            Image = bitmap;
        }

        private Font CreateFont(Rectangle rect, Graphics graphics)
        {
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            do
            {
                fontSize--;
                font = new Font(fontName, fontSize, FontStyle.Bold);
                size = graphics.MeasureString(Text, font);
            } while (size.Width > rect.Width);
            return font;
        }
    }
}