namespace Goap_Based_Dungeon_Solver.Source.Game.Actions.Args
{
    class MoveArg : CharacterActionArg
    {
        public readonly Directions Direction;

        public MoveArg(DynamicElement character, Directions direction)
            : base(character)
        {
            Direction = direction;
        }

        public override string ToString()
        {
            return Direction.ToString();
        }
    }
}
