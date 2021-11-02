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
        ScrollEventArgs MyOldValue;

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
                Maximum /= CoefficientStrings;
                Maximum -= LargeChange - 1;
                m_ValueStringMax = Maximum * CoefficientStrings + LargeChange - 1;
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
                    Debug.WriteLine("Down Arrow Captured");
                    break;
                case ScrollEventType.ThumbTrack:
                    //Ползунок полосы прокрутки перемещается в данный момент.
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
            se.NewValue = m_ScrollPos;
            base.OnScroll(se);
        }

        public void OnScrolled(ScrollEventArgs se)
        {
            OnScroll(se);
        }

        public void OnPressingControlButton(ScrollEventArgs se)
        {
            OnScroll(se);
        }

        private void OnFirst()
        {
            m_ScrollPos = Minimum;
            m_ValueString = Minimum;
            m_MarkerReserve = Minimum;
        }

        private void OnLast()
        {
            m_ScrollPos = Maximum;
            m_ValueString = m_ValueStringMax;
            m_MarkerReserve = m_ValueStringMax;
        }

        private void OnThumbTrack(ScrollEventArgs se)
        {
            if (se.NewValue > se.OldValue)
            {
                m_ScrollPos++;
                m_ValueString += CoefficientStrings;
                m_MarkerReserve += CoefficientStrings;
            }
            else if (se.NewValue < se.OldValue)
            {
                m_ScrollPos--;
                m_ValueString -= CoefficientStrings;
                m_MarkerReserve -= CoefficientStrings;
            }
            else
            {
                Debug.WriteLine("stay");
            }
        }

        private void OnLargeIncrement()
        {
            m_IncrementClick++;

            if (m_ValueString < m_ValueStringMax && m_ScrollPos < Maximum)
            {
                m_ScrollPos++;
                m_ValueString += CoefficientStrings;
                m_MarkerReserve += CoefficientStrings;
            }
            if (m_IncrementClick > CoefficientStrings)
            {
                m_IncrementClick = 0;
            }
        }

        private void OnLargeDecrement()
        {
            m_DecrementClick++;

            if (m_ValueString > 0 && m_ScrollPos > 0)
            {
                m_ScrollPos--;
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
                m_IncrementClick = 0;
            }
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
                m_DecrementClick = 0;
            }
        }
    }
}
