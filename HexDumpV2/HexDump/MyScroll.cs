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
        readonly OpenFileDialog openFileDialog;

        public MyScroll(OpenFileDialog openFileDialog)
        {
            this.openFileDialog = openFileDialog;
        }

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

        public void SetSettingScroll()
        {
            using (var fileStream = File.OpenRead(openFileDialog.FileName))
            {
                this.Maximum = (int)(fileStream.Length / 16);
                this.Maximum = this.LargeChange - 1;
            }
        }
    }
}
