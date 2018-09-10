using CSGameUtils;
using Goap_Based_Dungeon_Solver.Source.Game;
using Source.Solver.Node;
using System.Linq;

namespace Source.Solver
{
    class ActionNode : IAStarNode
    {
        public IAStarNode PrevNode { get; private set; }

        public int TotalCost { get { return FindPathCost(); } }

        public ActionType Action { get; private set; }

        public EffectType[] Effects { get; protected set; }

        public EffectType[] Conditions { get; protected set; }

        protected static GameManager game;

        public static void SetGameManager(GameManager manager)
        {
            game = manager;
        }

        public ActionNode(ActionType action)
        {
            Action = action;
        }

        public void SetPrevNode(IAStarNode node)
        {
            PrevNode = node;
        }

        protected virtual int FindPathCost()
        {
            return 0;
        }

        public bool HasEffect(EffectType effect)
        {
            return Effects.Contains(effect);
        }

        public override string ToString()
        {
            return Action.ToString();
        }
    }
}
