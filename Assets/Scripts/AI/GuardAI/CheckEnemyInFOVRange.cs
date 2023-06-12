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
    public class CheckEnemyInFOVRange : BTNode
    {
        private static int _enemyLayerMask = 1 << 6;

        private Transform _transform;

        public CheckEnemyInFOVRange(Transform transform)
        {
            _transform = transform;
        }


        public override NodeState Evaluate()
        {
            object t = GetData("target");
            if (t == null)
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position, GuardBT.fovRange, _enemyLayerMask);

                if (colliders.Length > 0)
                {
                    parent.parent.SetData("target", colliders[0].transform);

                    //Play walk anim here

                    nodeState = NodeState.SUCCESS;
                    return nodeState;
                }

                nodeState = NodeState.FAILURE;
                return nodeState;
            }

            nodeState = NodeState.SUCCESS;
            return nodeState;
        }
    }
}
