
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
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.BoxCountElem = new System.Windows.Forms.TextBox();
            this.IncrementElem = new System.Windows.Forms.Button();
            this.DecrementElem = new System.Windows.Forms.Button();
            this.myScroll = new HexDump.MyScroll();
            this.SuspendLayout();
            // 
            // PathBox
            // 
            this.PathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathBox.Location = new System.Drawing.Point(25, 25);
            this.PathBox.Margin = new System.Windows.Forms.Padding(2);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(271, 20);
            this.PathBox.TabIndex = 0;
            // 
            // MainHexBox
            // 
            this.MainHexBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainHexBox.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainHexBox.Location = new System.Drawing.Point(25, 67);
            this.MainHexBox.Margin = new System.Windows.Forms.Padding(2);
            this.MainHexBox.Multiline = true;
            this.MainHexBox.Name = "MainHexBox";
            this.MainHexBox.Size = new System.Drawing.Size(410, 271);
            this.MainHexBox.TabIndex = 1;
            // 
            // Search
            // 
            this.Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search.Location = new System.Drawing.Point(299, 24);
            this.Search.Margin = new System.Windows.Forms.Padding(2);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(102, 22);
            this.Search.TabIndex = 2;
            this.Search.Text = "Обзор";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(516, 66);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(516, 319);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(76, 20);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(25, 342);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(76, 20);
            this.textBox3.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(516, 220);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(76, 20);
            this.textBox4.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(534, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "MyValue";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(534, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Value";
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(506, 111);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(86, 20);
            this.textBox5.TabIndex = 11;
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(506, 147);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(86, 20);
            this.textBox6.TabIndex = 12;
            // 
            // BoxCountElem
            // 
            this.BoxCountElem.Location = new System.Drawing.Point(457, 24);
            this.BoxCountElem.Name = "BoxCountElem";
            this.BoxCountElem.Size = new System.Drawing.Size(46, 20);
            this.BoxCountElem.TabIndex = 13;
            // 
            // IncrementElem
            // 
            this.IncrementElem.Location = new System.Drawing.Point(509, 23);
            this.IncrementElem.Name = "IncrementElem";
            this.IncrementElem.Size = new System.Drawing.Size(31, 23);
            this.IncrementElem.TabIndex = 14;
            this.IncrementElem.Text = "+";
            this.IncrementElem.UseVisualStyleBackColor = true;
            this.IncrementElem.Click += new System.EventHandler(this.IncrementElem_Click);
            // 
            // DecrementElem
            // 
            this.DecrementElem.Location = new System.Drawing.Point(420, 23);
            this.DecrementElem.Name = "DecrementElem";
            this.DecrementElem.Size = new System.Drawing.Size(31, 23);
            this.DecrementElem.TabIndex = 15;
            this.DecrementElem.Text = "-";
            this.DecrementElem.UseVisualStyleBackColor = true;
            this.DecrementElem.Click += new System.EventHandler(this.DecrementElem_Click);
            // 
            // myScroll
            // 
            this.myScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myScroll.Location = new System.Drawing.Point(482, 67);
            this.myScroll.MarkerReserve = 0;
            this.myScroll.Name = "myScroll";
            this.myScroll.ScrollPos = 0;
            this.myScroll.Size = new System.Drawing.Size(21, 271);
            this.myScroll.TabIndex = 7;
            this.myScroll.ValueString = 0;
            this.myScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MyScroll_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.DecrementElem);
            this.Controls.Add(this.IncrementElem);
            this.Controls.Add(this.BoxCountElem);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.myScroll);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.MainHexBox);
            this.Controls.Add(this.PathBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(616, 405);
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
        private System.Windows.Forms.TextBox textBox3;
        private MyScroll myScroll;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox BoxCountElem;
        private System.Windows.Forms.Button IncrementElem;
        private System.Windows.Forms.Button DecrementElem;
    }
}

