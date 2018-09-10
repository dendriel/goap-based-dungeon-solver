using Goap_Based_Dungeon_Solver.Source.Game.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class InteractionFinder
    {
        private Scenario currScenario;

        private List<Func<Point, InteractionType>> interactionFinders;

        public InteractionFinder(Scenario scenario)
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
            interactionFinders = new List<Func<Point, InteractionType>>
            {
                IsClimbLaddertInteraction,
                IsAttackEnemyInteraction,
                IsGetObjectInteraction,
                IsPullLeverInteraction,
                IsUseKeyInteraction,
            };
        }

        public InteractionType FindInteractionType(Point charPos)
        {
            foreach(var finder in interactionFinders)
            {
                var interaction = finder(charPos);
                if (interaction != InteractionType.NONE)
                {
                    return interaction;
                }
            }

            return InteractionType.NONE;
        }

        private InteractionType IsClimbLaddertInteraction(Point charPos)
        {
            return BaseAction.IsObjectAround(currScenario, charPos, MapElements.LADDER) ?
                InteractionType.CLIMB_LADDER : InteractionType.NONE;
        }

        private InteractionType IsAttackEnemyInteraction(Point charPos)
        {
            return BaseAction.IsCharacterAround(currScenario, charPos, MapElements.ENEMY) ?
                InteractionType.ATTACK_ENEMY : InteractionType.NONE;
        }

        private InteractionType IsGetObjectInteraction(Point charPos)
        {
            return BaseAction.IsOverObject(currScenario, charPos) ?
                InteractionType.GET_OBJECT : InteractionType.NONE;
        }

        private InteractionType IsPullLeverInteraction(Point charPos)
        {
            return BaseAction.IsObjectAround(currScenario, charPos, MapElements.INACTIVE_LEVER) ?
                InteractionType.PULL_LEVER : InteractionType.NONE;
        }

        private InteractionType IsUseKeyInteraction(Point charPos)
        {
            if ((BaseAction.IsObjectAround(currScenario, charPos, MapElements.IRON_DOOR) &&
                currScenario.PlayerHasObject(MapElements.IRON_KEY)) ||
                (BaseAction.IsObjectAround(currScenario, charPos, MapElements.BRONZE_DOOR) &&
                currScenario.PlayerHasObject(MapElements.BRONZE_KEY)))
            {
                return InteractionType.USE_KEY;
            }
            return InteractionType.NONE;
        }
    }
}
