using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8Puzzle
{
    class Answer
    {
        public PuzzleNode Node;
        public int f_Limit;
    }
    class RBFSSearch
    {
        public int[,] initialState = { { 7,2,4 }, { 5, 0, 6 }, { 8, 3, 1 } };
  
        public PuzzleNode Make_Node(int[,] state)
        {
            PuzzleNode n = new PuzzleNode();
           
            n.Action = ' ';
            n.Depth = 0;
            n.FEvaluate = 0;
            n.Parrent = null;
            n.PathCost = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    n.State[i, j] = state[i, j];
            return n;
        }
                
        public Answer RecursiveBestFirstSearch()
        {

            return RBFS(Make_Node(initialState), 10000);
        }
        public PuzzleNode GetBest(List<PuzzleNode> lst)
        {
            int min = 10000;
            PuzzleNode best=null;
            foreach (PuzzleNode n in lst)
            {
                if (n.FEvaluate < min)
                {
                    min = n.FEvaluate;
                    best = n;
                }
            }
            return best;
        }
        public int GetAlternativeBestValue(List<PuzzleNode> lst)
        {
            int min1 = 10000;
            int min2 = 10000;
            foreach (PuzzleNode n in lst)
            {
                if (n.FEvaluate < min1) { min2 = min1; min1= n.FEvaluate; continue; }
                if (n.FEvaluate < min2) { min2 = n.FEvaluate; } 
            }
            return min2;
        }
        public Answer RBFS(PuzzleNode node, int fLimit)
        {
            Answer ans = new Answer();
            if (node.GoalTest()) { ans.Node = node; ans.f_Limit = 0; return ans; }
            List<PuzzleNode> successors = node.Successors();
            if (successors.Count <= 0) { ans.Node = null; ans.f_Limit = 10000; return ans; }
            foreach (PuzzleNode s in successors)
                s.FEvaluate = Math.Max(s.PathCost + s.CalculateHManhattanDistance(), s.FEvaluate);
            PuzzleNode best;
            int alternative;
            while (true)
            {
                best = GetBest(successors);
                alternative = GetAlternativeBestValue(successors);
                if (best.FEvaluate > fLimit) { ans.f_Limit = best.FEvaluate; ans.Node = null; return ans; break; }
                Answer result = RBFS(best, Math.Min(fLimit, alternative));
                best.FEvaluate = result.f_Limit;
                if (result.Node != null) return result;
            }

                
        }

    }
}
