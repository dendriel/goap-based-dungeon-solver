namespace Goap_Based_Dungeon_Solver.Source.Game.Actions.Args
{
    class CharacterActionArg : ActionArg
    {
        public DynamicElement Character { get; private set; }

        public CharacterActionArg(DynamicElement character)
        {
            Character = character;
        }
    }
}
