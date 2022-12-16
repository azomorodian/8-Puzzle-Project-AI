using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8Puzzle
{
    class PuzzleNode
    {
        public static int[,] Goal = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
        public int[,] State = new int[3, 3];

        public int PathCost;
        public int Depth;
        public PuzzleNode Parrent;
        public int FEvaluate;
        public char Action;

        public bool GoalTest()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (State[i, j] != Goal[i, j]) return false;
            return true;
        }
        public List<PuzzleNode> Successors()
        {
            List<PuzzleNode> SuccessorsSet = new List<PuzzleNode>();
            int BlankRow = -1;
            int BlankCol = -1;
            for (int i = 0; i < 3 && BlankCol == -1; i++)
                for (int j = 0; j < 3 && BlankCol == -1; j++)
                    if (State[i, j] == 0) { BlankRow = i; BlankCol = j; }
            //UP
            PuzzleNode n = null;
            if (BlankRow > 0)
            {
                n = new PuzzleNode();
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        n.State[i, j] = State[i, j];
                    }
                n.State[BlankRow, BlankCol] = State[BlankRow - 1, BlankCol];
                n.State[BlankRow - 1, BlankCol] = 0;
                n.Depth = Depth + 1;
                n.PathCost= PathCost + 1;
                n.Parrent = this;
                n.FEvaluate = n.PathCost + n.CalculateHManhattanDistance();
                n.Action = 'U';
                SuccessorsSet.Add(n);
            }
            //Down
            if (BlankRow < 2)
            {
                n = new PuzzleNode();
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        n.State[i, j] = State[i, j];
                    }
                n.State[BlankRow, BlankCol] = State[BlankRow + 1, BlankCol];
                n.State[BlankRow + 1, BlankCol] = 0;
                n.Depth = Depth + 1;
                n.PathCost = PathCost + 1;
                n.Parrent = this;
                n.FEvaluate = n.PathCost + n.CalculateHManhattanDistance();
                n.Action = 'D';
                SuccessorsSet.Add(n);
            }
            //Left
            if (BlankCol > 0)
            {
                n = new PuzzleNode();
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        n.State[i, j] = State[i, j];
                    }
                n.State[BlankRow, BlankCol] = State[BlankRow, BlankCol - 1];
                n.State[BlankRow, BlankCol - 1] = 0;
                n.Depth = Depth + 1;
                n.PathCost = PathCost +1;
                n.Parrent = this; 
                n.FEvaluate = n.PathCost + n.CalculateHManhattanDistance();
                n.Action = 'L';
                SuccessorsSet.Add(n);
            }
            //Right
            if (BlankCol < 2)
            {
                n = new PuzzleNode();
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        n.State[i, j] = State[i, j];
                    }
                n.State[BlankRow, BlankCol] = State[BlankRow, BlankCol + 1];
                n.State[BlankRow, BlankCol + 1] = 0;
                n.Depth = Depth +1;
                n.PathCost = PathCost + 1;
                n.Parrent = this;
                n.FEvaluate = n.PathCost + n.CalculateHManhattanDistance();
                n.Action = 'R';
                SuccessorsSet.Add(n);
            }
            return SuccessorsSet;
        }
        public int CalculateHManhattanDistance()
        {
            int res = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                        for (int p = 0; p < 3; p++)
                        {
                            if ((State[i, j] == 0) ||(Goal[k,p]==0)) continue;
                            if (State[i, j] == Goal[k, p])
                            {

                                res += Math.Abs(i - k) + Math.Abs(j - p);
                            }
                        }
                }
            return res;
        }

    }
}
