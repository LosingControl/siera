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
        const int RowRatio = 3;

        int scrollPos = 0;
        int incrementClick = 0;
        int decrementClick = 0;
        int markerReserve = 0;
        int valueString = 0;


        public MyScroll()
        { }

        public int MarkerReserve
        {
            get { return markerReserve; }
            set { markerReserve = value; }
        }

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
            Maximum = 0;
            Minimum = 0;
            ScrollPos = 0;
            ValueString = 0;
            Value = 0;
            MarkerReserve = 0;
        }

        public void SetSettingScroll(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                Maximum = (int)(fileStream.Length / 16);
                Maximum /= RowRatio;
                Maximum -= LargeChange - 1;
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {

            switch (se.Type)
            {
                case ScrollEventType.SmallDecrement:
                    OnSmallDecrement();
                    break;

                case ScrollEventType.SmallIncrement:
                    OnSmallIncrement();
                    break;

                case ScrollEventType.LargeDecrement:
                    OnLargeDecrement();
                    break;

                case ScrollEventType.LargeIncrement:
                    OnLargeIncrement();
                    break;

                case ScrollEventType.ThumbPosition:
                    //Скролит после остановки Thumb. Ползунок полосы прокрутки переместился.

                    break;
                case ScrollEventType.ThumbTrack:
                    //Ползунок полосы прокрутки перемещается в данный момент.
                    OnThumbTrack(se);
                    break;

                case ScrollEventType.First:
                    ScrollPos = Maximum;
                    ValueString = Maximum * RowRatio + LargeChange - 1;
                    //markerReserve = ValueString;
                    break;

                case ScrollEventType.Last:
                    ScrollPos = Minimum;
                    ValueString = Minimum;
                    //markerReserve = ValueString;
                    break;

                case ScrollEventType.EndScroll:
                    break;

                default:
                    break;
            }
            base.OnScroll(se);
        }

        private void OnThumbTrack(ScrollEventArgs se)
        {
            if (se.NewValue > se.OldValue)
            {
                ScrollPos++;
                ValueString += RowRatio;
                markerReserve += RowRatio;
            }
            else if (se.NewValue < se.OldValue)
            {
                ScrollPos--;
                ValueString -= RowRatio;
                markerReserve -= RowRatio;
            }
            else
            {
                Debug.WriteLine("stay");
            }
        }

        private void OnLargeIncrement()
        {
            incrementClick++;

            if (ValueString < (Maximum * RowRatio + LargeChange - 1) && ScrollPos < Maximum)
            {
                ScrollPos++;
                ValueString += RowRatio;
                markerReserve += RowRatio;
            }
            if (ScrollPos < Maximum && (incrementClick > RowRatio))
            {
                ScrollPos++;
                incrementClick = 0;
            }
        }

        private void OnLargeDecrement()
        {
            decrementClick++;

            if (ValueString > 0 && ScrollPos > 0)
            {
                ScrollPos--;
                ValueString -= RowRatio;
                markerReserve -= RowRatio;
            }
            if (ScrollPos > 0 && (decrementClick > RowRatio))
            {
                ScrollPos--;
                decrementClick = 0;
            }
        }

        private void OnSmallIncrement()
        {
            incrementClick++;

            decrementClick = 0;

            if (ValueString < (Maximum * RowRatio + LargeChange - 1))
            {
                ValueString++;
                markerReserve++;
            }
            if (ScrollPos < Maximum && (incrementClick > RowRatio))
            {
                ScrollPos++;
                incrementClick = 0;
            }
        }

        private void OnSmallDecrement()
        {
            decrementClick++;

            incrementClick = 0;

            if (ValueString > 0)
            {
                ValueString--;
                markerReserve--;
            }
            if (ScrollPos > 0 && (decrementClick > RowRatio))
            {
                ScrollPos--;
                decrementClick = 0;
            }
        }
    }
}
