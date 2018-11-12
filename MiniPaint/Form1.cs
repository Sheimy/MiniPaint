﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Form1 : Form
    {
        bool FlagText = false;
        bool flagTextEndEnter = false;
        bool FlagFill = false;
        TextBox textb; 
        


        public Form1()
        {
            InitializeComponent();
            buffer = new Buffer(pictureBox1, new Pen(leftChoiceBTN.BackColor, 1.5f), RightChoiceBTN.BackColor);
            buffer.ChangeStack += Buffer_ChangeStack;
            textb = new TextBox();
            textb.BackColor = SystemColors.Control; 

            textb.Visible = false;
            pictureBox1.Controls.Add(textb);
            DoubleBuffered = true;
        }

        private void Buffer_ChangeStack(int stack_count, int current)
        {
            if (stack_count == 0)
                MenuDo.Enabled = MenuUndo.Enabled = false;
            else if (current == stack_count-1)
            {
                MenuUndo.Enabled = true;
                MenuDo.Enabled = false;
            }
            else if (current==-1)
            {
                MenuDo.Enabled = true;
                MenuUndo.Enabled = false;
            }
            else if (current < stack_count-1)
                MenuDo.Enabled = MenuUndo.Enabled = true;
        }

        private void ButtonColors_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            ChangeColour(colorDialog1.Color);

        }

        private void LineToolsBTN_Click(object sender, EventArgs e)
        {
            if (flagTextEndEnter)
                buffer.MouseUp((MouseEventArgs)e); // проверить, как это работает
            clickFigure();
            buffer.Selected_step_init(new Line()); /////// спросить, норм ли это
        }

        private void ColorBTN1_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.White);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!FlagText && !FlagFill)
                buffer.MouseDown(e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!FlagText && !FlagFill)
                buffer.MouseMove(e);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!FlagText && !FlagFill)
                buffer.MouseUp(e);
        }

        private void MenuUndo_Click(object sender, EventArgs e)
        {
            buffer.UnDo();
        }

        private void MenuDo_Click(object sender, EventArgs e)
        {
            buffer.ReDo();
        }

        private void ElipseToolsBTN_Click(object sender, EventArgs e)
        {
            if (flagTextEndEnter)
                buffer.MouseUp((MouseEventArgs)e); // проверить, как это работает

            clickFigure();
            buffer.Selected_step_init(new Elipse());
        }

        private void SquardToolsBTN_Click(object sender, EventArgs e)
        {
            if (flagTextEndEnter)
                buffer.MouseUp((MouseEventArgs)e); // проверить, как это работает
            clickFigure();
            buffer.Selected_step_init(new Square());
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagText && flagTextEndEnter == false)
            {
                buffer.MouseDown(e); // возможно сделать отдельный метод (или переименовать это)

                textb.Location = e.Location;
                textb.Size = new Size(100, 100); // должен задаваться в зависимости от выбранного шрифта
                textb.Visible = true;
                textb.Multiline = true;
                textb.BorderStyle = BorderStyle.None;

                textb.ScrollBars = ScrollBars.Vertical;
                textb.Focus();
                flagTextEndEnter = true;
            }

            else if (FlagText && flagTextEndEnter)
            {
                textb.Visible = false;
                buffer.MouseUp(e);
                ClearTextBox();   
            }
        }

        private void TextToolsBTN_Click(object sender, EventArgs e)
        {
            textb.Visible = false;
            if (flagTextEndEnter)
                buffer.MouseUp((MouseEventArgs)e);

            clickFigure();
            FlagText = true;
            buffer.Selected_step_init(new TextElement());
            fontDialog1.ShowDialog();
            textb.Font = fontDialog1.Font;
        }

        private void RubberToolsBTN_Click(object sender, EventArgs e)
        {
            if (flagTextEndEnter)
                buffer.MouseUp((MouseEventArgs)e); // проверить, как это работает
            clickFigure();
            buffer.Selected_step_init(new Rubber());
        }

        private void ClearTextBox()
        {
            textb.Text = "";
            flagTextEndEnter = false;
        }
        private void clickFigure()
        {
            FlagText = false;
            textb.Visible = false;
            FlagFill = false;
            ClearTextBox();
        }

      
        private void ColorBTN2_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.Red);
        }

        private void ColorBTN3_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.Green);
        }

        private void ChangeColour(Color col)
        {
            if (rightRBTN.Checked)
            {
                RightChoiceBTN.BackColor = col;
                buffer.ChangeBackColour(col);
            }

            if (leftRBTN.Checked)
            {
                leftChoiceBTN.BackColor = col;
                buffer.ChangePenColour(col);
            }
        }

        private void ColorBTN4_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.Blue);
        }

        private void ColorBTN5_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.Black);
        }

        private void ColorBTN6_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.Magenta);
        }

        private void ColorBTN7_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.Yellow);
        }

        private void ColorBTN8_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.Aqua);
        }

        private void LineBox_Enter(object sender, EventArgs e)
        {

        }

        private void Line1BTN_Click(object sender, EventArgs e)
        {
            ChangeLineBackColor(sender as Button);
            buffer.ChangePenWigth(1.5f);

        }
        private void ChangeLineBackColor(Button butt)
        {
            Line1BTN.BackColor = SystemColors.ControlLight;
            Line2BTN.BackColor = SystemColors.ControlLight;
            Line3BTN.BackColor = SystemColors.ControlLight;
            Line4BTN.BackColor = SystemColors.ControlLight;

            butt.BackColor = Color.NavajoWhite;
        }

        private void Line2BTN_Click(object sender, EventArgs e)
        {
            ChangeLineBackColor(sender as Button);
            buffer.ChangePenWigth(3);
        }

        private void Line3BTN_Click(object sender, EventArgs e)
        {
            ChangeLineBackColor(sender as Button);
            buffer.ChangePenWigth(4.5f);
        }

        private void Line4BTN_Click(object sender, EventArgs e)
        {
            ChangeLineBackColor(sender as Button);
            buffer.ChangePenWigth(6);
        }

        private void FillToolsBTN_Click(object sender, EventArgs e)
        {
            FlagFill = true;
            buffer.Selected_step_init(new Fill());
        }

        private void clickPictureBox(object sender, EventArgs e)
        {
            if (FlagFill)
                buffer.MouseDown((MouseEventArgs)e);
        }

        private void WidthLineBTN_Click(object sender, EventArgs e)
        {
            
        }
    }
}