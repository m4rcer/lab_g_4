using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace lab_g_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string getNthOrderDragonCurve(int n)
        {
            if (n <= 0)
                return "";

            var builder = new StringBuilder("1");

            for (int i = 1; i < n; i++)
            {
                builder.Append("1");
                var prevOrder = new StringBuilder(builder.ToString());
                prevOrder.Remove(prevOrder.Length - 1, 1);
                prevOrder[
                    (int)Math.Ceiling(
                        ((double)prevOrder.Length / 2) - 1)
                    ] = '0';
                builder.Append(prevOrder);
            }

            return builder.ToString();
        }

        private void drawBtn_Click(object sender, EventArgs e)
        {
            var order = 5;
            if(orderInput.Text != "")
                order = int.Parse(orderInput.Text);

            var pen = new Pen(Color.Black, 6);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            var tailPen = new Pen(Color.Black, 6);
            tailPen.CompoundArray = new float[] { 0.0f, 0.1f, 0.5f, 1.0f };
            var g = pictureBox.CreateGraphics();


            int dx = 40;

            string str = getNthOrderDragonCurve(order);

            int x1 = pictureBox.Size.Width / 2;
            int y1 = pictureBox.Size.Height / 2;

            int x2 = pictureBox.Size.Width / 2;
            int y2 = pictureBox.Size.Height / 2 - dx;

            int x3 = x2; int y3 = y2;

            g.DrawLine(pen, x1, y1, x3, y3);

            for (int i = 0; i < str.Length; i++)
            {

                if (y2 - y1 < 0)
                { 
                    if (str[i] == '1') x3 = x2 - dx; 
                    else x3 = x2 + dx; 
                    y3 = y2;
                }
                if (x2 - x1 < 0)
                { 
                    if (str[i] == '1') y3 = y2 - dx; 
                    else y3 = y2 + dx; 
                    x3 = x2;
                }
                if (x2 - x1 > 0)
                { 
                    if (str[i] == '1') y3 = y2 + dx; 
                    else y3 = y2 - dx; 
                    x3 = x2;
                }
                if (y2 - y1 > 0)
                { 
                    if (str[i] == '1') x3 = x2 + dx; 
                    else x3 = x2 - dx;
                    y3 = y2;
                }
                if (i == str.Length - 1)
                {
                    g.DrawLine(tailPen, x2, y2, x3, y3);
                }
                else
                { 
                    g.DrawLine(pen, x2, y2, x3, y3);
                }
                
                x1 = x2; y1 = y2;
                x2 = x3; y2 = y3;

            }

        pen.Dispose();
            g.Dispose();
        }
    }
}
