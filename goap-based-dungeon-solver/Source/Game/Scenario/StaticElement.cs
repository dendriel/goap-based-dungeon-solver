using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    /// <summary>
    /// Static elements in the scenario.
    /// </summary>
    class StaticElement : ScenarioElement
    {
        /// <summary>
        /// Static elements types.
        /// </summary>
        public enum StaticElementType
        {
            FLOOR,
            WALL,
            WATER,
        }

        /// <summary>
        /// Create a new static element.
        /// </summary>
        /// <param name="image">Element appearance (image).</param>
        /// <param name="position">Element starting position.</param>
        /// <param name="blockeable">The element may block the path?</param>
        public StaticElement(Bitmap image, Point position, MapElements type, bool blockeable=false) : base(image, position, type)
        {
            Blockeable = blockeable;
        }
    }
}
