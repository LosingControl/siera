using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexDump
{
    public class MyScroll : VScrollBar
    {
        //VScrollBar vScroller = new VScrollBar();
        //public event ScrollEventHandler Scroll;

        bool scrollUp = true;
        int ScrollPos = 0;
        int incrementClick = 0;
        int decrementClick = 0;
        int k = 3;
        int valueString = 0;

        public MyScroll()
        { }

        public new int Value
        {
            get { return ScrollPos; }
            set { ScrollPos = value; }
        }

        public int ValueString 
        {
            get { return valueString; }
            set { valueString = value; }
        }

        public void ResetSettings()
        {
            Maximum = 100;
            Minimum = 0;
            Value = 0;
            ValueString = 0;
            scrollUp = true;
        }

        public void SetSettingScroll(string path, int maxLineInBox)
        {
            using (var fileStream = File.OpenRead(path))
            {
                Maximum = (int)(fileStream.Length / 16);
                Maximum /= k;
                Maximum -= LargeChange - 1;
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            if (se.Type == ScrollEventType.EndScroll)
            {
                if (scrollUp && (incrementClick > k))
                {
                    if (Value < Maximum)
                    {
                        Value++;
                        //valueLine += x;
                    }
                    incrementClick = 0;
                }
                else if (!scrollUp && (decrementClick > k))
                {
                    if (Value > 0)
                    {
                        Value--;
                        //valueLine -= x;
                    }
                    decrementClick = 0;
                }
            }
            else if (se.Type == ScrollEventType.SmallDecrement)
            {
                scrollUp = false;
                decrementClick++;
                incrementClick = 0;
                if (ValueString > 0)
                {
                    ValueString--;
                }
            }
            else if (se.Type == ScrollEventType.SmallIncrement)
            {
                scrollUp = true;
                incrementClick++;
                decrementClick = 0;
                /*if (Value == 6)
                    System.Diagnostics.Debugger.Break();*/
                if (ValueString < (Maximum * k + LargeChange - 1))// пока решил проблему максимума так
                {
                    ValueString++;
                }
            }          
            base.OnScroll(se);
            se.NewValue = Value;
        }
    }
}
