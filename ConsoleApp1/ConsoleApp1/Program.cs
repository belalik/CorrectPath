using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static List<string> allPossibleRoutes = new List<string>();

        static List<string> dirtyRoutes = new List<string>();

        static void Main(string[] args)
        {

            Console.WriteLine(CorrectPath(Console.ReadLine()));
            
        }

        public static string CorrectPath(string str)
        {
            CreateAllRoutes(str);
            allPossibleRoutes = CleanUp(dirtyRoutes);

            //Console.WriteLine("all possible size: "+allPossibleRoutes.Count);
            //Console.WriteLine("dirty size: "+dirtyRoutes.Count);
            //Console.WriteLine();
            //Console.WriteLine("trying for: "+str);
            //for (int i=0; i<allPossibleRoutes.Count; i++)
            //{
            //    Console.WriteLine(allPossibleRoutes.ElementAt(i));
            //}


            string path = Move(str);
            //Console.WriteLine("Tried with: "+str);
            //Console.WriteLine("FINAL PATH IS: "+path);
            //return path;
            return path;
            
        }

        public static string Move2 (string str)
        {


            return str;
        }

        public static string Move (string str)
        {
            
            Board board = new Board(5, 5);

            Referee referee = new Referee();

            string thePath = "";
            string remainingPath = "";

            for (int i=0; i<str.Length; i++)
            {

                //remaining path, excluding current char .. 
                remainingPath = str.Substring(i+1, str.Length - (i+1));
                
                
                char move = str[i];
                switch (move)
                {
                    case '?':

                        //CreateAllPossibleRoutes(str, thePath, remainingPath);
                        //CreateAllRoutes(str);

                        char nextMove = DecideNextMove(thePath, remainingPath);
                        board.Move(nextMove);
                        thePath += nextMove;

                        break;
                    case 'u':
                        if (referee.AllowedMove(board, 'u'))
                        {
                            board.Move('u');
                            thePath += 'u';
                        }
                        break;
                    case 'd':
                        if (referee.AllowedMove(board, 'd'))
                        {
                            board.Move('d');
                            thePath += 'd';
                        }
                        break;
                    case 'r':
                        if (referee.AllowedMove(board, 'r'))
                        {
                            board.Move('r');
                            thePath += 'r';
                        }
                        break;
                    case 'l':
                        if (referee.AllowedMove(board, 'l'))
                        {
                            board.Move('l');
                            thePath += 'l';
                        }
                        break;
                }
            }

            return thePath;
        } // Closes Move method.

        public static bool isValidPath(string path)
        {
            bool isValid = true;
            
            Board b = new Board(5, 5);
            Referee referee2 = new Referee();
            int i = 0;

            while (i<path.Length && isValid)
            {
                if (referee2.AllowedMove(b, path[i]))
                {
                    b.Move(path[i]);
                }
                else
                {
                    isValid = false;
                }
                i++;
            }

            if (b.ActiveCell.X != 4 || b.ActiveCell.Y != 4)
            {
                isValid = false;
            }
            if (isValid)
            {
                //Console.WriteLine("path: \"" + path + "\" is VALID");
            }
            else
            {
                //Console.WriteLine("path: \"" + path + "\" is NOT VALID");
            }
            
            //Console.WriteLine("Returning isValid="+isValid);
            return isValid;
        }

        public static char DecideNextMove(string pathSoFar, string remainingPath)
        {
            int correctPath = 0;
            char rightMove = 'x';

            
            for (int i=0; i<allPossibleRoutes.Count; i++)
            {
                
                if (isValidPath(allPossibleRoutes.ElementAt(i)))
                {
                    rightMove = allPossibleRoutes.ElementAt(i)[pathSoFar.Length];
                }
            }
            
            return rightMove;
        }

        public static void CreateAllRoutes(string str)
        {

            //Console.WriteLine("str is: " + str);
            
            bool keepTrying = true;
            int i = 0;

            string newstring1 = "";
            string newstring2 = "";
            string newstring3 = "";
            string newstring4 = "";

            if (str.Contains('?'))
            {
                char[] ch = str.ToCharArray();

                while (i < str.Length && keepTrying)
                {
                    if (ch[i] == '?')
                    {
                        keepTrying = false;

                        ch[i] = 'u';
                        newstring1 = new string(ch);
                        dirtyRoutes.Add(newstring1);

                        ch[i] = 'd';
                        newstring2 = new string(ch);
                        dirtyRoutes.Add(newstring2);

                        ch[i] = 'l';
                        newstring3 = new string(ch);
                        dirtyRoutes.Add(newstring3);

                        ch[i] = 'r';
                        newstring4 = new string(ch);
                        dirtyRoutes.Add(newstring4);
                    }
                    i++;
                }

                CreateAllRoutes(newstring1);
                CreateAllRoutes(newstring2);
                CreateAllRoutes(newstring3);
                CreateAllRoutes(newstring4);
            }

        }

        

        public static List<string> CleanUp(List<string> dirty)
        {
            List<string> cleaned = new List<string>();

            for (int i = 0; i < dirty.Count; i++)
            {
                if (!dirty.ElementAt(i).Contains("?"))
                {
                    cleaned.Add(dirty.ElementAt(i));
                }
            }

            return cleaned;

        }
    }

    class Board
    {
        public Cell[,] theBoard;
        public Cell activeCell;

        public Board(int rows, int columns) {
            
            theBoard = new Cell[rows, columns];

            for (int i=0; i<rows; i++)
            {
                for (int j=0; j<columns; j++)
                {
                    theBoard[i,j] = new Cell(i, j, false);
                }
            }
            //Console.WriteLine("Trying to output (3,4) x value: "+theBoard[2,3].X+" and Passed: "+ theBoard[2, 3].Passed);
            theBoard[0,0].Passed = true;
            activeCell = theBoard[0,0];
            //Console.WriteLine("Board initialized");
        }

        public Cell ActiveCell
        {
            get { return activeCell; }
            set { activeCell = value; }
        }

        public Cell[,] TheBoard
        {
            get { return theBoard; }
            //set { theBoard = value; }
        }

        public Cell GetAnyCell(int x, int y)
        {
            return theBoard[x, y];
        }

        public void Move(char move)
        {
            int currentX = ActiveCell.X;
            int currentY = ActiveCell.Y;

            // move to be called when we have already determined the move to be valid.
            switch (move)
            {
                case 'u':
                    GetAnyCell(currentX - 1, currentY).Passed = true;
                    ActiveCell = GetAnyCell(currentX - 1, currentY);
                    break;
                case 'd':
                    GetAnyCell(currentX + 1, currentY).Passed = true;
                    ActiveCell = GetAnyCell(currentX + 1, currentY);
                    break;
                case 'r':
                    GetAnyCell(currentX, currentY + 1).Passed = true;
                    ActiveCell = GetAnyCell(currentX, currentY + 1);
                    break;
                case 'l':
                    GetAnyCell(currentX, currentY - 1).Passed = true;
                    ActiveCell = GetAnyCell(currentX, currentY - 1);
                    break;
                
            }
            //Console.WriteLine("Moved "+move+" from cell ("+currentX+","+currentY+") to "+"("+this.ActiveCell.X+", "+this.ActiveCell.Y+")" );
        }
    }


    class Referee
    {

        /*
         *  Method which determines if a move is allowed (check boundaries AND if target cell has not been visited before. 
         *  
         *  Could also put the move itself here, simple else statement - but decided against it in order to keep method 
         *  simple and make it achieve only one certain thing (answer whether move is allowed).  Move has been decided to 
         *  be the Board's responsibility. 
         */
        public bool AllowedMove(Board b, char move)
        {
            bool valid = true;
            Cell c = b.ActiveCell;

            switch (move)
            {
                case 'u':
                    if (c.X == 0) {
                        valid = false;
                    }
                    else if (b.GetAnyCell(c.X - 1, c.Y).Passed)
                    {
                        valid = false;
                    }
                    break;
                case 'd':
                    if (b.ActiveCell.X == b.TheBoard.GetLength(0)-1)
                    {
                        valid = false;
                    }
                    else if (b.GetAnyCell(c.X + 1, c.Y).Passed)
                    {
                        valid = false;
                    }
                    break;
                case 'r':
                    //Console.WriteLine("Trying to move right with Y="+c.Y+" and The.Board.Length="+ (b.TheBoard.GetLength(1)-1));
                    if (c.Y == b.TheBoard.GetLength(1)-1)
                    {
                        valid = false;
                    }
                    else if (b.GetAnyCell(c.X, c.Y+1).Passed)
                    {
                        valid = false;
                    }
                    break;
                    
                case 'l':
                    if (c.Y == 0)
                    {
                        valid = false;
                    }
                    else if (b.GetAnyCell(c.X, c.Y - 1).Passed)
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


