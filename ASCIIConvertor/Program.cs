using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ASCIIConvertor
{
    class Program
    {
        #region Settings
        private const double WIDTH_OFFSET = 1.7;
        private const int MAX_WIDTH = 474;
        private const double X_OFFSET = 1.0;
        private const double Y_OFFSET = 1.1;
        private const int MAX_HEIGHT = 600;
        #endregion
        //char[] _asciiChars = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' };


        [STAThread]
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            var OpenIMGDialog = new OpenFileDialog
            {
                Filter = "Image | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.WriteLine("Нажмите Enter чтоб продолжить...\n");

            while (true)
            {
                Console.ReadLine();

                if (OpenIMGDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                var bitmap = new Bitmap(OpenIMGDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ReconstructPixel();

                var convertor = new BitmapASCIIConvertor(bitmap);
                var rows = convertor.Convert();

                foreach (var row in rows)
                    Console.WriteLine(row);

                var rowNegative = convertor.ConvertNegative();
                File.WriteAllLines("result.txt", rowNegative.Select(r => new string(r)));

                Console.SetCursorPosition(0, 0);
            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var maxWidth = MAX_WIDTH;
            var counting = X_OFFSET + Y_OFFSET / MAX_HEIGHT;
            var newHeight = bitmap.Height / WIDTH_OFFSET * maxWidth / bitmap.Width;
            if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
            return bitmap;
        }
    }
}
