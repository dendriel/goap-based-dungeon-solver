using System.Collections.Generic;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class PlayerState
    {
        public bool HasClimbedUpLadder { get; private set; }

        private readonly List<MapElements> objects;

        public PlayerState()
        {
            objects = new List<MapElements>();
        }

        public void SetClimbedUpLadder()
        {
            HasClimbedUpLadder = true;
        }

        public void AddObject(MapElements obj)
        {
            objects.Add(obj);
        }

        public void RemObject(MapElements obj)
        {
            objects.Remove(obj);
        }

        public bool HasObject(MapElements obj)
        {
            return objects.Contains(obj);
        }
    }
}
