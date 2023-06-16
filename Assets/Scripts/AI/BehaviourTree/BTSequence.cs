using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Authored & Written by @mattordev
/// 
/// Please see the licene.md document for usage.
/// </summary>
namespace GraspofLife.World.AI.BehaviourTree
{
    /// <summary>
    /// Stops running when one of the children fail. If a child fails, then the sequence fails. If ALL of the sequences children succeed, then the sequence will succeed.
    /// </summary>
    public class BTSequence : BTNode
    {
        public BTSequence() : base() { }
        public BTSequence(List<BTNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (BTNode node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        nodeState = NodeState.FAILURE;
                        return nodeState;

                    case NodeState.SUCCESS:
                        continue;

                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;

                    default:
                        nodeState = NodeState.SUCCESS;
                        return nodeState;
                }
            }

            nodeState = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return nodeState;
        }
    }
}
