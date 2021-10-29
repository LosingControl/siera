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
        //const int reserveLimit = 20;
        //const int ByteInLine = 16;
        MyHexDump hex = new MyHexDump();

        //int count_strings_in_MainHexBox;
        
        string[] allArrByte;
        int x = 0;
        int k = 3;

        public Form1()
        {
            InitializeComponent();
            textBox3.Text = MainHexBox.Height.ToString();
            //count_strings_in_MainHexBox = (MainHexBox.Height / MainHexBox.Font.Height);
            
        }

        private void Search_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                hex.RereadPointer = true;

                myScroll.ResetSettings();

                MainHexBox.Clear();

                PathBox.Clear();
                
                PathBox.Text = openFileDialog.FileName;

                allArrByte = hex.GetHexDump(myScroll.ValueString, PathBox.Text, myScroll.MarkerReserve);

                PrintHex();
            }
        }

        private void PrintHex()
        {
            MainHexBox.Clear();

            allArrByte = hex.GetHexDump(myScroll.ValueString, PathBox.Text, myScroll.MarkerReserve);
            //проблема в производительности 
            for (int i = myScroll.ValueString; i < allArrByte.Length; i++)
            {
                MainHexBox.Text += allArrByte[i];
            }
        }

        private void MyScroll_Scroll(object sender, ScrollEventArgs e)
        {
            myScroll.SetSettingScroll(PathBox.Text);

            PrintHex();

            x++;
            textBox1.Text = myScroll.Maximum.ToString();
            textBox2.Text = myScroll.ValueString.ToString();
            textBox5.Text = myScroll.ScrollPos.ToString();
            textBox6.Text = myScroll.Minimum.ToString();
            textBox4.Text = myScroll.MarkerReserve.ToString();

            if (x > k)
            {
                x = 0;
                base.OnScroll(e);
            }
            
        }
    }
}
