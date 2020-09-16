namespace CaroAI_2020
{
    partial class GameCaro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDepth = new System.Windows.Forms.Button();
            this.btnAI = new System.Windows.Forms.Button();
            this.color = new System.Windows.Forms.PictureBox();
            this.player1 = new System.Windows.Forms.PictureBox();
            this.board = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.nEWGAMEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pvsp = new System.Windows.Forms.ToolStripMenuItem();
            this.pvsc = new System.Windows.Forms.ToolStripMenuItem();
            this.minimax = new System.Windows.Forms.ToolStripMenuItem();
            this.alphaBeta = new System.Windows.Forms.ToolStripMenuItem();
            this.alphaBetaNewVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.depth0 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.depth1 = new System.Windows.Forms.ToolStripMenuItem();
            this.depth2 = new System.Windows.Forms.ToolStripMenuItem();
            this.depth3 = new System.Windows.Forms.ToolStripMenuItem();
            this.depth4 = new System.Windows.Forms.ToolStripMenuItem();
            this.depth5 = new System.Windows.Forms.ToolStripMenuItem();
            this.depth6 = new System.Windows.Forms.ToolStripMenuItem();
            this.depth7 = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.rESTARTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDepth
            // 
            this.btnDepth.BackColor = System.Drawing.Color.LightGray;
            this.btnDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDepth.Location = new System.Drawing.Point(31, 362);
            this.btnDepth.Name = "btnDepth";
            this.btnDepth.Size = new System.Drawing.Size(127, 27);
            this.btnDepth.TabIndex = 2;
            this.btnDepth.Text = "Chọn depth";
            this.btnDepth.UseVisualStyleBackColor = false;
            this.btnDepth.Click += new System.EventHandler(this.btnPVC_Click);
            // 
            // btnAI
            // 
            this.btnAI.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAI.Location = new System.Drawing.Point(12, 312);
            this.btnAI.Name = "btnAI";
            this.btnAI.Size = new System.Drawing.Size(165, 50);
            this.btnAI.TabIndex = 3;
            this.btnAI.Text = "Chọn chế độ chơi";
            this.btnAI.UseVisualStyleBackColor = false;
            this.btnAI.Click += new System.EventHandler(this.btnPVP_Click);
            // 
            // color
            // 
            this.color.BackgroundImage = global::CaroAI_2020.Properties.Resources.logoNLU;
            this.color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.color.Location = new System.Drawing.Point(31, 395);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(127, 111);
            this.color.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.color.TabIndex = 6;
            this.color.TabStop = false;
            // 
            // player1
            // 
            this.player1.BackgroundImage = global::CaroAI_2020.Properties.Resources.FIT;
            this.player1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.player1.Location = new System.Drawing.Point(12, 29);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(165, 152);
            this.player1.TabIndex = 0;
            this.player1.TabStop = false;
            // 
            // board
            // 
            this.board.AutoScroll = true;
            this.board.BackColor = System.Drawing.Color.Gainsboro;
            this.board.BackgroundImage = global::CaroAI_2020.Properties.Resources.new_game;
            this.board.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.board.Location = new System.Drawing.Point(189, 29);
            this.board.Margin = new System.Windows.Forms.Padding(0);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(450, 480);
            this.board.TabIndex = 1;
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.LightGray;
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.Red;
            this.btnHelp.Location = new System.Drawing.Point(12, 185);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(165, 127);
            this.btnHelp.TabIndex = 8;
            this.btnHelp.Text = "Game Caro sử dụng minimax search và cắt tỉa alpha-beta (kèm theo phiên bản nâng c" +
    "ấp).\r\nHà Ngọc Kiên \r\n17130103";
            this.btnHelp.UseVisualStyleBackColor = false;
            // 
            // nEWGAMEToolStripMenuItem
            // 
            this.nEWGAMEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pvsp,
            this.pvsc});
            this.nEWGAMEToolStripMenuItem.Name = "nEWGAMEToolStripMenuItem";
            this.nEWGAMEToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.nEWGAMEToolStripMenuItem.Text = "GAME OPTIONS";
            // 
            // pvsp
            // 
            this.pvsp.Name = "pvsp";
            this.pvsp.Size = new System.Drawing.Size(181, 22);
            this.pvsp.Text = "Person vs Person";
            this.pvsp.Click += new System.EventHandler(this.pvsp_Click);
            // 
            // pvsc
            // 
            this.pvsc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimax,
            this.alphaBeta,
            this.alphaBetaNewVersion});
            this.pvsc.Name = "pvsc";
            this.pvsc.Size = new System.Drawing.Size(181, 22);
            this.pvsc.Text = "Person vs Computer";
            // 
            // minimax
            // 
            this.minimax.Name = "minimax";
            this.minimax.Size = new System.Drawing.Size(201, 22);
            this.minimax.Text = "Minimax";
            this.minimax.Click += new System.EventHandler(this.minimax_Click);
            // 
            // alphaBeta
            // 
            this.alphaBeta.Name = "alphaBeta";
            this.alphaBeta.Size = new System.Drawing.Size(201, 22);
            this.alphaBeta.Text = "Alpha-Beta";
            this.alphaBeta.Click += new System.EventHandler(this.alphaBeta_Click);
            // 
            // alphaBetaNewVersion
            // 
            this.alphaBetaNewVersion.Name = "alphaBetaNewVersion";
            this.alphaBetaNewVersion.Size = new System.Drawing.Size(201, 22);
            this.alphaBetaNewVersion.Text = "Alpha-Beta New Version";
            this.alphaBetaNewVersion.Click += new System.EventHandler(this.alphaBetaNewVersion_Click);
            // 
            // depth0
            // 
            this.depth0.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.depth1,
            this.depth2,
            this.depth3,
            this.depth4,
            this.depth5,
            this.depth6,
            this.depth7});
            this.depth0.Name = "depth0";
            this.depth0.Size = new System.Drawing.Size(55, 20);
            this.depth0.Text = "DEPTH";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem2.Text = "0";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // depth1
            // 
            this.depth1.Name = "depth1";
            this.depth1.Size = new System.Drawing.Size(80, 22);
            this.depth1.Text = "1";
            this.depth1.Click += new System.EventHandler(this.depth1_Click);
            // 
            // depth2
            // 
            this.depth2.Name = "depth2";
            this.depth2.Size = new System.Drawing.Size(80, 22);
            this.depth2.Text = "2";
            this.depth2.Click += new System.EventHandler(this.depth2_Click);
            // 
            // depth3
            // 
            this.depth3.Name = "depth3";
            this.depth3.Size = new System.Drawing.Size(80, 22);
            this.depth3.Text = "3";
            this.depth3.Click += new System.EventHandler(this.depth3_Click);
            // 
            // depth4
            // 
            this.depth4.Name = "depth4";
            this.depth4.Size = new System.Drawing.Size(80, 22);
            this.depth4.Text = "4";
            this.depth4.Click += new System.EventHandler(this.depth4_Click);
            // 
            // depth5
            // 
            this.depth5.Name = "depth5";
            this.depth5.Size = new System.Drawing.Size(80, 22);
            this.depth5.Text = "5";
            this.depth5.Click += new System.EventHandler(this.depth5_Click);
            // 
            // depth6
            // 
            this.depth6.Name = "depth6";
            this.depth6.Size = new System.Drawing.Size(80, 22);
            this.depth6.Text = "6";
            this.depth6.Click += new System.EventHandler(this.depth6_Click);
            // 
            // depth7
            // 
            this.depth7.Name = "depth7";
            this.depth7.Size = new System.Drawing.Size(80, 22);
            this.depth7.Text = "7";
            this.depth7.Click += new System.EventHandler(this.depth7_Click);
            // 
            // eXITToolStripMenuItem1
            // 
            this.eXITToolStripMenuItem1.Name = "eXITToolStripMenuItem1";
            this.eXITToolStripMenuItem1.Size = new System.Drawing.Size(41, 20);
            this.eXITToolStripMenuItem1.Text = "EXIT";
            this.eXITToolStripMenuItem1.Click += new System.EventHandler(this.eXITToolStripMenuItem1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nEWGAMEToolStripMenuItem,
            this.depth0,
            this.eXITToolStripMenuItem1,
            this.rESTARTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(648, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // rESTARTToolStripMenuItem
            // 
            this.rESTARTToolStripMenuItem.Name = "rESTARTToolStripMenuItem";
            this.rESTARTToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.rESTARTToolStripMenuItem.Text = "RESTART";
            this.rESTARTToolStripMenuItem.Click += new System.EventHandler(this.rESTARTToolStripMenuItem_Click);
            // 
            // GameCaro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(648, 518);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.color);
            this.Controls.Add(this.player1);
            this.Controls.Add(this.btnAI);
            this.Controls.Add(this.btnDepth);
            this.Controls.Add(this.board);
            this.Controls.Add(this.menuStrip1);
            this.MaximizeBox = false;
            this.Name = "GameCaro";
            this.Text = "Game Caro AI - 2020 - Hà Kiên IT";
            this.Load += new System.EventHandler(this.BoardGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel board;
        private System.Windows.Forms.Button btnDepth;
        private System.Windows.Forms.Button btnAI;
        private System.Windows.Forms.PictureBox player1;
        private System.Windows.Forms.PictureBox color;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolStripMenuItem nEWGAMEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pvsp;
        private System.Windows.Forms.ToolStripMenuItem pvsc;
        private System.Windows.Forms.ToolStripMenuItem minimax;
        private System.Windows.Forms.ToolStripMenuItem alphaBeta;
        private System.Windows.Forms.ToolStripMenuItem alphaBetaNewVersion;
        private System.Windows.Forms.ToolStripMenuItem depth0;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem depth1;
        private System.Windows.Forms.ToolStripMenuItem depth2;
        private System.Windows.Forms.ToolStripMenuItem depth3;
        private System.Windows.Forms.ToolStripMenuItem depth4;
        private System.Windows.Forms.ToolStripMenuItem depth5;
        private System.Windows.Forms.ToolStripMenuItem depth6;
        private System.Windows.Forms.ToolStripMenuItem depth7;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rESTARTToolStripMenuItem;
    }
}

