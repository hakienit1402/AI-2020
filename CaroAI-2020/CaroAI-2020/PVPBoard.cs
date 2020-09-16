using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroAI_2020
{
    public class PVPBoard
    {
        #region Properties
        private Panel gameboard;
        
        public Panel Gameboard { get => gameboard; set => gameboard = value; }

        // kích thước board
        public static int soDong = 15;
        public static int soCot = 15;

        // Khởi tạo player
        private List<Player> player ;
        public List<Player> Player { get => player; set => player = value; }

        //biến kiểm tra đến lượt ai.
        private int isX;
        public int IsX { get => isX; set => isX = value; }
        

        private PictureBox playerColor;
        public PictureBox PlayerColor { get => playerColor; set => playerColor = value; }


        // Khởi tạo matrix list lồng list
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
    
        #endregion

        #region Initialize
        public PVPBoard(Panel gameboard, PictureBox player)
        {
            this.gameboard = gameboard;
            this.playerColor = player;
            this.Player = new List<Player>()
            {
                new Player("X", Image.FromFile(Application.StartupPath + "\\Resources\\doX.png")),
                new Player("O", Image.FromFile(Application.StartupPath + "\\Resources\\xanhO.png"))
            };
            isX = 0;

        }

        #endregion

        #region Methods
       
        public void DrawBoard()
        {
            //khởi tạo matrix
            Matrix = new List<List<Button>>();
            //khởi tạo btn đầu tiên X=0 , Y=0 .
            Button firstbtn = new Button()
            {
                Width = 0,
                Location = new Point(0, 0)
            };
            //duyệt 2 chiều.
            for (int i = 0; i <= soDong; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j <= soCot; j++)
                {   // khởi tạo button có chiều dài chiều rộng khai báo trước.
                    Button btn = new Button()
                    {
                        BackColor = Color.Snow,
                        Width = Piece.PIECE_WIDTH,
                        Height = Piece.PIECE_HEIGHT,
                        Location = new Point(firstbtn.Location.X + firstbtn.Width, firstbtn.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i
                    };
                    btn.Click += Btn_Click;
                    gameboard.Controls.Add(btn);// thêm vào panel board
                    Matrix[i].Add(btn);
                    firstbtn = btn;
                    changePlayer();
                }
                // set lại button đầu tiên của dòng tiếp theo X=0, Y = Y + PIECE_HEIGHT(btn). 
                firstbtn.Location = new Point(0, firstbtn.Location.Y + Piece.PIECE_HEIGHT);
                firstbtn.Width = 0;
                firstbtn.Height = 0;
            }
        }
        //chia player
        void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)

                return;
            btn.BackgroundImage = Player[isX].Color;
            isX = isX == 1 ? 0 : 1;
            //hiện người đánh.
            changePlayer();
           
            if (HasWin(btn))
            {
                EndGame(btn);
            }
        }

     

        // chia player
        void changePlayer()
        {
            PlayerColor.Image = Player[isX].Color;
        }
        // Xử lí thắng 
        private void EndGame(Button btn)
        {
            if (btn.BackgroundImage == Player[0].Color)
            {
                MessageBox.Show("X win! New game ^^ ");

                gameboard.Enabled = false;
            }
            else
                if (btn.BackgroundImage == Player[1].Color)
            {
                MessageBox.Show("O win! New game ^^ ");
                gameboard.Enabled = false;
            }
        }
        // tìm vị trí btn

        private bool HasWin(Button btn)
        {

            return checkNgang(btn) || checkDoc(btn) || checkCheoTrai(btn) || checkCheoPhai(btn);
        }

        private Point GetButtonPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag.ToString());
            int horizontal = Matrix[vertical].IndexOf(btn);
            Point point = new Point(horizontal, vertical);
            return point;
        }

        private bool checkNgang(Button btn)
        {
            Point point = GetButtonPoint(btn);
            int Left = 0;
            int Right = 0;

            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                    Left++;
                else
                    break;
            }

            for (int i = point.X + 1; i < soCot; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                    Right++;
                else
                    break;
            }
            return Left + Right >= 5;
        }

        private bool checkDoc(Button btn)
        {
            Point point = GetButtonPoint(btn);
            int Top = 0;
            int Bottom = 0;

            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                    Top++;
                else
                    break;
            }

            for (int i = point.Y + 1; i < soDong; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                    Bottom++;
                else
                    break;
            }
            return Top + Bottom >= 5;
        }

        private bool checkCheoTrai(Button btn)
        {
            Point point = GetButtonPoint(btn);
            int Top = 0;
            int Bottom = 0;

            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0)
                    break;
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                    Top++;
                else
                    break;

            }

            for (int i = 1; i < soCot - point.X; i++)
            {
                if (point.Y + i >= soDong)
                    break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                    Bottom++;
                else
                    break;

            }

            return Top + Bottom >= 5;
        }

        private bool checkCheoPhai(Button btn)
        {
            Point point = GetButtonPoint(btn);
            int Top = 0;
            int Bottom = 0;

            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y + i >= soDong)
                    break;
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                    Bottom++;
                else
                    break;

            }

            for (int i = 1; i < soCot - point.X; i++)
            {
                if (point.Y - i < 0)
                    break;
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                    Top++;
                else
                    break;

            }

            return Top + Bottom >= 5;
        }
        #endregion



    }
}
