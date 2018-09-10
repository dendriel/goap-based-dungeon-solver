namespace Goap_Based_Dungeon_Solver.Source.Game.Actions.Args
{
    class ClimbLadderArg : CharacterActionArg
    {
        public MapElements LadderType { get; private set; }

        public ClimbLadderArg(DynamicElement character, MapElements ladderType)
            : base(character)
        {
            LadderType = ladderType;
        }
    }
}
