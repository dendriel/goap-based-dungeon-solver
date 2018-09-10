using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Ladder : DynamicElement
    {
        public Ladder(Point startingPosition)
            : base(Resources.ladder, startingPosition, MapElements.LADDER) { }
    }
}
