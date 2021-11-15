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
        const int ByteInLine = 16;*/
        int count_strings_in_MainHexBox;
        string[] allArrByte;
        int CountElem = 0;
        int printingString;
        bool reading = true;
        
        
        


        public Form1()
        {
            InitializeComponent();
            textBox3.Text = MainHexBox.Height.ToString();
            BoxCountElem.Text = CountElem.ToString();   
            count_strings_in_MainHexBox = (MainHexBox.Height / MainHexBox.Font.Height);
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

                //allArrByte = hex.GetHexDump(myScroll.PosString, PathBox.Text, myScroll.MarkerReserve);

                PrintHex();
                reading = false;
            }
        }

        private void PrintHex()
        {
            printingString = myScroll.Value;

            if (myScroll.MarkerReserve == MyHexDump.LimitStock)
            {
                printingString = 0;
                reading = true;
                myScroll.MarkerReserve = 0;

            }


            allArrByte = hex.GetHexDump(myScroll.PosString, PathBox.Text, reading);
            reading = false;

            MainHexBox.Clear();
             
            int j = 0;
            for (int i = printingString; j < count_strings_in_MainHexBox && i < allArrByte.Length; j++)
            {
                m_StrBldForm.Append(allArrByte[i++]);
            }

            MainHexBox.Text = m_StrBldForm.ToString();

            m_StrBldForm.Clear();
        }

        private void MyScroll_Scroll(object sender, ScrollEventArgs e)
        {
            
            PrintHex();

            textBox1.Text = myScroll.Maximum.ToString();
            textBox2.Text = myScroll.PosString.ToString();
            textBox6.Text = myScroll.Value.ToString();
            textBox4.Text = myScroll.MarkerReserve.ToString();
        }

        internal static class NativeMethods
        {
            internal static ushort HIWORD(IntPtr dwValue)
            {
                return (ushort)((((long)dwValue) >> 0x10) & 0xffff);
            }

            internal static ushort HIWORD(uint dwValue)
            {
                return (ushort)(dwValue >> 0x10);
            }

            internal static int GET_WHEEL_DELTA_WPARAM(IntPtr wParam)
            {
                return (short)HIWORD(wParam);
            }

            internal static int GET_WHEEL_DELTA_WPARAM(uint wParam)
            {
                return (short)HIWORD(wParam);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            
            if (m.Msg == WmMouseWheel)
            {
                int zDelta = NativeMethods.GET_WHEEL_DELTA_WPARAM(m.WParam);
                ScrollEventArgs se;

                if (zDelta > 0)
                {
                    se =  new ScrollEventArgs(ScrollEventType.ThumbTrack, myScroll.PosString, myScroll.PosString - 1, ScrollOrientation.VerticalScroll);
                }
                else
                {
                    se = new ScrollEventArgs(ScrollEventType.ThumbTrack, myScroll.PosString, myScroll.PosString + 1, ScrollOrientation.VerticalScroll);
                }
                myScroll.OnPressingControlButton(se);
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
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.SmallIncrement, myScroll.PosString, 
                            myScroll.PosString + 1, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.Up:
                        Debug.WriteLine("Up Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.SmallDecrement, myScroll.PosString, 
                            myScroll.PosString - 1, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.PageDown:
                        Debug.WriteLine("PageDown Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.LargeIncrement, myScroll.PosString, 
                            myScroll.PosString + 3, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.PageUp:
                        Debug.WriteLine("PageUp Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.LargeDecrement, myScroll.PosString, 
                            myScroll.PosString - 3, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.Home:
                        Debug.WriteLine("Home Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.First, 1, myScroll.Minimum, 
                            ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.End:
                        Debug.WriteLine("End Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.Last, 
                            myScroll.Maximum - (myScroll.LargeChange - 1), ScrollOrientation.VerticalScroll));
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void IncrementElem_Click(object sender, EventArgs e)
        {
            BoxCountElem.Text = (Convert.ToInt32(BoxCountElem.Text) + 1).ToString();
            Button n = (Button)sender;
            Button temp = new Button();
            temp.Width = n.Width;
            temp.Name = string.Format("newTextBox{0}", BoxCountElem.Text);
            temp.Location = new Point(n.Location.X + n.Width + 10, n.Location.Y);
            //temp.Click += new EventHandler(IncrementElem_Click);
            Controls.Add(temp);           
        }

        private void DecrementElem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(BoxCountElem.Text) > 0)
            {
                Controls.RemoveByKey(string.Format("newTextBox{0}", BoxCountElem.Text));
                BoxCountElem.Text = (Convert.ToInt32(BoxCountElem.Text) - 1).ToString();
            }
        }
    }
}
