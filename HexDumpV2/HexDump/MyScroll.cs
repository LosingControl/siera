using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HexDump
{
    public class MyScroll : VScrollBar
    {
        int m_CoefficientStrings = 1;
        int m_countStringsInMainHexBox;
        long m_MarkerReserve = 0;
        long m_PosString = 0;
        int m_ValueStringMax = 0;
        long m_Step = 0;
        double m_Residue;

        public int GetCoefficientStrings
        {
            get => m_CoefficientStrings;
        }

        public long MarkerReserve
        {
            get { return m_MarkerReserve; }
            set { m_MarkerReserve = value; }
        }

        public long PosString
        {
            get { return m_PosString; }
            set { m_PosString = value; }
        }

        public int ValueStringMax { get => m_ValueStringMax; private set => m_ValueStringMax = value; }

        public int CountStringsInMainHexBox { private get => m_countStringsInMainHexBox; set => m_countStringsInMainHexBox = value; }

        public MyScroll()
        { }

        public void ResetSettings()
        {
            Maximum = 0;
            Minimum = 0;
            m_PosString = 0;
            m_Step = 0;
            base.Value = 0;
            m_MarkerReserve = 0;
        }

        public void SetSettingScroll(string path)
        {
            m_Residue = m_CoefficientStrings == 1 ? m_Residue = 0 : m_Residue = 1;

            using (var fileStream = File.OpenRead(path))
            {
                if ((Math.Ceiling(fileStream.Length / (decimal)16) > Int32.MaxValue))
                {
                    m_CoefficientStrings = (int)Math.Ceiling((decimal)fileStream.Length / Int32.MaxValue);
                }
                Maximum = ((int)Math.Ceiling(fileStream.Length / (decimal)16)) / m_CoefficientStrings;
                LargeChange = 1;
                Maximum -= m_countStringsInMainHexBox;
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            if (se.NewValue > se.OldValue)
            {
                long DifferenceNewOldMeaning = Math.Abs(se.OldValue - se.NewValue);

                if (se.NewValue <= Maximum)
                {
                    m_Residue += DifferenceNewOldMeaning;

                    m_Step += DifferenceNewOldMeaning;

                    if (m_Step >= m_CoefficientStrings)
                    {
                        base.Value += (int)(m_Residue / m_CoefficientStrings);
                        m_Residue %= m_CoefficientStrings;
                        m_Step = 0;
                    }

                    m_PosString = se.NewValue;
                }

                if (m_MarkerReserve < Maximum && Value < Maximum)
                {
                    m_MarkerReserve += DifferenceNewOldMeaning;
                }
                /*if (se.NewValue <= Maximum)
                {
                    m_PosString = se.NewValue;

                    //ost = (int)Math.Ceiling(m_PosString % (decimal)CoefficientStrings);

                    base.Value = se.NewValue / CoefficientStrings;

                    m_MarkerReserve += step;

                    se.NewValue = base.Value;

                }*/

            }
            else if (se.NewValue < se.OldValue)
            {
                long DifferenceNewOldMeaning = Math.Abs(se.OldValue - se.NewValue);

                if (se.NewValue >= Minimum)
                {
                    m_Residue += DifferenceNewOldMeaning;

                    m_Step += DifferenceNewOldMeaning;

                    if (m_Step >= m_CoefficientStrings)
                    {
                        base.Value -= (int)(m_Residue / m_CoefficientStrings);
                        m_Residue %= m_CoefficientStrings;
                        m_Step = 0;
                    }

                    m_PosString = se.NewValue;
                }

                if (m_MarkerReserve > Minimum)
                {
                    m_MarkerReserve -= DifferenceNewOldMeaning;
                }
                
                /* if (se.NewValue >= Minimum)
                 {
                     m_PosString = se.NewValue;

                     base.Value = se.NewValue / CoefficientStrings;

                     //ost = (int)Math.Ceiling(m_PosString % (decimal)CoefficientStrings);

                     m_MarkerReserve -= step;

                     se.NewValue = base.Value;

                 }*/

            }
            
            se.NewValue = base.Value;
            base.OnScroll(se);
        }

        public void OnPressingControlButton(ScrollEventArgs se)
        {
            OnScroll(se);
        }

    }
}
