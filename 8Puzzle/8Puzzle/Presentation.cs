using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace _8Puzzle
{
     class Presentation
    {
        public  PictureBox picPuzzle;
        int[,] State = new int[3, 3];
        int cubicWidth;
        int width;
        int border;
        int height;
        Graphics gPuzzle;
        Bitmap bmpPuzzle;
        public Presentation(int BorderSize,int CubicWidth,int Width,int Height,PictureBox pic)
        {
            width = Width;
            height = Height;
            cubicWidth = CubicWidth;
            border = BorderSize;
            bmpPuzzle = new Bitmap(Width, Height);
            picPuzzle = pic;
            picPuzzle.Image = bmpPuzzle;
            gPuzzle = Graphics.FromImage(picPuzzle.Image);
            
        }
        public void StateCopy(ref int [,] gameState)
        {
            for(int i=0;i<3;i++)
                for(int j=0;j<3;j++)
                    State[i,j] = gameState[i,j];
        }
        
        public void Update()
        {

            Pen pen1 = new Pen(Color.Black, 5);
            gPuzzle.DrawRectangle(pen1, 0, 0, width-1, height-1);
            
            for(int i=0;i<3;i++)
                for (int j = 0; j < 3; j++)
                {
                    gPuzzle.DrawRectangle(Pens.Black, 5 + cubicWidth * i, 5 + cubicWidth * j, cubicWidth, cubicWidth);
                    gPuzzle.FillRectangle(Brushes.LightGray, 5 + cubicWidth * i, 5 + cubicWidth * j, cubicWidth, cubicWidth);
                    if (State[j, i] != 0)
                    {
                        FillRoundRectangle(Brushes.White, 10 + cubicWidth * i, 10 + cubicWidth * j, cubicWidth - 10, cubicWidth - 10, 30);
                        DrawRoundRectangle(new Pen(Color.Black, 3), 10 + cubicWidth * i, 10 + cubicWidth * j, cubicWidth - 10, cubicWidth - 10, 30);

                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        gPuzzle.DrawString(State[j, i].ToString(), new Font("Arial", 30), Brushes.Black, new Rectangle(10 + cubicWidth * i, 10 + cubicWidth * j, cubicWidth - 10, cubicWidth - 10), sf);
                    }
                }

            picPuzzle.Refresh();   
           
        }
        void FillRoundRectangle(Brush p, int x, int y, int width, int height,int Rad)
        {            
            gPuzzle.FillPie(p, x, y, Rad, Rad, 180, 90);
            gPuzzle.FillPie(p, x + width - Rad, y, Rad, Rad, 0, -90);
            gPuzzle.FillPie(p, x, y + width - Rad, Rad, Rad, 180, -90);
            gPuzzle.FillPie(p, x + width - Rad, y + width - Rad, Rad, Rad, 0, 90);
            gPuzzle.FillRectangle(p, x + Rad/2, y, width - Rad, width);
            gPuzzle.FillRectangle(p, x, y + Rad/2, width, width - Rad);            
        }
        void DrawRoundRectangle(Pen p, int x, int y, int width, int height,int Rad)
        {            
            gPuzzle.DrawArc(p, x, y, Rad, Rad, 180, 90);
            gPuzzle.DrawArc(p, x + width - Rad, y, Rad, Rad, 0, -90);
            gPuzzle.DrawArc(p, x, y + width - Rad, Rad, Rad, 180, -90);
            gPuzzle.DrawArc(p, x + width - Rad, y + width - Rad, Rad, Rad, 0, 90);
            gPuzzle.DrawLine(p, x + Rad / 2, y, x + width - Rad/2+1, y);
            gPuzzle.DrawLine(p, x + Rad / 2, y+width , x + width - Rad / 2 + 1, y+width);
            gPuzzle.DrawLine(p, x , y+Rad/2, x, y+width -Rad/2+1);
            gPuzzle.DrawLine(p, x+width, y + Rad / 2, x+width, y + width - Rad / 2 + 1);
        }
        


    }
}
