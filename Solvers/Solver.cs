using System;
using System.Collections.Generic;
using System.Text;

namespace _8PuzzleGame.Solvers
{
    public abstract class Solver
    {
        protected int[,] GoalState { get; set; }
        protected int MaxFringeSize { get; set; }
        protected int SearchDepth { get; set; }
        protected int NodesExpanded { get; set; }

        public Solver()
        {
            this.GoalState = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8, } };
            SearchDepth = 0;
        }

        public abstract void Solve(State state);

        protected List<State> GenerateChildrenStates(State currentState, int x, int y)
        {
            var children = new List<State>();
            var leftState = currentState.MoveZeroToTheLeft(x, y);
            if (leftState != null )
            {

                if (currentState.Parent == null)
                {
                    leftState.LastMove = "left";
                    leftState.Parent = currentState;
                    leftState.SearchDepth++;
                    children.Add(leftState);
                }
                else if (!leftState.CurrentBoard.Equals(currentState.Parent.CurrentBoard))
                {
                    leftState.LastMove = "left";
                    leftState.Parent = currentState;
                    leftState.SearchDepth++;
                    children.Add(leftState);
                }

            }
            
            var rightState = currentState.MoveZeroToTheRight(x, y);
            if (rightState != null)
            {
                if (currentState.Parent == null)
                {
                    rightState.LastMove = "right";
                    rightState.Parent = currentState;
                    rightState.SearchDepth++;
                    children.Add(rightState);
                }
                else if (!rightState.CurrentBoard.Equals(currentState.Parent.CurrentBoard))
                {
                    rightState.LastMove = "right";
                    rightState.Parent = currentState;
                    rightState.SearchDepth++;
                    children.Add(rightState);
                }
            }
            
            var upState = currentState.MoveZeroUp(x, y);
            if (upState != null )
            {
                if (currentState.Parent == null)
                {
                    upState.LastMove = "up";
                    upState.Parent = currentState;
                    upState.SearchDepth++;
                    children.Add(upState);
                }
                else if (!upState.CurrentBoard.Equals(currentState.Parent.CurrentBoard))
                {
                    upState.LastMove = "up";
                    upState.Parent = currentState;
                    upState.SearchDepth++;
                    children.Add(upState);
                }
            }
            var downState = currentState.MoveZeroDown(x, y);
            if (downState != null)
            {
                if (currentState.Parent == null)
                {
                    downState.LastMove = "down";
                    downState.Parent = currentState;
                    downState.SearchDepth++;
                    children.Add(downState);
                }
                else if (!downState.CurrentBoard.Equals(currentState.Parent.CurrentBoard))
                {
                    downState.LastMove = "down";
                    downState.Parent = currentState;
                    downState.SearchDepth++;
                    children.Add(downState);
                }

            }




            return children;
        }

        public void PrintResults(State finalState, Board finalboard)
        {
            var searchDepth = finalState.SearchDepth;
            var path = FindPath(finalState);
            var pathToGoal = this.GetPathAsString(path);
            var costOfPath = path.Count;

            Console.WriteLine($"Path to goal: {pathToGoal}");
            Console.WriteLine($"Cost of path: {costOfPath}");
            Console.WriteLine($"Nodes expanded: {this.NodesExpanded}");
           
            Console.WriteLine($"Max fringe size: {this.MaxFringeSize}");
            Console.WriteLine($"Search depth: {searchDepth}");
            //Console.WriteLine($"Max search depth: {this.MaxSearchDepth}");
            finalboard.Output();
        }

        public virtual int FindPat(State state)
        {
            int path = 0;
            while (state.Parent != null)
            {
                path++;
                state = state.Parent;
            }

            return path;
        }

        public  List<string> FindPath(State state)
        {
            var path = new List<string>();
            while (state.Parent != null)
            {
                path.Add(state.LastMove);
                state = state.Parent;
            }

            return path;
        }

        private string GetPathAsString(List<string> path)
        {
            var sb = new StringBuilder();
            sb.Append("[");

            for (int i = path.Count - 1; i >= 0; i--)
            {
                sb.Append("'");
                sb.Append(path[i]);
                sb.Append("'");
                sb.Append(", ");
            }

            var pathToGoal = sb.ToString().TrimEnd(new[] { ',', ' ' });
            pathToGoal += "]";

            return pathToGoal;
        }
    }
}
