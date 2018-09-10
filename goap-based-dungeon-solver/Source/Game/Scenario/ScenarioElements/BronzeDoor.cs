using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class BronzeDoor : DynamicElement
    {
        public BronzeDoor(Point startingPosition)
            : base(Resources.bronze_door, startingPosition, MapElements.BRONZE_DOOR, true) { }
    }
}
