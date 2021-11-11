using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HexDump
{
    public class MyScroll : VScrollBar
    {
        const int CoefficientStrings = 3;

        int m_ScrollPos = 0;
        int m_IncrementClick = 0;
        int m_DecrementClick = 0;
        int m_MarkerReserve = 0;
        int m_ValueString = 0;
        int m_ValueStringMax = 0;


        public int GetCoefficientStrings
        {
            get => CoefficientStrings;
        }

        public int MarkerReserve
        {
            get { return m_MarkerReserve; }
            set { m_MarkerReserve = value; }
        }

        public int ScrollPos
        {
            get { return m_ScrollPos; }
            set { m_ScrollPos = value; }
        }

        public int ValueString
        {
            get { return m_ValueString; }
            set { m_ValueString = value; }
        }

        public int ValueStringMax { get => m_ValueStringMax; private set => m_ValueStringMax = value; }

        public MyScroll()
        { }

        public void ResetSettings()
        {
            Maximum = 0;
            Minimum = 0;
            m_ScrollPos = 0;
            m_ValueString = 0;
            base.Value = 0;
            m_MarkerReserve = 0;
        }

        public void SetSettingScroll(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                Maximum = (int)(fileStream.Length / 16);
                Maximum = Maximum / CoefficientStrings;
                Maximum -= LargeChange - 1;
                m_ValueStringMax = Maximum * CoefficientStrings + LargeChange - 1;
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            //Debug.WriteLine("se.NewValue = {0}" + '\n', se.NewValue);
            switch (se.Type)
            {
                case ScrollEventType.SmallDecrement:
                    OnSmallDecrement();
                    break;

                case ScrollEventType.SmallIncrement:
                    OnSmallIncrement();
                    break;

                case ScrollEventType.LargeDecrement:
                    OnLargeDecrement(se);
                    break;

                case ScrollEventType.LargeIncrement:
                    OnLargeIncrement(se);
                    break;

                case ScrollEventType.ThumbPosition:
                    Debug.WriteLine("Down Arrow Captured");
                    break;

                case ScrollEventType.ThumbTrack:

                    //Debug.WriteLine("ThumbTrack se.NewValue = {0}" + '\n', se.NewValue);
                    OnThumbTrack(se);
                    break;

                case ScrollEventType.First:
                    OnFirst();
                    //markerReserve = ValueString;
                    break;

                case ScrollEventType.Last:
                    OnLast();
                    //markerReserve = ValueString;
                    break;

                case ScrollEventType.EndScroll:
                    
                    break;

                default:
                    break;
            }
            /*Debug.WriteLine("Value OnScroll = {0}",Value);
            Debug.WriteLine("m_ScrollPos OnScroll = {0}", m_ScrollPos);*/
            base.OnScroll(se);
        }

        public void OnPressingControlButton(ScrollEventArgs se)
        {
            OnScroll(se);
        }

        private void OnFirst()
        {
            m_ScrollPos = Minimum;
            Value = m_ScrollPos;
            m_ValueString = Minimum;
            m_MarkerReserve = Minimum;
        }

        private void OnLast()
        {
            m_ScrollPos = Maximum;
            Value = m_ScrollPos;
            m_ValueString = m_ValueStringMax;
            m_MarkerReserve = m_ValueStringMax;
        }

        private void OnThumbTrack(ScrollEventArgs se)
        {
            if (se.NewValue > se.OldValue)
            {
                OnSmallIncrement();
            }
            else if (se.NewValue < se.OldValue)
            {
                OnSmallDecrement();
            }
            else
            {
                Debug.WriteLine("stay");
            }
            //Debug.WriteLine(" OnThumbTrack se.NewValue = {0}" + '\n', se.NewValue);
        }

        private void OnLargeIncrement(ScrollEventArgs se)
        {
            m_IncrementClick++;

            if (m_ValueString < m_ValueStringMax && m_ScrollPos < Maximum)
            {
                m_ScrollPos++;
                se.NewValue = m_ScrollPos;
                Value = se.NewValue;
                m_ValueString += CoefficientStrings;
                m_MarkerReserve += CoefficientStrings;
            }
            if (m_IncrementClick > CoefficientStrings)
            {
                m_IncrementClick = 0;
            }
        }

        private void OnLargeDecrement(ScrollEventArgs se)
        {
            m_DecrementClick++;

            if (m_ValueString > 0 && m_ScrollPos > 0)
            {
                m_ScrollPos--;
                //??
                se.NewValue = m_ScrollPos;
                Value = se.NewValue;
                m_ValueString -= CoefficientStrings;
                m_MarkerReserve -= CoefficientStrings;
            }
            if (m_DecrementClick > CoefficientStrings)
            {
                m_DecrementClick = 0;
            }
        }

        private void OnSmallIncrement()
        {
            m_IncrementClick++;

            m_DecrementClick = 0;

            if (m_ValueString < m_ValueStringMax)
            {
                m_ValueString++;
                m_MarkerReserve++;
            }
            if (m_ScrollPos < Maximum && (m_IncrementClick >= CoefficientStrings))
            {
                m_ScrollPos++;
                Value = m_ScrollPos;
                m_IncrementClick = 0;
            }
            //Debug.WriteLine("OnSmall Increm Value = {0}", Value);
            //Debug.WriteLine("m_ScrollPos  Increm = {0}", m_ScrollPos);
        }

        private void OnSmallDecrement()
        {
            m_DecrementClick++;

            m_IncrementClick = 0;

            if (m_ValueString > 0)
            {
                m_ValueString--;
                m_MarkerReserve--;
            }
            if (m_ScrollPos > 0 && (m_DecrementClick >= CoefficientStrings))
            {
                m_ScrollPos--;
                Value = m_ScrollPos;
                m_DecrementClick = 0;
            }
            /*Debug.WriteLine("OnSmall Decreme Value = {0}", Value);
            Debug.WriteLine("m_ScrollPos Decreme = {0}", m_ScrollPos);*/
        }
    }
}
