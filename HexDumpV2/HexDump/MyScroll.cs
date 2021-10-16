using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexDump
{
    //this.ScrollHexBox.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollHexBox_Scroll);
    public class MyScroll : VScrollBar
    {
        VScrollBar vScroller = new VScrollBar();
        //public event ScrollEventHandler Scroll;

        int phantomScrollPos = 0;
        int x = 0;
        int k = 3;

        public MyScroll()
        { }

        public void SetSettingScroll(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                this.Maximum = (int)(fileStream.Length / 16);
                this.Maximum -= this.LargeChange - 1;
            }
        }

        /*public void Scrolling(ScrollEventArgs se)
        {
            
        }*/

        protected override void OnScroll(ScrollEventArgs se)
        {
            //ScrollEventHandler handler = Scroll;
           // if (handler != null)
            //{
              //  handler(this, se);
                x++;
            base.OnScroll(se);

            //}

            if (x > k)
            {
                x = 0;
                phantomScrollPos++;
            }
            se.NewValue = phantomScrollPos;
            this.Value = se.NewValue;
        }
    }
}
