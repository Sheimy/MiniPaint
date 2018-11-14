﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace MiniPaint
{
    public partial class Form1 : Form
    {
        bool flagTextEndEnter = false;
        TextBox textb;
        enum Tools
        {line=1,elipse,square,text,fill,rubber };

        Tools tools = Tools.line;

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


       private void Buffer_ChangeStack(bool undo, bool redo)
        {
            MenuUndo.Enabled = undo;
            MenuDo.Enabled = redo;
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
            buffer.Selected_step_init(new Line());
            tools = Tools.line;
        }

        private void ColorBTN1_Click(object sender, EventArgs e)
        {
            ChangeColour(Color.White);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tools!=Tools.text && tools != Tools.fill)
                buffer.MouseDown(e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tools != Tools.text && tools != Tools.fill)
                buffer.MouseMove(e);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tools != Tools.text && tools != Tools.fill)
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
            tools = Tools.elipse;
        }

        private void SquardToolsBTN_Click(object sender, EventArgs e)
        {
            if (flagTextEndEnter)
                buffer.MouseUp((MouseEventArgs)e); // проверить, как это работает
            clickFigure();
            buffer.Selected_step_init(new Square());
            tools = Tools.square;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tools == Tools.text && flagTextEndEnter == false)
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

            else if (tools == Tools.text && flagTextEndEnter)
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
            LineBox.Hide();
            buffer.Selected_step_init(new TextElement());
            tools = Tools.text;
        }

        private void RubberToolsBTN_Click(object sender, EventArgs e)
        {
            if (flagTextEndEnter)
                buffer.MouseUp((MouseEventArgs)e); // проверить, как это работает
            clickFigure();
            WidthLineBTN.Enabled = true;
            buffer.Selected_step_init(new Rubber());
            tools = Tools.rubber;

        }

        private void ClearTextBox()
        {
            textb.Text = "";
            flagTextEndEnter = false;
        }
        private void clickFigure()
        {
            textb.Visible = false;
            WidthLineBTN.Enabled = true;
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
            WidthLineBTN.Enabled = false;
            buffer.Selected_step_init(new Fill());
            tools = Tools.fill;
            LineBox.Hide();
        }

        private void clickPictureBox(object sender, EventArgs e)
        {
            if (tools == Tools.fill)
                buffer.MouseDown((MouseEventArgs)e);
        }

        private void WidthLineBTN_Click(object sender, EventArgs e)
        {
            if (tools == Tools.text)
            {
                RubberOptionBox.Hide();
                LineBox.Hide();
                fontDialog1.ShowDialog();
                textb.Font = fontDialog1.Font;
            }
            else if (tools == Tools.line || tools == Tools.elipse || tools == Tools.square)
            {
                RubberOptionBox.Hide();
                LineBox.Show();
            }
            else if (tools == Tools.rubber)
            {
                LineBox.Hide();

                buffer.ChangePenWigth(3);
                RubberOptionBox.Show();           
            }
            //else if (tools == Tools.)
        }


        private void RubberPlusBTN_Click(object sender, EventArgs e) {

            if (buffer.Pen.Width < 81)
            {
                buffer.ChangePenWigth(buffer.Pen.Width + 3);
                DrawRubber();
            }

        }
        private void RubberMinusBTN_Click(object sender, EventArgs e)
        {
            if (buffer.Pen.Width > 3)
            {
                buffer.ChangePenWigth(buffer.Pen.Width -3);
                DrawRubber();
            }

        }

        private void DrawRubber()
        {
            Graphics RubberGraphics = RubberPictureBox.CreateGraphics();
            RubberGraphics.Clear(SystemColors.Control);
            Pen newPen = new Pen(Color.Black, 2);

            RubberGraphics.DrawRectangle(newPen, 45 - buffer.Pen.Width / 2, 45 - buffer.Pen.Width / 2, buffer.Pen.Width, buffer.Pen.Width);
            RubberGraphics.Dispose();
        }

        private void RubberPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (RubberOptionBox.Visible)
                DrawRubber();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buffer.Save();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Images|*.png;*.bmp;*.jpg";

            DialogResult result = saveFileDialog1.ShowDialog();
            if (result== DialogResult.OK)
                buffer.SaveAs(saveFileDialog1.FileName);

        }

        private void процессToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            buffer.DeserealBuffer();
        }

        private void процессToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buffer.SerealBuffer();
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.Image,0,0);
            
        }
    }
}