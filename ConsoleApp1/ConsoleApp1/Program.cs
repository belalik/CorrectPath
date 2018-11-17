using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("test");
        }
    }

    class Board
    {
        public Cell[,] TheBoard { get; set; }
        public Cell activeCell;

        public Board(int rows, int columns) {
            
            Cell[,] TheBoard = new Cell[rows, columns];

            for (int i=1; i<rows; i++)
            {
                for (int j=1; j<columns; j++)
                {
                    TheBoard[i,j] = new Cell(i, j, false);
                }
            }
        }

        public Cell ActiveCell
        {
            get { return activeCell; }
            set { activeCell = value; }
        }

        public Cell GetAnyCell(int x, int y)
        {
            return TheBoard[x, y];
        }
    }


    class Referee
    {
        public bool AllowedMove(Board b, char move)
        {
            bool valid = true;
            Cell c = b.ActiveCell;

            switch (move)
            {
                case 'u':
                    if ((c.X == 1) || b.GetAnyCell(c.X-1, c.Y).Passed)
                    {
                        valid = false;
                    }
                    else
                    {
                        b.GetAnyCell(c.X - 1, c.Y).Passed = true;
                        b.ActiveCell = b.GetAnyCell(c.X - 1, c.Y);
                    }
                    break;
                case 'd':
                    if (c.X == b.TheBoard.GetLength(0) || b.GetAnyCell(c.X+1, c.Y).Passed)
                    {
                        valid = false;
                    }
                    else
                    {
                        b.GetAnyCell(c.X + 1, c.Y).Passed = true;
                        b.ActiveCell = b.GetAnyCell(c.X + 1, c.Y);
                    }
                    break;
                case 'r':
                    if (c.Y == b.TheBoard.GetLength(1) || b.GetAnyCell(c.X, c.Y+1).Passed)
                    {
                        valid = false;
                    }
                    break;
                case 'l':
                    if ((c.Y == 1) || b.GetAnyCell(c.X, c.Y - 1).Passed)
                    {
                        valid = false;
                    }
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            return valid;
        }
        
    }

    class Human
    {

        public Cell IsInCell { get; set; }

        public bool MoveHuman (string movement)
        {
            return true;
        }

        public bool IsWithinBoard (string movement)
        {
            return true;
        }
    }

    class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Passed { get; set; }

        public Cell (int x, int y, bool passed)
        {
            this.X = x;
            this.Y = y;
            this.Passed = false;
        }

    }

    class Movement
    {
        
        public Cell FromCell { get; set; }
        public Cell ToCell { get; set; }
    }
}


