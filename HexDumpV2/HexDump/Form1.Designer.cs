
namespace HexDump
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.PathBox = new System.Windows.Forms.TextBox();
            this.MainHexBox = new System.Windows.Forms.TextBox();
            this.Search = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ScrollHexBox = new System.Windows.Forms.VScrollBar();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.myScroll = new HexDump.MyScroll();
            this.SuspendLayout();
            // 
            // PathBox
            // 
            this.PathBox.Location = new System.Drawing.Point(33, 31);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(360, 22);
            this.PathBox.TabIndex = 0;
            // 
            // MainHexBox
            // 
            this.MainHexBox.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainHexBox.Location = new System.Drawing.Point(33, 82);
            this.MainHexBox.Multiline = true;
            this.MainHexBox.Name = "MainHexBox";
            this.MainHexBox.Size = new System.Drawing.Size(546, 333);
            this.MainHexBox.TabIndex = 1;
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(399, 29);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(136, 27);
            this.Search.TabIndex = 2;
            this.Search.Text = "Обзор";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(688, 81);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(688, 393);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 4;
            // 
            // ScrollHexBox
            // 
            this.ScrollHexBox.Location = new System.Drawing.Point(582, 82);
            this.ScrollHexBox.Name = "ScrollHexBox";
            this.ScrollHexBox.Size = new System.Drawing.Size(21, 333);
            this.ScrollHexBox.TabIndex = 5;
            
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(33, 421);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 22);
            this.textBox3.TabIndex = 6;
            // 
            // myScroll
            // 
            this.myScroll.Location = new System.Drawing.Point(642, 82);
            this.myScroll.Name = "myScroll";
            this.myScroll.Size = new System.Drawing.Size(21, 333);
            this.myScroll.TabIndex = 7;
            this.myScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.myScroll_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.myScroll);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.ScrollHexBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.MainHexBox);
            this.Controls.Add(this.PathBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.TextBox MainHexBox;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.VScrollBar ScrollHexBox;
        private System.Windows.Forms.TextBox textBox3;
        private MyScroll myScroll;
    }
}

