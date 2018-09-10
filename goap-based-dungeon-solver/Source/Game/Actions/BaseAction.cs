using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;
using System;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game.Actions
{
    abstract class BaseAction
    {
        public abstract bool Execute(Scenario scenario, ActionArg arg);

        protected static StaticElement GetTileAtPos(Scenario scenario, Point position)
        {
            return scenario.TilesList.Find(t => t.Position.Equals(position));
        }

        public static DynamicElement GetObjectAtPos(Scenario scenario, Point position)
        {
            return scenario.ObjectsList.Find(o => o.Position.Equals(position));
        }

        public static DynamicElement GetCharacterAtPos(Scenario scenario, Point position)
        {
            return scenario.CharactersList.Find(c => c.Position.Equals(position));
        }

        public static DynamicElement FindCharacter(Scenario scenario, MapElements type)
        {
            return scenario.CharactersList.Find(elem => elem.Type == type);
        }

        public static DynamicElement FindObject(Scenario scenario, MapElements type)
        {
            return scenario.ObjectsList.Find(elem => elem.Type == type);
        }

        protected ScenarioElement GetScenarioElementAtNextPos(Scenario scenario, Point nextPos)
        {
            return (ScenarioElement)GetObjectAtPos(scenario, nextPos) ?? GetTileAtPos(scenario, nextPos);
        }

        protected static Point FindNextPosition(Point currPos, Directions dir)
        {
            if (dir == Directions.UP)
            {
                return new Point(currPos.X, currPos.Y - 1);
            }

            if (dir == Directions.RIGHT)
            {
                return new Point(currPos.X + 1, currPos.Y);
            }

            if (dir == Directions.DOWN)
            {
                return new Point(currPos.X, currPos.Y + 1);
            }

            // LEFT.
            return new Point(currPos.X - 1, currPos.Y);
        }

        /// <summary>
        /// Check if the position is moveable.
        /// </summary>
        /// <param name="pos">The position to be checked.</param>
        /// <param name="mapState">Current map state.</param>
        /// <returns>true if the position is moveable; false otherwise.</returns>
        public static bool IsPositionMoveable(Scenario scenario, Point pos)
        {
            var tileAtPos = GetTileAtPos(scenario, pos);
            var objAtPos = GetObjectAtPos(scenario, pos);
            var charAtPos = GetCharacterAtPos(scenario, pos);

            // Can't move over another character.
            if (charAtPos != null)
            {
                return false;
            }

            // Can move if there is an object that allows it.
            if (objAtPos != null )
            {
                return objAtPos.Blockeable != true;
            }

            return tileAtPos.Blockeable != true;
        }

        public static bool IsOverObject(Scenario scenario, Point charPos)
        {
            var obj = GetObjectAtPos(scenario, charPos);

            return obj != null && obj.IsPickupable;
        }

        public static bool IsCharacterAround(Scenario scenario, Point charPos, MapElements type)
        {
            return IsDynamicElementAround(scenario, charPos, type, GetCharacterAtPos);
        }

        public static bool IsObjectAround(Scenario scenario, Point charPos, MapElements type)
        {
            return IsDynamicElementAround(scenario, charPos, type, GetObjectAtPos);
        }

        public static bool IsDynamicElementAround(
            Scenario scenario,
            Point charPos,
            MapElements type,
            Func<Scenario, Point, DynamicElement> GetElem
        )
        {
            return GetDynamicElementAround(scenario, charPos, type, GetElem) != null;
        }


        public static DynamicElement GetCharacterAround(Scenario scenario, Point charPos, MapElements type)
        {
            return GetDynamicElementAround(scenario, charPos, type, GetCharacterAtPos);
        }

        public static DynamicElement GetObjectAround(Scenario scenario, Point charPos, MapElements type)
        {
            return GetDynamicElementAround(scenario, charPos, type, GetObjectAtPos);
        }

        public static DynamicElement GetDynamicElementAround(
            Scenario scenario,
            Point charPos,
            MapElements type,
            Func<Scenario, Point, DynamicElement> GetElem
        )
        {
            var obj = GetElem(scenario, DirUtils.GetNorthFromPos(charPos));
            if (obj != null && obj.Type == type) return obj;

            obj = GetElem(scenario, DirUtils.GetEastFromPos(charPos));
            if (obj != null && obj.Type == type) return obj;

            obj = GetElem(scenario, DirUtils.GetSouthFromPos(charPos));
            if (obj != null && obj.Type == type) return obj;

            obj = GetElem(scenario, DirUtils.GetWestFromPos(charPos));
            if (obj != null && obj.Type == type) return obj;

            return null;
        }

        protected void RemoveCharacter(Scenario scenario, DynamicElement character)
        {
            scenario.RemCharacterFromScenario(character);
            character.Disable();
        }

        protected void RemoveObject(Scenario scenario, DynamicElement obj)
        {
            scenario.RemObjectFromScenario(obj);
            obj.Disable();
        }
    }
}
