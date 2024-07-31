using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class DealDamageAction : Action
    {
        Companion companion;
        public override void Awake()
        {
            companion = gameObject.GetComponent<Companion>();
        }

        protected override Status OnUpdate()
        {
            if (companion.enemyTarget.GetComponent<EmeraldAI.EmeraldAISystem>() != null)
            {
                companion.enemyTarget.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(100, EmeraldAI.EmeraldAISystem.TargetType.Player, gameObject.transform, 400);
            }
            return Status.Success;
        }
    }
}