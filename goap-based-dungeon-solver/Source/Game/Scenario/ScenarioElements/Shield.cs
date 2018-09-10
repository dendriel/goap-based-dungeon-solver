using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Shield : DynamicElement
    {
        public Shield(Point startingPosition)
            : base(Resources.shield, startingPosition, MapElements.SHIELD, false, true) { }
    }
}
