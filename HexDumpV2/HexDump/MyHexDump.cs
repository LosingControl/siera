using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        const int LimitStock = MaximumReadBytes / ByteInLine;

        long stockBottom = 0;
        long stockTop = 0;
        
        readonly StringBuilder m_StrBldHexDump = new StringBuilder();

        string[] arrByte;
        
        public long StockBottom { get => stockBottom; set => stockBottom = value; }
        public long StockTop { get => stockTop; set => stockTop = value; }

        public int GetLimitStock { get => LimitStock;}

        public MyHexDump()
        { }

        public string[] GetHexDump(long scroll_value, string path, MyScroll scroll)
        {
            stockBottom = scroll_value + LimitStock / 2;
            stockTop = scroll_value - LimitStock / 2;

            GetStringsByte(scroll_value, path, scroll);
         
            return arrByte;
        }


        private void GetStringsByte(long scroll_value, string path , MyScroll scroll)
        {
            int newLine = 0;
            using (var fileStream = File.OpenRead(path))
            {
                scroll_value = SetSizeArray(scroll_value, fileStream, scroll);

                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    if (fileStream.Length > 0)
                    {
                        byte[] stringFromFile = new byte[ByteInLine];

                        fileStream.Seek((scroll_value * 16), SeekOrigin.Begin);

                        for (int line = 0; line < arrByte.Length; line++)
                        {
                            long output_offset = scroll_value++ * ByteInLine;

                            stringFromFile = binaryReader.ReadBytes((int)stringFromFile.Length);

                            FillingStringBytes(stringFromFile, output_offset);

                            if (stringFromFile.Length != 0)
                            {
                                m_StrBldHexDump.AppendLine();
                            }

                            arrByte[newLine] = m_StrBldHexDump.ToString();

                            newLine++;

                            m_StrBldHexDump.Clear();
                        }
                    }
                }
            }
        }

        private void FillingStringBytes(byte[] stringFromFile, long output_offset)
        {
            for (int i = 0; i < stringFromFile.Length; i++)
            {

                if (i == 0)
                {
                    m_StrBldHexDump.Append(output_offset.ToString("X8") + ": ");
                }

                m_StrBldHexDump.Append(' ' + stringFromFile[i].ToString("X2"));
            }
        }

        private long SetSizeArray(long scroll_value, FileStream fileStream, MyScroll scroll)
        {
            long size;

            if (fileStream.Length < MaximumReadBytes)
            {
                size = fileStream.Length / ByteInLine + 1;
                arrByte = new string[size];
            }
            else
            {
                if (scroll_value == 0)
                {
                    size = LimitStock + 1;
                    arrByte = new string[size];
                }
                else
                {
                    size = LimitStock + LimitStock + 1;

                    if (scroll_value == scroll.Maximum)
                    {
                        scroll_value -= LimitStock / 2;
                    }

                    arrByte = new string[size];
                }

            }

            return scroll_value;
        }
    }
}
