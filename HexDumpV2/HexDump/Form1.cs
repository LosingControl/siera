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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HexDump
{
    public partial class Form1 : Form
    {
        const int WmKeyDown = 0x100;
        const int WmSysKeyDown = 0x0104;
        const int WmPageDown = 0x22;
        const int WmPageUp = 0x21;
        const int WmEnd = 0x23;
        const int WmHome = 0x24;
        const int WmUp = 0x26;
        const int WmDown = 0x28;
        const int WmVScroll = 0x115;
        const int WmMouseWheel = 0x020A;

        readonly OpenFileDialog openFileDialog = new OpenFileDialog();
        readonly MyHexDump hex = new MyHexDump { };
        readonly StringBuilder m_StrBldForm = new StringBuilder();

        /*const int reserveLimit = 20;
        const int ByteInLine = 16;
        int count_strings_in_MainHexBox;*/
        string[] allArrByte;
        int m_CountClickOnScroll = 0;
        

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

                MainHexBox.Clear();

                PathBox.Clear();
                PathBox.Text = openFileDialog.FileName;

                myScroll.ResetSettings();
                myScroll.SetSettingScroll(PathBox.Text);

                allArrByte = hex.GetHexDump(myScroll.ValueString, PathBox.Text, myScroll.MarkerReserve);

                PrintHex();
            }
        }

        private void PrintHex()
        {
            MainHexBox.Clear();

            allArrByte = hex.GetHexDump(myScroll.ValueString, PathBox.Text, myScroll.MarkerReserve);

            for (int i = myScroll.ValueString; i < allArrByte.Length; i++)
            {
                m_StrBldForm.Append(allArrByte[i]);
                //MainHexBox.AppendText(allArrByte[i]);
            }

            MainHexBox.Text = m_StrBldForm.ToString();

            m_StrBldForm.Clear();
        }

        private void MyScroll_Scroll(object sender, ScrollEventArgs e)
        {
            PrintHex();

            m_CountClickOnScroll++;
            textBox1.Text = myScroll.Maximum.ToString();
            textBox2.Text = myScroll.ValueString.ToString();
            textBox5.Text = myScroll.ScrollPos.ToString();
            textBox6.Text = myScroll.Minimum.ToString();
            textBox4.Text = myScroll.MarkerReserve.ToString();

            if (m_CountClickOnScroll > myScroll.GetCoefficientStrings)
            {
                m_CountClickOnScroll = 0;
                //base.OnScroll(e);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WmMouseWheel)
            {
                //ScrollEventArgs se = new ScrollEventArgs(ScrollEventType.ThumbTrack, myScroll.OldValue, );
                Debug.WriteLine("MouseWheel");
                //myScroll.OnScrolled(se);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WmKeyDown || msg.Msg == WmSysKeyDown)
            {
                switch (keyData)
                {
                    case Keys.Down:                 
                        Debug.WriteLine("Down Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.SmallIncrement, 1));
                        return true;

                    case Keys.Up:
                        Debug.WriteLine("Up Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.SmallDecrement, 1));
                        return true;

                    case Keys.PageDown:
                        Debug.WriteLine("PageDown Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.LargeIncrement, myScroll.GetCoefficientStrings));
                        return true;

                    case Keys.PageUp:
                        Debug.WriteLine("PageUp Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.LargeDecrement, myScroll.GetCoefficientStrings));
                        return true;

                    case Keys.Home:
                        Debug.WriteLine("Home Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.First, myScroll.Minimum));
                        return true;

                    case Keys.End:
                        Debug.WriteLine("End Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.Last, myScroll.Maximum));
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
