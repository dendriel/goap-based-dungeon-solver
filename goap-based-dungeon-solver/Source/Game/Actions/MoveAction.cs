using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;

namespace Goap_Based_Dungeon_Solver.Source.Game.Actions
{
    class MoveAction : BaseAction
    {
        public override bool Execute(Scenario scenario, ActionArg arg)
        {
            var moveData = arg as MoveArg;
            var character = moveData.Character;
            var direction = moveData.Direction;

            var nextPos = FindNextPosition(character.Position, direction);

            if (IsPositionMoveable(scenario, nextPos) != true)
            {
                return false;
            }

            character.Move(direction);

            var scenElemAtNextPos = GetScenarioElementAtNextPos(scenario, nextPos);
            character.SetParentTile(scenElemAtNextPos);

            return true;
        }
    }
}
