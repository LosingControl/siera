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

        string[] temp = new string[20];

        public MyHexDump()
        { }
        public string[] GetHexDump(int scroll_value, string path, int count_strings_in_MainHexBox)
        {
            
            int x = 0;
            using (var fileStream = File.OpenRead(path))
            {            
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    if (fileStream.Length > 0)
                    {
                        var strBld = new StringBuilder();

                        byte[] stringFromFile = new byte[16];

                        fileStream.Seek((scroll_value * 16), SeekOrigin.Begin);

                        for (int line = 0; line < count_strings_in_MainHexBox; line++)
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
                            temp[x] = strBld.ToString();
                            x++;
                            strBld.Clear();
                            
                        }
                    }
                }
            }
            return temp;
        }
    }
}
