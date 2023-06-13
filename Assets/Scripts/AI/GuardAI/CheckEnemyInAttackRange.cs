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
    public class CheckEnemyInAttackRange : BTNode
    {
        private static int _enemyLayerMask = 1 << 6;

        private Transform _transform;

        public CheckEnemyInAttackRange(Transform transform)
        {
            _transform = transform;
        }


        public override NodeState Evaluate()
        {
            object t = GetData("target");
            if (t == null)
            {
                nodeState = NodeState.FAILURE;
                return nodeState;
            }

            Transform target = (Transform)t;
            if (Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange)
            {
                // Set animation here for attacking true, walking false
                nodeState = NodeState.SUCCESS;
                return nodeState;
            }

            nodeState = NodeState.FAILURE;
            return nodeState;
        }
    }
}
