using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class BronzeKey : DynamicElement
    {
        public BronzeKey(Point startingPosition)
            : base(Resources.bronze_key, startingPosition, MapElements.BRONZE_KEY, false, true) { }
    }
}
