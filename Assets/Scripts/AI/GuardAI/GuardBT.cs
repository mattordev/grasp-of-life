using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

using GraspofLife.World.AI.BehaviourTree;

/// <summary>
/// Authored & Written by @mattordev
/// 
/// Please see the licene.md document for usage.
/// </summary>
namespace GraspofLife
{
    public class GuardBT : BTTree
    {
        // The waypoints that the patrol task uses
        public UnityEngine.Transform[] waypoints;

        // define variables that will be used in the tasks and other classes.

        public static NavMeshAgent guardAgent;

        public static float speed = 2.5f;
        public static float fovRange = 6f;
        public static float attackRange = 2f;

        protected override BTNode SetupTree()
        {
            guardAgent = GetComponent<NavMeshAgent>();
            guardAgent.speed = speed;

            BTNode root = new BTSelector(new List<BTNode>
            {
                new BTSequence (new List<BTNode>
                {
                    new CheckEnemyInAttackRange(transform),
                    new TaskAttack(transform),
                }),
                new BTSequence (new List<BTNode>
                {
                    new CheckEnemyInFOVRange(transform),
                    new TaskGoToTarget(transform),
                }),
                new TaskPatrol(transform, waypoints),
            });

            return root;
        }
    }
}
