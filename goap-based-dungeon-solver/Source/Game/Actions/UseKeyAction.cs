using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;

namespace Goap_Based_Dungeon_Solver.Source.Game.Actions
{
    class UseKeyAction : BaseAction
    {
        public override bool Execute(Scenario scenario, ActionArg arg)
        {
            var useKeyArg = arg as UseKeyArg;
            var character = useKeyArg.Character;
            var keyType = useKeyArg.KeyType;
            var doorType = useKeyArg.DoorType;

            if (scenario.PlayerHasObject(keyType) != true)
            {
                return false;
            }

            var door = GetObjectAround(scenario, character.Position, doorType);
            if (door == null)
            {
                return false;
            }

            OpenDoor(scenario, door);
            scenario.RemObjectFromPlayer(keyType);

            return true;
        }

        private void OpenDoor(Scenario scenario, DynamicElement door)
        {
            RemoveObject(scenario, door);
        }
    }
}
