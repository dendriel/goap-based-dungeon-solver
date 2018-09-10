using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class IronKey : DynamicElement
    {
        public IronKey(Point startingPosition)
            : base(Resources.iron_key, startingPosition, MapElements.IRON_KEY, false, true) { }
    }
}
