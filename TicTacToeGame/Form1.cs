using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public partial class Form1 : Form
    {
        private const int BoardSize = 3;
        private const int CellSize = 100;
        private char[,] board;
        private char currentPlayer;
        private bool gameOver;
        public Form1()
        {
            InitializeComponent();
            InitializeBoard();
            MaximizeBox = false;
        }

        private void InitializeBoard()
        {
            board = new char[BoardSize, BoardSize];
            currentPlayer = 'X';
            gameOver = false;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    board[row, col] = ' ';
                }
            }

            gamePanel.Controls.Clear();
            gamePanel.Refresh();

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    Button button = new Button
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(col * CellSize, row * CellSize),
                        Font = new Font("Arial", 36),
                        Tag = new Point(row, col)
                    };
                    button.Click += Button_Click;
                    gamePanel.Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (gameOver)
                return;

            Button button = (Button)sender;
            Point position = (Point)button.Tag;
            int row = position.X;
            int col = position.Y;

            // Check if the selected cell is empty
            if (board[row, col] == ' ')
            {
                board[row, col] = currentPlayer;
                button.Text = currentPlayer.ToString();

                // Check if the current player has won
                if (HasPlayerWon(currentPlayer))
                {
                    MessageBox.Show($"Player {currentPlayer} wins!");
                    gameOver = true;
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("It's a draw!");
                    gameOver = true;
                }
                else
                {
                    // Switch to the next player
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
        }

        private bool HasPlayerWon(char player)
        {
            // Check rows
            for (int row = 0; row < BoardSize; row++)
            {
                if (board[row, 0] == player && board[row, 1] == player && board[row, 2] == player)
                    return true;
            }

            // Check columns
            for (int col = 0; col < BoardSize; col++)
            {
                if (board[0, col] == player && board[1, col] == player && board[2, col] == player)
                    return true;
            }

            // Check diagonals
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
                return true;

            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (board[row, col] == ' ')
                        return false;
                }
            }
            return true;
        }


        private void newGameButton_Click(object sender, EventArgs e)
        {
            InitializeBoard();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
