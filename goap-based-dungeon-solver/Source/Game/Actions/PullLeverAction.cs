using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;

namespace Goap_Based_Dungeon_Solver.Source.Game.Actions
{
    class PullLeverAction : BaseAction
    {
        public override bool Execute(Scenario scenario, ActionArg arg)
        {
            var pullLeverArg = arg as PullLeverArg;
            var character = pullLeverArg.Character;

            var lever = GetObjectAround(scenario, character.Position, MapElements.INACTIVE_LEVER) as Lever;
            if (lever == null)
            {
                return false;
            }

            var bridge = FindObject(scenario, MapElements.BROKEN_BRIDGE) as Bridge;
            if (bridge == null)
            {
                return false;
            }

            lever.IsActive = true;
            bridge.IsFixed = true;

            return true;
        }
    }
}
