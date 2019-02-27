﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPaint
{
    [Serializable]
    class Elipse :Step
    {
        public override void Draw_end()
        {
            if (flag_draw)
            {
                Graphics gr = Graphics.FromImage(bmp);
                gr.DrawEllipse(Pen, CreateRect(start, end));

                gr.Dispose();
            }
        }

        public override void Draw_move(MouseEventArgs e)
        {
            if (!flag_draw)
                flag_draw = true;
            pbx.Refresh();
            Graphics gr = pbx.CreateGraphics();
            end = new Point(e.X, e.Y);

            gr.DrawEllipse(Pen, CreateRect(start, end));
            gr.Dispose();
        }

        public override Step GetNewObj()
        {
            return new Elipse();
        }
    }
}