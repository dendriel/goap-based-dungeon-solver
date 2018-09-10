using CSGameUtils;
using Goap_Based_Dungeon_Solver.Source.Game;
using System.Collections.Generic;

namespace Source.Solver
{
    class Planner : AStarSolver
    {
        private readonly Queue<EffectType> unresolvedConditions;

        private readonly GameManager game;

        public Planner(IAStarNode _startingNode, GameManager game) : base(_startingNode, () => false)
        {
            this.game = game;
            ActionNode.SetGameManager(game);
            unresolvedConditions = new Queue<EffectType>();
        }

        protected override void RefreshOpenList()
        {
            openList.Clear();
        }

        private EffectType GetUnresolvedCondition()
        {
            var unsatisfied = EffectType.NONE;
            do
            {
                var condition = unresolvedConditions.Dequeue();
                if (IsSatisfied(condition) != true)
                {
                    unsatisfied = condition;
                    break;
                }

            } while (unresolvedConditions.Count > 0);

            return unsatisfied;
        }

        protected override IAStarNode[] FindNeighbors()
        {
            var currNode = CurrNode as ActionNode;

            EffectType condition = EffectType.NONE;
            if (currNode.Conditions == null)
            {
                condition = GetUnresolvedCondition();
                return FindNeighbors(condition, currNode);
            }

            var i = 0;
            for (; i < currNode.Conditions.Length; i++)
            {
                var currCondition = currNode.Conditions[i];
                if (IsSatisfied(currCondition) != true)
                {
                    condition = currCondition;
                    break;
                }
            }

            // Enqueue remaining conditions to be dequeued when an ActionNode have no conditions.
            for (var j = i + 1; j < currNode.Conditions.Length; j++)
            {
                unresolvedConditions.Enqueue(currNode.Conditions[j]);
            }

            return FindNeighbors(condition, currNode);
        }

        private bool IsSatisfied(EffectType condition)
        {
            // testing purpose only!!
            //if (condition == EffectType.DRINK_STORED) return true;

            return false;
        }

        private ActionNode[] FindNeighbors(EffectType condition, ActionNode parentNode)
        {
            var neighbors = NodeCreator.CreateNodesByEffects(condition);
            foreach (var n in neighbors)
            {
                n.SetPrevNode(parentNode);
            }

            return neighbors;
        }

        protected override bool HasFoundASolution()
        {
            var node = CurrNode as ActionNode;
            return (node.Conditions == null || node.Conditions.Length == 0) && unresolvedConditions.Count == 0;
        }
    }
}
