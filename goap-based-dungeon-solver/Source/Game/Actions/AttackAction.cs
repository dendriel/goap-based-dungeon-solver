using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;
using System.Collections.Generic;

namespace Goap_Based_Dungeon_Solver.Source.Game.Actions
{
    class AttackAction : BaseAction
    {
        public override bool Execute(Scenario scenario, ActionArg arg)
        {
            var attackArg = arg as AttackActionArg;
            if (attackArg == null)
            {
                return false;
            }

            var character = attackArg.Character;
            var enemyType = attackArg.EnemyType;
            var weapons = attackArg.Weapons;

            if (HasWeaponsToKillEnemy(scenario, weapons) != true)
            {
                return false;
            }

            var enemy = GetCharacterAround(scenario, character.Position, enemyType);
            if (enemy == null)
            {
                return false;
            }

            KillEnemy(scenario, enemy);

            return true;
        }

        private bool HasWeaponsToKillEnemy(Scenario scenario, IEnumerable<MapElements> weapons)
        {
            foreach (var w in weapons)
            {
                if (scenario.PlayerHasObject(w) != true)
                {
                    return false;
                }
            }
            return true;
        }

        private void KillEnemy(Scenario scenario, DynamicElement enemy)
        {
            RemoveCharacter(scenario, enemy);
        }
    }
}
