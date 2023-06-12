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
    public class TaskGoToTarget : BTNode
    {
        private Transform _transform;

        public TaskGoToTarget(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");

            if (Vector3.Distance(_transform.position, target.position) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, target.position, GuardBT.speed * Time.deltaTime);
            }

            nodeState = NodeState.RUNNING;
            return nodeState;
        }
    }
}
