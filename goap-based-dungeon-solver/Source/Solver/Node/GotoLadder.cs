using Goap_Based_Dungeon_Solver.Source.Game;

namespace Source.Solver.Node
{
    class GotoLadder : ActionNode
    {
        public GotoLadder(bool createConditions=true)
            : base(ActionType.GOTO_LADDER)
        {
            if (createConditions)
            {
                Conditions = FindGotoConditions();
            }
            Effects = new[] { EffectType.CLOSE_TO_LADDER };
        }

        private EffectType[] FindGotoConditions()
        {
            var ladderPos = game.FindObjetPos(MapElements.LADDER);
            var playerPos = game.FindCharacterPos(MapElements.HERO);

            var path = game.FindShortestPath(playerPos, ladderPos);

            // Look for blocking objects or characters and create conditions.

            //return new[] { EffectType.CLOSE_TO_LADDER };
            return null;
        }
    }
}
