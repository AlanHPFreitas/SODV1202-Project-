using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Connect_Four
{


    internal class Board
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public string[,] Table { get; set; }

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Table = new string[Rows, Cols];
        }

        public Board()
        {
            Rows = 6;
            Cols = 7;
            Table = new string[Rows, Cols];
        }


        public void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("  1 2 3 4 5 6 7");
            for (int row = 0; row < Rows; row++)
            {
                Console.Write("|");
                for (int col = 0; col < Cols; col++)
                {
                    Console.Write(Table[row, col]);
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------");
        }


        public void InitializeBoard()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Table[row, col] = "#";
                }
            }
        } 

    }

    internal abstract class PlayerBase
    {
        public string Name { get; set; }
        public string Symbol { get; set; }

        public PlayerBase(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public abstract void MakeMove(int column, Board board);
    }

    internal class Player : PlayerBase
    {
        public Player(string name, string symbol) : base(name, symbol)
        {
        }

        public override void MakeMove(int column, Board board)
        {
            for (int row = board.Rows - 1; row >= 0; row--)
            {
                if (board.Table[row, column] == "#")
                {
                    board.Table[row, column] = Symbol;
                    return;
                }
            }
        }
    }

    internal class Controler
    {
        public Board GameBoard { get; set; }
        public List<Player> players { get; set; }

        public bool gameover;


        public Controler()
        {
            GameBoard = new Board();
            players = new List<Player>(2);
            gameover = false;

            AskForPlayers();
        }


        public void AskForPlayers()
        {
            string name;
            Console.WriteLine("Write player 1 name: ");
            name = Console.ReadLine();
            var newPlayer = new Player(name, "X");
            players.Add(newPlayer);
            Console.WriteLine("Write player 2 name: ");
            name = Console.ReadLine();
            newPlayer = new Player(name, "O");
            players.Add(newPlayer);
        }



        public void CheckWinCondition(Player currentPlayer)
        {
            // Check horizontal
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (GameBoard.Table[row, col] != "#" &&
                        GameBoard.Table[row, col] == GameBoard.Table[row, col + 1] &&
                        GameBoard.Table[row, col] == GameBoard.Table[row, col + 2] &&
                        GameBoard.Table[row, col] == GameBoard.Table[row, col + 3])
                    {
                        gameover = true;
                        Console.WriteLine("Player {0} wins!", currentPlayer.Name);
                        return;
                    }
                }
            }

            // Check vertical
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (GameBoard.Table[row, col] != "#" &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 1, col] &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 2, col] &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 3, col])
                    {
                        gameover = true;
                        Console.WriteLine("Player {0} wins!", currentPlayer.Name);
                        return;
                    }
                }
            }

            // Check diagonal (top left to bottom right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (GameBoard.Table[row, col] != "#" &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 1, col + 1] &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 2, col + 2] &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 3, col + 3])
                    {
                        gameover = true;
                        Console.WriteLine("Player {0} wins!", currentPlayer.Name);
                        return;
                    }
                }
            }

            // Check diagonal (top right to bottom left)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 3; col < 7; col++)
                {
                    if (GameBoard.Table[row, col] != "#" &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 1, col - 1] &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 2, col - 2] &&
                        GameBoard.Table[row, col] == GameBoard.Table[row + 3, col - 3])
                    {
                        gameover = true;
                        Console.WriteLine("Player {0} wins!", currentPlayer.Name);
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
                    if (GameBoard.Table[row, col] == "#")
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

        public void RestartGame()
        {
            GameBoard.InitializeBoard();
            gameover = false;
        }

    }


    internal class program {

        static void Main()
        {
            Controler GameControler = new Controler();
            int GameTurn = 0;
            Player currentPlayer = GameControler.players[GameTurn];

            GameControler.GameBoard.InitializeBoard();

            bool playAgain = true;
            while (playAgain)
            {
                while (!GameControler.gameover)
                {
                    Console.WriteLine("Player {0}, choose a column (1-7):", currentPlayer.Name);
                    int column = int.Parse(Console.ReadLine()) - 1;
                    currentPlayer.MakeMove(column, GameControler.GameBoard);
                    GameControler.CheckWinCondition(currentPlayer);
                    GameControler.GameBoard.PrintBoard();

                    GameTurn++;
                    currentPlayer = GameControler.players[GameTurn % 2];
                }

                Console.WriteLine("Do you want to play again? (Y/N)");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "y")
                {
                    GameControler.RestartGame
();
                    GameTurn = 0;
                    currentPlayer = GameControler.players[GameTurn];
                }
                else
                {
                    playAgain = false;
                }
            }
        }

    }

}
