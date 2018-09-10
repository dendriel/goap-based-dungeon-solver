namespace Goap_Based_Dungeon_Solver.Source.Game.Actions.Args
{
    class UseKeyArg : CharacterActionArg
    {
        public MapElements KeyType { get; private set; }

        public MapElements DoorType { get; private set; }

        public UseKeyArg(DynamicElement character, MapElements keyType, MapElements doorType)
            : base(character)
        {
            KeyType = keyType;
            DoorType = doorType;
        }
    }
}
