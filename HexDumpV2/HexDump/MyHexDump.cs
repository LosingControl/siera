using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexDump
{
    class MyHexDump
    {
        const int ByteInLine = 16;
        const int MaximumReadBytes = 4194304;
        string[] arrByte;
        bool rereadPointer;
        readonly StringBuilder strBld = new StringBuilder();

        public bool RereadPointer { get => rereadPointer; set => rereadPointer = value; }

        public MyHexDump()
        { }

        public string[] GetHexDump(int scroll_value, string path, int markerReserve)
        {
            int x = 0;

            if (Math.Abs(markerReserve) == MaximumReadBytes / ByteInLine)
            {
                rereadPointer = true;
            }

            if (rereadPointer)
            {
                rereadPointer = false;
                SetSizeArray(ref path);
                GetStringsByte(ref scroll_value, ref x, ref path);
            }

            return arrByte;
        }

        private void SetSizeArray(ref string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                if (fileStream.Length < MaximumReadBytes)
                {
                    arrByte = new string[fileStream.Length / ByteInLine + 1];
                }
                else
                {
                    arrByte = new string[MaximumReadBytes / ByteInLine + 1];
                }
            }
        }

        private void GetStringsByte(ref int scroll_value, ref int x, ref string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    if (fileStream.Length > 0)
                    {
                        byte[] stringFromFile = new byte[ByteInLine];

                        fileStream.Seek((scroll_value * 16), SeekOrigin.Begin);

                        for (int line = 0; line < arrByte.Length; line++)
                        {
                            int output_offset = scroll_value++ * ByteInLine;
                            stringFromFile = binaryReader.ReadBytes((int)stringFromFile.Length);

                            for (int i = 0; i < stringFromFile.Length; i++)
                            {

                                if (i == 0)
                                {
                                    strBld.Append(output_offset.ToString("X8") + ": ");
                                }

                                strBld.Append(' ' + stringFromFile[i].ToString("X2"));
                            }

                            strBld.AppendLine();

                            arrByte[x] = strBld.ToString();

                            x++;

                            strBld.Clear();
                        }
                    }
                }
            }
        }
    }
}
