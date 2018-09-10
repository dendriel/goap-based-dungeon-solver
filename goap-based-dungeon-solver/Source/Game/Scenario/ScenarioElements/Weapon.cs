using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Weapon : DynamicElement
    {
        public Weapon(Point startingPosition)
            : base(Resources.weapon, startingPosition, MapElements.WEAPON, false, true) { }
    }
}
