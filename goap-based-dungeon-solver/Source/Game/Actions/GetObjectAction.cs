using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;

namespace Goap_Based_Dungeon_Solver.Source.Game.Actions
{
    class GetObjectAction : BaseAction
    {
        public override bool Execute(Scenario scenario, ActionArg arg)
        {
            var getObjArg = arg as GetObjectArg;
            var character = getObjArg.Character;

            var obj = GetObjectAtPos(scenario, character.Position);
            if (obj == null)
            {
                return false;
            }

            scenario.AddObjectToPlayer(obj.Type);
            scenario.RemObjectFromScenario(obj);

            var scenElemAtNextPos = GetScenarioElementAtNextPos(scenario, character.Position);
            character.SetParentTile(scenElemAtNextPos);

            obj.Disable();

            return true;
        }
    }
}
