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
        //MyScroll myScroll = new MyScroll();

        public Form1()
        {
            InitializeComponent();
            textBox3.Text = MainHexBox.Height.ToString();
            /*
            ScrollHexBox.Maximum = int.MaxValue;
            ScrollHexBox.Value = int.MaxValue;
            textBox3.Text = ScrollHexBox.Value.ToString();*/
        }

        private void Search_Click(object sender, EventArgs e)
        {
            //if (!openFile)
            //{
            //    openFileDialog.InitialDirectory = "c:\\";
            //    openFileDialog.FileName = null;
            //}
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MainHexBox.Clear();
                PathBox.Text = openFileDialog.FileName;
                PrintHexDump(myScroll.Value);
                //openFile = true;
            }
        }

        private void PrintHexDump(int scroll_value)
        {
            using (var fileStream = File.OpenRead(openFileDialog.FileName))
            {
                fileStream.Seek((scroll_value * 16), SeekOrigin.Begin);

                if (fileStream.Length > 0)
                {
                    int count_strings_in_MainHexBox = (MainHexBox.Height / MainHexBox.Font.Height);

                    byte[] sumbol_from_file = new byte[1];
                    var strBld = new StringBuilder();

                    for (int line = 0; line < count_strings_in_MainHexBox; line++)
                    {
                        /*if (line == 17)
                            System.Diagnostics.Debugger.Break();*/
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
                }
            }
        }

        private void myScroll_Scroll(object sender, ScrollEventArgs e)
        {
            myScroll.SetSettingScroll(PathBox.Text);
            textBox1.Text = myScroll.Maximum.ToString();
            //textBox2.Text = myScroll.Value.ToString();
            PrintHexDump(myScroll.Value);
        }

        
    }
}
