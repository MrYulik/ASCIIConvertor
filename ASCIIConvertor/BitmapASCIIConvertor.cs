using System.Drawing;

namespace ASCIIConvertor
{
    class BitmapASCIIConvertor
    {
        private readonly char[] _asciiChars = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' };
        private readonly char[] _asciiCharsNegative = { '@', '#', '$', '%', '?', '*', '+', ':', ',', '.' };
        private readonly Bitmap _bitmap;

        public BitmapASCIIConvertor(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public char[][] Convert()
        {
            return Convert(_asciiChars);
        }

        public char[][] ConvertNegative()
        {
            return Convert(_asciiCharsNegative);
        }

        private char[][] Convert(char[] asciiChars)
        {
            var result = new char[_bitmap.Height][];

            try
            {
                for (int y = 0; y < _bitmap.Height; y++)
                {
                    result[y] = new char[_bitmap.Height];

                    for (int x = 0; x < _bitmap.Width; x++)
                    {
                        int mapIndex = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, asciiChars.Length - 1);
                        result[y][x] = asciiChars[mapIndex];
                    }
                }
            }
            catch { }

            return result;
        }

        private float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}
