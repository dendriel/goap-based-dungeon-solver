namespace Source.Solver.Node
{
    class ClimbLadder : ActionNode
    {
        public ClimbLadder()
            : base(ActionType.CLIMB_LADDER)
        {
            Conditions = new[] { EffectType.CLOSE_TO_LADDER };
            Effects = new[] { EffectType.FINISHED_LEVEL };
        }
    }
}
