using CSGameUtils;
using Goap_Based_Dungeon_Solver.Source.Game;
using Goap_Based_Dungeon_Solver.Source.Game.Actions;
using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;
using Source.Solver;
using Source.Solver.Node;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Planner
{
    class PlanCreator
    {
        private GameManager game;

        public PlanCreator(GameManager game)
        {
            this.game = game;
        }

        public void ExecuteSolution(ActionType[] solution)
        {
            CreatePlan(solution);

            //PrintSolution(solution);
        }

        public void CreatePlan(ActionType[] solution)
        {
            var plan = new List<BaseAction>();
            foreach(var action in solution)
            {
                switch(action)
                {
                    case ActionType.GOTO_LADDER:
                        var ladderPos = game.FindObjetPos(MapElements.LADDER);
                        var playerPos = game.FindCharacterPos(MapElements.HERO);
                        var path = game.FindShortestPath(playerPos, ladderPos);
                        var moveArgs = PathToActions(playerPos, ladderPos, path);
                        break;
                    case ActionType.CLIMB_LADDER:
                        break;
                    default:
                        MessageBox.Show(string.Format("Unknow action type: {0}", action));
                        break;
                }
                //var actions = CreateActions(node);
                //plan.AddRange(actions);
            }
        }

        private MoveArg[] PathToActions(Point from, Point to, List<GoapSpNode> path)
        {
            var moveArgs = new List<MoveArg>();

            var fromId = game.PosToNodeId(from);
            var toId = game.PosToNodeId(to);

            var currPosId = fromId;

            foreach (var n in path)
            {
                if (n.ID == fromId || n.ID == toId)
                {
                    continue;
                }

                var nextMove = CreateNextMove(currPosId, n.ID);
                moveArgs.Add(nextMove);
                currPosId = n.ID;
            }

            return moveArgs.ToArray();
        }

        private MoveArg CreateNextMove(int currPosId, int nextPosId)
        {
            var character = game.FindCharacter(MapElements.HERO);
            var currPos = game.NodeIdToPos(currPosId);
            var nextPos = game.NodeIdToPos(nextPosId);

            // This logic cold be something "get relative dir between points"
            if (nextPos.X > currPos.X)
            {
                return new MoveArg(character, Directions.RIGHT);
            }

            if (nextPos.X < currPos.X)
            {
                return new MoveArg(character, Directions.LEFT);
            }

            if (nextPos.Y < currPos.Y)
            {
                return new MoveArg(character, Directions.UP);
            }

            return new MoveArg(character, Directions.DOWN);
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
