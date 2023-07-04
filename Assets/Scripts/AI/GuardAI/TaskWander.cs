using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using GraspofLife.World.AI.BehaviourTree;

/// <summary>
/// Authored & Written by @mattordev
/// 
/// Please see the licene.md document for usage.
/// </summary>
namespace GraspofLife
{
    public class TaskWander : BTNode
    {
        private Transform _transform;

        private float wanderCooldown;
        private float timer;
        private float maxWanderDuration = 10f;
        private float timeLeftTillScriptCleanup;

        private Vector3 _homePoint;

        private Vector3 newPos;
        private bool isWandering;

        public GameObject wanderSphere;
        private bool attachedRadius = false;

        public TaskWander(Vector3 homePoint, Transform transform)
        {
            _homePoint = homePoint;
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            if (!isWandering)
            {
                // Start wandering
                StartWandering();
            }
            else if (timer >= wanderCooldown)
            {
                // Continue wandering or find a new point
                ContinueWandering();
            }

            timer += Time.deltaTime;
            timeLeftTillScriptCleanup -= Time.deltaTime;

            // Check if time for script cleanup
            if (timeLeftTillScriptCleanup <= 0f)
            {
                return NodeState.FAILURE;
            }

            return NodeState.RUNNING;
        }

        private void StartWandering()
        {
            // SetupWanderRadius();
            isWandering = true;
            wanderCooldown = Random.Range(1f, 10f);
            timer = 0f;
        }

        private void ContinueWandering()
        {
            Debug.Log("wandering");
            newPos = RandomWanderPoint(_transform.position, GuardBT.wanderRadius, -1);
            NavMeshHit hit;
            bool blocked = NavMesh.Raycast(_transform.position, newPos, out hit, NavMesh.AllAreas);
            Debug.DrawLine(_transform.position, newPos, blocked ? Color.red : Color.green);
            if (!blocked)
            {
                if (GuardBT.guardAgent.isOnNavMesh)
                    GuardBT.guardAgent.SetDestination(newPos);
                else
                    Debug.LogWarning(GuardBT.guardAgent.name + " is not on nav mesh");

                wanderCooldown = Random.Range(1f, 10f);
                timer = 0f;
            }
            else
            {
                timer = 0f;
            }
        }

        private void SetupWanderRadius()
        {
            // Set the size of the wander sphere to be the correct size
            SphereCollider sCol = wanderSphere.GetComponent<SphereCollider>();
            sCol.radius = GuardBT.wanderRadius;

            // Unparent it if false, this unlocks the radius from the AI (meaning it only wanders in place, and won't stray too far)
            if (attachedRadius == true)
            {
                // Make sure it's parented
                wanderSphere.transform.parent = _transform;
                // Reset the pos
                wanderSphere.transform.localPosition = Vector3.zero;
                return;
            }

            wanderSphere.transform.parent = null;
        }

        private static Vector3 RandomWanderPoint(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
            return navHit.position;
        }
    }
}
