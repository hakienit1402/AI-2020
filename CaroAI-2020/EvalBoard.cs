 using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroAI_2020
{
    public class EvalBoard
	{
		public int height, width;
		public int[,] EBoard;
		public int evaluationBoard = 0;
		public EvalBoard(int height, int width)
		{
			this.height = height;
			this.width = width;
			EBoard = new int[height,width];
			// ResetBoard();
		}

        public void resetBoard()
        {
            for (int r = 0; r < height; r++)
                for (int c = 0; c < width; c++)
                    EBoard[r, c] = 0;
        }

        public void setPosition(int x, int y, int diem)
		{
			EBoard[x,y] = diem;
		}
		public Point MaxPos()
		{
			int Max = 0; // diem max 
			Point p = new Point();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (EBoard[i,j] > Max)
					{
						p.X = i;
						p.Y = j;
						Max = EBoard[i,j];
					}
				}
			}
			if (Max == 0)
			{
				return Point.Empty;
			}
			evaluationBoard = Max;
			return p;
		}


		
	}
}


