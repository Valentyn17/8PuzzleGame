using _8PuzzleGame.Solvers;

namespace _8PuzzleGame
{
    public class Startup
    {
        public static void Main()
        {
            // 1,2,5,3,4,0,6,7,8
            var arr = new int[3, 3] { { 7, 2, 4 }, { 5, 0, 6 }, { 8, 3, 1 } };
           //var arr = new int[3, 3] { { 0, 4, 2 }, { 5, 6, 8 }, { 7, 3, 1 } };
            var board = new Board(arr);
            var startingState = new State(board, null, null, 0);

            var Dls = new DlsSolver(27);
            var measurer = new PerformanceMeasurer();
            measurer.StartMeasuring();
            Dls.Solve(startingState);
            measurer.StopMeasuring();
            measurer.PrintResults();

            var rBFS = new RBFS();
            var measurer2 = new PerformanceMeasurer();
            measurer2.StartMeasuring();
            rBFS.Solve(startingState);
            measurer2.StopMeasuring();
            measurer2.PrintResults();
        }
    }
}
