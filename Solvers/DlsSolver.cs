using System.Collections.Generic;

namespace _8PuzzleGame.Solvers
{
    public class DlsSolver : Solver
    {

        private bool isCutOff;
        private int depthLimit;
        public DlsSolver(int limit)
        {
            depthLimit = limit;
        }
        public override void Solve(State state)
        {
            var search = RecursiveDLS(state.CurrentBoard, state, depthLimit, out isCutOff);
            if (search != null)
            {
                PrintResults(search, search.CurrentBoard);
            }

        }

        private State RecursiveDLS(Board problem, State node, int limit, out bool isCutOff)
        {
            isCutOff = false;
            if (node.CurrentBoard.IsEqual(GoalState))
            {
                return node;
            }
            else if (limit == 0)
            {
                isCutOff = true;
                return null;
            }
            else
            {
                bool cutOffOccured = false;
                var zeroXAndY = node.CurrentBoard.IndexOfZero();
                var zeroX = zeroXAndY.Item1;
                var zeroY = zeroXAndY.Item2;
                var children = this.GenerateChildrenStates(node, zeroX, zeroY);
                foreach (var child in children)
                {
                    if (children.Count > this.MaxFringeSize)
                    {
                        this.MaxFringeSize = children.Count;
                    }
                    NodesExpanded++;
                    bool IsChildCutOff;
                    var res = RecursiveDLS(problem, child, limit - 1, out IsChildCutOff);
                    if (IsChildCutOff)
                    {
                        cutOffOccured = true;
                    }
                    else if (res != null)
                    {
                        return res;
                    }
                }
                isCutOff = cutOffOccured;
                return null;
            }
        }


        //public DlsSolver( int limit)
        //{
        //    MaxSearchDepth = limit;
        //}

        //public int MaxSearchDepth { get; }

        //public override void Solve(State state)
        //{
        //    var visited = new HashSet<Board>();
        //    var stack = new Stack<State>();

        //    stack.Push(state);
        //    visited.Add(state.CurrentBoard);

        //    while (stack.Count > 0)
        //    {
        //        if (stack.Count > this.MaxFringeSize)
        //        {
        //            this.MaxFringeSize = stack.Count;
        //        }

        //        state = stack.Pop();


        //        List<State> children;

        //        if (state.CurrentBoard.IsEqual(this.GoalState))
        //        {
        //            this.PrintResults(state, state.CurrentBoard);
        //            break;
        //        }

        //        this.NodesExpanded++;
        //        if (state.SearchDepth > this.MaxSearchDepth)
        //        {
        //            continue;
        //            //children=new List<State>();
        //        }
        //        else
        //        {
        //            var zeroXAndY = state.CurrentBoard.IndexOfZero();
        //            var zeroX = zeroXAndY.Item1;
        //            var zeroY = zeroXAndY.Item2;
        //            children = this.GenerateChildrenStates(state, zeroX, zeroY);
        //        }
        //        for (var i = 0; i < children.Count; i++)
        //        {
        //            var currentChild = children[i];

        //            if (!visited.Contains(currentChild.CurrentBoard))
        //            {
        //                stack.Push(currentChild);
        //                visited.Add(currentChild.CurrentBoard);

        //                //if (currentChild.SearchDepth > this.MaxSearchDepth)
        //                //{
        //                //    this.MaxSearchDepth = currentChild.SearchDepth;
        //                //}

        //            }
        //        }
        //    }
        //}
    }
}
