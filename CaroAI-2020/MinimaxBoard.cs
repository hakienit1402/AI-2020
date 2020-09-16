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
    public class MinimaxBoard
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
        public int Depth { get => depth; set => depth = value; }

        public int[] point = new int[2];
        int _x, _y;
        System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
        private int maxDepth = 7;
        private int depth;

        #endregion

        #region Initialize
        public MinimaxBoard(Panel gameboard, PictureBox player, int depth)
        {
            this.gameboard = gameboard;
            this.Depth = depth;
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
            Point btnpos = GetButtonPoint(btn);
            _x = btnpos.X; _y = btnpos.Y;
            /*MessageBox.Show("Vị trí bạn đánh: " + btnpos.Y + ";" + btnpos.X);*/
            /* MessageBox.Show("Vị trí bạn đánh: " + _x + ";" + _y);*/
            bestMove(matrix);
            /* var start = DateTime.Now;*/
            time.Reset();
            time.Start();
            Point point = FindChess(matrix);
            time.Stop();
            long totaltime = time.ElapsedMilliseconds;
            MessageBox.Show($"Time =  {time.ElapsedMilliseconds} (ms) "); 
            /* var time = DateTime.Now.Subtract(start);
             MessageBox.Show($"Time =  {time.Milliseconds} (ms) ");*/
            OtherPlayerMark(point);
            
            changePlayer();
           // timer 
           if (HasWin(btn))
           {
               EndGame(btn);
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
            btn.Focus();
            changePlayer();
            if (HasWin(btn))
            {
                EndGame(btn);
            }
        }
        // chia player
        void changePlayer()
        {
            PlayerColor.Image = Player[currentPlayer].Color;
        }
        // Xử lí thắng 
        private void EndGame(Button btn)
        {   if (btn.BackgroundImage == Player[0].Color)
            {
                MessageBox.Show("AI win! New game ^^ ");
                gameboard.Enabled = false;
            }else 
                if (btn.BackgroundImage == Player[1].Color)
            {
                MessageBox.Show("You win! New game ^^ ");
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
        #region check win
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
        #endregion
        #region AI
        // Hàm lượng giá
        #region luong gia

/*        private long[] ArrayPointAttack = new long[7] { 0, 9, 54, 169, 1458, 12345, 140299 };
        private long[] ArrayPointDefend = new long[7] { 0, 3, 27, 99, 729, 3456, 6789 };*/
        private long[] ArrayPointAttack = new long[7] { 0, 9, 81, 729, 6561, 59049, 531441 };
        private long[] ArrayPointDefend = new long[7] { 0, 3, 27, 243, 2187, 19683, 177147 };
        #endregion
        // hàm clone board 
        /*  public List<List<Button>> GetClone()
          {
              List<List<Button>> TestBoard = new List<List<Button>>();
               Button oldbtn = new Button()
               {
                   Width = 0,
                   Location = new Point(0, 0)
               };
               for (int k = 0; k <= 15; k++)
               {
                   TestBoard.Add(new List<Button>());
                   for (int l = 0; l <= 15; l++)
                   {
                       Button btn = new Button()
                       {
                           BackColor = Color.Snow,
                           Width = Piece.PIECE_WIDTH,
                           Height = Piece.PIECE_HEIGHT,
                           Location = new Point(oldbtn.Location.X + oldbtn.Width, oldbtn.Location.Y),
                           BackgroundImage = Matrix[k][l].BackgroundImage,
                           BackgroundImageLayout = ImageLayout.Stretch,
                       };
                       TestBoard[k].Add(btn);
                       oldbtn = btn;
                   }
                   oldbtn.Location = new Point(0, oldbtn.Location.Y + Piece.PIECE_HEIGHT);
                   oldbtn.Width = 0;
                   oldbtn.Height = 0;
               }
              return TestBoard;
          }*/

        public int Minimax(int depth, List<List<Button>> Board, bool minmax)
        {
            
            if (depth == 0)
            {
                return heuristic(Board, _x, _y);
                /*bestMove(Board);*/
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
                            /* List<List<Button>> TestBoard = GetClone();*/
                            Board[i][j].BackgroundImage = Player[0].Color;
                            _x = i;_y = j;
                            int tempScore = Minimax(depth - 1, Board, false);
                           
                            if (maxScore < tempScore)
                            {   
                                maxScore = tempScore;
                                point[0] = i;
                                point[1] = j;
                            }
                            Board[i][j].BackgroundImage = null;
                        }
                        }
                    }
                    return   maxScore;
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
           
            /*return max;*/
        }
        public int AlphaBeta(int depth, List<List<Button>> Board, int alpha,int beta, bool minmax)
        {
            if (depth == 0)
            {
                return heuristic(Board, _x, _y);
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
                            _x = i; _y = j;
                           int tempScore =  AlphaBeta(depth - 1, Board, alpha, beta, false);
                            if (maxScore < tempScore)
                            {
                                maxScore = tempScore;
                                point[0] = i;
                                point[1] = j;
                            }
                            Board[i][j].BackgroundImage = null;
                            if (maxScore >= beta)
                            {
                                break;
                            }
                            else
                            {
                                alpha = maxScore;
                            }
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
                            Board[i][j].BackgroundImage = Player[1].Color;
                            maxScore = Math.Min(maxScore, AlphaBeta(depth - 1, Board, alpha, beta, true));
                            Board[i][j].BackgroundImage = null;
                            if (alpha >= maxScore)
                            {
                                break;
                            }
                            else
                            {
                                beta = maxScore;
                            }
                        }
                    }
                }
                return maxScore;
            }

        }
        public Point MaxScorePoint(List<List<Button>> Board)
        {
            Point maxPoint = new Point();
            long max = 0;
            for (int i=0; i<15; i++)
            {
                for (int j = 0; j <15;  j++)
                {
                    if (Board[i][j].BackgroundImage == null)
                    {
                        long attackScore = (AttackDoc(Board, i, j) + AttackNgang(Board, i, j) + AttackPhu(Board, i, j) + AttackChinh(Board, i, j));
                        long defendScore = (DefendDoc(Board, i, j) + DefendNgang(Board, i, j) + DefendPhu(Board, i, j) + DefendChinh(Board, i, j));
                        long score = attackScore > defendScore ? attackScore : defendScore;
                        if (max < score)
                        {
                            max = score;
                            maxPoint = new Point(j, i);
                        }
                    }
                }
            }
            return maxPoint;
        }
        public int AlphaBetaNewVS(int depth, List<List<Button>> Board, int alpha, int beta, bool minmax)
        {
            if (depth == 0)
            {
                return heuristic(Board, _x, _y);
            }
            if (minmax == true) // node max
            {
                List<Point> listPos = new List<Point>();
                for (int i=0; i < maxDepth; i++)
                {
                    Point node = MaxScorePoint(Board);
                    if (node == null) break;
                    listPos.Add(node);
                    long resetAscore =
                    AttackDoc(Board, node.Y, node.X) +
                    AttackNgang(Board, node.Y, node.X) +
                    AttackPhu(Board, node.Y, node.X) +
                    AttackChinh(Board, node.Y, node.X);
                    long resetDscore =
                    DefendDoc(Board, node.Y, node.X) +
                    DefendNgang(Board, node.Y, node.X) +
                    DefendPhu(Board, node.Y, node.X) +
                    DefendChinh(Board, node.Y, node.X);
                    resetAscore = 0; resetDscore = 0;
                }
                int maxScore = Int32.MinValue;
                for (int i =0; i< listPos.Count(); i++)
                {
                    Point testPoin = listPos[i];
                    /*Button btn = Board[testPoin.Y][testPoin.X];*/
                    Board[testPoin.Y][testPoin.X].BackgroundImage = Player[0].Color; 
                    _x = testPoin.Y; _y = testPoin.X;
                    int tempScore = AlphaBetaNewVS(depth - 1, Board, alpha, beta, false);
                    if (maxScore < tempScore)
                    {
                        maxScore = tempScore;
                        point[0] = testPoin.Y;
                        point[1] = testPoin.X;
                    }
                    Board[testPoin.Y][testPoin.X].BackgroundImage = null;
                    if (maxScore >= beta || HasWin(Board[testPoin.Y][testPoin.X]))
                    {
                        break;
                    }
                    else
                    {
                        alpha = maxScore;
                    }
                }
                return maxScore;
            }
            else // node min
            {
                List<Point> listPosMin = new List<Point>();
                for (int i = 0; i < maxDepth; i++)
                {
                    Point node = MaxScorePoint(Board);
                    if (node == null) break;
                    listPosMin.Add(node);
                    long resetAscore =
                    AttackDoc(Board, node.Y, node.X) +
                    AttackNgang(Board, node.Y, node.X) +
                    AttackPhu(Board, node.Y, node.X) +
                    AttackChinh(Board, node.Y, node.X);
                    long resetDscore =
                    DefendDoc(Board, node.Y, node.X) +
                    DefendNgang(Board, node.Y, node.X) +
                    DefendPhu(Board, node.Y, node.X) +
                    DefendChinh(Board, node.Y, node.X);
                    resetAscore = 0; resetDscore = 0;
                }
                int maxScore = Int32.MaxValue;
                for (int i = 0; i < listPosMin.Count(); i++)
                {
                    Point testPoin = listPosMin[i];
                   /* Button btn = Board[testPoin.Y][testPoin.X];*/
                    Board[testPoin.Y][testPoin.X].BackgroundImage = Player[1].Color;
                    maxScore = Math.Min(maxScore, AlphaBeta(depth - 1, Board, alpha, beta, true));
                    Board[testPoin.Y][testPoin.X].BackgroundImage = null;
                    if (alpha >= maxScore || HasWin(Board[testPoin.Y][testPoin.X]))
                    {
                        break;
                    }
                    else
                    {
                        beta = maxScore;
                    }
                }
                return maxScore;
            }


        }
        public int heuristic(List<List<Button>> Board,int row, int col)
        {   long attackScore = (AttackDoc(Board, row, col) + AttackNgang(Board, row, col) + AttackPhu(Board, row, col) + AttackChinh(Board, row, col));
            long defendScore = (DefendDoc(Board, row, col) + DefendNgang(Board, row, col) + DefendPhu(Board, row, col) + DefendChinh(Board, row, col));
            int value = 0;
            if ( attackScore > defendScore )
            {
                 value = (int)attackScore;
            }
            else 
            { 
                value = (int)defendScore;
            }
            return value;
        } 
        // hàm tính best move khi depth 0;
        public void bestMove (List<List<Button>> Board)
        {
            long tmp = 0;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    //chỉ duyệt những ô chưa đánh
                    if (Board[i][j].BackgroundImage == null)
                    {
                        long attackScore = (AttackDoc(Board, i, j) + AttackNgang(Board, i, j) + AttackPhu(Board, i, j) + AttackChinh(Board, i, j));
                        long defendScore = (DefendDoc(Board, i, j) + DefendNgang(Board, i, j) + DefendPhu(Board, i, j) + DefendChinh(Board, i, j));
                        long score = attackScore > defendScore ? attackScore : defendScore;
                        if (tmp < score)
                        {
                            /*_x = i; _y = j;*/
                            tmp = score;
                            point[0] = i;
                            point[1] = j;
                        }
                    }
                }
            }
           
           

        }
        // hàm tìm nước đi tốt nhất.
        public Point FindChess(List<List<Button>> Board)
        {
            int score = Minimax(Depth, Board, true);
            /*int score = AlphaBeta(3, Board, Int32.MinValue, Int32.MaxValue, true);*/
            /*int score = AlphaBetaNewVS(5, Board, Int32.MinValue, Int32.MaxValue, true);*/
            MessageBox.Show("Vị trí máy đánh tiếp theo : " + point[1] + ";" + point[0]);    
            return new Point(point[1], point[0]); 
        }
        #region evalution
        private long AttackDoc(List<List<Button>> board,int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            long TempPoint = 0;
            int ChessPlayer = 0; //số quân cờ của người
            int ChessComputer = 0; //số quân cờ của máy

           
            for (int i = 1; i < 5 && currentRow + i < 15; i++)
            {
               
                if (board[currentRow + i][currentCol].BackgroundImage == Player[0].Color)
                {
                    
                    ChessComputer++;
                }
               
                else if (board[currentRow + i][currentCol].BackgroundImage == Player[1].Color)
                {
                    
                    ChessPlayer++;
                   
                    TempPoint -= 9;
                    break; 
                }
                else 
                {
                    
                    break;
                }
            }

           
            for (int i = 1; i < 5 && currentRow - i >= 0; i++)
            {
                
                if (board[currentRow - i][currentCol].BackgroundImage == Player[0].Color)
                {
                    
                    ChessComputer++;
                }
                else if (board[currentRow - i][currentCol].BackgroundImage == Player[1].Color)
                {
                   
                    TempPoint -= 9;
                    
                    ChessPlayer++;
                    break; 
                }
                else 
                {
                   
                    break;
                }
            }

          
            if (ChessPlayer == 2)
                return 0;
         
            if (ChessComputer == 4)
              
                TotalPoint += ArrayPointAttack[ChessComputer] * 2;
          
            TotalPoint -= ArrayPointDefend[ChessPlayer];
         
            TotalPoint += ArrayPointAttack[ChessComputer];
         
            TotalPoint += TempPoint;

        
            return TotalPoint;
        }
        private long AttackNgang(List<List<Button>> board,int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            long TempPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 5 && currentCol + i < 15; i++)
            {
                if (board[currentRow][currentCol + i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                }
                else if (board[currentRow][currentCol + i].BackgroundImage == Player[1].Color)
                {
                    TempPoint -= 9;
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 5 && currentCol - i >= 0; i++)
            {
                if (board[currentRow][currentCol - i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                }
                else if (board[currentRow][currentCol - i].BackgroundImage == Player[1].Color)
                {
                    TempPoint -= 9;
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (ChessPlayer == 2)
                return 0;
            if (ChessComputer == 4)
                TotalPoint += ArrayPointAttack[ChessComputer] * 2;
            TotalPoint -= ArrayPointDefend[ChessPlayer];
            TotalPoint += ArrayPointAttack[ChessComputer];
            TotalPoint += TempPoint;

            return TotalPoint;
        }
        private long AttackPhu(List<List<Button>> board,int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            long TempPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 5 && currentCol + i < 15 && currentRow - i >= 0; i++)
            {
                if (board[currentRow - i][currentCol + i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                }
                else if (board[currentRow - i][currentCol + i].BackgroundImage == Player[1].Color)
                {
                    TempPoint -= 9;
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 5 && currentCol - i >= 0 && currentRow + i < 15; i++)
            {
                if (board[currentRow + i][currentCol - i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                }
                else if (board[currentRow + i][currentCol - i].BackgroundImage == Player[1].Color)
                {
                    TempPoint -= 9;
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (ChessPlayer == 2)
                return 0;
            if (ChessComputer == 4)
                TotalPoint += ArrayPointAttack[ChessComputer] * 2;
            TotalPoint -= ArrayPointDefend[ChessPlayer];
            TotalPoint += ArrayPointAttack[ChessComputer];
            TotalPoint += TempPoint;

            return TotalPoint;
        } 
        private long AttackChinh(List<List<Button>> board,int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            long TempPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 5 && currentCol + i < 15 && currentRow + i < 15; i++)
            {
                if (board[currentRow + i][currentCol + i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                }
                else if (board[currentRow + i][currentCol + i].BackgroundImage == Player[1].Color)
                {
                    TempPoint -= 9;
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 5 && currentCol - i >= 0 && currentRow - i >= 0; i++)
            {
                if (board[currentRow - i][currentCol - i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                }
                else if (board[currentRow - i][currentCol - i].BackgroundImage == Player[1].Color)
                {
                    TempPoint -= 9;
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (ChessPlayer == 2)
                return 0;
            if (ChessComputer == 4)
                TotalPoint += ArrayPointAttack[ChessComputer] * 2;
            TotalPoint -= ArrayPointDefend[ChessPlayer];
            TotalPoint += ArrayPointAttack[ChessComputer];
            TotalPoint += TempPoint;

            return TotalPoint;
        }
        private long DefendDoc(List<List<Button>> board,int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0; //số quân cờ của người
            int ChessComputer = 0; //số quân cờ của máy

         
            for (int i = 1; i < 5 && currentRow + i < 15; i++)
            {
               
                if (board[currentRow + i][currentCol].BackgroundImage == Player[0].Color)
                {
                   
                    ChessComputer++;
                    break; 
                }
               
                else if (board[currentRow + i][currentCol].BackgroundImage == Player[1].Color)
                {
                    
                    ChessPlayer++;
                }
                else 
                {
                    
                    break;
                }
            }

       
            for (int i = 1; i < 5 && currentRow - i >= 0; i++)
            {
               
                if (board[currentRow - i][currentCol].BackgroundImage == Player[0].Color)
                {
                    
                    ChessComputer++;
                    break; 
                }
                else if (board[currentRow - i][currentCol].BackgroundImage == Player[1].Color)
                {
                   
                    ChessPlayer++;
                }
                else 
                {
                    
                    break;
                }
            }

        
            if (ChessComputer == 2)
                return 0;
            
            TotalPoint += ArrayPointDefend[ChessPlayer];
          
            if (ChessPlayer > 0)
            {
                
                TotalPoint -= ArrayPointAttack[ChessComputer] * 2;
            }
            return TotalPoint;
        }
        private long DefendNgang(List<List<Button>> board,int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 5 && currentCol + i < 15; i++)
            {
                if (board[currentRow][currentCol + i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                    break;
                }
                else if (board[currentRow][currentCol + i].BackgroundImage == Player[1].Color)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 5 && currentCol - i >= 0; i++)
            {
                if (board[currentRow][currentCol - i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                    break;
                }
                else if (board[currentRow][currentCol - i].BackgroundImage == Player[1].Color)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            if (ChessComputer == 2)
                return 0;

            TotalPoint += ArrayPointDefend[ChessPlayer];
            if (ChessPlayer > 0)
            {
                TotalPoint -= ArrayPointAttack[ChessComputer] * 2;
            }
            return TotalPoint;
        }
        private long DefendPhu(List<List<Button>> board,int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 5 && currentCol + i < 15 && currentRow - i >= 0; i++)
            {
                if (board[currentRow - i][currentCol + i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                    break;
                }
                else if (board[currentRow - i][currentCol + i].BackgroundImage == Player[1].Color)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 5 && currentCol - i >= 0 && currentRow + i < 15; i++)
            {
                if (board[currentRow + i][currentCol - i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                    break;
                }
                else if (board[currentRow + i][currentCol - i].BackgroundImage == Player[1].Color)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            if (ChessComputer == 2)
                return 0;

            TotalPoint += ArrayPointDefend[ChessPlayer];
            if (ChessPlayer > 0)
            {
                TotalPoint -= ArrayPointAttack[ChessComputer] * 2;
            }

            return TotalPoint;
        }
        private long DefendChinh(List<List<Button>> board,int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 5 && currentCol + i < 15 && currentRow + i < 15; i++)
            {
                if (board[currentRow + i][currentCol + i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                    break;
                }
                else if (board[currentRow + i][currentCol + i].BackgroundImage == Player[1].Color)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 5 && currentCol - i >= 0 && currentRow - i >= 0; i++)
            {
                if (board[currentRow - i][currentCol - i].BackgroundImage == Player[0].Color)
                {
                    ChessComputer++;
                    break;
                }
                else if (board[currentRow - i][currentCol - i].BackgroundImage == Player[1].Color)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            if (ChessComputer == 2)
                return 0;

            TotalPoint += ArrayPointDefend[ChessPlayer];
            if (ChessPlayer > 0)
            {
                TotalPoint -= ArrayPointAttack[ChessComputer] * 2;
            }

            return TotalPoint;
        }
        #endregion
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
