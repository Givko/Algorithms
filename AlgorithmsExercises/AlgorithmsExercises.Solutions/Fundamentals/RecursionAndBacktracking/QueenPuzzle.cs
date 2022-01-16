using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.RecursionAndBacktracking
{
    public class QueenPuzzle
    {
        private static HashSet<int> attackedRows = new HashSet<int>();
        private static HashSet<int> attackedCols = new HashSet<int>();
        private static HashSet<int> attackedRightDiagonals = new HashSet<int>();
        private static HashSet<int> attackedLeftDiagonals = new HashSet<int>();

        public static void QueenSolutions()
        {
            var chessBoard = new char[8, 8]
            {
                { '-','-','-','-','-','-','-','-'},
                { '-','-','-','-','-','-','-','-'},
                { '-','-','-','-','-','-','-','-'},
                { '-','-','-','-','-','-','-','-'},
                { '-','-','-','-','-','-','-','-'},
                { '-','-','-','-','-','-','-','-'},
                { '-','-','-','-','-','-','-','-'},
                { '-','-','-','-','-','-','-','-'}
            };

            PlaceQueen(chessBoard, 0);
        }

        private static void PlaceQueen(char[,] chessBoard, int row)
        {
            if (row >= chessBoard.GetLength(0))
            {
                Print(chessBoard);
                return;
            }

            for (int j = 0; j < chessBoard.GetLength(1); j++)
            {
                if (IsSquareAttacked(row, j))
                {
                    continue;
                }

                chessBoard[row, j] = 'Q';
                attackedRows.Add(row);
                attackedCols.Add(j);
                attackedLeftDiagonals.Add(row - j);
                attackedRightDiagonals.Add(row + j);

                PlaceQueen(chessBoard, row + 1);

                chessBoard[row, j] = '-';
                attackedRows.Remove(row);
                attackedCols.Remove(j);
                attackedLeftDiagonals.Remove(row - j);
                attackedRightDiagonals.Remove(row + j);
            }
        }

        private static void Print(char[,] chessBoard)
        {
            for (int i = 0; i < chessBoard.GetLength(0); i++)
            {
                for (int j = 0; j < chessBoard.GetLength(1); j++)
                {
                    Console.Write(chessBoard[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static bool IsSquareAttacked(int row, int col)
        {
            return attackedRows.Contains(row) ||
                attackedCols.Contains(col) ||
                attackedRightDiagonals.Contains(row + col) ||
                attackedLeftDiagonals.Contains(row - col);
        }

        private static bool IsOutOfBoundary(char[,] chessBoard, int row, int col)
        {
            return row < 0 || col < 0 || row >= chessBoard.GetLength(0) || col >= chessBoard.GetLength(1);
        }
    }
}
