using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroAI_2020
{
    public partial class GameCaro : Form
    {
        #region Properties
        PVPBoard PVPBoard;
        PvsCBoard PvsCBoard;
        MinimaxBoard MinimaxBoard;
        AlphaBetaBoard AlphaBetaBoard;
        AlphaBetaNewVSBoard AlphaBetaNewVSBoard;
        private int depth = 0;
        #endregion

        public GameCaro()
        {
            InitializeComponent();
        }
        private void BoardGame_Load(object sender, EventArgs e)
        {
            blockButton();
        }
        //Mở khóa
        public void unlockButton()
        {
           
            btnAI.Enabled = true;
            btnDepth.Enabled = true;
        }
        //Khóa button.
        public void blockButton()
        {
           
            btnAI.Enabled = false;
            btnDepth.Enabled = false;
            /*btnNew.Focus();*/
        }


        private void btnPVP_Click(object sender, EventArgs e)
        {
            board.BackgroundImage = null;
            PVPBoard = new PVPBoard(board, color);
            PVPBoard.DrawBoard();
        }

        private void btnPVC_Click(object sender, EventArgs e)
        {
            board.BackgroundImage = null;
            PvsCBoard = new PvsCBoard(board, color);
            PvsCBoard.DrawBoard();
            PvsCBoard.OtherPlayerMark(new Point(7, 7));
        }

        private void pvsp_Click(object sender, EventArgs e)
        {
            board.BackgroundImage = null;
            PVPBoard = new PVPBoard(board, color);
            PVPBoard.DrawBoard();
            btnAI.Text = "1 vs 1";
        }

        private void minimax_Click(object sender, EventArgs e)
        {
            board.BackgroundImage = null;
            MinimaxBoard = new MinimaxBoard(board, color,depth);
            MinimaxBoard.DrawBoard();
            btnAI.Text = "Minimax";
            MessageBox.Show("Minimax with depth = " + depth);
            MinimaxBoard.OtherPlayerMark(new Point(7, 7));
        }

        private void alphaBeta_Click(object sender, EventArgs e)
        {
            board.BackgroundImage = null;
            AlphaBetaBoard = new AlphaBetaBoard(board, color, depth);
            AlphaBetaBoard.DrawBoard();
            btnAI.Text = "Alpha-Beta";
            MessageBox.Show("Alpha-Beta with depth = " + depth);
            AlphaBetaBoard.OtherPlayerMark(new Point(7, 7));
        }

        private void alphaBetaNewVersion_Click(object sender, EventArgs e)
        {
            board.BackgroundImage = null;
            AlphaBetaNewVSBoard = new AlphaBetaNewVSBoard(board, color, depth);
            AlphaBetaNewVSBoard.DrawBoard();
            btnAI.Text = "Alpha-Beta New Version";
            MessageBox.Show("Alpha-Beta New Version with depth = " + depth);
            AlphaBetaNewVSBoard.OtherPlayerMark(new Point(7, 7));
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            depth = 0;
            MessageBox.Show("AI with depth = " + depth);
            btnDepth.Text = " DEPTH = " + depth.ToString();
        }

        private void depth1_Click(object sender, EventArgs e)
        {
            depth = 1;
            MessageBox.Show("AI with depth = " + depth);
            btnDepth.Text = " DEPTH = " + depth.ToString();
        }

        private void depth2_Click(object sender, EventArgs e)
        {
            depth = 2;
            MessageBox.Show("AI with depth = " + depth);
            btnDepth.Text = " DEPTH = " +depth.ToString();
        }

        private void depth3_Click(object sender, EventArgs e)
        {
            depth = 3;
            MessageBox.Show("AI with depth = " + depth);
            btnDepth.Text = " DEPTH = " +depth.ToString();
        }

        private void depth4_Click(object sender, EventArgs e)
        {
            depth = 4;
            MessageBox.Show("AI with depth = " + depth);
            btnDepth.Text = " DEPTH = " +depth.ToString();
        }

        private void depth5_Click(object sender, EventArgs e)
        {
            depth = 5;
            MessageBox.Show("AI with depth = " + depth);
            btnDepth.Text = " DEPTH = " +depth.ToString();
        }

        private void depth6_Click(object sender, EventArgs e)
        {
            depth = 6;
            MessageBox.Show("AI with depth = " + depth);
            btnDepth.Text = " DEPTH = " + depth.ToString();
        }

        private void depth7_Click(object sender, EventArgs e)
        {
            depth = 7;
            MessageBox.Show("AI with depth = " + depth);
            btnDepth.Text = " DEPTH = " + depth.ToString();
        }

        private void eXITToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void rESTARTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}


