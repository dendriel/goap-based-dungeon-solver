
namespace Goap_Based_Dungeon_Solver.Source.Game
{
    /// <summary>
    /// Scenario Data dto.
    /// </summary>
    public class ScenarioData
    {
        /// <summary>
        /// Scenario name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Scenario information.
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Scenario width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Scenario height.
        /// </summary>
        public int Height { get; set; }
        
        /// <summary>
        /// Starting matrix from the map.
        /// </summary>
        public MapElements[,] MapMatrix { get; set; }
    }
}
