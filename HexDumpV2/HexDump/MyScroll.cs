using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexDump
{
    class MyScroll : VScrollBar
    {
        VScrollBar vScroller = new VScrollBar();
        
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            if (se.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                this.Value = se.NewValue;
            }
        }

        public void Scrolling(ScrollEventArgs se)
        {
            OnScroll(se);
        }

        public void SetSettingScroll(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                this.Maximum = (int)(fileStream.Length / 16);
                this.Maximum = this.LargeChange - 1;
            }
        }
    }
}
