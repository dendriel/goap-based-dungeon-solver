using Source.Solver.Node;
using System;
using System.Collections.Generic;

namespace Source.Solver
{
    static class NodeCreator
    {
        private static List<Tuple<ActionNode, Func<ActionNode>>> availableNodes = new List<Tuple<ActionNode, Func<ActionNode>>>()
        {
            new Tuple<ActionNode, Func<ActionNode>>(new ClimbLadder(), () => new ClimbLadder()),
            new Tuple<ActionNode, Func<ActionNode>>(new GotoLadder(false), () => new GotoLadder())
        };

        public static ActionNode[] CreateNodesByEffects(EffectType type)
        {
            var nodes = new List<ActionNode>();

            foreach(var node in availableNodes)
            {
                if (node.Item1.HasEffect(type))
                {
                    nodes.Add(node.Item2());
                }
            }

            return nodes.ToArray();
        }
    }
}
