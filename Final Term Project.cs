using System;

namespace Connect_Four
{
   
    class ConnectFour
    {
        static char[,] board;
        static char currentPlayer;
        static bool gameover;

        static void Main()
        {
            InitializeBoard();
            currentPlayer = 'X';
            gameover = false;

            while (!gameover)
            {
                PrintBoard();
                Console.WriteLine("Player {0}, choose a column (1-7):", currentPlayer);
                int column = int.Parse(Console.ReadLine()) - 1;
                MakeMove(column);
                CheckWinCondition();
                SwitchPlayer();
            }
        }

        static void InitializeBoard()
        {
            board = new char[6, 7];
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        static void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("  1 2 3 4 5 6 7");
            for (int row = 0; row < 6; row++)
            {
                Console.Write("|");
                for (int col = 0; col < 7; col++)
                {
                    Console.Write(board[row, col]);
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------");
        }

        static void MakeMove(int column)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = currentPlayer;
                    return;
                }
            }
        }

        static void CheckWinCondition()
        {
            // Check horizontal
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row, col + 1] &&
                        board[row, col] == board[row, col + 2] &&
                        board[row, col] == board[row, col + 3])
                    {
                        gameover = true;
                        Console.WriteLine("Player {0} wins!", currentPlayer);
                        return;
                    }
                }
            }

            // Check vertical
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col] &&
                        board[row, col] == board[row + 2, col] &&
                        board[row, col] == board[row + 3, col])
                    {
                        gameover = true;
                        Console.WriteLine("Player {0} wins!", currentPlayer);
                        return;
                    }
                }
            }

            // Check diagonal (top left to bottom right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col + 1] &&
                        board[row, col] == board[row + 2, col + 2] &&
                        board[row, col] == board[row + 3, col + 3])
                    {
                        gameover = true;
                        Console.WriteLine("Player {0} wins!", currentPlayer);
                        return;
                    }
                }
            }

            // Check diagonal (top right to bottom left)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 3; col < 7; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col - 1] &&
                        board[row, col] == board[row + 2, col - 2] &&
                        board[row, col] == board[row + 3, col - 3])
                    {
                        gameover = true;
                        Console.WriteLine("Player {0} wins!", currentPlayer);
                        return;
                    }
                }
            }

            // Check for a draw
            bool isBoardFull = true;
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        isBoardFull = false;
                        break;
                    }
                }
            }

            if (isBoardFull)
            {
                gameover = true;
                Console.WriteLine("It's a draw!");
            }
        }

        static void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }

}
