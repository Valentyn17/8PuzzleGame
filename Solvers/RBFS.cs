using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleGame.Solvers
{
    public class RBFS:Solver
    {
        private static  long infinity = Int64.MaxValue;
        public override void Solve(State state)
        {
            var root = state;
            var searchresult = Rbfs(state.CurrentBoard, root, root.Heuristic() + FindPat(root), infinity);
            if (searchresult.Item1.CurrentBoard.IsEqual(this.GoalState))
            {
                PrintResults(searchresult.Item1, searchresult.Item1.CurrentBoard);
            }
        }
        private (State, long) Rbfs(Board problem, State node, long nodef, long flimit)
        {
            if (node.CurrentBoard.IsEqual(this.GoalState))
            {
                //Console.WriteLine($"Fringe size:  {successors.Count}");
                return (node, flimit);
            }
            var zeroXAndY = node.CurrentBoard.IndexOfZero();
            var zeroX = zeroXAndY.Item1;
            var zeroY = zeroXAndY.Item2;
            var successors = this.GenerateChildrenStates(node, zeroX, zeroY);
            if (successors.Count == 0)
            {
                return (null, infinity);
            }
            if (successors.Count > this.MaxFringeSize)
            {
                this.MaxFringeSize = successors.Count;
            }

            var f = new long[successors.Count];
            for (int i = 0; i < successors.Count; i++)
            {
                f[i] = Math.Max(successors[i].Heuristic()+FindPat(successors[i]), nodef);
                NodesExpanded++;
            }
            while (true)
            {
                int bestIndex = GetBestFValueIndex(f);
                if (f[bestIndex] > 182000)
                {
                    break;
                }    

                    if (f[bestIndex]>flimit)
                {
                    return (null, f[bestIndex]);
                }
                int altindex = GetNextBestFValueIndex(f, bestIndex);
                var searchresult = Rbfs(problem, successors[bestIndex], f[bestIndex], Math.Min(flimit, f[altindex]));
                f[bestIndex] = searchresult.Item2;
                if (searchresult.Item1!=null)
                {
                    
                    return searchresult;
                }
            }
            return (null, infinity);
        }

        private static int GetBestFValueIndex(long[] f)
        {
            int lidx = 0;
            long lowestSoFar = infinity;

            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] < lowestSoFar)
                {
                    lowestSoFar = f[i];
                    lidx = i;
                }
            }

            return lidx;
        }

        private static int GetNextBestFValueIndex(long[] f, int bestIndex)
        {
            // array may only contain 1 item, therefore default to bestIndex
            int lidx = bestIndex;
            long lowestSoFar = infinity;

            for (int i = 0; i < f.Length; i++)
            {
                if (i != bestIndex && f[i] < lowestSoFar)
                {
                    lowestSoFar = f[i];
                    lidx = i;
                }
            }

            return lidx;
        }


        public override int FindPat(State state)
        {
            return base.FindPat(state);
        }

    }
}
