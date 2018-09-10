using System.Collections.Generic;

namespace Goap_Based_Dungeon_Solver.Source.Game.Actions.Args
{
    class AttackActionArg : CharacterActionArg
    {
        public MapElements EnemyType { get; private set; }

        public List<MapElements> Weapons { get; private set; }

        public AttackActionArg(DynamicElement character, MapElements enemyType, params MapElements[] weapons)
            : base(character)
        {
            EnemyType = enemyType;
            Weapons = new List<MapElements>(weapons);
        }
    }
}
