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

        // Implemented constructer overloading
        // The first one is parameterized constructor
        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Table = new string[Rows, Cols];
        }

        // Non parameterized constructor
        public Board()
        {
            Rows = 6;
            Cols = 7;
            Table = new string[Rows, Cols];
        }


        public void PrintBoard()
        {
            
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
            Console.WriteLine("---------------\n");
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
    //Implemented abstract class inheriting from interface
    internal abstract class PlayerBase : IComparable<PlayerBase> 
    {
        public string Name { get; set; }
        public string Symbol { get; set; }

        public int NumberOfWins { get; set; }

        //Overriding method of interface
        public  int CompareTo(PlayerBase obj)
        {
          if (NumberOfWins > obj.NumberOfWins)
            {
                return 1;

            }
           else if (NumberOfWins < obj.NumberOfWins)
            {
                return -1;

            }
          else { return 0; }
        }

        public PlayerBase(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
            NumberOfWins = 0;
        }

        public abstract void MakeMove(int column, Board board);
    }

    // HumanPlayer is a concrete class inheriting from PlayerBase abstract class
    internal class HumanPlayer : PlayerBase
    {
        public HumanPlayer (string name, string symbol) : base(name, symbol)
        {
        }

        public override void MakeMove(int column, Board board)
        {
            for (int row = board.Rows - 1; row >= 0; row--)
            {
                if (board.Table[row, column] == "#")
                {
                    board.Table[row, column] = Symbol;
                    break;
                }
            }
        }
    }

    internal class Controler
    {
        public Board GameBoard { get; set; }
        public List<PlayerBase> Players { get; set; }

        public bool gameover;


        public Controler()
        {
            GameBoard = new Board();
            Players = new List<PlayerBase>(2);
            gameover = false;

            AskForPlayers();
        }


        public void AskForPlayers()
        {
            string name;
            Console.WriteLine("Write player 1 name: ");
            name = Console.ReadLine();
            //Polymorphism with PlayerBase and HumanPlayer
            PlayerBase newPlayer = new HumanPlayer(name, "X");
            Players.Add(newPlayer);
            Console.WriteLine("Write player 2 name: ");
            name = Console.ReadLine();
            newPlayer = new HumanPlayer(name, "O");
            Players.Add(newPlayer);
        }



        public void CheckWinCondition(PlayerBase currentPlayer)
        {
            // Check horizontal
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (GameBoard.Table[row, col] == currentPlayer.Symbol &&
                        GameBoard.Table[row, col + 1] == currentPlayer.Symbol &&
                        GameBoard.Table[row, col + 2] == currentPlayer.Symbol &&
                        GameBoard.Table[row, col + 3] == currentPlayer.Symbol)
                    {
                        gameover = true;
                        Console.WriteLine("\nPlayer {0} wins!\n", currentPlayer.Name);
                        currentPlayer.NumberOfWins++;
                        return;
                    }
                }
            }

            // Check vertical
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (GameBoard.Table[row, col] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 1, col] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 2, col] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 3, col] == currentPlayer.Symbol)
                    {
                        gameover = true;
                        Console.WriteLine("\nPlayer {0} wins!\n", currentPlayer.Name);
                        currentPlayer.NumberOfWins++;
                        return;
                    }
                }
            }

            // Check diagonal (top left to bottom right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (GameBoard.Table[row, col] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 1, col + 1] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 2, col + 2] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 3, col + 3] == currentPlayer.Symbol)
                    {
                        gameover = true;
                        Console.WriteLine("\nPlayer {0} wins!\n", currentPlayer.Name);
                        currentPlayer.NumberOfWins++;
                        return;
                    }
                }
            }

            // Check diagonal (top right to bottom left)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 3; col < 7; col++)
                {
                    if (GameBoard.Table[row, col] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 1, col - 1] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 2, col - 2] == currentPlayer.Symbol &&
                        GameBoard.Table[row + 3, col - 3] == currentPlayer.Symbol)
                    {
                        gameover = true;
                        Console.WriteLine("\nPlayer {0} wins!\n", currentPlayer.Name);
                        currentPlayer.NumberOfWins++;
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
                Console.WriteLine("\nIt's a draw!\n");
            }

        }

        public void RestartGame()
        {
            GameBoard.InitializeBoard();
            gameover = false;
        }

    }


    internal class Program {

        static void Main()
        {
            Controler GameControler = new Controler();
            int GameTurn = 0;
            PlayerBase currentPlayer = GameControler.Players[GameTurn];

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
                    currentPlayer = GameControler.Players[GameTurn % 2];
                }

                Console.WriteLine("Do you want to play again? (Y/N)");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "y")
                {
                    GameControler.RestartGame
();
                    GameTurn = 0;
                    currentPlayer = GameControler.Players[GameTurn];
                }
                else
                {
                    playAgain = false;
                    if (GameControler.Players[0].CompareTo(GameControler.Players[1]) == 1)
                    {
                        Console.WriteLine($"Player {GameControler.Players[0].Name} won the game! Total wins: {GameControler.Players[0].NumberOfWins}");
                    }
                    else if (GameControler.Players[0].CompareTo(GameControler.Players[1]) == -1)
                    {
                        Console.WriteLine($"Player {GameControler.Players[1].Name} won the game! Total wins: {GameControler.Players[1].NumberOfWins}");
                    }
                    else 
                    {
                        Console.WriteLine("The game ended in a draw!");
                    }
                }
            }
        }

    }

}
