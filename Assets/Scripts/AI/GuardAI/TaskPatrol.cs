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
    public class TaskPatrol : BTNode
    {
        private Transform _transform;
        private Transform[] _waypoints;

        private int currentWayPointIndex = 0;
        private float speed = 2f;

        private float waitTime = 1f;
        private float waitCounter = 0f;
        private bool waiting = false;

        private float minimumDistance = 1.5f;

        public TaskPatrol(Transform transform, Transform[] waypoints)
        {
            _transform = transform;
            _waypoints = waypoints;
        }

        public override NodeState Evaluate()
        {
            if (waiting)
            {
                waitCounter += Time.deltaTime;
                if (waitCounter >= waitTime)
                    waiting = false;
            }
            else
            {
                Transform wp = _waypoints[currentWayPointIndex];

                if (Vector3.Distance(GuardBT.guardAgent.transform.position, wp.position) < minimumDistance)
                {
                    GuardBT.guardAgent.SetDestination(_transform.position);
                    // _transform.position = wp.position;
                    waitCounter = 0f;
                    waiting = true;

                    currentWayPointIndex = (currentWayPointIndex + 1) % _waypoints.Length;
                }
                else
                {
                    GuardBT.guardAgent.SetDestination(wp.position);
                    // _transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
                    // _transform.LookAt(wp.position);
                }
            }



            nodeState = NodeState.RUNNING;
            return nodeState;
        }
    }
}
