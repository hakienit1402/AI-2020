using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
namespace CaroAI_2020
{
    public class PvsCTest
    {
        #region Properties
        private Panel gameboard;//chessBoard
        public Panel Gameboard { get => gameboard; set => gameboard = value; }

        // kích thước board
        public static int soDong = 15;
        public static int soCot = 15;

        // Khởi tạo player
        private List<Player> player;
        public List<Player> Player { get => player; set => player = value; }

        //biến kiểm tra lượt của ai.
        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        // Màu của quân cờ.
        private PictureBox playerColor;// playerMark
        public PictureBox PlayerColor { get => playerColor; set => playerColor = value; }
        // Sự kiện đánh vào ô cờ.

        // Khởi tạo matrix list lồng list ma trận lưu lại các ô cờ.
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        public int[] point = new int[2];

        // matrix Test
        public int[,] evalBoard = new int[15, 15];

        #endregion

        #region Initialize
        public PvsCTest(Panel gameboard, PictureBox player)
        {
            this.gameboard = gameboard;
            this.playerColor = player;
            this.Player = new List<Player>()
            {   // tạo 2 người chơi.
                new Player("X", Image.FromFile(Application.StartupPath + "\\Resources\\doX.png")),
                new Player("O", Image.FromFile(Application.StartupPath + "\\Resources\\xanhO.png"))
            };


        }

        #endregion

        #region Methods

        public void DrawBoard()
        {
            gameboard.Controls.Clear();
            gameboard.Enabled = true;
            currentPlayer = 0;// người chơi đầu tiên là X. 0: X ; 1: O
            //khởi tạo matrix
            Matrix = new List<List<Button>>();
            //khởi tạo btn đầu tiên tọa độ X=0 , Y=0.
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
        //Event click vào ô cờ.
        private void Btn_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn.BackgroundImage != null)

                return;

            btn.BackgroundImage = Player[currentPlayer].Color;
            currentPlayer = currentPlayer == 1 ? 0 : 1;
            Point point = FindChess(matrix);
            OtherPlayerMark(point);
            changePlayer();
            //hiện người đánh.
            if (HasWin(btn))
            {
                EndGame();
            }
        }

        public void OtherPlayerMark(Point point)
        {
            Button btn = Matrix[point.Y][point.X];
            if (btn.BackgroundImage != null)
                return;
            btn.BackgroundImage = Player[currentPlayer].Color;
            /*currentPlayer = 1;*/
            currentPlayer = currentPlayer == 1 ? 0 : 1;
            changePlayer();
            if (HasWin(btn))
            {
                EndGame();
            }
        }
        // chia player
        void changePlayer()
        {
            PlayerColor.Image = Player[currentPlayer].Color;
        }
        // Xử lí thắng 
        private void EndGame()
        {
            MessageBox.Show("Has win! New game ^^ ");
            gameboard.Enabled = false;
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
        #region AI
        // Hàm lượng giá
        #region luong gia
        private int[] AScore = new int[7] { 0, 9, 54, 169, 1458, 12345,140299 };
        private int[] DScore = new int[7] { 0, 3, 27, 99, 729, 3456, 6789 };
       
        #endregion
        // hàm tính điểm bàn cờ.
        public void resetBoard()// reset điểm bàn cờ.
        {
            for (int r = 0; r < 15; r++)
                for (int c = 0; c < 15; c++)
                    evalBoard[r, c] = 0;
        }
        public int evalMaxPosition()// hàm trả về điểm cao nhất.
        {
            int Max = 0; // diem max 
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (evalBoard[i, j] > Max)
                    {
                        point[0] = i;
                        point[1] = j;
                        Max = evalBoard[i, j];
                    }
                }
            }
            /*maxScore = Max;*/
            return Max;
        }
        //heuristic
        public int heuristic(List<List<Button>> board )
        {   int maxScore = 0;
            evalScoreBoard(board,true);// tính điểm
            int MAX = evalMaxPosition();// lấy điểm cao nhất
            if (maxScore < MAX ) {
            return maxScore= MAX;// gán cho maxScore 
            }
            return maxScore; // trả về maxScore
        }
        public void evalScoreBoard(List<List<Button>> board, bool minmax)
        {
            int row, col;
            int numAI, numHuman;
            resetBoard();
            for (row = 0; row <15; row++)
                for (col = 0; col < 15 - 4; col++)
                {
                    numAI = 0;
                    numHuman = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (board[row][col + i].BackgroundImage == Player[1].Color) // neu quan do la cua human
                            numHuman++;
                        if (board[row][col + i].BackgroundImage == Player[0].Color) // neu quan do la cua pc
                            numAI++;
                    }
                    // trong vong 5 o khong co quan dich
                    if (numHuman * numAI == 0 && numHuman != numAI)
                        for (int i = 0; i < 5; i++)
                        {
                            if (board[row][col + i].BackgroundImage == null)
                            { // neu o chua danh
                                /*if (numHuman == 2) evalBoard[row, col + i] = 0;*/
                                if (numHuman == 0) // numAI khac 0
                                    if (minmax == false)
                                        evalBoard[row, col + i] += DScore[numAI]; // cho diem phong ngu
                                    else
                                        evalBoard[row, col + i] += AScore[numAI];// cho diem tan cong
                                if (numAI == 0) // numHuman khac 0
                                    if (minmax == true)
                                        evalBoard[row, col + i] += DScore[numHuman];// cho diem phong ngu	
                                    else
                                        evalBoard[row, col + i] += AScore[numHuman];// cho diem tan cong
                                if (numHuman == 4 || numAI == 4)
                                    evalBoard[row, col + i] *=2 ;
                            }
                        }
                }
            // Duyet theo cot
            for (col = 0; col < 15; col++)
                for (row = 0; row < 15 - 4; row++)
                {
                    numAI = 0;
                    numHuman = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (board[row + i][col].BackgroundImage == Player[1].Color)
                            numHuman++;
                        if (board[row + i][col].BackgroundImage == Player[0].Color)
                            numAI++;
                    }
                    if (numHuman * numAI == 0 && numHuman != numAI)
                        for (int i = 0; i < 5; i++)
                        {
                            if (board[row + i][col].BackgroundImage == null) // Neu o chua duoc danh
                            {
                               /* if (numHuman == 2) evalBoard[row + i, col] = 0;*/
                                if (numHuman == 0) // numAI khac 0
                                    if (minmax == false)
                                        evalBoard[row + i, col] += DScore[numAI]; // cho diem phong ngu
                                    else
                                        evalBoard[row + i, col] += AScore[numAI];// cho diem tan cong
                                if (numAI == 0) // numHuman khac 0
                                    if (minmax == true)
                                        evalBoard[row + i, col] += DScore[numHuman];// cho diem phong ngu	
                                    else
                                        evalBoard[row + i, col] += AScore[numHuman];// cho diem tan cong
                                if (numHuman == 4 || numAI == 4)
                                    evalBoard[row + i, col] *= 2;
                            }

                        }
                }
            // Duyet theo duong cheo xuong
            for (col = 0; col <15 - 4; col++)
                for (row = 0; row < 15 - 4; row++)
                {
                    numAI = 0;
                    numHuman = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (board[row + i][col + i].BackgroundImage == Player[1].Color)
                            numHuman++;
                        if (board[row + i][col + i].BackgroundImage == Player[0].Color)
                            numAI++;
                    }
                    if (numHuman * numAI == 0 && numHuman != numAI)
                        for (int i = 0; i < 5; i++)
                        {
                            if (board[row + i][col + i].BackgroundImage == null) // Neu o chua duoc danh
                            {
                               
                               /* if (numHuman == 2) evalBoard[row + i, col + i] = 0;*/
                                if (numHuman == 0) // numAI khac 0
                                    if (minmax == false)
                                        evalBoard[row + i, col + i] += DScore[numAI]; // cho diem phong ngu
                                    else
                                        evalBoard[row + i, col + i] += AScore[numAI];// cho diem tan cong
                                if (numAI == 0) // numHuman khac 0
                                    if (minmax == true)
                                        evalBoard[row + i, col + i] += DScore[numHuman];// cho diem phong ngu	
                                    else
                                        evalBoard[row + i, col + i] += AScore[numHuman];// cho diem tan cong
                                if (numHuman == 4 || numAI == 4)
                                    evalBoard[row + i, col + i] *= 2;
                            }

                        }
                }

            // Duyet theo duong cheo len
            for (row = 4; row < 15; row++)
                for (col = 0; col < 15 - 4; col++)
                {
                    numAI = 0; // so quan PC
                    numHuman = 0; // so quan Human
                    for (int i = 0; i < 5; i++)
                    {
                        if (board[row - i][col + i].BackgroundImage == Player[1].Color) // neu la human
                            numHuman++; // tang so quan human
                        if (board[row - i][col + i].BackgroundImage == Player[1].Color) // neu la PC
                            numAI++; // tang so quan PC
                    }
                    if (numHuman * numAI == 0 && numHuman != numAI)
                        for (int i = 0; i < 5; i++)
                        {
                            if (board[row - i][col + i].BackgroundImage == null)
                            { // neu o chua duoc danh
                               
                              /*  if (numHuman == 2) evalBoard[row - i, col + i] = 0;*/
                                if (numHuman == 0) // numAI khac 0
                                    if (minmax == false)
                                        evalBoard[row - i, col + i] += DScore[numAI]; // cho diem phong ngu
                                    else
                                        evalBoard[row - i, col + i] += AScore[numAI];// cho diem tan cong
                                if (numAI == 0) // numHuman khac 0
                                    if (minmax == true)
                                        evalBoard[row - i, col + i] += DScore[numHuman];// cho diem phong ngu	
                                    else
                                        evalBoard[row - i, col + i] += AScore[numHuman];// cho diem tan cong
                                if (numHuman == 4 || numAI == 4)
                                    evalBoard[row - i, col + i] *= 2;
                            }

                        }
                }
        }
        //minimax
        public int Minimax(int depth, List<List<Button>> Board, bool minmax)
        {
            if (depth == 0)
            {
                return heuristic(Board);
            }
            if (minmax == true) // node max
            {
                int maxScore = Int32.MinValue;
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (Board[i][j].BackgroundImage == null)
                        {
                            Board[i][j].BackgroundImage = Player[0].Color; 
                            int tempScore = Minimax(depth - 1, Board, false);
                            if (maxScore < tempScore)
                            {
                                maxScore = tempScore;
                                /*point[0] = i;
                                point[1] = j;*/
                            }
                            Board[i][j].BackgroundImage = null;
                        }
                    }
                }
                return maxScore;
            }
            else // node min
            {
                int maxScore = Int32.MaxValue;
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (Board[i][j].BackgroundImage == null)
                        {
                            //gán i,j là người.
                            /*List<List<Button>> TestBoard = GetClone();*/
                            Board[i][j].BackgroundImage = Player[1].Color;
                            maxScore = Math.Min(maxScore, Minimax(depth - 1, Board, true));
                            Board[i][j].BackgroundImage = null;
                        }
                    }
                }
                return maxScore;
            }
        }

        public Point FindChess(List<List<Button>> Board)
        {        
            int TempPoint = Minimax(2, Board, true);  
            MessageBox.Show("Vị trí điểm cao nhất : " + point[1] + ";" + point[0]);
            return new Point(point[1], point[0]);
        }

        #endregion
        public class ButtonClickEvent : EventArgs
        {
            private Point clickedPoint;

            public Point ClickedPoint
            {
                get
                {
                    return clickedPoint;
                }

                set
                {
                    clickedPoint = value;
                }
            }

            public ButtonClickEvent(Point point)
            {
                this.ClickedPoint = point;
            }
        }

    }
}
