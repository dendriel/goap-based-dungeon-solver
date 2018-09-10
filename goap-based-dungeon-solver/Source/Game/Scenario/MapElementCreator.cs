using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class MapElementCreator
    {
        private Dictionary<MapElements, Func<Point, MapElements, ScenarioElement>> mapElementCreator;

        public delegate ScenarioElement CreateTileCb(Point position, StaticElement tile);

        public delegate ScenarioElement CreateObjectCb(Point position, DynamicElement obj);

        public delegate ScenarioElement CreateCharacterCb(Point position, DynamicElement character);

        private CreateTileCb CreateTile;

        private CreateObjectCb CreateObject;

        private CreateCharacterCb CreateCharacter;

        public MapElementCreator(CreateTileCb createTile, CreateObjectCb createObj, CreateCharacterCb createCharacter)
        {
            CreateTile = createTile;
            CreateObject = createObj;
            CreateCharacter = createCharacter;

            InitializeMapElementCreator();
        }

        public ScenarioElement CreateMapElement(Point position, MapElements type)
        {
            if (mapElementCreator.ContainsKey(type) != true)
            {
                return null;
            }

            return mapElementCreator[type](position, type);
        }

        private void InitializeMapElementCreator()
        {
            mapElementCreator = new Dictionary<MapElements, Func<Point, MapElements, ScenarioElement>>
            {
                { MapElements.EMPTY, (pos, id) => null },
                // Tiles.
                { MapElements.FLOOR, (pos, id) => CreateTile(pos, new Floor(pos)) },
                { MapElements.WALL, (pos, id) => CreateTile(pos, new Wall(pos)) },
                { MapElements.WATER, (pos, id) => CreateTile(pos, new Water(pos)) },
                // Objects.
                { MapElements.FIXED_BRIDGE, (pos, id) => CreateObject(pos, new Bridge(pos, true)) },
                { MapElements.BROKEN_BRIDGE, (pos, id) => CreateObject(pos, new Bridge(pos)) },
                { MapElements.LADDER, (pos, id) => CreateObject(pos, new Ladder(pos)) },
                { MapElements.IRON_DOOR, (pos, id) => CreateObject(pos, new IronDoor(pos)) },
                { MapElements.BRONZE_DOOR, (pos, id) => CreateObject(pos, new BronzeDoor(pos)) },
                { MapElements.IRON_KEY, (pos, id) => CreateObject(pos, new IronKey(pos)) },
                { MapElements.BRONZE_KEY, (pos, id) => CreateObject(pos, new BronzeKey(pos)) },
                { MapElements.INACTIVE_LEVER, (pos, id) => CreateObject(pos, new Lever(pos)) },
                { MapElements.ACTIVE_LEVER, (pos, id) => CreateObject(pos, new Lever(pos, true)) },
                { MapElements.WEAPON, (pos, id) => CreateObject(pos, new Weapon(pos)) },
                { MapElements.SHIELD, (pos, id) => CreateObject(pos, new Shield(pos)) },
                // Characters
                { MapElements.ENEMY, (pos, id) => CreateCharacter(pos, new Enemy(pos)) },
                { MapElements.HERO, (pos, id) => CreateCharacter(pos, new Hero(pos)) },
            };
        }
    }
}
