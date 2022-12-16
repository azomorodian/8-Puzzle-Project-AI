using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace _8Puzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(new ThreadStart(ThreadFunction), 1024*1024*300);
            T.Start();
            T.Join();
            if (ff.Node != null)
            {
                MessageBox.Show("The Problem Solved.");
                btnViewSolution.Enabled = true;
                 
            }
        }
        Answer ff = null;

        RBFSSearch rbfs = new RBFSSearch();    
        void ThreadFunction()
        {            
            ff = rbfs.RecursiveBestFirstSearch();

        }
        Presentation InitialState;
        Presentation GoalState;
        Presentation SolutionState;
        private void Form1_Load(object sender, EventArgs e)
        {
           InitialState = new Presentation(5, 70, 220, 220,pictureBox1);
           InitialState.StateCopy(ref rbfs.initialState);
           InitialState.Update();

           GoalState = new Presentation(5, 70, 220, 220, pictureBox2);
           GoalState.StateCopy(ref PuzzleNode.Goal);
           GoalState.Update();

           SolutionState = new Presentation(5, 70, 220, 220, pictureBox3);
           SolutionState.StateCopy(ref rbfs.initialState);
           SolutionState.Update();


        }
        int CurrentIndex;
        List<PuzzleNode> Solution = new List<PuzzleNode>();
        private void btnViewSolution_Click(object sender, EventArgs e)
        {
            PuzzleNode Current; 
            Current = ff.Node;
            Solution.Clear();
            while (Current != null)
            {
                Solution.Add(Current);
                Current = Current.Parrent;
            }
            CurrentIndex = Solution.Count - 1;
            MessageBox.Show(Solution.Count.ToString());
            timer1.Enabled = true;           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SolutionState.StateCopy(ref Solution[CurrentIndex].State);
            SolutionState.Update();
            if (CurrentIndex > 0)
                CurrentIndex--;
            else
            {
                timer1.Enabled = false;
                CurrentIndex = Solution.Count - 1;
            }

        }

    }
}
