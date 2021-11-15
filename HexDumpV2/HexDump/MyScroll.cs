using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HexDump
{
    public class MyScroll : VScrollBar
    {
        const int CoefficientStrings = 1;
        int limit = MyHexDump.LimitStock;
        int m_ScrollPos = 0;
        int m_IncrementClick = 0;
        int m_DecrementClick = 0;
        int m_MarkerReserve = 0;
        int m_PosString = 0;
        int m_ValueStringMax = 0;
        int ost;

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

        public int PosString
        {
            get { return m_PosString; }
            set { m_PosString = value; }
        }

        public int ValueStringMax { get => m_ValueStringMax; private set => m_ValueStringMax = value; }

        public MyScroll()
        { }

        public void ResetSettings()
        {
            Maximum = 0;
            Minimum = 0;
            m_ScrollPos = 0;
            m_PosString = 0;
            base.Value = 0;
            m_MarkerReserve = 0;
        }

        public void SetSettingScroll(string path)
        {
            
            using (var fileStream = File.OpenRead(path))
            {
                Maximum = (int)(fileStream.Length / 16);
                Maximum = Maximum / CoefficientStrings;
                Maximum -= LargeChange;
                //Maximum = Maximum * CoefficientStrings;
                m_ValueStringMax = Maximum - (LargeChange - 1);
                
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            if (se.NewValue > se.OldValue)
            {
;               if (m_PosString < m_ValueStringMax)
                {
                    m_PosString = se.NewValue;

                    base.Value = m_PosString / CoefficientStrings;

                    ost = Math.Abs(se.OldValue - se.NewValue);

                    m_MarkerReserve += ost;

                    se.NewValue = base.Value;
                }               
            }
            else if (se.NewValue < se.OldValue)
            {

                if (m_PosString > Minimum)
                {
                    m_PosString = se.NewValue;

                    base.Value = m_PosString / CoefficientStrings;

                    //m_ScrollPos = se.NewValue * CoefficientStrings + (int)Math.Ceiling(m_PosString % (decimal)CoefficientStrings);

                    ost = Math.Abs(se.OldValue - se.NewValue);

                    m_MarkerReserve -= ost;

                    se.NewValue = base.Value;
                }
            }

            base.OnScroll(se);
        }

        public void OnPressingControlButton(ScrollEventArgs se)
        {
            OnScroll(se);
        }

    }
}
