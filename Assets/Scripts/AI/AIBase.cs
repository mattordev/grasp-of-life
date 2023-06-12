using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Authored & Written by @mattordev
/// 
/// Please see the licene.md document for usage.
/// </summary>
namespace GraspofLife.World.AI
{
    /// <summary>
    /// The Base AI Class. References the agent.
    /// </summary>
    public class AIBase : MonoBehaviour
    {
        // Reference to the Nav Mesh Agent
        public NavMeshAgent agent;

        // Start is called before the first frame update
        void Start()
        {
            if (!agent)
            {
                agent = GetComponent<NavMeshAgent>();
            }
        }
    }
}
