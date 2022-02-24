using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;


namespace HexDump
{
    public partial class Form1 : Form
    {
        const int WmKeyDown = 0x100;
        const int WmSysKeyDown = 0x0104;
        const int WmMouseWheel = 0x020A;

        readonly OpenFileDialog openFileDialog = new OpenFileDialog();
        readonly MyHexDump hex = new MyHexDump { };

        int count_strings_in_MainHexBox;
        string[] allArrByte;
        int CountElem = 0;
        bool printing = true;


        public Form1()
        {
            InitializeComponent();
            textBox3.Text = MainHexBox.Height.ToString();

            BoxCountElem.Text = CountElem.ToString();   

            count_strings_in_MainHexBox = (MainHexBox.Height / MainHexBox.Font.Height);

            myScroll.CountStringsInMainHexBox = count_strings_in_MainHexBox;
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MainHexBox.Clear();

                PathBox.Clear();
                PathBox.Text = openFileDialog.FileName;

                myScroll.ResetSettings();
                myScroll.SetSettingScroll(PathBox.Text);

                allArrByte = hex.GetHexDump(myScroll.PosString, PathBox.Text, myScroll);

                PrintHex();
            }
        }

        private void PrintHex()
        {
            CheckingForRereadingArray();

            MainHexBox.Clear();

            int j = 0;
            for (long i = myScroll.MarkerReserve; j < count_strings_in_MainHexBox && i < allArrByte.Length; j++)
            {
                MainHexBox.Text += allArrByte[i++];
            }

            textBox5.Text = myScroll.Value.ToString();
        }

        private void CheckingForRereadingArray()
        {
            if (myScroll.PosString > hex.StockBottom || (myScroll.PosString < hex.StockTop))
            {
                
                allArrByte = hex.GetHexDump(myScroll.PosString, PathBox.Text, myScroll);

                if (myScroll.PosString == myScroll.Minimum)
                {
                    myScroll.MarkerReserve = 0;
                }
                else if (myScroll.PosString == myScroll.Maximum)
                {
                    myScroll.MarkerReserve = hex.GetLimitStock / 2;
                }
                else
                {
                    myScroll.MarkerReserve = (allArrByte.Length / 4);
                }
            }
        }

        private void MyScroll_Scroll(object sender, ScrollEventArgs e)
        {
            if (printing || e.NewValue < myScroll.Maximum)
            {
                PrintHex();
            }

            printing = e.NewValue == myScroll.Maximum ? false : true;

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
                    se =  new ScrollEventArgs(ScrollEventType.ThumbTrack, myScroll.Value, myScroll.Value - 1, ScrollOrientation.VerticalScroll);
                }
                else
                {
                    se = new ScrollEventArgs(ScrollEventType.ThumbTrack, myScroll.Value, myScroll.Value + 1, ScrollOrientation.VerticalScroll);
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
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.SmallIncrement,
                            myScroll.Value, myScroll.Value + 1, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.Up:
                        Debug.WriteLine("Up Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.SmallDecrement,
                            myScroll.Value, myScroll.Value - 1, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.PageDown:
                        Debug.WriteLine("PageDown Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.LargeIncrement,
                            myScroll.Value, myScroll.Value + 3, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.PageUp:
                        Debug.WriteLine("PageUp Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.LargeDecrement,
                            myScroll.Value, myScroll.Value - 3, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.Home:
                        Debug.WriteLine("Home Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.First,
                            myScroll.Value, myScroll.Minimum, ScrollOrientation.VerticalScroll));
                        return true;

                    case Keys.End:
                    case Keys.Back:
                        Debug.WriteLine("End Arrow Captured");
                        myScroll.OnPressingControlButton(new ScrollEventArgs(ScrollEventType.Last, 
                            myScroll.Value, myScroll.Maximum, ScrollOrientation.VerticalScroll));
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
