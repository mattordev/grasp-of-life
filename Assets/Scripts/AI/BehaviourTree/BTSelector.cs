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
    /// Basically an OR logic gate. If one of the selectors childs succeeds, the selector will succeed. If all of the children fail, the entire selector will fail.
    /// </summary>
    public class BTSelector : BTNode
    {
        public BTSelector() : base() { }
        public BTSelector(List<BTNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (BTNode node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;

                    case NodeState.SUCCESS:
                        nodeState = NodeState.SUCCESS;
                        return nodeState;

                    case NodeState.RUNNING:
                        nodeState = NodeState.RUNNING;
                        return nodeState;

                    default:
                        continue;
                }
            }

            nodeState = NodeState.FAILURE;
            return nodeState;
        }

    }

}
