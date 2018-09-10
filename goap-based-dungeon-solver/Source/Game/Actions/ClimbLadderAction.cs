using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game.Actions
{
    class ClimbLadderAction : BaseAction
    {
        public override bool Execute(Scenario scenario, ActionArg arg)
        {
            var climbLadderArg = arg as ClimbLadderArg;
            if (climbLadderArg == null)
            {
                return false;
            }

            var character = climbLadderArg.Character;
            var ladder = climbLadderArg.LadderType;
            
            if (IsObjectAround(scenario, character.Position, ladder) != true)
            {
                return false;
            }

            scenario.SetClimbedUpLadder();

            return true;
        }
    }
}
