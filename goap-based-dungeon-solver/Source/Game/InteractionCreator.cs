using Goap_Based_Dungeon_Solver.Source.Game.Actions;
using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;
using System.Collections.Generic;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class InteractionCreator
    {
        private Scenario currScenario;

        private Dictionary<InteractionType, CreateActionCb> interactionsCreator;

        private delegate void CreateActionCb(DynamicElement character, out BaseAction action, out ActionArg arg);

        public InteractionCreator(Scenario scenario)
        {
            currScenario = scenario;
            Initialize();
        }

        public void SetCurrScenario(Scenario scenario)
        {
            currScenario = scenario;
        }

        private void Initialize()
        {
            interactionsCreator = new Dictionary<InteractionType, CreateActionCb>
            {
                { InteractionType.GET_OBJECT,   CreateGetObjectAction },
                { InteractionType.PULL_LEVER,   CreatePullLeverAction },
                { InteractionType.ATTACK_ENEMY, CreateAttackAction },
                { InteractionType.CLIMB_LADDER, CreateClimbLadderAction },
                { InteractionType.USE_KEY,      CreateUseKeyAction },
            };
        }

        public bool CreateInteraction(InteractionType type, DynamicElement character, out BaseAction action, out ActionArg arg)
        {
            if (interactionsCreator.ContainsKey(type) != true)
            {
                action = null;
                arg = null;
                return false;
            }

            interactionsCreator[type](character, out action, out arg);
            return true;
        }

        private void CreateGetObjectAction(DynamicElement character, out BaseAction action, out ActionArg arg)
        {
            action = new GetObjectAction();
            arg = new GetObjectArg(character);
        }

        private void CreatePullLeverAction(DynamicElement character, out BaseAction action, out ActionArg arg)
        {
            action = new PullLeverAction();
            arg = new PullLeverArg(character);
        }

        private void CreateAttackAction(DynamicElement character, out BaseAction action, out ActionArg arg)
        {
            action = new AttackAction();
            arg = new AttackActionArg(character, MapElements.ENEMY, MapElements.WEAPON, MapElements.SHIELD);
        }

        private void CreateClimbLadderAction(DynamicElement character, out BaseAction action, out ActionArg arg)
        {
            action = new ClimbLadderAction();
            arg = new ClimbLadderArg(character, MapElements.LADDER);
        }

        private void CreateUseKeyAction(DynamicElement character, out BaseAction action, out ActionArg arg)
        {
            var keyType = MapElements.EMPTY;
            var doorType = MapElements.EMPTY;

            if (BaseAction.IsObjectAround(currScenario, character.Position, MapElements.IRON_DOOR) &&
                currScenario.PlayerHasObject(MapElements.IRON_KEY))
            {
                keyType = MapElements.IRON_KEY;
                doorType = MapElements.IRON_DOOR;
            }

            if (BaseAction.IsObjectAround(currScenario, character.Position, MapElements.BRONZE_DOOR) &&
                currScenario.PlayerHasObject(MapElements.BRONZE_KEY))
            {
                keyType = MapElements.BRONZE_KEY;
                doorType = MapElements.BRONZE_DOOR;
            }

            arg = new UseKeyArg(character, keyType, doorType);
            action = new UseKeyAction();
        }
    }
}
