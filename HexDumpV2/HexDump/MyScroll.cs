using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int scrollPos = 0;
        int incrementClick = 0;
        int decrementClick = 0;
        int k = 3;
        int valueString = 0;


        public MyScroll()
        { }

        public int ScrollPos
        {
            get { return scrollPos; }
            set { scrollPos = value; }
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
            ScrollPos = 0;
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

            switch (se.Type)
            {
                case ScrollEventType.SmallDecrement:
                    scrollUp = false;
                    decrementClick++;
                    incrementClick = 0;

                    if (ValueString > 0)
                    {
                        ValueString--;
                    }
                    if (ScrollPos > 0 && (decrementClick > k))
                    {
                        ScrollPos--;
                        decrementClick = 0;
                    }
                    break;

                case ScrollEventType.SmallIncrement:
                    scrollUp = true;
                    incrementClick++;
                    decrementClick = 0;

                    if (ValueString < (Maximum * k + LargeChange - 1))// пока решил проблему максимума так
                    {
                        ValueString++;
                    }
                    if (ScrollPos < Maximum && (incrementClick > k))
                    {
                        ScrollPos++;
                        incrementClick = 0;
                    }
                    break;

                case ScrollEventType.LargeDecrement:
                    scrollUp = false;
                    decrementClick++;
                    //incrementClick = 0;

                    if (ValueString > 0 && ScrollPos > 0)
                    {
                        ScrollPos--;
                        ValueString -= k;
                    }
                    /*if (ScrollPos > 0 && (decrementClick > k))
                    {
                        ScrollPos--;
                        decrementClick = 0;
                    }*/
                    break;

                case ScrollEventType.LargeIncrement:
                    scrollUp = true;
                    incrementClick++;
                    //decrementClick = 0;

                    if (ValueString < (Maximum * k + LargeChange - 1) && ScrollPos < Maximum)// пока решил проблему максимума так
                    {
                        ScrollPos++;
                        ValueString += k;
                    }
                    /*if (ScrollPos < Maximum && (incrementClick > k))
                    {
                        ScrollPos++;
                        incrementClick = 0;
                    }*/
                    break;

                case ScrollEventType.ThumbPosition:
                    
                    break;
                case ScrollEventType.ThumbTrack://доделать 
                    if (se.NewValue > se.OldValue)
                    {
                        ValueString++;
                        Value++;
                    }
                    else if (se.NewValue < se.OldValue)
                    {
                        ValueString--;
                        Value--;
                    }
                    else
                    {
                        Debug.WriteLine("stay");
                    }

                    break;

                case ScrollEventType.First:
                    break;

                case ScrollEventType.Last:
                    break;

                case ScrollEventType.EndScroll:
                    break;

                default:
                    break;
            }

            /*if (se.Type == ScrollEventType.EndScroll)
            {
                if (scrollUp && (incrementClick > k))
                {
                    if (ScrollPos < Maximum)
                    {
                        ScrollPos++;
                    }
                    incrementClick = 0;
                }
                else if (!scrollUp && (decrementClick > k))
                {
                    if (ScrollPos > 0)
                    {
                        ScrollPos--;
                    }
                    decrementClick = 0;
                }
            }
            else if (se.Type == ScrollEventType.SmallDecrement || se.Type == ScrollEventType.LargeDecrement)
            {
                scrollUp = false;
                decrementClick++;
                incrementClick = 0;

                if (ValueString > 0)
                {
                    ValueString--;
                }
            }
            else if (se.Type == ScrollEventType.SmallIncrement || se.Type == ScrollEventType.LargeIncrement)
            {
                scrollUp = true;
                incrementClick++;
                decrementClick = 0;

                if (ValueString < (Maximum * k + LargeChange - 1))// пока решил проблему максимума так
                {
                    ValueString++;
                }
            }
            */
            base.OnScroll(se);
            se.NewValue = ScrollPos;
        }
    }
}
