using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace HexDump
{
    public partial class Form1 : Form
    {
        readonly OpenFileDialog openFileDialog = new OpenFileDialog();
        //private bool openFile = false; Строка 61
        const int ByteInLine = 16;
        MyHexDump hex = new MyHexDump();
        //MyScroll myScroll = new MyScroll();
        int count_strings_in_MainHexBox;
        string[] allArrByte;
        int x = 0;
        int k = 3;
        //int offset = 0;

        public Form1()
        {
            InitializeComponent();
            textBox3.Text = MainHexBox.Height.ToString();
            count_strings_in_MainHexBox = (MainHexBox.Height / MainHexBox.Font.Height);

        }

        private void Search_Click(object sender, EventArgs e)
        {
            //if (!openFile)
            //{
            //    openFileDialog.InitialDirectory = "c:\\";
            //    openFileDialog.FileName = null;
            //}
            myScroll.ResetSettings();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MainHexBox.Clear();
                PathBox.Clear();
                PathBox.Text = openFileDialog.FileName;
                //PrintHexDump(myScroll.ValueString);
                PrintHex();
                //openFile = true;              
            }
        }

        private void PrintHex()
        {

            //идея - выводить всегда по (count_strings_in_MainHexBox + 20), когда
            //myScroll.ValueString == (count_strings_in_MainHexBox + 20) отрисовывать
            //в текст бокс начиная с (myScroll.ValueString + 1 или myScroll.ValueString - 1
            //или myScroll.ValueString) пока не решил. Для этого можно создать переменную
            //которая отслеживает (count_strings_in_MainHexBox + 20) в большую сторону и в меньшую сторону
            //посредством - X = (count_strings_in_MainHexBox + 20) или X = -(count_strings_in_MainHexBox + 20)
            MainHexBox.Clear();
            allArrByte = hex.GetHexDump(myScroll.ValueString, PathBox.Text, count_strings_in_MainHexBox);
            foreach (var item in allArrByte)
            {
                MainHexBox.Text += item;
            }
        }

        private void PrintHexDump(int scroll_value)
        {
            using (var fileStream = File.OpenRead(openFileDialog.FileName))
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
                        }
                        MainHexBox.Text = strBld.ToString();
                    }
                }

                /*if (fileStream.Length > 0)
                {
                    int count_strings_in_MainHexBox = (MainHexBox.Height / MainHexBox.Font.Height);

                    byte[] sumbol_from_file = new byte[1];
                    var strBld = new StringBuilder();

                    for (int line = 0; line < count_strings_in_MainHexBox; line++)
                    {
                        if (line == 17)
                            System.Diagnostics.Debugger.Break();
                        long output_offset = scroll_value++ * ByteInLine;

                        strBld.Append(output_offset.ToString("X8") + ": ");

                        for (int i = 0; i < ByteInLine; i++)
                        {
                            if (fileStream.Read(sumbol_from_file, 0, sumbol_from_file.Length) != 0)
                                strBld.Append(' ' + sumbol_from_file[0].ToString("X2"));
                        }

                        strBld.AppendLine(); //тут лишняя строка в конце вывода всего хекс дампа
                    }

                    MainHexBox.Text = strBld.ToString();
                }*/
            }
        }

        private void MyScroll_Scroll(object sender, ScrollEventArgs e)
        {
            myScroll.SetSettingScroll(PathBox.Text);
            //PrintHexDump(myScroll.ValueString);
            PrintHex();
            x++;
            textBox1.Text = myScroll.Maximum.ToString();
            textBox2.Text = myScroll.ValueString.ToString();
            textBox5.Text = myScroll.ScrollPos.ToString();
            textBox6.Text = myScroll.Minimum.ToString();
            //textBox4.Text = myScroll.MyValue.ToString();
            if (x > k)
            {
                x = 0;
                base.OnScroll(e);
            }
            
        }
    }
}
