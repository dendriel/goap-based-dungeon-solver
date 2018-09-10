using System.Drawing;
using Goap_Based_Dungeon_Solver.Properties;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Enemy : DynamicElement
    {
        public Enemy(Point startingPosition)
            : base(Resources.enemy, startingPosition, MapElements.ENEMY)
        {}
    }
}
