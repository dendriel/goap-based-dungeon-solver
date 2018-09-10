using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    /// <summary>
    /// An instance of a scenario. Handles dynamic scenario data.
    /// </summary>
    class Scenario
    {
        private ScenarioData data;

        private MapElementCreator mapElementCreator;

        public string Name { get { return data.Name; } }
        
        public string Info { get { return data.Info; } }
        
        public List<StaticElement> TilesList { get; private set; }
        
        public List<DynamicElement> ObjectsList { get; private set; }
        
        public List<DynamicElement> CharactersList { get; private set; }
        
        public DynamicElement Player { get; private set; } // CharactersList.Where(c => c.IsPlayer);
        
        public MapElements[,] Map;

        public PlayerState CurrPlayerState { get; private set; }

        /// <summary>
        /// Load a new scenario.
        /// </summary>
        /// <param name="_data">The scenario static data</param>
        public Scenario(ScenarioData _data)
        {
            data = _data;

            mapElementCreator = new MapElementCreator(CreateTile, CreateObject, CreateCharacter);

            Reload();
        }

        /// <summary>
        /// Reload scenario.
        /// </summary>
        public void Reload()
        {
            CurrPlayerState = new PlayerState();
            LoadElements(data.MapMatrix);
        }
        
        public void AddObjectToPlayer(MapElements obj)
        {
            CurrPlayerState.AddObject(obj);
        }

        public void RemObjectFromPlayer(MapElements obj)
        {
            CurrPlayerState.RemObject(obj);
        }

        public bool PlayerHasObject(MapElements obj)
        {
            return CurrPlayerState.HasObject(obj);
        }

        public void SetClimbedUpLadder()
        {
            CurrPlayerState.SetClimbedUpLadder();
        }

        public bool HasClimbedUpLadder()
        {
            return CurrPlayerState.HasClimbedUpLadder;
        }

        public void RemCharacterFromScenario(DynamicElement character)
        {
            CharactersList.Remove(character);
        }

        public void RemObjectFromScenario(DynamicElement obj)
        {
            ObjectsList.Remove(obj);
        }

        /// <summary>
        /// Load scenario elements.
        /// </summary>
        public void LoadElements(MapElements[,] mapState)
        {
            TilesList = new List<StaticElement>();
            ObjectsList = new List<DynamicElement>();
            CharactersList = new List<DynamicElement>();

            Map = mapState.Clone() as MapElements[,];

            for (var row = 0; row < mapState.GetLength(1); row++)
            {
                for (var col = 0; col < mapState.GetLength(0); col++)
                {
                    ParseMapElement(new Point(col, row), Map[row, col]);
                }
            }
        }

        private ScenarioElement CreateTile(Point position, StaticElement tile)
        {
            TilesList.Add(tile);
            return tile;
        }

        private ScenarioElement CreateObject(Point position, DynamicElement obj)
        {
            ObjectsList.Add(obj);
            return obj;
        }

        private ScenarioElement CreateCharacter(Point position, DynamicElement character)
        {
            CharactersList.Add(character);
            return character;
        }
        
        void ParseMapElement(Point position, MapElements type)
        {
            var elem = mapElementCreator.CreateMapElement(position, type);
            if (elem == null)
            {
                MessageBox.Show(string.Format("ParseMapElement doesn't know element {0}", type), "ParseMapElement Failure!");
                return;
            }

            ParseDynamicElement(elem, position);
        }

        private void ParseDynamicElement(ScenarioElement elem, Point position)
        {
            var dynamicElem = elem as DynamicElement;
            if (dynamicElem == null)
            {
                return;
            }

            var tile = GetTileForDynamicElement(dynamicElem, position);
            TilesList.Add(tile);
            dynamicElem.SetParentTile(tile);
            dynamicElem.BringSpriteToFront();
        }

        private StaticElement GetTileForDynamicElement(DynamicElement elem, Point position)
        {
            if (elem is Bridge)
            {
                return new Water(position);
            }

            return new Floor(position);
        }
    }
}
