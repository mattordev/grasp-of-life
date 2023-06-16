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
    /// The state of the node:
    /// 
    /// RUNNING - The node is still processing.
    /// SUCESS - The node completed sucessfully.
    /// FAILURE - The node failed for some reason.
    /// </summary>
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    /// <summary>
    /// Node class. Features the BTNode.Evaluate to see whether a task is running.
    /// Has references to child nodes and a dictionary for smart data linking and uses the lazy system.object type and a string identifier.
    /// </summary>
    public class BTNode
    {
        protected NodeState nodeState;

        public BTNode parent;
        protected List<BTNode> children = new List<BTNode>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public BTNode()
        {
            parent = null;
        }

        public BTNode(List<BTNode> children)
        {
            foreach (BTNode child in children)
                _Attach(child);
        }

        private void _Attach(BTNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            BTNode node = parent;

            // Try and get the data recursively.
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            BTNode node = parent;

            // Try and get the data recursively.
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}
