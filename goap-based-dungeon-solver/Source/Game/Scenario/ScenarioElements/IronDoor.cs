using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class IronDoor : DynamicElement
    {
        public IronDoor(Point startingPosition)
            : base(Resources.iron_door, startingPosition, MapElements.IRON_DOOR, true) { }
    }
}
