using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GraspofLife.World.AI.BehaviourTree;

/// <summary>
/// Authored & Written by @mattordev
/// 
/// Please see the licene.md document for usage.
/// </summary>
namespace GraspofLife
{
    public class TaskAttack : BTNode
    {
        private Transform lastTarget;
        private EnemyManager enemyManager;

        private float attackTime = 1f;
        private float attackCounter = 0f;

        public TaskAttack(Transform transform)
        {

        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");
            if (target != lastTarget)
            {
                enemyManager = target.GetComponent<EnemyManager>();
                lastTarget = target;
            }

            attackCounter += Time.deltaTime;
            if (attackCounter >= attackTime)
            {
                bool enemyIsDead = enemyManager.TakeHit();
                if (enemyIsDead)
                {
                    ClearData("target");
                    // Switch back to walking to true, attack to false
                }
                else
                {
                    attackCounter = 0f;
                }
            }

            nodeState = NodeState.RUNNING;
            return nodeState;
        }
    }
}
