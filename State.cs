using System;

namespace _8PuzzleGame
{
    public class State 
    {
        private int heuristic;
        public State(Board currentBoard, State parent, string lastMove, int searchDepth)
        {
            this.CurrentBoard = currentBoard;
            this.Parent = parent;
            this.LastMove = lastMove;
            this.SearchDepth = searchDepth;
        }

        public Board CurrentBoard { get; set; }
        public State Parent { get; set; }
        public string LastMove { get; set; }
        public int SearchDepth { get; set; }

        public int Heuristic()
        {
            var matrix = this.CurrentBoard.Matrix;
            
            this.heuristic = 0;
            int k = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != k && matrix[i, j] != 0)
                        heuristic++;
                    k++;
                    
                }
            }

            return heuristic;
        }

        public State MoveZeroToTheLeft(int x, int y)
        {
            if (y == 0)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x, y - 1];
            clonedState.CurrentBoard.Matrix[x, y - 1] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State MoveZeroToTheRight(int x, int y)
        {
            if (y == (this.CurrentBoard.Matrix.Length / 3) - 1)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x, y + 1];
            clonedState.CurrentBoard.Matrix[x, y + 1] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State MoveZeroUp(int x, int y)
        {
            if (x == 0)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x - 1, y];
            clonedState.CurrentBoard.Matrix[x - 1, y] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State MoveZeroDown(int x, int y)
        {
            if (x == (this.CurrentBoard.Matrix.Length / 3) - 1)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x + 1, y];
            clonedState.CurrentBoard.Matrix[x + 1, y] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State Clone()
        {
            var newMatrix = new int[3, 3];

            for (int i = 0; i < this.CurrentBoard.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.CurrentBoard.Matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = this.CurrentBoard.Matrix[i, j];
                }
            }

            var clonedBoard = new Board(newMatrix);

            return new State(clonedBoard, this.Parent, this.LastMove, this.SearchDepth);
        }

    }
}
