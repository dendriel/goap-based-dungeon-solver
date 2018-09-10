using CSGameUtils;
using Goap_Based_Dungeon_Solver.Source.Game.Actions;
using Source.Solver;
using Source.Solver.Node;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class SolutionExecutor
    {
        private GameManager game;

        public SolutionExecutor(GameManager game)
        {
            this.game = game;
        }

        public void ExecuteSolution(IAStarNode[] solution)
        {
            CreatePlan(solution);

            PrintSolution(solution);
        }

        public void CreatePlan(IAStarNode[] solution)
        {
            var plan = new List<BaseAction>();
            foreach(var node in solution)
            {
                //var actions = CreateActions(node);
                //plan.AddRange(actions);
            }
        }

        private BaseAction[] CreateActions(IAStarNode node)
        {
            var actions = new List<BaseAction>();

            var actionNode = node as ActionNode; // TODO TODO this doesn't seems right. Binding to solver!
            if (actionNode is ClimbLadder)
            {
                // use the interaction creator!
            }

            return null;
        }

        private void PrintSolution(IAStarNode[] solution)
        {
            var solutionText = new List<string>();
            for (var i = solution.Length - 1; i >= 0; i--)
            {
                var node = solution[i];
                solutionText.Add(node.ToString());
            }
            MessageBox.Show(string.Join(Environment.NewLine, solutionText), "Solution");
        }
    }
}
